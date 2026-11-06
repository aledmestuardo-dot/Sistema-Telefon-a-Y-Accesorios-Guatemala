using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Script.Serialization;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones PostgREST reutilizables para entidades con claves foráneas.</summary>
    internal sealed class CrudRelacionalSupabaseDatos
    {
        private const int TamanoPagina = 500;
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        public List<T> Listar<T>(string tabla, string clave) where T : new()
        {
            try
            {
                List<T> resultado = new List<T>();
                int inicio = 0;
                while (true)
                {
                    string ruta = Ruta(tabla) + "?select=*&order=" + clave + ".asc";
                    List<T> pagina = LeerFilas<T>(SupabaseRestDatos.Enviar(HttpMethod.Get, ruta, null, null,
                        inicio, inicio + TamanoPagina - 1));
                    resultado.AddRange(pagina);
                    if (pagina.Count < TamanoPagina) return resultado;
                    inicio += TamanoPagina;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar " + tabla + " en Supabase. " + ex.Message, ex);
            }
        }

        /// <summary>Lee solo campos no sensibles de usuarios para listas FK; no habilita CRUD de usuarios.</summary>
        public List<SistemaTelefonia.Entidad.UsuariosEntidad> ListarUsuariosCatalogo()
        {
            const string rutaBase = "/rest/v1/Tbl_Usuarios?select=CodigoUsuario,TipoUsuario,Nombre,CorreoElectronico,FechaCreacion,FechaUltimoAcceso,Estado&Estado=eq.true&order=CodigoUsuario.asc";
            try
            {
                List<SistemaTelefonia.Entidad.UsuariosEntidad> resultado = new List<SistemaTelefonia.Entidad.UsuariosEntidad>();
                int inicio = 0;
                while (true)
                {
                    string json = SupabaseRestDatos.Enviar(HttpMethod.Get, rutaBase, null, null,
                        inicio, inicio + TamanoPagina - 1);
                    // Este catálogo es una proyección segura: no incluye Contrasenia,
                    // EsDueno ni AuthUserId. No usar el mapeador de entidad completa,
                    // que exige todas las propiedades de valor aunque no estén en SELECT.
                    List<SistemaTelefonia.Entidad.UsuariosEntidad> pagina = LeerObjetos(json)
                        .Select(MaterializarUsuarioCatalogo).ToList();
                    resultado.AddRange(pagina);
                    if (pagina.Count < TamanoPagina) return resultado;
                    inicio += TamanoPagina;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible cargar usuarios activos para el catálogo de empleados en Supabase. " + ex.Message, ex);
            }
        }

        private static SistemaTelefonia.Entidad.UsuariosEntidad MaterializarUsuarioCatalogo(
            Dictionary<string, object> fila)
        {
            object ultimoAcceso;
            DateTime? fechaUltimoAcceso = null;
            if (fila.TryGetValue("FechaUltimoAcceso", out ultimoAcceso) && ultimoAcceso != null)
                fechaUltimoAcceso = DateTime.Parse(Convert.ToString(ultimoAcceso, CultureInfo.InvariantCulture),
                    CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

            return new SistemaTelefonia.Entidad.UsuariosEntidad
            {
                CodigoUsuario = Convert.ToInt32(fila["CodigoUsuario"], CultureInfo.InvariantCulture),
                TipoUsuario = Convert.ToString(fila["TipoUsuario"], CultureInfo.InvariantCulture),
                Nombre = Convert.ToString(fila["Nombre"], CultureInfo.InvariantCulture),
                CorreoElectronico = Convert.ToString(fila["CorreoElectronico"], CultureInfo.InvariantCulture),
                FechaCreacion = DateTime.Parse(Convert.ToString(fila["FechaCreacion"], CultureInfo.InvariantCulture),
                    CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                FechaUltimoAcceso = fechaUltimoAcceso,
                Estado = Convert.ToBoolean(fila["Estado"], CultureInfo.InvariantCulture)
            };
        }

        public List<T> Buscar<T>(string tabla, string clave, string criterio) where T : new()
        {
            string texto = (criterio ?? string.Empty).Trim();
            List<T> filas = Listar<T>(tabla, clave);
            if (texto.Length == 0) return filas;
            PropertyInfo[] campos = Campos(typeof(T)).Where(p => TipoBase(p.PropertyType) == typeof(string)
                || TipoBase(p.PropertyType) == typeof(int)).ToArray();
            return filas.Where(fila => campos.Any(campo =>
            {
                object valor = campo.GetValue(fila, null);
                return valor != null && Convert.ToString(valor, CultureInfo.InvariantCulture)
                    .IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) >= 0;
            })).ToList();
        }

        public int Agregar<T>(string tabla, string clave, HashSet<string> nulos, T entidad)
        {
            try
            {
                string ruta = Ruta(tabla) + "?select=" + clave;
                string json = SupabaseRestDatos.Enviar(HttpMethod.Post, ruta,
                    CrearDatos(entidad, clave, nulos), "return=representation", -1, -1);
                List<Dictionary<string, object>> filas = LeerObjetos(json);
                if (filas.Count == 0) throw new InvalidOperationException("Supabase no devolvió la clave del registro creado.");
                return Convert.ToInt32(filas[0][clave], CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar el registro en " + tabla + " de Supabase. " + ex.Message, ex);
            }
        }

        public void Editar<T>(string tabla, string clave, int id, HashSet<string> nulos, T entidad)
        {
            try
            {
                string ruta = Ruta(tabla) + "?" + clave + "=eq." + id.ToString(CultureInfo.InvariantCulture)
                    + "&select=" + clave;
                ConfirmarFila(SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta,
                    CrearDatos(entidad, clave, nulos), "return=representation", -1, -1), tabla, "editar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar el registro en " + tabla + " de Supabase. " + ex.Message, ex);
            }
        }

        public void Inactivar(string tabla, string clave, int id)
        {
            try
            {
                string ruta = Ruta(tabla) + "?" + clave + "=eq." + id.ToString(CultureInfo.InvariantCulture)
                    + "&select=" + clave;
                Dictionary<string, object> datos = new Dictionary<string, object> { { "Estado", false } };
                ConfirmarFila(SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, datos,
                    "return=representation", -1, -1), tabla, "inactivar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible inactivar el registro en " + tabla + " de Supabase. " + ex.Message, ex);
            }
        }

        public List<T> ListarRelacionados<T>(string tabla, string clave, string claveForanea, int id) where T : new()
        {
            try
            {
                string ruta = Ruta(tabla) + "?select=*&" + claveForanea + "=eq."
                    + id.ToString(CultureInfo.InvariantCulture) + "&order=" + clave + ".asc";
                List<T> resultado = new List<T>();
                int inicio = 0;
                while (true)
                {
                    List<T> pagina = LeerFilas<T>(SupabaseRestDatos.Enviar(HttpMethod.Get, ruta, null, null,
                        inicio, inicio + TamanoPagina - 1));
                    resultado.AddRange(pagina);
                    if (pagina.Count < TamanoPagina) return resultado;
                    inicio += TamanoPagina;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar los registros relacionados en Supabase. " + ex.Message, ex);
            }
        }

        public int GuardarMaestroDetalle(string tablaMaestro, string claveMaestro, string tablaDetalle,
            string claveDetalle, string claveForanea, int id, object maestro, IEnumerable detalles, string accion,
            HashSet<string> nulosMaestro, HashSet<string> nulosDetalle)
        {
            try
            {
                List<Dictionary<string, object>> filasDetalle = new List<Dictionary<string, object>>();
                if (detalles != null)
                    foreach (object detalle in detalles)
                        filasDetalle.Add(CrearDatos(detalle, claveDetalle, nulosDetalle, true));

                Dictionary<string, object> argumentos = new Dictionary<string, object>
                {
                    { "p_tabla_maestro", tablaMaestro },
                    { "p_clave_maestro", claveMaestro },
                    { "p_tabla_detalle", tablaDetalle },
                    { "p_clave_detalle", claveDetalle },
                    { "p_clave_foranea", claveForanea },
                    { "p_codigo_maestro", id },
                    { "p_maestro", maestro == null ? null : CrearDatos(maestro, claveMaestro, nulosMaestro) },
                    { "p_detalles", filasDetalle },
                    { "p_accion", accion }
                };
                string json = SupabaseRestDatos.Enviar(HttpMethod.Post,
                    "/rest/v1/rpc/GuardarMaestroDetalle", argumentos, null, -1, -1);
                if (string.IsNullOrWhiteSpace(json)) return id;
                object valor = Serializador.DeserializeObject(json);
                return valor == null ? id : Convert.ToInt32(valor, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible guardar el encabezado y sus detalles en Supabase. " + ex.Message, ex);
            }
        }

        private static string Ruta(string tabla)
        {
            return "/rest/v1/" + Uri.EscapeDataString(tabla);
        }

        private static Dictionary<string, object> CrearDatos(object entidad, string clave,
            HashSet<string> nulos, bool incluirClave = false)
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();
            foreach (PropertyInfo propiedad in Campos(entidad.GetType()))
            {
                if (propiedad.Name == clave && !incluirClave) continue;
                object valor = propiedad.GetValue(entidad, null);
                if (nulos != null && nulos.Contains(propiedad.Name))
                {
                    if (valor == null || (valor is int && (int)valor == 0)) valor = null;
                    if (valor is string && string.IsNullOrWhiteSpace((string)valor)) valor = null;
                }
                if (valor is DateTime)
                    valor = ((DateTime)valor).ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture);
                datos[propiedad.Name] = valor;
            }
            return datos;
        }

        private static List<T> LeerFilas<T>(string json) where T : new()
        {
            List<T> resultado = new List<T>();
            foreach (Dictionary<string, object> fila in LeerObjetos(json))
            {
                T entidad = new T();
                foreach (PropertyInfo propiedad in Campos(typeof(T)))
                {
                    object valor;
                    if (!fila.TryGetValue(propiedad.Name, out valor) || valor == null)
                    {
                        if (propiedad.PropertyType.IsValueType && Nullable.GetUnderlyingType(propiedad.PropertyType) == null)
                            throw new InvalidOperationException("La columna " + propiedad.Name + " devolvió NULL inesperadamente.");
                        propiedad.SetValue(entidad, null, null);
                        continue;
                    }
                    Type tipo = TipoBase(propiedad.PropertyType);
                    if (tipo == typeof(DateTime))
                        valor = DateTime.Parse(Convert.ToString(valor, CultureInfo.InvariantCulture),
                            CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                    else if (tipo != valor.GetType())
                        valor = Convert.ChangeType(valor, tipo, CultureInfo.InvariantCulture);
                    propiedad.SetValue(entidad, valor, null);
                }
                resultado.Add(entidad);
            }
            return resultado;
        }

        private static List<Dictionary<string, object>> LeerObjetos(string json)
        {
            ArrayList filas = Serializador.Deserialize<ArrayList>(json ?? "[]");
            List<Dictionary<string, object>> resultado = new List<Dictionary<string, object>>();
            if (filas == null) return resultado;
            foreach (object fila in filas)
            {
                Dictionary<string, object> objeto = fila as Dictionary<string, object>;
                if (objeto == null) throw new InvalidOperationException("Supabase devolvió una fila con formato inesperado.");
                resultado.Add(objeto);
            }
            return resultado;
        }

        private static PropertyInfo[] Campos(Type tipo)
        {
            return tipo.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite && p.GetIndexParameters().Length == 0).ToArray();
        }

        private static Type TipoBase(Type tipo)
        {
            return Nullable.GetUnderlyingType(tipo) ?? tipo;
        }

        private static void ConfirmarFila(string json, string tabla, string operacion)
        {
            if (LeerObjetos(json).Count == 0)
                throw new InvalidOperationException("No se encontró el registro en " + tabla + " para " + operacion + ".");
        }
    }
}
