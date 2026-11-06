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
    public partial class FrmReclamos : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<ReclamosEntidad> reclamos = new BindingList<ReclamosEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;
        private DashboardCrudSimple dashboard;
        private TableLayoutPanel layoutBloquesReclamos;
        private TableLayoutPanel layoutEncabezadoReclamos;
        private TableLayoutPanel layoutOpcionesColumnasReclamos;
        private TableLayoutPanel layoutBusquedaReclamos;
        private IconPictureBox iconoMarcaReclamos;
        private CheckBox chkActivarTodosFiltros;
        private ComboBox cboFiltroEstadoReclamos;
        private bool actualizandoFiltrosReclamos;

        public FrmReclamos()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AplicarModeloVisualReclamos();
            CargarCatalogosFK();

            origen.DataSource = reclamos;
            dgvReclamos.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvReclamos);
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            TemaVisualCrud.Aplicar(this);
            layoutEncabezadoReclamos.BackColor = pnlHeader.BackColor;
            ConfigurarBusquedaModeloReclamos();
            ConfigurarOpcionesColumnasReclamos();
            ConstruirDashboardReclamos();
            ConstruirOrdenBloquesReclamos();
            ConstruirNavegacionLateral();

            ConectarEventos();
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            ConfigurarResaltadoCamposReclamos();
            ConfigurarNavegacionTecladoReclamos();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
            Shown += async (s, e) => await dashboard.CargarAsync();
            SizeChanged += (s, e) => AjustarInterfazReclamos();
        }

        private void AplicarModeloVisualReclamos()
        {
            pnlHeader.Height = 108;
            pnlHeader.Dock = DockStyle.None;
            pnlHeader.Padding = new Padding(22, 8, 15, 8);
            pnlHeader.Controls.Remove(lblTitulo);
            pnlHeader.Controls.Remove(lblSubtitulo);
            layoutEncabezadoReclamos = new TableLayoutPanel
            {
                Name = "layoutEncabezadoReclamos", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlHeader.BackColor,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutEncabezadoReclamos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadoReclamos.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutEncabezadoReclamos.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            lblTitulo.Height = 48;
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Margin = Padding.Empty;
            lblTitulo.Padding = Padding.Empty;
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblSubtitulo.Height = 42;
            lblSubtitulo.Dock = DockStyle.Fill;
            lblSubtitulo.Margin = Padding.Empty;
            lblSubtitulo.Padding = Padding.Empty;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            layoutEncabezadoReclamos.Controls.Add(lblTitulo, 0, 0);
            layoutEncabezadoReclamos.Controls.Add(lblSubtitulo, 0, 1);
            pnlHeader.Controls.Add(layoutEncabezadoReclamos);
            pnlContenido.Padding = new Padding(14);
            pnlContenido.Dock = DockStyle.None;
            pnlContenido.AutoScroll = true;
            pnlFormulario.Height = 260;
            pnlAcciones.Height = 58;
            lblFormularioTitulo.Height = 40;
            lblCatalogoTitulo.Height = 40;
            lblColumnas.Height = 40;
            pnlBusqueda.Height = 54;
            pnlColumnas.Height = 76;
            selector.WrapContents = false;
            selector.AutoScroll = false;
            chkSeleccionar.Text = "Seleccionar registro";
            chkSeleccionar.AutoSize = true;
            chkSeleccionar.Font = new Font("Segoe UI", 9.5F);
            chkActivarTodosFiltros = new CheckBox { Name = "chkActivarFiltros", Text = "Activar todos los filtros",
                AutoSize = true, BackColor = Color.Transparent, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F),
                Margin = new Padding(5, 3, 9, 3), UseVisualStyleBackColor = false };
            selector.Controls.Remove(chkSeleccionar);
            foreach (CheckBox filtro in ObtenerFiltrosColumnasReclamos()) filtro.Checked = true;
            chkActivarTodosFiltros.Checked = true;
            foreach (CheckBox filtro in ObtenerFiltrosColumnasReclamos())
            { filtro.AutoSize = true; filtro.Font = new Font("Segoe UI", 9.5F); }
            lblRegistros.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRegistros.ForeColor = Color.White;
            lblRegistros.AutoEllipsis = true;
            dgvReclamos.Dock = DockStyle.Top;
            dgvReclamos.RowTemplate.Height = 32;
            dgvReclamos.ColumnHeadersHeight = 40;
            dgvReclamos.ScrollBars = ScrollBars.Horizontal;
            foreach (Control control in new Control[] { cmbCodigoCliente, cmbCodigoLinea, cmbCodigoDetalleVenta })
            {
                ComboBox combo = (ComboBox)control;
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.Font = new Font("Segoe UI", 9.5F);
            }
            cmbTipoReclamo.Font = new Font("Segoe UI", 9.5F);
            foreach (Control control in new Control[] { txtCodigo, txtDescripcion, dtpFecha, cmbCodigoCliente, cmbCodigoLinea, cmbCodigoDetalleVenta, cmbTipoReclamo })
            { control.BackColor = ColorTranslator.FromHtml("#E1ECF6"); control.ForeColor = ColorTranslator.FromHtml("#072342"); }
            foreach (Label etiqueta in layoutCampos.Controls.OfType<Label>())
            { etiqueta.Font = new Font("Segoe UI", 9F, FontStyle.Bold); etiqueta.ForeColor = Color.White; }
            foreach (RadioButton opcion in panelEstado.Controls.OfType<RadioButton>())
            { opcion.Font = new Font("Segoe UI", 9.5F); opcion.ForeColor = Color.White; }
            lblTitulo.Text = "Gestión de reclamos";
            lblSubtitulo.Text = "Administra los reclamos asociados a clientes, líneas y ventas";
            lblFormularioTitulo.Text = "Información del reclamo";
            lblCatalogoTitulo.Text = "Reclamos registrados";
            pnlHeader.BackColor = ColorTranslator.FromHtml("#032B53");
            pnlFormulario.BackColor = ColorTranslator.FromHtml("#072342");
            pnlCatalogo.BackColor = ColorTranslator.FromHtml("#072342");
            pnlAcciones.BackColor = ColorTranslator.FromHtml("#03162B");
            int ancho = Math.Min(195, Math.Max(148, (int)(ClientSize.Width * .18)));
            pnlMenu.Width = ancho;
            pnlHeader.SetBounds(ancho, 0, Math.Max(0, ClientSize.Width - ancho), 108);
            pnlContenido.SetBounds(ancho, 108, Math.Max(0, ClientSize.Width - ancho), Math.Max(0, ClientSize.Height - 108));
            pnlHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenido.SizeChanged += (s, e) => AjustarCatalogoModeloReclamos();
            pnlCatalogo.SizeChanged += (s, e) => AjustarCatalogoModeloReclamos();
        }

        private void ConfigurarOpcionesColumnasReclamos()
        {
            pnlColumnas.Controls.Remove(selector);
            pnlColumnas.Controls.Remove(lblColumnas);
            lblCatalogoTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblColumnas.Font = lblCatalogoTitulo.Font;
            lblColumnas.ForeColor = lblCatalogoTitulo.ForeColor;
            lblColumnas.TextAlign = ContentAlignment.MiddleLeft;

            layoutOpcionesColumnasReclamos = new TableLayoutPanel
            {
                Name = "layoutOpcionesColumnasReclamos", Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 2,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlColumnas.BackColor
            };
            layoutOpcionesColumnasReclamos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            layoutOpcionesColumnasReclamos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutOpcionesColumnasReclamos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            layoutOpcionesColumnasReclamos.RowStyles.Add(new RowStyle(SizeType.Absolute, lblColumnas.Height));
            layoutOpcionesColumnasReclamos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            lblColumnas.Dock = DockStyle.Fill;
            layoutOpcionesColumnasReclamos.Controls.Add(lblColumnas, 0, 0);
            layoutOpcionesColumnasReclamos.SetColumnSpan(lblColumnas, 3);

            chkActivarTodosFiltros.Margin = new Padding(4, 4, 10, 0);
            chkActivarTodosFiltros.Anchor = AnchorStyles.Left;
            chkSeleccionar.Margin = new Padding(8, 4, 4, 0);
            chkSeleccionar.Anchor = AnchorStyles.Right;
            selector.Dock = DockStyle.Fill;
            selector.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            selector.WrapContents = false;
            // Evita que FlowLayoutPanel muestre flechas de desplazamiento junto a
            // «Seleccionar registro»; las opciones deben caber en una sola fila.
            selector.AutoScroll = false;
            selector.FlowDirection = FlowDirection.LeftToRight;
            layoutOpcionesColumnasReclamos.Controls.Add(chkActivarTodosFiltros, 0, 1);
            layoutOpcionesColumnasReclamos.Controls.Add(selector, 1, 1);
            layoutOpcionesColumnasReclamos.Controls.Add(chkSeleccionar, 2, 1);
            pnlColumnas.Controls.Add(layoutOpcionesColumnasReclamos);

            chkActivarTodosFiltros.CheckedChanged += (s, e) =>
            {
                if (actualizandoFiltrosReclamos) return;
                actualizandoFiltrosReclamos = true;
                try
                {
                    foreach (CheckBox filtro in ObtenerFiltrosColumnasReclamos())
                        filtro.Checked = chkActivarTodosFiltros.Checked;
                }
                finally { actualizandoFiltrosReclamos = false; }
                ActualizarColumnas();
            };
            foreach (CheckBox filtro in ObtenerFiltrosColumnasReclamos())
                filtro.CheckedChanged += (s, e) =>
                {
                    if (actualizandoFiltrosReclamos) return;
                    actualizandoFiltrosReclamos = true;
                    try { chkActivarTodosFiltros.Checked = ObtenerFiltrosColumnasReclamos().All(x => x.Checked); }
                    finally { actualizandoFiltrosReclamos = false; }
                    ActualizarColumnas();
                };
            pnlColumnas.SizeChanged += (s, e) => AjustarAnchosOpcionesColumnasReclamos();
            AjustarAnchosOpcionesColumnasReclamos();
        }

        private void ConfigurarBusquedaModeloReclamos()
        {
            cboFiltroEstadoReclamos = Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault();
            if (cboFiltroEstadoReclamos == null) return;

            Panel panelFiltro = Controls.Find("pnlFiltroEstadoComun", true).OfType<Panel>().FirstOrDefault();
            if (panelFiltro != null)
            {
                panelFiltro.Controls.Remove(cboFiltroEstadoReclamos);
                panelFiltro.Controls.Remove(btnBuscar);
                pnlBusqueda.Controls.Remove(panelFiltro);
                panelFiltro.Dispose();
            }
            pnlBusqueda.Controls.Remove(txtBuscar);
            pnlBusqueda.Controls.Clear();

            layoutBusquedaReclamos = new TableLayoutPanel
            {
                Name = "layoutBusquedaReclamos", Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlBusqueda.BackColor
            };
            layoutBusquedaReclamos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBusquedaReclamos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutBusquedaReclamos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBusquedaReclamos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Math.Max(96, btnNuevo.Width)));
            cboFiltroEstadoReclamos.Dock = DockStyle.Fill;
            txtBuscar.Dock = DockStyle.Fill;
            btnBuscar.Dock = DockStyle.Fill;
            cboFiltroEstadoReclamos.Margin = new Padding(0, 0, 8, 0);
            txtBuscar.Margin = new Padding(0, 0, 8, 0);
            btnBuscar.Margin = Padding.Empty;
            layoutBusquedaReclamos.Controls.Add(cboFiltroEstadoReclamos, 0, 0);
            layoutBusquedaReclamos.Controls.Add(txtBuscar, 1, 0);
            layoutBusquedaReclamos.Controls.Add(btnBuscar, 2, 0);
            pnlBusqueda.Controls.Add(layoutBusquedaReclamos);
            pnlAcciones.SizeChanged += (s, e) =>
            {
                if (layoutBusquedaReclamos != null && layoutBusquedaReclamos.ColumnStyles.Count == 3)
                    layoutBusquedaReclamos.ColumnStyles[2].Width = Math.Max(72, btnNuevo.Width);
            };
        }

        private IEnumerable<CheckBox> ObtenerFiltrosColumnasReclamos()
        {
            return new[] { chkCodigo, chkCodigoCliente, chkCodigoLinea, chkCodigoDetalleVenta,
                chkFecha, chkDescripcion, chkTipoReclamo, chkEstado };
        }

        private void AjustarAnchosOpcionesColumnasReclamos()
        {
            if (layoutOpcionesColumnasReclamos == null || layoutOpcionesColumnasReclamos.ColumnStyles.Count < 3) return;
            int anchoActivar = TextRenderer.MeasureText(chkActivarTodosFiltros.Text, chkActivarTodosFiltros.Font).Width +
                28 + chkActivarTodosFiltros.Margin.Horizontal + 12;
            int anchoSeleccionar = TextRenderer.MeasureText(chkSeleccionar.Text, chkSeleccionar.Font).Width +
                28 + chkSeleccionar.Margin.Horizontal + 12;
            layoutOpcionesColumnasReclamos.ColumnStyles[0].Width = anchoActivar;
            layoutOpcionesColumnasReclamos.ColumnStyles[2].Width = anchoSeleccionar;
            layoutOpcionesColumnasReclamos.PerformLayout();
        }

        private void ConstruirDashboardReclamos()
        {
            dashboard = new DashboardCrudSimple("Resumen de reclamos", "Reclamos por tipo", ObtenerDatosDashboardReclamos);
        }

        private DashboardCrudDatos ObtenerDatosDashboardReclamos()
        {
            List<ReclamosEntidad> lista = relacional.Listar<ReclamosEntidad>();
            List<ReclamosEntidad> activos = lista.Where(x => x.Estado).ToList();
            return new DashboardCrudDatos
            {
                TitulosIndicadores = new[] { "RECLAMOS REGISTRADOS", "RECLAMOS ACTIVOS", "RECLAMOS INACTIVOS", "TIPOS DE RECLAMO" },
                ValoresIndicadores = new[] { lista.Count.ToString("N0"), activos.Count.ToString("N0"), lista.Count(x => !x.Estado).ToString("N0"),
                    lista.Select(x => x.TipoReclamo).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString("N0") },
                Serie = lista.Where(x => !string.IsNullOrWhiteSpace(x.TipoReclamo)).GroupBy(x => x.TipoReclamo.Trim(), StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Take(8).Select(x => new KeyValuePair<string, int>(x.Key, x.Count())).ToList(),
                Alertas = "• Sin descripción: " + activos.Count(x => string.IsNullOrWhiteSpace(x.Descripcion)) + Environment.NewLine +
                    "• Sin detalle de venta: " + activos.Count(x => x.CodigoDetalleVenta <= 0),
                Actividad = activos.Count == 0 ? "No hay reclamos activos." : string.Join(Environment.NewLine,
                    activos.OrderByDescending(x => x.Fecha).Take(4).Select(x => "• Reclamo " + x.CodigoReclamo + " · " + x.TipoReclamo))
            };
        }

        private void ConstruirOrdenBloquesReclamos()
        {
            layoutBloquesReclamos = new TableLayoutPanel { Name = "layoutBloquesReclamos", Dock = DockStyle.Top,
                ColumnCount = 1, RowCount = 5, Padding = Padding.Empty, Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize };
            layoutBloquesReclamos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesReclamos.RowStyles.Add(new RowStyle(SizeType.Absolute, 360));
            layoutBloquesReclamos.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            layoutBloquesReclamos.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlFormulario.Height));
            layoutBloquesReclamos.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlAcciones.Height));
            layoutBloquesReclamos.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlCatalogo.Height));
            pnlContenido.Controls.Clear(); pnlContenido.Controls.Add(layoutBloquesReclamos);
            layoutBloquesReclamos.Controls.Add(dashboard, 0, 0);
            layoutBloquesReclamos.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = pnlContenido.BackColor }, 0, 1);
            layoutBloquesReclamos.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesReclamos.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesReclamos.Controls.Add(pnlCatalogo, 0, 4);
            pnlFormulario.Dock = pnlAcciones.Dock = pnlCatalogo.Dock = DockStyle.Fill;
            dashboard.Dock = DockStyle.Fill;
            AjustarCatalogoModeloReclamos();
            pnlContenido.AutoScroll = true;
        }

        private void AjustarInterfazReclamos()
        {
            if (IsDisposed || layoutBloquesReclamos == null) return;
            int ancho = Math.Min(195, Math.Max(148, (int)(ClientSize.Width * .18)));
            pnlMenu.Width = ancho;
            pnlHeader.SetBounds(ancho, 0, Math.Max(0, ClientSize.Width - ancho), 108);
            pnlContenido.SetBounds(ancho, 108, Math.Max(0, ClientSize.Width - ancho), Math.Max(0, ClientSize.Height - 108));
            AjustarCatalogoModeloReclamos();
            int catalogo = pnlCatalogo.Height;
            int dashboardAlto = Math.Max(360, pnlContenido.ClientSize.Height - pnlContenido.Padding.Vertical - 10 - pnlFormulario.Height - pnlAcciones.Height - catalogo);
            layoutBloquesReclamos.RowStyles[0].Height = dashboardAlto;
            layoutBloquesReclamos.RowStyles[2].Height = pnlFormulario.Height;
            layoutBloquesReclamos.RowStyles[3].Height = pnlAcciones.Height;
            layoutBloquesReclamos.RowStyles[4].Height = catalogo;
            layoutBloquesReclamos.Height = dashboardAlto + 10 + pnlFormulario.Height + pnlAcciones.Height + catalogo;
        }

        private void AjustarCatalogoModeloReclamos()
        {
            if (pnlCatalogo == null || dgvReclamos == null) return;
            int altoGrilla = dgvReclamos.ColumnHeadersHeight + 10 * dgvReclamos.RowTemplate.Height + 2;
            dgvReclamos.Height = altoGrilla;
            Panel pie = pnlCatalogo.Controls.Find("pnlPaginacion", false).OfType<Panel>().FirstOrDefault();
            if (pie != null) pie.Height = 48;
            pnlCatalogo.Height = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height + pnlColumnas.Height + altoGrilla +
                (pie == null ? lblRegistros.Height : pie.Height);
        }

        private void ConfigurarResaltadoCamposReclamos()
        {
            Control[] campos = { cmbCodigoCliente, cmbCodigoLinea, cmbCodigoDetalleVenta, dtpFecha, txtDescripcion, cmbTipoReclamo };
            foreach (Control campo in campos)
            {
                CampoRedondeado.Aplicar(campo, 7);
                campo.MouseEnter += (s, e) => { if (campo.Enabled && !campo.Focused) campo.BackColor = Color.FromArgb(205, 243, 255); };
                campo.MouseLeave += (s, e) => RestaurarColorCampoReclamo(campo);
                campo.Enter += (s, e) => { if (campo.Enabled) campo.BackColor = ColorTranslator.FromHtml("#80DEFF"); };
                campo.Leave += (s, e) => RestaurarColorCampoReclamo(campo);
                campo.EnabledChanged += (s, e) => RestaurarColorCampoReclamo(campo);
            }
        }

        private static void RestaurarColorCampoReclamo(Control campo)
        {
            if (campo == null || campo.IsDisposed) return;
            campo.BackColor = campo.Enabled ? ColorTranslator.FromHtml("#E1ECF6") : Color.FromArgb(215, 222, 230);
            campo.ForeColor = campo.Enabled ? ColorTranslator.FromHtml("#072342") : Color.FromArgb(104, 116, 128);
        }

        private void ConfigurarNavegacionTecladoReclamos()
        {
            Control[] orden = { cmbCodigoCliente, cmbCodigoLinea, cmbCodigoDetalleVenta, dtpFecha, txtDescripcion, cmbTipoReclamo, rdoActivo, rdoInactivo };
            foreach (Control control in orden) control.KeyDown += CampoReclamo_KeyDown;
            AcceptButton = null;
        }

        private void CampoReclamo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Alt || e.Control || e.Shift) return;
            ComboBox lista = sender as ComboBox;
            if (lista != null && lista.DroppedDown) return;
            Control actual = sender as Control;
            if (actual == null || !ValidarCampoParaAvanzarReclamo(actual)) return;
            e.SuppressKeyPress = true; e.Handled = true;
            Control[] orden = { cmbCodigoCliente, cmbCodigoLinea, cmbCodigoDetalleVenta, dtpFecha, txtDescripcion, cmbTipoReclamo, rdoActivo, rdoInactivo };
            for (int i = Array.IndexOf(orden, actual) + 1; i < orden.Length; i++)
                if (orden[i].Enabled && orden[i].Visible) { orden[i].Focus(); return; }
        }

        private bool ValidarCampoParaAvanzarReclamo(Control campo)
        {
            if (campo == cmbCodigoCliente && !ComboFK.Obtener(cmbCodigoCliente).HasValue) { cmbCodigoCliente.Focus(); return false; }
            if (campo == cmbCodigoLinea && !ComboFK.Obtener(cmbCodigoLinea).HasValue) { cmbCodigoLinea.Focus(); return false; }
            if (campo == cmbCodigoDetalleVenta && !ComboFK.Obtener(cmbCodigoDetalleVenta).HasValue) { cmbCodigoDetalleVenta.Focus(); return false; }
            if (campo == txtDescripcion && (string.IsNullOrWhiteSpace(txtDescripcion.Text) || txtDescripcion.Text.Trim().Length > 200)) { txtDescripcion.Focus(); return false; }
            if (campo == cmbTipoReclamo && (string.IsNullOrWhiteSpace(cmbTipoReclamo.Text) || cmbTipoReclamo.Text.Trim().Length > 45)) { cmbTipoReclamo.Focus(); return false; }
            return true;
        }

        private void ConstruirNavegacionLateral()
        {
            Dictionary<string, Label> referencias = pnlItemsMenu.Controls.OfType<Label>().Where(x => x.Tag != null)
                .GroupBy(x => NormalizarDestinoReclamos(Convert.ToString(x.Tag))).ToDictionary(g => g.Key, g => g.First());
            pnlItemsMenu.Controls.Clear();
            TableLayoutPanel filas = new TableLayoutPanel { Name = "layoutBotonesBarraLateral", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 18, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = ColorTranslator.FromHtml("#061D30") };
            filas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 18; i++) filas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 18));
            IconButton volver = CrearBotonLateralReclamos("btnVolverMenu", "Volver al menú principal", IconChar.Home, null);
            volver.BackColor = ColorTranslator.FromHtml("#08243A"); volver.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#526779"); volver.FlatAppearance.BorderSize = 1;
            volver.Click += (s, e) => Navegacion.VolverAlMenu(this); filas.Controls.Add(volver, 0, 0);
            string[] destinos = { "FrmUsuarios", "FrmSucursales", "FrmEmpleados", "FrmClientes", "FrmPlanes", "FrmLineas", "FrmProveedores", "FrmMarcas", "FrmEquipos", "FrmCompras", "FrmInventario", "FrmVentas", "FrmFacturacion", "FrmPagos", "FrmCardex", "FrmReclamos" };
            for (int i = 0; i < destinos.Length; i++)
            {
                Label referencia;
                if (!referencias.TryGetValue(destinos[i], out referencia)) continue;
                string texto = referencia.Text.Trim().TrimStart('›', '⌂', '▸', ' ').Trim();
                IconButton boton = CrearBotonLateralReclamos("btn" + destinos[i].Substring(3), texto, IconoModuloReclamos(destinos[i]), destinos[i]);
                filas.Controls.Add(boton, 0, i + 1);
            }
            IconButton salir = CrearBotonLateralReclamos("btnCerrarSesion", "Cerrar sesión", IconChar.SignOutAlt, null);
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
            iconoMarcaReclamos = new IconPictureBox
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
            pnlLogo.Controls.Add(iconoMarcaReclamos);
            iconoMarcaReclamos.BringToFront();
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
            pnlMenu.SizeChanged += (s, e) => { int width = Math.Max(0, pnlMenu.ClientSize.Width - 16); pnlLogo.Width = width; iconoMarcaReclamos.Left = (pnlLogo.ClientSize.Width - iconoMarcaReclamos.Width) / 2; lblSistema.Width = Math.Max(0, pnlLogo.ClientSize.Width - 12); lblUsuario.Width = lblUsuarioRol.Width = width; pnlItemsMenu.SetBounds(8, 198, width, Math.Max(0, pnlMenu.ClientSize.Height - 206)); };
        }

        private IconButton CrearBotonLateralReclamos(string nombre, string texto, IconChar icono, string destino)
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

        private static string NormalizarDestinoReclamos(string destino)
        { return string.IsNullOrWhiteSpace(destino) ? string.Empty : (destino.StartsWith("Frm", StringComparison.OrdinalIgnoreCase) ? destino : "Frm" + destino); }

        private static IconChar IconoModuloReclamos(string destino)
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
                MostrarReclamos(reclamos.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarReclamos();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarReclamos();
                    e.SuppressKeyPress = true;
                }
            };

            dgvReclamos.SelectionChanged += DgvReclamos_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Control opcion = sender as Control;
            if (opcion != null && opcion.Tag != null) Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoReclamo", chkCodigo.Checked);
            CambiarVisibilidad("CodigoCliente", chkCodigoCliente.Checked);
            CambiarVisibilidad("CodigoLinea", chkCodigoLinea.Checked);
            CambiarVisibilidad("CodigoDetalleVenta", chkCodigoDetalleVenta.Checked);
            CambiarVisibilidad("Fecha", chkFecha.Checked);
            CambiarVisibilidad("Descripcion", chkDescripcion.Checked);
            CambiarVisibilidad("TipoReclamo", chkTipoReclamo.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<ReclamosEntidad> registros = relacional.Listar<ReclamosEntidad>();
                reclamos.Clear();
                foreach (ReclamosEntidad registro in registros) reclamos.Add(registro);
                MostrarReclamos(reclamos.ToList());
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
            if (dgvReclamos.Columns.Contains(columna))
                dgvReclamos.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvReclamos.Rows.Count > 0)
                {
                    int indice = dgvReclamos.CurrentRow == null ? 0 : dgvReclamos.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvReclamos.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvReclamos.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvReclamos.Rows.Count)
                return;

            dgvReclamos.ClearSelection();

            DataGridViewCell celda = dgvReclamos.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvReclamos.CurrentCell = celda;

            dgvReclamos.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            try
            {
                int codigo = relacional.Agregar(CrearEntidad());
                ConsultarRelacional();
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
            ReclamosEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoReclamo, CrearEntidad());
                ConsultarRelacional();
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
            ReclamosEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoReclamo + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<ReclamosEntidad>(seleccionado.CodigoReclamo);
                ConsultarRelacional();
                LimpiarFormulario();
                MessageBox.Show("Registro marcado como inactivo.", "Eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ReclamosEntidad CrearEntidad()
        {
            return new ReclamosEntidad(
                ComboFK.Obtener(cmbCodigoCliente).Value,
                ComboFK.Obtener(cmbCodigoLinea).Value,
                ComboFK.Obtener(cmbCodigoDetalleVenta).Value,
                dtpFecha.Value,
                txtDescripcion.Text.Trim(),
                cmbTipoReclamo.Text.Trim(),
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoCliente).HasValue)
            {
                MessageBox.Show("El campo Cód. cliente debe ser mayor que cero.");
                cmbCodigoCliente.Focus();
                return false;
            }

            if (!ComboFK.Obtener(cmbCodigoLinea).HasValue)
            {
                MessageBox.Show("El campo Cód. línea debe ser mayor que cero.");
                cmbCodigoLinea.Focus();
                return false;
            }

            if (!ComboFK.Obtener(cmbCodigoDetalleVenta).HasValue)
            {
                MessageBox.Show("El campo Cód. detalle venta debe ser mayor que cero.");
                cmbCodigoDetalleVenta.Focus();
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

            if (string.IsNullOrWhiteSpace(cmbTipoReclamo.Text))
            {
                MessageBox.Show("El campo Tipo de reclamo es obligatorio.");
                cmbTipoReclamo.Focus();
                return false;
            }

            if (cmbTipoReclamo.Text.Trim().Length > 45)
            {
                MessageBox.Show("El campo Tipo de reclamo no puede superar 45 caracteres.");
                cmbTipoReclamo.Focus();
                return false;
            }

            return true;
        }

        private ReclamosEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvReclamos.CurrentRow == null)
                return null;

            return dgvReclamos.CurrentRow.DataBoundItem as ReclamosEntidad;
        }

        private void DgvReclamos_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            ReclamosEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoReclamo.ToString();
            ComboFK.Seleccionar(cmbCodigoCliente, registro.CodigoCliente);
            ComboFK.Seleccionar(cmbCodigoLinea, registro.CodigoLinea);
            ComboFK.Seleccionar(cmbCodigoDetalleVenta, registro.CodigoDetalleVenta);
            dtpFecha.Value = LimitarFecha(dtpFecha, registro.Fecha);
            txtDescripcion.Text = registro.Descripcion;
            cmbTipoReclamo.Text = registro.TipoReclamo;
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

        private static bool Contiene(string valor, string criterio)
        {
            return valor != null && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void BuscarReclamos()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarReclamos(reclamos.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<ReclamosEntidad> resultado = relacional.Buscar<ReclamosEntidad>(criterio);
                MostrarReclamos(resultado);
                ActualizarContador(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarReclamos(List<ReclamosEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvReclamos.DataSource = origen;
            ActualizarColumnas();
            dgvReclamos.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoCliente, null);
            ComboFK.Seleccionar(cmbCodigoLinea, null);
            ComboFK.Seleccionar(cmbCodigoDetalleVenta, null);
            dtpFecha.Value = DateTime.Now;
            txtDescripcion.Clear();
            cmbTipoReclamo.Text = string.Empty;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvReclamos.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoCliente.Focus();
        }

        private void ActualizarContador(List<ReclamosEntidad> resultado = null)
        {
            List<ReclamosEntidad> registros = resultado ?? reclamos.ToList();
            int activos = registros.Count(reclamo => reclamo.Estado);
            lblRegistros.Text = "Resultados: " + registros.Count + "  |  Activos: " + activos +
                "  |  Inactivos: " + (registros.Count - activos);
        }
        private void CargarCatalogosFK()
        {
            try
            {
                ComboFK.Cargar(cmbCodigoCliente, relacional.ListarCatalogo<ClientesEntidad>(), x => x.CodigoCliente, x => x.Nombre + " | " + x.Telefono, false);
                ComboFK.Cargar(cmbCodigoLinea, relacional.ListarCatalogo<LineasEntidad>(), x => x.CodigoLinea, x => x.NumeroTelefonico, false);
                ComboFK.Cargar(cmbCodigoDetalleVenta, relacional.ListarCatalogo<DetalleVentaEntidad>(), x => x.CodigoDetalleVenta, x => "Detalle | equipo " + x.CodigoEquipo + " | venta " + x.CodigoVenta, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
