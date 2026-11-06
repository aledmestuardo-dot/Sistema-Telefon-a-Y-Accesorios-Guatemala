using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;

namespace SistemaTelefonia.Datos
{
    /// <summary>Lee opciones locales desde variables de entorno o el .env ignorado por Git.</summary>
    internal static class ConfiguracionLocalDatos
    {
        private static readonly Lazy<Dictionary<string, string>> ValoresEnv =
            new Lazy<Dictionary<string, string>>(CargarArchivoEnv);

        public static string Obtener(string nombre)
        {
            string valor = Environment.GetEnvironmentVariable(nombre);
            if (!string.IsNullOrWhiteSpace(valor)) return valor.Trim();

            string valorArchivo;
            return ValoresEnv.Value.TryGetValue(nombre, out valorArchivo) ? valorArchivo : null;
        }

        private static Dictionary<string, string> CargarArchivoEnv()
        {
            Dictionary<string, string> valores = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            // ClickOnce coloca los archivos marcados como datos en un directorio estable entre actualizaciones.
            CargarArchivo(ObtenerRutaEnvClickOnce(), valores);

            // Compatibilidad con instalaciones existentes y desarrollo: .env persistente de usuario.
            string rutaUsuario = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SistemaTelefonia", ".env");
            CargarArchivo(rutaUsuario, valores, sobrescribir: false);

            // Compatibilidad con desarrollo: buscar .env desde la carpeta de ejecución hacia arriba.
            DirectoryInfo directorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (directorio != null)
            {
                string ruta = Path.Combine(directorio.FullName, ".env");
                CargarArchivo(ruta, valores, sobrescribir: false);
                directorio = directorio.Parent;
            }

            return valores;
        }

        private static string ObtenerRutaEnvClickOnce()
        {
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                    return Path.Combine(ApplicationDeployment.CurrentDeployment.DataDirectory, ".env");
            }
            catch (InvalidDeploymentException)
            {
                // Si ClickOnce no proporciona directorio de datos, se usan las rutas locales alternativas.
            }

            return null;
        }

        private static void CargarArchivo(string ruta, Dictionary<string, string> valores, bool sobrescribir = true)
        {
            if (!File.Exists(ruta)) return;

            foreach (string lineaOriginal in File.ReadAllLines(ruta))
            {
                string linea = lineaOriginal.Trim();
                if (linea.Length == 0 || linea.StartsWith("#", StringComparison.Ordinal)) continue;

                int separador = linea.IndexOf('=');
                if (separador <= 0) continue;

                string clave = linea.Substring(0, separador).Trim();
                string contenido = linea.Substring(separador + 1).Trim();
                if (contenido.Length >= 2 && contenido[0] == '"' && contenido[contenido.Length - 1] == '"')
                    contenido = contenido.Substring(1, contenido.Length - 2);

                if (sobrescribir || !valores.ContainsKey(clave))
                    valores[clave] = contenido;
            }
        }
    }
}
