using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    // Permite moverse entre las pantallas (una por tabla) desde el menú lateral.
    // Cada pantalla se crea una sola vez; así conserva sus datos temporales al volver a ella.
    internal static class Navegacion
    {
        // Mismo orden en que las tablas se crean en el script SQL.
        public static readonly string[] Pantallas =
        {
            "Usuarios",
            "Sucursales",
            "Empleados",
            "Clientes",
            "Planes",
            "Lineas",
            "Proveedores",
            "Marcas",
            "Equipos",
            "Compras",
            "Inventario",
            "Ventas",
            "Facturacion",
            "Pagos",
            "Cardex",
            "Reclamos"
        };

        private static readonly Dictionary<string, string> titulos = new Dictionary<string, string>
        {
            { "Lineas", "Líneas" },
            { "Facturacion", "Facturación" },
        };

        private static readonly Dictionary<string, Form> abiertas = new Dictionary<string, Form>();

        public static void AplicarTemaAAbiertas(TemaInterfaz tema)
        {
            foreach (Form formulario in new List<Form>(abiertas.Values))
                TemaVisualCrud.Aplicar(formulario, tema);
        }

        public static string Titulo(string pantalla)
        {
            string titulo;
            return titulos.TryGetValue(pantalla, out titulo) ? titulo : pantalla;
        }

        // Registra una pantalla secundaria y la quita del registro cuando se cierra.
        public static void Registrar(string pantalla, Form formulario)
        {
            if (abiertas.ContainsKey(pantalla))
                return;

            abiertas[pantalla] = formulario;
            formulario.KeyPreview = true;
            formulario.KeyDown += (s, e) =>
            {
                if (e.Alt && e.KeyCode == Keys.M)
                {
                    e.SuppressKeyPress = true;
                    VolverAlMenu(formulario);
                }
            };
            formulario.FormClosed += (s, e) =>
            {
                string clave = PantallaDe(formulario);
                if (!string.IsNullOrEmpty(clave)) abiertas.Remove(clave);
            };
        }

        public static void VolverAlMenu(Form actual)
        {
            FrmMenuPrincipal menu = FrmMenuPrincipal.Instancia;
            if (menu == null || menu.IsDisposed)
            {
                menu = new FrmMenuPrincipal();
                FrmMenuPrincipal.Instancia = menu;
            }
            menu.StartPosition = FormStartPosition.Manual;
            if (actual.WindowState == FormWindowState.Normal)
            {
                menu.Location = actual.Location;
                menu.Size = actual.Size;
            }
            menu.WindowState = actual.WindowState;
            PrepararPresentacion(menu, actual);
            menu.Show();
            actual.Hide();
        }

        public static void CerrarSesion(Form actual)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas cerrar la sesión actual?", "Cerrar sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            // Revoca la sesión remota si el usuario inició con Supabase.
            new UsuariosNegocio().CerrarSesionSupabase();

            foreach (Form formulario in new List<Form>(abiertas.Values))
            {
                abiertas.Remove(PantallaDe(formulario));
                formulario.Hide();
            }
            FrmMenuPrincipal menu = FrmMenuPrincipal.Instancia;
            if (menu != null && !menu.IsDisposed) menu.Hide();
            FrmLogin login = FrmLogin.Instancia;
            if (login == null || login.IsDisposed)
            {
                login = new FrmLogin();
                FrmLogin.Instancia = login;
            }
            login.Show();
            login.Activate();
            login.LimpiarCredenciales();
            actual.Hide();
        }

        /// <summary>Indica si un módulo ya fue adaptado al origen Supabase activo.</summary>
        public static bool ModuloDisponible(OrigenBaseDatos origen, string pantalla)
        {
            if (origen != OrigenBaseDatos.Supabase) return true;
            // La administración de usuarios usa una Edge Function con rol de servidor
            // protegida por JWT; las demás tablas continúan usando PostgREST y RLS.
            return true;
        }

        private static string PantallaDe(Form formulario)
        {
            foreach (KeyValuePair<string, Form> par in abiertas)
                if (par.Value == formulario) return par.Key;
            return string.Empty;
        }

        public static void Abrir(Form actual, string pantalla)
        {
            // Los Tags de los formularios llevan el prefijo "Frm"; el registro usa
            // el nombre lógico de la pantalla (por ejemplo, "Pagos").
            if (pantalla != null && pantalla.StartsWith("Frm", StringComparison.Ordinal))
                pantalla = pantalla.Substring(3);

            OrigenBaseDatos origen = new ConfiguracionConexionNegocio().ObtenerOrigenElegido();
            if (!ModuloDisponible(origen, pantalla))
            {
                MessageBox.Show("Este módulo todavía no está adaptado para Supabase.",
                    "Módulo pendiente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Form destino;

            if (!abiertas.TryGetValue(pantalla, out destino))
            {
                Type tipo = typeof(Navegacion).Assembly.GetType("SistemaTelefonia.Presentacion.Frm" + pantalla);
                if (tipo == null)
                {
                    MessageBox.Show("La pantalla " + pantalla + " no existe.");
                    return;
                }

                destino = (Form)Activator.CreateInstance(tipo);
                Registrar(pantalla, destino);
            }

            if (destino == actual)
                return;

            // La nueva pantalla aparece en la misma posición y con el mismo tamaño.
            destino.StartPosition = FormStartPosition.Manual;
            if (actual.WindowState == FormWindowState.Normal)
            {
                destino.Location = actual.Location;
                destino.Size = actual.Size;
            }
            destino.WindowState = actual.WindowState;
            PrepararPresentacion(destino, actual);

            destino.Show();
            actual.Hide();
        }

        // Mantiene el mismo tema y tamaño adaptable al cambiar entre pantallas.
        private static void PrepararPresentacion(Form destino, Form referencia)
        {
            AjustesDisenoCrud.AjustarVentana(destino, referencia);
            TemaVisualCrud.Aplicar(destino, PreferenciasVisuales.Tema);
        }
    }
}
