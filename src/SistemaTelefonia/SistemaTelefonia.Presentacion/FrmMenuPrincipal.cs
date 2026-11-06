using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmMenuPrincipal : Form
    {
        public static FrmMenuPrincipal Instancia { get; set; }

        private readonly Color fondo = Color.FromArgb(3, 22, 43);
        private readonly Color panel = Color.FromArgb(2, 25, 47);
        private readonly Color azul = Color.FromArgb(3, 43, 83);
        private readonly Color cian = Color.FromArgb(0, 203, 255);
        private readonly Color texto = Color.FromArgb(220, 230, 240);
        private ComboBox cboTema;
        private Button btnVistaCuadricula;
        private Button btnVistaLista;
        private Button btnDashboard;
        private Label lblTema;
        private TextBox txtBuscarModulo;
        private Label lblBuscarModulo;
        private Label lblSinResultados;
        private Label lblConteoModulos;
        private VistaModulos vistaActual;
        private Panel panelImagenMenu;
        private Panel panelModulos;
        private Image imagenMenu;
        private bool modoSupabase;

        public FrmMenuPrincipal()
        {
            InitializeComponent();
            modoSupabase = new ConfiguracionConexionNegocio().ObtenerOrigenElegido() == OrigenBaseDatos.Supabase;
            CargarImagenMenu();
            ConfigurarPanelImagen();
            ConfigurarMenu();
            AgregarAccesoDashboard();
            AplicarDisponibilidadPorOrigen();
            AplicarTemaMenu(PreferenciasVisuales.Tema);
            AplicarVista(PreferenciasVisuales.Vista, false);
            AjustesDisenoCrud.AjustarVentana(this);
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                WindowState = FormWindowState.Maximized;
        }

        public FrmMenuPrincipal(UsuariosEntidad usuario) : this()
        {
            if (usuario == null) return;
            string alias = string.IsNullOrWhiteSpace(usuario.Nombre) ? "Usuario" : usuario.Nombre.Trim();
            string correo = string.IsNullOrWhiteSpace(usuario.CorreoElectronico)
                ? string.Empty
                : " · " + usuario.CorreoElectronico.Trim();
            string origen = modoSupabase
                ? "Supabase · módulos compartidos"
                : "SQL Server (Docker)";
            lblSubtitulo.Text = "Sesión: " + alias + correo + "  |  " + origen;
        }

        private void AplicarDisponibilidadPorOrigen()
        {
            if (modoSupabase)
            {
                foreach (IconButton boton in flpContenido.Controls.OfType<IconButton>())
                    boton.Visible = Navegacion.ModuloDisponible(OrigenBaseDatos.Supabase, Convert.ToString(boton.Tag));

                lblSeccion.Text = "Módulos disponibles en Supabase";
            }
        }

        private void CargarImagenMenu()
        {
            string ruta = Path.Combine(Application.StartupPath, "Resources", "TelefoniaAccesorios.png");
            if (!File.Exists(ruta)) return;

            // Lee y clona el recurso para que el archivo no quede bloqueado en disco.
            using (FileStream archivo = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Image original = Image.FromStream(archivo))
                imagenMenu = new Bitmap(original);
        }

        private void ConfigurarPanelImagen()
        {
            // Reubica los controles existentes; sus eventos y navegación permanecen intactos.
            pnlCuerpo.Controls.Remove(flpContenido);
            pnlCuerpo.Controls.Remove(lblSeccion);
            pnlCuerpo.Controls.Clear();

            panelImagenMenu = new Panel
            {
                Name = "pnlImagenMenu",
                Dock = DockStyle.Left,
                Width = 390,
                BackColor = Color.FromArgb(2, 25, 52),
                Margin = Padding.Empty
            };
            panelImagenMenu.Paint += PintarImagenMenu;
            panelImagenMenu.Resize += (s, e) => panelImagenMenu.Invalidate();

            panelModulos = new Panel
            {
                Name = "pnlModulosMenu",
                Dock = DockStyle.Fill,
                BackColor = fondo,
                Padding = new Padding(24, 16, 24, 18)
            };
            panelModulos.Controls.Add(flpContenido);
            panelModulos.Controls.Add(lblSeccion);
            pnlCuerpo.Controls.Add(panelModulos);
            pnlCuerpo.Controls.Add(panelImagenMenu);
            pnlCuerpo.Resize += (s, e) => AjustarPanelImagen();
            panelModulos.Resize += (s, e) => AcomodarOpcionesVista();
            FormClosed += (s, e) =>
            {
                if (imagenMenu != null) { imagenMenu.Dispose(); imagenMenu = null; }
            };
            AjustarPanelImagen();
        }

        private void AjustarPanelImagen()
        {
            if (panelImagenMenu == null || panelModulos == null) return;
            bool mostrarImagen = pnlCuerpo.ClientSize.Width >= 1240 && pnlCuerpo.ClientSize.Height >= 520;
            panelImagenMenu.Visible = mostrarImagen;
            if (mostrarImagen)
                panelImagenMenu.Width = Math.Min(460, Math.Max(340, pnlCuerpo.ClientSize.Width * 32 / 100));
        }

        private void PintarImagenMenu(object sender, PaintEventArgs e)
        {
            Rectangle area = panelImagenMenu.ClientRectangle;
            if (area.Width <= 0 || area.Height <= 0) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (imagenMenu != null)
            {
                float escala = Math.Max(area.Width / (float)imagenMenu.Width, area.Height / (float)imagenMenu.Height);
                float ancho = imagenMenu.Width * escala;
                float alto = imagenMenu.Height * escala;
                RectangleF destino = new RectangleF((area.Width - ancho) / 2F, (area.Height - alto) / 2F, ancho, alto);
                e.Graphics.DrawImage(imagenMenu, destino);
            }
            else
            {
                using (LinearGradientBrush baseAzul = new LinearGradientBrush(area,
                    Color.FromArgb(10, 73, 127), Color.FromArgb(2, 25, 52), 90F))
                    e.Graphics.FillRectangle(baseAzul, area);
            }

            // El velo mantiene la imagen integrada con el menú y evita competir con los módulos.
            using (LinearGradientBrush velo = new LinearGradientBrush(area,
                Color.FromArgb(145, 2, 25, 52), Color.FromArgb(65, 2, 25, 52), LinearGradientMode.Horizontal))
                e.Graphics.FillRectangle(velo, area);

            // El mensaje da contexto al menú y se dibuja sobre la imagen, sin cubrir los módulos.
            using (Font titulo = new Font("Segoe UI", 25F, FontStyle.Bold))
            using (Font descripcion = new Font("Segoe UI", 10.5F, FontStyle.Regular))
            using (SolidBrush blanco = new SolidBrush(Color.White))
            using (SolidBrush secundario = new SolidBrush(Color.FromArgb(225, 235, 245)))
            {
                float x = 30F;
                float y = Math.Max(24F, area.Height - 132F);
                e.Graphics.DrawString("¡Bienvenido!", titulo, blanco, new PointF(x, y));
                e.Graphics.DrawString("Selecciona un módulo para administrar tus operaciones de telefonía y accesorios.",
                    descripcion, secundario, new RectangleF(x, y + 48F, area.Width - 60F, 66F));
            }
        }

        private void AgregarAccesoDashboard()
        {
            btnDashboard = new Button
            {
                Name = "btnDashboardGeneral",
                Text = "Dashboard",
                Size = new Size(142, 38),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.FromArgb(0, 150, 205),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.Click += (s, e) => new FrmDashboard().Show(this);
            panelModulos.Controls.Add(btnDashboard);
            AcomodarOpcionesVista();
            btnDashboard.BringToFront();
        }

        private void ConfigurarMenu()
        {
            // Los colores base se aplican desde AplicarTemaMenu para respetar el tema elegido.
            pnlCuerpo.BackColor = fondo;
            flpContenido.BackColor = fondo;
            lblSeccion.ForeColor = cian;

            flpContenido.Resize += (s, e) => AjustarBotones(flpContenido);
            pnlEncabezado.Resize += (s, e) => AcomodarSelectorTema();
            panelModulos.Resize += (s, e) => AcomodarBusquedaModulo();
            btnCerrar.Click += (s, e) => Navegacion.CerrarSesion(this);

            Color[] acentos =
            {
                Color.FromArgb(0, 203, 255), Color.FromArgb(47, 153, 255),
                Color.FromArgb(83, 182, 225), Color.FromArgb(108, 155, 255)
            };
            int indiceAcento = 0;
            foreach (Control control in flpContenido.Controls)
            {
                IconButton boton = control as IconButton;
                if (boton == null) continue;

                boton.Width = 320;
                boton.Height = 142;
                boton.Margin = new Padding(12);
                boton.Padding = new Padding(22, 16, 18, 16);
                Color acento = acentos[indiceAcento++ % acentos.Length];
                boton.BackColor = Color.FromArgb(17, 35, 58);
                boton.ForeColor = Color.FromArgb(230, 239, 249);
                boton.IconColor = acento;
                boton.IconSize = 48;
                boton.ImageAlign = ContentAlignment.MiddleLeft;
                boton.TextImageRelation = TextImageRelation.ImageBeforeText;
                boton.TextAlign = ContentAlignment.MiddleLeft;
                boton.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
                boton.FlatStyle = FlatStyle.Flat;
                boton.Cursor = Cursors.Hand;
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = Color.FromArgb(35, 61, 91);
                boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 55, 85);
                boton.Resize += (s, e) => AplicarEsquinasRedondeadas(boton, 16);
                boton.Click += (s, e) => Navegacion.Abrir(this, (string)boton.Tag);
                AplicarEsquinasRedondeadas(boton, 16);
            }

            CrearBusquedaModulo();
            CrearSelectorTema();
            CrearSelectorVista();
            AjustarBotones(flpContenido);
        }

        private void CrearBusquedaModulo()
        {
            lblBuscarModulo = new Label
            {
                Name = "lblBuscarModulo",
                Text = "Buscar módulo",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 230, 240)
            };
            txtBuscarModulo = new TextBox
            {
                Name = "txtBuscarModulo",
                AccessibleName = "Buscar módulos por nombre",
                Font = new Font("Segoe UI", 10F),
                Width = 250,
                Height = 28
            };
            lblSinResultados = new Label
            {
                Name = "lblSinModulosCoincidentes",
                Text = "No se encontraron módulos con ese nombre.",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Visible = false,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular)
            };
            lblConteoModulos = new Label
            {
                Name = "lblConteoModulos",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(166, 185, 211),
                AccessibleName = "Cantidad de módulos visibles"
            };

            txtBuscarModulo.TextChanged += (s, e) => FiltrarModulos();
            panelModulos.Controls.Add(lblBuscarModulo);
            panelModulos.Controls.Add(txtBuscarModulo);
            panelModulos.Controls.Add(lblSinResultados);
            panelModulos.Controls.Add(lblConteoModulos);
            lblBuscarModulo.BringToFront();
            txtBuscarModulo.BringToFront();
            AcomodarBusquedaModulo();
        }

        private void AcomodarBusquedaModulo()
        {
            if (txtBuscarModulo == null || lblBuscarModulo == null || panelModulos == null) return;
            int izquierda = panelModulos.Padding.Left;
            int arriba = panelModulos.Padding.Top + 43;
            lblBuscarModulo.Location = new Point(izquierda, arriba + 5);
            txtBuscarModulo.Location = new Point(izquierda + 112, arriba);
            txtBuscarModulo.Width = Math.Max(160, Math.Min(300,
                panelModulos.ClientSize.Width - izquierda - panelModulos.Padding.Right - 112));
            if (lblSinResultados != null)
            {
                lblSinResultados.Location = new Point(izquierda, 116);
                lblSinResultados.Size = new Size(Math.Max(100, panelModulos.ClientSize.Width - izquierda -
                    panelModulos.Padding.Right), Math.Max(60, panelModulos.ClientSize.Height - 132));
            }
            if (lblConteoModulos != null)
                lblConteoModulos.Location = new Point(izquierda, arriba + 36);
            txtBuscarModulo.BringToFront();
            lblBuscarModulo.BringToFront();
        }

        private void FiltrarModulos()
        {
            string criterio = NormalizarTexto(txtBuscarModulo.Text.Trim());
            int visibles = 0;
            foreach (IconButton boton in flpContenido.Controls.OfType<IconButton>())
            {
                bool disponible = Navegacion.ModuloDisponible(
                    modoSupabase ? OrigenBaseDatos.Supabase : OrigenBaseDatos.SqlServer,
                    Convert.ToString(boton.Tag));
                bool coincide = disponible && (criterio.Length == 0 || NormalizarTexto(boton.Text).Contains(criterio));
                boton.Visible = coincide;
                if (coincide) visibles++;
            }
            lblSinResultados.Visible = criterio.Length > 0 && visibles == 0;
            int disponibles = flpContenido.Controls.OfType<IconButton>().Count(boton => Navegacion.ModuloDisponible(
                modoSupabase ? OrigenBaseDatos.Supabase : OrigenBaseDatos.SqlServer,
                Convert.ToString(boton.Tag)));
            if (lblConteoModulos != null)
                lblConteoModulos.Text = criterio.Length == 0
                    ? disponibles + (disponibles == 1 ? " módulo disponible" : " módulos disponibles")
                    : visibles + " de " + disponibles + " módulos";
            if (lblConteoModulos != null) lblConteoModulos.BringToFront();
        }

        private static string NormalizarTexto(string texto)
        {
            string descompuesto = (texto ?? string.Empty).Normalize(NormalizationForm.FormD);
            StringBuilder resultado = new StringBuilder(descompuesto.Length);
            foreach (char caracter in descompuesto)
                if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                    resultado.Append(caracter);
            return resultado.ToString().Normalize(NormalizationForm.FormC).ToUpperInvariant();
        }

        private void CrearSelectorTema()
        {
            lblTema = new Label
            {
                Name = "lblTema",
                Text = "TEMA",
                AutoSize = false,
                Size = new Size(150, 18),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold)
            };
            cboTema = new ComboBox
            {
                Name = "cboTema",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F),
                Size = new Size(150, 30)
            };
            cboTema.Items.AddRange(new object[] { "Azul clásico", "Negro", "Claro" });
            cboTema.SelectedIndex = (int)PreferenciasVisuales.Tema;
            cboTema.SelectedIndexChanged += CboTema_SelectedIndexChanged;
            pnlEncabezado.Controls.Add(lblTema);
            pnlEncabezado.Controls.Add(cboTema);
            AcomodarSelectorTema();
        }

        private void CrearSelectorVista()
        {
            btnVistaCuadricula = CrearBotonVista("Cuadrícula", 94);
            btnVistaLista = CrearBotonVista("Lista", 64);
            btnVistaCuadricula.Click += (s, e) => AplicarVista(VistaModulos.Cuadricula, true);
            btnVistaLista.Click += (s, e) => AplicarVista(VistaModulos.Lista, true);
            panelModulos.Controls.Add(btnVistaCuadricula);
            panelModulos.Controls.Add(btnVistaLista);
            btnVistaCuadricula.BringToFront();
            btnVistaLista.BringToFront();
            AcomodarOpcionesVista();
        }

        private Button CrearBotonVista(string texto, int ancho)
        {
            return new Button
            {
                Text = texto,
                Size = new Size(ancho, 30),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
        }

        private void AcomodarSelectorTema()
        {
            if (cboTema == null || lblTema == null) return;
            int derecha = pnlEncabezado.ClientSize.Width - pnlEncabezado.Padding.Right;
            cboTema.Location = new Point(derecha - cboTema.Width, 54);
            lblTema.Location = new Point(derecha - lblTema.Width, 31);
        }

        private void AcomodarOpcionesVista()
        {
            if (btnVistaCuadricula == null || btnVistaLista == null || btnDashboard == null) return;
            int derecha = panelModulos.ClientSize.Width - panelModulos.Padding.Right;
            btnVistaLista.Location = new Point(derecha - btnVistaLista.Width, panelModulos.Padding.Top + 7);
            btnVistaCuadricula.Location = new Point(btnVistaLista.Left - 6 - btnVistaCuadricula.Width,
                panelModulos.Padding.Top + 7);
            btnDashboard.Location = new Point(btnVistaCuadricula.Left - 6 - btnDashboard.Width,
                panelModulos.Padding.Top + 7);
            bool hayEspacio = btnDashboard.Left >= panelModulos.Padding.Left;
            btnDashboard.Visible = hayEspacio;
            btnDashboard.Enabled = hayEspacio;
            if (!hayEspacio)
            {
                btnDashboard.Location = new Point(panelModulos.Padding.Left, panelModulos.Padding.Top + 43);
                btnDashboard.Visible = panelModulos.ClientSize.Width > 460;
                btnDashboard.Enabled = btnDashboard.Visible;
                flpContenido.Padding = new Padding(4, btnDashboard.Visible ? 98 : 62, 4, 18);
            }
            else
            {
                flpContenido.Padding = new Padding(4, 62, 4, 18);
            }
            btnDashboard.BringToFront();
            btnVistaCuadricula.BringToFront();
            btnVistaLista.BringToFront();
            flpContenido.BringToFront();
        }

        private void CboTema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTema.SelectedIndex < 0) return;
            TemaInterfaz tema = (TemaInterfaz)cboTema.SelectedIndex;
            PreferenciasVisuales.Guardar(tema, vistaActual);
            AplicarTemaMenu(tema);
            Navegacion.AplicarTemaAAbiertas(tema);
        }

        private void AplicarTemaMenu(TemaInterfaz tema)
        {
            bool claro = tema == TemaInterfaz.Claro;
            bool negro = tema == TemaInterfaz.Negro;
            Color fondo = claro ? Color.FromArgb(239, 245, 251) :
                (negro ? Color.FromArgb(17, 19, 21) : Color.FromArgb(3, 22, 43));
            Color encabezado = claro ? Color.White :
                (negro ? Color.FromArgb(12, 14, 16) : Color.FromArgb(2, 25, 47));
            Color superficie = claro ? Color.White :
                (negro ? Color.FromArgb(28, 31, 34) : Color.FromArgb(17, 35, 58));
            Color textoMenu = claro ? Color.FromArgb(7, 35, 66) : Color.FromArgb(235, 242, 249);
            Color borde = claro ? Color.FromArgb(215, 227, 239) :
                (negro ? Color.FromArgb(58, 65, 72) : Color.FromArgb(35, 61, 91));
            Color hover = claro ? Color.FromArgb(226, 241, 253) :
                (negro ? Color.FromArgb(45, 51, 57) : Color.FromArgb(25, 55, 85));

            pnlCuerpo.BackColor = fondo;
            panelModulos.BackColor = fondo;
            flpContenido.BackColor = fondo;
            pnlEncabezado.BackColor = encabezado;
            pnlPie.BackColor = encabezado;
            lblSeccion.ForeColor = claro ? Color.FromArgb(7, 35, 66) : cian;
            lblBuscarModulo.ForeColor = claro ? Color.FromArgb(7, 35, 66) : Color.FromArgb(220, 230, 240);
            if (lblConteoModulos != null)
                lblConteoModulos.ForeColor = claro ? Color.FromArgb(72, 101, 130) : Color.FromArgb(166, 185, 211);
            txtBuscarModulo.BackColor = claro ? Color.White : (negro ? Color.FromArgb(28, 31, 34) : Color.FromArgb(7, 35, 66));
            txtBuscarModulo.ForeColor = claro ? Color.FromArgb(7, 35, 66) : Color.FromArgb(235, 242, 249);
            lblSinResultados.ForeColor = claro ? Color.FromArgb(72, 101, 130) : Color.FromArgb(170, 190, 210);
            lblTitulo.ForeColor = claro ? Color.FromArgb(7, 35, 66) : Color.White;
            lblSubtitulo.ForeColor = claro ? Color.FromArgb(72, 101, 130) : Color.FromArgb(170, 190, 210);
            lblPie.ForeColor = claro ? Color.FromArgb(72, 101, 130) : Color.FromArgb(145, 165, 185);
            picLogo.IconColor = claro ? Color.FromArgb(0, 112, 210) : cian;

            foreach (IconButton boton in flpContenido.Controls.OfType<IconButton>())
            {
                boton.BackColor = superficie;
                boton.ForeColor = textoMenu;
                boton.FlatAppearance.BorderColor = borde;
                boton.FlatAppearance.MouseOverBackColor = hover;
            }

            cboTema.BackColor = claro ? Color.White : Color.FromArgb(235, 242, 249);
            cboTema.ForeColor = Color.FromArgb(7, 35, 66);
            lblTema.ForeColor = claro ? Color.FromArgb(7, 35, 66) : Color.FromArgb(220, 230, 240);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(165, 45, 51);
            ActualizarEstadoBotonesVista(claro, negro);
        }

        private void ActualizarEstadoBotonesVista(bool claro, bool negro)
        {
            if (btnVistaCuadricula == null || btnVistaLista == null) return;
            Color fondo = claro ? Color.White :
                (negro ? Color.FromArgb(28, 31, 34) : Color.FromArgb(3, 43, 83));
            Color texto = claro ? Color.FromArgb(7, 35, 66) : Color.FromArgb(235, 242, 249);
            Color activo = Color.FromArgb(0, 119, 204);
            foreach (Button boton in new[] { btnVistaCuadricula, btnVistaLista })
            {
                bool seleccionado = (boton == btnVistaLista && vistaActual == VistaModulos.Lista) ||
                    (boton == btnVistaCuadricula && vistaActual == VistaModulos.Cuadricula);
                boton.BackColor = seleccionado ? activo : fondo;
                boton.ForeColor = seleccionado ? Color.White : texto;
                boton.FlatAppearance.BorderColor = claro ? Color.FromArgb(215, 227, 239) :
                    (negro ? Color.FromArgb(58, 65, 72) : Color.FromArgb(35, 61, 91));
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.MouseOverBackColor = seleccionado ? activo : Color.FromArgb(0, 91, 170);
            }
        }

        private void AplicarVista(VistaModulos vista, bool guardar)
        {
            vistaActual = vista;
            bool lista = vista == VistaModulos.Lista;
            flpContenido.WrapContents = !lista;
            flpContenido.FlowDirection = lista ? FlowDirection.TopDown : FlowDirection.LeftToRight;
            AjustarBotones(flpContenido);
            ActualizarEstadoBotonesVista(PreferenciasVisuales.Tema == TemaInterfaz.Claro,
                PreferenciasVisuales.Tema == TemaInterfaz.Negro);
            if (guardar) PreferenciasVisuales.Guardar(PreferenciasVisuales.Tema, vista);
        }


        private void AjustarBotones(FlowLayoutPanel contenido)
        {
            if (contenido == null || contenido.IsDisposed || contenido.ClientSize.Width <= 0) return;

            if (vistaActual == VistaModulos.Lista)
            {
                contenido.Padding = new Padding(4, 62, 4, 18);
                int anchoLista = Math.Max(180, contenido.ClientSize.Width - contenido.Padding.Horizontal -
                    SystemInformation.VerticalScrollBarWidth - 8);
                foreach (IconButton botonLista in contenido.Controls.OfType<IconButton>())
                {
                    botonLista.Width = anchoLista;
                    botonLista.Height = 62;
                    botonLista.Margin = new Padding(8, 5, 8, 5);
                    botonLista.Padding = new Padding(20, 8, 14, 8);
                    botonLista.IconSize = 26;
                    botonLista.TextAlign = ContentAlignment.MiddleLeft;
                }
                return;
            }

            // Cuatro columnas en pantallas amplias; se adapta a ventanas más estrechas.
            int anchoDisponible = contenido.ClientSize.Width - contenido.Padding.Horizontal;
            int columnas = Math.Max(1, Math.Min(4, (anchoDisponible + 24) / 344));
            int espacioEntre = 24;
            int anchoBoton = Math.Max(220, (anchoDisponible - (columnas * espacioEntre)) / columnas);
            anchoBoton = Math.Min(anchoBoton, 390);
            int total = columnas * anchoBoton + (columnas * espacioEntre);
            int margenIzquierdo = Math.Max(12, (contenido.ClientSize.Width - total) / 2);
            contenido.Padding = new Padding(margenIzquierdo, 62, 12, 18);

            foreach (Control control in contenido.Controls)
            {
                control.Width = anchoBoton;
                control.Height = Math.Max(132, Math.Min(164, (int)(anchoBoton * 0.42)));
                control.Margin = new Padding(12);
                IconButton boton = control as IconButton;
                if (boton != null)
                {
                    boton.Padding = new Padding(22, 16, 18, 16);
                    boton.IconSize = 42;
                    boton.TextAlign = ContentAlignment.MiddleLeft;
                }
            }
        }

        private void AplicarEsquinasRedondeadas(Control control, int radio)
        {
            if (control.Width <= 0 || control.Height <= 0) return;
            using (var ruta = new GraphicsPath())
            {
                int d = radio * 2;
                ruta.AddArc(0, 0, d, d, 180, 90);
                ruta.AddArc(control.Width - d - 1, 0, d, d, 270, 90);
                ruta.AddArc(control.Width - d - 1, control.Height - d - 1, d, d, 0, 90);
                ruta.AddArc(0, control.Height - d - 1, d, d, 90, 90);
                ruta.CloseFigure();
                control.Region = new Region(ruta);
            }
        }

        private IconChar IconoPara(string pantalla)
        {
            switch (pantalla)
            {
                case "Usuarios": return IconChar.UserShield;
                case "Sucursales": return IconChar.Building;
                case "Empleados": return IconChar.UserTie;
                case "Clientes": return IconChar.Users;
                case "Planes": return IconChar.Wifi;
                case "Lineas": return IconChar.SimCard;
                case "Proveedores": return IconChar.Truck;
                case "Marcas": return IconChar.Tags;
                case "Equipos": return IconChar.MobileAlt;
                case "Compras": return IconChar.CartPlus;
                case "Inventario": return IconChar.Boxes;
                case "Ventas": return IconChar.ShoppingCart;
                case "Facturacion": return IconChar.FileInvoiceDollar;
                case "Pagos": return IconChar.CreditCard;
                case "Cardex": return IconChar.ChartLine;
                case "Reclamos": return IconChar.Headset;
                default: return IconChar.FolderOpen;
            }
        }
    }
}
