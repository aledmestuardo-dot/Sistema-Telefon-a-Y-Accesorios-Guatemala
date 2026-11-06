using System;
using System.IO;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Persiste localmente el origen elegido, sin guardar credenciales.</summary>
    public static class OrigenBaseDatosDatos
    {
        private static string RutaPreferencia
        {
            get
            {
                string carpeta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "SistemaTelefonia");
                return Path.Combine(carpeta, "origen-datos.txt");
            }
        }

        public static OrigenBaseDatos Leer()
        {
            try
            {
                if (!File.Exists(RutaPreferencia)) return OrigenBaseDatos.SqlServer;
                string valor = File.ReadAllText(RutaPreferencia).Trim();
                OrigenBaseDatos origen;
                return Enum.TryParse(valor, true, out origen) ? origen : OrigenBaseDatos.SqlServer;
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("No fue posible leer la última base seleccionada.", ex);
            }
        }

        public static void Guardar(OrigenBaseDatos origen)
        {
            try
            {
                string carpeta = Path.GetDirectoryName(RutaPreferencia);
                Directory.CreateDirectory(carpeta);
                File.WriteAllText(RutaPreferencia, origen.ToString());
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("No fue posible guardar la base seleccionada.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new InvalidOperationException("Windows no permitió guardar la preferencia de base.", ex);
            }
        }
    }
}
