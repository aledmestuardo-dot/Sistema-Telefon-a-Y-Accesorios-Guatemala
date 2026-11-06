using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FontAwesome.Sharp;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmPlanes : Form
    {
        // La lista contiene los planes consultados; paginacion pertenece a Presentacion.
        private readonly BindingList<PlanesEntidad> planes = new BindingList<PlanesEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly PlanesNegocio negocio = new PlanesNegocio();
        private bool cargando = false;
        private bool modoNuevo = false;
        private bool modoEdicion = false;
        private readonly List<IconButton> botonesBarraLateralPlanes = new List<IconButton>();
        private bool actualizandoFiltros;
        private bool dashboardCargando;
        private ComboBox cboFiltroEstado;
        private CheckBox chkActivarFiltros;
        private TableLayoutPanel layoutOpcionesColumnas;
        private TableLayoutPanel layoutBloquesPlanes;
        private PanelRedondeado pnlDashboardPlanes;
        private Label[] valoresDashboardPlanes;
        private Label lblVigenciasDashboard;
        private Label lblBeneficiosDashboard;
        private Chart graficaDashboardPlanes;

        public FrmPlanes()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || (Site != null && Site.DesignMode)) return;
            ConfigurarDisenoAdaptable();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            origen.DataSource = planes;
            dgvPlanes.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvPlanes);
            ConfigurarAlturaNavegacionPlanes();
            ConfigurarFiltroBusqueda();
            ConfigurarOpcionesColumnas();
            ConfigurarDashboardPlanes();
            ConstruirOrdenBloques();
            AplicarEstiloPlanes();
            TemaVisualCrud.Aplicar(this);
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            AplicarEstiloBotonesNavegacionPlanes();
            ConfigurarResaltadoCampos();
            ConectarEventos();
            ConfigurarNavegacionTeclado();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarPlanes();
            AjustarInterfazPlanes();
            dgvPlanes.DataBindingComplete += (s, e) =>
            {
                foreach (DataGridViewColumn columna in dgvPlanes.Columns)
                    columna.MinimumWidth = 72;
                foreach (DataGridViewRow fila in dgvPlanes.Rows)
                    fila.Height = 32;
                AjustarInterfazPlanes();
            };
            Shown += async (s, e) => await CargarDashboardPlanesAsync();
            SizeChanged += (s, e) => AjustarInterfazPlanes();
        }

        private void ConfigurarDisenoAdaptable()
        {
            pnlHeader.Height = 108;
            pnlContenido.Padding = new Padding(14);
            pnlContenido.AutoScroll = true;
            pnlFormulario.Height = 260;
            pnlAcciones.Height = 58;
            lblTitulo.Height = 48;
            lblSubtitulo.Height = 42;
            lblTitulo.Text = "Gestión de planes";
            lblSubtitulo.Text = "Administra los planes, precios y beneficios incluidos";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleLeft;
            pnlFormulario.Dock = DockStyle.Top;
            pnlAcciones.Dock = DockStyle.Top;
            pnlCatalogo.Dock = DockStyle.Top;
            lblFormularioTitulo.Height = 40;
            lblFormularioTitulo.Text = "Información del plan";
            lblCatalogoTitulo.Height = 40;
            lblCatalogoTitulo.Text = "Planes registrados";
            lblColumnas.Height = 40;
            pnlBusqueda.Height = 54;
            pnlColumnas.Height = 76;
            selector.WrapContents = false;
            selector.AutoScroll = true;
            dgvPlanes.Dock = DockStyle.Top;
            dgvPlanes.RowTemplate.Height = 32;
            dgvPlanes.ColumnHeadersHeight = 40;
            dgvPlanes.ScrollBars = ScrollBars.Horizontal;
            pnlContenido.SizeChanged += (s, e) => AjustarInterfazPlanes();
            pnlCatalogo.SizeChanged += (s, e) => AjustarInterfazPlanes();
        }

        private void ConfigurarFiltroBusqueda()
        {
            cboFiltroEstado = Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault();
            if (cboFiltroEstado == null) return;
            Control host = cboFiltroEstado.Parent;
            if (host != null) host.Controls.Remove(cboFiltroEstado);
            if (btnBuscar.Parent != null) btnBuscar.Parent.Controls.Remove(btnBuscar);
            Panel filtroComun = pnlBusqueda.Controls.Find("pnlFiltroEstadoComun", false).OfType<Panel>().FirstOrDefault();
            if (filtroComun != null)
            {
                pnlBusqueda.Controls.Remove(filtroComun);
                filtroComun.Dispose();
            }
            pnlBusqueda.Controls.Remove(txtBuscar);
            pnlBusqueda.Controls.Remove(btnBuscar);
            pnlBusqueda.Padding = new Padding(8, 4, 8, 4);
            TableLayoutPanel busqueda = new TableLayoutPanel
            {
                Name = "layoutBusquedaPlanes", Dock = DockStyle.Fill, ColumnCount = 3,
                RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlBusqueda.BackColor
            };
            busqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            busqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            busqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, btnNuevo.Width));
            busqueda.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            cboFiltroEstado.Dock = DockStyle.Fill;
            cboFiltroEstado.Margin = new Padding(0, 0, 8, 0);
            cboFiltroEstado.Font = new Font("Segoe UI", 9.5F);
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Margin = new Padding(0, 0, 8, 0);
            btnBuscar.Dock = DockStyle.Fill;
            btnBuscar.Width = btnNuevo.Width;
            btnBuscar.Margin = Padding.Empty;
            busqueda.Controls.Add(cboFiltroEstado, 0, 0);
            busqueda.Controls.Add(txtBuscar, 1, 0);
            busqueda.Controls.Add(btnBuscar, 2, 0);
            pnlBusqueda.Controls.Add(busqueda);
        }

        private void ConfigurarOpcionesColumnas()
        {
            chkSeleccionar.Text = "Seleccionar registro";
            chkSeleccionar.AutoSize = true;
            chkSeleccionar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkSeleccionar.Margin = new Padding(8, 4, 4, 0);
            selector.Controls.Remove(chkSeleccionar);
            chkActivarFiltros = new CheckBox
            {
                Name = "chkActivarTodosFiltros", Text = "Activar todos los filtros",
                AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.White,
                BackColor = Color.Transparent, Font = chkSeleccionar.Font,
                Margin = new Padding(4, 4, 10, 0)
            };
            int anchoActivar = TextRenderer.MeasureText(chkActivarFiltros.Text, chkActivarFiltros.Font).Width
                + 28 + chkActivarFiltros.Margin.Horizontal + 12;
            int anchoSeleccionar = TextRenderer.MeasureText(chkSeleccionar.Text, chkSeleccionar.Font).Width
                + 28 + chkSeleccionar.Margin.Horizontal + 12;
            layoutOpcionesColumnas = new TableLayoutPanel
            {
                Name = "layoutOpcionesColumnasPlanes", Dock = DockStyle.Fill,
                ColumnCount = 3, RowCount = 2, Margin = Padding.Empty,
                Padding = Padding.Empty, BackColor = pnlColumnas.BackColor
            };
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoActivar));
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutOpcionesColumnas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoSeleccionar));
            layoutOpcionesColumnas.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layoutOpcionesColumnas.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            lblColumnas.Dock = DockStyle.Fill;
            layoutOpcionesColumnas.Controls.Add(lblColumnas, 0, 0);
            layoutOpcionesColumnas.SetColumnSpan(lblColumnas, 3);
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

            chkActivarFiltros.CheckedChanged += (s, e) =>
            {
                if (actualizandoFiltros) return;
                actualizandoFiltros = true;
                foreach (CheckBox filtro in ObtenerFiltrosColumnas()) filtro.Checked = chkActivarFiltros.Checked;
                actualizandoFiltros = false;
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
        }

        private IEnumerable<CheckBox> ObtenerFiltrosColumnas()
        {
            return selector.Controls.OfType<CheckBox>().Where(c => c != chkSeleccionar);
        }

        private void AjustarAnchosOpcionesColumnas()
        {
            if (layoutOpcionesColumnas == null || chkActivarFiltros == null || chkSeleccionar == null ||
                layoutOpcionesColumnas.ColumnStyles.Count < 3) return;

            float anchoActivar = TextRenderer.MeasureText(chkActivarFiltros.Text, chkActivarFiltros.Font).Width
                + 28 + chkActivarFiltros.Margin.Horizontal + 12;
            float anchoSeleccionar = TextRenderer.MeasureText(chkSeleccionar.Text, chkSeleccionar.Font).Width
                + 28 + chkSeleccionar.Margin.Horizontal + 12;
            layoutOpcionesColumnas.ColumnStyles[0].Width = anchoActivar;
            layoutOpcionesColumnas.ColumnStyles[2].Width = anchoSeleccionar;
        }

        private void SincronizarActivarFiltros()
        {
            if (chkActivarFiltros == null) return;
            bool anterior = actualizandoFiltros;
            actualizandoFiltros = true;
            chkActivarFiltros.Checked = ObtenerFiltrosColumnas().All(c => c.Checked);
            actualizandoFiltros = anterior;
        }

        private void ConfigurarDashboardPlanes()
        {
            Color fondo = ColorTranslator.FromHtml("#08243A");
            Color superficie = ColorTranslator.FromHtml("#0B3558");
            pnlDashboardPlanes = new PanelRedondeado
            {
                Name = "pnlDashboardPlanes", Dock = DockStyle.Fill, Height = 360,
                Padding = new Padding(12), BackColor = fondo, Margin = Padding.Empty
            };
            pnlDashboardPlanes.RadioEsquinas = 12;
            TableLayoutPanel raiz = new TableLayoutPanel
            {
                Name = "layoutDashboardPlanes", Dock = DockStyle.Fill, ColumnCount = 1,
                RowCount = 3, Padding = Padding.Empty, Margin = Padding.Empty, BackColor = fondo
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
                Text = "Resumen de planes", Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);
            Button actualizar = new Button
            {
                Name = "btnActualizarDashboardPlanes", Text = "↻ Actualizar", Dock = DockStyle.Fill,
                Margin = new Padding(4, 1, 0, 1), FlatStyle = FlatStyle.Flat,
                BackColor = ColorTranslator.FromHtml("#0B6E91"), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand
            };
            actualizar.FlatAppearance.BorderSize = 0;
            actualizar.Click += async (s, e) => await CargarDashboardPlanesAsync();
            encabezado.Controls.Add(actualizar, 1, 0);
            raiz.Controls.Add(encabezado, 0, 0);

            string[] titulos = { "PLANES REGISTRADOS", "PLANES ACTIVOS", "PLANES INACTIVOS", "COSTO PROMEDIO MENSUAL" };
            Color[] acentos = { ColorTranslator.FromHtml("#3B9EFF"), ColorTranslator.FromHtml("#35C98B"),
                ColorTranslator.FromHtml("#E7B84B"), ColorTranslator.FromHtml("#8B72E8") };
            valoresDashboardPlanes = new Label[4];
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
                valoresDashboardPlanes[i] = new Label
                {
                    Text = "—", Dock = DockStyle.Fill,
                    ForeColor = i == 0 ? ColorTranslator.FromHtml("#00D9FF") : Color.White,
                    Font = new Font("Segoe UI", 18F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft,
                    AutoEllipsis = true
                };
                tarjeta.Controls.Add(valoresDashboardPlanes[i]);
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
            Panel grafica = CrearPanelGraficaPlanes(superficie);
            detalle.Controls.Add(grafica, 0, 0);
            TableLayoutPanel derecha = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                Padding = Padding.Empty, Margin = new Padding(8, 0, 0, 0), BackColor = fondo
            };
            derecha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            derecha.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            derecha.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            Panel vigencias = CrearPanelDashboardPlanes("Planes por vigencia", out lblVigenciasDashboard, superficie);
            Panel beneficios = CrearPanelDashboardPlanes("Beneficios destacados", out lblBeneficiosDashboard, superficie);
            vigencias.Margin = new Padding(0, 0, 0, 5);
            beneficios.Margin = new Padding(0, 5, 0, 0);
            derecha.Controls.Add(vigencias, 0, 0);
            derecha.Controls.Add(beneficios, 0, 1);
            detalle.Controls.Add(derecha, 1, 0);
            raiz.Controls.Add(detalle, 0, 2);
            pnlDashboardPlanes.Controls.Add(raiz);
        }

        private Panel CrearPanelGraficaPlanes(Color superficie)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(7, 5, 7, 5),
                BackColor = superficie, Margin = Padding.Empty
            };
            panel.Controls.Add(new Label
            {
                Text = "Costo mensual por plan", Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true
            });
            graficaDashboardPlanes = new Chart
            {
                Dock = DockStyle.Fill, BackColor = superficie, Palette = ChartColorPalette.None
            };
            ChartArea area = new ChartArea("CostoMensual") { BackColor = superficie };
            area.AxisX.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisX.LabelStyle.Angle = -35;
            area.AxisX.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Format = "C0";
            area.AxisY.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#34495E");
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            graficaDashboardPlanes.ChartAreas.Add(area);
            graficaDashboardPlanes.Series.Add(new Series("Costo")
            {
                ChartArea = "CostoMensual", ChartType = SeriesChartType.Column,
                Color = ColorTranslator.FromHtml("#00D9FF"), IsXValueIndexed = true
            });
            panel.Controls.Add(graficaDashboardPlanes);
            return panel;
        }

        private Panel CrearPanelDashboardPlanes(string titulo, out Label contenido, Color superficie)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(9, 6, 9, 6),
                BackColor = superficie, Margin = Padding.Empty
            };
            panel.Controls.Add(new Label
            {
                Text = titulo, Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true
            });
            contenido = new Label
            {
                Dock = DockStyle.Fill, ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F), AutoEllipsis = true,
                TextAlign = ContentAlignment.TopLeft
            };
            panel.Controls.Add(contenido);
            return panel;
        }

        private async Task CargarDashboardPlanesAsync()
        {
            if (dashboardCargando || IsDisposed || pnlDashboardPlanes == null) return;
            dashboardCargando = true;
            try
            {
                List<PlanesEntidad> datos = await Task.Run(() => negocio.Listar());
                int activos = datos.Count(p => p.Estado);
                valoresDashboardPlanes[0].Text = datos.Count.ToString("N0");
                valoresDashboardPlanes[1].Text = activos.ToString("N0");
                valoresDashboardPlanes[2].Text = (datos.Count - activos).ToString("N0");
                valoresDashboardPlanes[3].Text = datos.Count == 0 ? "—" :
                    datos.Average(p => p.CostoMensual).ToString("C2", CultureInfo.GetCultureInfo("es-GT"));
                Series serie = graficaDashboardPlanes.Series["Costo"];
                serie.Points.Clear();
                foreach (PlanesEntidad plan in datos.OrderBy(p => p.CostoMensual).Take(8))
                {
                    int punto = serie.Points.AddY((double)plan.CostoMensual);
                    serie.Points[punto].AxisLabel = plan.Nombre;
                    serie.Points[punto].ToolTip = plan.Nombre + ": " + plan.CostoMensual.ToString("C2", CultureInfo.GetCultureInfo("es-GT"));
                }
                lblVigenciasDashboard.Text = datos.Count == 0 ? "Aún no hay planes registrados." : string.Join("\n",
                    datos.GroupBy(p => p.VigenciaMeses).OrderBy(g => g.Key).Take(4)
                        .Select(g => g.Key + (g.Key == 1 ? " mes · " : " meses · ") + g.Count() + " planes"));
                lblBeneficiosDashboard.Text = datos.Count == 0 ? "Los beneficios aparecerán al registrar planes." : string.Join("\n",
                    datos.OrderByDescending(p => p.CantidadDatos).ThenByDescending(p => p.MinutosIncluidos).Take(4)
                        .Select(p => p.Nombre + " · " + p.CantidadDatos.ToString("N0") + " MB · " + p.MinutosIncluidos.ToString("N0") + " min"));
            }
            catch (Exception)
            {
                foreach (Label valor in valoresDashboardPlanes) valor.Text = "—";
                lblVigenciasDashboard.Text = "No se pudo cargar el resumen. Comprueba la conexión de datos.";
                lblBeneficiosDashboard.Text = "Los beneficios no están disponibles.";
            }
            finally { dashboardCargando = false; }
        }

        private void ConstruirOrdenBloques()
        {
            layoutBloquesPlanes = new TableLayoutPanel
            {
                Name = "layoutBloquesPlanes", Dock = DockStyle.Top, ColumnCount = 1,
                RowCount = 5, Padding = Padding.Empty, Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesPlanes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutBloquesPlanes.RowStyles.Add(new RowStyle(SizeType.Absolute, 360));
            layoutBloquesPlanes.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            layoutBloquesPlanes.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlFormulario.Height));
            layoutBloquesPlanes.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlAcciones.Height));
            layoutBloquesPlanes.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlCatalogo.Height));
            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesPlanes);
            pnlDashboardPlanes.Dock = DockStyle.Fill;
            pnlFormulario.Dock = DockStyle.Fill;
            pnlAcciones.Dock = DockStyle.Fill;
            pnlCatalogo.Dock = DockStyle.Fill;
            layoutBloquesPlanes.Controls.Add(pnlDashboardPlanes, 0, 0);
            layoutBloquesPlanes.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesPlanes.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesPlanes.Controls.Add(pnlCatalogo, 0, 4);
        }

        private void AplicarEstiloPlanes()
        {
            RedondearPanel(pnlHeader, 12);
            RedondearPanel(pnlFormulario, 12);
            RedondearPanel(pnlCatalogo, 12);
            RedondearGrilla(dgvPlanes, 10);
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
            foreach (Control campo in new Control[] { txtCodigo, txtNombre, txtDescripcion, nudCostoMensual,
                nudCantidadDatos, nudMinutosIncluidos, nudVigenciaMeses })
            {
                campo.Font = new Font("Segoe UI", 9.5F);
                campo.BackColor = ColorTranslator.FromHtml("#E1ECF6");
                campo.ForeColor = ColorTranslator.FromHtml("#072342");
            }
            panelEstado.BackColor = ColorTranslator.FromHtml("#072342");
            foreach (RadioButtonTextoBlanco opcion in new[] { rdoActivo, rdoInactivo })
            {
                opcion.Font = new Font("Segoe UI", 9.5F);
                opcion.BackColor = panelEstado.BackColor;
                opcion.ForeColor = Color.White;
            }
            dgvPlanes.BackgroundColor = ColorTranslator.FromHtml("#072342");
            dgvPlanes.GridColor = ColorTranslator.FromHtml("#14466E");
            dgvPlanes.BorderStyle = BorderStyle.None;
            dgvPlanes.EnableHeadersVisualStyles = false;
            dgvPlanes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPlanes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPlanes.ColumnHeadersHeight = 40;
            dgvPlanes.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070D2");
            dgvPlanes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPlanes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvPlanes.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#072342");
            dgvPlanes.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#EBF2F9");
            dgvPlanes.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#114B77");
            dgvPlanes.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#EBF2F9");
            dgvPlanes.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0A2C4E");
            dgvPlanes.AlternatingRowsDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#EBF2F9");
            dgvPlanes.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvPlanes.RowTemplate.Height = 32;
            foreach (DataGridViewColumn columna in dgvPlanes.Columns) columna.MinimumWidth = 72;
            foreach (DataGridViewRow fila in dgvPlanes.Rows) fila.Height = 32;
            dgvPlanes.ScrollBars = ScrollBars.Horizontal;
            btnBuscar.Width = btnNuevo.Width;
            Panel paginacion = Controls.Find("pnlPaginacion", true).OfType<Panel>().FirstOrDefault();
            if (paginacion != null)
            {
                Button anterior = paginacion.Controls.Find("btnPaginaAnterior", false).OfType<Button>().FirstOrDefault();
                Button siguiente = paginacion.Controls.Find("btnPaginaSiguiente", false).OfType<Button>().FirstOrDefault();
                if (anterior != null) { anterior.Width = btnNuevo.Width; anterior.Height = btnNuevo.Height; }
                if (siguiente != null) { siguiente.Width = btnNuevo.Width; siguiente.Height = btnNuevo.Height; }
                paginacion.Height = btnNuevo.Height + 16;
            }
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
        }

        private void RedondearPanel(Control panel, int radio)
        {
            PanelRedondeado redondeado = panel as PanelRedondeado;
            if (redondeado != null) redondeado.RadioEsquinas = radio;
        }

        private void RedondearGrilla(DataGridView tabla, int radio)
        {
            DataGridViewRedondeado redondeado = tabla as DataGridViewRedondeado;
            if (redondeado != null) redondeado.RadioEsquinas = radio;
        }

        private void ConfigurarResaltadoCampos()
        {
            Control[] campos = { txtCodigo, txtNombre, txtDescripcion, nudCostoMensual,
                nudCantidadDatos, nudMinutosIncluidos, nudVigenciaMeses };
            foreach (Control campo in campos)
            {
                CampoRedondeado.Aplicar(campo, 7);
                campo.MouseEnter += (s, e) =>
                {
                    if (campo.Enabled && !campo.Focused) campo.BackColor = Color.FromArgb(205, 243, 255);
                };
                campo.MouseLeave += (s, e) => RestaurarColorCampo(campo);
                campo.Enter += (s, e) =>
                {
                    if (campo.Enabled) campo.BackColor = ColorTranslator.FromHtml("#80DEFF");
                };
                campo.Leave += (s, e) => RestaurarColorCampo(campo);
                campo.EnabledChanged += (s, e) => RestaurarColorCampo(campo);
            }
        }

        private void RestaurarColorCampo(Control campo)
        {
            if (campo == null) return;
            campo.BackColor = campo.Enabled ? ColorTranslator.FromHtml("#E1ECF6") : Color.FromArgb(215, 222, 230);
            campo.ForeColor = campo.Enabled ? ColorTranslator.FromHtml("#072342") : Color.FromArgb(104, 116, 128);
        }

        private void ConfigurarNavegacionTeclado()
        {
            Control[] orden = { txtNombre, txtDescripcion, nudCostoMensual, nudCantidadDatos,
                nudMinutosIncluidos, nudVigenciaMeses, rdoActivo, rdoInactivo };
            foreach (Control control in orden) control.KeyDown += CampoPlan_KeyDown;
            AcceptButton = null;
        }

        private void CampoPlan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Alt || e.Control || e.Shift) return;
            Control actual = sender as Control;
            if (actual == null) return;
            if (!ValidarCampoParaAvanzar(actual)) return;
            e.SuppressKeyPress = true;
            e.Handled = true;
            Control[] orden = { txtNombre, txtDescripcion, nudCostoMensual, nudCantidadDatos,
                nudMinutosIncluidos, nudVigenciaMeses, rdoActivo, rdoInactivo };
            int indice = Array.IndexOf(orden, actual);
            for (int i = indice + 1; i < orden.Length; i++)
            {
                if (!orden[i].Enabled || !orden[i].Visible) continue;
                orden[i].Focus();
                return;
            }
        }

        private bool ValidarCampoParaAvanzar(Control campo)
        {
            if (campo == txtNombre && string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.");
                txtNombre.Focus();
                return false;
            }
            if (campo == txtDescripcion && string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("El campo Descripción es obligatorio.");
                txtDescripcion.Focus();
                return false;
            }
            if (campo == nudCostoMensual && nudCostoMensual.Value <= 0)
            {
                MessageBox.Show("El campo Costo mensual debe ser mayor que cero.");
                nudCostoMensual.Focus();
                return false;
            }
            if (campo == nudCantidadDatos && nudCantidadDatos.Value <= 0)
            {
                MessageBox.Show("El campo Datos (MB) debe ser mayor que cero.");
                nudCantidadDatos.Focus();
                return false;
            }
            if (campo == nudVigenciaMeses && nudVigenciaMeses.Value <= 0)
            {
                MessageBox.Show("El campo Vigencia (meses) debe ser mayor que cero.");
                nudVigenciaMeses.Focus();
                return false;
            }
            return true;
        }

        private void AjustarInterfazPlanes()
        {
            if (IsDisposed || layoutBloquesPlanes == null || pnlCatalogo.Width <= 0) return;
            AjustarAnchosOpcionesColumnas();
            int filasReservadas = 10;
            int altoGrilla = dgvPlanes.ColumnHeadersHeight + filasReservadas * dgvPlanes.RowTemplate.Height + 2;
            if (dgvPlanes.Height != altoGrilla) dgvPlanes.Height = altoGrilla;
            Panel paginacion = pnlCatalogo.Controls.Find("pnlPaginacion", false).OfType<Panel>().FirstOrDefault();
            int altoPaginacion = paginacion == null ? 0 : btnNuevo.Height + 16;
            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height +
                pnlColumnas.Height + altoGrilla + altoPaginacion;
            pnlCatalogo.Height = altoCatalogo;
            layoutBloquesPlanes.RowStyles[4].Height = altoCatalogo;
            int altoFijo = pnlContenido.Padding.Vertical + pnlFormulario.Height + pnlAcciones.Height + altoCatalogo + 10;
            int altoDashboard = Math.Max(360, pnlContenido.ClientSize.Height - altoFijo);
            pnlDashboardPlanes.Height = altoDashboard;
            layoutBloquesPlanes.RowStyles[0].Height = altoDashboard;
            int altoTotal = altoDashboard + 10 + pnlFormulario.Height + pnlAcciones.Height + altoCatalogo;
            if (layoutBloquesPlanes.Height != altoTotal) layoutBloquesPlanes.Height = altoTotal;
            if (paginacion != null) AjustarPaginacion(paginacion);
        }

        private void AjustarPaginacion(Panel barra)
        {
            Button anterior = barra.Controls.Find("btnPaginaAnterior", false).OfType<Button>().FirstOrDefault();
            Button siguiente = barra.Controls.Find("btnPaginaSiguiente", false).OfType<Button>().FirstOrDefault();
            Label pagina = barra.Controls.Find("lblPagina", false).OfType<Label>().FirstOrDefault();
            Label registros = barra.Controls.Find("lblRegistros", false).OfType<Label>().FirstOrDefault();
            if (anterior == null || siguiente == null || pagina == null) return;
            barra.BackColor = ColorTranslator.FromHtml("#072342");
            pagina.Width = 132;
            pagina.ForeColor = Color.White;
            pagina.Font = new Font("Segoe UI", 9.5F);
            if (registros != null)
            {
                registros.ForeColor = Color.White;
                registros.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
            int y = Math.Max(0, (barra.Height - anterior.Height) / 2);
            siguiente.Location = new Point(barra.ClientSize.Width - 10 - siguiente.Width, y);
            pagina.Location = new Point(siguiente.Left - pagina.Width - 8, y);
            anterior.Location = new Point(pagina.Left - anterior.Width - 8, y);
            if (registros != null)
            {
                registros.Location = new Point(10, y);
                registros.Size = new Size(Math.Max(0, anterior.Left - 20), anterior.Height);
            }
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            AplicarEstiloBotonesNavegacionPlanes();
        }

        private void ConfigurarAlturaNavegacionPlanes()
        {
            pnlLogo.Height = 142;
            Action ajustarAnchoLateral = () =>
            {
                int ancho = Math.Max(148, Math.Min(195, (int)(ClientSize.Width * 0.18)));
                if (pnlMenu.Width != ancho) pnlMenu.Width = ancho;
            };
            SizeChanged += (s, e) => ajustarAnchoLateral();
            ajustarAnchoLateral();
            pnlMenu.Padding = new Padding(8);
            pnlLogo.BackColor = ColorTranslator.FromHtml("#032B53");
            lblLogo.Text = string.Empty;
            lblSistema.Text = "Sistema de Telefonía\ny Accesorios";
            lblSistema.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pnlLogo.Controls.Remove(lblLogo);
            IconPictureBox iconoMarca = pnlLogo.Controls.OfType<IconPictureBox>()
                .FirstOrDefault(i => i.Name == "icoMarcaTelefonia");
            if (iconoMarca == null)
            {
                iconoMarca = new IconPictureBox
                {
                    Name = "icoMarcaTelefonia", IconChar = IconChar.MobileAlt,
                    IconFont = IconFont.Auto, IconColor = Color.FromArgb(0, 203, 255),
                    BackColor = Color.Transparent, SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(42, 42), Anchor = AnchorStyles.Top
                };
                pnlLogo.Controls.Add(iconoMarca);
            }
            iconoMarca.Location = new Point((pnlLogo.ClientSize.Width - 42) / 2, 8);
            iconoMarca.BringToFront();
            lblSistema.Dock = DockStyle.None;
            lblSistema.SetBounds(6, 52, pnlLogo.ClientSize.Width - 12, 82);
            lblSistema.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlLogo.SizeChanged += (s, e) =>
            {
                iconoMarca.Left = (pnlLogo.ClientSize.Width - iconoMarca.Width) / 2;
                lblSistema.SetBounds(6, 52, pnlLogo.ClientSize.Width - 12, 82);
            };
            lblUsuarioRol.Height = 24;
            lblUsuarioRol.Name = "lblRol";
            lblUsuarioRol.Text = UsuariosNegocio.UsuarioActual == null ||
                string.IsNullOrWhiteSpace(UsuariosNegocio.UsuarioActual.TipoUsuario)
                ? "Administrador" : UsuariosNegocio.UsuarioActual.TipoUsuario;
            lblUsuarioRol.Font = new Font("Segoe UI", 9.5F);
            lblUsuario.Text = "Usuario del sistema";
            lblUsuario.Name = "lblDescripcionRol";
            lblUsuario.Height = 24;
            lblUsuario.Font = new Font("Segoe UI", 9F);
            btnVolverMenu.Text = "Volver al menú principal";
            btnVolverMenu.IconChar = IconChar.Home;
            btnVolverMenu.IconSize = 20;
            btnVolverMenu.IconColor = Color.White;
            btnVolverMenu.ImageAlign = ContentAlignment.MiddleLeft;
            btnVolverMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVolverMenu.TextAlign = ContentAlignment.MiddleLeft;
            btnVolverMenu.Padding = new Padding(12, 0, 4, 0);
            btnVolverMenu.BackColor = ColorTranslator.FromHtml("#08243A");
            btnVolverMenu.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#526779");
            btnVolverMenu.FlatAppearance.BorderSize = 1;
            btnCerrarSesion.IconChar = IconChar.SignOutAlt;
            btnCerrarSesion.IconSize = 20;
            btnCerrarSesion.IconColor = Color.White;
            btnCerrarSesion.ImageAlign = ContentAlignment.MiddleLeft;
            btnCerrarSesion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCerrarSesion.TextAlign = ContentAlignment.MiddleLeft;
            btnCerrarSesion.Padding = new Padding(12, 0, 4, 0);
            btnCerrarSesion.BackColor = ColorTranslator.FromHtml("#A83F48");
            btnCerrarSesion.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#D56A70");
            btnCerrarSesion.FlatAppearance.BorderSize = 1;

            pnlMenu.SuspendLayout();
            pnlMenu.Controls.Remove(pnlItemsMenu);
            pnlMenu.Controls.Remove(btnVolverMenu);
            pnlMenu.Controls.Remove(btnCerrarSesion);
            pnlMenu.Controls.Remove(lblUsuario);
            pnlMenu.Controls.Remove(lblUsuarioRol);
            pnlMenu.Controls.Remove(pnlLogo);

            TableLayoutPanel layoutBarraLateral = new TableLayoutPanel
            {
                Name = "layoutBarraLateralPlanes", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 3, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = ColorTranslator.FromHtml("#061D30")
            };
            layoutBarraLateral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutBarraLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, 142));
            layoutBarraLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            layoutBarraLateral.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            TableLayoutPanel identidad = new TableLayoutPanel
            {
                Name = "layoutIdentidadPlanes", Dock = DockStyle.Fill, ColumnCount = 1,
                RowCount = 2, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = ColorTranslator.FromHtml("#061D30")
            };
            identidad.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            identidad.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            identidad.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            lblUsuarioRol.Dock = DockStyle.Fill;
            lblUsuarioRol.ForeColor = Color.White;
            lblUsuarioRol.TextAlign = ContentAlignment.MiddleCenter;
            lblUsuario.Dock = DockStyle.Fill;
            lblUsuario.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            lblUsuario.TextAlign = ContentAlignment.MiddleCenter;
            identidad.Controls.Add(lblUsuarioRol, 0, 0);
            identidad.Controls.Add(lblUsuario, 0, 1);

            pnlLogo.Dock = DockStyle.Fill;
            pnlLogo.BackColor = ColorTranslator.FromHtml("#032B53");
            pnlItemsMenu.Dock = DockStyle.Fill;
            pnlItemsMenu.AutoScroll = false;
            pnlItemsMenu.BackColor = ColorTranslator.FromHtml("#061D30");

            Dictionary<string, Label> etiquetasMenu = pnlItemsMenu.Controls.OfType<Label>()
                .Where(l => l.Tag != null)
                .ToDictionary(l => Convert.ToString(l.Tag), l => l, StringComparer.OrdinalIgnoreCase);
            string[] destinos = { "FrmUsuarios", "FrmSucursales", "FrmEmpleados", "FrmClientes",
                "FrmPlanes", "FrmLineas", "FrmProveedores", "FrmMarcas", "FrmEquipos", "FrmCompras",
                "FrmInventario", "FrmVentas", "FrmFacturacion", "FrmPagos", "FrmCardex", "FrmReclamos" };
            TableLayoutPanel layoutBotonesMenu = new TableLayoutPanel
            {
                Name = "layoutBotonesBarraLateral", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 18, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = ColorTranslator.FromHtml("#061D30"),
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBotonesMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 18; i++)
                layoutBotonesMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 18F));

            botonesBarraLateralPlanes.Clear();
            ConfigurarBotonNavegacion(btnVolverMenu, "Volver al menú principal", IconChar.Home,
                Color.White, ColorTranslator.FromHtml("#08243A"), true);
            btnVolverMenu.Dock = DockStyle.Fill;
            btnVolverMenu.Margin = Padding.Empty;
            btnVolverMenu.Click += (s, e) => Navegacion.VolverAlMenu(this);
            btnVolverMenu.MouseEnter += (s, e) =>
            {
                if (btnVolverMenu.Enabled) btnVolverMenu.BackColor = Color.FromArgb(0, 86, 132);
            };
            btnVolverMenu.MouseLeave += (s, e) => AplicarEstiloBotonesNavegacionPlanes();
            btnCerrarSesion.Click += (s, e) => Navegacion.CerrarSesion(this);
            btnCerrarSesion.MouseEnter += (s, e) =>
            {
                if (btnCerrarSesion.Enabled) btnCerrarSesion.BackColor = Color.FromArgb(185, 67, 74);
            };
            btnCerrarSesion.MouseLeave += (s, e) => AplicarEstiloBotonesNavegacionPlanes();
            ConfigurarBotonNavegacion(btnCerrarSesion, "Cerrar sesión", IconChar.SignOutAlt,
                Color.White, ColorTranslator.FromHtml("#A83F48"), true);
            btnCerrarSesion.Dock = DockStyle.Fill;
            btnCerrarSesion.Margin = Padding.Empty;
            layoutBotonesMenu.Controls.Add(btnVolverMenu, 0, 0);
            string[] nombresBotones = { "btnUsuarios", "btnSucursales", "btnEmpleados", "btnClientes",
                "btnPlanes", "btnLineas", "btnProveedores", "btnMarcas", "btnEquipos", "btnCompras",
                "btnInventario", "btnVentas", "btnFacturacion", "btnPagos", "btnCardex", "btnReclamos" };
            for (int i = 0; i < destinos.Length; i++)
            {
                Label opcion;
                if (!etiquetasMenu.TryGetValue(destinos[i], out opcion)) continue;

                string texto = (opcion.Text ?? string.Empty).Trim().TrimStart('›', '⌂').Trim();
                IconButton boton = new IconButton
                {
                    Name = nombresBotones[i], Text = texto, Tag = destinos[i],
                    Dock = DockStyle.Fill, Margin = Padding.Empty, Padding = new Padding(12, 0, 4, 0),
                    TextAlign = ContentAlignment.MiddleLeft, ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    IconChar = IconoParaMenu(destinos[i].Substring(3)), IconFont = IconFont.Auto,
                    IconColor = ColorTranslator.FromHtml("#00D9FF"), IconSize = 20,
                    BackColor = ColorTranslator.FromHtml("#061D30"), ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F),
                    Cursor = Cursors.Hand, UseVisualStyleBackColor = false
                };
                boton.FlatAppearance.BorderSize = 0;
                boton.Click += MenuOpcion_Click;
                boton.MouseEnter += MenuOpcion_MouseEnter;
                boton.MouseLeave += MenuOpcion_MouseLeave;
                botonesBarraLateralPlanes.Add(boton);
                layoutBotonesMenu.Controls.Add(boton, 0, i + 1);
            }
            layoutBotonesMenu.Controls.Add(btnCerrarSesion, 0, 17);
            pnlItemsMenu.Controls.Clear();
            pnlItemsMenu.Controls.Add(layoutBotonesMenu);
            layoutBarraLateral.Controls.Add(pnlLogo, 0, 0);
            layoutBarraLateral.Controls.Add(identidad, 0, 1);
            layoutBarraLateral.Controls.Add(pnlItemsMenu, 0, 2);
            pnlMenu.Controls.Add(layoutBarraLateral);
            pnlMenu.ResumeLayout(true);
            pnlMenu.PerformLayout();
            AplicarEstadoBotonesBarraLateral();
            AplicarEstiloBotonesNavegacionPlanes();
            pnlMenu.Paint += (s, e) =>
            {
                using (Pen borde = new Pen(ColorTranslator.FromHtml("#34495E"), 1))
                    e.Graphics.DrawRectangle(borde, 0, 0, pnlMenu.Width - 1, pnlMenu.Height - 1);
            };
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

        private static void ConfigurarBotonNavegacion(IconButton boton, string texto, IconChar icono,
            Color colorIcono, Color fondo, bool borde)
        {
            boton.Text = texto;
            boton.IconChar = icono;
            boton.IconFont = IconFont.Auto;
            boton.IconSize = 20;
            boton.IconColor = colorIcono;
            boton.ImageAlign = ContentAlignment.MiddleLeft;
            boton.TextImageRelation = TextImageRelation.ImageBeforeText;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.Padding = new Padding(12, 0, 4, 0);
            boton.Font = new Font("Segoe UI", 9.5F);
            boton.ForeColor = Color.White;
            boton.BackColor = fondo;
            boton.Cursor = Cursors.Hand;
            boton.UseVisualStyleBackColor = false;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderColor = borde
                ? ColorTranslator.FromHtml(texto.StartsWith("Cerrar", StringComparison.Ordinal)
                    ? "#D56A70" : "#526779") : fondo;
            boton.FlatAppearance.BorderSize = borde ? 1 : 0;
        }

        private void MenuOpcion_MouseEnter(object sender, EventArgs e)
        {
            IconButton boton = sender as IconButton;
            if (boton == null) return;
            boton.BackColor = ColorTranslator.FromHtml("#0B3558");
            boton.ForeColor = ColorTranslator.FromHtml("#00D9FF");
            boton.IconColor = ColorTranslator.FromHtml("#00D9FF");
        }

        private void MenuOpcion_MouseLeave(object sender, EventArgs e)
        {
            AplicarEstadoBotonesBarraLateral();
        }

        private void AplicarEstadoBotonesBarraLateral()
        {
            Color normal = ColorTranslator.FromHtml("#061D30");
            Color activo = ColorTranslator.FromHtml("#0B3558");
            Color cian = ColorTranslator.FromHtml("#00D9FF");
            foreach (IconButton boton in botonesBarraLateralPlanes)
            {
                bool seleccionado = string.Equals(Convert.ToString(boton.Tag), GetType().Name,
                    StringComparison.OrdinalIgnoreCase);
                boton.BackColor = seleccionado ? activo : normal;
                boton.ForeColor = seleccionado ? cian : Color.White;
                boton.IconColor = cian;
                boton.FlatStyle = FlatStyle.Flat;
            }
        }

        private void AplicarEstiloBotonesNavegacionPlanes()
        {
            btnVolverMenu.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btnVolverMenu.Cursor = Cursors.Hand;
            btnCerrarSesion.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnVolverMenu.BackColor = ColorTranslator.FromHtml("#08243A");
            btnVolverMenu.ForeColor = Color.White;
            btnVolverMenu.FlatStyle = FlatStyle.Flat;
            btnVolverMenu.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#526779");
            btnVolverMenu.FlatAppearance.BorderSize = 1;
            btnCerrarSesion.BackColor = ColorTranslator.FromHtml("#A83F48");
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#D56A70");
            btnCerrarSesion.FlatAppearance.BorderSize = 1;
        }

        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => IniciarNuevo();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                if (cboFiltroEstado != null) cboFiltroEstado.SelectedIndex = 0;
                LimpiarFormulario();
                ConsultarPlanes();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarPlanes();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarPlanes();
                    e.SuppressKeyPress = true;
                }
            };

            dgvPlanes.SelectionChanged += DgvPlanes_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNombre.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDescripcion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCostoMensual.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCantidadDatos.CheckedChanged += (s, e) => ActualizarColumnas();
            chkMinutosIncluidos.CheckedChanged += (s, e) => ActualizarColumnas();
            chkVigenciaMeses.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            IconButton boton = control as IconButton;
            if (boton != null && boton.Tag != null)
                Navegacion.Abrir(this, boton.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoPlan", chkCodigo.Checked);
            CambiarVisibilidad("Nombre", chkNombre.Checked);
            CambiarVisibilidad("Descripcion", chkDescripcion.Checked);
            CambiarVisibilidad("CostoMensual", chkCostoMensual.Checked);
            CambiarVisibilidad("CantidadDatos", chkCantidadDatos.Checked);
            CambiarVisibilidad("MinutosIncluidos", chkMinutosIncluidos.Checked);
            CambiarVisibilidad("VigenciaMeses", chkVigenciaMeses.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvPlanes.Columns.Contains(columna))
                dgvPlanes.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvPlanes.Rows.Count > 0)
                {
                    int indice = dgvPlanes.CurrentRow == null ? 0 : dgvPlanes.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvPlanes.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvPlanes.ClearSelection();
                modoEdicion = false;
                EstablecerCamposHabilitados(false);
                ActualizarEstadoBotones(false);
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvPlanes.Rows.Count)
                return;

            dgvPlanes.ClearSelection();

            DataGridViewCell celda = dgvPlanes.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvPlanes.CurrentCell = celda;

            dgvPlanes.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!modoNuevo)
                return;

            if (!ValidarFormulario())
                return;

            try
            {
                PlanesEntidad nuevo = CrearEntidad();
                int codigoGenerado = negocio.Agregar(nuevo);

                ConsultarPlanes();
                LimpiarFormulario();
                MessageBox.Show(
                    "Plan agregado correctamente. Código: " + codigoGenerado,
                    "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al agregar plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            PlanesEntidad seleccionado = ObtenerSeleccionado();

            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }

            // El primer clic habilita los campos; el siguiente aplica la edicion local.
            if (!modoEdicion)
            {
                modoEdicion = true;
                modoNuevo = false;
                EstablecerCamposHabilitados(true);
                btnGuardar.Enabled = false;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = false;
                MessageBox.Show("Modifica los campos y pulsa Editar nuevamente para aplicar los cambios.");
                return;
            }

            if (!ValidarFormulario())
                return;

            try
            {
                PlanesEntidad cambios = CrearEntidad();
                cambios.CodigoPlan = seleccionado.CodigoPlan;
                negocio.Editar(cambios);

                ConsultarPlanes();
                LimpiarFormulario();
                MessageBox.Show("Plan actualizado correctamente.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al editar plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            PlanesEntidad seleccionado = ObtenerSeleccionado();

            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Deseas marcar como inactivo el plan " + seleccionado.CodigoPlan + "?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                // CodigoPlan identifica el registro; se conserva la fila mediante baja lógica.
                negocio.Eliminar(seleccionado.CodigoPlan);
                BuscarPlanes();
                LimpiarFormulario();
                MessageBox.Show("Plan marcado como inactivo.",
                    "Eliminar plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PlanesEntidad CrearEntidad()
        {
            return new PlanesEntidad(
                txtNombre.Text.Trim(),
                txtDescripcion.Text.Trim(),
                nudCostoMensual.Value,
                decimal.ToInt32(nudCantidadDatos.Value),
                decimal.ToInt32(nudMinutosIncluidos.Value),
                decimal.ToInt32(nudVigenciaMeses.Value),
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.");
                txtNombre.Focus();
                return false;
            }

            if (txtNombre.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Nombre no puede superar 100 caracteres.");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("El campo Descripción es obligatorio.");
                txtDescripcion.Focus();
                return false;
            }

            if (txtDescripcion.Text.Trim().Length > 200)
            {
                MessageBox.Show("El campo Descripción no puede superar 200 caracteres.");
                txtDescripcion.Focus();
                return false;
            }

            if (nudCostoMensual.Value <= 0)
            {
                MessageBox.Show("El campo Costo mensual debe ser mayor que cero.");
                nudCostoMensual.Focus();
                return false;
            }

            if (nudCantidadDatos.Value <= 0)
            {
                MessageBox.Show("El campo Datos (MB) debe ser mayor que cero.");
                nudCantidadDatos.Focus();
                return false;
            }

            if (nudVigenciaMeses.Value <= 0)
            {
                MessageBox.Show("El campo Vigencia (meses) debe ser mayor que cero.");
                nudVigenciaMeses.Focus();
                return false;
            }

            return true;
        }

        private PlanesEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvPlanes.CurrentRow == null)
                return null;

            return dgvPlanes.CurrentRow.DataBoundItem as PlanesEntidad;
        }

        private void DgvPlanes_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked || modoNuevo)
                return;

            PlanesEntidad registro = ObtenerSeleccionado();
            if (registro == null)
            {
                EstablecerCamposHabilitados(false);
                ActualizarEstadoBotones(false);
                return;
            }

            cargando = true;

            txtCodigo.Text = registro.CodigoPlan.ToString();
            txtNombre.Text = registro.Nombre;
            txtDescripcion.Text = registro.Descripcion;
            nudCostoMensual.Value = Limitar(nudCostoMensual, registro.CostoMensual);
            nudCantidadDatos.Value = Limitar(nudCantidadDatos, registro.CantidadDatos);
            nudMinutosIncluidos.Value = Limitar(nudMinutosIncluidos, registro.MinutosIncluidos);
            nudVigenciaMeses.Value = Limitar(nudVigenciaMeses, registro.VigenciaMeses);
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
            modoEdicion = false;
            EstablecerCamposHabilitados(false);
            ActualizarEstadoBotones(true);
        }

        private decimal Limitar(NumericUpDown control, decimal valor)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, valor));
        }

        private static bool Contiene(string valor, string criterio)
        {
            return valor != null && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void BuscarPlanes()
        {
            string criterio = txtBuscar.Text.Trim();
            try
            {
                // Refresca el conjunto completo para que el total no dependa de la pagina ni de filtros anteriores.
                List<PlanesEntidad> todos = negocio.Listar();
                planes.Clear();
                foreach (PlanesEntidad plan in todos)
                    planes.Add(plan);

                List<PlanesEntidad> resultado = criterio.Length == 0
                    ? todos
                    : negocio.Buscar(criterio);
                int activos = resultado.Count(p => p.Estado);
                int inactivos = resultado.Count - activos;
                MostrarPlanes(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + "  |  Activos: " + activos +
                    "  |  Inactivos: " + inactivos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    MensajesError.Resumir(ex),
                    "Error al buscar planes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MostrarPlanes(List<PlanesEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvPlanes.DataSource = origen;
            ActualizarColumnas();
            dgvPlanes.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;
            modoNuevo = false;
            modoEdicion = false;

            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            nudCostoMensual.Value = 0;
            nudCantidadDatos.Value = 0;
            nudMinutosIncluidos.Value = 0;
            nudVigenciaMeses.Value = 0;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvPlanes.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            EstablecerCamposHabilitados(false);
            ActualizarEstadoBotones(false);
            txtNombre.Focus();
        }

        private void IniciarNuevo()
        {
            LimpiarFormulario();
            modoNuevo = true;
            EstablecerCamposHabilitados(true);
            ActualizarEstadoBotones(false);
            txtNombre.Focus();
        }

        private void EstablecerCamposHabilitados(bool habilitados)
        {
            txtNombre.Enabled = habilitados;
            txtDescripcion.Enabled = habilitados;
            nudCostoMensual.Enabled = habilitados;
            nudCantidadDatos.Enabled = habilitados;
            nudMinutosIncluidos.Enabled = habilitados;
            nudVigenciaMeses.Enabled = habilitados;
            rdoActivo.Enabled = habilitados;
            rdoInactivo.Enabled = habilitados;
        }

        private void ActualizarEstadoBotones(bool haySeleccion)
        {
            btnGuardar.Enabled = modoNuevo;
            btnEditar.Enabled = !modoNuevo && (modoEdicion || haySeleccion);
            btnEliminar.Enabled = !modoNuevo && !modoEdicion && haySeleccion;
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            AplicarEstiloBotonesNavegacionPlanes();
        }

        private void ConsultarPlanes()
        {
            try
            {
                List<PlanesEntidad> registros = negocio.Listar();

                planes.Clear();
                foreach (PlanesEntidad plan in registros)
                    planes.Add(plan);

                // El paginador compartido recorta la lista a diez filas por página.
                MostrarPlanes(planes.ToList());
                int activos = planes.Count(p => p.Estado);
                lblRegistros.Text = "Resultados: " + planes.Count + "  |  Activos: " + activos +
                    "  |  Inactivos: " + (planes.Count - activos);
                if (pnlDashboardPlanes != null) _ = CargarDashboardPlanesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    MensajesError.Resumir(ex),
                    "Error al consultar planes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
