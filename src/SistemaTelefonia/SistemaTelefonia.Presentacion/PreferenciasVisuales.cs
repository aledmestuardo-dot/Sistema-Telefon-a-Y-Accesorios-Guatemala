using System;
using System.IO;

namespace SistemaTelefonia.Presentacion
{
    internal enum TemaInterfaz
    {
        Azul,
        Negro,
        Claro
    }

    internal enum VistaModulos
    {
        Cuadricula,
        Lista
    }

    /// <summary>Guarda las preferencias visuales por usuario, fuera de la carpeta portable.</summary>
    internal static class PreferenciasVisuales
    {
        private static readonly string Ruta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SistemaTelefonia", "preferencias-visuales.txt");

        public static TemaInterfaz Tema { get; private set; }
        public static VistaModulos Vista { get; private set; }

        static PreferenciasVisuales()
        {
            Tema = TemaInterfaz.Azul;
            Vista = VistaModulos.Cuadricula;
            try
            {
                if (!File.Exists(Ruta)) return;
                string[] valores = File.ReadAllLines(Ruta);
                TemaInterfaz tema;
                VistaModulos vista;
                if (valores.Length > 0 && Enum.TryParse(valores[0], true, out tema)) Tema = tema;
                if (valores.Length > 1 && Enum.TryParse(valores[1], true, out vista)) Vista = vista;
            }
            catch
            {
                // Si no se pueden leer las preferencias, se usan los valores predeterminados.
            }
        }

        public static void Guardar(TemaInterfaz tema, VistaModulos vista)
        {
            Tema = tema;
            Vista = vista;
            try
            {
                string directorio = Path.GetDirectoryName(Ruta);
                if (!Directory.Exists(directorio)) Directory.CreateDirectory(directorio);
                File.WriteAllLines(Ruta, new[] { tema.ToString(), vista.ToString() });
            }
            catch
            {
                // El cambio visual funciona durante la sesión aunque no haya permiso para guardarlo.
            }
        }
    }
}
