using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Script.Serialization;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones REST de Supabase para Tbl_Clientes.</summary>
    internal class ClientesSupabaseDatos
    {
        private const int TamanoPagina = 500;
        private const string RutaTabla = "/rest/v1/Tbl_Clientes?select=CodigoCliente,Nombre,Nit,CorreoElectronico,Telefono,Direccion,Departamento,Municipio,Estado&order=CodigoCliente.asc";
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        public List<ClientesEntidad> Listar()
        {
            try
            {
                List<ClientesEntidad> clientes = new List<ClientesEntidad>();
                int inicio = 0;
                while (true)
                {
                    string json = SupabaseRestDatos.Enviar(HttpMethod.Get, RutaTabla, null, null,
                        inicio, inicio + TamanoPagina - 1);
                    List<ClientesEntidad> pagina = Deserializar(json);
                    clientes.AddRange(pagina);
                    if (pagina.Count < TamanoPagina) break;
                    inicio += TamanoPagina;
                }
                return clientes;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar clientes en Supabase. " + ex.Message, ex);
            }
        }

        public List<ClientesEntidad> Buscar(string criterio)
        {
            string texto = (criterio ?? string.Empty).Trim();
            if (texto.Length == 0) return Listar();
            return Listar().FindAll(c => Coincide(c.CodigoCliente.ToString(CultureInfo.InvariantCulture), texto)
                || Coincide(c.Nombre, texto) || Coincide(c.Nit, texto)
                || Coincide(c.CorreoElectronico, texto) || Coincide(c.Telefono, texto)
                || Coincide(c.Direccion, texto) || Coincide(c.Departamento, texto)
                || Coincide(c.Municipio, texto));
        }

        public int Agregar(ClientesEntidad cliente)
        {
            try
            {
                string json = SupabaseRestDatos.Enviar(HttpMethod.Post,
                    "/rest/v1/Tbl_Clientes?select=CodigoCliente", CrearDatos(cliente),
                    "return=representation", -1, -1);
                List<Dictionary<string, object>> filas = DeserializarFilas(json);
                if (filas.Count == 0)
                    throw new InvalidOperationException("Supabase no devolvió el código del cliente creado.");
                return Convert.ToInt32(filas[0]["CodigoCliente"], CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar el cliente en Supabase. " + ex.Message, ex);
            }
        }

        public void Editar(ClientesEntidad cliente)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Clientes?CodigoCliente=eq."
                    + cliente.CodigoCliente.ToString(CultureInfo.InvariantCulture) + "&select=CodigoCliente";
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, CrearDatos(cliente),
                    "return=representation", -1, -1);
                ConfirmarFila(json, "editar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar el cliente en Supabase. " + ex.Message, ex);
            }
        }

        public void Inactivar(int codigoCliente)
        {
            try
            {
                string ruta = "/rest/v1/Tbl_Clientes?CodigoCliente=eq."
                    + codigoCliente.ToString(CultureInfo.InvariantCulture) + "&select=CodigoCliente";
                Dictionary<string, object> datos = new Dictionary<string, object> { { "Estado", false } };
                string json = SupabaseRestDatos.Enviar(new HttpMethod("PATCH"), ruta, datos,
                    "return=representation", -1, -1);
                ConfirmarFila(json, "inactivar");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible inactivar el cliente en Supabase. " + ex.Message, ex);
            }
        }

        private static Dictionary<string, object> CrearDatos(ClientesEntidad c)
        {
            return new Dictionary<string, object>
            {
                { "Nombre", c.Nombre }, { "Nit", c.Nit }, { "CorreoElectronico", c.CorreoElectronico },
                { "Telefono", c.Telefono }, { "Direccion", c.Direccion },
                { "Departamento", c.Departamento }, { "Municipio", c.Municipio }, { "Estado", c.Estado }
            };
        }

        private static List<ClientesEntidad> Deserializar(string json)
        {
            List<ClientesEntidad> lista = new List<ClientesEntidad>();
            foreach (Dictionary<string, object> fila in DeserializarFilas(json))
                lista.Add(new ClientesEntidad(
                    Convert.ToInt32(fila["CodigoCliente"], CultureInfo.InvariantCulture), Texto(fila, "Nombre"),
                    Texto(fila, "Nit"), Texto(fila, "CorreoElectronico"), Texto(fila, "Telefono"),
                    Texto(fila, "Direccion"), Texto(fila, "Departamento"), Texto(fila, "Municipio"),
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
                throw new InvalidOperationException("No se encontró el cliente para " + operacion + ".");
        }
    }
}
