using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FontAwesome.Sharp;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmSucursales : Form
    {
        private readonly BindingList<SucursalesEntidad> sucursales = new BindingList<SucursalesEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly SucursalesNegocio negocio = new SucursalesNegocio();
        private bool cargando;
        private bool modoNuevo;
        private bool modoEdicion;
        private bool actualizandoFiltros;
        private readonly List<IconButton> botonesBarraLateral = new List<IconButton>();
        private ComboBox cboFiltroEstado;
        private CheckBox chkActivarFiltros;
        private TableLayoutPanel layoutBusqueda;
        private TableLayoutPanel layoutOpcionesColumnas;
        private TableLayoutPanel layoutBloquesSucursales;
        private PanelRedondeado pnlDashboard;
        private DashboardCrudSimple dashboardCrud;
        private Label[] valoresDashboard;
        private Label lblAlertasDashboard;
        private Label lblActividadDashboard;
        private Chart graficaDashboard;

        public FrmSucursales()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || (Site != null && Site.DesignMode)) return;
            ConfigurarDisenoAdaptable();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();
            CargarDepartamentos();
            origen.DataSource = sucursales;
            dgvSucursales.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvSucursales);
            ConfigurarFiltroBusqueda();
            ConfigurarOpcionesColumnas();
            AjustesDisenoCrud.AplicarCatalogoModeloPagos(this, pnlHeader, lblTitulo, lblSubtitulo,
                pnlBusqueda, txtBuscar, btnBuscar, pnlColumnas, lblColumnas, selector, chkSeleccionar);
            dashboardCrud = new DashboardCrudSimple("Resumen de sucursales", "Aperturas por departamento", ObtenerDatosDashboardSucursales);
            ConstruirOrdenBloques();
            TemaVisualCrud.Aplicar(this);
            ConfigurarResaltadoCampos();
            ConectarEventos();
            ConfigurarNavegacionTeclado();
            ActualizarColumnas();
            LimpiarFormulario();
            BuscarSucursales();
            AjustarInterfazSucursales();
            dgvSucursales.DataBindingComplete += (s, e) => AjustarInterfazSucursales();
            Shown += (s, e) => AjustarInterfazSucursales();
            Shown += async (s, e) => await dashboardCrud.CargarAsync();
            SizeChanged += (s, e) => AjustarInterfazSucursales();
        }

        private DashboardCrudDatos ObtenerDatosDashboardSucursales()
        {
            List<SucursalesEntidad> lista = negocio.Listar();
            List<SucursalesEntidad> activas = lista.Where(s => s.Estado).ToList();
            int sinTelefono = activas.Count(s => string.IsNullOrWhiteSpace(s.Telefono));
            int sinCorreo = activas.Count(s => string.IsNullOrWhiteSpace(s.CorreoElectronico));
            return new DashboardCrudDatos
            {
                TitulosIndicadores = new[] { "SUCURSALES REGISTRADAS", "ACTIVAS", "INACTIVAS", "DEPARTAMENTOS" },
                ValoresIndicadores = new[]
                {
                    lista.Count.ToString("N0"), activas.Count.ToString("N0"),
                    lista.Count(s => !s.Estado).ToString("N0"),
                    lista.Where(s => !string.IsNullOrWhiteSpace(s.Departamento))
                        .Select(s => s.Departamento.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString("N0")
                },
                Serie = activas.Where(s => !string.IsNullOrWhiteSpace(s.Departamento))
                    .GroupBy(s => s.Departamento.Trim(), StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Take(8)
                    .Select(g => new KeyValuePair<string, int>(g.Key, g.Count())).ToList(),
                Alertas = "• Sin teléfono: " + sinTelefono + Environment.NewLine + "• Sin correo: " + sinCorreo,
                Actividad = activas.Count == 0 ? "No hay sucursales activas." :
                    string.Join(Environment.NewLine, activas.OrderByDescending(s => s.FechaApertura).Take(4)
                        .Select(s => s.FechaApertura.ToString("dd/MM", CultureInfo.GetCultureInfo("es-GT")) + " · " + s.Nombre))
            };
        }

        private void ConfigurarDisenoAdaptable()
        {
            pnlHeader.Height = 108;
            pnlContenido.Padding = new Padding(14);
            pnlContenido.AutoScroll = true;
            pnlFormulario.Height = 310;
            pnlAcciones.Height = 58;
            lblTitulo.Height = 48;
            lblSubtitulo.Height = 42;
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblFormularioTitulo.Height = 40;
            lblCatalogoTitulo.Height = 40;
            lblColumnas.Height = 40;
            pnlBusqueda.Height = 54;
            pnlColumnas.Height = 76;
            selector.WrapContents = false;
            selector.AutoScroll = true;
            dgvSucursales.Dock = DockStyle.Top;
            dgvSucursales.RowTemplate.Height = 32;
            dgvSucursales.ColumnHeadersHeight = 40;
            dgvSucursales.ScrollBars = ScrollBars.Both;
            selector.SizeChanged += (s, e) => OrganizarFiltrosColumnas();
            pnlColumnas.SizeChanged += (s, e) => OrganizarFiltrosColumnas();
        }

        private void ConfigurarFiltroBusqueda()
        {
            cboFiltroEstado = Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault();
            if (cboFiltroEstado == null) return;
            Control host = cboFiltroEstado.Parent;
            if (host != null) host.Controls.Remove(cboFiltroEstado);
            if (btnBuscar.Parent != null) btnBuscar.Parent.Controls.Remove(btnBuscar);
            Panel panelEstadoComun = pnlBusqueda.Controls.Find("pnlFiltroEstadoComun", false)
                .OfType<Panel>().FirstOrDefault();
            if (panelEstadoComun != null)
            {
                pnlBusqueda.Controls.Remove(panelEstadoComun);
                panelEstadoComun.Dispose();
            }
            pnlBusqueda.Controls.Remove(txtBuscar);
            pnlBusqueda.Controls.Remove(btnBuscar);
            pnlBusqueda.Padding = new Padding(8, 4, 8, 4);
            layoutBusqueda = new TableLayoutPanel
            {
                Name = "layoutBusquedaSucursales", Dock = DockStyle.Fill, ColumnCount = 3,
                RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = pnlBusqueda.BackColor
            };
            layoutBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            layoutBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutBusqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, btnNuevo.Width));
            layoutBusqueda.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            cboFiltroEstado.Dock = DockStyle.Fill;
            cboFiltroEstado.Margin = new Padding(0, 0, 8, 0);
            cboFiltroEstado.Font = new Font("Segoe UI", 9.5F);
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Margin = new Padding(0, 0, 8, 0);
            btnBuscar.Dock = DockStyle.Fill;
            btnBuscar.Margin = Padding.Empty;
            layoutBusqueda.Controls.Add(cboFiltroEstado, 0, 0);
            layoutBusqueda.Controls.Add(txtBuscar, 1, 0);
            layoutBusqueda.Controls.Add(btnBuscar, 2, 0);
            pnlBusqueda.Controls.Add(layoutBusqueda);
            pnlBusqueda.Height = btnNuevo.Height + 16;
        }

        private void ConfigurarOpcionesColumnas()
        {
            chkSeleccionar.Text = "Seleccionar registro";
            selector.Controls.Remove(chkSeleccionar);
            chkActivarFiltros = new CheckBox
            {
                Name = "chkActivarTodosFiltros", Text = "Activar todos los filtros",
                AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.White,
                BackColor = Color.Transparent, Font = new Font("Segoe UI", 9.5F)
            };
            layoutOpcionesColumnas = new TableLayoutPanel
            {
                Name = "layoutOpcionesColumnasSucursales", Dock = DockStyle.Fill,
                ColumnCount = 3, RowCount = 2, Margin = Padding.Empty,
                Padding = Padding.Empty, BackColor = pnlColumnas.BackColor
            };
            int anchoActivar = TextRenderer.MeasureText(chkActivarFiltros.Text, chkActivarFiltros.Font).Width +
                28 + chkActivarFiltros.Margin.Horizontal + 12;
            int anchoSeleccionar = chkSeleccionar.GetPreferredSize(Size.Empty).Width + 36;
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoActivar));
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoSeleccionar));
            layoutOpcionesColumnas.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layoutOpcionesColumnas.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            lblColumnas.Dock = DockStyle.Fill;
            layoutOpcionesColumnas.Controls.Add(lblColumnas, 0, 0);
            layoutOpcionesColumnas.SetColumnSpan(lblColumnas, 3);
            chkActivarFiltros.Margin = new Padding(4, 4, 10, 0);
            chkSeleccionar.Margin = new Padding(8, 4, 4, 0);
            chkSeleccionar.Anchor = AnchorStyles.Right;
            selector.Dock = DockStyle.Fill;
            selector.Margin = Padding.Empty;
            selector.Padding = Padding.Empty;
            selector.FlowDirection = FlowDirection.LeftToRight;
            selector.WrapContents = false;
            selector.AutoScroll = true;
            layoutOpcionesColumnas.Controls.Add(chkActivarFiltros, 0, 1);
            layoutOpcionesColumnas.Controls.Add(selector, 1, 1);
            layoutOpcionesColumnas.Controls.Add(chkSeleccionar, 2, 1);
            pnlColumnas.Controls.Remove(selector);
            pnlColumnas.Controls.Remove(lblColumnas);
            pnlColumnas.Controls.Add(layoutOpcionesColumnas);
            pnlColumnas.Height = 76;
            chkActivarFiltros.CheckedChanged += (s, e) =>
            {
                if (actualizandoFiltros) return;
                actualizandoFiltros = true;
                try
                {
                    foreach (CheckBox filtro in ObtenerFiltrosColumnas()) filtro.Checked = chkActivarFiltros.Checked;
                }
                finally { actualizandoFiltros = false; }
                ActualizarColumnas();
            };
            foreach (CheckBox filtro in ObtenerFiltrosColumnas())
                filtro.CheckedChanged += (s, e) =>
                {
                    if (actualizandoFiltros) return;
                    SincronizarActivarFiltros();
                    ActualizarColumnas();
                };
            SincronizarActivarFiltros();
            OrganizarFiltrosColumnas();
            AjustarAnchosOpcionesFiltrosSucursal();
        }

        private IEnumerable<CheckBox> ObtenerFiltrosColumnas()
        {
            return selector.Controls.OfType<CheckBox>().Where(c => c != chkSeleccionar);
        }

        private void SincronizarActivarFiltros()
        {
            if (chkActivarFiltros == null) return;
            bool estadoAnterior = actualizandoFiltros;
            actualizandoFiltros = true;
            try { chkActivarFiltros.Checked = ObtenerFiltrosColumnas().All(c => c.Checked); }
            finally { actualizandoFiltros = estadoAnterior; }
        }

        private void OrganizarFiltrosColumnas()
        {
            if (selector == null) return;
            selector.FlowDirection = FlowDirection.LeftToRight;
            selector.WrapContents = false;
            selector.AutoScroll = true;
            selector.Height = 36;
            foreach (CheckBox filtro in ObtenerFiltrosColumnas())
            {
                filtro.AutoSize = true;
                filtro.Font = new Font("Segoe UI", 9F);
                filtro.Margin = new Padding(2, 3, 5, 3);
            }
            AjustarAnchosOpcionesFiltrosSucursal();
        }

        private void AjustarAnchosOpcionesFiltrosSucursal()
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

        private void ConfigurarDashboard()
        {
            Color fondo = ColorTranslator.FromHtml("#08243A");
            Color superficie = ColorTranslator.FromHtml("#0B3558");
            pnlDashboard = new PanelRedondeado
            {
                Name = "pnlDashboardSucursales", Dock = DockStyle.Fill, Height = 360,
                Padding = new Padding(12), BackColor = fondo, Margin = Padding.Empty
            };
            pnlDashboard.RadioEsquinas = 12;
            TableLayoutPanel raiz = new TableLayoutPanel
            {
                Name = "layoutDashboardSucursales", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 3, Padding = Padding.Empty,
                Margin = Padding.Empty, BackColor = fondo
            };
            raiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            TableLayoutPanel encabezado = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                Padding = Padding.Empty, Margin = Padding.Empty, BackColor = fondo
            };
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
            encabezado.Controls.Add(new Label
            {
                Text = "Dashboard de sucursales", Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);
            Button actualizar = new Button
            {
                Text = "↻ Actualizar", Dock = DockStyle.Fill, Margin = new Padding(4, 1, 0, 1),
                FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#0B6E91"),
                ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand
            };
            actualizar.FlatAppearance.BorderSize = 0;
            actualizar.Click += (s, e) => ActualizarDashboardSucursales(sucursales.ToList());
            encabezado.Controls.Add(actualizar, 1, 0);
            raiz.Controls.Add(encabezado, 0, 0);

            string[] titulos = { "SUCURSALES REGISTRADAS", "ACTIVAS", "INACTIVAS", "DEPARTAMENTOS" };
            valoresDashboard = new Label[4];
            TableLayoutPanel tarjetas = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1,
                Padding = Padding.Empty, Margin = new Padding(0, 5, 0, 5), BackColor = fondo
            };
            for (int i = 0; i < 4; i++) tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            for (int i = 0; i < titulos.Length; i++)
            {
                Panel tarjeta = new Panel
                {
                    Dock = DockStyle.Fill, Margin = new Padding(0, 0, i == 3 ? 0 : 8, 0),
                    Padding = new Padding(10, 6, 6, 4), BackColor = superficie
                };
                Label titulo = new Label
                {
                    Text = titulos[i], Dock = DockStyle.Top, Height = 20,
                    ForeColor = ColorTranslator.FromHtml("#A9BAC8"),
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), AutoEllipsis = true
                };
                valoresDashboard[i] = new Label
                {
                    Text = "—", Dock = DockStyle.Fill,
                    ForeColor = i == 0 ? ColorTranslator.FromHtml("#00D9FF") : Color.White,
                    Font = new Font("Segoe UI", 18F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft
                };
                tarjeta.Controls.Add(valoresDashboard[i]);
                tarjeta.Controls.Add(titulo);
                tarjetas.Controls.Add(tarjeta, i, 0);
            }
            raiz.Controls.Add(tarjetas, 0, 1);

            TableLayoutPanel detalle = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                Padding = Padding.Empty, Margin = Padding.Empty, BackColor = fondo
            };
            detalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64));
            detalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
            Panel panelGrafica = CrearPanelGraficaSucursales(out graficaDashboard);
            detalle.Controls.Add(panelGrafica, 0, 0);
            TableLayoutPanel derecha = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                Padding = Padding.Empty, Margin = new Padding(8, 0, 0, 0), BackColor = fondo
            };
            derecha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            derecha.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            derecha.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            Panel alertas = CrearPanelDashboardSucursales("Alertas operativas", out lblAlertasDashboard);
            Panel actividad = CrearPanelDashboardSucursales("Actividad reciente", out lblActividadDashboard);
            alertas.Margin = new Padding(0, 0, 0, 5);
            actividad.Margin = new Padding(0, 5, 0, 0);
            derecha.Controls.Add(alertas, 0, 0);
            derecha.Controls.Add(actividad, 0, 1);
            detalle.Controls.Add(derecha, 1, 0);
            raiz.Controls.Add(detalle, 0, 2);
            pnlDashboard.Controls.Add(raiz);

            ChartArea area = new ChartArea("Aperturas") { BackColor = superficie };
            area.AxisX.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisX.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisY.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#34495E");
            graficaDashboard.ChartAreas.Add(area);
            graficaDashboard.Legends.Add(new Legend("Leyenda")
            {
                Docking = Docking.Bottom, BackColor = superficie, ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F)
            });
            graficaDashboard.Series.Add(new Series("Aperturas")
            {
                ChartArea = "Aperturas", Legend = "Leyenda", ChartType = SeriesChartType.Line,
                BorderWidth = 3, Color = ColorTranslator.FromHtml("#00D9FF"),
                MarkerStyle = MarkerStyle.Circle, MarkerSize = 5
            });
        }

        private Panel CrearPanelGraficaSucursales(out Chart grafica)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(7, 5, 7, 5),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty
            };
            panel.Controls.Add(new Label
            {
                Text = "Aperturas por mes", Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF")
            });
            grafica = new Chart
            {
                Dock = DockStyle.Fill, BackColor = ColorTranslator.FromHtml("#0B3558"),
                Palette = ChartColorPalette.None
            };
            panel.Controls.Add(grafica);
            return panel;
        }

        private Panel CrearPanelDashboardSucursales(string titulo, out Label contenido)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(9, 6, 9, 6),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty
            };
            panel.Controls.Add(new Label
            {
                Text = titulo, Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true
            });
            contenido = new Label
            {
                Dock = DockStyle.Fill, ForeColor = Color.White, Font = new Font("Segoe UI", 9F),
                AutoEllipsis = true, TextAlign = ContentAlignment.TopLeft
            };
            panel.Controls.Add(contenido);
            return panel;
        }

        private void ConstruirOrdenBloques()
        {
            layoutBloquesSucursales = new TableLayoutPanel
            {
                Name = "layoutBloquesSucursales", Dock = DockStyle.Top, ColumnCount = 1,
                RowCount = 5, Padding = Padding.Empty, Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesSucursales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutBloquesSucursales.RowStyles.Add(new RowStyle(SizeType.Absolute, 360));
            layoutBloquesSucursales.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            layoutBloquesSucursales.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlFormulario.Height));
            layoutBloquesSucursales.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlAcciones.Height));
            layoutBloquesSucursales.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlCatalogo.Height));
            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesSucursales);
            pnlFormulario.Dock = DockStyle.Fill;
            pnlAcciones.Dock = DockStyle.Fill;
            pnlCatalogo.Dock = DockStyle.Fill;
            layoutBloquesSucursales.Controls.Add(dashboardCrud, 0, 0);
            dashboardCrud.Dock = DockStyle.Fill;
            layoutBloquesSucursales.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesSucursales.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesSucursales.Controls.Add(pnlCatalogo, 0, 4);
        }

        private void AplicarEstiloSucursal()
        {
            pnlHeader.RadioEsquinas = 12;
            pnlFormulario.RadioEsquinas = 12;
            pnlCatalogo.RadioEsquinas = 12;
            dgvSucursales.RadioEsquinas = 10;
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblFormularioTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCatalogoTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblColumnas.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            foreach (Label etiqueta in layoutCampos.Controls.OfType<Label>())
            {
                etiqueta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                etiqueta.ForeColor = Color.White;
            }
            foreach (Control campo in layoutCampos.Controls.Cast<Control>().Where(c =>
                c is TextBox || c is ComboBox || c is DateTimePicker))
            {
                campo.Font = new Font("Segoe UI", 9.5F);
                campo.BackColor = Color.FromArgb(225, 236, 246);
                campo.ForeColor = Color.FromArgb(7, 35, 66);
            }
            rdoActivo.ForeColor = Color.White;
            rdoInactivo.ForeColor = Color.White;
            dgvSucursales.BackgroundColor = ColorTranslator.FromHtml("#072342");
            dgvSucursales.GridColor = ColorTranslator.FromHtml("#14466E");
            dgvSucursales.BorderStyle = BorderStyle.None;
            dgvSucursales.EnableHeadersVisualStyles = false;
            dgvSucursales.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSucursales.ColumnHeadersHeight = 40;
            dgvSucursales.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070D2");
            dgvSucursales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSucursales.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvSucursales.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#072342");
            dgvSucursales.DefaultCellStyle.ForeColor = Color.FromArgb(235, 242, 249);
            dgvSucursales.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#114B77");
            dgvSucursales.DefaultCellStyle.SelectionForeColor = Color.FromArgb(235, 242, 249);
            dgvSucursales.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0A2C4E");
            dgvSucursales.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(235, 242, 249);
            dgvSucursales.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvSucursales.RowTemplate.Height = 32;
            btnBuscar.Width = btnNuevo.Width;
            foreach (Button boton in ObtenerBotonesDescendientes(this))
            {
                boton.EnabledChanged -= BotonSucursal_EnabledChanged;
                boton.EnabledChanged += BotonSucursal_EnabledChanged;
            }
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            Panel paginacion = Controls.Find("pnlPaginacion", true).OfType<Panel>().FirstOrDefault();
            if (paginacion != null)
            {
                Button anterior = paginacion.Controls.Find("btnPaginaAnterior", false).OfType<Button>().FirstOrDefault();
                Button siguiente = paginacion.Controls.Find("btnPaginaSiguiente", false).OfType<Button>().FirstOrDefault();
                if (anterior != null) { anterior.Width = btnNuevo.Width; anterior.Height = btnNuevo.Height; }
                if (siguiente != null) { siguiente.Width = btnNuevo.Width; siguiente.Height = btnNuevo.Height; }
                paginacion.Height = btnNuevo.Height + 16;
            }
        }

        private void ConfigurarResaltadoCampos()
        {
            Control[] campos = { txtCodigo, txtNombre, txtDireccion, cboDepartamento, cboMunicipio, txtTelefono,
                txtHorarioAtencion, txtCorreoElectronico, dtpFechaApertura };
            foreach (Control campo in campos)
            {
                CampoRedondeado.Aplicar(campo, 7);
                campo.MouseEnter += (s, e) =>
                {
                    if (campo.Enabled && !campo.Focused) campo.BackColor = Color.FromArgb(205, 243, 255);
                };
                campo.MouseLeave += (s, e) => RestaurarColorCampoSucursal(campo);
                campo.Enter += (s, e) =>
                {
                    if (campo.Enabled) campo.BackColor = ColorTranslator.FromHtml("#80DEFF");
                };
                campo.Leave += (s, e) => RestaurarColorCampoSucursal(campo);
                campo.EnabledChanged += (s, e) => RestaurarColorCampoSucursal(campo);
            }
        }

        private void RestaurarColorCampoSucursal(Control campo)
        {
            if (campo == null) return;
            campo.BackColor = campo.Enabled
                ? ColorTranslator.FromHtml("#E1ECF6") : Color.FromArgb(215, 222, 230);
            campo.ForeColor = campo.Enabled
                ? ColorTranslator.FromHtml("#072342") : Color.FromArgb(104, 116, 128);
        }

        private IEnumerable<Button> ObtenerBotonesDescendientes(Control raiz)
        {
            foreach (Control control in raiz.Controls)
            {
                if (control is Button boton) yield return boton;
                foreach (Button hijo in ObtenerBotonesDescendientes(control)) yield return hijo;
            }
        }

        private void BotonSucursal_EnabledChanged(object sender, EventArgs e)
        {
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
        }

        private void AjustarInterfazSucursales()
        {
            if (IsDisposed || layoutBloquesSucursales == null || pnlCatalogo.Width <= 0) return;
            OrganizarFiltrosColumnas();
            int ancho = Math.Min(195, Math.Max(148, (int)(ClientSize.Width * .18)));
            pnlMenu.Width = ancho;
            pnlHeader.SetBounds(ancho, 0, Math.Max(0, ClientSize.Width - ancho), 108);
            pnlContenido.SetBounds(ancho, 108, Math.Max(0, ClientSize.Width - ancho), Math.Max(0, ClientSize.Height - 108));
            int altoGrilla = dgvSucursales.ColumnHeadersHeight + 10 * dgvSucursales.RowTemplate.Height + 2;
            if (dgvSucursales.Height != altoGrilla) dgvSucursales.Height = altoGrilla;
            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height +
                pnlColumnas.Height + altoGrilla + (pnlCatalogo.Controls.Find("pnlPaginacion", false).Length > 0 ? 48 : lblRegistros.Height);
            pnlCatalogo.Height = altoCatalogo;
            layoutBloquesSucursales.RowStyles[4].Height = altoCatalogo;
            int altoFijo = pnlContenido.Padding.Vertical + pnlFormulario.Height + pnlAcciones.Height + altoCatalogo + 10;
            int altoDashboard = Math.Max(360, pnlContenido.ClientSize.Height - altoFijo);
            dashboardCrud.Height = altoDashboard;
            layoutBloquesSucursales.RowStyles[0].Height = altoDashboard;
            int altoTotal = altoDashboard + 10 + pnlFormulario.Height + pnlAcciones.Height + altoCatalogo;
            if (layoutBloquesSucursales.Height != altoTotal) layoutBloquesSucursales.Height = altoTotal;
        }

        private Panel pnlPaginacionSucursales()
        {
            return Controls.Find("pnlPaginacion", true).OfType<Panel>().FirstOrDefault();
        }

        private void AjustarPaginacionSucursales(Panel barra)
        {
            if (barra == null) return;
            Button anterior = barra.Controls.Find("btnPaginaAnterior", false).OfType<Button>().FirstOrDefault();
            Button siguiente = barra.Controls.Find("btnPaginaSiguiente", false).OfType<Button>().FirstOrDefault();
            Label pagina = barra.Controls.Find("lblPagina", false).OfType<Label>().FirstOrDefault();
            if (anterior == null || siguiente == null || pagina == null) return;
            int xSiguiente = barra.ClientSize.Width - 10 - siguiente.Width;
            siguiente.Location = new Point(xSiguiente, Math.Max(0, (barra.Height - siguiente.Height) / 2));
            pagina.Location = new Point(xSiguiente - pagina.Width - 8, Math.Max(0, (barra.Height - pagina.Height) / 2));
            anterior.Location = new Point(pagina.Left - anterior.Width - 8, Math.Max(0, (barra.Height - anterior.Height) / 2));
            Label registros = barra.Controls.Find("lblRegistros", false).OfType<Label>().FirstOrDefault();
            if (registros != null)
            {
                registros.Location = new Point(10, Math.Max(0, (barra.Height - 32) / 2));
                registros.Size = new Size(Math.Max(0, anterior.Left - 20), 32);
            }
        }

        private void ActualizarDashboardSucursales(List<SucursalesEntidad> lista)
        {
            lista = lista ?? new List<SucursalesEntidad>();
            List<SucursalesEntidad> activas = lista.Where(s => s.Estado).ToList();
            List<SucursalesEntidad> inactivas = lista.Where(s => !s.Estado).ToList();
            int departamentos = lista.Where(s => !string.IsNullOrWhiteSpace(s.Departamento))
                .Select(s => s.Departamento.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).Count();
            valoresDashboard[0].Text = lista.Count.ToString("N0");
            valoresDashboard[1].Text = activas.Count.ToString("N0");
            valoresDashboard[2].Text = inactivas.Count.ToString("N0");
            valoresDashboard[3].Text = departamentos.ToString("N0");
            int sinTelefono = lista.Count(s => string.IsNullOrWhiteSpace(s.Telefono));
            int sinCorreo = lista.Count(s => string.IsNullOrWhiteSpace(s.CorreoElectronico));
            lblAlertasDashboard.Text = sinTelefono == 0 && sinCorreo == 0
                ? "Todas las sucursales tienen datos de contacto."
                : "• Sucursales sin teléfono: " + sinTelefono + Environment.NewLine
                    + "• Sucursales sin correo: " + sinCorreo;
            List<SucursalesEntidad> recientes = lista.OrderByDescending(s => s.FechaApertura).Take(3).ToList();
            lblActividadDashboard.Text = recientes.Count == 0 ? "No hay sucursales registradas."
                : string.Join(Environment.NewLine, recientes.Select(s =>
                    s.FechaApertura.ToString("dd/MM", CultureInfo.GetCultureInfo("es-GT")) + "  " + s.Nombre));

            DateTime referencia = lista.Select(s => s.FechaApertura).DefaultIfEmpty(DateTime.Today).Max();
            DateTime primerMes = new DateTime(referencia.Year, referencia.Month, 1).AddMonths(-5);
            Series serie = graficaDashboard.Series["Aperturas"];
            serie.Points.Clear();
            for (int i = 0; i < 6; i++)
            {
                DateTime mes = primerMes.AddMonths(i);
                int cantidad = lista.Count(s => s.FechaApertura.Year == mes.Year && s.FechaApertura.Month == mes.Month);
                serie.Points.AddXY(mes.ToString("MMM", CultureInfo.GetCultureInfo("es-GT")), cantidad);
            }
        }

        private void ConstruirNavegacionLateral()
        {
            AjustesDisenoCrud.ConstruirBarraLateralModelo(this);
        }

        private void ConfigurarBotonLateral(IconButton boton, string texto, IconChar icono, bool volver)
        {
            boton.Text = texto;
            boton.Dock = DockStyle.Fill;
            boton.Margin = Padding.Empty;
            boton.Padding = new Padding(12, 0, 4, 0);
            boton.IconChar = icono;
            boton.IconFont = IconFont.Auto;
            boton.IconSize = 20;
            boton.IconColor = Color.White;
            boton.ImageAlign = ContentAlignment.MiddleLeft;
            boton.TextImageRelation = TextImageRelation.ImageBeforeText;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.FlatStyle = FlatStyle.Flat;
            boton.UseVisualStyleBackColor = false;
            boton.BackColor = ColorTranslator.FromHtml(volver ? "#08243A" : "#A83F48");
            boton.ForeColor = Color.White;
            boton.FlatAppearance.BorderColor = ColorTranslator.FromHtml(volver ? "#526779" : "#D56A70");
            boton.FlatAppearance.BorderSize = 1;
        }

        private static IconChar IconoParaMenu(string modulo)
        {
            switch (modulo)
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

        private void AplicarEstadoBotonesBarraLateral()
        {
            Color normal = ColorTranslator.FromHtml("#061D30");
            Color activo = ColorTranslator.FromHtml("#0B3558");
            Color cian = ColorTranslator.FromHtml("#00D9FF");
            foreach (IconButton boton in botonesBarraLateral)
            {
                bool seleccionado = string.Equals(Convert.ToString(boton.Tag), GetType().Name,
                    StringComparison.OrdinalIgnoreCase);
                boton.BackColor = seleccionado ? activo : normal;
                boton.ForeColor = seleccionado ? cian : Color.White;
                boton.IconColor = cian;
                boton.FlatAppearance.BorderSize = 0;
                boton.FlatAppearance.MouseOverBackColor = activo;
                boton.FlatAppearance.MouseDownBackColor = activo;
            }
        }

        private void MenuOpcion_MouseEnter(object sender, EventArgs e)
        {
            IconButton boton = sender as IconButton;
            if (boton == null) return;
            boton.BackColor = ColorTranslator.FromHtml("#0B3558");
            boton.ForeColor = ColorTranslator.FromHtml("#00D9FF");
            boton.IconColor = ColorTranslator.FromHtml("#00D9FF");
        }

        private void MenuOpcion_MouseLeave(object sender, EventArgs e) { AplicarEstadoBotonesBarraLateral(); }

        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => IniciarNuevo();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                LimpiarFormulario();
                BuscarSucursales();
            };
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarSucursales();
            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { BuscarSucursales(); e.SuppressKeyPress = true; }
            };
            dgvSucursales.SelectionChanged += DgvSucursales_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;
            cboDepartamento.SelectedIndexChanged += (s, e) => CargarMunicipios(cboDepartamento.SelectedItem as string, null);
        }

        private void ConfigurarNavegacionTeclado()
        {
            Control[] orden =
            {
                txtNombre, txtDireccion, cboDepartamento, cboMunicipio, txtTelefono,
                txtHorarioAtencion, txtCorreoElectronico, dtpFechaApertura, rdoActivo, rdoInactivo
            };
            foreach (Control control in orden)
                control.KeyDown += CampoSucursal_KeyDown;
            AcceptButton = null;
        }

        private void CampoSucursal_KeyDown(object sender, KeyEventArgs e)
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
                txtNombre, txtDireccion, cboDepartamento, cboMunicipio, txtTelefono,
                txtHorarioAtencion, txtCorreoElectronico, dtpFechaApertura, rdoActivo, rdoInactivo
            };
            int indice = Array.IndexOf(orden, actual);
            for (int i = indice + 1; i < orden.Length; i++)
            {
                if (!orden[i].Enabled || !orden[i].Visible) continue;
                orden[i].Focus();
                return;
            }
            // Enter no ejecuta Guardar; al llegar al último campo conserva el foco.
        }

        private bool ValidarCampoParaAvanzar(Control control)
        {
            if (control == txtNombre) return Requerido(txtNombre, 100, "Nombre");
            if (control == txtDireccion) return Requerido(txtDireccion, 200, "Dirección");
            if (control == cboDepartamento && cboDepartamento.SelectedIndex < 0)
            {
                MessageBox.Show("El campo Departamento es obligatorio.");
                cboDepartamento.Focus();
                return false;
            }
            if (control == cboMunicipio && string.IsNullOrWhiteSpace(cboMunicipio.SelectedItem as string))
            {
                MessageBox.Show("Selecciona un municipio para el departamento elegido.");
                cboMunicipio.Focus();
                return false;
            }
            if (control == txtTelefono) return Requerido(txtTelefono, 20, "Teléfono");
            if (control == txtHorarioAtencion) return Requerido(txtHorarioAtencion, 100, "Horario de atención");
            if (control == txtCorreoElectronico && txtCorreoElectronico.Text.Trim().Length > 100)
            {
                MessageBox.Show("El correo electrónico no puede superar 100 caracteres.");
                txtCorreoElectronico.Focus();
                return false;
            }
            return true;
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            IconButton opcion = sender as IconButton;
            if (opcion != null && opcion.Tag != null) Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoSucursal", chkCodigo.Checked);
            CambiarVisibilidad("Nombre", chkNombre.Checked);
            CambiarVisibilidad("Direccion", chkDireccion.Checked);
            CambiarVisibilidad("Municipio", chkMunicipio.Checked);
            CambiarVisibilidad("Departamento", chkDepartamento.Checked);
            CambiarVisibilidad("Telefono", chkTelefono.Checked);
            CambiarVisibilidad("HorarioAtencion", chkHorarioAtencion.Checked);
            CambiarVisibilidad("CorreoElectronico", chkCorreoElectronico.Checked);
            CambiarVisibilidad("FechaApertura", chkFechaApertura.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvSucursales.Columns.Contains(columna)) dgvSucursales.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvSucursales.Rows.Count > 0) SeleccionarFila(dgvSucursales.CurrentRow == null ? 0 : dgvSucursales.CurrentRow.Index);
            }
            else
            {
                dgvSucursales.ClearSelection();
                modoEdicion = false;
                EstablecerCamposHabilitados(false);
                ActualizarEstadoBotones(false);
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvSucursales.Rows.Count) return;
            dgvSucursales.ClearSelection();
            DataGridViewCell celda = dgvSucursales.Rows[indice].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
            if (celda != null) dgvSucursales.CurrentCell = celda;
            dgvSucursales.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!modoNuevo || !ValidarFormulario()) return;
            try
            {
                int codigo = negocio.Agregar(CrearEntidad());
                LimpiarFormulario();
                BuscarSucursales();
                MessageBox.Show("Sucursal agregada correctamente. Código: " + codigo, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al agregar sucursal", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            SucursalesEntidad seleccionado = ObtenerSeleccionado();
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
            if (!ValidarFormulario()) return;
            try
            {
                SucursalesEntidad cambios = CrearEntidad();
                cambios.CodigoSucursal = seleccionado.CodigoSucursal;
                negocio.Editar(cambios);
                LimpiarFormulario();
                BuscarSucursales();
                MessageBox.Show("Sucursal actualizada correctamente.");
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al editar sucursal", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            SucursalesEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null) { MessageBox.Show("Marca Seleccionar y elige una fila de la tabla."); return; }
            if (MessageBox.Show("¿Deseas marcar como inactiva la sucursal " + seleccionado.CodigoSucursal + "?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                negocio.Eliminar(seleccionado.CodigoSucursal);
                LimpiarFormulario();
                BuscarSucursales();
                MessageBox.Show("Sucursal marcada como inactiva.", "Eliminar sucursal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar sucursal", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private SucursalesEntidad CrearEntidad()
        {
            return new SucursalesEntidad(txtNombre.Text.Trim(), txtDireccion.Text.Trim(), cboMunicipio.SelectedItem as string,
                cboDepartamento.SelectedItem as string, txtTelefono.Text.Trim(), txtHorarioAtencion.Text.Trim(), ValorONulo(txtCorreoElectronico.Text),
                dtpFechaApertura.Value, rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!Requerido(txtNombre, 100, "Nombre")) return false;
            if (!Requerido(txtDireccion, 200, "Dirección")) return false;
            if (cboDepartamento.SelectedIndex < 0)
            {
                MessageBox.Show("El campo Departamento es obligatorio.");
                cboDepartamento.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cboMunicipio.SelectedItem as string))
            {
                MessageBox.Show("Selecciona un municipio para el departamento elegido.");
                cboMunicipio.Focus();
                return false;
            }
            if (!Requerido(txtTelefono, 20, "Teléfono")) return false;
            if (!Requerido(txtHorarioAtencion, 100, "Horario de atención")) return false;
            if (txtCorreoElectronico.Text.Trim().Length > 100)
            {
                MessageBox.Show("El correo electrónico no puede superar 100 caracteres.");
                txtCorreoElectronico.Focus();
                return false;
            }
            return true;
        }

        private bool Requerido(TextBox campo, int maximo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(campo.Text)) { MessageBox.Show("El campo " + nombre + " es obligatorio."); campo.Focus(); return false; }
            if (campo.Text.Trim().Length <= maximo) return true;
            MessageBox.Show("El campo " + nombre + " no puede superar " + maximo + " caracteres.");
            campo.Focus();
            return false;
        }

        private SucursalesEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvSucursales.CurrentRow == null) return null;
            return dgvSucursales.CurrentRow.DataBoundItem as SucursalesEntidad;
        }

        private void DgvSucursales_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked) return;
            SucursalesEntidad s = ObtenerSeleccionado();
            if (s == null) return;
            cargando = true;
            txtCodigo.Text = s.CodigoSucursal.ToString();
            txtNombre.Text = s.Nombre;
            txtDireccion.Text = s.Direccion;
            cboDepartamento.SelectedItem = s.Departamento;
            CargarMunicipios(s.Departamento, s.Municipio);
            txtTelefono.Text = s.Telefono;
            txtHorarioAtencion.Text = s.HorarioAtencion;
            txtCorreoElectronico.Text = s.CorreoElectronico ?? string.Empty;
            dtpFechaApertura.Value = LimitarFecha(dtpFechaApertura, s.FechaApertura);
            rdoActivo.Checked = s.Estado;
            rdoInactivo.Checked = !s.Estado;
            cargando = false;
            modoEdicion = false;
            EstablecerCamposHabilitados(false);
            ActualizarEstadoBotones(true);
        }

        private static DateTime LimitarFecha(DateTimePicker control, DateTime valor)
        {
            return valor < control.MinDate ? control.MinDate : (valor > control.MaxDate ? control.MaxDate : valor);
        }

        private static string ValorONulo(string valor) { return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim(); }

        private void BuscarSucursales()
        {
            string criterio = txtBuscar.Text.Trim();
            try
            {
                List<SucursalesEntidad> todos = negocio.Listar();
                sucursales.Clear();
                foreach (SucursalesEntidad sucursal in todos) sucursales.Add(sucursal);
                if (dashboardCrud != null) _ = dashboardCrud.CargarAsync();
                List<SucursalesEntidad> resultado = criterio.Length == 0 ? todos : negocio.Buscar(criterio);
                MostrarSucursales(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + sucursales.Count;
                AjustarInterfazSucursales();
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar sucursales", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void MostrarSucursales(List<SucursalesEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvSucursales.DataSource = origen;
            ActualizarColumnas();
            dgvSucursales.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;
            modoNuevo = false;
            modoEdicion = false;
            txtCodigo.Clear(); txtNombre.Clear(); txtDireccion.Clear();
            cboDepartamento.SelectedIndex = -1; CargarMunicipios(null, null);
            txtTelefono.Clear(); txtHorarioAtencion.Clear(); txtCorreoElectronico.Clear();
            dtpFechaApertura.Value = DateTime.Now; rdoActivo.Checked = true; rdoInactivo.Checked = false;
            dgvSucursales.ClearSelection(); chkSeleccionar.Checked = false;
            cargando = false;
            EstablecerCamposHabilitados(false);
            ActualizarEstadoBotones(false);
        }

        private void IniciarNuevo()
        {
            LimpiarFormulario();
            modoNuevo = true;
            EstablecerCamposHabilitados(true);
            ActualizarEstadoBotones(false);
            txtNombre.Focus();
        }

        private void CargarDepartamentos()
        {
            cboDepartamento.Items.Clear();
            cboDepartamento.Items.AddRange(UbicacionesGuatemala.ObtenerDepartamentos());
            cboDepartamento.SelectedIndex = -1;
            CargarMunicipios(null, null);
        }

        private void CargarMunicipios(string departamento, string municipioSeleccionado)
        {
            cboMunicipio.BeginUpdate();
            cboMunicipio.Items.Clear();
            if (!string.IsNullOrWhiteSpace(departamento))
                cboMunicipio.Items.AddRange(UbicacionesGuatemala.ObtenerMunicipios(departamento));
            cboMunicipio.SelectedIndex = -1;
            cboMunicipio.Enabled = cboDepartamento.SelectedIndex >= 0 && (modoNuevo || modoEdicion);
            if (!string.IsNullOrWhiteSpace(municipioSeleccionado))
                cboMunicipio.SelectedItem = municipioSeleccionado;
            cboMunicipio.EndUpdate();
        }

        private void EstablecerCamposHabilitados(bool habilitados)
        {
            txtNombre.Enabled = habilitados; txtDireccion.Enabled = habilitados;
            cboDepartamento.Enabled = habilitados;
            cboMunicipio.Enabled = habilitados && cboDepartamento.SelectedIndex >= 0;
            txtTelefono.Enabled = habilitados; txtHorarioAtencion.Enabled = habilitados;
            txtCorreoElectronico.Enabled = habilitados; dtpFechaApertura.Enabled = habilitados;
            rdoActivo.Enabled = habilitados; rdoInactivo.Enabled = habilitados;
        }

        private void ActualizarEstadoBotones(bool haySeleccion)
        {
            btnGuardar.Enabled = modoNuevo;
            btnEditar.Enabled = !modoNuevo && (modoEdicion || haySeleccion);
            btnEliminar.Enabled = !modoNuevo && !modoEdicion && haySeleccion;
        }
    }
}
