using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Script.Serialization;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones REST de Supabase para Tbl_Proveedores.</summary>
    internal class ProveedoresSupabaseDatos
    {
        private const int TamanoPagina = 500;
        private const string RutaTabla = "/rest/v1/Tbl_Proveedores?select=CodigoProveedor,Nombre,Nit,Direccion,Telefono,CorreoElectronico,NombreContacto,Descripcion,Estado&order=CodigoProveedor.asc";
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        public List<ProveedoresEntidad> Listar()
        {
            try
            {
                List<ProveedoresEntidad> proveedores = new List<ProveedoresEntidad>();
                int inicio = 0;
                while (true)
                {
                    string json = SupabaseRestDatos.Enviar(HttpMethod.Get, RutaTabla, null, null,
                        inicio, inicio + TamanoPagina - 1);
                    List<ProveedoresEntidad> pagina = Deserializar(json);
                    proveedores.AddRange(pagina);
                    if (pagina.Count < TamanoPagina) break;
                    inicio += TamanoPagina;
                }
                return proveedores;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar proveedores en Supabase. " + ex.Message, ex);
            }
        }

        public List<ProveedoresEntidad> Buscar(string criterio)
        {
            string texto = (criterio ?? string.Empty).Trim();
            if (texto.Length == 0) return Listar();
            return Listar().FindAll(p => Coincide(p.CodigoProveedor.ToString(CultureInfo.InvariantCulture), texto)
                || Coincide(p.Nombre, texto) || Coincide(p.Nit, texto)
                || Coincide(p.Direccion, texto) || Coincide(p.Telefono, texto)
                || Coincide(p.CorreoElectronico, texto) || Coincide(p.NombreContacto, texto)
                || Coincide(p.Descripcion, texto));
        }

        public int Agregar(ProveedoresEntidad proveedor)
        {
            try
            {
                string json = SupabaseRestDatos.Enviar(HttpMethod.Post,
                    "/rest/v1/Tbl_Proveedores?select=CodigoProveedor", CrearDatos(proveedor),
                    "return=representation", -1, -1);
                List<Dictionary<string, object>> filas = DeserializarFilas(json);
                if (filas.Count == 0)
                    throw new InvalidOperationException("Supabase no devolvió el código del proveedor creado.");
                return Convert.ToInt32(filas[0]["CodigoProveedor"], CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar el proveedor en Supabase. " + ex.Message, ex);
            }
        }

        public void Editar(ProveedoresEntidad proveedor)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Proveedores?CodigoProveedor=eq."
                    + proveedor.CodigoProveedor.ToString(CultureInfo.InvariantCulture) + "&select=CodigoProveedor";
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, CrearDatos(proveedor),
                    "return=representation", -1, -1);
                ConfirmarFila(json, "editar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar el proveedor en Supabase. " + ex.Message, ex);
            }
        }

        public void Inactivar(int codigoProveedor)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Proveedores?CodigoProveedor=eq."
                    + codigoProveedor.ToString(CultureInfo.InvariantCulture) + "&select=CodigoProveedor";
                Dictionary<string, object> datos = new Dictionary<string, object> { { "Estado", false } };
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, datos,
                    "return=representation", -1, -1);
                ConfirmarFila(json, "inactivar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible inactivar el proveedor en Supabase. " + ex.Message, ex);
            }
        }

        private static Dictionary<string, object> CrearDatos(ProveedoresEntidad p)
        {
            return new Dictionary<string, object>
            {
                { "Nombre", p.Nombre }, { "Nit", p.Nit }, { "Direccion", p.Direccion },
                { "Telefono", p.Telefono }, { "CorreoElectronico", p.CorreoElectronico },
                { "NombreContacto", p.NombreContacto }, { "Descripcion", p.Descripcion }, { "Estado", p.Estado }
            };
        }

        private static List<ProveedoresEntidad> Deserializar(string json)
        {
            List<ProveedoresEntidad> lista = new List<ProveedoresEntidad>();
            foreach (Dictionary<string, object> fila in DeserializarFilas(json))
                lista.Add(new ProveedoresEntidad(
                    Convert.ToInt32(fila["CodigoProveedor"], CultureInfo.InvariantCulture), Texto(fila, "Nombre"),
                    Texto(fila, "Nit"), Texto(fila, "Direccion"), Texto(fila, "Telefono"),
                    Texto(fila, "CorreoElectronico"), Texto(fila, "NombreContacto"), Texto(fila, "Descripcion"),
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
                throw new InvalidOperationException("No se encontró el proveedor para " + operacion + ".");
        }
    }
}
