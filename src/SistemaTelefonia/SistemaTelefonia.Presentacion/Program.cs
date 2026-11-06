using System;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (FrmSeleccionBaseDatos selector = new FrmSeleccionBaseDatos())
            {
                if (selector.ShowDialog() != DialogResult.OK) return;
            }

            // El formulario de inicio de sesión permanece como formulario principal;
            // así cerrar la ventana de acceso también termina la aplicación.
            FrmLogin login = new FrmLogin();
            FrmLogin.Instancia = login;
            Application.Run(login);
        }
    }
}
