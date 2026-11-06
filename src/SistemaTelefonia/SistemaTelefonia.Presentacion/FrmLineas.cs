using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FontAwesome.Sharp;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmLineas : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<LineasEntidad> lineas = new BindingList<LineasEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;
        private TableLayoutPanel layoutBloquesLineas;
        private Panel pnlDashboardLineas;
        private Label[] indicadoresLineas;
        private Chart chartLineas;
        private Button btnActualizarDashboardLineas;
        private Label lblResumenClientesLineas;
        private Label lblResumenPlanesLineas;
        private CheckBox chkActivarFiltrosLineas;

        public FrmLineas()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AplicarModeloVisualLineas();
            CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = lineas;
            dgvLineas.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvLineas);
            ConstruirDashboardLineas();
            ConstruirOrdenBloquesLineas();

            ConectarEventos();
            TemaVisualCrud.Aplicar(this);
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            AplicarEstiloNavegacionLineas();
            AplicarEstiloDashboardLineas();
            ConfigurarResaltadoCamposLineas();
            ConfigurarNavegacionTecladoLineas();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
            ActualizarDashboardLineas();
            SizeChanged += (s, e) => AjustarInterfazLineas();
        }

        private void AplicarModeloVisualLineas()
        {
            // Se vacía la composición anterior y se reubican los paneles existentes;
            // los campos (incluidos los ComboBox FK) conservan nombre, tipo y eventos.
            pnlHeader.Height = 108;
            lblTitulo.Height = 48;
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblSubtitulo.Height = 42;
            pnlContenido.Padding = new Padding(14);
            pnlContenido.AutoScroll = true;
            pnlFormulario.Height = 260;
            pnlAcciones.Height = 58;
            pnlCatalogo.Dock = DockStyle.Top;
            pnlFormulario.Dock = DockStyle.Top;
            pnlAcciones.Dock = DockStyle.Top;
            lblFormularioTitulo.Height = 40;
            lblCatalogoTitulo.Height = 40;
            lblColumnas.Height = 40;
            pnlBusqueda.Height = 54;
            pnlColumnas.Height = 76;
            selector.WrapContents = false;
            selector.AutoScroll = true;
            chkSeleccionar.Text = "Seleccionar registro";
            chkSeleccionar.AutoSize = true;
            chkSeleccionar.Font = new Font("Segoe UI", 9.5F);
            chkActivarFiltrosLineas = new CheckBox { Name = "chkActivarFiltros", Text = "Activar todos los filtros",
                AutoSize = true, BackColor = Color.Transparent, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F),
                Margin = new Padding(5, 3, 9, 3), UseVisualStyleBackColor = false };
            selector.Controls.Remove(chkSeleccionar);
            selector.Controls.Add(chkActivarFiltrosLineas);
            selector.Controls.SetChildIndex(chkActivarFiltrosLineas, 0);
            selector.Controls.Add(chkSeleccionar);
            chkActivarFiltrosLineas.CheckedChanged += (s, e) =>
            {
                foreach (CheckBox filtro in selector.Controls.OfType<CheckBox>().Where(x => x != chkSeleccionar && x != chkActivarFiltrosLineas))
                    filtro.Checked = chkActivarFiltrosLineas.Checked;
            };
            foreach (CheckBox filtro in selector.Controls.OfType<CheckBox>())
            {
                filtro.AutoSize = true;
                filtro.Font = new Font("Segoe UI", 9.5F);
            }
            dgvLineas.Dock = DockStyle.Top;
            dgvLineas.RowTemplate.Height = 32;
            dgvLineas.ColumnHeadersHeight = 40;
            dgvLineas.ScrollBars = ScrollBars.Horizontal;
            foreach (Control control in new Control[] { cmbCodigoCliente, cmbCodigoPlan, cmbTipoLinea })
            {
                ComboBox lista = (ComboBox)control;
                lista.DropDownStyle = ComboBoxStyle.DropDownList;
                lista.Font = new Font("Segoe UI", 9.5F);
                lista.BackColor = ColorTranslator.FromHtml("#E1ECF6");
                lista.ForeColor = ColorTranslator.FromHtml("#072342");
            }
            foreach (Control control in new Control[] { txtCodigo, txtNumeroTelefonico })
            {
                control.Font = new Font("Segoe UI", 9.5F);
                control.BackColor = ColorTranslator.FromHtml("#E1ECF6");
                control.ForeColor = ColorTranslator.FromHtml("#072342");
            }
            foreach (Label etiqueta in layoutCampos.Controls.OfType<Label>())
            {
                etiqueta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                etiqueta.ForeColor = Color.White;
            }
            foreach (RadioButton opcion in panelEstado.Controls.OfType<RadioButton>())
            {
                opcion.Font = new Font("Segoe UI", 9.5F);
                opcion.ForeColor = Color.White;
            }
            lblTitulo.Text = "Gestión de líneas";
            lblSubtitulo.Text = "Administra las líneas telefónicas asignadas a clientes y planes";
            lblFormularioTitulo.Text = "Información de la línea";
            lblCatalogoTitulo.Text = "Líneas registradas";
            pnlHeader.BackColor = ColorTranslator.FromHtml("#032B53");
            pnlFormulario.BackColor = ColorTranslator.FromHtml("#072342");
            pnlCatalogo.BackColor = ColorTranslator.FromHtml("#072342");
            pnlAcciones.BackColor = ColorTranslator.FromHtml("#03162B");
            int ancho = Math.Min(195, Math.Max(148, (int)(ClientSize.Width * .18)));
            pnlMenu.Width = ancho;
            pnlHeader.Left = ancho;
            pnlContenido.Left = ancho;
            pnlContenido.SizeChanged += (s, e) => AjustarCatalogoModeloLineas();
            pnlCatalogo.SizeChanged += (s, e) => AjustarCatalogoModeloLineas();
        }

        private void ConstruirDashboardLineas()
        {
            pnlDashboardLineas = new PanelRedondeado { Name = "pnlDashboardLineas", Dock = DockStyle.Fill,
                BackColor = ColorTranslator.FromHtml("#08243A"), Padding = new Padding(12) };
            ((PanelRedondeado)pnlDashboardLineas).RadioEsquinas = 12;
            TableLayoutPanel raiz = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1,
                RowCount = 3, BackColor = pnlDashboardLineas.BackColor, Margin = Padding.Empty };
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Panel encabezadoDashboard = new Panel { Dock = DockStyle.Fill, BackColor = pnlDashboardLineas.BackColor };
            Label titulo = new Label { Text = "Resumen de líneas", Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft };
            btnActualizarDashboardLineas = new Button { Name = "btnActualizarDashboardLineas", Text = "↻ Actualizar", Dock = DockStyle.Right,
                Width = 112, BackColor = ColorTranslator.FromHtml("#0B6E91"), ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand };
            btnActualizarDashboardLineas.Margin = new Padding(4, 1, 0, 1);
            btnActualizarDashboardLineas.FlatAppearance.BorderSize = 0;
            btnActualizarDashboardLineas.Click += (s, e) => { ConsultarRelacional(); ActualizarDashboardLineas(); };
            encabezadoDashboard.Controls.Add(titulo); encabezadoDashboard.Controls.Add(btnActualizarDashboardLineas);
            TableLayoutPanel tarjetas = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4,
                RowCount = 1, BackColor = pnlDashboardLineas.BackColor, Margin = new Padding(0, 5, 0, 5) };
            indicadoresLineas = new Label[4];
            string[] encabezados = { "LÍNEAS REGISTRADAS", "LÍNEAS ACTIVAS", "LÍNEAS INACTIVAS", "TIPOS DE LÍNEA" };
            for (int i = 0; i < 4; i++)
            {
                tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                Panel tarjeta = new Panel { Dock = DockStyle.Fill, BackColor = ColorTranslator.FromHtml("#0B3558"),
                    Margin = new Padding(0, 0, i == 3 ? 0 : 8, 0), Padding = Padding.Empty };
                TableLayoutPanel contenido = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1,
                    RowCount = 3, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = tarjeta.BackColor };
                contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 3));
                contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                Color[] acentos = { ColorTranslator.FromHtml("#3B9EFF"), ColorTranslator.FromHtml("#35C98B"),
                    ColorTranslator.FromHtml("#E7B84B"), ColorTranslator.FromHtml("#8B72E8") };
                Panel acento = new Panel { Dock = DockStyle.Fill, BackColor = acentos[i], Margin = Padding.Empty };
                Label tituloTarjeta = new Label { Text = encabezados[i], Dock = DockStyle.Fill,
                    Margin = new Padding(10, 0, 8, 0), Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    ForeColor = ColorTranslator.FromHtml("#A9BAC8"), TextAlign = ContentAlignment.BottomLeft, AutoEllipsis = true };
                indicadoresLineas[i] = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.White, Margin = new Padding(10, 0, 8, 4), TextAlign = ContentAlignment.MiddleLeft };
                contenido.Controls.Add(acento, 0, 0); contenido.Controls.Add(tituloTarjeta, 0, 1);
                contenido.Controls.Add(indicadoresLineas[i], 0, 2); tarjeta.Controls.Add(contenido); tarjetas.Controls.Add(tarjeta, i, 0);
            }
            TableLayoutPanel detalle = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2,
                RowCount = 1, BackColor = pnlDashboardLineas.BackColor, Margin = new Padding(0, 4, 0, 0) };
            detalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F)); detalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            Panel panelGrafica = CrearPanelGraficaLineas();
            detalle.Controls.Add(panelGrafica, 0, 0);
            TableLayoutPanel paneles = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1,
                RowCount = 2, BackColor = pnlDashboardLineas.BackColor, Margin = new Padding(8, 0, 0, 0) };
            paneles.RowStyles.Add(new RowStyle(SizeType.Percent, 45F)); paneles.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            Panel panelClientes = CrearPanelResumenLineas("Líneas por cliente", out lblResumenClientesLineas);
            Panel panelPlanes = CrearPanelResumenLineas("Líneas por plan", out lblResumenPlanesLineas);
            panelClientes.Margin = new Padding(0, 0, 0, 5); panelPlanes.Margin = new Padding(0, 5, 0, 0);
            paneles.Controls.Add(panelClientes, 0, 0); paneles.Controls.Add(panelPlanes, 0, 1);
            detalle.Controls.Add(paneles, 1, 0);
            raiz.Controls.Add(encabezadoDashboard, 0, 0); raiz.Controls.Add(tarjetas, 0, 1); raiz.Controls.Add(detalle, 0, 2);
            pnlDashboardLineas.Controls.Add(raiz);
        }

        private Panel CrearPanelResumenLineas(string titulo, out Label contenido)
        {
            Panel panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(9, 6, 9, 6),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty };
            Label encabezado = new Label { Text = titulo, Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true };
            contenido = new Label { Dock = DockStyle.Fill, ForeColor = Color.White, Font = new Font("Segoe UI", 9F),
                AutoEllipsis = true, TextAlign = ContentAlignment.TopLeft };
            panel.Controls.Add(contenido); panel.Controls.Add(encabezado);
            return panel;
        }

        private Panel CrearPanelGraficaLineas()
        {
            Panel panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(7, 5, 7, 5),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty };
            Label encabezado = new Label { Text = "Líneas por tipo", Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true };
            chartLineas = new Chart { Dock = DockStyle.Fill, BackColor = ColorTranslator.FromHtml("#0B3558") };
            chartLineas.Palette = ChartColorPalette.None;
            ChartArea area = new ChartArea("Lineas") { BackColor = ColorTranslator.FromHtml("#0B3558") };
            area.AxisX.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F); area.AxisX.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F); area.AxisY.LabelStyle.Format = "N0";
            area.AxisY.LineColor = ColorTranslator.FromHtml("#526779"); area.AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#34495E");
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartLineas.ChartAreas.Add(area);
            chartLineas.Series.Add(new Series("Por tipo") { ChartArea = "Lineas", ChartType = SeriesChartType.Column,
                BorderWidth = 0, Color = ColorTranslator.FromHtml("#00D9FF"), IsXValueIndexed = true });
            panel.Controls.Add(chartLineas); panel.Controls.Add(encabezado);
            return panel;
        }

        private void ConstruirOrdenBloquesLineas()
        {
            layoutBloquesLineas = new TableLayoutPanel { Name = "layoutBloquesLineas", Dock = DockStyle.Top,
                ColumnCount = 1, RowCount = 5, Padding = Padding.Empty, Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize };
            layoutBloquesLineas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesLineas.RowStyles.Add(new RowStyle(SizeType.Absolute, 360));
            layoutBloquesLineas.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            layoutBloquesLineas.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlFormulario.Height));
            layoutBloquesLineas.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlAcciones.Height));
            layoutBloquesLineas.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlCatalogo.Height));
            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(layoutBloquesLineas);
            layoutBloquesLineas.Controls.Add(pnlDashboardLineas, 0, 0);
            layoutBloquesLineas.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = pnlContenido.BackColor }, 0, 1);
            layoutBloquesLineas.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesLineas.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesLineas.Controls.Add(pnlCatalogo, 0, 4);
            pnlFormulario.Dock = pnlAcciones.Dock = pnlCatalogo.Dock = DockStyle.Fill;
            pnlDashboardLineas.Dock = DockStyle.Fill;
        }

        private void ActualizarDashboardLineas()
        {
            if (indicadoresLineas == null) return;
            List<LineasEntidad> datos = lineas.ToList();
            indicadoresLineas[0].Text = datos.Count.ToString();
            indicadoresLineas[1].Text = datos.Count(x => x.Estado).ToString();
            indicadoresLineas[2].Text = datos.Count(x => !x.Estado).ToString();
            indicadoresLineas[3].Text = datos.Select(x => x.TipoLinea).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().Count().ToString();
            chartLineas.Series[0].Points.Clear();
            foreach (var grupo in datos.Where(x => !string.IsNullOrWhiteSpace(x.TipoLinea)).GroupBy(x => x.TipoLinea).OrderBy(x => x.Key))
                chartLineas.Series[0].Points.AddXY(grupo.Key, grupo.Count());
            lblResumenClientesLineas.Text = datos.Count == 0 ? "No hay líneas registradas." : string.Join("\r\n", datos.GroupBy(x => x.CodigoCliente).OrderByDescending(x => x.Count()).Take(5).Select(x => "Cliente " + x.Key + ": " + x.Count()));
            lblResumenPlanesLineas.Text = datos.Count == 0 ? "No hay líneas registradas." : string.Join("\r\n", datos.GroupBy(x => x.CodigoPlan).OrderByDescending(x => x.Count()).Take(5).Select(x => "Plan " + x.Key + ": " + x.Count()));
        }

        private void AplicarEstiloDashboardLineas()
        {
            if (pnlDashboardLineas == null) return;
            Color fondo = ColorTranslator.FromHtml("#08243A");
            Color superficie = ColorTranslator.FromHtml("#0B3558");
            pnlDashboardLineas.BackColor = fondo;
            btnActualizarDashboardLineas.BackColor = ColorTranslator.FromHtml("#0B6E91");
            btnActualizarDashboardLineas.ForeColor = Color.White;
            btnActualizarDashboardLineas.FlatStyle = FlatStyle.Flat;
            btnActualizarDashboardLineas.FlatAppearance.BorderSize = 0;
            foreach (Control control in DescendientesLineas(pnlDashboardLineas))
            {
                if (control is TableLayoutPanel) control.BackColor = fondo;
                else if (control is Panel) control.BackColor = control.Name == "encabezadoDashboard" ? fondo : superficie;
            }
            chartLineas.BackColor = superficie;
            foreach (Label etiqueta in DescendientesLineas(pnlDashboardLineas).OfType<Label>())
            {
                if (etiqueta.Font.Size >= 13F) etiqueta.ForeColor = Color.White;
                else if (etiqueta.Font.Bold && etiqueta.Dock == DockStyle.Top) etiqueta.ForeColor = ColorTranslator.FromHtml("#00D9FF");
                else if (etiqueta.Font.Size <= 8F) etiqueta.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
                else etiqueta.ForeColor = Color.White;
            }
            foreach (Label valor in indicadoresLineas) valor.ForeColor = Color.White;
            if (lblResumenClientesLineas != null) lblResumenClientesLineas.ForeColor = Color.White;
            if (lblResumenPlanesLineas != null) lblResumenPlanesLineas.ForeColor = Color.White;
        }

        private static IEnumerable<Control> DescendientesLineas(Control control)
        {
            foreach (Control hijo in control.Controls)
            {
                yield return hijo;
                foreach (Control descendiente in DescendientesLineas(hijo)) yield return descendiente;
            }
        }

        private void AjustarInterfazLineas()
        {
            if (IsDisposed || layoutBloquesLineas == null) return;
            int ancho = Math.Min(195, Math.Max(148, (int)(ClientSize.Width * .18)));
            pnlMenu.Width = ancho; pnlHeader.Left = ancho; pnlContenido.Left = ancho;
            pnlHeader.Width = Math.Max(0, ClientSize.Width - ancho);
            AjustarCatalogoModeloLineas();
            int altoFijo = pnlContenido.Padding.Vertical + pnlFormulario.Height + pnlAcciones.Height + pnlCatalogo.Height + 10;
            int altoDashboard = Math.Max(360, pnlContenido.ClientSize.Height - altoFijo);
            layoutBloquesLineas.RowStyles[0].Height = altoDashboard;
            layoutBloquesLineas.RowStyles[2].Height = pnlFormulario.Height;
            layoutBloquesLineas.RowStyles[3].Height = pnlAcciones.Height;
            layoutBloquesLineas.RowStyles[4].Height = pnlCatalogo.Height;
            layoutBloquesLineas.Height = altoDashboard + 10 + pnlFormulario.Height + pnlAcciones.Height + pnlCatalogo.Height;
        }

        private void AjustarCatalogoModeloLineas()
        {
            if (IsDisposed || pnlCatalogo.ClientSize.Width <= 0) return;
            int altoGrilla = dgvLineas.ColumnHeadersHeight + 10 * dgvLineas.RowTemplate.Height + 2;
            dgvLineas.Height = altoGrilla;
            Panel paginacion = pnlCatalogo.Controls.Find("pnlPaginacion", false).OfType<Panel>().FirstOrDefault();
            if (paginacion != null)
            {
                Button anterior = paginacion.Controls.Find("btnPaginaAnterior", false).OfType<Button>().FirstOrDefault();
                Button siguiente = paginacion.Controls.Find("btnPaginaSiguiente", false).OfType<Button>().FirstOrDefault();
                if (anterior != null) anterior.Size = new Size(128, 32);
                if (siguiente != null) siguiente.Size = new Size(128, 32);
                paginacion.Height = 48;
            }
            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height +
                pnlColumnas.Height + altoGrilla + (paginacion == null ? lblRegistros.Height : paginacion.Height);
            pnlCatalogo.Height = altoCatalogo;
        }

        private void ConstruirNavegacionLateral()
        {
            Dictionary<string, Label> opciones = pnlItemsMenu.Controls.OfType<Label>()
                .Where(x => x.Tag != null).ToDictionary(x => x.Tag.ToString(), x => x);
            pnlItemsMenu.Controls.Clear();
            TableLayoutPanel filas = new TableLayoutPanel
            {
                Name = "layoutBotonesBarraLateral", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 18, Margin = Padding.Empty,
                Padding = Padding.Empty, BackColor = ColorTranslator.FromHtml("#061D30")
            };
            filas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 18; i++) filas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 18));
            string[] destinos = { "FrmUsuarios", "FrmSucursales", "FrmEmpleados", "FrmClientes", "FrmPlanes", "FrmLineas",
                "FrmProveedores", "FrmMarcas", "FrmEquipos", "FrmCompras", "FrmInventario", "FrmVentas",
                "FrmFacturacion", "FrmPagos", "FrmCardex", "FrmReclamos" };
            IconButton volver = CrearBotonLateral("btnVolverMenu", "Volver al menú principal", IconChar.Home, null);
            volver.BackColor = ColorTranslator.FromHtml("#08243A");
            volver.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#526779");
            volver.FlatAppearance.BorderSize = 1;
            volver.Click += (s, e) => Navegacion.VolverAlMenu(this);
            filas.Controls.Add(volver, 0, 0);
            for (int i = 0; i < destinos.Length; i++)
            {
                Label referencia;
                if (!opciones.TryGetValue(destinos[i], out referencia)) continue;
                IconButton boton = CrearBotonLateral("btn" + destinos[i].Substring(3),
                    referencia.Text.Trim().TrimStart('›', '⌂').Trim(), IconoModulo(destinos[i]), destinos[i]);
                filas.Controls.Add(boton, 0, i + 1);
            }
            IconButton salir = CrearBotonLateral("btnCerrarSesion", "Cerrar sesión", IconChar.SignOutAlt, null);
            salir.BackColor = ColorTranslator.FromHtml("#A83F48");
            salir.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#D56A70");
            salir.FlatAppearance.BorderSize = 1;
            salir.Click += (s, e) => Navegacion.CerrarSesion(this);
            filas.Controls.Add(salir, 0, 17);

            pnlMenu.SuspendLayout();
            pnlMenu.Controls.Clear();
            pnlLogo.Dock = DockStyle.None;
            pnlLogo.SetBounds(8, 8, pnlMenu.ClientSize.Width - 16, 142);
            lblLogo.Text = "📱";
            lblLogo.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblSistema.Text = "Sistema de\r\nTelefonía\r\ny Accesorios";
            lblSistema.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsuario.Dock = DockStyle.None;
            lblUsuario.SetBounds(8, 150, pnlMenu.ClientSize.Width - 16, 24);
            lblUsuarioRol.Dock = DockStyle.None;
            lblUsuarioRol.SetBounds(8, 174, pnlMenu.ClientSize.Width - 16, 24);
            pnlItemsMenu.Dock = DockStyle.None;
            pnlItemsMenu.SetBounds(8, 198, pnlMenu.ClientSize.Width - 16, pnlMenu.ClientSize.Height - 206);
            pnlLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblUsuarioRol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlItemsMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlItemsMenu.Controls.Add(filas);
            pnlMenu.Controls.Add(pnlItemsMenu);
            pnlMenu.Controls.Add(lblUsuarioRol);
            pnlMenu.Controls.Add(lblUsuario);
            pnlMenu.Controls.Add(pnlLogo);
            pnlMenu.ResumeLayout(true);
            pnlMenu.SizeChanged += (s, e) =>
            {
                pnlLogo.Width = Math.Max(0, pnlMenu.ClientSize.Width - 16);
                lblUsuario.Width = lblUsuarioRol.Width = Math.Max(0, pnlMenu.ClientSize.Width - 16);
                pnlItemsMenu.SetBounds(8, 198, Math.Max(0, pnlMenu.ClientSize.Width - 16), Math.Max(0, pnlMenu.ClientSize.Height - 206));
            };
        }

        private IconButton CrearBotonLateral(string nombre, string texto, IconChar icono, string destino)
        {
            IconButton boton = new IconButton
            {
                Name = nombre, Text = texto, Tag = destino, Dock = DockStyle.Fill,
                Margin = Padding.Empty, Padding = new Padding(12, 0, 4, 0),
                TextAlign = ContentAlignment.MiddleLeft, ImageAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText, IconChar = icono,
                IconFont = IconFont.Auto, IconColor = ColorTranslator.FromHtml("#00D9FF"), IconSize = 20,
                BackColor = ColorTranslator.FromHtml("#061D30"), ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand, UseVisualStyleBackColor = false
            };
            boton.FlatAppearance.BorderSize = 0;
            if (destino != null)
            {
                if (destino == GetType().Name) boton.BackColor = ColorTranslator.FromHtml("#0B3558");
                boton.Click += MenuOpcion_Click;
                boton.MouseEnter += (s, e) => { boton.BackColor = ColorTranslator.FromHtml("#0B3558"); boton.ForeColor = ColorTranslator.FromHtml("#00D9FF"); };
                boton.MouseLeave += (s, e) => { bool activo = destino == GetType().Name; boton.BackColor = ColorTranslator.FromHtml(activo ? "#0B3558" : "#061D30"); boton.ForeColor = activo ? ColorTranslator.FromHtml("#00D9FF") : Color.White; };
            }
            else
            {
                boton.MouseEnter += (s, e) => boton.BackColor = ColorTranslator.FromHtml(
                    nombre == "btnCerrarSesion" ? "#B9434A" : "#005684");
                boton.MouseLeave += (s, e) => AplicarEstiloNavegacionLineas();
            }
            return boton;
        }

        private static IconChar IconoModulo(string destino)
        {
            switch (destino)
            {
                case "FrmUsuarios": return IconChar.UserShield; case "FrmSucursales": return IconChar.Building;
                case "FrmEmpleados": return IconChar.UserTie; case "FrmClientes": return IconChar.Users;
                case "FrmPlanes": return IconChar.Wifi; case "FrmLineas": return IconChar.SimCard;
                case "FrmProveedores": return IconChar.Truck; case "FrmMarcas": return IconChar.Tags;
                case "FrmEquipos": return IconChar.MobileAlt; case "FrmCompras": return IconChar.CartPlus;
                case "FrmInventario": return IconChar.Boxes; case "FrmVentas": return IconChar.ShoppingCart;
                case "FrmFacturacion": return IconChar.FileInvoiceDollar; case "FrmPagos": return IconChar.CreditCard;
                case "FrmCardex": return IconChar.ChartLine; default: return IconChar.Headset;
            }
        }
        private void ConectarEventos()
        {
            foreach (Button boton in new[] { btnNuevo, btnGuardar, btnEditar, btnEliminar, btnLimpiar, btnBuscar })
                boton.EnabledChanged += (s, e) => TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                MostrarLineas(lineas.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarLineas();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarLineas();
                    e.SuppressKeyPress = true;
                }
            };

            dgvLineas.SelectionChanged += DgvLineas_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoCliente.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoPlan.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNumeroTelefonico.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTipoLinea.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaActivacion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaVencimiento.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void AplicarEstiloNavegacionLineas()
        {
            TableLayoutPanel filas = pnlItemsMenu.Controls.Find("layoutBotonesBarraLateral", false)
                .OfType<TableLayoutPanel>().FirstOrDefault();
            if (filas == null) return;
            foreach (IconButton boton in filas.Controls.OfType<IconButton>())
            {
                if (boton.Name == "btnVolverMenu")
                {
                    boton.BackColor = ColorTranslator.FromHtml("#08243A");
                    boton.ForeColor = Color.White;
                    boton.IconColor = ColorTranslator.FromHtml("#00D9FF");
                    boton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#526779");
                    boton.FlatAppearance.BorderSize = 1;
                    continue;
                }
                if (boton.Name == "btnCerrarSesion")
                {
                    boton.BackColor = ColorTranslator.FromHtml("#A83F48");
                    boton.ForeColor = Color.White;
                    boton.IconColor = Color.White;
                    boton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#D56A70");
                    boton.FlatAppearance.BorderSize = 1;
                    continue;
                }
                bool activo = string.Equals(Convert.ToString(boton.Tag), GetType().Name, StringComparison.OrdinalIgnoreCase);
                boton.BackColor = ColorTranslator.FromHtml(activo ? "#0B3558" : "#061D30");
                boton.ForeColor = activo ? ColorTranslator.FromHtml("#00D9FF") : Color.White;
                boton.IconColor = ColorTranslator.FromHtml("#00D9FF");
            }
        }

        private void ConfigurarResaltadoCamposLineas()
        {
            Control[] campos = { cmbCodigoCliente, cmbCodigoPlan, txtNumeroTelefonico, cmbTipoLinea,
                dtpFechaActivacion, dtpFechaVencimiento };
            foreach (Control campo in campos)
            {
                CampoRedondeado.Aplicar(campo, 7);
                campo.MouseEnter += (s, e) => { if (campo.Enabled && !campo.Focused) campo.BackColor = Color.FromArgb(205, 243, 255); };
                campo.MouseLeave += (s, e) => RestaurarColorCampoLinea(campo);
                campo.Enter += (s, e) => { if (campo.Enabled) campo.BackColor = ColorTranslator.FromHtml("#80DEFF"); };
                campo.Leave += (s, e) => RestaurarColorCampoLinea(campo);
                campo.EnabledChanged += (s, e) => RestaurarColorCampoLinea(campo);
            }
        }

        private void RestaurarColorCampoLinea(Control campo)
        {
            if (campo == null || campo.IsDisposed) return;
            campo.BackColor = campo.Enabled ? ColorTranslator.FromHtml("#E1ECF6") : Color.FromArgb(215, 222, 230);
            campo.ForeColor = campo.Enabled ? ColorTranslator.FromHtml("#072342") : Color.FromArgb(104, 116, 128);
        }

        private void ConfigurarNavegacionTecladoLineas()
        {
            Control[] orden = { cmbCodigoCliente, cmbCodigoPlan, txtNumeroTelefonico, cmbTipoLinea,
                dtpFechaActivacion, dtpFechaVencimiento, rdoActivo, rdoInactivo };
            foreach (Control control in orden) control.KeyDown += CampoLinea_KeyDown;
            AcceptButton = null;
        }

        private void CampoLinea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Alt || e.Control || e.Shift) return;
            ComboBox lista = sender as ComboBox;
            if (lista != null && lista.DroppedDown) return;
            Control actual = sender as Control;
            if (actual == null || !ValidarCampoParaAvanzarLinea(actual)) return;
            e.SuppressKeyPress = true;
            e.Handled = true;
            Control[] orden = { cmbCodigoCliente, cmbCodigoPlan, txtNumeroTelefonico, cmbTipoLinea,
                dtpFechaActivacion, dtpFechaVencimiento, rdoActivo, rdoInactivo };
            for (int i = Array.IndexOf(orden, actual) + 1; i < orden.Length; i++)
                if (orden[i].Enabled && orden[i].Visible) { orden[i].Focus(); return; }
        }

        private bool ValidarCampoParaAvanzarLinea(Control campo)
        {
            if (campo == cmbCodigoCliente && !ComboFK.Obtener(cmbCodigoCliente).HasValue)
            { MessageBox.Show("Selecciona un cliente."); cmbCodigoCliente.Focus(); return false; }
            if (campo == cmbCodigoPlan && !ComboFK.Obtener(cmbCodigoPlan).HasValue)
            { MessageBox.Show("Selecciona un plan."); cmbCodigoPlan.Focus(); return false; }
            if (campo == txtNumeroTelefonico && string.IsNullOrWhiteSpace(txtNumeroTelefonico.Text))
            { MessageBox.Show("El campo Número telefónico es obligatorio."); txtNumeroTelefonico.Focus(); return false; }
            if (campo == cmbTipoLinea && string.IsNullOrWhiteSpace(cmbTipoLinea.Text))
            { MessageBox.Show("Selecciona el tipo de línea."); cmbTipoLinea.Focus(); return false; }
            return true;
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Control opcion = sender as Control;
            if (opcion != null && opcion.Tag != null)
                Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoLinea", chkCodigo.Checked);
            CambiarVisibilidad("CodigoCliente", chkCodigoCliente.Checked);
            CambiarVisibilidad("CodigoPlan", chkCodigoPlan.Checked);
            CambiarVisibilidad("NumeroTelefonico", chkNumeroTelefonico.Checked);
            CambiarVisibilidad("TipoLinea", chkTipoLinea.Checked);
            CambiarVisibilidad("FechaActivacion", chkFechaActivacion.Checked);
            CambiarVisibilidad("FechaVencimiento", chkFechaVencimiento.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<LineasEntidad> registros = relacional.Listar<LineasEntidad>();
                lineas.Clear();
                foreach (LineasEntidad registro in registros) lineas.Add(registro);
                MostrarLineas(lineas.ToList());
                ActualizarContador();
                ActualizarDashboardLineas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActualizarContador();
            }
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvLineas.Columns.Contains(columna))
                dgvLineas.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvLineas.Rows.Count > 0)
                {
                    int indice = dgvLineas.CurrentRow == null ? 0 : dgvLineas.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvLineas.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvLineas.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvLineas.Rows.Count)
                return;

            dgvLineas.ClearSelection();

            DataGridViewCell celda = dgvLineas.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvLineas.CurrentCell = celda;

            dgvLineas.Rows[indice].Selected = true;
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
            LineasEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoLinea, CrearEntidad());
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
            LineasEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoLinea + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<LineasEntidad>(seleccionado.CodigoLinea);
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

        private LineasEntidad CrearEntidad()
        {
            return new LineasEntidad(
                ComboFK.Obtener(cmbCodigoCliente).Value,
                ComboFK.Obtener(cmbCodigoPlan).Value,
                txtNumeroTelefonico.Text.Trim(),
                cmbTipoLinea.Text.Trim(),
                dtpFechaActivacion.Checked ? (DateTime?)dtpFechaActivacion.Value : null,
                dtpFechaVencimiento.Checked ? (DateTime?)dtpFechaVencimiento.Value : null,
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

            if (!ComboFK.Obtener(cmbCodigoPlan).HasValue)
            {
                MessageBox.Show("El campo Cód. plan debe ser mayor que cero.");
                cmbCodigoPlan.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroTelefonico.Text))
            {
                MessageBox.Show("El campo Número telefónico es obligatorio.");
                txtNumeroTelefonico.Focus();
                return false;
            }

            if (txtNumeroTelefonico.Text.Trim().Length > 20)
            {
                MessageBox.Show("El campo Número telefónico no puede superar 20 caracteres.");
                txtNumeroTelefonico.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbTipoLinea.Text))
            {
                MessageBox.Show("El campo Tipo de línea es obligatorio.");
                cmbTipoLinea.Focus();
                return false;
            }

            if (cmbTipoLinea.Text.Trim().Length > 20)
            {
                MessageBox.Show("El campo Tipo de línea no puede superar 20 caracteres.");
                cmbTipoLinea.Focus();
                return false;
            }

            if (dtpFechaActivacion.Checked && dtpFechaVencimiento.Checked &&
                dtpFechaVencimiento.Value < dtpFechaActivacion.Value)
            {
                MessageBox.Show("La fecha de vencimiento no puede ser anterior a la fecha de activación.");
                dtpFechaVencimiento.Focus();
                return false;
            }

            return true;
        }

        private LineasEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvLineas.CurrentRow == null)
                return null;

            return dgvLineas.CurrentRow.DataBoundItem as LineasEntidad;
        }

        private void DgvLineas_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            LineasEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoLinea.ToString();
            ComboFK.Seleccionar(cmbCodigoCliente, registro.CodigoCliente);
            ComboFK.Seleccionar(cmbCodigoPlan, registro.CodigoPlan);
            txtNumeroTelefonico.Text = registro.NumeroTelefonico;
            cmbTipoLinea.SelectedIndex = cmbTipoLinea.FindStringExact(registro.TipoLinea ?? string.Empty);
            if (registro.FechaActivacion.HasValue)
            {
                dtpFechaActivacion.Value = LimitarFecha(dtpFechaActivacion, registro.FechaActivacion.Value);
                dtpFechaActivacion.Checked = true;
            }
            else
            {
                dtpFechaActivacion.Checked = false;
            }
            if (registro.FechaVencimiento.HasValue)
            {
                dtpFechaVencimiento.Value = LimitarFecha(dtpFechaVencimiento, registro.FechaVencimiento.Value);
                dtpFechaVencimiento.Checked = true;
            }
            else
            {
                dtpFechaVencimiento.Checked = false;
            }
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

        private void BuscarLineas()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarLineas(lineas.ToList());
                ActualizarContador();
                ActualizarDashboardLineas();
                return;
            }
            try
            {
                List<LineasEntidad> resultado = relacional.Buscar<LineasEntidad>(criterio);
                MostrarLineas(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + lineas.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarLineas(List<LineasEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvLineas.DataSource = origen;
            ActualizarColumnas();
            dgvLineas.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoCliente, null);
            ComboFK.Seleccionar(cmbCodigoPlan, null);
            txtNumeroTelefonico.Clear();
            cmbTipoLinea.SelectedIndex = -1;
            dtpFechaActivacion.Value = DateTime.Now;
            dtpFechaActivacion.Checked = false;
            dtpFechaVencimiento.Value = DateTime.Now;
            dtpFechaVencimiento.Checked = false;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvLineas.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoCliente.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de líneas registradas: " + lineas.Count;
        }
        private void CargarCatalogosFK()
        {
            try
            {
                ComboFK.Cargar(cmbCodigoCliente, relacional.ListarCatalogo<ClientesEntidad>(), x => x.CodigoCliente, x => x.Nombre + " | " + x.Telefono, false);
                ComboFK.Cargar(cmbCodigoPlan, relacional.ListarCatalogo<PlanesEntidad>(), x => x.CodigoPlan, x => "Plan " + x.Nombre, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
