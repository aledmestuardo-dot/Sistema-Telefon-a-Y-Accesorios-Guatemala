using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    partial class FrmLogin
    {
        private Label lblTitulo, lblUsuario, lblClave, lblMarca, lblSubtitulo;
        private TextBox txtUsuario, txtClave;
        private Panel pnlCampos, pnlTarjeta, pnlMarca;

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.btnEntrar = new SistemaTelefonia.Presentacion.FrmLogin.GradientButton();
            this.btnCambiarConexion = new System.Windows.Forms.Button();
            this.pnlCampos = new System.Windows.Forms.Panel();
            this.pnlTarjeta = new System.Windows.Forms.Panel();
            this.lblIconoUsuario = new System.Windows.Forms.Label();
            this.pnlMarca = new System.Windows.Forms.Panel();
            this.pnlCampos.SuspendLayout();
            this.pnlTarjeta.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.lblTitulo.Location = new System.Drawing.Point(24, 170);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(288, 38);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "¡Bienvenido!";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.lblUsuario.Location = new System.Drawing.Point(2, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(275, 20);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "USUARIO";
            // 
            // lblClave
            // 
            this.lblClave.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblClave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.lblClave.Location = new System.Drawing.Point(2, 69);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(275, 20);
            this.lblClave.TabIndex = 2;
            this.lblClave.Text = "CONTRASEÑA";
            // 
            // lblMarca
            // 
            this.lblMarca.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(210)))));
            this.lblMarca.Location = new System.Drawing.Point(28, 20);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(280, 26);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "SISTEMA DE TELEFONÍA MÓVIL";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(101)))), ((int)(((byte)(130)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(20, 205);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(296, 25);
            this.lblSubtitulo.TabIndex = 3;
            this.lblSubtitulo.Text = "al sistema de telefonía móvil de Guatemala";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUsuario
            // 
            this.txtUsuario.BackColor = System.Drawing.Color.White;
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.txtUsuario.Location = new System.Drawing.Point(0, 22);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(280, 32);
            this.txtUsuario.TabIndex = 1;
            // 
            // txtClave
            // 
            this.txtClave.BackColor = System.Drawing.Color.White;
            this.txtClave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClave.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtClave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.txtClave.Location = new System.Drawing.Point(0, 91);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(280, 32);
            this.txtClave.TabIndex = 3;
            this.txtClave.UseSystemPasswordChar = true;
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(210)))));
            this.btnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.FlatAppearance.BorderSize = 0;
            this.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEntrar.ForeColor = System.Drawing.Color.White;
            this.btnEntrar.Location = new System.Drawing.Point(0, 151);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(280, 45);
            this.btnEntrar.TabIndex = 4;
            this.btnEntrar.Text = "INICIAR SESIÓN";
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.BtnEntrar_Click);
            //
            // btnCambiarConexion
            // 
            this.btnCambiarConexion.BackColor = System.Drawing.Color.Transparent;
            this.btnCambiarConexion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambiarConexion.FlatAppearance.BorderSize = 0;
            this.btnCambiarConexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarConexion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnCambiarConexion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(170)))));
            this.btnCambiarConexion.Location = new System.Drawing.Point(0, 202);
            this.btnCambiarConexion.Name = "btnCambiarConexion";
            this.btnCambiarConexion.Size = new System.Drawing.Size(280, 32);
            this.btnCambiarConexion.TabIndex = 5;
            this.btnCambiarConexion.Text = "CAMBIAR CONEXIÓN";
            this.btnCambiarConexion.UseVisualStyleBackColor = false;
            this.btnCambiarConexion.Click += new System.EventHandler(this.BtnCambiarConexion_Click);
            //
            // pnlCampos
            // 
            this.pnlCampos.BackColor = System.Drawing.Color.Transparent;
            this.pnlCampos.Controls.Add(this.lblUsuario);
            this.pnlCampos.Controls.Add(this.txtUsuario);
            this.pnlCampos.Controls.Add(this.lblClave);
            this.pnlCampos.Controls.Add(this.txtClave);
            this.pnlCampos.Controls.Add(this.btnEntrar);
            this.pnlCampos.Controls.Add(this.btnCambiarConexion);
            this.pnlCampos.Location = new System.Drawing.Point(28, 245);
            this.pnlCampos.Name = "pnlCampos";
            this.pnlCampos.Size = new System.Drawing.Size(280, 237);
            this.pnlCampos.TabIndex = 4;
            // 
            // pnlTarjeta
            // 
            this.pnlTarjeta.BackColor = System.Drawing.Color.Transparent;
            this.pnlTarjeta.Controls.Add(this.lblMarca);
            this.pnlTarjeta.Controls.Add(this.lblIconoUsuario);
            this.pnlTarjeta.Controls.Add(this.lblTitulo);
            this.pnlTarjeta.Controls.Add(this.lblSubtitulo);
            this.pnlTarjeta.Controls.Add(this.pnlCampos);
            this.pnlTarjeta.Location = new System.Drawing.Point(222, 35);
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.Padding = new System.Windows.Forms.Padding(28, 20, 28, 25);
            this.pnlTarjeta.Size = new System.Drawing.Size(336, 490);
            this.pnlTarjeta.TabIndex = 1;
            this.pnlTarjeta.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlTarjeta_Paint);
            // 
            // lblIconoUsuario
            // 
            this.lblIconoUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(239)))), ((int)(((byte)(252)))));
            this.lblIconoUsuario.Font = new System.Drawing.Font("Segoe UI Symbol", 47F);
            this.lblIconoUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(210)))));
            this.lblIconoUsuario.Location = new System.Drawing.Point(117, 55);
            this.lblIconoUsuario.Name = "lblIconoUsuario";
            this.lblIconoUsuario.Size = new System.Drawing.Size(102, 102);
            this.lblIconoUsuario.TabIndex = 1;
            this.lblIconoUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIconoUsuario.Paint += new System.Windows.Forms.PaintEventHandler(this.LblIconoUsuario_Paint);
            // 
            // pnlMarca
            // 
            this.pnlMarca.BackColor = System.Drawing.Color.Transparent;
            this.pnlMarca.Location = new System.Drawing.Point(0, 0);
            this.pnlMarca.Name = "pnlMarca";
            this.pnlMarca.Size = new System.Drawing.Size(205, 560);
            this.pnlMarca.TabIndex = 0;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btnEntrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(19)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(960, 680);
            this.Controls.Add(this.pnlMarca);
            this.Controls.Add(this.pnlTarjeta);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 480);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acceso al sistema";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmLogin_Paint);
            this.pnlCampos.ResumeLayout(false);
            this.pnlCampos.PerformLayout();
            this.pnlTarjeta.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void FrmLogin_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush fondo = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(5, 17, 34),
                Color.FromArgb(13, 43, 76),
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(fondo, this.ClientRectangle);
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen lineas = new Pen(Color.FromArgb(28, 91, 148, 190), 1F))
            using (SolidBrush nodos = new SolidBrush(Color.FromArgb(100, 95, 190, 235)))
            {
                Point[] puntos =
                {
                    new Point(0, ClientSize.Height / 3), new Point(ClientSize.Width / 5, ClientSize.Height / 3 + 72),
                    new Point(ClientSize.Width / 3, ClientSize.Height / 2), new Point(ClientSize.Width * 4 / 5, ClientSize.Height / 3),
                    new Point(ClientSize.Width, ClientSize.Height / 2), new Point(ClientSize.Width * 2 / 3, ClientSize.Height * 3 / 4),
                    new Point(ClientSize.Width / 5, ClientSize.Height), new Point(ClientSize.Width * 4 / 5, 0)
                };
                for (int i = 0; i < puntos.Length - 1; i += 2)
                {
                    e.Graphics.DrawLine(lineas, puntos[i], puntos[i + 1]);
                    e.Graphics.FillEllipse(nodos, puntos[i].X - 3, puntos[i].Y - 3, 6, 6);
                    e.Graphics.FillEllipse(nodos, puntos[i + 1].X - 3, puntos[i + 1].Y - 3, 6, 6);
                }
                using (SolidBrush brillo = new SolidBrush(Color.FromArgb(13, 35, 153, 220)))
                {
                    e.Graphics.FillEllipse(brillo, ClientSize.Width - 310, -170, 430, 430);
                    e.Graphics.FillEllipse(brillo, -220, ClientSize.Height - 220, 450, 450);
                }
            }
        }

        private void PnlTarjeta_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borde = new Rectangle(0, 0, pnlTarjeta.Width - 1, pnlTarjeta.Height - 1);
            using (GraphicsPath ruta = CrearRectanguloRedondeado(borde, 28))
            using (LinearGradientBrush relleno = new LinearGradientBrush(
                borde, Color.FromArgb(17, 38, 64), Color.FromArgb(12, 27, 47), LinearGradientMode.Vertical))
            using (Pen contorno = new Pen(Color.FromArgb(70, 131, 174, 211), 1F))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(relleno, ruta);
                using (SolidBrush brillo = new SolidBrush(Color.FromArgb(16, 58, 150, 219)))
                    e.Graphics.FillEllipse(brillo, pnlTarjeta.Width - 200, -90, 260, 220);
                e.Graphics.DrawPath(contorno, ruta);
            }
        }

        private void LblIconoUsuario_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle area = new Rectangle(3, 3, lblIconoUsuario.Width - 7, lblIconoUsuario.Height - 7);
            using (LinearGradientBrush fondo = new LinearGradientBrush(
                area, Color.FromArgb(37, 92, 139), Color.FromArgb(20, 54, 91), 45F))
            using (Pen borde = new Pen(Color.FromArgb(87, 161, 213), 1.5F))
            using (SolidBrush silueta = new SolidBrush(Color.FromArgb(202, 226, 247)))
            {
                e.Graphics.FillEllipse(fondo, area);
                e.Graphics.DrawEllipse(borde, area);
                float centro = lblIconoUsuario.Width / 2F;
                e.Graphics.FillEllipse(silueta, centro - 10, 18, 20, 22);
                e.Graphics.FillEllipse(silueta, centro - 21, 43, 42, 24);
                e.Graphics.FillRectangle(silueta, centro - 21, 54, 42, 12);
            }
        }

        private static GraphicsPath CrearRectanguloRedondeado(Rectangle rectangulo, int radio)
        {
            int diametro = radio * 2;
            GraphicsPath ruta = new GraphicsPath();
            ruta.AddArc(rectangulo.X, rectangulo.Y, diametro, diametro, 180, 90);
            ruta.AddArc(rectangulo.Right - diametro, rectangulo.Y, diametro, diametro, 270, 90);
            ruta.AddArc(rectangulo.Right - diametro, rectangulo.Bottom - diametro, diametro, diametro, 0, 90);
            ruta.AddArc(rectangulo.X, rectangulo.Bottom - diametro, diametro, diametro, 90, 90);
            ruta.CloseFigure();
            return ruta;
        }

        private GradientButton btnEntrar;
        private Button btnCambiarConexion;
        private Label lblIconoUsuario;

        private sealed class GradientButton : Button
        {
            private bool hovered;
            private bool presionado;

            protected override void OnMouseEnter(System.EventArgs e) { hovered = true; Invalidate(); base.OnMouseEnter(e); }
            protected override void OnMouseLeave(System.EventArgs e) { hovered = false; presionado = false; Invalidate(); base.OnMouseLeave(e); }
            protected override void OnMouseDown(MouseEventArgs e) { presionado = true; Invalidate(); base.OnMouseDown(e); }
            protected override void OnMouseUp(MouseEventArgs e) { presionado = false; Invalidate(); base.OnMouseUp(e); }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle area = new Rectangle(0, 0, Width - 1, Height - 1);
                Color inicio = presionado ? Color.FromArgb(14, 75, 153) :
                    hovered ? Color.FromArgb(53, 169, 243) : Color.FromArgb(38, 151, 226);
                Color fin = presionado ? Color.FromArgb(28, 91, 190) :
                    hovered ? Color.FromArgb(72, 125, 235) : Color.FromArgb(56, 91, 211);
                using (GraphicsPath ruta = CrearRectanguloRedondeado(area, 11))
                using (LinearGradientBrush gradiente = new LinearGradientBrush(
                    area, inicio, fin, 0F))
                using (SolidBrush texto = new SolidBrush(ForeColor))
                using (Pen borde = new Pen(Color.FromArgb(65, 197, 245), 1F))
                {
                    e.Graphics.FillPath(gradiente, ruta);
                    e.Graphics.DrawPath(borde, ruta);
                    StringFormat alineacion = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(Text, Font, texto, area, alineacion);
                }
            }
        }
    }
}
