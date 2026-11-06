using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Script.Serialization;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones REST de Supabase para el módulo piloto de sucursales.</summary>
    internal class SucursalesSupabaseDatos
    {
        private const int TamanoPagina = 500;
        private const string RutaTabla = "/rest/v1/Tbl_Sucursales?select=CodigoSucursal,Nombre,Direccion,Municipio,Departamento,Telefono,HorarioAtencion,CorreoElectronico,FechaApertura,Estado&order=CodigoSucursal.asc";
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        public List<SucursalesEntidad> Listar()
        {
            try
            {
                List<SucursalesEntidad> sucursales = new List<SucursalesEntidad>();
                int inicio = 0;
                while (true)
                {
                    string json = SupabaseRestDatos.Enviar(HttpMethod.Get, RutaTabla, null, null,
                        inicio, inicio + TamanoPagina - 1);
                    List<SucursalesEntidad> pagina = Deserializar(json);
                    sucursales.AddRange(pagina);
                    if (pagina.Count < TamanoPagina) break;
                    inicio += TamanoPagina;
                }
                return sucursales;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar sucursales en Supabase. " + ex.Message, ex);
            }
        }

        public List<SucursalesEntidad> Buscar(string criterio)
        {
            string texto = (criterio ?? string.Empty).Trim();
            if (texto.Length == 0) return Listar();

            // Se filtra el resultado completo para mantener la búsqueda transversal
            // equivalente a la que ofrece el formulario con SQL Server.
            return Listar().FindAll(s => Coincide(s.CodigoSucursal.ToString(CultureInfo.InvariantCulture), texto)
                || Coincide(s.Nombre, texto)
                || Coincide(s.Direccion, texto)
                || Coincide(s.Municipio, texto)
                || Coincide(s.Departamento, texto)
                || Coincide(s.Telefono, texto)
                || Coincide(s.HorarioAtencion, texto)
                || Coincide(s.CorreoElectronico, texto));
        }

        public int Agregar(SucursalesEntidad sucursal)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Sucursales?select=CodigoSucursal";
                string json = SupabaseRestDatos.Enviar(HttpMethod.Post, ruta, CrearDatos(sucursal),
                    "return=representation", -1, -1);
                List<Dictionary<string, object>> filas = DeserializarFilas(json);
                if (filas.Count == 0)
                    throw new InvalidOperationException("Supabase no devolvió el código de la sucursal creada.");
                return Convert.ToInt32(filas[0]["CodigoSucursal"], CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar la sucursal en Supabase. " + ex.Message, ex);
            }
        }

        public void Editar(SucursalesEntidad sucursal)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Sucursales?CodigoSucursal=eq."
                    + sucursal.CodigoSucursal.ToString(CultureInfo.InvariantCulture)
                    + "&select=CodigoSucursal";
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta,
                    CrearDatos(sucursal), "return=representation", -1, -1);
                ConfirmarFilaAfectada(json, "editar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar la sucursal en Supabase. " + ex.Message, ex);
            }
        }

        public void Inactivar(int codigoSucursal)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Sucursales?CodigoSucursal=eq."
                    + codigoSucursal.ToString(CultureInfo.InvariantCulture)
                    + "&select=CodigoSucursal";
                Dictionary<string, object> datos = new Dictionary<string, object> { { "Estado", false } };
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, datos,
                    "return=representation", -1, -1);
                ConfirmarFilaAfectada(json, "inactivar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible inactivar la sucursal en Supabase. " + ex.Message, ex);
            }
        }

        private static Dictionary<string, object> CrearDatos(SucursalesEntidad s)
        {
            Dictionary<string, object> datos = new Dictionary<string, object>
            {
                { "Nombre", s.Nombre },
                { "Direccion", s.Direccion },
                { "Municipio", s.Municipio },
                { "Departamento", s.Departamento },
                { "Telefono", s.Telefono },
                { "HorarioAtencion", s.HorarioAtencion },
                { "CorreoElectronico", s.CorreoElectronico },
                { "FechaApertura", s.FechaApertura.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture) },
                { "Estado", s.Estado }
            };
            return datos;
        }

        private static List<SucursalesEntidad> Deserializar(string json)
        {
            List<SucursalesEntidad> lista = new List<SucursalesEntidad>();
            foreach (Dictionary<string, object> fila in DeserializarFilas(json))
            {
                lista.Add(new SucursalesEntidad(
                    Convert.ToInt32(fila["CodigoSucursal"], CultureInfo.InvariantCulture),
                    Texto(fila, "Nombre"),
                    Texto(fila, "Direccion"),
                    Texto(fila, "Municipio"),
                    Texto(fila, "Departamento"),
                    Texto(fila, "Telefono"),
                    Texto(fila, "HorarioAtencion"),
                    Texto(fila, "CorreoElectronico"),
                    DateTime.Parse(Texto(fila, "FechaApertura"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
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
                ? Convert.ToString(valor, CultureInfo.InvariantCulture)
                : null;
        }

        private static bool Coincide(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor)
                && valor.IndexOf(criterio, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private static void ConfirmarFilaAfectada(string json, string operacion)
        {
            if (DeserializarFilas(json).Count == 0)
                throw new InvalidOperationException("No se encontró la sucursal para " + operacion + ".");
        }
    }
}
