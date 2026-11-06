using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmSeleccionBaseDatos : Form
    {
        private readonly ConfiguracionConexionNegocio configuracion = new ConfiguracionConexionNegocio();

        public FrmSeleccionBaseDatos()
        {
            InitializeComponent();
            AplicarEstiloModelo();
            CargarUltimaSeleccion();
            ActualizarEstiloSeleccion();
        }

        private void AplicarEstiloModelo()
        {
            Redondear(btnContinuar, 7);
            Redondear(btnCancelar, 7);
            Redondear(pnlSqlServer, 14);
            Redondear(pnlSupabase, 14);
        }

        private static void Redondear(Control control, int radio)
        {
            Rectangle r = control.ClientRectangle;
            if (r.Width <= 0 || r.Height <= 0) return;
            int d = radio * 2;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }

        private void ActualizarEstiloSeleccion()
        {
            if (pnlSqlServer == null || pnlSupabase == null) return;
            pnlSqlServer.Invalidate();
            pnlSupabase.Invalidate();
        }

        private void DibujarBordeTarjeta(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            bool seleccionado = control == pnlSqlServer ? rbSqlServer.Checked : rbSupabase.Checked;
            Color borde = seleccionado ? Color.FromArgb(0, 210, 235) : Color.FromArgb(61, 91, 120);
            int grosor = seleccionado ? 3 : 1;
            Rectangle r = new Rectangle(grosor / 2, grosor / 2, control.Width - grosor, control.Height - grosor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = CrearRectanguloRedondeado(r, 14))
            using (Pen pen = new Pen(borde, grosor))
                e.Graphics.DrawPath(pen, path);
        }

        private static GraphicsPath CrearRectanguloRedondeado(Rectangle r, int radio)
        {
            int d = radio * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void PnlTarjeta_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = new Rectangle(0, 0, pnlTarjeta.Width - 1, pnlTarjeta.Height - 1);
            using (GraphicsPath path = CrearRectanguloRedondeado(r, 16))
            using (Pen pen = new Pen(Color.FromArgb(24, 65, 101), 1))
                e.Graphics.DrawPath(pen, path);
        }

        private void PnlSqlServer_Paint(object sender, PaintEventArgs e)
        {
            DibujarBordeTarjeta(sender, e);
        }

        private void PnlSupabase_Paint(object sender, PaintEventArgs e)
        {
            DibujarBordeTarjeta(sender, e);
        }

        private void PnlDockerMarca_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Brush blanco = new SolidBrush(Color.White))
            {
                // Contenedores de la marca Docker.
                g.FillRectangle(blanco, 18, 30, 14, 13);
                g.FillRectangle(blanco, 34, 30, 14, 13);
                g.FillRectangle(blanco, 50, 30, 14, 13);
                g.FillRectangle(blanco, 34, 14, 14, 13);
                g.FillRectangle(blanco, 50, 14, 14, 13);
                g.FillRectangle(blanco, 66, 30, 14, 13);
                g.FillRectangle(blanco, 50, 0, 14, 12);

                // Casco de la ballena y cola, dibujados como formas vectoriales.
                using (GraphicsPath casco = new GraphicsPath())
                {
                    casco.AddBezier(14, 47, 31, 51, 67, 51, 83, 43);
                    casco.AddBezier(83, 43, 80, 61, 66, 73, 47, 75);
                    casco.AddBezier(47, 75, 26, 76, 12, 65, 10, 50);
                    casco.CloseFigure();
                    g.FillPath(blanco, casco);
                }
                Point[] cola = { new Point(77, 44), new Point(83, 31), new Point(90, 38), new Point(88, 47) };
                g.FillPolygon(blanco, cola);
            }
        }

        private void PnlSupabaseMarca_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Point[] rayo =
            {
                new Point(35, 13), new Point(12, 42), new Point(31, 42),
                new Point(27, 68), new Point(55, 34), new Point(37, 34)
            };
            using (Brush verde = new SolidBrush(Color.FromArgb(62, 207, 142)))
                g.FillPolygon(verde, rayo);
        }

        private void CargarUltimaSeleccion()
        {
            try
            {
                rbSupabase.Checked = configuracion.ObtenerOrigenElegido() == OrigenBaseDatos.Supabase;
                rbSqlServer.Checked = !rbSupabase.Checked;
            }
            catch (Exception ex)
            {
                rbSqlServer.Checked = true;
                lblEstado.Text = MensajesError.Resumir(ex);
            }
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            OrigenBaseDatos origen = rbSupabase.Checked
                ? OrigenBaseDatos.Supabase
                : OrigenBaseDatos.SqlServer;
            string mensaje;

            btnContinuar.Enabled = false;
            lblEstado.Text = "Probando la conexión seleccionada...";
            UseWaitCursor = true;
            try
            {
                if (!configuracion.ProbarConexion(origen, out mensaje))
                {
                    string resumen = MensajesError.Resumir(mensaje);
                    lblEstado.Text = resumen;
                    MessageBox.Show(resumen, "No fue posible conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                configuracion.GuardarOrigenElegido(origen);
                lblEstado.Text = mensaje;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblEstado.Text = MensajesError.Resumir(ex);
                MessageBox.Show(MensajesError.Resumir(ex), "Error de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                btnContinuar.Enabled = true;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PnlSqlServer_Click(object sender, EventArgs e)
        {
            rbSqlServer.Checked = true;
            rbSupabase.Checked = false;
            ActualizarEstiloSeleccion();
        }

        private void PnlSupabase_Click(object sender, EventArgs e)
        {
            rbSupabase.Checked = true;
            rbSqlServer.Checked = false;
            ActualizarEstiloSeleccion();
        }

        private void RbSqlServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSqlServer.Checked) rbSupabase.Checked = false;
            ActualizarEstiloSeleccion();
        }

        private void RbSupabase_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSupabase.Checked) rbSqlServer.Checked = false;
            ActualizarEstiloSeleccion();
        }
    }
}
