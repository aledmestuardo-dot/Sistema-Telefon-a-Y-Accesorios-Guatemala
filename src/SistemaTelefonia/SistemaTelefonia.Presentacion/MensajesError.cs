using System;
using System.Text.RegularExpressions;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Convierte errores técnicos en mensajes breves y útiles para el usuario.</summary>
    internal static class MensajesError
    {
        private const int LongitudMaxima = 180;

        public static string Resumir(Exception ex)
        {
            if (ex == null) return "No se pudo completar la operación. Inténtalo de nuevo.";

            string detalle = ex.Message;
            for (Exception interna = ex.InnerException; interna != null; interna = interna.InnerException)
                detalle += " " + interna.Message;

            return Resumir(detalle);
        }

        public static string Resumir(string detalle)
        {
            string texto = Regex.Replace((detalle ?? string.Empty).Trim(), @"\s+", " ");
            if (texto.Length == 0) return "No se pudo completar la operación. Inténtalo de nuevo.";

            if (Contiene(texto, "Login failed for user", "18456"))
                return "SQL Server rechazó las credenciales. Verifica el usuario y la contraseña configurados.";

            if (Contiene(texto, "Cannot open database", "4060", "no se puede abrir la base de datos"))
                return "No se encontró o no está disponible la base de datos. Revisa su nombre y que esté inicializada.";

            bool errorRed = Contiene(texto, "network-related", "TCP Provider", "remote computer refused", "actively refused",
                "server was not found", "no se encontró el servidor", "error: 0 -", "error 53", "error 40", "error 26",
                "No such host is known", "name or service not known", "no se pudo resolver el nombre");
            if (errorRed && Contiene(texto, "Supabase"))
                return "No se pudo conectar con Supabase. Comprueba Internet, URL y clave publicable.";
            if (errorRed)
                return "No se pudo conectar con SQL Server. Comprueba que Docker esté activo y revisa el servidor y el puerto.";

            if (Contiene(texto, "HTTP 401", "no está autorizada", "sesión de Supabase expiró", "sesión de Supabase no está activa"))
                return "La sesión venció o no tiene autorización. Inicia sesión de nuevo.";

            if (Contiene(texto, "HTTP 403", "permission denied", "insufficient_privilege", "no tienes permiso"))
                return "No tienes permiso para realizar esta acción. Contacta al administrador.";

            if (Contiene(texto, "23505", "duplicate key", "unique constraint", "ya existe un registro"))
                return "Ya existe un registro con esos datos. Revisa el código o nombre.";

            if (Contiene(texto, "23503", "foreign key", "conflicted with the foreign key", "clave foránea"))
                return "La relación seleccionada no es válida o está en uso. Revisa los registros vinculados.";

            if (Contiene(texto, "23502", "cannot insert the value NULL", "does not allow null", "not-null constraint"))
                return "Completa los campos obligatorios y vuelve a intentar.";

            if (Contiene(texto, "timeout", "timed out", "tiempo de espera agotado"))
                return "El servicio tardó demasiado en responder. Inténtalo de nuevo.";

            if (Contiene(texto, "Could not find file", "FileNotFoundException", "no se encuentra el archivo"))
                return "No se encontró el archivo seleccionado. Comprueba la ruta e inténtalo de nuevo.";

            if (Contiene(texto, "Supabase respondió con HTTP", "Supabase Auth respondió con HTTP"))
                return "Supabase rechazó la solicitud. Revisa los datos y los permisos e inténtalo de nuevo.";

            int inicioDetalle = texto.IndexOf(" Detalle:", StringComparison.OrdinalIgnoreCase);
            if (inicioDetalle >= 0) texto = texto.Substring(0, inicioDetalle).TrimEnd('.', ' ');

            if (texto.Length <= LongitudMaxima) return texto;

            int finPrimeraOracion = texto.IndexOf(". ", StringComparison.Ordinal);
            if (finPrimeraOracion > 0 && finPrimeraOracion < 125)
                return texto.Substring(0, finPrimeraOracion + 1) + " Revisa los datos e inténtalo de nuevo.";

            return "No se pudo completar la operación. Revisa los datos e inténtalo de nuevo.";
        }

        private static bool Contiene(string texto, params string[] frases)
        {
            foreach (string frase in frases)
                if (texto.IndexOf(frase, StringComparison.OrdinalIgnoreCase) >= 0) return true;
            return false;
        }
    }
}
