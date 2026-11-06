using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Script.Serialization;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones REST de Supabase para Tbl_Marcas con la sesión y RLS activas.</summary>
    internal class MarcasSupabaseDatos
    {
        private const int TamanoPagina = 500;
        private const string RutaTabla = "/rest/v1/Tbl_Marcas?select=CodigoMarca,CodigoProveedor,Nombre,PaisOrigen,Descripcion,SitioWeb,FechaRegistro,Estado&order=CodigoMarca.asc";
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        public List<MarcasEntidad> Listar()
        {
            try
            {
                List<MarcasEntidad> marcas = new List<MarcasEntidad>();
                int inicio = 0;
                while (true)
                {
                    string json = SupabaseRestDatos.Enviar(HttpMethod.Get, RutaTabla, null, null,
                        inicio, inicio + TamanoPagina - 1);
                    List<MarcasEntidad> pagina = Deserializar(json);
                    marcas.AddRange(pagina);
                    if (pagina.Count < TamanoPagina) break;
                    inicio += TamanoPagina;
                }
                return marcas;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar marcas en Supabase. " + ex.Message, ex);
            }
        }

        public List<MarcasEntidad> Buscar(string criterio)
        {
            string texto = (criterio ?? string.Empty).Trim();
            if (texto.Length == 0) return Listar();

            return Listar().FindAll(m => Coincide(m.CodigoMarca.ToString(CultureInfo.InvariantCulture), texto)
                || Coincide(m.CodigoProveedor.ToString(CultureInfo.InvariantCulture), texto)
                || Coincide(m.Nombre, texto) || Coincide(m.PaisOrigen, texto)
                || Coincide(m.Descripcion, texto) || Coincide(m.SitioWeb, texto));
        }

        public int Agregar(MarcasEntidad marca)
        {
            try
            {
                string json = SupabaseRestDatos.Enviar(HttpMethod.Post,
                    "/rest/v1/Tbl_Marcas?select=CodigoMarca", CrearDatos(marca),
                    "return=representation", -1, -1);
                List<Dictionary<string, object>> filas = DeserializarFilas(json);
                if (filas.Count == 0)
                    throw new InvalidOperationException("Supabase no devolvió el código de la marca creada.");
                return Convert.ToInt32(filas[0]["CodigoMarca"], CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar la marca en Supabase. " + ex.Message, ex);
            }
        }

        public void Editar(int codigoMarca, MarcasEntidad marca)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Marcas?CodigoMarca=eq."
                    + codigoMarca.ToString(CultureInfo.InvariantCulture) + "&select=CodigoMarca";
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, CrearDatos(marca),
                    "return=representation", -1, -1);
                ConfirmarFila(json, "editar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar la marca en Supabase. " + ex.Message, ex);
            }
        }

        public void Inactivar(int codigoMarca)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Marcas?CodigoMarca=eq."
                    + codigoMarca.ToString(CultureInfo.InvariantCulture) + "&select=CodigoMarca";
                Dictionary<string, object> datos = new Dictionary<string, object> { { "Estado", false } };
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, datos,
                    "return=representation", -1, -1);
                ConfirmarFila(json, "inactivar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible inactivar la marca en Supabase. " + ex.Message, ex);
            }
        }

        private static Dictionary<string, object> CrearDatos(MarcasEntidad marca)
        {
            return new Dictionary<string, object>
            {
                { "CodigoProveedor", marca.CodigoProveedor },
                { "Nombre", marca.Nombre },
                { "PaisOrigen", marca.PaisOrigen },
                { "Descripcion", marca.Descripcion },
                { "SitioWeb", marca.SitioWeb },
                { "FechaRegistro", marca.FechaRegistro.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture) },
                { "Estado", marca.Estado }
            };
        }

        private static List<MarcasEntidad> Deserializar(string json)
        {
            List<MarcasEntidad> lista = new List<MarcasEntidad>();
            foreach (Dictionary<string, object> fila in DeserializarFilas(json))
            {
                lista.Add(new MarcasEntidad(
                    Convert.ToInt32(fila["CodigoMarca"], CultureInfo.InvariantCulture),
                    Convert.ToInt32(fila["CodigoProveedor"], CultureInfo.InvariantCulture),
                    Texto(fila, "Nombre"), Texto(fila, "PaisOrigen"), Texto(fila, "Descripcion"),
                    Texto(fila, "SitioWeb"), DateTime.Parse(Texto(fila, "FechaRegistro"),
                        CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    Convert.ToBoolean(fila["Estado"], CultureInfo.InvariantCulture)));
            }
            return lista;
        }

        private static List<Dictionary<string, object>> DeserializarFilas(string json)
        {
            ArrayList filas = Serializador.Deserialize<ArrayList>(json);
            List<Dictionary<string, object>> resultado = new List<Dictionary<string, object>>();
            if (filas == null) return resultado;
            foreach (object fila in filas)
            {
                Dictionary<string, object> objeto = fila as Dictionary<string, object>;
                if (objeto == null)
                    throw new InvalidOperationException("Supabase devolvió una fila con formato inesperado.");
                resultado.Add(objeto);
            }
            return resultado;
        }

        private static string Texto(Dictionary<string, object> fila, string clave)
        {
            object valor;
            return fila.TryGetValue(clave, out valor) && valor != null
                ? Convert.ToString(valor, CultureInfo.InvariantCulture) : null;
        }

        private static bool Coincide(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor)
                && valor.IndexOf(criterio, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private static void ConfirmarFila(string json, string operacion)
        {
            if (DeserializarFilas(json).Count == 0)
                throw new InvalidOperationException("No se encontró la marca para " + operacion + ".");
        }
    }
}
