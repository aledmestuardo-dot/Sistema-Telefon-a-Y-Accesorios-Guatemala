using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace SistemaTelefonia.Datos
{
    /// <summary>Envía solicitudes REST autenticadas a Supabase usando la sesión y RLS.</summary>
    internal static class SupabaseRestDatos
    {
        private const string VariableUrl = "SUPABASE_URL";
        private const string VariableClavePublica = "SUPABASE_PUBLISHABLE_KEY";
        private static readonly HttpClient ClienteHttp = new HttpClient { Timeout = TimeSpan.FromSeconds(20) };
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        public static string Enviar(HttpMethod metodo, string ruta, object cuerpo, string preferencia,
            int inicioRango, int finRango)
        {
            string url;
            string clave;
            LeerConfiguracion(out url, out clave);

            string token = SesionSupabaseDatos.AccessToken;
            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("La sesión de Supabase no está activa. Cierra sesión e inicia de nuevo.");

            HttpResponseMessage respuesta = EnviarSolicitud(metodo, url, clave, token, ruta, cuerpo,
                preferencia, inicioRango, finRango);

            if ((int)respuesta.StatusCode == 401)
            {
                respuesta.Dispose();
                if (!RenovarSesion(url, clave))
                    throw new InvalidOperationException("La sesión de Supabase expiró. Inicia sesión nuevamente.");

                respuesta = EnviarSolicitud(metodo, url, clave, SesionSupabaseDatos.AccessToken,
                    ruta, cuerpo, preferencia, inicioRango, finRango);
            }

            using (respuesta)
            {
                string contenido = respuesta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!respuesta.IsSuccessStatusCode)
                    throw new InvalidOperationException("Supabase respondió con HTTP "
                        + (int)respuesta.StatusCode + ". " + MensajeError(contenido));
                return contenido;
            }
        }

        private static HttpResponseMessage EnviarSolicitud(HttpMethod metodo, string url, string clave,
            string token, string ruta, object cuerpo, string preferencia, int inicioRango, int finRango)
        {
            using (HttpRequestMessage solicitud = new HttpRequestMessage(metodo, url + ruta))
            {
                solicitud.Headers.TryAddWithoutValidation("apikey", clave);
                solicitud.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                if (!string.IsNullOrWhiteSpace(preferencia))
                    solicitud.Headers.TryAddWithoutValidation("Prefer", preferencia);
                if (inicioRango >= 0 && finRango >= inicioRango)
                {
                    solicitud.Headers.TryAddWithoutValidation("Range-Unit", "items");
                    solicitud.Headers.TryAddWithoutValidation("Range", inicioRango.ToString(CultureInfo.InvariantCulture)
                        + "-" + finRango.ToString(CultureInfo.InvariantCulture));
                }
                if (cuerpo != null)
                    solicitud.Content = new StringContent(Serializador.Serialize(cuerpo), Encoding.UTF8, "application/json");

                return ClienteHttp.SendAsync(solicitud).GetAwaiter().GetResult();
            }
        }

        private static bool RenovarSesion(string url, string clave)
        {
            string refreshToken = SesionSupabaseDatos.RefreshToken;
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                SesionSupabaseDatos.Limpiar();
                return false;
            }

            Dictionary<string, string> cuerpo = new Dictionary<string, string>
            {
                { "refresh_token", refreshToken }
            };

            using (HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post,
                url + "/auth/v1/token?grant_type=refresh_token"))
            {
                solicitud.Headers.TryAddWithoutValidation("apikey", clave);
                solicitud.Content = new StringContent(Serializador.Serialize(cuerpo), Encoding.UTF8, "application/json");
                using (HttpResponseMessage respuesta = ClienteHttp.SendAsync(solicitud).GetAwaiter().GetResult())
                {
                    if (!respuesta.IsSuccessStatusCode)
                    {
                        SesionSupabaseDatos.Limpiar();
                        return false;
                    }

                    string contenido = respuesta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Dictionary<string, object> datos = Serializador.Deserialize<Dictionary<string, object>>(contenido);
                    string accessToken = ObtenerTexto(datos, "access_token");
                    string nuevoRefresh = ObtenerTexto(datos, "refresh_token");
                    if (string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrWhiteSpace(nuevoRefresh))
                    {
                        SesionSupabaseDatos.Limpiar();
                        return false;
                    }

                    SesionSupabaseDatos.Guardar(accessToken, nuevoRefresh, SesionSupabaseDatos.IdAuth);
                    return true;
                }
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

        private static string ObtenerTexto(Dictionary<string, object> datos, string clave)
        {
            object valor;
            return datos != null && datos.TryGetValue(clave, out valor) && valor != null
                ? Convert.ToString(valor, CultureInfo.InvariantCulture)
                : null;
        }

        private static string MensajeError(string json)
        {
            try
            {
                Dictionary<string, object> error = Serializador.Deserialize<Dictionary<string, object>>(json);
                string mensaje = ObtenerTexto(error, "message") ?? ObtenerTexto(error, "msg")
                    ?? ObtenerTexto(error, "error");
                string detalle = ObtenerTexto(error, "details");
                if (!string.IsNullOrWhiteSpace(detalle)) mensaje += " " + detalle;
                return string.IsNullOrWhiteSpace(mensaje)
                    ? "Revisa los permisos, las políticas RLS y la disponibilidad del proyecto."
                    : mensaje;
            }
            catch
            {
                return "Revisa los permisos, las políticas RLS y la disponibilidad del proyecto.";
            }
        }
    }
}
