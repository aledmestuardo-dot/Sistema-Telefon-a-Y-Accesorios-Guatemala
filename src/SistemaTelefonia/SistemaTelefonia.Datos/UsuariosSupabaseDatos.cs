using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Script.Serialization;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Administra perfiles y cuentas Auth mediante la función segura de Supabase.</summary>
    public sealed class UsuariosSupabaseDatos
    {
        private static readonly JavaScriptSerializer Serializador = new JavaScriptSerializer();

        public List<UsuariosEntidad> Listar()
        {
            Dictionary<string, object> respuesta = Ejecutar("listar", null);
            object valor;
            ArrayList filas = respuesta.TryGetValue("usuarios", out valor) ? valor as ArrayList : null;
            List<UsuariosEntidad> resultado = new List<UsuariosEntidad>();
            if (filas == null) return resultado;
            foreach (object elemento in filas)
            {
                Dictionary<string, object> fila = elemento as Dictionary<string, object>;
                if (fila == null) throw new InvalidOperationException("Supabase devolvió un usuario con formato inesperado.");
                resultado.Add(Materializar(fila));
            }
            return resultado;
        }

        public int Agregar(UsuariosEntidad usuario)
        {
            Dictionary<string, object> entrada = new Dictionary<string, object>
            {
                { "nombre", usuario.Nombre },
                { "correo", usuario.CorreoElectronico },
                { "contrasenia", usuario.Contrasenia },
                { "estado", usuario.Estado }
            };
            Dictionary<string, object> resultado = Ejecutar("agregar", entrada);
            return Convert.ToInt32(ObtenerObjeto(resultado, "usuario")["CodigoUsuario"], CultureInfo.InvariantCulture);
        }

        public void Editar(UsuariosEntidad usuario)
        {
            Dictionary<string, object> entrada = new Dictionary<string, object>
            {
                { "codigoUsuario", usuario.CodigoUsuario },
                { "nombre", usuario.Nombre },
                { "correo", usuario.CorreoElectronico },
                { "contrasenia", usuario.Contrasenia },
                { "estado", usuario.Estado },
                { "TipoUsuario", usuario.TipoUsuario }
            };
            Ejecutar("editar", entrada);
        }

        public void Inactivar(int codigoUsuario)
        {
            Ejecutar("inactivar", new Dictionary<string, object> { { "codigoUsuario", codigoUsuario } });
        }

        private static Dictionary<string, object> Ejecutar(string accion, Dictionary<string, object> campos)
        {
            Dictionary<string, object> entrada = campos ?? new Dictionary<string, object>();
            entrada["accion"] = accion;
            string json;
            try
            {
                json = SupabaseRestDatos.Enviar(HttpMethod.Post, "/functions/v1/usuarios-admin", entrada,
                    null, -1, -1);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible administrar usuarios en Supabase. " + ex.Message, ex);
            }

            Dictionary<string, object> resultado = Serializador.Deserialize<Dictionary<string, object>>(json ?? "{}");
            if (resultado == null) throw new InvalidOperationException("Supabase devolvió una respuesta inesperada al administrar usuarios.");
            object error;
            if (resultado.TryGetValue("error", out error) && error != null)
                throw new InvalidOperationException(Convert.ToString(error, CultureInfo.InvariantCulture));
            return resultado;
        }

        private static UsuariosEntidad Materializar(Dictionary<string, object> fila)
        {
            return new UsuariosEntidad
            {
                CodigoUsuario = Entero(fila, "CodigoUsuario"),
                TipoUsuario = Texto(fila, "TipoUsuario"),
                Nombre = Texto(fila, "Nombre"),
                Contrasenia = string.Empty,
                CorreoElectronico = Texto(fila, "CorreoElectronico"),
                FechaCreacion = Fecha(fila, "FechaCreacion"),
                FechaUltimoAcceso = FechaNullable(fila, "FechaUltimoAcceso"),
                Estado = Booleano(fila, "Estado"),
                EsDueno = Booleano(fila, "EsDueno"),
                AuthUserId = Texto(fila, "AuthUserId")
            };
        }

        private static Dictionary<string, object> ObtenerObjeto(Dictionary<string, object> datos, string clave)
        {
            object valor;
            Dictionary<string, object> objeto = datos.TryGetValue(clave, out valor) ? valor as Dictionary<string, object> : null;
            if (objeto == null) throw new InvalidOperationException("Supabase no devolvió el perfil del usuario.");
            return objeto;
        }

        private static string Texto(Dictionary<string, object> fila, string clave)
        {
            object valor;
            return fila.TryGetValue(clave, out valor) && valor != null ? Convert.ToString(valor, CultureInfo.InvariantCulture) : string.Empty;
        }

        private static int Entero(Dictionary<string, object> fila, string clave)
        {
            return Convert.ToInt32(fila[clave], CultureInfo.InvariantCulture);
        }

        private static bool Booleano(Dictionary<string, object> fila, string clave)
        {
            object valor;
            return fila.TryGetValue(clave, out valor) && valor != null && Convert.ToBoolean(valor, CultureInfo.InvariantCulture);
        }

        private static DateTime Fecha(Dictionary<string, object> fila, string clave)
        {
            return DateTime.Parse(Texto(fila, clave), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        }

        private static DateTime? FechaNullable(Dictionary<string, object> fila, string clave)
        {
            object valor;
            if (!fila.TryGetValue(clave, out valor) || valor == null) return null;
            return DateTime.Parse(Convert.ToString(valor, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind);
        }
    }
}
