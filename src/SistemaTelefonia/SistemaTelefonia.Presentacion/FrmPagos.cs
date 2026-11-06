using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmPagos : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<PagosEntidad> pagos = new BindingList<PagosEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;
        private readonly List<FacturacionEntidad> facturasPendientes = new List<FacturacionEntidad>();
        private DashboardCrudSimple dashboard;
        private TableLayoutPanel layoutBloquesPagos;
        private TableLayoutPanel layoutOpcionesColumnasPagos;
        private TableLayoutPanel layoutBusquedaPagos;
        private TableLayoutPanel layoutEncabezadoPagos;
        private IconPictureBox iconoMarcaPagos;
        private CheckBox chkActivarTodosFiltros;
        private ComboBox cboFiltroEstadoPagos;
        private bool actualizandoFiltrosPagos;

        public FrmPagos()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AplicarModeloVisualPagos();
            CargarCatalogosFK();

            origen.DataSource = pagos;
            dgvPagos.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvPagos);
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            TemaVisualCrud.Aplicar(this);
            AplicarTipografiaModeloUsuariosPagos();
            ConfigurarBusquedaModeloPagos();
            ConfigurarOpcionesColumnasPagos();
            ConfigurarDashboardPagos();
            ConstruirOrdenBloquesPagos();
            ConstruirNavegacionLateral();

            ConectarEventos();
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            ConfigurarResaltadoCamposPagos();
            ConfigurarNavegacionTecladoPagos();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
            ConsultarFacturasPendientes();
            Shown += async (s, e) => await dashboard.CargarAsync();
            SizeChanged += (s, e) => AjustarInterfazPagos();
        }

        private void AplicarModeloVisualPagos()
        {
            pnlHeader.Height = 108; pnlHeader.Dock = DockStyle.None; pnlHeader.Padding = new Padding(22, 8, 15, 8);
            pnlHeader.Controls.Remove(lblTitulo); pnlHeader.Controls.Remove(lblSubtitulo);
            layoutEncabezadoPagos = new TableLayoutPanel { Name = "layoutEncabezadoPagos", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 2, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = pnlHeader.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize };
            layoutEncabezadoPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadoPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutEncabezadoPagos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            lblTitulo.Dock = DockStyle.Fill; lblTitulo.Height = 48; lblTitulo.Margin = Padding.Empty; lblTitulo.Padding = Padding.Empty;
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft; lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblSubtitulo.Dock = DockStyle.Fill; lblSubtitulo.Height = 42; lblSubtitulo.Margin = Padding.Empty;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleLeft; lblSubtitulo.Padding = Padding.Empty; lblSubtitulo.Font = new Font("Segoe UI", 10F);
            layoutEncabezadoPagos.Controls.Add(lblTitulo, 0, 0);
            layoutEncabezadoPagos.Controls.Add(lblSubtitulo, 0, 1);
            pnlHeader.Controls.Add(layoutEncabezadoPagos);
            pnlContenido.Padding = new Padding(14); pnlContenido.Dock = DockStyle.None; pnlContenido.AutoScroll = true; pnlFormulario.Height = 260; pnlAcciones.Height = 58;
            lblFormularioTitulo.Height = 40; lblCatalogoTitulo.Height = 40; lblColumnas.Height = 40; pnlBusqueda.Height = 54; pnlColumnas.Height = 76;
            selector.WrapContents = false; selector.AutoScroll = true; chkSeleccionar.Text = "Seleccionar registro"; chkSeleccionar.AutoSize = true;
            chkSeleccionar.Font = new Font("Segoe UI", 9.5F);
            chkActivarTodosFiltros = new CheckBox { Name = "chkActivarFiltros", Text = "Activar todos los filtros", AutoSize = true,
                BackColor = Color.Transparent, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F), Margin = new Padding(5, 3, 9, 3), UseVisualStyleBackColor = false };
            selector.Controls.Remove(chkSeleccionar);
            foreach (CheckBox filtro in selector.Controls.OfType<CheckBox>().Where(x => x != chkSeleccionar && x != chkActivarTodosFiltros)) filtro.Checked = true;
            chkActivarTodosFiltros.Checked = true;
            foreach (CheckBox f in selector.Controls.OfType<CheckBox>()) { f.AutoSize = true; f.Font = new Font("Segoe UI", 9F); f.Margin = new Padding(2, 3, 5, 3); }
            chkSeleccionar.Margin = new Padding(8, 4, 4, 0);
            lblRegistros.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); lblRegistros.ForeColor = Color.White; lblRegistros.AutoEllipsis = true;
            lblFormularioTitulo.TextAlign = ContentAlignment.MiddleLeft;
            dgvPagos.Dock = DockStyle.Top; dgvPagos.RowTemplate.Height = 32; dgvPagos.ColumnHeadersHeight = 40; dgvPagos.ScrollBars = ScrollBars.Horizontal;
            foreach (ComboBox combo in new[] { cmbCodigoFactura, cmbTipoPago, cmbMoneda })
            { combo.Font = new Font("Segoe UI", 9.5F); combo.BackColor = ColorTranslator.FromHtml("#E1ECF6"); combo.ForeColor = ColorTranslator.FromHtml("#072342"); }
            cmbTipoPago.DropDownStyle = ComboBoxStyle.DropDownList; cmbMoneda.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (Control campo in new Control[] { txtCodigo, cmbCodigoFactura, cmbTipoPago, nudMonto, cmbMoneda, dtpFechaPago, txtReferencia })
            { campo.Font = new Font("Segoe UI", 9.5F); campo.BackColor = ColorTranslator.FromHtml("#E1ECF6"); campo.ForeColor = ColorTranslator.FromHtml("#072342"); }
            foreach (Label etiqueta in layoutCampos.Controls.OfType<Label>()) { etiqueta.Font = new Font("Segoe UI", 9F, FontStyle.Bold); etiqueta.ForeColor = Color.White; }
            foreach (RadioButton r in panelEstado.Controls.OfType<RadioButton>()) { r.Font = new Font("Segoe UI", 9.5F); r.ForeColor = Color.White; }
            lblTitulo.Text = "Gestión de pagos"; lblSubtitulo.Text = "Administra los pagos asociados a las facturas";
            lblFormularioTitulo.Text = "Información del pago"; lblCatalogoTitulo.Text = "Pagos registrados";
            pnlHeader.BackColor = ColorTranslator.FromHtml("#032B53"); pnlFormulario.BackColor = ColorTranslator.FromHtml("#072342");
            pnlCatalogo.BackColor = ColorTranslator.FromHtml("#072342"); pnlAcciones.BackColor = ColorTranslator.FromHtml("#03162B");
            int ancho = Math.Min(195, Math.Max(148, (int)(ClientSize.Width * .18))); pnlMenu.Width = ancho;
            pnlHeader.SetBounds(ancho, 0, Math.Max(0, ClientSize.Width - ancho), 108);
            pnlContenido.SetBounds(ancho, 108, Math.Max(0, ClientSize.Width - ancho), Math.Max(0, ClientSize.Height - 108));
            pnlHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenido.SizeChanged += (s, e) => AjustarCatalogoModeloPagos(); pnlCatalogo.SizeChanged += (s, e) => AjustarCatalogoModeloPagos();
        }

        private void AplicarTipografiaModeloUsuariosPagos()
        {
            // Mantener el mismo estilo que FrmUsuarios, después de que el tema común
            // haya aplicado colores y tamaños al resto del formulario.
            lblTitulo.Text = "Gestión de pagos";
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Height = 48;
            lblTitulo.Margin = Padding.Empty;
            lblTitulo.Padding = Padding.Empty;
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblSubtitulo.Dock = DockStyle.Fill;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleLeft;
            layoutEncabezadoPagos.BackColor = pnlHeader.BackColor;

            lblCatalogoTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblColumnas.Font = lblCatalogoTitulo.Font;
            lblColumnas.ForeColor = lblCatalogoTitulo.ForeColor;
        }

        private void ConfigurarDashboardPagos()
        { dashboard = new DashboardCrudSimple("Resumen de pagos", "Pagos por tipo", ObtenerDatosDashboardPagos); }

        private void ConfigurarBusquedaModeloPagos()
        {
            cboFiltroEstadoPagos = Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault();
            if (cboFiltroEstadoPagos == null) return;

            Panel panelFiltro = Controls.Find("pnlFiltroEstadoComun", true).OfType<Panel>().FirstOrDefault();
            if (panelFiltro != null)
            {
                panelFiltro.Controls.Remove(cboFiltroEstadoPagos);
                panelFiltro.Controls.Remove(btnBuscar);
                pnlBusqueda.Controls.Remove(panelFiltro);
                panelFiltro.Dispose();
            }
            pnlBusqueda.Controls.Remove(txtBuscar);
            pnlBusqueda.Controls.Clear();

            layoutBusquedaPagos = new TableLayoutPanel { Name = "layoutBusquedaPagos", Dock = DockStyle.Fill,
                ColumnCount = 3, RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlBusqueda.BackColor };
            layoutBusquedaPagos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBusquedaPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            layoutBusquedaPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBusquedaPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Math.Max(96, btnNuevo.Width)));
            cboFiltroEstadoPagos.Dock = DockStyle.Fill; txtBuscar.Dock = DockStyle.Fill; btnBuscar.Dock = DockStyle.Fill;
            cboFiltroEstadoPagos.Margin = new Padding(0, 0, 8, 0); txtBuscar.Margin = new Padding(0, 0, 8, 0); btnBuscar.Margin = Padding.Empty;
            layoutBusquedaPagos.Controls.Add(cboFiltroEstadoPagos, 0, 0);
            layoutBusquedaPagos.Controls.Add(txtBuscar, 1, 0);
            layoutBusquedaPagos.Controls.Add(btnBuscar, 2, 0);
            pnlBusqueda.Controls.Add(layoutBusquedaPagos);
            pnlAcciones.SizeChanged += (s, e) =>
            {
                if (layoutBusquedaPagos != null && layoutBusquedaPagos.ColumnStyles.Count == 3)
                    layoutBusquedaPagos.ColumnStyles[2].Width = Math.Max(72, btnNuevo.Width);
            };
            layoutBusquedaPagos.ColumnStyles[2].Width = Math.Max(72, btnNuevo.Width);
        }

        private void ConfigurarOpcionesColumnasPagos()
        {
            pnlColumnas.Controls.Remove(selector);
            pnlColumnas.Controls.Remove(lblColumnas);
            layoutOpcionesColumnasPagos = new TableLayoutPanel { Name = "layoutOpcionesColumnasPagos", Dock = DockStyle.Fill,
                ColumnCount = 3, RowCount = 2, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlColumnas.BackColor };
            layoutOpcionesColumnasPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170));
            layoutOpcionesColumnasPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutOpcionesColumnasPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            layoutOpcionesColumnasPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, lblColumnas.Height));
            layoutOpcionesColumnasPagos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            lblColumnas.Dock = DockStyle.Fill; lblColumnas.TextAlign = ContentAlignment.MiddleLeft;
            lblColumnas.Font = lblCatalogoTitulo.Font;
            lblColumnas.ForeColor = lblCatalogoTitulo.ForeColor;
            chkActivarTodosFiltros.Anchor = AnchorStyles.Left; chkSeleccionar.Anchor = AnchorStyles.Right;
            selector.Dock = DockStyle.Fill; selector.WrapContents = false; selector.AutoScroll = true; selector.FlowDirection = FlowDirection.LeftToRight;
            layoutOpcionesColumnasPagos.Controls.Add(lblColumnas, 0, 0);
            layoutOpcionesColumnasPagos.SetColumnSpan(lblColumnas, 3);
            layoutOpcionesColumnasPagos.Controls.Add(chkActivarTodosFiltros, 0, 1);
            layoutOpcionesColumnasPagos.Controls.Add(selector, 1, 1);
            layoutOpcionesColumnasPagos.Controls.Add(chkSeleccionar, 2, 1);
            pnlColumnas.Controls.Add(layoutOpcionesColumnasPagos);
            chkActivarTodosFiltros.CheckedChanged += (s, e) =>
            {
                if (actualizandoFiltrosPagos) return;
                actualizandoFiltrosPagos = true;
                try { foreach (CheckBox filtro in selector.Controls.OfType<CheckBox>()) filtro.Checked = chkActivarTodosFiltros.Checked; }
                finally { actualizandoFiltrosPagos = false; }
                ActualizarColumnas();
            };
            foreach (CheckBox filtro in selector.Controls.OfType<CheckBox>())
                filtro.CheckedChanged += (s, e) =>
                {
                    if (actualizandoFiltrosPagos) return;
                    actualizandoFiltrosPagos = true;
                    try { chkActivarTodosFiltros.Checked = selector.Controls.OfType<CheckBox>().All(x => x.Checked); }
                    finally { actualizandoFiltrosPagos = false; }
                    ActualizarColumnas();
                };
            pnlColumnas.SizeChanged += (s, e) => AjustarAnchosOpcionesColumnasPagos();
            AjustarAnchosOpcionesColumnasPagos();
        }

        private void AjustarAnchosOpcionesColumnasPagos()
        {
            if (layoutOpcionesColumnasPagos == null || layoutOpcionesColumnasPagos.ColumnStyles.Count < 3) return;
            int anchoActivar = TextRenderer.MeasureText(chkActivarTodosFiltros.Text, chkActivarTodosFiltros.Font).Width +
                28 + chkActivarTodosFiltros.Margin.Horizontal + 12;
            int anchoSeleccionar = TextRenderer.MeasureText(chkSeleccionar.Text, chkSeleccionar.Font).Width +
                28 + chkSeleccionar.Margin.Horizontal + 12;
            layoutOpcionesColumnasPagos.ColumnStyles[0].Width = anchoActivar;
            layoutOpcionesColumnasPagos.ColumnStyles[2].Width = anchoSeleccionar;
            layoutOpcionesColumnasPagos.PerformLayout();
        }

        private DashboardCrudDatos ObtenerDatosDashboardPagos()
        {
            List<PagosEntidad> lista = relacional.Listar<PagosEntidad>(); List<PagosEntidad> activos = lista.Where(x => x.Estado).ToList();
            return new DashboardCrudDatos
            {
                TitulosIndicadores = new[] { "PAGOS REGISTRADOS", "PAGOS ACTIVOS", "PAGOS INACTIVOS", "MONTO REGISTRADO" },
                ValoresIndicadores = new[] { lista.Count.ToString("N0"), activos.Count.ToString("N0"), lista.Count(x => !x.Estado).ToString("N0"), activos.Sum(x => x.Monto).ToString("C2") },
                Serie = lista.Where(x => !string.IsNullOrWhiteSpace(x.TipoPago)).GroupBy(x => x.TipoPago.Trim(), StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Take(8).Select(x => new KeyValuePair<string, int>(x.Key, x.Count())).ToList(),
                Alertas = "• Facturas asociadas: " + lista.Select(x => x.CodigoFactura).Distinct().Count() + Environment.NewLine + "• Referencias vacías: " + activos.Count(x => string.IsNullOrWhiteSpace(x.Referencia)),
                Actividad = activos.Count == 0 ? "No hay pagos activos." : string.Join(Environment.NewLine, activos.OrderByDescending(x => x.FechaPago).Take(4).Select(x => "• " + x.TipoPago + " · " + x.Monto.ToString("C2")))
            };
        }

        private void ConstruirOrdenBloquesPagos()
        {
            layoutBloquesPagos = new TableLayoutPanel { Name = "layoutBloquesPagos", Dock = DockStyle.Top, ColumnCount = 1, RowCount = 5,
                Padding = Padding.Empty, Margin = Padding.Empty, BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize };
            layoutBloquesPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, 360)); layoutBloquesPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            layoutBloquesPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlFormulario.Height)); layoutBloquesPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlAcciones.Height));
            layoutBloquesPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlCatalogo.Height));
            pnlContenido.Controls.Clear(); pnlContenido.Controls.Add(layoutBloquesPagos);
            layoutBloquesPagos.Controls.Add(dashboard, 0, 0); layoutBloquesPagos.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = pnlContenido.BackColor }, 0, 1);
            layoutBloquesPagos.Controls.Add(pnlFormulario, 0, 2); layoutBloquesPagos.Controls.Add(pnlAcciones, 0, 3); layoutBloquesPagos.Controls.Add(pnlCatalogo, 0, 4);
            pnlFormulario.Dock = pnlAcciones.Dock = pnlCatalogo.Dock = DockStyle.Fill; dashboard.Dock = DockStyle.Fill; pnlContenido.AutoScroll = true;
            AjustarCatalogoModeloPagos();
        }

        private void AjustarInterfazPagos()
        {
            if (IsDisposed || layoutBloquesPagos == null) return;
            int ancho = Math.Min(195, Math.Max(148, (int)(ClientSize.Width * .18))); pnlMenu.Width = ancho;
            pnlHeader.SetBounds(ancho, 0, Math.Max(0, ClientSize.Width - ancho), 108);
            pnlContenido.SetBounds(ancho, 108, Math.Max(0, ClientSize.Width - ancho), Math.Max(0, ClientSize.Height - 108)); AjustarCatalogoModeloPagos();
            int altoCatalogo = pnlCatalogo.Height; int altoDashboard = Math.Max(360, pnlContenido.ClientSize.Height - pnlContenido.Padding.Vertical - 10 - pnlFormulario.Height - pnlAcciones.Height - altoCatalogo);
            layoutBloquesPagos.RowStyles[0].Height = altoDashboard; layoutBloquesPagos.RowStyles[2].Height = pnlFormulario.Height;
            layoutBloquesPagos.RowStyles[3].Height = pnlAcciones.Height; layoutBloquesPagos.RowStyles[4].Height = altoCatalogo;
            layoutBloquesPagos.Height = altoDashboard + 10 + pnlFormulario.Height + pnlAcciones.Height + altoCatalogo;
        }

        private void AjustarCatalogoModeloPagos()
        {
            if (pnlCatalogo == null || dgvPagos == null) return;
            int altoGrilla = dgvPagos.ColumnHeadersHeight + 10 * dgvPagos.RowTemplate.Height + 2; dgvPagos.Height = altoGrilla;
            Panel pie = pnlCatalogo.Controls.Find("pnlPaginacion", false).OfType<Panel>().FirstOrDefault(); if (pie != null) pie.Height = 48;
            pnlCatalogo.Height = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height + pnlColumnas.Height + altoGrilla + (pie == null ? lblRegistros.Height : pie.Height);
        }

        private void ConfigurarResaltadoCamposPagos()
        {
            Control[] campos = { cmbCodigoFactura, cmbTipoPago, nudMonto, cmbMoneda, dtpFechaPago, txtReferencia };
            foreach (Control campo in campos)
            {
                CampoRedondeado.Aplicar(campo, 7);
                campo.MouseEnter += (s, e) => { if (campo.Enabled && !campo.Focused) campo.BackColor = Color.FromArgb(205, 243, 255); };
                campo.MouseLeave += (s, e) => RestaurarColorCampoPago(campo); campo.Enter += (s, e) => { if (campo.Enabled) campo.BackColor = ColorTranslator.FromHtml("#80DEFF"); };
                campo.Leave += (s, e) => RestaurarColorCampoPago(campo); campo.EnabledChanged += (s, e) => RestaurarColorCampoPago(campo);
            }
        }

        private static void RestaurarColorCampoPago(Control campo)
        { if (campo == null || campo.IsDisposed) return; campo.BackColor = campo.Enabled ? ColorTranslator.FromHtml("#E1ECF6") : Color.FromArgb(215, 222, 230); campo.ForeColor = campo.Enabled ? ColorTranslator.FromHtml("#072342") : Color.FromArgb(104, 116, 128); }

        private void ConfigurarNavegacionTecladoPagos()
        { Control[] orden = { cmbCodigoFactura, cmbTipoPago, nudMonto, cmbMoneda, dtpFechaPago, txtReferencia, rdoActivo, rdoInactivo }; foreach (Control c in orden) c.KeyDown += CampoPago_KeyDown; AcceptButton = null; }

        private void CampoPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Alt || e.Control || e.Shift) return; ComboBox combo = sender as ComboBox; if (combo != null && combo.DroppedDown) return;
            Control actual = sender as Control; if (actual == null || !ValidarCampoParaAvanzarPago(actual)) return; e.SuppressKeyPress = true; e.Handled = true;
            Control[] orden = { cmbCodigoFactura, cmbTipoPago, nudMonto, cmbMoneda, dtpFechaPago, txtReferencia, rdoActivo, rdoInactivo };
            for (int i = Array.IndexOf(orden, actual) + 1; i < orden.Length; i++) if (orden[i].Enabled && orden[i].Visible) { orden[i].Focus(); return; }
        }

        private bool ValidarCampoParaAvanzarPago(Control campo)
        {
            if (campo == cmbCodigoFactura && !ComboFK.Obtener(cmbCodigoFactura).HasValue) { cmbCodigoFactura.Focus(); return false; }
            if (campo == cmbTipoPago && (string.IsNullOrWhiteSpace(cmbTipoPago.Text) || cmbTipoPago.Text.Trim().Length > 100)) { cmbTipoPago.Focus(); return false; }
            if (campo == nudMonto && nudMonto.Value <= 0) { nudMonto.Focus(); return false; }
            if (campo == cmbMoneda && (string.IsNullOrWhiteSpace(cmbMoneda.Text) || cmbMoneda.Text.Trim().Length > 10)) { cmbMoneda.Focus(); return false; }
            if (campo == txtReferencia && txtReferencia.Text.Trim().Length > 100) { txtReferencia.Focus(); return false; }
            return true;
        }

        private void ConstruirNavegacionLateral()
        {
            Dictionary<string, Label> referencias = pnlItemsMenu.Controls.OfType<Label>().Where(x => x.Tag != null)
                .GroupBy(x => NormalizarDestinoPagos(Convert.ToString(x.Tag))).ToDictionary(g => g.Key, g => g.First());
            pnlItemsMenu.Controls.Clear();
            TableLayoutPanel filas = new TableLayoutPanel { Name = "layoutBotonesBarraLateral", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 18, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = ColorTranslator.FromHtml("#061D30") };
            filas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 18; i++) filas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 18));
            IconButton volver = CrearBotonLateralPagos("btnVolverMenu", "Volver al menú principal", IconChar.Home, null);
            volver.BackColor = ColorTranslator.FromHtml("#08243A"); volver.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#526779"); volver.FlatAppearance.BorderSize = 1;
            volver.Click += (s, e) => Navegacion.VolverAlMenu(this); filas.Controls.Add(volver, 0, 0);
            string[] destinos = { "FrmUsuarios", "FrmSucursales", "FrmEmpleados", "FrmClientes", "FrmPlanes", "FrmLineas", "FrmProveedores", "FrmMarcas", "FrmEquipos", "FrmCompras", "FrmInventario", "FrmVentas", "FrmFacturacion", "FrmPagos", "FrmCardex", "FrmReclamos" };
            for (int i = 0; i < destinos.Length; i++)
            {
                Label referencia;
                if (!referencias.TryGetValue(destinos[i], out referencia)) continue;
                string texto = referencia.Text.Trim().TrimStart('›', '⌂', '▸', ' ').Trim();
                IconButton boton = CrearBotonLateralPagos("btn" + destinos[i].Substring(3), texto, IconoModuloPagos(destinos[i]), destinos[i]);
                filas.Controls.Add(boton, 0, i + 1);
            }
            IconButton salir = CrearBotonLateralPagos("btnCerrarSesion", "Cerrar sesión", IconChar.SignOutAlt, null);
            salir.BackColor = ColorTranslator.FromHtml("#A83F48"); salir.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#D56A70"); salir.FlatAppearance.BorderSize = 1;
            salir.IconColor = Color.White;
            salir.Click += (s, e) => Navegacion.CerrarSesion(this); filas.Controls.Add(salir, 0, 17);
            pnlMenu.SuspendLayout(); pnlMenu.Controls.Clear();
            pnlMenu.Padding = Padding.Empty; pnlMenu.BackColor = ColorTranslator.FromHtml("#061D30");
            pnlMenu.Paint += (s, e) => { using (Pen borde = new Pen(ColorTranslator.FromHtml("#34495E"), 1)) e.Graphics.DrawRectangle(borde, 0, 0, pnlMenu.ClientSize.Width - 1, pnlMenu.ClientSize.Height - 1); };
            pnlLogo.SetBounds(8, 8, pnlMenu.ClientSize.Width - 16, 142); pnlLogo.BackColor = ColorTranslator.FromHtml("#032B53");
            pnlLogo.Controls.Remove(lblLogo);
            lblSistema.Text = "Sistema de Telefonía\ny Accesorios";
            lblSistema.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            iconoMarcaPagos = new IconPictureBox
            {
                Name = "icoMarcaTelefonia",
                IconChar = IconChar.MobileAlt,
                IconFont = IconFont.Auto,
                IconColor = Color.FromArgb(0, 203, 255),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(42, 42),
                Location = new Point((pnlLogo.ClientSize.Width - 42) / 2, 8),
                Anchor = AnchorStyles.Top
            };
            pnlLogo.Controls.Add(iconoMarcaPagos);
            iconoMarcaPagos.BringToFront();
            lblSistema.Dock = DockStyle.None; lblSistema.SetBounds(6, 52, pnlLogo.ClientSize.Width - 12, 82);
            lblSistema.TextAlign = ContentAlignment.MiddleCenter;
            lblSistema.ForeColor = Color.White;
            lblUsuario.SetBounds(8, 150, pnlMenu.ClientSize.Width - 16, 24); lblUsuarioRol.SetBounds(8, 174, pnlMenu.ClientSize.Width - 16, 24);
            lblUsuario.TextAlign = lblUsuarioRol.TextAlign = ContentAlignment.MiddleCenter;
            lblUsuario.ForeColor = Color.White; lblUsuarioRol.ForeColor = ColorTranslator.FromHtml("#AABED2");
            pnlItemsMenu.SetBounds(8, 198, pnlMenu.ClientSize.Width - 16, pnlMenu.ClientSize.Height - 206);
            pnlLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; lblUsuario.Anchor = lblUsuarioRol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlItemsMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right; pnlItemsMenu.Controls.Add(filas);
            CampoRedondeado.Aplicar(pnlLogo, 12);
            pnlMenu.Controls.Add(pnlItemsMenu); pnlMenu.Controls.Add(lblUsuarioRol); pnlMenu.Controls.Add(lblUsuario); pnlMenu.Controls.Add(pnlLogo); pnlMenu.ResumeLayout(true);
            pnlMenu.SizeChanged += (s, e) => { int width = Math.Max(0, pnlMenu.ClientSize.Width - 16); pnlLogo.Width = width; iconoMarcaPagos.Left = (pnlLogo.ClientSize.Width - iconoMarcaPagos.Width) / 2; lblSistema.Width = Math.Max(0, pnlLogo.ClientSize.Width - 12); lblUsuario.Width = lblUsuarioRol.Width = width; pnlItemsMenu.SetBounds(8, 198, width, Math.Max(0, pnlMenu.ClientSize.Height - 206)); };
        }

        private IconButton CrearBotonLateralPagos(string nombre, string texto, IconChar icono, string destino)
        {
            IconButton boton = new IconButton { Name = nombre, Text = texto, Tag = destino, Dock = DockStyle.Fill, Margin = Padding.Empty,
                Padding = new Padding(12, 0, 4, 0), TextAlign = ContentAlignment.MiddleLeft, ImageAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText, IconChar = icono, IconFont = IconFont.Auto, IconColor = ColorTranslator.FromHtml("#00D9FF"),
                IconSize = 20, BackColor = ColorTranslator.FromHtml("#061D30"), ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F), Cursor = Cursors.Hand, UseVisualStyleBackColor = false };
            boton.FlatAppearance.BorderSize = 0;
            if (destino != null)
            {
                boton.Click += MenuOpcion_Click;
                boton.MouseEnter += (s, e) => { boton.BackColor = ColorTranslator.FromHtml("#0B3558"); boton.ForeColor = ColorTranslator.FromHtml("#00D9FF"); };
                boton.MouseLeave += (s, e) => { bool activo = destino == GetType().Name; boton.BackColor = ColorTranslator.FromHtml(activo ? "#0B3558" : "#061D30"); boton.ForeColor = activo ? ColorTranslator.FromHtml("#00D9FF") : Color.White; };
                if (destino == GetType().Name) { boton.BackColor = ColorTranslator.FromHtml("#0B3558"); boton.ForeColor = ColorTranslator.FromHtml("#00D9FF"); }
            }
            else
            {
                Color baseColor = nombre == "btnCerrarSesion" ? ColorTranslator.FromHtml("#A83F48") : ColorTranslator.FromHtml("#08243A");
                boton.MouseEnter += (s, e) => boton.BackColor = nombre == "btnCerrarSesion" ? ColorTranslator.FromHtml("#B9434A") : ColorTranslator.FromHtml("#005684");
                boton.MouseLeave += (s, e) => boton.BackColor = baseColor;
            }
            return boton;
        }

        private static string NormalizarDestinoPagos(string destino)
        { return string.IsNullOrWhiteSpace(destino) ? string.Empty : (destino.StartsWith("Frm", StringComparison.OrdinalIgnoreCase) ? destino : "Frm" + destino); }

        private static IconChar IconoModuloPagos(string destino)
        {
            switch (destino)
            {
                case "FrmUsuarios": return IconChar.UserShield; case "FrmSucursales": return IconChar.Building; case "FrmEmpleados": return IconChar.UserTie;
                case "FrmClientes": return IconChar.Users; case "FrmPlanes": return IconChar.Wifi; case "FrmLineas": return IconChar.SimCard;
                case "FrmProveedores": return IconChar.Truck; case "FrmMarcas": return IconChar.Tags; case "FrmEquipos": return IconChar.MobileAlt;
                case "FrmCompras": return IconChar.CartPlus; case "FrmInventario": return IconChar.Boxes; case "FrmVentas": return IconChar.ShoppingCart;
                case "FrmFacturacion": return IconChar.FileInvoiceDollar; case "FrmPagos": return IconChar.CreditCard; case "FrmCardex": return IconChar.ChartLine;
                default: return IconChar.Headset;
            }
        }
        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                MostrarPagos(pagos.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarPagos();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarPagos();
                    e.SuppressKeyPress = true;
                }
            };

            dgvPagos.SelectionChanged += DgvPagos_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoFactura.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTipoPago.CheckedChanged += (s, e) => ActualizarColumnas();
            chkMonto.CheckedChanged += (s, e) => ActualizarColumnas();
            chkMoneda.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaPago.CheckedChanged += (s, e) => ActualizarColumnas();
            chkReferencia.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Control opcion = sender as Control;
            if (opcion != null && opcion.Tag != null) Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoPago", chkCodigo.Checked);
            CambiarVisibilidad("CodigoFactura", chkCodigoFactura.Checked);
            CambiarVisibilidad("TipoPago", chkTipoPago.Checked);
            CambiarVisibilidad("Monto", chkMonto.Checked);
            CambiarVisibilidad("Moneda", chkMoneda.Checked);
            CambiarVisibilidad("FechaPago", chkFechaPago.Checked);
            CambiarVisibilidad("Referencia", chkReferencia.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<PagosEntidad> registros = relacional.Listar<PagosEntidad>();
                pagos.Clear();
                foreach (PagosEntidad registro in registros) pagos.Add(registro);
                MostrarPagos(pagos.ToList());
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
            if (dgvPagos.Columns.Contains(columna))
                dgvPagos.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvPagos.Rows.Count > 0)
                {
                    int indice = dgvPagos.CurrentRow == null ? 0 : dgvPagos.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvPagos.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvPagos.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvPagos.Rows.Count)
                return;

            dgvPagos.ClearSelection();

            DataGridViewCell celda = dgvPagos.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvPagos.CurrentCell = celda;

            dgvPagos.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            try
            {
                int codigo = relacional.Agregar(CrearEntidad());
                ConsultarRelacional();
                ConsultarFacturasPendientes();
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
            PagosEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoPago, CrearEntidad());
                ConsultarRelacional();
                ConsultarFacturasPendientes();
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
            PagosEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoPago + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<PagosEntidad>(seleccionado.CodigoPago);
                ConsultarRelacional();
                ConsultarFacturasPendientes();
                LimpiarFormulario();
                MessageBox.Show("Registro marcado como inactivo.", "Eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PagosEntidad CrearEntidad()
        {
            return new PagosEntidad(
                ComboFK.Obtener(cmbCodigoFactura).Value,
                cmbTipoPago.Text.Trim(),
                nudMonto.Value,
                cmbMoneda.Text.Trim(),
                dtpFechaPago.Value,
                ValorONulo(txtReferencia.Text),
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoFactura).HasValue)
            {
                MessageBox.Show("El campo Cód. factura debe ser mayor que cero.");
                cmbCodigoFactura.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbTipoPago.Text))
            {
                MessageBox.Show("El campo Tipo de pago es obligatorio.");
                cmbTipoPago.Focus();
                return false;
            }

            if (cmbTipoPago.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Tipo de pago no puede superar 100 caracteres.");
                cmbTipoPago.Focus();
                return false;
            }

            if (nudMonto.Value <= 0)
            {
                MessageBox.Show("El campo Monto debe ser mayor que cero.");
                nudMonto.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbMoneda.Text))
            {
                MessageBox.Show("El campo Moneda es obligatorio.");
                cmbMoneda.Focus();
                return false;
            }

            if (cmbMoneda.Text.Trim().Length > 10)
            {
                MessageBox.Show("El campo Moneda no puede superar 10 caracteres.");
                cmbMoneda.Focus();
                return false;
            }

            if (txtReferencia.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Referencia no puede superar 100 caracteres.");
                txtReferencia.Focus();
                return false;
            }

            return true;
        }

        private PagosEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvPagos.CurrentRow == null)
                return null;

            return dgvPagos.CurrentRow.DataBoundItem as PagosEntidad;
        }

        private void DgvPagos_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            PagosEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            ConsultarFacturasPendientes();

            cargando = true;

            txtCodigo.Text = registro.CodigoPago.ToString();
            ComboFK.Seleccionar(cmbCodigoFactura, registro.CodigoFactura);
            cmbTipoPago.Text = registro.TipoPago;
            nudMonto.Value = Limitar(nudMonto, registro.Monto);
            cmbMoneda.Text = registro.Moneda;
            dtpFechaPago.Value = LimitarFecha(dtpFechaPago, registro.FechaPago);
            txtReferencia.Text = registro.Referencia;
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
        }

        private decimal Limitar(NumericUpDown control, decimal valor)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, valor));
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

        private void BuscarPagos()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarPagos(pagos.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<PagosEntidad> resultado = relacional.Buscar<PagosEntidad>(criterio);
                MostrarPagos(resultado);
                ActualizarContador(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarPagos(List<PagosEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvPagos.DataSource = origen;
            ActualizarColumnas();
            dgvPagos.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoFactura, null);
            cmbTipoPago.Text = string.Empty;
            nudMonto.Value = 0;
            cmbMoneda.Text = string.Empty;
            dtpFechaPago.Value = DateTime.Now;
            txtReferencia.Clear();
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvPagos.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoFactura.Focus();
        }

        private void ActualizarContador(List<PagosEntidad> resultado = null)
        {
            List<PagosEntidad> registros = resultado ?? pagos.ToList();
            int activos = registros.Count(p => p.Estado);
            lblRegistros.Text = "Resultados: " + registros.Count + "  |  Activos: " + activos + "  |  Inactivos: " + (registros.Count - activos);
        }
        private void CargarCatalogosFK()
        {
            try
            {
                ComboFK.Cargar(cmbCodigoFactura, relacional.ListarCatalogo<FacturacionEntidad>(), x => x.CodigoFactura, x => "Factura | " + x.NumeroFactura + " | " + x.NombreCliente, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CodigoFacturaSeleccionada()
        {
            return ComboFK.Obtener(cmbCodigoFactura) ?? 0;
        }

        private void ConsultarFacturasPendientes()
        {
            try
            {
                int codigoActual = CodigoFacturaSeleccionada();
                PagosEntidad pago = ObtenerSeleccionado();
                facturasPendientes.Clear();
                facturasPendientes.AddRange(relacional.ListarFacturasPendientes());

                // En edición, conserva visible la factura asociada al pago seleccionado.
                if (pago != null && facturasPendientes.All(f => f.CodigoFactura != pago.CodigoFactura))
                {
                    FacturacionEntidad factura = relacional.Listar<FacturacionEntidad>()
                        .FirstOrDefault(f => f.CodigoFactura == pago.CodigoFactura && f.Estado);
                    if (factura != null) facturasPendientes.Add(factura);
                }

                cmbCodigoFactura.DataSource = null;
                ComboFK.Cargar(cmbCodigoFactura, facturasPendientes, f => f.CodigoFactura,
                    f => "Factura " + f.NumeroFactura + " | " + f.NombreCliente + " | Q " + f.Total.ToString("N2"));
                SeleccionarFactura(pago == null ? codigoActual : pago.CodigoFactura);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar facturas pendientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SeleccionarFactura(int codigo)
        {
            ComboFK.Seleccionar(cmbCodigoFactura, codigo);
        }
    }
}
