using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmLogin : Form
    {
        public static FrmLogin Instancia { get; set; }
        private readonly UsuariosNegocio usuarios = new UsuariosNegocio();
        private readonly ConfiguracionConexionNegocio configuracion = new ConfiguracionConexionNegocio();

        public FrmLogin()
        {
            InitializeComponent();
            ConfigurarDisenoProfesional();
            AjustarDisenoAlMonitor();
            lblUsuario.Text = configuracion.ObtenerOrigenElegido() == OrigenBaseDatos.Supabase
                ? "Correo o usuario:"
                : "Usuario:";
        }

        // Borra credenciales anteriores al volver a la pantalla tras cerrar sesión.
        internal void LimpiarCredenciales()
        {
            txtUsuario.Clear();
            txtClave.Clear();
            txtUsuario.Focus();
        }

        private void AjustarDisenoAlMonitor()
        {
            Rectangle area = Screen.FromControl(this).WorkingArea;
            int anchoCliente = Math.Max(420, Math.Min(960, area.Width - 24));
            int altoCliente = Math.Max(480, Math.Min(680, area.Height - 40));
            MinimumSize = Size.Empty;
            ClientSize = new System.Drawing.Size(anchoCliente, altoCliente);

            float escala = Math.Min(1F, Math.Min(
                (anchoCliente - 32) / (float)pnlTarjeta.Width,
                (altoCliente - 32) / (float)pnlTarjeta.Height));
            if (escala < 1F)
                pnlTarjeta.Scale(new System.Drawing.SizeF(escala, escala));

            pnlTarjeta.Location = new System.Drawing.Point(
                Math.Max(8, (ClientSize.Width - pnlTarjeta.Width) / 2),
                Math.Max(8, (ClientSize.Height - pnlTarjeta.Height) / 2));
        }

        // Organiza la interfaz con la referencia visual sin cambiar el flujo de acceso.
        private void ConfigurarDisenoProfesional()
        {
            pnlTarjeta.Size = new Size(450, 520);
            pnlTarjeta.Padding = new Padding(0);
            pnlTarjeta.BackColor = Color.Transparent;
            pnlTarjeta.Controls.SetChildIndex(lblMarca, 0);

            lblMarca.Text = "TELEFONÍA & ACCESORIOS";
            lblMarca.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMarca.ForeColor = Color.FromArgb(188, 213, 237);
            lblMarca.SetBounds(25, 20, 400, 28);

            lblIconoUsuario.BackColor = Color.Transparent;
            lblIconoUsuario.ForeColor = Color.FromArgb(94, 190, 255);
            lblIconoUsuario.SetBounds(183, 58, 84, 84);

            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.SetBounds(30, 150, 390, 40);
            lblSubtitulo.Font = new Font("Segoe UI", 9.5F);
            lblSubtitulo.ForeColor = Color.FromArgb(166, 190, 215);
            lblSubtitulo.Text = "Accede a tu sistema de gestión";
            lblSubtitulo.SetBounds(30, 190, 390, 28);

            pnlCampos.BackColor = Color.Transparent;
            pnlCampos.SetBounds(50, 235, 350, 260);
            lblUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.FromArgb(208, 224, 240);
            lblUsuario.SetBounds(2, 0, 346, 22);
            lblClave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblClave.ForeColor = Color.FromArgb(208, 224, 240);
            lblClave.SetBounds(2, 82, 346, 22);

            ConfigurarCampoLogin(txtUsuario, IconChar.User, "pnlCampoUsuario", 24);
            ConfigurarCampoLogin(txtClave, IconChar.Lock, "pnlCampoClave", 106);

            btnEntrar.SetBounds(0, 168, 350, 48);
            btnEntrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEntrar.Text = "INICIAR SESIÓN";
            btnCambiarConexion.SetBounds(0, 222, 350, 30);
            btnCambiarConexion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCambiarConexion.ForeColor = Color.FromArgb(141, 194, 235);
            btnCambiarConexion.Text = "CAMBIAR CONEXIÓN";
            btnCambiarConexion.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 54, 82);
        }

        private void ConfigurarCampoLogin(TextBox caja, IconChar icono, string nombre, int superior)
        {
            pnlCampos.Controls.Remove(caja);
            caja.BorderStyle = BorderStyle.None;
            caja.BackColor = Color.FromArgb(27, 49, 75);
            caja.ForeColor = Color.White;
            caja.Font = new Font("Segoe UI", 11F);
            caja.SetBounds(48, 10, 288, 28);

            Panel campo = new Panel
            {
                Name = nombre,
                Bounds = new Rectangle(0, superior, 350, 48),
                BackColor = Color.FromArgb(27, 49, 75),
                Tag = false
            };
            campo.Paint += (s, e) => PintarCampoLogin((Panel)s, e);
            caja.Enter += (s, e) => { campo.Tag = true; campo.Invalidate(); };
            caja.Leave += (s, e) => { campo.Tag = false; campo.Invalidate(); };

            IconPictureBox imagen = new IconPictureBox
            {
                Name = nombre + "Icono",
                IconChar = icono,
                IconFont = IconFont.Solid,
                IconColor = Color.FromArgb(116, 164, 207),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(20, 20),
                Location = new Point(15, 14)
            };
            campo.Controls.Add(caja);
            campo.Controls.Add(imagen);
            imagen.BringToFront();
            pnlCampos.Controls.Add(campo);
            campo.SendToBack();
        }

        private void PintarCampoLogin(Panel campo, PaintEventArgs e)
        {
            Rectangle area = new Rectangle(0, 0, campo.Width - 1, campo.Height - 1);
            bool enfocado = campo.Tag is bool && (bool)campo.Tag;
            Color borde = enfocado ? Color.FromArgb(52, 171, 245) : Color.FromArgb(66, 94, 126);
            using (GraphicsPath ruta = CrearRutaRedondeada(area, 14))
            using (SolidBrush fondo = new SolidBrush(Color.FromArgb(27, 49, 75)))
            using (Pen contorno = new Pen(borde, enfocado ? 1.8F : 1F))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(fondo, ruta);
                e.Graphics.DrawPath(contorno, ruta);
            }
        }

        private static GraphicsPath CrearRutaRedondeada(Rectangle area, int radio)
        {
            int diametro = Math.Min(radio * 2, Math.Min(area.Width, area.Height));
            GraphicsPath ruta = new GraphicsPath();
            ruta.AddArc(area.Left, area.Top, diametro, diametro, 180, 90);
            ruta.AddArc(area.Right - diametro, area.Top, diametro, diametro, 270, 90);
            ruta.AddArc(area.Right - diametro, area.Bottom - diametro, diametro, diametro, 0, 90);
            ruta.AddArc(area.Left, area.Bottom - diametro, diametro, diametro, 90, 90);
            ruta.CloseFigure();
            return ruta;
        }

        private void BtnEntrar_Click(object sender, System.EventArgs e)
        {
            try
            {
                UsuariosEntidad usuario = usuarios.Autenticar(txtUsuario.Text, txtClave.Text);
                if (usuario == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos, o la cuenta está inactiva.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClave.Clear();
                    txtClave.Focus();
                    return;
                }

                // La sesión se conserva al abrir el menú cuando el origen es Supabase.
                FrmMenuPrincipal menu = new FrmMenuPrincipal(usuario);
                FrmMenuPrincipal.Instancia = menu;
                menu.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex),
                    "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Permite volver al selector si el usuario desea cambiar de base de datos.
        private void BtnCambiarConexion_Click(object sender, EventArgs e)
        {
            using (FrmSeleccionBaseDatos selector = new FrmSeleccionBaseDatos())
            {
                if (selector.ShowDialog(this) != DialogResult.OK)
                    return;
            }

            lblUsuario.Text = configuracion.ObtenerOrigenElegido() == OrigenBaseDatos.Supabase
                ? "Correo o usuario:"
                : "Usuario:";
            LimpiarCredenciales();
        }

        private void lblIconoUsuario_Click(object sender, EventArgs e)
        {

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
