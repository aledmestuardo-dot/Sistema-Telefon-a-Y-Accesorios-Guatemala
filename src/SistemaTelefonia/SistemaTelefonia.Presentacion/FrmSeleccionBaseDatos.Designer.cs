namespace SistemaTelefonia.Presentacion
{
    partial class FrmSeleccionBaseDatos
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTarjeta;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlSqlServer;
        private System.Windows.Forms.Panel pnlDockerMarca;
        private System.Windows.Forms.RadioButton rbSqlServer;
        private System.Windows.Forms.Label lblSqlServerTitulo;
        private System.Windows.Forms.Label lblSqlServerDetalle;
        private System.Windows.Forms.Panel pnlSupabase;
        private System.Windows.Forms.Panel pnlSupabaseMarca;
        private System.Windows.Forms.RadioButton rbSupabase;
        private System.Windows.Forms.Label lblSupabaseTitulo;
        private System.Windows.Forms.Label lblSupabaseDetalle;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTarjeta = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlSqlServer = new System.Windows.Forms.Panel();
            this.pnlDockerMarca = new System.Windows.Forms.Panel();
            this.rbSqlServer = new System.Windows.Forms.RadioButton();
            this.lblSqlServerTitulo = new System.Windows.Forms.Label();
            this.lblSqlServerDetalle = new System.Windows.Forms.Label();
            this.pnlSupabase = new System.Windows.Forms.Panel();
            this.pnlSupabaseMarca = new System.Windows.Forms.Panel();
            this.rbSupabase = new System.Windows.Forms.RadioButton();
            this.lblSupabaseTitulo = new System.Windows.Forms.Label();
            this.lblSupabaseDetalle = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlTarjeta.SuspendLayout();
            this.pnlSqlServer.SuspendLayout();
            this.pnlSupabase.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTarjeta
            //
            this.pnlTarjeta.BackColor = System.Drawing.Color.FromArgb(10, 39, 70);
            this.pnlTarjeta.Controls.Add(this.lblTitulo);
            this.pnlTarjeta.Controls.Add(this.lblSubtitulo);
            this.pnlTarjeta.Controls.Add(this.pnlSqlServer);
            this.pnlTarjeta.Controls.Add(this.pnlSupabase);
            this.pnlTarjeta.Controls.Add(this.lblEstado);
            this.pnlTarjeta.Controls.Add(this.btnContinuar);
            this.pnlTarjeta.Controls.Add(this.btnCancelar);
            this.pnlTarjeta.Location = new System.Drawing.Point(30, 25);
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.Size = new System.Drawing.Size(960, 590);
            this.pnlTarjeta.TabIndex = 0;
            this.pnlTarjeta.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlTarjeta_Paint);
            //
            // lblTitulo
            //
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(48, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(864, 64);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Seleccionar base de datos";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(190, 211, 232);
            this.lblSubtitulo.Location = new System.Drawing.Point(48, 90);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(864, 34);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Elige el origen que utilizará el sistema para consultar y guardar los datos.";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSqlServer
            // 
            this.pnlSqlServer.BackColor = System.Drawing.Color.FromArgb(36, 150, 237);
            this.pnlSqlServer.Controls.Add(this.pnlDockerMarca);
            this.pnlSqlServer.Controls.Add(this.rbSqlServer);
            this.pnlSqlServer.Controls.Add(this.lblSqlServerTitulo);
            this.pnlSqlServer.Controls.Add(this.lblSqlServerDetalle);
            this.pnlSqlServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlSqlServer.Location = new System.Drawing.Point(48, 150);
            this.pnlSqlServer.Name = "pnlSqlServer";
            this.pnlSqlServer.Size = new System.Drawing.Size(400, 300);
            this.pnlSqlServer.TabIndex = 2;
            this.pnlSqlServer.Click += new System.EventHandler(this.PnlSqlServer_Click);
            this.pnlSqlServer.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSqlServer_Paint);
            //
            // pnlDockerMarca
            //
            this.pnlDockerMarca.BackColor = System.Drawing.Color.Transparent;
            this.pnlDockerMarca.Location = new System.Drawing.Point(154, 38);
            this.pnlDockerMarca.Name = "pnlDockerMarca";
            this.pnlDockerMarca.Size = new System.Drawing.Size(92, 86);
            this.pnlDockerMarca.TabIndex = 0;
            this.pnlDockerMarca.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlDockerMarca_Paint);
            this.pnlDockerMarca.Click += new System.EventHandler(this.PnlSqlServer_Click);
            // 
            // rbSqlServer
            // 
            this.rbSqlServer.AutoSize = true;
            this.rbSqlServer.BackColor = System.Drawing.Color.Transparent;
            this.rbSqlServer.ForeColor = System.Drawing.Color.White;
            this.rbSqlServer.Location = new System.Drawing.Point(364, 18);
            this.rbSqlServer.Name = "rbSqlServer";
            this.rbSqlServer.Size = new System.Drawing.Size(14, 13);
            this.rbSqlServer.TabIndex = 0;
            this.rbSqlServer.UseVisualStyleBackColor = false;
            this.rbSqlServer.CheckedChanged += new System.EventHandler(this.RbSqlServer_CheckedChanged);
            //
            // lblSqlServerTitulo
            // 
            this.lblSqlServerTitulo.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblSqlServerTitulo.ForeColor = System.Drawing.Color.White;
            this.lblSqlServerTitulo.Location = new System.Drawing.Point(20, 134);
            this.lblSqlServerTitulo.Name = "lblSqlServerTitulo";
            this.lblSqlServerTitulo.Size = new System.Drawing.Size(360, 42);
            this.lblSqlServerTitulo.TabIndex = 1;
            this.lblSqlServerTitulo.Text = "Docker";
            this.lblSqlServerTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSqlServerTitulo.Click += new System.EventHandler(this.PnlSqlServer_Click);
            //
            // lblSqlServerDetalle
            // 
            this.lblSqlServerDetalle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSqlServerDetalle.ForeColor = System.Drawing.Color.White;
            this.lblSqlServerDetalle.Location = new System.Drawing.Point(20, 188);
            this.lblSqlServerDetalle.Name = "lblSqlServerDetalle";
            this.lblSqlServerDetalle.Size = new System.Drawing.Size(360, 54);
            this.lblSqlServerDetalle.TabIndex = 2;
            this.lblSqlServerDetalle.Text = "SQL Server en Docker\r\nBase de datos local de este equipo";
            this.lblSqlServerDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSqlServerDetalle.Click += new System.EventHandler(this.PnlSqlServer_Click);
            // 
            // pnlSupabase
            // 
            this.pnlSupabase.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            this.pnlSupabase.Controls.Add(this.pnlSupabaseMarca);
            this.pnlSupabase.Controls.Add(this.rbSupabase);
            this.pnlSupabase.Controls.Add(this.lblSupabaseTitulo);
            this.pnlSupabase.Controls.Add(this.lblSupabaseDetalle);
            this.pnlSupabase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlSupabase.Location = new System.Drawing.Point(512, 150);
            this.pnlSupabase.Name = "pnlSupabase";
            this.pnlSupabase.Size = new System.Drawing.Size(400, 300);
            this.pnlSupabase.TabIndex = 3;
            this.pnlSupabase.Click += new System.EventHandler(this.PnlSupabase_Click);
            this.pnlSupabase.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSupabase_Paint);
            //
            // pnlSupabaseMarca
            //
            this.pnlSupabaseMarca.BackColor = System.Drawing.Color.Transparent;
            this.pnlSupabaseMarca.Location = new System.Drawing.Point(160, 38);
            this.pnlSupabaseMarca.Name = "pnlSupabaseMarca";
            this.pnlSupabaseMarca.Size = new System.Drawing.Size(80, 80);
            this.pnlSupabaseMarca.TabIndex = 0;
            this.pnlSupabaseMarca.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSupabaseMarca_Paint);
            this.pnlSupabaseMarca.Click += new System.EventHandler(this.PnlSupabase_Click);
            // 
            // rbSupabase
            // 
            this.rbSupabase.AutoSize = true;
            this.rbSupabase.BackColor = System.Drawing.Color.Transparent;
            this.rbSupabase.ForeColor = System.Drawing.Color.White;
            this.rbSupabase.Location = new System.Drawing.Point(364, 18);
            this.rbSupabase.Name = "rbSupabase";
            this.rbSupabase.Size = new System.Drawing.Size(14, 13);
            this.rbSupabase.TabIndex = 0;
            this.rbSupabase.UseVisualStyleBackColor = false;
            this.rbSupabase.CheckedChanged += new System.EventHandler(this.RbSupabase_CheckedChanged);
            //
            // lblSupabaseTitulo
            // 
            this.lblSupabaseTitulo.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblSupabaseTitulo.ForeColor = System.Drawing.Color.White;
            this.lblSupabaseTitulo.Location = new System.Drawing.Point(20, 143);
            this.lblSupabaseTitulo.Name = "lblSupabaseTitulo";
            this.lblSupabaseTitulo.Size = new System.Drawing.Size(360, 42);
            this.lblSupabaseTitulo.TabIndex = 1;
            this.lblSupabaseTitulo.Text = "Supabase";
            this.lblSupabaseTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSupabaseTitulo.Click += new System.EventHandler(this.PnlSupabase_Click);
            //
            // lblSupabaseDetalle
            // 
            this.lblSupabaseDetalle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSupabaseDetalle.ForeColor = System.Drawing.Color.White;
            this.lblSupabaseDetalle.Location = new System.Drawing.Point(20, 197);
            this.lblSupabaseDetalle.Name = "lblSupabaseDetalle";
            this.lblSupabaseDetalle.Size = new System.Drawing.Size(360, 88);
            this.lblSupabaseDetalle.TabIndex = 2;
            this.lblSupabaseDetalle.Text = "PostgreSQL en la nube\r\nBase compartida del proyecto";
            this.lblSupabaseDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSupabaseDetalle.Click += new System.EventHandler(this.PnlSupabase_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(190, 211, 232);
            this.lblEstado.Location = new System.Drawing.Point(48, 470);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(864, 38);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "Se recordará tu selección y podrás cambiarla al iniciar la aplicación.";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnContinuar
            // 
            this.btnContinuar.BackColor = System.Drawing.Color.FromArgb(0, 112, 210);
            this.btnContinuar.FlatAppearance.BorderSize = 0;
            this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnContinuar.ForeColor = System.Drawing.Color.White;
            this.btnContinuar.Location = new System.Drawing.Point(690, 526);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(222, 46);
            this.btnContinuar.TabIndex = 5;
            this.btnContinuar.Text = "Probar y continuar";
            this.btnContinuar.UseVisualStyleBackColor = false;
            this.btnContinuar.Click += new System.EventHandler(this.BtnContinuar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(91, 108, 126);
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(566, 526);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(112, 46);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // FrmSeleccionBaseDatos
            // 
            this.AcceptButton = this.btnContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 25, 47);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(1020, 640);
            this.Controls.Add(this.pnlTarjeta);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSeleccionBaseDatos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Telefonía - Seleccionar base de datos";
            this.pnlTarjeta.ResumeLayout(false);
            this.pnlSqlServer.ResumeLayout(false);
            this.pnlSqlServer.PerformLayout();
            this.pnlSupabase.ResumeLayout(false);
            this.pnlSupabase.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
