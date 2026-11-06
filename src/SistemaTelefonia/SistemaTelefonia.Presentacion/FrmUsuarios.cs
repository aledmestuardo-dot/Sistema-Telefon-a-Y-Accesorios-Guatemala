using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    [DesignerCategory("Form")]
    public partial class FrmUsuarios : Form
    {
        private readonly BindingSource origen = new BindingSource();
        private readonly UsuariosNegocio negocio = new UsuariosNegocio();
        private bool cargando;
        private bool modoNuevo;
        private bool modoEdicion;
        private bool actualizandoFiltro;
        private bool esSupabase;
        private ComboBox cboFiltroEstado;
        private CheckBox chkEsDueno;
        private CheckBox chkActivarFiltros;
        private TableLayoutPanel layoutBusqueda;
        private TableLayoutPanel layoutOpcionesColumnas;
        private readonly List<FontAwesome.Sharp.IconButton> botonesBarraLateral = new List<FontAwesome.Sharp.IconButton>();
        private Panel pnlDashboardUsuarios;
        private TableLayoutPanel layoutBloquesUsuarios;
        private Label[] valoresDashboard;
        private Label lblAlertasDashboard;
        private Label lblActividadDashboard;
        private Chart graficaDashboard;
        private Button btnActualizarDashboard;
        private bool cargandoDashboard;
        private Panel pnlPaginacion;
        private Button btnPaginaAnterior;
        private Button btnPaginaSiguiente;
        private Label lblPagina;
        private List<UsuariosEntidad> resultadosActuales = new List<UsuariosEntidad>();
        private int paginaActual = 1;
        private const int RegistrosPorPagina = 10;
        private const int FilasMinimasVisibles = 5;
        private const int FilasMaximasVisibles = 10;

        public FrmUsuarios()
        {
            InitializeComponent();

            // El diseñador de WinForms debe limitarse a InitializeComponent. Las
            // adaptaciones de ejecución se agregan después para no bloquear su carga.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || (Site != null && Site.DesignMode)) return;

            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConfigurarDisenoAdaptable();
            ConstruirNavegacionLateral();
            ConfigurarColumnaPropietario();
            ConfigurarOpcionesColumnas();
            ConfigurarOrigen();
            ConfigurarPaginacion();
            ConfigurarFiltroEstado();
            dgvUsuarios.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvUsuarios);
            TemaVisualCrud.Aplicar(this);
            AplicarEstadoBotonesBarraLateral();
            AjustesDisenoCrud.ResaltarPestanaActiva(pnlItemsMenu, GetType().Name);
            btnVolverMenu.BackColor = System.Drawing.ColorTranslator.FromHtml("#08243A");
            btnVolverMenu.ForeColor = System.Drawing.Color.White;
            btnVolverMenu.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#526779");
            btnVolverMenu.FlatAppearance.BorderSize = 1;
            btnCerrarSesion.BackColor = System.Drawing.ColorTranslator.FromHtml("#A83F48");
            btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            btnCerrarSesion.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#D56A70");
            btnCerrarSesion.FlatAppearance.BorderSize = 1;
            AjustarAnchosOpcionesFiltros();
            CrearDashboardUsuarios();
            ConectarEventos();
            ConfigurarNavegacionTeclado();
            ConfigurarResaltadoCampos();
            ConfigurarColumnasGrilla();
            ActualizarColumnas();
            LimpiarFormulario();
            BuscarUsuarios();
            AjustarInterfazUsuarios();
        }

        private void ConfigurarDisenoAdaptable()
        {
            pnlHeader.Height = 108;
            pnlContenido.Padding = new Padding(14);
            pnlContenido.AutoScroll = true;
            pnlFormulario.Height = 226;
            pnlAcciones.Height = 58;
            lblTitulo.Height = 48;
            lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblSubtitulo.Height = 42;
            pnlFormulario.Dock = DockStyle.Top;
            pnlAcciones.Dock = DockStyle.Top;
            pnlCatalogo.Dock = DockStyle.Top;
            lblFormularioTitulo.Height = 40;
            lblCatalogoTitulo.Height = 40;
            lblColumnas.Height = 40;
            pnlBusqueda.Height = 54;
            pnlColumnas.Height = 68;
            selector.AutoScroll = false;
            chkSeleccionar.Text = "Seleccionar registro";
            dgvUsuarios.Dock = DockStyle.Top;
            dgvUsuarios.RowTemplate.Height = 29;
            dgvUsuarios.ColumnHeadersHeight = 34;
            dgvUsuarios.ScrollBars = ScrollBars.Horizontal;
            pnlContenido.SizeChanged += (s, e) => AjustarInterfazUsuarios();
            selector.SizeChanged += (s, e) => OrganizarOpcionesColumnas();
            pnlCatalogo.SizeChanged += (s, e) => AjustarInterfazUsuarios();
        }

        private void CrearDashboardUsuarios()
        {
            Color fondo = ColorTranslator.FromHtml("#08243A");
            Color superficie = ColorTranslator.FromHtml("#0B3558");
            pnlDashboardUsuarios = new PanelRedondeado
            {
                Name = "pnlDashboardUsuarios", Dock = DockStyle.Fill, Height = 430,
                Margin = Padding.Empty, Padding = new Padding(12),
                BackColor = fondo
            };
            ((PanelRedondeado)pnlDashboardUsuarios).RadioEsquinas = 12;

            TableLayoutPanel raiz = new TableLayoutPanel
            {
                Name = "layoutDashboardUsuarios", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            raiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            TableLayoutPanel encabezado = new TableLayoutPanel
            {
                Name = "layoutEncabezadoDashboard", Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
            Label titulo = new Label
            {
                Name = "lblTituloDashboardUsuarios", Text = "Resumen de usuarios",
                Dock = DockStyle.Fill, Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft
            };
            btnActualizarDashboard = new Button
            {
                Name = "btnActualizarDashboardUsuarios", Text = "↻ Actualizar", Dock = DockStyle.Fill,
                Margin = new Padding(4, 1, 0, 1), FlatStyle = FlatStyle.Flat,
                BackColor = ColorTranslator.FromHtml("#0B6E91"), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnActualizarDashboard.FlatAppearance.BorderSize = 0;
            encabezado.Controls.Add(titulo, 0, 0);
            encabezado.Controls.Add(btnActualizarDashboard, 1, 0);
            raiz.Controls.Add(encabezado, 0, 0);

            TableLayoutPanel tarjetas = new TableLayoutPanel
            {
                Name = "layoutIndicadoresDashboard", Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1,
                Margin = new Padding(0, 5, 0, 5), Padding = Padding.Empty, BackColor = fondo
            };
            for (int i = 0; i < 4; i++) tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            string[] titulos = { "USUARIOS REGISTRADOS", "CUENTAS ACTIVAS", "CUENTAS INACTIVAS", "ADMINISTRADORES" };
            Color[] acentos = {
                ColorTranslator.FromHtml("#3B9EFF"), ColorTranslator.FromHtml("#35C98B"),
                ColorTranslator.FromHtml("#E7B84B"), ColorTranslator.FromHtml("#8B72E8")
            };
            valoresDashboard = new Label[4];
            for (int i = 0; i < titulos.Length; i++)
            {
                Panel tarjeta = new Panel
                {
                    Name = "pnlIndicadorDashboard" + i, Dock = DockStyle.Fill,
                    Margin = new Padding(0, 0, i == titulos.Length - 1 ? 0 : 8, 0),
                    Padding = new Padding(0), BackColor = superficie
                };
                TableLayoutPanel contenidoTarjeta = new TableLayoutPanel
                {
                    Name = "layoutIndicadorDashboard" + i, Dock = DockStyle.Fill,
                    ColumnCount = 1, RowCount = 3, Margin = Padding.Empty,
                    Padding = Padding.Empty, BackColor = superficie
                };
                contenidoTarjeta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                contenidoTarjeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 3));
                contenidoTarjeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                contenidoTarjeta.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                Panel acento = new Panel
                {
                    Name = "pnlAcentoIndicadorDashboard" + i, Dock = DockStyle.Fill,
                    BackColor = acentos[i], Margin = Padding.Empty
                };
                Label nombre = new Label
                {
                    Text = titulos[i], Dock = DockStyle.Fill, Margin = new Padding(10, 0, 8, 0),
                    ForeColor = ColorTranslator.FromHtml("#A9BAC8"),
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold), AutoEllipsis = true,
                    TextAlign = ContentAlignment.BottomLeft
                };
                valoresDashboard[i] = new Label
                {
                    Name = "lblValorIndicadorDashboard" + i, Text = "—", Dock = DockStyle.Fill,
                    Margin = new Padding(10, 0, 8, 4), ForeColor = Color.White,
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft,
                    AutoEllipsis = true
                };
                contenidoTarjeta.Controls.Add(acento, 0, 0);
                contenidoTarjeta.Controls.Add(nombre, 0, 1);
                contenidoTarjeta.Controls.Add(valoresDashboard[i], 0, 2);
                tarjeta.Controls.Add(contenidoTarjeta);
                tarjetas.Controls.Add(tarjeta, i, 0);
            }
            raiz.Controls.Add(tarjetas, 0, 1);

            TableLayoutPanel inferior = new TableLayoutPanel
            {
                Name = "layoutDetalleDashboard", Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                Margin = new Padding(0, 4, 0, 0), Padding = Padding.Empty, BackColor = fondo
            };
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
            Panel panelGrafica = CrearPanelGraficaDashboard("Nuevos usuarios por mes", out graficaDashboard);
            ConfigurarGraficaDashboard();
            inferior.Controls.Add(panelGrafica, 0, 0);

            TableLayoutPanel panelDerecho = new TableLayoutPanel
            {
                Name = "layoutAlertasActividadDashboard", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 2, Margin = new Padding(8, 0, 0, 0),
                Padding = Padding.Empty, BackColor = fondo
            };
            panelDerecho.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            Panel panelAlertas = CrearPanelDashboard("Tipos de cuenta", out lblAlertasDashboard);
            Panel panelActividad = CrearPanelDashboard("Accesos recientes", out lblActividadDashboard);
            panelAlertas.Margin = new Padding(0, 0, 0, 5);
            panelActividad.Margin = new Padding(0, 5, 0, 0);
            panelDerecho.Controls.Add(panelAlertas, 0, 0);
            panelDerecho.Controls.Add(panelActividad, 0, 1);
            inferior.Controls.Add(panelDerecho, 1, 0);
            raiz.Controls.Add(inferior, 0, 2);

            pnlDashboardUsuarios.Controls.Add(raiz);
            // La fila vacía del TableLayoutPanel da 10 px de espacio entre dashboard
            // y formulario sin añadir un panel separador visible.
            layoutBloquesUsuarios = new TableLayoutPanel
            {
                Name = "layoutBloquesUsuarios", Dock = DockStyle.Top,
                ColumnCount = 1, RowCount = 5, Margin = Padding.Empty,
                Padding = Padding.Empty, BackColor = pnlContenido.BackColor,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesUsuarios.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutBloquesUsuarios.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlDashboardUsuarios.Height));
            layoutBloquesUsuarios.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            layoutBloquesUsuarios.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlFormulario.Height));
            layoutBloquesUsuarios.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlAcciones.Height));
            layoutBloquesUsuarios.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlCatalogo.Height));

            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesUsuarios);
            pnlFormulario.Dock = DockStyle.Fill;
            pnlFormulario.Margin = Padding.Empty;
            pnlAcciones.Dock = DockStyle.Fill;
            pnlAcciones.Margin = Padding.Empty;
            pnlCatalogo.Dock = DockStyle.Fill;
            pnlCatalogo.Margin = Padding.Empty;
            layoutBloquesUsuarios.Controls.Add(pnlDashboardUsuarios, 0, 0);
            layoutBloquesUsuarios.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesUsuarios.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesUsuarios.Controls.Add(pnlCatalogo, 0, 4);
            layoutBloquesUsuarios.Height = pnlDashboardUsuarios.Height + 10 + pnlFormulario.Height +
                pnlAcciones.Height + pnlCatalogo.Height;

            btnActualizarDashboard.Click += async (s, e) => await CargarDashboardUsuariosAsync();
            Shown += async (s, e) => await CargarDashboardUsuariosAsync();
        }

        private Panel CrearPanelDashboard(string titulo, out Label contenido)
        {
            Color superficie = ColorTranslator.FromHtml("#0B3558");
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(9, 6, 9, 6),
                BackColor = superficie, Margin = Padding.Empty
            };
            Label encabezado = new Label
            {
                Text = titulo, Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true
            };
            contenido = new Label
            {
                Dock = DockStyle.Fill, ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F), AutoEllipsis = true,
                TextAlign = ContentAlignment.TopLeft
            };
            panel.Controls.Add(contenido);
            panel.Controls.Add(encabezado);
            return panel;
        }

        private Panel CrearPanelGraficaDashboard(string titulo, out Chart contenido)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(7, 5, 7, 5),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty
            };
            Label encabezado = new Label
            {
                Text = titulo, Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true
            };
            contenido = new Chart { Dock = DockStyle.Fill };
            panel.Controls.Add(contenido);
            panel.Controls.Add(encabezado);
            return panel;
        }

        private void ConfigurarGraficaDashboard()
        {
            graficaDashboard.Dock = DockStyle.Fill;
            graficaDashboard.BackColor = ColorTranslator.FromHtml("#0B3558");
            graficaDashboard.Palette = ChartColorPalette.None;
            ChartArea area = new ChartArea("ResumenMensual")
            {
                BackColor = ColorTranslator.FromHtml("#0B3558")
            };
            area.AxisX.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisX.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisY.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#34495E");
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            graficaDashboard.ChartAreas.Add(area);
            graficaDashboard.Series.Add(new Series("Usuarios")
            {
                ChartArea = "ResumenMensual", ChartType = SeriesChartType.Column,
                BorderWidth = 0, Color = ColorTranslator.FromHtml("#00D9FF"),
                IsXValueIndexed = true
            });
        }

        private sealed class ResumenDashboardUsuarios
        {
            public int UsuariosRegistrados;
            public int UsuariosActivos;
            public int UsuariosInactivos;
            public int Administradores;
            public List<string> Meses = new List<string>();
            public List<int> RegistrosPorMes = new List<int>();
            public string TiposUsuario;
            public string AccesosRecientes;
        }

        private sealed class ActividadDashboardUsuarios
        {
            public DateTime Fecha;
            public string Texto;
        }

        private async Task CargarDashboardUsuariosAsync()
        {
            if (cargandoDashboard || IsDisposed || pnlDashboardUsuarios == null) return;
            cargandoDashboard = true;
            btnActualizarDashboard.Enabled = false;
            btnActualizarDashboard.Text = "Cargando…";
            try
            {
                ResumenDashboardUsuarios datos = await Task.Run(() => ObtenerDatosDashboardUsuarios());
                if (IsDisposed) return;
                valoresDashboard[0].Text = datos.UsuariosRegistrados.ToString("N0");
                valoresDashboard[1].Text = datos.UsuariosActivos.ToString("N0");
                valoresDashboard[2].Text = datos.UsuariosInactivos.ToString("N0");
                valoresDashboard[3].Text = datos.Administradores.ToString("N0");
                lblAlertasDashboard.Text = datos.TiposUsuario;
                lblActividadDashboard.Text = datos.AccesosRecientes;
                Series usuariosRegistrados = graficaDashboard.Series["Usuarios"];
                usuariosRegistrados.Points.Clear();
                for (int i = 0; i < datos.Meses.Count; i++)
                {
                    usuariosRegistrados.Points.AddXY(datos.Meses[i], datos.RegistrosPorMes[i]);
                }
            }
            catch (Exception)
            {
                if (!IsDisposed)
                {
                    foreach (Label valor in valoresDashboard) valor.Text = "—";
                    lblAlertasDashboard.Text = "No se pudo cargar el resumen. Comprueba la conexión de datos.";
                    lblActividadDashboard.Text = "Los accesos recientes no están disponibles.";
                }
            }
            finally
            {
                cargandoDashboard = false;
                if (!IsDisposed)
                {
                    btnActualizarDashboard.Enabled = true;
                    btnActualizarDashboard.Text = "↻ Actualizar";
                }
            }
        }

        private ResumenDashboardUsuarios ObtenerDatosDashboardUsuarios()
        {
            List<UsuariosEntidad> usuarios = new UsuariosNegocio().Listar();
            List<UsuariosEntidad> activos = usuarios.Where(u => u.Estado).ToList();
            List<UsuariosEntidad> inactivos = usuarios.Where(u => !u.Estado).ToList();
            ResumenDashboardUsuarios resumen = new ResumenDashboardUsuarios
            {
                UsuariosRegistrados = usuarios.Count,
                UsuariosActivos = activos.Count,
                UsuariosInactivos = inactivos.Count,
                Administradores = usuarios.Count(u => string.Equals(u.TipoUsuario, "Administrador",
                    StringComparison.CurrentCultureIgnoreCase))
            };

            List<string> tiposUsuario = usuarios.GroupBy(u => string.IsNullOrWhiteSpace(u.TipoUsuario)
                    ? "Sin tipo asignado" : u.TipoUsuario.Trim(), StringComparer.CurrentCultureIgnoreCase)
                .OrderByDescending(g => g.Count()).ThenBy(g => g.Key)
                .Take(4)
                .Select(g => g.Key + "  ·  " + g.Count())
                .ToList();
            resumen.TiposUsuario = tiposUsuario.Count == 0 ? "No hay tipos de cuenta registrados."
                : string.Join(Environment.NewLine, tiposUsuario);
            List<ActividadDashboardUsuarios> accesos = usuarios
                .Where(u => u.FechaUltimoAcceso.HasValue)
                .OrderByDescending(u => u.FechaUltimoAcceso.Value)
                .Take(4)
                .Select(u => new ActividadDashboardUsuarios
                {
                    Fecha = u.FechaUltimoAcceso.Value,
                    Texto = u.Nombre + "  ·  " + u.FechaUltimoAcceso.Value.ToString("dd/MM HH:mm")
                }).ToList();
            resumen.AccesosRecientes = accesos.Count == 0 ? "Aún no hay accesos registrados."
                : string.Join(Environment.NewLine, accesos.Select(a => a.Texto));

            DateTime mesReferencia = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-5);
            for (int i = 0; i < 6; i++)
            {
                DateTime mes = mesReferencia.AddMonths(i);
                resumen.Meses.Add(mes.ToString("MMM", CultureInfo.GetCultureInfo("es-GT")));
                resumen.RegistrosPorMes.Add(usuarios.Count(u => u.FechaCreacion.Year == mes.Year
                    && u.FechaCreacion.Month == mes.Month));
            }
            return resumen;
        }

        private void ConfigurarColumnasGrilla()
        {
            Action actualizar = () =>
            {
                EstablecerEncabezado("CodigoUsuario", "Código");
                EstablecerEncabezado("TipoUsuario", "Tipo de usuario");
                EstablecerEncabezado("Nombre", "Nombre");
                EstablecerEncabezado("CorreoElectronico", "Correo electrónico");
                EstablecerEncabezado("FechaCreacion", "Fecha de creación");
                EstablecerEncabezado("FechaUltimoAcceso", "Último acceso");
                EstablecerEncabezado("Estado", "Estado");
                EstablecerEncabezado("EsDueno", "Propietario");
                if (dgvUsuarios.Columns.Contains("FechaCreacion"))
                    dgvUsuarios.Columns["FechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                if (dgvUsuarios.Columns.Contains("FechaUltimoAcceso"))
                    dgvUsuarios.Columns["FechaUltimoAcceso"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                if (dgvUsuarios.Columns.Contains("Estado"))
                    dgvUsuarios.Columns["Estado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            };
            dgvUsuarios.DataBindingComplete += (s, e) => actualizar();
            dgvUsuarios.ColumnAdded += (s, e) => actualizar();
            actualizar();
        }

        private void EstablecerEncabezado(string nombre, string texto)
        {
            if (dgvUsuarios.Columns.Contains(nombre)) dgvUsuarios.Columns[nombre].HeaderText = texto;
        }

        private void OrganizarOpcionesColumnas()
        {
            if (selector == null || selector.ClientSize.Width <= 0) return;
            selector.SuspendLayout();
            selector.FlowDirection = FlowDirection.LeftToRight;
            // Mantener todos los filtros en una sola línea y comenzar justo después
            // de "Activar todos los filtros". El desplazamiento horizontal solo se
            // usa en resoluciones donde la fila no quepa completa.
            selector.WrapContents = false;
            selector.AutoScroll = true;
            selector.Padding = new Padding(0, 2, 0, 2);
            foreach (CheckBox casilla in selector.Controls.OfType<CheckBox>().Where(c => c.Visible))
            {
                casilla.AutoSize = true;
                casilla.Font = new System.Drawing.Font("Segoe UI", 9F);
                casilla.Margin = new Padding(2, 3, 5, 3);
            }
            selector.Height = 36;
            pnlColumnas.Height = lblColumnas.Height + selector.Height;
            selector.ResumeLayout(true);
        }

        private void AjustarInterfazUsuarios()
        {
            if (IsDisposed || pnlContenido == null || pnlCatalogo == null) return;
            if (selector != null && selector.ClientSize.Width > 0) OrganizarOpcionesColumnas();

            // Cada página mantiene el mismo espacio para diez registros, incluso
            // cuando la última página contiene menos filas.
            int filasReservadas = FilasMaximasVisibles;
            if (dgvUsuarios != null)
            {
                int altoFila = Math.Max(26, dgvUsuarios.RowTemplate.Height);
                int altoGrilla = dgvUsuarios.ColumnHeadersHeight + filasReservadas * altoFila + 2;
                if (dgvUsuarios.Height != altoGrilla) dgvUsuarios.Height = altoGrilla;
            }

            if (pnlPaginacion != null && pnlCatalogo.ClientSize.Width > 0)
            {
                int anchoBoton = Math.Max(72, btnNuevo.Width);
                btnPaginaAnterior.Width = anchoBoton;
                btnPaginaSiguiente.Width = anchoBoton;
                pnlPaginacion.Height = btnNuevo.Height + 16;
                AjustarDistribucionPaginacion();
            }

            AjustarDistribucionBusqueda();

            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height +
                pnlBusqueda.Height + pnlColumnas.Height + dgvUsuarios.Height +
                (pnlPaginacion == null ? 0 : pnlPaginacion.Height);
            if (layoutBloquesUsuarios == null)
            {
                if (pnlCatalogo.Height != altoCatalogo) pnlCatalogo.Height = altoCatalogo;
            }
            else
            {
                layoutBloquesUsuarios.RowStyles[4].Height = altoCatalogo;
            }

            // Usar el espacio sobrante para ampliar el dashboard. En pantallas bajas,
            // mantener un mínimo legible y permitir desplazamiento vertical.
            if (pnlDashboardUsuarios != null)
            {
                int altoBloquesFijos = pnlContenido.Padding.Vertical +
                    pnlFormulario.Height + pnlAcciones.Height + altoCatalogo + 10;
                int espacioDashboard = pnlContenido.ClientSize.Height - altoBloquesFijos;
                int altoDashboard = Math.Max(360, espacioDashboard);
                if (pnlDashboardUsuarios.Height != altoDashboard)
                    pnlDashboardUsuarios.Height = altoDashboard;
                layoutBloquesUsuarios.RowStyles[0].Height = altoDashboard;
                layoutBloquesUsuarios.RowStyles[2].Height = pnlFormulario.Height;
                layoutBloquesUsuarios.RowStyles[3].Height = pnlAcciones.Height;
                layoutBloquesUsuarios.RowStyles[4].Height = altoCatalogo;
                int altoLayout = altoDashboard + 10 + pnlFormulario.Height +
                    pnlAcciones.Height + altoCatalogo;
                if (layoutBloquesUsuarios.Height != altoLayout)
                    layoutBloquesUsuarios.Height = altoLayout;
            }
        }

        private void ConstruirNavegacionLateral()
        {
            Dictionary<string, Label> etiquetasMenu = pnlItemsMenu.Controls.OfType<Label>()
                .Where(l => l.Tag != null)
                .ToDictionary(l => Convert.ToString(l.Tag), l => l, StringComparer.OrdinalIgnoreCase);
            string[] destinos = { "FrmUsuarios", "FrmSucursales", "FrmEmpleados", "FrmClientes",
                "FrmPlanes", "FrmLineas", "FrmProveedores", "FrmMarcas", "FrmEquipos", "FrmCompras",
                "FrmInventario", "FrmVentas", "FrmFacturacion", "FrmPagos", "FrmCardex", "FrmReclamos" };
            pnlItemsMenu.Controls.Clear();
            botonesBarraLateral.Clear();

            lblLogo.Text = string.Empty;
            lblSistema.Text = "Sistema de Telefonía\ny Accesorios";
            lblSistema.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            pnlLogo.Height = 142;
            pnlLogo.BackColor = System.Drawing.Color.FromArgb(3, 43, 83);
            pnlLogo.Controls.Remove(lblLogo);
            FontAwesome.Sharp.IconPictureBox iconoMarca = pnlLogo.Controls
                .OfType<FontAwesome.Sharp.IconPictureBox>().FirstOrDefault(i => i.Name == "icoMarcaTelefonia");
            if (iconoMarca == null)
            {
                iconoMarca = new FontAwesome.Sharp.IconPictureBox
                {
                    Name = "icoMarcaTelefonia",
                    IconChar = FontAwesome.Sharp.IconChar.MobileAlt,
                    IconFont = FontAwesome.Sharp.IconFont.Auto,
                    IconColor = System.Drawing.Color.FromArgb(0, 203, 255),
                    BackColor = System.Drawing.Color.Transparent,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new System.Drawing.Size(42, 42),
                    Location = new System.Drawing.Point((pnlLogo.ClientSize.Width - 42) / 2, 8),
                    Anchor = AnchorStyles.Top
                };
                pnlLogo.Controls.Add(iconoMarca);
            }
            iconoMarca.BringToFront();
            lblSistema.Dock = DockStyle.None;
            lblSistema.SetBounds(6, 52, pnlLogo.ClientSize.Width - 12, 82);
            lblSistema.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlLogo.SizeChanged += (s, e) =>
            {
                iconoMarca.Left = (pnlLogo.ClientSize.Width - iconoMarca.Width) / 2;
                lblSistema.SetBounds(6, 52, pnlLogo.ClientSize.Width - 12, 82);
            };

            UsuariosEntidad usuarioActual = UsuariosNegocio.UsuarioActual;
            lblUsuarioRol.Text = usuarioActual == null || string.IsNullOrWhiteSpace(usuarioActual.TipoUsuario)
                ? "Administrador" : usuarioActual.TipoUsuario;
            lblUsuario.Text = usuarioActual == null ? lblUsuario.Text : usuarioActual.Nombre;
            lblUsuario.Text = "Usuario del sistema";
            lblUsuario.Name = "lblDescripcionRol";
            lblUsuario.Visible = true;
            lblUsuario.Dock = DockStyle.Bottom;
            lblUsuario.Height = 24;
            lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblUsuario.ForeColor = System.Drawing.ColorTranslator.FromHtml("#A9BAC8");
            lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblUsuarioRol.Name = "lblRol";
            lblUsuarioRol.Dock = DockStyle.Bottom;
            lblUsuarioRol.Height = 24;
            lblUsuarioRol.ForeColor = System.Drawing.Color.White;
            lblUsuarioRol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblUsuarioRol.AutoEllipsis = true;
            CampoRedondeado.Aplicar(pnlLogo, 12);

            pnlMenu.SuspendLayout();
            pnlMenu.Controls.Remove(pnlItemsMenu);
            pnlMenu.Controls.Remove(btnVolverMenu);
            pnlMenu.Controls.Remove(btnCerrarSesion);
            pnlMenu.Controls.Remove(lblUsuario);
            pnlMenu.Controls.Remove(lblUsuarioRol);
            pnlMenu.Controls.Remove(pnlLogo);
            pnlItemsMenu.Controls.Clear();
            TableLayoutPanel layoutLateral = new TableLayoutPanel
            {
                Name = "layoutBarraLateralUsuarios", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#061D30"),
                Margin = Padding.Empty, Padding = Padding.Empty
            };
            layoutLateral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlLogo.Height));
            layoutLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            layoutLateral.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            pnlLogo.Dock = DockStyle.Fill;
            btnVolverMenu.Text = "Volver al menú principal";
            btnVolverMenu.Dock = DockStyle.Fill;
            btnVolverMenu.Margin = Padding.Empty;
            btnVolverMenu.IconChar = FontAwesome.Sharp.IconChar.Home;
            btnVolverMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnVolverMenu.IconSize = 20;
            btnVolverMenu.IconColor = System.Drawing.Color.White;
            btnVolverMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnVolverMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVolverMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnVolverMenu.Padding = new Padding(12, 0, 4, 0);
            btnVolverMenu.BackColor = System.Drawing.ColorTranslator.FromHtml("#08243A");
            btnVolverMenu.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#526779");
            btnVolverMenu.FlatAppearance.BorderSize = 1;
            pnlItemsMenu.Dock = DockStyle.Fill;
            pnlItemsMenu.AutoScroll = false;
            pnlItemsMenu.BackColor = System.Drawing.ColorTranslator.FromHtml("#061D30");
            btnCerrarSesion.Dock = DockStyle.Fill;
            btnCerrarSesion.Margin = Padding.Empty;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            btnCerrarSesion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCerrarSesion.IconSize = 20;
            btnCerrarSesion.IconColor = System.Drawing.Color.White;
            btnCerrarSesion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnCerrarSesion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCerrarSesion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnCerrarSesion.Padding = new Padding(12, 0, 4, 0);
            btnCerrarSesion.BackColor = System.Drawing.ColorTranslator.FromHtml("#A83F48");
            btnCerrarSesion.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#D56A70");
            btnCerrarSesion.FlatAppearance.BorderSize = 1;
            TableLayoutPanel panelRol = new TableLayoutPanel
            {
                Name = "layoutRolBarraLateral", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#061D30"), Margin = Padding.Empty,
                Padding = new Padding(2, 0, 2, 0)
            };
            panelRol.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panelRol.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            panelRol.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            lblUsuarioRol.Dock = DockStyle.Fill;
            lblUsuario.Dock = DockStyle.Fill;
            panelRol.Controls.Add(lblUsuarioRol, 0, 0);
            panelRol.Controls.Add(lblUsuario, 0, 1);
            TableLayoutPanel layoutBotonesMenu = new TableLayoutPanel
            {
                Name = "layoutBotonesBarraLateral", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 18,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#061D30"),
                Margin = Padding.Empty, Padding = Padding.Empty, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBotonesMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 18; i++)
                layoutBotonesMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 18F));
            layoutBotonesMenu.Controls.Add(btnVolverMenu, 0, 0);
            string[] nombresBotones = { "btnUsuarios", "btnSucursales", "btnEmpleados", "btnClientes",
                "btnPlanes", "btnLineas", "btnProveedores", "btnMarcas", "btnEquipos", "btnCompras",
                "btnInventario", "btnVentas", "btnFacturacion", "btnPagos", "btnCardex", "btnReclamos" };
            for (int i = 0; i < destinos.Length; i++)
            {
                Label etiqueta = etiquetasMenu[destinos[i]];
                string texto = (etiqueta.Text ?? string.Empty).Trim().TrimStart('›', '⌂').Trim();
                var boton = new FontAwesome.Sharp.IconButton
                {
                    Name = nombresBotones[i], Text = texto, Tag = destinos[i], Dock = DockStyle.Fill,
                    Margin = Padding.Empty, Padding = new Padding(12, 0, 4, 0),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    ImageAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    IconChar = IconoParaMenu(destinos[i].Substring(3)),
                    IconFont = FontAwesome.Sharp.IconFont.Auto, IconColor = System.Drawing.ColorTranslator.FromHtml("#00D9FF"),
                    IconSize = 20, BackColor = System.Drawing.ColorTranslator.FromHtml("#061D30"),
                    ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat,
                    Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular),
                    Cursor = Cursors.Hand, UseVisualStyleBackColor = false
                };
                boton.FlatAppearance.BorderSize = 0;
                boton.Click += MenuOpcion_Click;
                boton.MouseEnter += MenuOpcion_MouseEnter;
                boton.MouseLeave += MenuOpcion_MouseLeave;
                botonesBarraLateral.Add(boton);
                layoutBotonesMenu.Controls.Add(boton, 0, i + 1);
            }
            layoutBotonesMenu.Controls.Add(btnCerrarSesion, 0, 17);
            pnlItemsMenu.Controls.Add(layoutBotonesMenu);
            layoutLateral.Controls.Add(pnlLogo, 0, 0);
            layoutLateral.Controls.Add(panelRol, 0, 1);
            layoutLateral.Controls.Add(pnlItemsMenu, 0, 2);
            pnlMenu.Controls.Add(layoutLateral);
            pnlMenu.ResumeLayout(true);

            AplicarEstadoBotonesBarraLateral();

            btnVolverMenu.Click += (s, e) => Navegacion.VolverAlMenu(this);
            btnCerrarSesion.Click += (s, e) => Navegacion.CerrarSesion(this);
            btnVolverMenu.MouseEnter += (s, e) =>
            {
                if (btnVolverMenu.Enabled) btnVolverMenu.BackColor = System.Drawing.Color.FromArgb(0, 86, 132);
            };
            btnVolverMenu.MouseLeave += (s, e) =>
            {
                btnVolverMenu.BackColor = System.Drawing.ColorTranslator.FromHtml("#08243A");
            };
            btnCerrarSesion.MouseEnter += (s, e) =>
            {
                if (btnCerrarSesion.Enabled) btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(185, 67, 74);
            };
            btnCerrarSesion.MouseLeave += (s, e) => btnCerrarSesion.BackColor = System.Drawing.ColorTranslator.FromHtml("#A83F48");
            pnlMenu.Paint += (s, e) =>
            {
                using (var borde = new System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml("#34495E"), 1))
                    e.Graphics.DrawRectangle(borde, 0, 0, pnlMenu.Width - 1, pnlMenu.Height - 1);
            };
        }

        private void AgregarIconoMenu(Label opcion)
        {
            string formulario = Convert.ToString(opcion.Tag);
            string modulo = formulario.StartsWith("Frm", StringComparison.Ordinal) ? formulario.Substring(3) : formulario;
            if (opcion.Controls.OfType<FontAwesome.Sharp.IconPictureBox>().Any()) return;
            var icono = new FontAwesome.Sharp.IconPictureBox
            {
                Name = "icoMenu" + modulo,
                IconChar = IconoParaMenu(modulo),
                IconFont = FontAwesome.Sharp.IconFont.Auto,
                IconColor = System.Drawing.Color.FromArgb(0, 203, 255),
                // Compartir la referencia que usa AjustesDisenoCrud para detectar
                // iconos existentes y evitar que vuelva a insertar otro igual.
                Tag = opcion,
                BackColor = System.Drawing.Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new System.Drawing.Size(24, 24),
                Location = new System.Drawing.Point(10, 16),
                Enabled = true,
                Cursor = Cursors.Hand
            };
            icono.Click += MenuOpcion_Click;
            icono.MouseEnter += MenuOpcion_MouseEnter;
            icono.MouseLeave += MenuOpcion_MouseLeave;
            opcion.Controls.Add(icono);
            opcion.Text = opcion.Text.TrimStart('›', ' ', '⌂');
        }

        private static FontAwesome.Sharp.IconChar IconoParaMenu(string modulo)
        {
            switch (modulo)
            {
                case "Usuarios": return FontAwesome.Sharp.IconChar.UserShield;
                case "Sucursales": return FontAwesome.Sharp.IconChar.Building;
                case "Empleados": return FontAwesome.Sharp.IconChar.UserTie;
                case "Clientes": return FontAwesome.Sharp.IconChar.Users;
                case "Planes": return FontAwesome.Sharp.IconChar.Wifi;
                case "Lineas": return FontAwesome.Sharp.IconChar.SimCard;
                case "Proveedores": return FontAwesome.Sharp.IconChar.Truck;
                case "Marcas": return FontAwesome.Sharp.IconChar.Tags;
                case "Equipos": return FontAwesome.Sharp.IconChar.MobileAlt;
                case "Compras": return FontAwesome.Sharp.IconChar.CartPlus;
                case "Inventario": return FontAwesome.Sharp.IconChar.Boxes;
                case "Ventas": return FontAwesome.Sharp.IconChar.ShoppingCart;
                case "Facturacion": return FontAwesome.Sharp.IconChar.FileInvoiceDollar;
                case "Pagos": return FontAwesome.Sharp.IconChar.CreditCard;
                case "Cardex": return FontAwesome.Sharp.IconChar.ChartLine;
                case "Reclamos": return FontAwesome.Sharp.IconChar.Headset;
                default: return FontAwesome.Sharp.IconChar.FolderOpen;
            }
        }

        private void MenuOpcion_MouseEnter(object sender, EventArgs e)
        {
            FontAwesome.Sharp.IconButton opcion = sender as FontAwesome.Sharp.IconButton;
            if (opcion == null) return;
            opcion.BackColor = System.Drawing.ColorTranslator.FromHtml("#0B3558");
            opcion.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00D9FF");
            opcion.IconColor = System.Drawing.ColorTranslator.FromHtml("#00D9FF");
        }

        private void MenuOpcion_MouseLeave(object sender, EventArgs e)
        {
            AplicarEstadoBotonesBarraLateral();
        }

        private void AplicarEstadoBotonesBarraLateral()
        {
            System.Drawing.Color normal = System.Drawing.ColorTranslator.FromHtml("#061D30");
            System.Drawing.Color activo = System.Drawing.ColorTranslator.FromHtml("#0B3558");
            System.Drawing.Color cian = System.Drawing.ColorTranslator.FromHtml("#00D9FF");
            foreach (FontAwesome.Sharp.IconButton boton in botonesBarraLateral)
            {
                bool seleccionado = string.Equals(Convert.ToString(boton.Tag), GetType().Name,
                    StringComparison.OrdinalIgnoreCase);
                boton.BackColor = seleccionado ? activo : normal;
                boton.ForeColor = seleccionado ? cian : System.Drawing.Color.White;
                boton.IconColor = cian;
                boton.FlatStyle = FlatStyle.Flat;
                boton.UseVisualStyleBackColor = false;
                boton.FlatAppearance.BorderSize = 0;
                boton.FlatAppearance.MouseOverBackColor = activo;
                boton.FlatAppearance.MouseDownBackColor = seleccionado
                    ? activo : System.Drawing.ColorTranslator.FromHtml("#08243A");
            }
        }

        private void ConfigurarFiltroEstado()
        {
            cboFiltroEstado = new ComboBox
            {
                Name = "cboFiltroEstado",
                AccessibleName = "Filtrar usuarios por estado",
                Dock = DockStyle.None,
                Width = 160,
                DropDownStyle = ComboBoxStyle.DropDownList,
                IntegralHeight = false,
                Font = new System.Drawing.Font("Segoe UI", 9.5F)
            };
            cboFiltroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            cboFiltroEstado.SelectedIndex = 0;

            // Filtro, búsqueda y acción comparten una sola fila con medidas adaptables.
            pnlBusqueda.Padding = new Padding(8, 4, 8, 4);
            pnlBusqueda.Controls.Add(cboFiltroEstado);
            txtBuscar.Dock = DockStyle.None;
            btnBuscar.Dock = DockStyle.None;
            cboFiltroEstado.Dock = DockStyle.None;
            pnlBusqueda.Height = btnNuevo.Height + 16;
            pnlBusqueda.SizeChanged += (s, e) => AjustarDistribucionBusqueda();
            AjustarDistribucionBusqueda();
        }

        private void AjustarDistribucionBusqueda()
        {
            if (pnlBusqueda == null || txtBuscar == null || btnBuscar == null || cboFiltroEstado == null ||
                pnlBusqueda.ClientSize.Width <= 0 || pnlBusqueda.ClientSize.Height <= 0) return;

            if (layoutBusqueda == null)
            {
                pnlBusqueda.Controls.Remove(cboFiltroEstado);
                pnlBusqueda.Controls.Remove(txtBuscar);
                pnlBusqueda.Controls.Remove(btnBuscar);
                layoutBusqueda = new TableLayoutPanel { Name = "layoutBusquedaUsuarios", Dock = DockStyle.Fill,
                    ColumnCount = 3, RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlBusqueda.BackColor };
                layoutBusqueda.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                layoutBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
                layoutBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                layoutBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, btnNuevo.Width));
                pnlBusqueda.Controls.Add(layoutBusqueda);
                cboFiltroEstado.Dock = DockStyle.Fill;
                txtBuscar.Dock = DockStyle.Fill;
                btnBuscar.Dock = DockStyle.Fill;
                cboFiltroEstado.Margin = new Padding(0, 0, 8, 0);
                txtBuscar.Margin = new Padding(0, 0, 8, 0);
                btnBuscar.Margin = Padding.Empty;
                layoutBusqueda.Controls.Add(cboFiltroEstado, 0, 0);
                layoutBusqueda.Controls.Add(txtBuscar, 1, 0);
                layoutBusqueda.Controls.Add(btnBuscar, 2, 0);
            }
        }

        private void ConfigurarOrigen()
        {
            esSupabase = new ConfiguracionConexionNegocio().ObtenerOrigenElegido() == OrigenBaseDatos.Supabase;
            chkContrasenia.Visible = false;
            chkEsDueno.Visible = esSupabase;
            if (!esSupabase) return;

            cmbTipoUsuario.Items.Clear();
            cmbTipoUsuario.Items.Add("Administrador");
            cmbTipoUsuario.SelectedIndex = 0;
            cmbTipoUsuario.Enabled = false;
            lblContrasenia.Text = "Contraseña inicial/nueva:";
            chkContrasenia.Visible = false;
            dtpFechaCreacion.Enabled = false;
            dtpFechaUltimoAcceso.Enabled = false;
        }

        private void ConfigurarPaginacion()
        {
            pnlPaginacion = new Panel
            {
                Name = "pnlPaginacionUsuarios",
                Dock = DockStyle.Bottom,
                Height = btnNuevo.Height + 16,
                BackColor = System.Drawing.Color.FromArgb(7, 35, 66)
            };
            btnPaginaAnterior = new Button
            {
                Name = "btnPaginaAnteriorUsuarios",
                Text = "‹ Anterior",
                Dock = DockStyle.Right,
                Width = btnNuevo.Width,
                Enabled = false
            };
            btnPaginaSiguiente = new Button
            {
                Name = "btnPaginaSiguienteUsuarios",
                Text = "Siguiente ›",
                Dock = DockStyle.Right,
                Width = btnNuevo.Width,
                Enabled = false
            };
            lblPagina = new Label
            {
                Name = "lblPaginaUsuarios",
                Text = "Página 0 de 0",
                Dock = DockStyle.Right,
                Width = 132,
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            pnlCatalogo.Controls.Remove(lblRegistros);
            lblRegistros.Dock = DockStyle.None;
            lblRegistros.Width = 360;
            lblRegistros.AutoEllipsis = true;
            lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblPagina.Dock = DockStyle.None;
            btnPaginaAnterior.Dock = DockStyle.None;
            btnPaginaSiguiente.Dock = DockStyle.None;
            pnlPaginacion.Controls.Add(lblRegistros);
            pnlPaginacion.Controls.Add(lblPagina);
            pnlPaginacion.Controls.Add(btnPaginaAnterior);
            pnlPaginacion.Controls.Add(btnPaginaSiguiente);
            pnlCatalogo.Controls.Add(pnlPaginacion);
            pnlPaginacion.BringToFront();
            btnPaginaAnterior.Click += (s, e) => CambiarPagina(paginaActual - 1);
            btnPaginaSiguiente.Click += (s, e) => CambiarPagina(paginaActual + 1);
            pnlPaginacion.SizeChanged += (s, e) => AjustarDistribucionPaginacion();
            AjustarDistribucionPaginacion();
        }

        private void AjustarDistribucionPaginacion()
        {
            if (pnlPaginacion == null || btnPaginaAnterior == null || btnPaginaSiguiente == null ||
                lblPagina == null || pnlPaginacion.ClientSize.Width <= 0) return;

            int alto = Math.Max(30, Math.Min(btnNuevo.Height, pnlPaginacion.ClientSize.Height - 8));
            int y = Math.Max(0, (pnlPaginacion.ClientSize.Height - alto) / 2);
            int separacion = 8;
            int anchoBoton = Math.Max(72, btnNuevo.Width);
            int derecha = pnlPaginacion.ClientSize.Width - 8;

            // Siguiente queda al extremo derecho; Anterior se ubica antes del indicador.
            btnPaginaSiguiente.SetBounds(derecha - anchoBoton, y, anchoBoton, alto);
            lblPagina.SetBounds(btnPaginaSiguiente.Left - separacion - lblPagina.Width, y,
                lblPagina.Width, alto);
            btnPaginaAnterior.SetBounds(lblPagina.Left - separacion - anchoBoton, y, anchoBoton, alto);
            lblRegistros.SetBounds(8, y,
                Math.Max(120, btnPaginaAnterior.Left - separacion - 8), alto);
        }

        private void ConfigurarColumnaPropietario()
        {
            chkEsDueno = new CheckBox
            {
                Name = "chkEsDueno",
                Text = "Cuenta dueña",
                Checked = true,
                AutoSize = true,
                ForeColor = System.Drawing.Color.White,
                BackColor = System.Drawing.Color.Transparent,
                Margin = new Padding(5, 3, 9, 3)
            };
            selector.Controls.Add(chkEsDueno);
            chkEsDueno.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void ConfigurarOpcionesColumnas()
        {
            chkSeleccionar.Text = "Seleccionar registro";
            if (chkSeleccionar.Parent != null) chkSeleccionar.Parent.Controls.Remove(chkSeleccionar);
            chkSeleccionar.AutoSize = true;
            chkSeleccionar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkActivarFiltros = new CheckBox
            {
                Name = "chkActivarTodosFiltros",
                Text = "Activar todos los filtros",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                ForeColor = System.Drawing.Color.White,
                BackColor = System.Drawing.Color.Transparent
            };
            pnlColumnas.Controls.Remove(selector);
            pnlColumnas.Controls.Remove(lblColumnas);
            layoutOpcionesColumnas = new TableLayoutPanel { Name = "layoutOpcionesColumnasUsuarios", Dock = DockStyle.Fill,
                ColumnCount = 3, RowCount = 2, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlColumnas.BackColor };
            int anchoActivar = chkActivarFiltros.GetPreferredSize(System.Drawing.Size.Empty).Width + 36;
            int anchoSeleccionar = chkSeleccionar.GetPreferredSize(System.Drawing.Size.Empty).Width + 36;
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoActivar));
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoSeleccionar));
            layoutOpcionesColumnas.RowStyles.Add(new RowStyle(SizeType.Absolute, lblColumnas.Height));
            layoutOpcionesColumnas.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            lblColumnas.Dock = DockStyle.Fill;
            layoutOpcionesColumnas.Controls.Add(lblColumnas, 0, 0);
            layoutOpcionesColumnas.SetColumnSpan(lblColumnas, 3);
            chkActivarFiltros.Margin = new Padding(4, 4, 10, 0);
            chkActivarFiltros.Anchor = AnchorStyles.Left;
            chkSeleccionar.Anchor = AnchorStyles.Right;
            chkSeleccionar.Margin = new Padding(8, 4, 4, 0);
            selector.Dock = DockStyle.Fill;
            selector.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            selector.WrapContents = false;
            selector.AutoScroll = true;
            layoutOpcionesColumnas.Controls.Add(chkActivarFiltros, 0, 1);
            layoutOpcionesColumnas.Controls.Add(selector, 1, 1);
            layoutOpcionesColumnas.Controls.Add(chkSeleccionar, 2, 1);
            pnlColumnas.Controls.Add(layoutOpcionesColumnas);
            chkActivarFiltros.CheckedChanged += (s, e) =>
            {
                if (actualizandoFiltro) return;
                actualizandoFiltro = true;
                try
                {
                    foreach (CheckBox casilla in ObtenerFiltrosColumnas()) casilla.Checked = chkActivarFiltros.Checked;
                }
                finally { actualizandoFiltro = false; }
                SincronizarActivarFiltros();
                ActualizarColumnas();
            };
            foreach (CheckBox casilla in ObtenerFiltrosColumnas())
                casilla.CheckedChanged += (s, e) => { SincronizarActivarFiltros(); ActualizarColumnas(); };
            AjustarPosicionOpcionesColumnas();
            SincronizarActivarFiltros();
        }

        private IEnumerable<CheckBox> ObtenerFiltrosColumnas()
        {
            return new[] { chkCodigo, chkTipoUsuario, chkNombre, chkContrasenia, chkCorreoElectronico,
                chkFechaCreacion, chkFechaUltimoAcceso, chkEstado }
                .Concat(chkEsDueno == null ? Enumerable.Empty<CheckBox>() : new[] { chkEsDueno });
        }

        private void SincronizarActivarFiltros()
        {
            if (chkActivarFiltros == null || actualizandoFiltro) return;
            actualizandoFiltro = true;
            try { chkActivarFiltros.Checked = ObtenerFiltrosColumnas().All(c => c.Checked); }
            finally { actualizandoFiltro = false; }
        }

        private void AjustarPosicionOpcionesColumnas()
        {
            if (layoutOpcionesColumnas == null || selector == null) return;
            OrganizarOpcionesColumnas();
            AjustarAnchosOpcionesFiltros();
        }

        private void AjustarAnchosOpcionesFiltros()
        {
            if (layoutOpcionesColumnas == null || chkActivarFiltros == null || chkSeleccionar == null ||
                layoutOpcionesColumnas.ColumnStyles.Count < 3) return;

            chkActivarFiltros.AutoSize = true;
            chkSeleccionar.AutoSize = true;
            int anchoActivar = TextRenderer.MeasureText(chkActivarFiltros.Text, chkActivarFiltros.Font).Width +
                28 + chkActivarFiltros.Margin.Horizontal + 12;
            int anchoSeleccionar = TextRenderer.MeasureText(chkSeleccionar.Text, chkSeleccionar.Font).Width +
                28 + chkSeleccionar.Margin.Horizontal + 12;
            layoutOpcionesColumnas.ColumnStyles[0].SizeType = SizeType.Absolute;
            layoutOpcionesColumnas.ColumnStyles[0].Width = anchoActivar;
            layoutOpcionesColumnas.ColumnStyles[2].SizeType = SizeType.Absolute;
            layoutOpcionesColumnas.ColumnStyles[2].Width = anchoSeleccionar;
            layoutOpcionesColumnas.PerformLayout();
        }

        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => IniciarNuevo();
            btnLimpiar.Click += (s, e) =>
            {
                actualizandoFiltro = true;
                try
                {
                    txtBuscar.Clear();
                    cboFiltroEstado.SelectedIndex = 0;
                }
                finally { actualizandoFiltro = false; }
                LimpiarFormulario();
                BuscarUsuarios();
            };
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarUsuarios();
            cboFiltroEstado.SelectedIndexChanged += (s, e) =>
            {
                if (!actualizandoFiltro) BuscarUsuarios();
            };
            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { BuscarUsuarios(); e.SuppressKeyPress = true; }
            };
            dgvUsuarios.SelectionChanged += DgvUsuarios_SelectionChanged;
            dgvUsuarios.CellFormatting += DgvUsuarios_CellFormatting;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;
            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTipoUsuario.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNombre.CheckedChanged += (s, e) => ActualizarColumnas();
            chkContrasenia.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCorreoElectronico.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaCreacion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaUltimoAcceso.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void ConfigurarNavegacionTeclado()
        {
            Control[] orden =
            {
                cmbTipoUsuario, txtNombre, txtContrasenia, txtCorreoElectronico,
                dtpFechaCreacion, dtpFechaUltimoAcceso, rdoActivo, rdoInactivo
            };
            foreach (Control control in orden)
                control.KeyDown += CampoCaptura_KeyDown;
            AcceptButton = null;
        }

        private void CampoCaptura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Alt || e.Control || e.Shift) return;
            ComboBox combo = sender as ComboBox;
            if (combo != null && combo.DroppedDown) return;

            Control actual = sender as Control;
            if (actual == null || !ValidarCampoParaAvanzar(actual)) return;
            e.SuppressKeyPress = true;
            e.Handled = true;

            Control[] orden =
            {
                cmbTipoUsuario, txtNombre, txtContrasenia, txtCorreoElectronico,
                dtpFechaCreacion, dtpFechaUltimoAcceso, rdoActivo, rdoInactivo
            };
            int indice = Array.IndexOf(orden, actual);
            for (int i = indice + 1; i < orden.Length; i++)
            {
                if (!orden[i].Enabled || !orden[i].Visible) continue;
                orden[i].Focus();
                return;
            }
            // Enter nunca ejecuta Guardar; el último campo permanece enfocado.
        }

        private bool ValidarCampoParaAvanzar(Control control)
        {
            if (control == cmbTipoUsuario && !esSupabase)
                return Requerido(cmbTipoUsuario.Text, cmbTipoUsuario, "Tipo de usuario");
            if (control == txtNombre)
                return Requerido(txtNombre.Text, txtNombre, "Nombre");
            if (control == txtContrasenia)
            {
                if (modoNuevo && string.IsNullOrWhiteSpace(txtContrasenia.Text))
                    return Requerido(txtContrasenia.Text, txtContrasenia, "Contraseña");
                if (esSupabase && !string.IsNullOrWhiteSpace(txtContrasenia.Text) && txtContrasenia.Text.Trim().Length < 8)
                {
                    MessageBox.Show("La contraseña de Supabase debe tener al menos 8 caracteres.");
                    txtContrasenia.Focus();
                    return false;
                }
            }
            if (control == txtCorreoElectronico)
            {
                if (!Requerido(txtCorreoElectronico.Text, txtCorreoElectronico, "Correo electrónico")) return false;
                try { new MailAddress(txtCorreoElectronico.Text.Trim()); }
                catch (FormatException)
                {
                    MessageBox.Show("Escribe un correo electrónico válido.");
                    txtCorreoElectronico.Focus();
                    return false;
                }
            }
            return true;
        }

        private void ConfigurarResaltadoCampos()
        {
            Control[] campos =
            {
                cmbTipoUsuario, txtNombre, txtContrasenia, txtCorreoElectronico,
                dtpFechaCreacion, dtpFechaUltimoAcceso
            };
            foreach (Control campo in campos)
            {
                CampoRedondeado.Aplicar(campo, 7);
                campo.MouseEnter += Campo_MouseEnter;
                campo.MouseLeave += Campo_MouseLeave;
                campo.Enter += Campo_Enter;
                campo.Leave += Campo_Leave;
                campo.EnabledChanged += (s, e) => RestaurarColorCampo(campo);
            }
            cmbTipoUsuario.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTipoUsuario.DrawItem += CmbTipoUsuario_DrawItem;
            CampoRedondeado.Aplicar(txtCodigo, 7);
            rdoActivo.EnabledChanged += EstadoRadio_EnabledChanged;
            rdoInactivo.EnabledChanged += EstadoRadio_EnabledChanged;
            ActualizarColorRadioEstado();
        }

        private void CmbTipoUsuario_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            bool acentuado = modoNuevo;
            Color fondo = acentuado ? ColorTranslator.FromHtml("#00D9FF") :
                (e.State.HasFlag(DrawItemState.Selected) ? Color.FromArgb(205, 243, 255) : cmbTipoUsuario.BackColor);
            Color texto = acentuado ? Color.FromArgb(7, 35, 66) : cmbTipoUsuario.ForeColor;
            using (SolidBrush pincel = new SolidBrush(fondo)) e.Graphics.FillRectangle(pincel, e.Bounds);
            TextRenderer.DrawText(e.Graphics, cmbTipoUsuario.Items[e.Index].ToString(), cmbTipoUsuario.Font,
                Rectangle.Inflate(e.Bounds, -4, 0), texto, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            e.DrawFocusRectangle();
        }

        private void EstadoRadio_EnabledChanged(object sender, EventArgs e) { ActualizarColorRadioEstado(); }

        private void ActualizarColorRadioEstado()
        {
            // El fondo del bloque es oscuro; mantener contraste también en estado deshabilitado.
            rdoActivo.ForeColor = System.Drawing.Color.White;
            rdoInactivo.ForeColor = System.Drawing.Color.White;
        }

        private void Campo_MouseEnter(object sender, EventArgs e)
        {
            Control campo = sender as Control;
            if (campo != null && campo.Enabled && !campo.Focused)
            {
                campo.BackColor = System.Drawing.Color.FromArgb(205, 243, 255);
                campo.ForeColor = System.Drawing.Color.FromArgb(7, 35, 66);
            }
        }

        private void Campo_MouseLeave(object sender, EventArgs e)
        {
            RestaurarColorCampo(sender as Control);
        }

        private void Campo_Enter(object sender, EventArgs e)
        {
            Control campo = sender as Control;
            if (campo != null && campo.Enabled)
            {
                campo.BackColor = System.Drawing.Color.FromArgb(128, 222, 255);
                campo.ForeColor = System.Drawing.Color.FromArgb(7, 35, 66);
            }
        }

        private void Campo_Leave(object sender, EventArgs e)
        {
            RestaurarColorCampo(sender as Control);
        }

        private void RestaurarColorCampo(Control campo)
        {
            if (campo == null) return;
            if (!campo.Enabled)
            {
                campo.BackColor = System.Drawing.Color.FromArgb(215, 222, 230);
                campo.ForeColor = System.Drawing.Color.FromArgb(104, 116, 128);
            }
            else if (PreferenciasVisuales.Tema == TemaInterfaz.Negro)
            {
                campo.BackColor = System.Drawing.Color.FromArgb(45, 49, 53);
                campo.ForeColor = System.Drawing.Color.FromArgb(238, 242, 246);
            }
            else if (PreferenciasVisuales.Tema == TemaInterfaz.Claro)
            {
                campo.BackColor = System.Drawing.Color.White;
                campo.ForeColor = System.Drawing.Color.FromArgb(7, 35, 66);
            }
            else
            {
                campo.BackColor = System.Drawing.Color.FromArgb(225, 236, 246);
                campo.ForeColor = System.Drawing.Color.FromArgb(7, 35, 66);
            }
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Control opcion = sender as Control;
            if (opcion != null && opcion.Tag != null) Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoUsuario", chkCodigo.Checked);
            CambiarVisibilidad("TipoUsuario", chkTipoUsuario.Checked);
            CambiarVisibilidad("Nombre", chkNombre.Checked);
            CambiarVisibilidad("Contrasenia", chkContrasenia.Checked);
            CambiarVisibilidad("CorreoElectronico", chkCorreoElectronico.Checked);
            CambiarVisibilidad("FechaCreacion", chkFechaCreacion.Checked);
            CambiarVisibilidad("FechaUltimoAcceso", chkFechaUltimoAcceso.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
            CambiarVisibilidad("EsDueno", esSupabase && chkEsDueno != null && chkEsDueno.Checked);
            CambiarVisibilidad("AuthUserId", false);
            CambiarVisibilidad("Contrasenia", false);
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvUsuarios.Columns.Contains(columna)) dgvUsuarios.Columns[columna].Visible = visible;
        }

        private void DgvUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvUsuarios.Columns[e.ColumnIndex].Name == "EsDueno")
            {
                e.Value = Convert.ToBoolean(e.Value);
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex >= 0 && dgvUsuarios.Columns[e.ColumnIndex].Name == "Estado" && e.Value != null)
            {
                bool activo = Convert.ToBoolean(e.Value);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvUsuarios.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = activo ? "Activo" : "Inactivo";
            }
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvUsuarios.Rows.Count > 0) SeleccionarFila(dgvUsuarios.CurrentRow == null ? 0 : dgvUsuarios.CurrentRow.Index);
            }
            else
            {
                dgvUsuarios.ClearSelection();
                modoEdicion = false;
                EstablecerCamposHabilitados(false);
                ActualizarEstadoBotones(false);
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvUsuarios.Rows.Count) return;
            dgvUsuarios.ClearSelection();
            DataGridViewCell celda = dgvUsuarios.Rows[indice].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
            if (celda != null) dgvUsuarios.CurrentCell = celda;
            dgvUsuarios.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!modoNuevo || !ValidarFormulario(true)) return;
            try
            {
                int codigo = negocio.Agregar(CrearEntidad());
                LimpiarFormulario();
                actualizandoFiltro = true;
                try { txtBuscar.Clear(); cboFiltroEstado.SelectedIndex = 0; }
                finally { actualizandoFiltro = false; }
                BuscarUsuarios();
                int indiceAgregado = resultadosActuales.FindIndex(u => u.CodigoUsuario == codigo);
                if (indiceAgregado >= 0)
                {
                    paginaActual = indiceAgregado / RegistrosPorPagina + 1;
                    MostrarPaginaActual();
                }
                MessageBox.Show("Usuario agregado correctamente. Código: " + codigo, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al agregar usuario", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            UsuariosEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null) { MessageBox.Show("Marca Seleccionar y elige una fila de la tabla."); return; }
            if (!modoEdicion)
            {
                modoNuevo = false;
                modoEdicion = true;
                EstablecerCamposHabilitados(true);
                ActualizarEstadoBotones(true);
                MessageBox.Show("Modifica los campos y pulsa Editar nuevamente para aplicar los cambios.");
                return;
            }
            if (!ValidarFormulario(false)) return;
            try
            {
                UsuariosEntidad cambios = CrearEntidad();
                cambios.CodigoUsuario = seleccionado.CodigoUsuario;
                negocio.Editar(cambios);
                LimpiarFormulario();
                BuscarUsuarios();
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al editar usuario", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            UsuariosEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null) { MessageBox.Show("Marca Seleccionar y elige una fila de la tabla."); return; }
            if (MessageBox.Show("¿Deseas marcar como inactivo al usuario " + seleccionado.Nombre + "?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                negocio.Eliminar(seleccionado.CodigoUsuario);
                LimpiarFormulario();
                BuscarUsuarios();
                MessageBox.Show("Usuario marcado como inactivo.", "Eliminar usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar usuario", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private UsuariosEntidad CrearEntidad()
        {
            return new UsuariosEntidad(cmbTipoUsuario.Text.Trim(), txtNombre.Text.Trim(), txtContrasenia.Text.Trim(),
                txtCorreoElectronico.Text.Trim(), dtpFechaCreacion.Value,
                dtpFechaUltimoAcceso.Checked ? (DateTime?)dtpFechaUltimoAcceso.Value : null, rdoActivo.Checked);
        }

        private bool ValidarFormulario(bool nuevaCuenta)
        {
            if (!Requerido(txtNombre.Text, txtNombre, "Nombre")) return false;
            if (!esSupabase && !Requerido(cmbTipoUsuario.Text, cmbTipoUsuario, "Tipo de usuario")) return false;
            if (nuevaCuenta && !Requerido(txtContrasenia.Text, txtContrasenia, "Contraseña")) return false;
            if (!Requerido(txtCorreoElectronico.Text, txtCorreoElectronico, "Correo electrónico")) return false;
            if (esSupabase && (nuevaCuenta || !string.IsNullOrWhiteSpace(txtContrasenia.Text))
                && txtContrasenia.Text.Trim().Length < 8)
            {
                MessageBox.Show("La contraseña de Supabase debe tener al menos 8 caracteres.");
                txtContrasenia.Focus();
                return false;
            }
            try { new MailAddress(txtCorreoElectronico.Text.Trim()); }
            catch (FormatException)
            {
                MessageBox.Show("Escribe un correo electrónico válido.");
                txtCorreoElectronico.Focus();
                return false;
            }
            return true;
        }

        private bool Requerido(string texto, Control control, string nombre)
        {
            if (string.IsNullOrWhiteSpace(texto)) { MessageBox.Show("El campo " + nombre + " es obligatorio."); control.Focus(); return false; }
            if (texto.Trim().Length <= 100) return true;
            MessageBox.Show("El campo " + nombre + " no puede superar 100 caracteres.");
            control.Focus();
            return false;
        }

        private UsuariosEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvUsuarios.CurrentRow == null) return null;
            return dgvUsuarios.CurrentRow.DataBoundItem as UsuariosEntidad;
        }

        private void DgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked) return;
            UsuariosEntidad u = ObtenerSeleccionado();
            if (u == null) return;
            cargando = true;
            txtCodigo.Text = u.CodigoUsuario.ToString();
            cmbTipoUsuario.Text = u.TipoUsuario;
            txtNombre.Text = u.Nombre;
            txtContrasenia.Clear();
            txtCorreoElectronico.Text = u.CorreoElectronico;
            dtpFechaCreacion.Value = LimitarFecha(dtpFechaCreacion, u.FechaCreacion);
            if (u.FechaUltimoAcceso.HasValue)
            {
                dtpFechaUltimoAcceso.Value = LimitarFecha(dtpFechaUltimoAcceso, u.FechaUltimoAcceso.Value);
                dtpFechaUltimoAcceso.Checked = true;
            }
            else dtpFechaUltimoAcceso.Checked = false;
            rdoActivo.Checked = u.Estado;
            rdoInactivo.Checked = !u.Estado;
            cargando = false;
            modoEdicion = false;
            EstablecerCamposHabilitados(false);
            ActualizarEstadoBotones(true);
        }

        private static DateTime LimitarFecha(DateTimePicker control, DateTime valor)
        {
            return valor < control.MinDate ? control.MinDate : (valor > control.MaxDate ? control.MaxDate : valor);
        }

        private void BuscarUsuarios()
        {
            string criterio = txtBuscar.Text.Trim();
            try
            {
                List<UsuariosEntidad> coincidencias = negocio.Buscar(criterio);
                int cantidadActivos = coincidencias.Count(usuario => usuario.Estado);
                int cantidadInactivos = coincidencias.Count(usuario => !usuario.Estado);
                List<UsuariosEntidad> resultado = coincidencias;
                if (cboFiltroEstado.SelectedIndex == 1)
                    resultado = coincidencias.Where(usuario => usuario.Estado).ToList();
                else if (cboFiltroEstado.SelectedIndex == 2)
                    resultado = coincidencias.Where(usuario => !usuario.Estado).ToList();
                resultadosActuales = resultado;
                paginaActual = 1;
                MostrarPaginaActual();
                lblRegistros.Text = "Resultados: " + coincidencias.Count + "  |  Activos: " +
                    cantidadActivos + "  |  Inactivos: " + cantidadInactivos;
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void MostrarPaginaActual()
        {
            int totalPaginas = resultadosActuales.Count == 0 ? 0
                : (int)Math.Ceiling(resultadosActuales.Count / (double)RegistrosPorPagina);
            if (totalPaginas > 0 && paginaActual > totalPaginas) paginaActual = totalPaginas;
            int omitir = Math.Max(0, (paginaActual - 1) * RegistrosPorPagina);
            List<UsuariosEntidad> lista = resultadosActuales.Skip(omitir).Take(RegistrosPorPagina).ToList();
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvUsuarios.DataSource = origen;
            ActualizarColumnas();
            dgvUsuarios.ClearSelection();
            lblPagina.Text = totalPaginas == 0 ? "Página 0 de 0" : "Página " + paginaActual + " de " + totalPaginas;
            btnPaginaAnterior.Enabled = paginaActual > 1;
            btnPaginaSiguiente.Enabled = paginaActual < totalPaginas;
            AjustarInterfazUsuarios();
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
        }

        private void CambiarPagina(int nuevaPagina)
        {
            int totalPaginas = resultadosActuales.Count == 0 ? 0
                : (int)Math.Ceiling(resultadosActuales.Count / (double)RegistrosPorPagina);
            if (nuevaPagina < 1 || nuevaPagina > totalPaginas) return;
            paginaActual = nuevaPagina;
            MostrarPaginaActual();
        }

        private void LimpiarFormulario()
        {
            cargando = true;
            modoNuevo = false;
            modoEdicion = false;
            txtCodigo.Clear(); cmbTipoUsuario.SelectedIndex = -1; txtNombre.Clear(); txtContrasenia.Clear(); txtCorreoElectronico.Clear();
            dtpFechaCreacion.Value = DateTime.Now; dtpFechaUltimoAcceso.Value = DateTime.Now; dtpFechaUltimoAcceso.Checked = false;
            rdoActivo.Checked = true; rdoInactivo.Checked = false;
            dgvUsuarios.ClearSelection(); chkSeleccionar.Checked = false;
            cargando = false;
            EstablecerCamposHabilitados(false);
            RestaurarColorCampo(cmbTipoUsuario);
            cmbTipoUsuario.Invalidate();
            ActualizarEstadoBotones(false);
            if (esSupabase)
            {
                cmbTipoUsuario.SelectedItem = "Administrador";
                lblContrasenia.Text = "Contraseña inicial/nueva:";
            }
        }

        private void IniciarNuevo()
        {
            LimpiarFormulario();
            modoNuevo = true;
            cmbTipoUsuario.BackColor = ColorTranslator.FromHtml("#00D9FF");
            cmbTipoUsuario.ForeColor = Color.FromArgb(7, 35, 66);
            cmbTipoUsuario.Invalidate();
            EstablecerCamposHabilitados(true);
            ActualizarEstadoBotones(false);
            if (esSupabase) lblContrasenia.Text = "Contraseña inicial:";
            Control primerCampo = cmbTipoUsuario.Enabled ? (Control)cmbTipoUsuario : txtNombre;
            BeginInvoke((MethodInvoker)delegate
            {
                if (primerCampo.Enabled && primerCampo.Visible) primerCampo.Focus();
            });
        }

        private void EstablecerCamposHabilitados(bool habilitados)
        {
            cmbTipoUsuario.Enabled = habilitados && !esSupabase; txtNombre.Enabled = habilitados; txtContrasenia.Enabled = habilitados;
            txtCorreoElectronico.Enabled = habilitados; dtpFechaCreacion.Enabled = habilitados && !esSupabase;
            dtpFechaUltimoAcceso.Enabled = habilitados && !esSupabase;
            bool esPropietario = ObtenerSeleccionado() != null && ObtenerSeleccionado().EsDueno;
            bool esLaSesionActual = ObtenerSeleccionado() != null && UsuariosNegocio.UsuarioActual != null
                && ObtenerSeleccionado().CodigoUsuario == UsuariosNegocio.UsuarioActual.CodigoUsuario;
            rdoActivo.Enabled = habilitados && !(esLaSesionActual || (esSupabase && esPropietario));
            rdoInactivo.Enabled = habilitados && !(esLaSesionActual || (esSupabase && esPropietario));
            if (esSupabase && esPropietario && UsuariosNegocio.UsuarioActual != null
                && !UsuariosNegocio.UsuarioActual.EsDueno) txtContrasenia.Enabled = false;
            if (esSupabase && !modoNuevo && txtContrasenia.Enabled)
                lblContrasenia.Text = "Contraseña nueva (opcional):";
        }

        private void ActualizarEstadoBotones(bool haySeleccion)
        {
            btnGuardar.Enabled = modoNuevo;
            btnEditar.Enabled = !modoNuevo && (modoEdicion || haySeleccion);
            UsuariosEntidad seleccionado = ObtenerSeleccionado();
            bool protegido = seleccionado != null
                && ((esSupabase && seleccionado.EsDueno) || (UsuariosNegocio.UsuarioActual != null
                    && seleccionado.CodigoUsuario == UsuariosNegocio.UsuarioActual.CodigoUsuario));
            btnEliminar.Enabled = !modoNuevo && !modoEdicion && haySeleccion && !protegido;
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
        }
    }
}
