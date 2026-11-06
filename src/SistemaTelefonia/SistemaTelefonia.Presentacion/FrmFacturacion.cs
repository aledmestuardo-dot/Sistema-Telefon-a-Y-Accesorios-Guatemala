using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmFacturacion : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<FacturacionEntidad> facturacion = new BindingList<FacturacionEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private readonly List<VentasEntidad> ventasDisponibles = new List<VentasEntidad>();
        private readonly Dictionary<int, string> nombresClientesVentas = new Dictionary<int, string>();
        private bool cargando = false;
        private PanelRedondeado pnlDashboardFacturacion;
        private TableLayoutPanel layoutBloquesFacturacion;
        private Label[] valoresDashboard;
        private Label lblResumenEstado;
        private Label lblFacturasRecientes;
        private Chart graficaDashboard;
        private Button btnActualizarDashboard;
        private Button btnAgregarFactura;
        private Button btnAbrirFacturaSeleccionada;
        private CheckBox chkActivarFiltros;
        private TableLayoutPanel layoutOpcionesColumnasFacturacion;
        private TableLayoutPanel layoutBusquedaFacturacion;
        private ComboBox cboFiltroEstadoFacturacion;
        private bool actualizandoFiltroColumnas;

        public FrmFacturacion()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = facturacion;
            dgvFacturacion.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvFacturacion);
            ConfigurarCatalogoFacturacion();
            Shown += (s, e) => AplicarEsquinasRedondeadasRelacional();

            ConectarEventos();
            ConfigurarComboVentas();
            ConstruirDashboardRelacional();
            AplicarEsquinasRedondeadasRelacional();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
        }

        private void ConstruirNavegacionLateral()
        {
            AjustesDisenoCrud.ConstruirBarraLateralModelo(this);
        }
        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                MostrarFacturacion(facturacion.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarFacturacion();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarFacturacion();
                    e.SuppressKeyPress = true;
                }
            };

            dgvFacturacion.SelectionChanged += DgvFacturacion_SelectionChanged;
            dgvFacturacion.SelectionChanged += (s, e) => ActualizarBotonAbrirSeleccionada();
            dgvFacturacion.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) AbrirFacturaCompleta(dgvFacturacion.Rows[e.RowIndex].DataBoundItem as FacturacionEntidad); };
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;
            chkSeleccionar.CheckedChanged += (s, e) => ActualizarBotonAbrirSeleccionada();

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoVenta.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNumeroFactura.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaEmision.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNombreCliente.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNit.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTotal.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
            cmbCodigoVenta.SelectedValueChanged += CmbCodigoVenta_SelectedValueChanged;
        }

        /// <summary>El selector muestra ventas activas que todavía no tienen factura.</summary>
        private void ConfigurarComboVentas()
        {
            try
            {
                List<VentasEntidad> ventas = relacional.Listar<VentasEntidad>();
                HashSet<int> ventasFacturadas = new HashSet<int>(relacional.Listar<FacturacionEntidad>().Select(f => f.CodigoVenta));
                nombresClientesVentas.Clear();
                foreach (ClientesEntidad cliente in relacional.ListarCatalogo<ClientesEntidad>())
                    if (!nombresClientesVentas.ContainsKey(cliente.CodigoCliente))
                        nombresClientesVentas.Add(cliente.CodigoCliente, cliente.Nombre);
                ventasDisponibles.Clear();
                ventasDisponibles.AddRange(ventas.Where(v => v.Estado && !ventasFacturadas.Contains(v.CodigoVenta)));
                ComboFK.Cargar(cmbCodigoVenta, ventasDisponibles, v => v.CodigoVenta, DescripcionVenta);
            }
            catch (Exception ex)
            {
                cmbCodigoVenta.DataSource = null;
                cmbCodigoVenta.Items.Clear();
                MessageBox.Show("No fue posible cargar los códigos de venta para facturación. " + MensajesError.Resumir(ex),
                    "Error al cargar ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbCodigoVenta_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cargando) return;
            int? codigoVenta = ComboFK.Obtener(cmbCodigoVenta);
            VentasEntidad venta = codigoVenta.HasValue
                ? ventasDisponibles.FirstOrDefault(x => x.CodigoVenta == codigoVenta.Value) : null;
            if (venta == null) return;

            // Prellena el total de la factura con el total registrado en la venta.
            nudTotal.Value = Limitar(nudTotal, venta.Total);
        }

        private void ConstruirDashboardRelacional()
        {
            pnlFormulario.Visible = false;
            pnlAcciones.Visible = false;
            pnlContenido.AutoScroll = true;
            pnlCatalogo.Dock = DockStyle.Fill;
            pnlCatalogo.Margin = Padding.Empty;
            lblTitulo.Text = "Gestión de facturación";
            lblSubtitulo.Text = "Consulta y administra facturas con sus conceptos";
            lblCatalogoTitulo.Text = "Facturas registradas";
            Color fondo = ColorTranslator.FromHtml("#08243A");
            Color superficie = ColorTranslator.FromHtml("#0B3558");
            pnlDashboardFacturacion = new PanelRedondeado
            {
                Name = "pnlDashboardFacturacion", Dock = DockStyle.Fill, Margin = Padding.Empty,
                Padding = new Padding(12), BackColor = fondo, RadioEsquinas = 12
            };
            TableLayoutPanel raiz = new TableLayoutPanel
            {
                Name = "layoutDashboardFacturacion", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            raiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            TableLayoutPanel encabezado = new TableLayoutPanel
            {
                Name = "layoutEncabezadoDashboardFacturacion", Dock = DockStyle.Fill,
                ColumnCount = 2, RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
            encabezado.Controls.Add(new Label
            {
                Name = "lblTituloDashboardFacturacion", Text = "Dashboard de facturación", Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);
            btnActualizarDashboard = new Button
            {
                Name = "btnActualizarDashboardFacturacion", Text = "↻ Actualizar", Dock = DockStyle.Fill,
                Margin = new Padding(4, 1, 0, 1), FlatStyle = FlatStyle.Flat,
                BackColor = ColorTranslator.FromHtml("#0B6E91"), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnActualizarDashboard.FlatAppearance.BorderSize = 0;
            btnActualizarDashboard.Click += (s, e) => ConsultarRelacional();
            encabezado.Controls.Add(btnActualizarDashboard, 1, 0);
            raiz.Controls.Add(encabezado, 0, 0);

            FlowLayoutPanel accionesDashboard = new FlowLayoutPanel
            {
                Name = "layoutAccionesDashboardFacturacion", Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft, WrapContents = false,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            btnAbrirFacturaSeleccionada = CrearBotonDashboard("btnAbrirFacturaSeleccionada",
                "Ver / editar seleccionada", ColorTranslator.FromHtml("#2C4E71"), new Size(232, 36));
            btnAbrirFacturaSeleccionada.Margin = new Padding(4, 9, 4, 9);
            btnAbrirFacturaSeleccionada.Enabled = false;
            btnAbrirFacturaSeleccionada.Click += (s, e) =>
            {
                FacturacionEntidad factura = ObtenerSeleccionado();
                if (factura != null) AbrirFacturaCompleta(factura);
            };
            btnAgregarFactura = CrearBotonDashboard("btnAgregarFactura", "Agregar nueva factura",
                ColorTranslator.FromHtml("#208E60"), new Size(232, 36));
            btnAgregarFactura.Margin = new Padding(4, 9, 4, 9);
            btnAgregarFactura.Click += (s, e) => AbrirFacturaCompleta(null);
            accionesDashboard.Controls.Add(btnAbrirFacturaSeleccionada);
            accionesDashboard.Controls.Add(btnAgregarFactura);
            foreach (string nombre in new[] { "btnImportarExcel", "btnExportarExcel", "btnImprimir" })
            {
                Button boton = Controls.Find(nombre, true).OfType<Button>().FirstOrDefault();
                if (boton == null) continue;
                if (boton.Parent != null) boton.Parent.Controls.Remove(boton);
                boton.Dock = DockStyle.None;
                boton.Size = new Size(158, 48);
                boton.Margin = new Padding(4, 3, 4, 3);
                boton.TextAlign = ContentAlignment.MiddleCenter;
                boton.AutoEllipsis = false;
                accionesDashboard.Controls.Add(boton);
            }
            raiz.Controls.Add(accionesDashboard, 0, 1);

            TableLayoutPanel tarjetas = new TableLayoutPanel
            {
                Name = "layoutIndicadoresDashboardFacturacion", Dock = DockStyle.Fill,
                ColumnCount = 4, RowCount = 1, Margin = new Padding(0, 5, 0, 5),
                Padding = Padding.Empty, BackColor = fondo
            };
            for (int i = 0; i < 4; i++) tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            string[] nombres = { "FACTURAS REGISTRADAS", "FACTURAS ACTIVAS", "FACTURAS INACTIVAS", "MONTO FACTURADO" };
            Color[] acentos = { ColorTranslator.FromHtml("#3B9EFF"), ColorTranslator.FromHtml("#35C98B"),
                ColorTranslator.FromHtml("#E7B84B"), ColorTranslator.FromHtml("#8B72E8") };
            valoresDashboard = new Label[4];
            for (int i = 0; i < nombres.Length; i++)
            {
                PanelRedondeado tarjeta = new PanelRedondeado
                {
                    Name = "pnlIndicadorDashboardFacturacion" + i, Dock = DockStyle.Fill,
                    Margin = new Padding(0, 0, i == nombres.Length - 1 ? 0 : 8, 0),
                    BackColor = superficie, RadioEsquinas = 10
                };
                TableLayoutPanel contenidoTarjeta = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
                    Margin = Padding.Empty, Padding = Padding.Empty, BackColor = superficie
                };
                contenidoTarjeta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                contenidoTarjeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 3));
                contenidoTarjeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                contenidoTarjeta.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                contenidoTarjeta.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = acentos[i], Margin = Padding.Empty }, 0, 0);
                contenidoTarjeta.Controls.Add(new Label
                {
                    Text = nombres[i], Dock = DockStyle.Fill, Margin = new Padding(10, 0, 8, 0),
                    ForeColor = ColorTranslator.FromHtml("#A9BAC8"), Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    AutoEllipsis = true, TextAlign = ContentAlignment.BottomLeft
                }, 0, 1);
                valoresDashboard[i] = new Label
                {
                    Name = "lblValorIndicadorDashboardFacturacion" + i, Text = "—", Dock = DockStyle.Fill,
                    Margin = new Padding(10, 0, 8, 4), ForeColor = Color.White,
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft,
                    AutoEllipsis = true
                };
                contenidoTarjeta.Controls.Add(valoresDashboard[i], 0, 2);
                tarjeta.Controls.Add(contenidoTarjeta);
                tarjetas.Controls.Add(tarjeta, i, 0);
            }
            raiz.Controls.Add(tarjetas, 0, 2);

            TableLayoutPanel inferior = new TableLayoutPanel
            {
                Name = "layoutDetalleDashboardFacturacion", Dock = DockStyle.Fill,
                ColumnCount = 2, RowCount = 1, Margin = new Padding(0, 4, 0, 0),
                Padding = Padding.Empty, BackColor = fondo
            };
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
            Panel panelGrafica = CrearPanelGraficaDashboardFacturacion("Facturas emitidas por mes", out graficaDashboard);
            inferior.Controls.Add(panelGrafica, 0, 0);
            TableLayoutPanel panelDerecho = new TableLayoutPanel
            {
                Name = "layoutResumenesDashboardFacturacion", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 2, Margin = new Padding(8, 0, 0, 0),
                Padding = Padding.Empty, BackColor = fondo
            };
            panelDerecho.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 42));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 58));
            Panel resumen = CrearPanelDashboardFacturacion("Facturas por estado", out lblResumenEstado);
            Panel recientes = CrearPanelDashboardFacturacion("Facturación reciente", out lblFacturasRecientes);
            resumen.Margin = new Padding(0, 0, 0, 5);
            recientes.Margin = new Padding(0, 5, 0, 0);
            panelDerecho.Controls.Add(resumen, 0, 0);
            panelDerecho.Controls.Add(recientes, 0, 1);
            inferior.Controls.Add(panelDerecho, 1, 0);
            raiz.Controls.Add(inferior, 0, 3);
            pnlDashboardFacturacion.Controls.Add(raiz);

            layoutBloquesFacturacion = new TableLayoutPanel
            {
                Name = "layoutBloquesFacturacion", Dock = DockStyle.Top, ColumnCount = 1, RowCount = 3,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlContenido.BackColor,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesFacturacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutBloquesFacturacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 380));
            layoutBloquesFacturacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            layoutBloquesFacturacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 320));
            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesFacturacion);
            layoutBloquesFacturacion.Controls.Add(pnlDashboardFacturacion, 0, 0);
            layoutBloquesFacturacion.Controls.Add(new Panel
            {
                Name = "pnlSeparadorDashboard", Dock = DockStyle.Fill, Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor
            }, 0, 1);
            pnlCatalogo.Margin = Padding.Empty;
            layoutBloquesFacturacion.Controls.Add(pnlCatalogo, 0, 2);
            pnlContenido.SizeChanged += (s, e) => AjustarDashboardFacturacion();
            AjustarDashboardFacturacion();
        }

        private static Button CrearBotonDashboard(string nombre, string texto, Color color, Size tamano)
        {
            Button boton = new Button
            {
                Name = nombre, Text = texto, Size = tamano, Margin = new Padding(4, 3, 4, 3),
                FlatStyle = FlatStyle.Flat, BackColor = color, ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false, TextAlign = ContentAlignment.MiddleCenter, AutoEllipsis = false
            };
            boton.FlatAppearance.BorderSize = 0;
            CampoRedondeado.Aplicar(boton, 8);
            return boton;
        }

        private static Panel CrearPanelDashboardFacturacion(string encabezado, out Label contenido)
        {
            PanelRedondeado panel = new PanelRedondeado
            {
                Dock = DockStyle.Fill, Padding = new Padding(9, 6, 9, 6),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty, RadioEsquinas = 10
            };
            panel.Controls.Add(new Label
            {
                Text = encabezado, Dock = DockStyle.Top, Height = 21,
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

        private static Panel CrearPanelGraficaDashboardFacturacion(string encabezado, out Chart grafica)
        {
            PanelRedondeado panel = new PanelRedondeado
            {
                Dock = DockStyle.Fill, Padding = new Padding(7, 5, 7, 5),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty, RadioEsquinas = 10
            };
            panel.Controls.Add(new Label
            {
                Text = encabezado, Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true
            });
            grafica = new Chart { Dock = DockStyle.Fill, BackColor = ColorTranslator.FromHtml("#0B3558") };
            ChartArea area = new ChartArea("FacturacionMensual") { BackColor = ColorTranslator.FromHtml("#0B3558") };
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
            grafica.ChartAreas.Add(area);
            grafica.Series.Add(new Series("Facturas")
            {
                ChartArea = "FacturacionMensual", ChartType = SeriesChartType.Column,
                BorderWidth = 0, Color = ColorTranslator.FromHtml("#00D9FF"), IsXValueIndexed = true
            });
            panel.Controls.Add(grafica);
            return panel;
        }

        private void AjustarDashboardFacturacion()
        {
            if (layoutBloquesFacturacion == null) return;
            int disponible = Math.Max(0, pnlContenido.ClientSize.Height - pnlContenido.Padding.Vertical);
            int altoCatalogo = CalcularAltoCatalogoFacturacion();
            int altoDashboard = Math.Max(380, disponible - altoCatalogo - 10);
            layoutBloquesFacturacion.RowStyles[0].Height = altoDashboard;
            layoutBloquesFacturacion.RowStyles[2].Height = altoCatalogo;
            layoutBloquesFacturacion.Height = altoDashboard + 10 + altoCatalogo;
        }

        private void ConfigurarCatalogoFacturacion()
        {
            lblCatalogoTitulo.Text = "Facturas registradas";
            lblCatalogoTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCatalogoTitulo.Height = 40;
            lblColumnas.Text = "Opciones de visualización:";
            lblColumnas.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblColumnas.Height = 40;
            lblColumnas.ForeColor = Color.White;
            pnlBusqueda.Height = 54;
            pnlBusqueda.Padding = new Padding(8, 4, 8, 4);
            pnlColumnas.Height = 76;
            selector.Controls.Remove(chkSeleccionar);
            selector.Dock = DockStyle.Fill;
            selector.FlowDirection = FlowDirection.LeftToRight;
            selector.WrapContents = false;
            selector.AutoScroll = true;
            selector.Padding = new Padding(0, 2, 0, 2);
            foreach (CheckBox casilla in selector.Controls.OfType<CheckBox>())
            {
                casilla.AutoSize = true;
                casilla.Font = new Font("Segoe UI", 9F);
                casilla.Margin = new Padding(2, 3, 5, 3);
            }

            chkSeleccionar.Text = "Seleccionar registro";
            chkSeleccionar.AutoSize = true;
            chkSeleccionar.Anchor = AnchorStyles.Right;
            chkSeleccionar.Margin = new Padding(8, 4, 4, 0);
            chkSeleccionar.Font = new Font("Segoe UI", 9F);

            chkActivarFiltros = new CheckBox
            {
                Name = "chkActivarTodosFiltrosFacturacion", Text = "Activar todos los filtros", AutoSize = true,
                Anchor = AnchorStyles.Left, ForeColor = Color.White, BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F), Margin = new Padding(4, 4, 10, 0)
            };
            layoutOpcionesColumnasFacturacion = new TableLayoutPanel
            {
                Name = "layoutOpcionesColumnasFacturacion", Dock = DockStyle.Fill,
                ColumnCount = 3, RowCount = 2, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = pnlColumnas.BackColor
            };
            layoutOpcionesColumnasFacturacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0));
            layoutOpcionesColumnasFacturacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutOpcionesColumnasFacturacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0));
            layoutOpcionesColumnasFacturacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layoutOpcionesColumnasFacturacion.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            pnlColumnas.Controls.Remove(selector);
            pnlColumnas.Controls.Remove(lblColumnas);
            lblColumnas.Dock = DockStyle.Fill;
            layoutOpcionesColumnasFacturacion.Controls.Add(lblColumnas, 0, 0);
            layoutOpcionesColumnasFacturacion.SetColumnSpan(lblColumnas, 3);
            selector.Margin = Padding.Empty;
            chkSeleccionar.Margin = new Padding(8, 4, 4, 0);
            chkSeleccionar.Anchor = AnchorStyles.Right;
            layoutOpcionesColumnasFacturacion.Controls.Add(chkActivarFiltros, 0, 1);
            layoutOpcionesColumnasFacturacion.Controls.Add(selector, 1, 1);
            layoutOpcionesColumnasFacturacion.Controls.Add(chkSeleccionar, 2, 1);
            pnlColumnas.Controls.Add(layoutOpcionesColumnasFacturacion);
            layoutOpcionesColumnasFacturacion.SizeChanged += (s, e) => AjustarAnchosChecksFacturacion();
            AjustarAnchosChecksFacturacion();
            chkActivarFiltros.CheckedChanged += (s, e) =>
            {
                if (actualizandoFiltroColumnas) return;
                actualizandoFiltroColumnas = true;
                try { foreach (CheckBox casilla in ObtenerFiltrosColumnasFacturacion()) casilla.Checked = chkActivarFiltros.Checked; }
                finally { actualizandoFiltroColumnas = false; }
                ActualizarColumnas();
            };
            foreach (CheckBox casilla in ObtenerFiltrosColumnasFacturacion())
                casilla.CheckedChanged += (s, e) => SincronizarFiltroGlobalFacturacion();
            SincronizarFiltroGlobalFacturacion();

            cboFiltroEstadoFacturacion = Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault();
            if (cboFiltroEstadoFacturacion != null)
            {
                pnlBusqueda.Controls.Remove(txtBuscar);
                pnlBusqueda.Controls.Remove(btnBuscar);
                Control padreEstado = cboFiltroEstadoFacturacion.Parent;
                if (padreEstado != null) padreEstado.Controls.Remove(cboFiltroEstadoFacturacion);
                pnlBusqueda.Controls.Clear();
                layoutBusquedaFacturacion = new TableLayoutPanel
                {
                    Name = "layoutBusquedaFacturacion", Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1,
                    Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlBusqueda.BackColor
                };
                layoutBusquedaFacturacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
                layoutBusquedaFacturacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                layoutBusquedaFacturacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));
                layoutBusquedaFacturacion.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                cboFiltroEstadoFacturacion.Dock = DockStyle.Fill;
                cboFiltroEstadoFacturacion.Margin = new Padding(0, 0, 8, 0);
                txtBuscar.Dock = DockStyle.Fill;
                txtBuscar.Margin = new Padding(0, 0, 8, 0);
                btnBuscar.Dock = DockStyle.Fill;
                btnBuscar.Margin = Padding.Empty;
                layoutBusquedaFacturacion.Controls.Add(cboFiltroEstadoFacturacion, 0, 0);
                layoutBusquedaFacturacion.Controls.Add(txtBuscar, 1, 0);
                layoutBusquedaFacturacion.Controls.Add(btnBuscar, 2, 0);
                pnlBusqueda.Controls.Add(layoutBusquedaFacturacion);
            }

            dgvFacturacion.Dock = DockStyle.Top;
            dgvFacturacion.RowTemplate.Height = 29;
            dgvFacturacion.ColumnHeadersHeight = 34;
            dgvFacturacion.ScrollBars = ScrollBars.Horizontal;
            dgvFacturacion.Height = dgvFacturacion.ColumnHeadersHeight + 10 * dgvFacturacion.RowTemplate.Height + 2;
            pnlCatalogo.SizeChanged += (s, e) => AjustarDashboardFacturacion();
            AjustarDashboardFacturacion();
        }

        private IEnumerable<CheckBox> ObtenerFiltrosColumnasFacturacion()
        {
            return new[] { chkCodigo, chkCodigoVenta, chkNumeroFactura, chkFechaEmision,
                chkNombreCliente, chkNit, chkTotal, chkEstado };
        }

        private void AjustarAnchosChecksFacturacion()
        {
            if (layoutOpcionesColumnasFacturacion == null || chkActivarFiltros == null || chkSeleccionar == null ||
                layoutOpcionesColumnasFacturacion.ColumnStyles.Count < 3) return;

            chkActivarFiltros.AutoSize = true;
            chkSeleccionar.AutoSize = true;
            int anchoActivar = TextRenderer.MeasureText(chkActivarFiltros.Text, chkActivarFiltros.Font).Width +
                28 + chkActivarFiltros.Margin.Horizontal + 12;
            int anchoSeleccionar = TextRenderer.MeasureText(chkSeleccionar.Text, chkSeleccionar.Font).Width +
                28 + chkSeleccionar.Margin.Horizontal + 12;
            layoutOpcionesColumnasFacturacion.ColumnStyles[0].SizeType = SizeType.Absolute;
            layoutOpcionesColumnasFacturacion.ColumnStyles[0].Width = anchoActivar;
            layoutOpcionesColumnasFacturacion.ColumnStyles[2].SizeType = SizeType.Absolute;
            layoutOpcionesColumnasFacturacion.ColumnStyles[2].Width = anchoSeleccionar;
        }

        private void SincronizarFiltroGlobalFacturacion()
        {
            if (chkActivarFiltros == null || actualizandoFiltroColumnas) return;
            actualizandoFiltroColumnas = true;
            try { chkActivarFiltros.Checked = ObtenerFiltrosColumnasFacturacion().All(c => c.Checked); }
            finally { actualizandoFiltroColumnas = false; }
            ActualizarColumnas();
        }

        private int CalcularAltoCatalogoFacturacion()
        {
            int altoGrilla = dgvFacturacion.ColumnHeadersHeight + 10 * Math.Max(26, dgvFacturacion.RowTemplate.Height) + 2;
            Panel paginacion = Controls.Find("pnlPaginacion", true).OfType<Panel>().FirstOrDefault();
            int altoPie = paginacion == null ? 48 : Math.Max(48, paginacion.Height);
            return pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height +
                pnlColumnas.Height + altoGrilla + altoPie;
        }

        private void AplicarEsquinasRedondeadasRelacional()
        {
            foreach (Panel panel in ControlesDescendientes<Panel>(this))
            {
                if (panel.Name == "pnlMenu" || panel.Name == "pnlItemsMenu" || panel.Name == "pnlContenido" ||
                    panel.Name == "pnlAcciones" || panel.Name.IndexOf("Separador", StringComparison.OrdinalIgnoreCase) >= 0) continue;
                CampoRedondeado.Aplicar(panel, panel == pnlDashboardFacturacion ? 12 : 10);
            }
            foreach (Control control in ControlesDescendientes<Control>(this))
            {
                if (control is TextBoxBase || control is ComboBox || control is NumericUpDown || control is DateTimePicker)
                    CampoRedondeado.Aplicar(control, 7);
                else if (control is DataGridView)
                    CampoRedondeado.Aplicar(control, 10);
                else if (control is Button)
                    CampoRedondeado.Aplicar(control, 8);
            }
        }

        private static IEnumerable<T> ControlesDescendientes<T>(Control raiz) where T : Control
        {
            foreach (Control control in raiz.Controls)
            {
                T coincidencia = control as T;
                if (coincidencia != null) yield return coincidencia;
                foreach (T hijo in ControlesDescendientes<T>(control)) yield return hijo;
            }
        }

        private void ActualizarDatosDashboardFacturacion()
        {
            if (valoresDashboard == null) return;
            valoresDashboard[0].Text = facturacion.Count.ToString("N0");
            valoresDashboard[1].Text = facturacion.Count(x => x.Estado).ToString("N0");
            valoresDashboard[2].Text = facturacion.Count(x => !x.Estado).ToString("N0");
            valoresDashboard[3].Text = "Q " + facturacion.Where(x => x.Estado).Sum(x => x.Total).ToString("N2");
            lblResumenEstado.Text = "Activas: " + facturacion.Count(x => x.Estado).ToString("N0") +
                Environment.NewLine + "Inactivas: " + facturacion.Count(x => !x.Estado).ToString("N0");
            Series serie = graficaDashboard.Series["Facturas"];
            serie.Points.Clear();
            DateTime mesActual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            for (int i = 5; i >= 0; i--)
            {
                DateTime mes = mesActual.AddMonths(-i);
                int cantidad = facturacion.Count(x => x.FechaEmision.Year == mes.Year && x.FechaEmision.Month == mes.Month);
                serie.Points.AddXY(mes.ToString("MMM"), cantidad);
            }
            FacturacionEntidad[] recientes = facturacion.OrderByDescending(x => x.FechaEmision)
                .ThenByDescending(x => x.CodigoFactura).Take(4).ToArray();
            lblFacturasRecientes.Text = recientes.Length == 0 ? "No hay facturas registradas." :
                string.Join(Environment.NewLine, recientes.Select(x =>
                    (string.IsNullOrWhiteSpace(x.NumeroFactura) ? "Factura " + x.CodigoFactura : x.NumeroFactura) +
                    " | " + x.FechaEmision.ToString("dd/MM/yyyy") + " | Q " + x.Total.ToString("N2")));
        }

        internal void ImportarFacturasExcel()
        {
            DataTable datos;
            string nombreArchivo;
            try { datos = ArchivosExcel.ImportarTabla(out nombreArchivo); }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible leer el archivo: " + MensajesError.Resumir(ex), "Importación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (datos == null) return;
            string[] requeridas = { "NumeroFactura", "CodigoVenta", "FechaEmision", "NombreCliente", "Descripcion", "Cantidad", "PrecioUnitario" };
            string[] faltantes = requeridas.Where(c => !datos.Columns.Contains(c)).ToArray();
            if (faltantes.Length > 0)
            {
                MessageBox.Show("El archivo debe tener estas columnas para importar facturas con detalle: " +
                    string.Join(", ", requeridas) + ". Cada fila representa una línea; repite los datos de la factura en sus líneas.",
                    "Formato de importación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (Form vista = new Form
            {
                Text = "Vista previa: " + nombreArchivo + " · " + datos.Rows.Count + " líneas",
                Size = new Size(1000, 640), StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(3, 22, 43), Font = new Font("Segoe UI", 9F)
            })
            using (TableLayoutPanel raiz = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1, Padding = new Padding(12) })
            using (DataGridView tabla = new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells, DataSource = datos
            })
            {
                raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
                raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
                Label instruccion = new Label
                {
                    Dock = DockStyle.Fill, ForeColor = Color.White,
                    Text = "Cada fila es un detalle. Agrupa las líneas por NumeroFactura y repite CodigoVenta, FechaEmision y NombreCliente.",
                    TextAlign = ContentAlignment.MiddleLeft
                };
                Button confirmar = new Button
                {
                    Name = "btnConfirmarImportacionFacturas", Text = "Importar facturas", Dock = DockStyle.Right,
                    Width = 174, Height = 42, BackColor = Color.FromArgb(112, 83, 174), ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), UseVisualStyleBackColor = false
                };
                confirmar.FlatAppearance.BorderSize = 0;
                CampoRedondeado.Aplicar(confirmar, 8);
                confirmar.Click += (s, e) => ProcesarImportacionFacturas(datos, vista);
                Panel pie = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(7, 35, 66), Padding = new Padding(4) };
                pie.Controls.Add(confirmar);
                raiz.Controls.Add(instruccion, 0, 0);
                raiz.Controls.Add(tabla, 0, 1);
                raiz.Controls.Add(pie, 0, 2);
                vista.Controls.Add(raiz);
                vista.ShowDialog(this);
            }
        }

        private void ProcesarImportacionFacturas(DataTable datos, Form vista)
        {
            int correctas = 0;
            List<string> errores = new List<string>();
            HashSet<int> ventasUsadas = new HashSet<int>();
            IEnumerable<DataRow> filasImportadas = datos.Rows.Cast<DataRow>();
            foreach (IGrouping<string, DataRow> grupo in filasImportadas.GroupBy(
                fila => Convert.ToString(fila["NumeroFactura"]).Trim(), StringComparer.OrdinalIgnoreCase))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(grupo.Key)) throw new InvalidOperationException("NumeroFactura es obligatorio.");
                    if (facturacion.Any(x => string.Equals(x.NumeroFactura, grupo.Key, StringComparison.OrdinalIgnoreCase)))
                        throw new InvalidOperationException("Ya existe una factura con el número " + grupo.Key + ".");
                    DataRow primera = grupo.First();
                    FacturacionEntidad factura = (FacturacionEntidad)ArchivosExcel.CrearEntidad(primera, typeof(FacturacionEntidad));
                    factura.CodigoFactura = 0;
                    factura.NumeroFactura = grupo.Key;
                    factura.Estado = true;
                    if (factura.FechaEmision == DateTime.MinValue) throw new InvalidOperationException("FechaEmision no tiene una fecha válida.");
                    if (string.IsNullOrWhiteSpace(factura.NombreCliente)) throw new InvalidOperationException("NombreCliente es obligatorio.");
                    if (!grupo.All(f => Convert.ToInt32(f["CodigoVenta"], CultureInfo.InvariantCulture) == factura.CodigoVenta))
                        throw new InvalidOperationException("Todas las líneas de la factura deben usar el mismo CodigoVenta.");
                    if (!ventasDisponibles.Any(x => x.CodigoVenta == factura.CodigoVenta) || !ventasUsadas.Add(factura.CodigoVenta))
                        throw new InvalidOperationException("CodigoVenta no está disponible o ya se usó en este archivo.");

                    List<DetalleFacturaEntidad> lineas = new List<DetalleFacturaEntidad>();
                    foreach (DataRow fila in grupo)
                    {
                        DetalleFacturaEntidad detalle = (DetalleFacturaEntidad)ArchivosExcel.CrearEntidad(fila, typeof(DetalleFacturaEntidad));
                        detalle.CodigoDetalleFactura = 0;
                        detalle.CodigoFactura = 0;
                        detalle.Estado = true;
                        if (string.IsNullOrWhiteSpace(detalle.Descripcion)) throw new InvalidOperationException("Descripcion es obligatoria.");
                        if (detalle.Cantidad <= 0) throw new InvalidOperationException("Cantidad debe ser mayor que cero.");
                        if (detalle.PrecioUnitario <= 0) throw new InvalidOperationException("PrecioUnitario debe ser mayor que cero.");
                        decimal bruto = detalle.Cantidad * detalle.PrecioUnitario;
                        if (detalle.Descuento < 0 || detalle.Descuento > bruto) throw new InvalidOperationException("Descuento debe estar entre cero y el importe de la línea.");
                        detalle.Subtotal = bruto - detalle.Descuento;
                        detalle.Iva = decimal.Round(detalle.Subtotal * 0.12m, 2, MidpointRounding.AwayFromZero);
                        lineas.Add(detalle);
                    }
                    factura.Total = lineas.Sum(x => x.Subtotal + x.Iva);
                    relacional.AgregarConDetalles<FacturacionEntidad, DetalleFacturaEntidad>(factura, lineas, "CodigoFactura");
                    correctas++;
                }
                catch (Exception ex)
                {
                    if (errores.Count < 5) errores.Add("Factura '" + grupo.Key + "': " + MensajesError.Resumir(ex));
                }
            }

            string detalleErrores = errores.Count == 0 ? string.Empty : Environment.NewLine + "Errores (primeros 5):" +
                Environment.NewLine + string.Join(Environment.NewLine, errores);
            int totalFacturas = datos.Rows.Cast<DataRow>()
                .Select(r => Convert.ToString(r["NumeroFactura"]).Trim()).Distinct(StringComparer.OrdinalIgnoreCase).Count();
            MessageBox.Show("Facturas importadas: " + correctas + " de " + totalFacturas +
                "." + detalleErrores, "Resultado de importación",
                MessageBoxButtons.OK, errores.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correctas > 0)
            {
                vista.Close();
                ConsultarRelacional();
                ConfigurarComboVentas();
            }
        }

        private void ActualizarBotonAbrirSeleccionada()
        {
            if (btnAbrirFacturaSeleccionada != null) btnAbrirFacturaSeleccionada.Enabled = ObtenerSeleccionado() != null;
        }

        private void AbrirFacturaCompleta(FacturacionEntidad factura)
        {
            using (FrmMaestroDetalle<FacturacionEntidad, DetalleFacturaEntidad> formulario = new FrmMaestroDetalle<FacturacionEntidad, DetalleFacturaEntidad>(
                "Factura", "CodigoFactura", "CodigoDetalleFactura", "CodigoFactura",
                new[] { "Total" }, new[] { "Iva", "Subtotal" }, factura))
                if (formulario.ShowDialog(this) == DialogResult.OK) ConsultarRelacional();
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Label opcion = sender as Label;
            if (opcion != null && opcion.Tag != null)
                Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoFactura", chkCodigo.Checked);
            CambiarVisibilidad("CodigoVenta", chkCodigoVenta.Checked);
            CambiarVisibilidad("NumeroFactura", chkNumeroFactura.Checked);
            CambiarVisibilidad("FechaEmision", chkFechaEmision.Checked);
            CambiarVisibilidad("NombreCliente", chkNombreCliente.Checked);
            CambiarVisibilidad("Nit", chkNit.Checked);
            CambiarVisibilidad("Total", chkTotal.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<FacturacionEntidad> registros = relacional.Listar<FacturacionEntidad>();
                facturacion.Clear();
                foreach (FacturacionEntidad registro in registros) facturacion.Add(registro);
                MostrarFacturacion(facturacion.ToList());
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActualizarContador();
            }
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvFacturacion.Columns.Contains(columna))
                dgvFacturacion.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvFacturacion.Rows.Count > 0)
                {
                    int indice = dgvFacturacion.CurrentRow == null ? 0 : dgvFacturacion.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvFacturacion.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvFacturacion.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvFacturacion.Rows.Count)
                return;

            dgvFacturacion.ClearSelection();

            DataGridViewCell celda = dgvFacturacion.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvFacturacion.CurrentCell = celda;

            dgvFacturacion.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            try
            {
                int codigo = relacional.Agregar(CrearEntidad());
                ConsultarRelacional();
                ConfigurarComboVentas();
                LimpiarFormulario();
                MessageBox.Show("Registro agregado correctamente. Código: " + codigo,
                    "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (!AjustesDisenoCrud.PermitirEdicion(this)) return;
            FacturacionEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoFactura, CrearEntidad());
                ConsultarRelacional();
                ConfigurarComboVentas();
                LimpiarFormulario();
                MessageBox.Show("Registro actualizado correctamente.", "Editar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al editar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            FacturacionEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoFactura + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.EliminarConDetalles<FacturacionEntidad, DetalleFacturaEntidad>(
                    seleccionado.CodigoFactura, "CodigoFactura");
                ConsultarRelacional();
                ConfigurarComboVentas();
                LimpiarFormulario();
                MessageBox.Show("La factura y sus detalles quedaron inactivos.", "Eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private FacturacionEntidad CrearEntidad()
        {
            return new FacturacionEntidad(
                ComboFK.Obtener(cmbCodigoVenta).Value,
                txtNumeroFactura.Text.Trim(),
                dtpFechaEmision.Value,
                txtNombreCliente.Text.Trim(),
                ValorONulo(txtNit.Text),
                nudTotal.Value,
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoVenta).HasValue)
            {
                MessageBox.Show("El campo Cód. venta debe ser mayor que cero.");
                cmbCodigoVenta.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroFactura.Text))
            {
                MessageBox.Show("El campo Número de factura es obligatorio.");
                txtNumeroFactura.Focus();
                return false;
            }

            if (txtNumeroFactura.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Número de factura no puede superar 100 caracteres.");
                txtNumeroFactura.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("El campo Nombre del cliente es obligatorio.");
                txtNombreCliente.Focus();
                return false;
            }

            if (txtNombreCliente.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Nombre del cliente no puede superar 100 caracteres.");
                txtNombreCliente.Focus();
                return false;
            }

            if (txtNit.Text.Trim().Length > 45)
            {
                MessageBox.Show("El campo NIT no puede superar 45 caracteres.");
                txtNit.Focus();
                return false;
            }

            if (nudTotal.Value <= 0)
            {
                MessageBox.Show("El campo Total debe ser mayor que cero.");
                nudTotal.Focus();
                return false;
            }

            return true;
        }

        private FacturacionEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvFacturacion.CurrentRow == null)
                return null;

            return dgvFacturacion.CurrentRow.DataBoundItem as FacturacionEntidad;
        }

        private void DgvFacturacion_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            FacturacionEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoFactura.ToString();
            ComboFK.Seleccionar(cmbCodigoVenta, registro.CodigoVenta);
            txtNumeroFactura.Text = registro.NumeroFactura;
            dtpFechaEmision.Value = LimitarFecha(dtpFechaEmision, registro.FechaEmision);
            txtNombreCliente.Text = registro.NombreCliente;
            txtNit.Text = registro.Nit;
            nudTotal.Value = Limitar(nudTotal, registro.Total);
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
        }

        private decimal Limitar(NumericUpDown control, decimal valor)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, valor));
        }

        private int ObtenerCodigoVentaSeleccionado()
        {
            return ComboFK.Obtener(cmbCodigoVenta) ?? 0;
        }

        private string DescripcionVenta(VentasEntidad venta)
        {
            string nombreCliente;
            return "Venta " + venta.FechaVenta.ToString("dd/MM/yyyy") + " | Cliente " +
                (nombresClientesVentas.TryGetValue(venta.CodigoCliente, out nombreCliente) ? nombreCliente : "#" + venta.CodigoCliente);
        }

        private void SeleccionarCodigoVenta(int codigoVenta)
        {
            int indice = ventasDisponibles.FindIndex(v => v.CodigoVenta == codigoVenta);
            VentasEntidad venta = null;
            if (indice < 0)
                venta = relacional.Listar<VentasEntidad>().FirstOrDefault(v => v.CodigoVenta == codigoVenta);
            if (indice < 0 && venta != null)
            {
                ventasDisponibles.Add(venta);
                ComboFK.Cargar(cmbCodigoVenta, ventasDisponibles, v => v.CodigoVenta, DescripcionVenta);
            }
            ComboFK.Seleccionar(cmbCodigoVenta, codigoVenta);
        }

        private DateTime LimitarFecha(DateTimePicker control, DateTime valor)
        {
            if (valor < control.MinDate) return control.MinDate;
            if (valor > control.MaxDate) return control.MaxDate;
            return valor;
        }

        // Los campos opcionales se guardan como NULL cuando se dejan vacíos.
        private static string ValorONulo(string texto)
        {
            string valor = texto.Trim();
            return valor.Length == 0 ? null : valor;
        }

        private static bool Contiene(string valor, string criterio)
        {
            return valor != null && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void BuscarFacturacion()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarFacturacion(facturacion.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<FacturacionEntidad> resultado = relacional.Buscar<FacturacionEntidad>(criterio);
                MostrarFacturacion(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarFacturacion(List<FacturacionEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvFacturacion.DataSource = origen;
            ActualizarColumnas();
            dgvFacturacion.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoVenta, null);
            txtNumeroFactura.Clear();
            dtpFechaEmision.Value = DateTime.Now;
            txtNombreCliente.Clear();
            txtNit.Clear();
            nudTotal.Value = 0;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvFacturacion.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoVenta.Focus();
        }

        private void ActualizarContador()
        {
            ActualizarDatosDashboardFacturacion();
        }
        private void CargarCatalogosFK()
        {
            try
            {
                ComboFK.Cargar(cmbCodigoVenta, relacional.ListarCatalogo<VentasEntidad>(), x => x.CodigoVenta, x => "Venta | " + x.FechaVenta.ToString("dd/MM/yyyy"), false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
