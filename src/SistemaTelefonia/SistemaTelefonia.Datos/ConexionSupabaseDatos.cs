using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Prueba Auth de Supabase y valida el perfil mediante REST con RLS.</summary>
    public class ConexionSupabaseDatos
    {
        private const string VariableUrl = "SUPABASE_URL";
        private const string VariableClavePublica = "SUPABASE_PUBLISHABLE_KEY";
        private static readonly HttpClient ClienteHttp = CrearClienteHttp();
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        /// <summary>Comprueba que la API Auth del proyecto Supabase responda.</summary>
        public bool ProbarConexion(out string mensaje)
        {
            try
            {
                string url;
                string clave;
                LeerConfiguracion(out url, out clave);

                using (HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, url + "/auth/v1/health"))
                {
                    solicitud.Headers.TryAddWithoutValidation("apikey", clave);
                    using (HttpResponseMessage respuesta = ClienteHttp.SendAsync(solicitud).GetAwaiter().GetResult())
                    {
                        if (!respuesta.IsSuccessStatusCode)
                        {
                            mensaje = (int)respuesta.StatusCode == 401 || (int)respuesta.StatusCode == 403
                                ? "Supabase rechazó la conexión. Revisa la clave publicable y los permisos del proyecto."
                                : "Supabase no respondió correctamente. Revisa la URL y la clave publicable del proyecto.";
                            return false;
                        }
                    }
                }

                mensaje = "Conexión con la API de Supabase establecida correctamente.";
                return true;
            }
            catch (InvalidOperationException ex)
            {
                mensaje = ex.Message;
                return false;
            }
            catch (Exception)
            {
                mensaje = "No se pudo conectar con Supabase. Comprueba Internet, URL y clave publicable.";
                return false;
            }
        }

        /// <summary>
        /// Inicia sesión con Supabase Auth y luego lee el perfil propio con el JWT,
        /// sujeto a la política RLS de Tbl_Usuarios.
        /// </summary>
        public UsuariosEntidad Autenticar(string usuarioOCorreo, string contrasenia)
        {
            string url;
            string clave;
            LeerConfiguracion(out url, out clave);

            string correo = ResolverCorreoLogin(usuarioOCorreo);
            Dictionary<string, object> solicitudLogin = new Dictionary<string, object>
            {
                { "email", correo },
                { "password", contrasenia }
            };

            using (HttpRequestMessage solicitud = new HttpRequestMessage(
                HttpMethod.Post, url + "/auth/v1/token?grant_type=password"))
            {
                solicitud.Headers.TryAddWithoutValidation("apikey", clave);
                solicitud.Content = new StringContent(Serializador.Serialize(solicitudLogin), Encoding.UTF8, "application/json");

                using (HttpResponseMessage respuesta = ClienteHttp.SendAsync(solicitud).GetAwaiter().GetResult())
                {
                    string contenido = respuesta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!respuesta.IsSuccessStatusCode)
                    {
                        if ((int)respuesta.StatusCode == 400 || (int)respuesta.StatusCode == 401)
                            return null;

                        throw new InvalidOperationException("Supabase Auth respondió con HTTP "
                            + (int)respuesta.StatusCode + ". " + MensajeApi(contenido));
                    }

                    Dictionary<string, object> resultado = DeserializarObjeto(contenido);
                    string token = ObtenerTexto(resultado, "access_token");
                    Dictionary<string, object> usuarioAuth = ObtenerObjeto(resultado, "user");
                    string idAuth = ObtenerTexto(usuarioAuth, "id");
                    string refreshToken = ObtenerTexto(resultado, "refresh_token");

                    if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(idAuth))
                        throw new InvalidOperationException("Supabase autenticó, pero la respuesta no incluyó la sesión esperada.");

                    try
                    {
                        UsuariosEntidad perfil = LeerPerfilPropio(url, clave, token, idAuth);
                        if (perfil == null)
                        {
                            try { CerrarSesion(url, clave, token); }
                            catch { /* La respuesta de acceso no debe depender del cierre remoto. */ }
                            return null;
                        }

                        SesionSupabaseDatos.Guardar(token, refreshToken, idAuth);
                        return perfil;
                    }
                    catch
                    {
                        try { CerrarSesion(url, clave, token); }
                        catch { /* Se descarta localmente la sesión aunque Auth no responda. */ }
                        throw;
                    }
                }
            }
        }

        /// <summary>Revoca la sesión Auth temporal usada en la prueba de acceso.</summary>
        public void CerrarSesionActual()
        {
            string token = SesionSupabaseDatos.AccessToken;
            SesionSupabaseDatos.Limpiar();
            if (string.IsNullOrWhiteSpace(token)) return;

            try
            {
                string url;
                string clave;
                LeerConfiguracion(out url, out clave);
                CerrarSesion(url, clave, token);
            }
            catch
            {
                // El token se elimina de memoria aunque el servicio no responda al cierre.
            }
        }

        private static UsuariosEntidad LeerPerfilPropio(string url, string clave, string token, string idAuth)
        {
            string columnas = "CodigoUsuario,TipoUsuario,Nombre,CorreoElectronico,FechaCreacion,FechaUltimoAcceso,Estado,EsDueno";
            string consulta = url + "/rest/v1/Tbl_Usuarios?select=" + columnas
                + "&AuthUserId=eq." + Uri.EscapeDataString(idAuth);

            using (HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, consulta))
            {
                solicitud.Headers.TryAddWithoutValidation("apikey", clave);
                solicitud.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                using (HttpResponseMessage respuesta = ClienteHttp.SendAsync(solicitud).GetAwaiter().GetResult())
                {
                    string contenido = respuesta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!respuesta.IsSuccessStatusCode)
                        throw new InvalidOperationException("No fue posible validar el perfil en Supabase. HTTP "
                            + (int)respuesta.StatusCode + ". " + MensajeApi(contenido));

                    ArrayList perfiles = Serializador.Deserialize<ArrayList>(contenido);
                    if (perfiles == null || perfiles.Count == 0) return null;
                    if (perfiles.Count > 1)
                        throw new InvalidOperationException("El identificador Auth está vinculado a más de un perfil.");

                    Dictionary<string, object> fila = perfiles[0] as Dictionary<string, object>;
                    if (fila == null) throw new InvalidOperationException("La respuesta del perfil tiene un formato inesperado.");

                    object activo;
                    if (!fila.TryGetValue("Estado", out activo) || !Convert.ToBoolean(activo, CultureInfo.InvariantCulture))
                        return null;

                    return new UsuariosEntidad
                    {
                        CodigoUsuario = Convert.ToInt32(fila["CodigoUsuario"], CultureInfo.InvariantCulture),
                        TipoUsuario = ObtenerTexto(fila, "TipoUsuario"),
                        Nombre = ObtenerTexto(fila, "Nombre"),
                        CorreoElectronico = ObtenerTexto(fila, "CorreoElectronico"),
                        FechaCreacion = Convert.ToDateTime(fila["FechaCreacion"], CultureInfo.InvariantCulture),
                        FechaUltimoAcceso = ObtenerFechaNullable(fila, "FechaUltimoAcceso"),
                        Estado = true,
                        EsDueno = fila.ContainsKey("EsDueno") && Convert.ToBoolean(fila["EsDueno"], CultureInfo.InvariantCulture),
                        AuthUserId = idAuth
                    };
                }
            }
        }

        private static DateTime? ObtenerFechaNullable(Dictionary<string, object> fila, string clave)
        {
            object valor;
            if (!fila.TryGetValue(clave, out valor) || valor == null) return null;
            return DateTime.Parse(Convert.ToString(valor, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind);
        }

        private static void CerrarSesion(string url, string clave, string token)
        {
            using (HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, url + "/auth/v1/logout"))
            {
                solicitud.Headers.TryAddWithoutValidation("apikey", clave);
                solicitud.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                using (ClienteHttp.SendAsync(solicitud).GetAwaiter().GetResult()) { }
            }
        }

        private static string ResolverCorreoLogin(string usuarioOCorreo)
        {
            string identificador = (usuarioOCorreo ?? string.Empty).Trim();
            if (identificador.IndexOf('@') >= 0) return identificador;

            switch (identificador.ToLowerInvariant())
            {
                case "alex": return "alexestuardodonismorales26@gmail.com";
                case "gabriel": return "galvarezo3@miumg.edu.gt";
                case "homar": return "lhernandezl38@miumg.edu.gt";
                default:
                    throw new InvalidOperationException("En Supabase usa el correo de la cuenta o uno de los usuarios iniciales: Alex, Gabriel u Homar.");
            }
        }

        private static void LeerConfiguracion(out string url, out string clave)
        {
            url = ConfiguracionLocalDatos.Obtener(VariableUrl);
            clave = ConfiguracionLocalDatos.Obtener(VariableClavePublica);

            Uri direccion;
            if (!Uri.TryCreate(url, UriKind.Absolute, out direccion)
                || (direccion.Scheme != Uri.UriSchemeHttps && direccion.Host != "localhost"))
                throw new InvalidOperationException("Configura SUPABASE_URL en las variables de entorno o en %LOCALAPPDATA%\\SistemaTelefonia\\.env.");
            if (string.IsNullOrWhiteSpace(clave))
                throw new InvalidOperationException("Configura SUPABASE_PUBLISHABLE_KEY en las variables de entorno o en %LOCALAPPDATA%\\SistemaTelefonia\\.env.");
            url = url.TrimEnd('/');
        }

        private static HttpClient CrearClienteHttp()
        {
            return new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        }

        private static Dictionary<string, object> DeserializarObjeto(string json)
        {
            Dictionary<string, object> objeto = Serializador.Deserialize<Dictionary<string, object>>(json);
            if (objeto == null) throw new InvalidOperationException("Supabase devolvió una respuesta JSON no válida.");
            return objeto;
        }

        private static Dictionary<string, object> ObtenerObjeto(Dictionary<string, object> objeto, string clave)
        {
            object valor;
            return objeto.TryGetValue(clave, out valor) ? valor as Dictionary<string, object> : null;
        }

        private static string ObtenerTexto(Dictionary<string, object> objeto, string clave)
        {
            if (objeto == null) return null;
            object valor;
            return objeto.TryGetValue(clave, out valor) && valor != null
                ? Convert.ToString(valor, CultureInfo.InvariantCulture)
                : null;
        }

        private static string MensajeApi(string json)
        {
            try
            {
                Dictionary<string, object> error = DeserializarObjeto(json);
                return ObtenerTexto(error, "msg") ?? ObtenerTexto(error, "message")
                    ?? ObtenerTexto(error, "error_description") ?? "Revisa la configuración del proyecto.";
            }
            catch
            {
                return "Revisa la configuración del proyecto y su disponibilidad.";
            }
        }
    }

    /// <summary>Sesión temporal en memoria; nunca se escribe en archivos locales.</summary>
    public static class SesionSupabaseDatos
    {
        public static string AccessToken { get; private set; }
        public static string RefreshToken { get; private set; }
        public static string IdAuth { get; private set; }

        internal static void Guardar(string accessToken, string refreshToken, string idAuth)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            IdAuth = idAuth;
        }

        public static void Limpiar()
        {
            AccessToken = null;
            RefreshToken = null;
            IdAuth = null;
        }
    }
}
