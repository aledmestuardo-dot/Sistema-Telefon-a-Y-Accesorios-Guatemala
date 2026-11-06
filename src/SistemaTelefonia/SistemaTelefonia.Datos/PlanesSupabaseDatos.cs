using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Script.Serialization;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones REST de Supabase para Tbl_Planes.</summary>
    internal class PlanesSupabaseDatos
    {
        private const int TamanoPagina = 500;
        private const string RutaTabla = "/rest/v1/Tbl_Planes?select=CodigoPlan,Nombre,Descripcion,CostoMensual,CantidadDatos,MinutosIncluidos,VigenciaMeses,Estado&order=CodigoPlan.asc";
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        public List<PlanesEntidad> Listar()
        {
            try
            {
                List<PlanesEntidad> planes = new List<PlanesEntidad>();
                int inicio = 0;
                while (true)
                {
                    string json = SupabaseRestDatos.Enviar(HttpMethod.Get, RutaTabla, null, null,
                        inicio, inicio + TamanoPagina - 1);
                    List<PlanesEntidad> pagina = Deserializar(json);
                    planes.AddRange(pagina);
                    if (pagina.Count < TamanoPagina) break;
                    inicio += TamanoPagina;
                }
                return planes;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar planes en Supabase. " + ex.Message, ex);
            }
        }

        public List<PlanesEntidad> Buscar(string criterio)
        {
            string texto = (criterio ?? string.Empty).Trim();
            if (texto.Length == 0) return Listar();
            return Listar().FindAll(p => Coincide(p.CodigoPlan.ToString(CultureInfo.InvariantCulture), texto)
                || Coincide(p.Nombre, texto) || Coincide(p.Descripcion, texto));
        }

        public int Agregar(PlanesEntidad plan)
        {
            try
            {
                string json = SupabaseRestDatos.Enviar(HttpMethod.Post,
                    "/rest/v1/Tbl_Planes?select=CodigoPlan", CrearDatos(plan),
                    "return=representation", -1, -1);
                List<Dictionary<string, object>> filas = DeserializarFilas(json);
                if (filas.Count == 0)
                    throw new InvalidOperationException("Supabase no devolvió el código del plan creado.");
                return Convert.ToInt32(filas[0]["CodigoPlan"], CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar el plan en Supabase. " + ex.Message, ex);
            }
        }

        public void Editar(PlanesEntidad plan)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Planes?CodigoPlan=eq."
                    + plan.CodigoPlan.ToString(CultureInfo.InvariantCulture) + "&select=CodigoPlan";
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, CrearDatos(plan),
                    "return=representation", -1, -1);
                ConfirmarFila(json, "editar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar el plan en Supabase. " + ex.Message, ex);
            }
        }

        public void Inactivar(int codigoPlan)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Planes?CodigoPlan=eq."
                    + codigoPlan.ToString(CultureInfo.InvariantCulture) + "&select=CodigoPlan";
                Dictionary<string, object> datos = new Dictionary<string, object> { { "Estado", false } };
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, datos,
                    "return=representation", -1, -1);
                ConfirmarFila(json, "inactivar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible inactivar el plan en Supabase. " + ex.Message, ex);
            }
        }

        private static Dictionary<string, object> CrearDatos(PlanesEntidad p)
        {
            return new Dictionary<string, object>
            {
                { "Nombre", p.Nombre }, { "Descripcion", p.Descripcion }, { "CostoMensual", p.CostoMensual },
                { "CantidadDatos", p.CantidadDatos }, { "MinutosIncluidos", p.MinutosIncluidos },
                { "VigenciaMeses", p.VigenciaMeses }, { "Estado", p.Estado }
            };
        }

        private static List<PlanesEntidad> Deserializar(string json)
        {
            List<PlanesEntidad> lista = new List<PlanesEntidad>();
            foreach (Dictionary<string, object> fila in DeserializarFilas(json))
                lista.Add(new PlanesEntidad(
                    Convert.ToInt32(fila["CodigoPlan"], CultureInfo.InvariantCulture), Texto(fila, "Nombre"),
                    Texto(fila, "Descripcion"), Convert.ToDecimal(fila["CostoMensual"], CultureInfo.InvariantCulture),
                    Convert.ToInt32(fila["CantidadDatos"], CultureInfo.InvariantCulture),
                    Convert.ToInt32(fila["MinutosIncluidos"], CultureInfo.InvariantCulture),
                    Convert.ToInt32(fila["VigenciaMeses"], CultureInfo.InvariantCulture),
                    Convert.ToBoolean(fila["Estado"], CultureInfo.InvariantCulture)));
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
                if (objeto == null) throw new InvalidOperationException("Supabase devolvió una fila con formato inesperado.");
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
                throw new InvalidOperationException("No se encontró el plan para " + operacion + ".");
        }
    }
}
