using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmVentas : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<VentasEntidad> ventas = new BindingList<VentasEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;
        private Label lblDashboardVentas;
        private Label lblDashboardMonto;
        private Label lblDashboardActivas;
        private Button btnAbrirVentaSeleccionada;
        private const int RegistrosPorPagina = 10;
        private readonly List<VentasEntidad> resultadosVentas = new List<VentasEntidad>();
        private int paginaVentas = 1;
        private Panel pnlPaginacionVentas;
        private Label lblPaginaVentas;
        private Button btnPaginaAnteriorVentas, btnPaginaSiguienteVentas;
        private TableLayoutPanel layoutBloquesVentas;
        private Panel panelDashboardVentas;
        private Button btnAgregarVenta;
        private DashboardFacturacionVisual dashboardFacturacionVentas;

        public FrmVentas()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            ConfigurarDisenoVentas();
            CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = ventas;
            dgvVentas.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvVentas);

            ConectarEventos();
            ConstruirDashboardRelacional();
            ConfigurarPaginacionVentas();
            AjustarInterfazVentas();
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
                EstablecerResultadosVentas(ventas);
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarVentas();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarVentas();
                    e.SuppressKeyPress = true;
                }
            };

            dgvVentas.SelectionChanged += DgvVentas_SelectionChanged;
            dgvVentas.SelectionChanged += (s, e) => ActualizarBotonAbrirSeleccionada();
            dgvVentas.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) AbrirVentaCompleta(dgvVentas.Rows[e.RowIndex].DataBoundItem as VentasEntidad); };
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;
            chkSeleccionar.CheckedChanged += (s, e) => ActualizarBotonAbrirSeleccionada();

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoSucursal.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoCliente.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoEmpleado.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaVenta.CheckedChanged += (s, e) => ActualizarColumnas();
            chkSubtotal.CheckedChanged += (s, e) => ActualizarColumnas();
            chkIva.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTotal.CheckedChanged += (s, e) => ActualizarColumnas();
            chkObservaciones.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void ConstruirDashboardRelacional()
        {
            pnlFormulario.Visible = false;
            pnlAcciones.Visible = false;
            pnlCatalogo.Dock = DockStyle.None;
            pnlCatalogo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTitulo.Text = "Gestión de ventas";
            lblSubtitulo.Text = "Consulta y administra ventas con sus líneas de detalle";
            lblCatalogoTitulo.Text = "Ventas registradas";
            Panel dashboard = new Panel { Name = "pnlDashboardVentas", BackColor = ColorTranslator.FromHtml("#08243A"), Padding = new Padding(14) };
            panelDashboardVentas = dashboard;
            dashboard.Controls.Add(new Label { Text = "Resumen de ventas", ForeColor = Color.FromArgb(0, 190, 230), Font = new Font("Segoe UI", 12F, FontStyle.Bold), Location = new Point(14, 10), AutoSize = true });
            Button agregar = new Button { Name = "btnAgregarVenta", Text = "+  Agregar nueva venta", BackColor = Color.FromArgb(0, 150, 110), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Size = new Size(240, 44), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnAgregarVenta = agregar;
            agregar.Click += (s, e) => AbrirVentaCompleta(null); dashboard.Controls.Add(agregar);
            btnAbrirVentaSeleccionada = new Button { Text = "Ver / editar seleccionada", BackColor = Color.FromArgb(30, 105, 165), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Size = new Size(220, 44), Anchor = AnchorStyles.Top | AnchorStyles.Right, Enabled = false };
            dashboard.Controls.Add(btnAbrirVentaSeleccionada);
            btnAbrirVentaSeleccionada.Click += (s, e) => { VentasEntidad v = ObtenerSeleccionado(); if (v != null) AbrirVentaCompleta(v); };
            lblDashboardVentas = TarjetaDashboard(dashboard, "Ventas registradas", 14);
            lblDashboardMonto = TarjetaDashboard(dashboard, "Monto total", 340);
            lblDashboardActivas = TarjetaDashboard(dashboard, "Ventas activas", 666);
            lblDashboardVentas.Tag = lblDashboardVentas.Parent; lblDashboardMonto.Tag = lblDashboardMonto.Parent; lblDashboardActivas.Tag = lblDashboardActivas.Parent;
            pnlContenido.Controls.Add(dashboard);
            ConstruirLayoutVentas(dashboard);
            InstalarDashboardFacturacionVentas();
            MoverAccionReporteVentas("btnImportarExcel");
            MoverAccionReporteVentas("btnExportarExcel");
            MoverAccionReporteVentas("btnImprimir");
            pnlContenido.Resize += (s, e) => AjustarInterfazVentas();
            lblMenuDetalleVenta.Visible = false;
            AjustarInterfazVentas();
        }

        private void InstalarDashboardFacturacionVentas()
        {
            dashboardFacturacionVentas = new DashboardFacturacionVisual("ventas",
                new Control[] { btnAgregarVenta, btnAbrirVentaSeleccionada },
                (s, e) => ConsultarRelacional());
            layoutBloquesVentas.Controls.Remove(panelDashboardVentas);
            layoutBloquesVentas.Controls.Add(dashboardFacturacionVentas.Panel, 0, 0);
            panelDashboardVentas = dashboardFacturacionVentas.Panel;
            lblDashboardVentas = dashboardFacturacionVentas.Valores[0];
            lblDashboardMonto = dashboardFacturacionVentas.Valores[1];
            lblDashboardActivas = dashboardFacturacionVentas.Valores[2];
        }

        private void MoverAccionReporteVentas(string nombre)
        {
            // pnlAcciones se retira del árbol visible al construir el layout; buscar
            // en layoutBotones evita perder los botones de reporte al reubicarlos.
            Button boton = layoutBotones.Controls.Find(nombre, false).OfType<Button>().FirstOrDefault();
            if (boton == null || boton.Parent == null) return;
            boton.Parent.Controls.Remove(boton);
            boton.Dock = DockStyle.None;
            boton.Size = new Size(nombre == "btnImprimir" ? 132 : 158, 36);
            boton.Margin = new Padding(4, 9, 4, 9);
            boton.TextAlign = ContentAlignment.MiddleCenter;
            dashboardFacturacionVentas.Acciones.Controls.Add(boton);
        }

        private void ConfigurarDisenoVentas()
        {
            pnlHeader.Height = 108;
            pnlContenido.Padding = new Padding(14);
            pnlContenido.AutoScroll = true;
            pnlFormulario.Dock = DockStyle.Top;
            pnlAcciones.Dock = DockStyle.Top;
            pnlCatalogo.Dock = DockStyle.Top;
            lblFormularioTitulo.Height = 40;
            lblCatalogoTitulo.Height = 40;
            lblColumnas.Height = 40;
            pnlBusqueda.Height = 54;
            pnlColumnas.Height = 68;
            selector.AutoScroll = false;
            dgvVentas.Dock = DockStyle.Top;
            dgvVentas.RowTemplate.Height = 29;
            dgvVentas.ColumnHeadersHeight = 34;
            dgvVentas.ScrollBars = ScrollBars.Horizontal;
        }

        private void ConstruirLayoutVentas(Panel dashboard)
        {
            layoutBloquesVentas = new TableLayoutPanel
            {
                Name = "layoutBloquesVentas", Dock = DockStyle.Top, ColumnCount = 1,
                RowCount = 3, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesVentas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesVentas.RowStyles.Add(new RowStyle(SizeType.Absolute, 360F));
            layoutBloquesVentas.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutBloquesVentas.RowStyles.Add(new RowStyle(SizeType.Absolute, 400F));
            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Remove(dashboard);
            pnlContenido.Controls.Add(layoutBloquesVentas);
            dashboard.Dock = DockStyle.Fill;
            pnlCatalogo.Dock = DockStyle.Fill;
            pnlCatalogo.Margin = Padding.Empty;
            layoutBloquesVentas.Controls.Add(dashboard, 0, 0);
            Panel separador = new Panel { Dock = DockStyle.Fill, Margin = Padding.Empty, BackColor = pnlContenido.BackColor };
            layoutBloquesVentas.Controls.Add(separador, 0, 1);
            layoutBloquesVentas.Controls.Add(pnlCatalogo, 0, 2);
        }

        private void ConfigurarPaginacionVentas()
        {
            pnlPaginacionVentas = new Panel { Name = "pnlPaginacionVentas", Dock = DockStyle.Bottom, Height = 54, BackColor = ColorTranslator.FromHtml("#08243A") };
            QuitarPaginacionComunVentas();
            if (lblRegistros.Parent != null) lblRegistros.Parent.Controls.Remove(lblRegistros);
            lblRegistros.Dock = DockStyle.None;
            lblPaginaVentas = new Label { Name = "lblPaginaVentas", ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter };
            btnPaginaAnteriorVentas = CrearBotonPagina("‹ Anterior", "btnPaginaAnteriorVentas");
            btnPaginaSiguienteVentas = CrearBotonPagina("Siguiente ›", "btnPaginaSiguienteVentas");
            btnPaginaAnteriorVentas.Click += (s, e) => CambiarPaginaVentas(paginaVentas - 1);
            btnPaginaSiguienteVentas.Click += (s, e) => CambiarPaginaVentas(paginaVentas + 1);
            pnlPaginacionVentas.Controls.Add(lblRegistros);
            pnlPaginacionVentas.Controls.Add(lblPaginaVentas);
            pnlPaginacionVentas.Controls.Add(btnPaginaAnteriorVentas);
            pnlPaginacionVentas.Controls.Add(btnPaginaSiguienteVentas);
            pnlCatalogo.Controls.Add(pnlPaginacionVentas);
            pnlPaginacionVentas.BringToFront();
            pnlPaginacionVentas.Resize += (s, e) => AcomodarPaginacionVentas();
            AjustarInterfazVentas();
        }

        private void QuitarPaginacionComunVentas()
        {
            foreach (Control control in Controls.Find("pnlPaginacion", true))
                if (control.Parent != null) control.Parent.Controls.Remove(control);
        }

        private static Button CrearBotonPagina(string texto, string nombre)
        {
            return new Button { Name = nombre, Text = texto, Size = new Size(132, 42), FlatStyle = FlatStyle.Flat,
                BackColor = ColorTranslator.FromHtml("#0B6E91"), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand };
        }

        private void AcomodarPaginacionVentas()
        {
            if (pnlPaginacionVentas == null || pnlPaginacionVentas.ClientSize.Width <= 0) return;
            const int separacion = 8;
            int y = Math.Max(0, (pnlPaginacionVentas.ClientSize.Height - 42) / 2);
            int derecha = pnlPaginacionVentas.ClientSize.Width - 8;
            btnPaginaSiguienteVentas.SetBounds(derecha - 132, y, 132, 42);
            lblPaginaVentas.SetBounds(btnPaginaSiguienteVentas.Left - separacion - 132, y, 132, 42);
            btnPaginaAnteriorVentas.SetBounds(lblPaginaVentas.Left - separacion - 132, y, 132, 42);
            lblRegistros.SetBounds(8, y, Math.Max(120, btnPaginaAnteriorVentas.Left - 16), 42);
        }

        private void AjustarInterfazVentas()
        {
            if (IsDisposed || panelDashboardVentas == null || dgvVentas == null || layoutBloquesVentas == null) return;
            int ancho = Math.Max(0, pnlContenido.ClientSize.Width - 30);
            int altoDashboard = Math.Max(380, Math.Min(430, pnlContenido.ClientSize.Height - 390));
            int altoFila = Math.Max(26, dgvVentas.RowTemplate.Height);
            dgvVentas.Height = dgvVentas.ColumnHeadersHeight + RegistrosPorPagina * altoFila + 2;
            if (pnlPaginacionVentas != null) pnlPaginacionVentas.Height = 58;
            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height +
                pnlColumnas.Height + dgvVentas.Height + (pnlPaginacionVentas == null ? 58 : pnlPaginacionVentas.Height);
            panelDashboardVentas.Height = altoDashboard;
            layoutBloquesVentas.RowStyles[0].Height = altoDashboard;
            layoutBloquesVentas.RowStyles[2].Height = altoCatalogo;
            layoutBloquesVentas.Height = altoDashboard + 10 + altoCatalogo;
            if (pnlPaginacionVentas != null) AcomodarPaginacionVentas();
        }

        private void CambiarPaginaVentas(int nuevaPagina)
        {
            int totalPaginas = (int)Math.Ceiling(resultadosVentas.Count / (double)RegistrosPorPagina);
            if (nuevaPagina < 1 || nuevaPagina > totalPaginas) return;
            paginaVentas = nuevaPagina;
            MostrarPaginaVentas();
        }

        private void MostrarPaginaVentas()
        {
            int totalPaginas = (int)Math.Ceiling(resultadosVentas.Count / (double)RegistrosPorPagina);
            if (totalPaginas == 0) paginaVentas = 1;
            else if (paginaVentas > totalPaginas) paginaVentas = totalPaginas;
            List<VentasEntidad> pagina = resultadosVentas.Skip((paginaVentas - 1) * RegistrosPorPagina).Take(RegistrosPorPagina).ToList();
            origen.DataSource = null;
            origen.DataSource = pagina;
            dgvVentas.DataSource = origen;
            ActualizarColumnas();
            if (dgvVentas.Columns.Contains("NumeroFila")) dgvVentas.Columns.Remove("NumeroFila");
            dgvVentas.Columns.Insert(0, new DataGridViewTextBoxColumn { Name = "NumeroFila", HeaderText = "N.º", ReadOnly = true, Width = 52, SortMode = DataGridViewColumnSortMode.NotSortable });
            for (int i = 0; i < dgvVentas.Rows.Count; i++) dgvVentas.Rows[i].Cells["NumeroFila"].Value = i + 1;
            dgvVentas.ClearSelection();
            lblPaginaVentas.Text = "Página " + (totalPaginas == 0 ? 0 : paginaVentas) + " de " + totalPaginas;
            btnPaginaAnteriorVentas.Enabled = paginaVentas > 1;
            btnPaginaSiguienteVentas.Enabled = paginaVentas < totalPaginas;
            lblRegistros.Text = "Resultados: " + resultadosVentas.Count + " | Total: " + ventas.Count;
            AcomodarPaginacionVentas();
        }

        private void EstablecerResultadosVentas(IEnumerable<VentasEntidad> lista)
        {
            resultadosVentas.Clear();
            resultadosVentas.AddRange(lista);
            paginaVentas = 1;
            MostrarPaginaVentas();
        }

        private Label TarjetaDashboard(Panel dashboard, string titulo, int izquierda)
        {
            Panel tarjeta = new Panel { BackColor = Color.FromArgb(10, 48, 84), Location = new Point(izquierda, 44), Size = new Size(310, 86), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            tarjeta.Controls.Add(new Label { Text = titulo, ForeColor = Color.FromArgb(170, 200, 225), Location = new Point(12, 8), Size = new Size(286, 24) });
            Label valor = new Label { Text = "0", ForeColor = Color.White, Font = new Font("Segoe UI", 18F, FontStyle.Bold), Location = new Point(12, 32), Size = new Size(286, 42) };
            tarjeta.Controls.Add(valor); dashboard.Controls.Add(tarjeta); return valor;
        }

        private void AjustarDashboardVentas(Panel dashboard, Button agregar)
        {
            int anchoPanel = Math.Max(0, pnlContenido.ClientSize.Width - 30);
            int altoCatalogo = Math.Max(0, pnlContenido.ClientSize.Height - 392);
            dashboard.SetBounds(15, 15, anchoPanel, 347);
            pnlCatalogo.SetBounds(15, 377, anchoPanel, altoCatalogo);
            agregar.SetBounds(dashboard.ClientSize.Width - agregar.Width - 14, 7, agregar.Width, 36);
            btnAbrirVentaSeleccionada.SetBounds(dashboard.ClientSize.Width - agregar.Width - btnAbrirVentaSeleccionada.Width - 24, 7, btnAbrirVentaSeleccionada.Width, 36);
            Panel[] tarjetas = dashboard.Controls.OfType<Panel>().ToArray();
            int ancho = Math.Max(145, (dashboard.ClientSize.Width - 44) / 3);
            for (int i = 0; i < tarjetas.Length; i++) { tarjetas[i].SetBounds(14 + i * (ancho + 8), 44, ancho, 86); tarjetas[i].Controls[0].Width = ancho - 24; tarjetas[i].Controls[1].Width = ancho - 24; }
        }

        private void ActualizarBotonAbrirSeleccionada()
        {
            if (btnAbrirVentaSeleccionada != null) btnAbrirVentaSeleccionada.Enabled = ObtenerSeleccionado() != null;
        }

        private void AbrirVentaCompleta(VentasEntidad venta)
        {
            using (FrmMaestroDetalle<VentasEntidad, DetalleVentaEntidad> formulario = new FrmMaestroDetalle<VentasEntidad, DetalleVentaEntidad>(
                "Venta", "CodigoVenta", "CodigoDetalleVenta", "CodigoVenta",
                new[] { "Subtotal", "Iva", "Total" }, new[] { "Subtotal" }, venta))
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
            CambiarVisibilidad("CodigoVenta", chkCodigo.Checked);
            CambiarVisibilidad("CodigoSucursal", chkCodigoSucursal.Checked);
            CambiarVisibilidad("CodigoCliente", chkCodigoCliente.Checked);
            CambiarVisibilidad("CodigoEmpleado", chkCodigoEmpleado.Checked);
            CambiarVisibilidad("FechaVenta", chkFechaVenta.Checked);
            CambiarVisibilidad("Subtotal", chkSubtotal.Checked);
            CambiarVisibilidad("Iva", chkIva.Checked);
            CambiarVisibilidad("Total", chkTotal.Checked);
            CambiarVisibilidad("Observaciones", chkObservaciones.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<VentasEntidad> registros = relacional.Listar<VentasEntidad>();
                ventas.Clear();
                foreach (VentasEntidad registro in registros) ventas.Add(registro);
                EstablecerResultadosVentas(ventas);
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
            if (dgvVentas.Columns.Contains(columna))
                dgvVentas.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvVentas.Rows.Count > 0)
                {
                    int indice = dgvVentas.CurrentRow == null ? 0 : dgvVentas.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvVentas.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvVentas.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvVentas.Rows.Count)
                return;

            dgvVentas.ClearSelection();

            DataGridViewCell celda = dgvVentas.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvVentas.CurrentCell = celda;

            dgvVentas.Rows[indice].Selected = true;
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
            VentasEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoVenta, CrearEntidad());
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
            VentasEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoVenta + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.EliminarConDetalles<VentasEntidad, DetalleVentaEntidad>(
                    seleccionado.CodigoVenta, "CodigoVenta");
                ConsultarRelacional();
                LimpiarFormulario();
                MessageBox.Show("La venta y sus detalles quedaron inactivos.", "Eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private VentasEntidad CrearEntidad()
        {
            return new VentasEntidad(
                ComboFK.Obtener(cmbCodigoSucursal).Value,
                ComboFK.Obtener(cmbCodigoCliente).Value,
                ComboFK.Obtener(cmbCodigoEmpleado).Value,
                dtpFechaVenta.Value,
                nudSubtotal.Value,
                nudIva.Value,
                nudTotal.Value,
                ValorONulo(txtObservaciones.Text),
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoSucursal).HasValue)
            {
                MessageBox.Show("El campo Cód. sucursal debe ser mayor que cero.");
                cmbCodigoSucursal.Focus();
                return false;
            }

            if (!ComboFK.Obtener(cmbCodigoCliente).HasValue)
            {
                MessageBox.Show("El campo Cód. cliente debe ser mayor que cero.");
                cmbCodigoCliente.Focus();
                return false;
            }

            if (!ComboFK.Obtener(cmbCodigoEmpleado).HasValue)
            {
                MessageBox.Show("El campo Cód. empleado debe ser mayor que cero.");
                cmbCodigoEmpleado.Focus();
                return false;
            }

            if (nudSubtotal.Value <= 0)
            {
                MessageBox.Show("El campo Subtotal debe ser mayor que cero.");
                nudSubtotal.Focus();
                return false;
            }

            if (nudTotal.Value <= 0)
            {
                MessageBox.Show("El campo Total debe ser mayor que cero.");
                nudTotal.Focus();
                return false;
            }

            if (txtObservaciones.Text.Trim().Length > 200)
            {
                MessageBox.Show("El campo Observaciones no puede superar 200 caracteres.");
                txtObservaciones.Focus();
                return false;
            }

            return true;
        }

        private VentasEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvVentas.CurrentRow == null)
                return null;

            return dgvVentas.CurrentRow.DataBoundItem as VentasEntidad;
        }

        private void DgvVentas_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            VentasEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoVenta.ToString();
            ComboFK.Seleccionar(cmbCodigoSucursal, registro.CodigoSucursal);
            ComboFK.Seleccionar(cmbCodigoCliente, registro.CodigoCliente);
            ComboFK.Seleccionar(cmbCodigoEmpleado, registro.CodigoEmpleado);
            dtpFechaVenta.Value = LimitarFecha(dtpFechaVenta, registro.FechaVenta);
            nudSubtotal.Value = Limitar(nudSubtotal, registro.Subtotal);
            nudIva.Value = Limitar(nudIva, registro.Iva);
            nudTotal.Value = Limitar(nudTotal, registro.Total);
            txtObservaciones.Text = registro.Observaciones;
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

        private void BuscarVentas()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarVentas(ventas.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<VentasEntidad> resultado = relacional.Buscar<VentasEntidad>(criterio);
                EstablecerResultadosVentas(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarVentas(List<VentasEntidad> lista)
        {
            EstablecerResultadosVentas(lista);
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoSucursal, null);
            ComboFK.Seleccionar(cmbCodigoCliente, null);
            ComboFK.Seleccionar(cmbCodigoEmpleado, null);
            dtpFechaVenta.Value = DateTime.Now;
            nudSubtotal.Value = 0;
            nudIva.Value = 0;
            nudTotal.Value = 0;
            txtObservaciones.Clear();
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvVentas.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoSucursal.Focus();
        }

        private void ActualizarContador()
        {
            if (lblRegistros != null)
                lblRegistros.Text = "Resultados: " + resultadosVentas.Count + " | Total: " + ventas.Count;
            if (lblDashboardVentas != null) lblDashboardVentas.Text = ventas.Count.ToString();
            if (lblDashboardMonto != null) lblDashboardMonto.Text = "Q " + ventas.Sum(v => v.Total).ToString("N2");
            if (lblDashboardActivas != null) lblDashboardActivas.Text = ventas.Count(v => v.Estado).ToString();
            if (dashboardFacturacionVentas != null)
                dashboardFacturacionVentas.Actualizar(
                    new[] { "VENTAS REGISTRADAS", "MONTO VENDIDO", "VENTAS ACTIVAS", "VENTAS INACTIVAS" },
                    new[] { ventas.Count.ToString("N0"), "Q " + ventas.Sum(v => v.Total).ToString("N2"),
                        ventas.Count(v => v.Estado).ToString("N0"), ventas.Count(v => !v.Estado).ToString("N0") },
                    ventas.GroupBy(v => new { v.FechaVenta.Year, v.FechaVenta.Month })
                        .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                        .Select(g => new KeyValuePair<string, double>(new System.DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                            (double)g.Sum(v => v.Total))),
                    "Activas: " + ventas.Count(v => v.Estado) + Environment.NewLine +
                        "Inactivas: " + ventas.Count(v => !v.Estado),
                    ventas.OrderByDescending(v => v.FechaVenta).Take(4).Any()
                        ? string.Join(Environment.NewLine, ventas.OrderByDescending(v => v.FechaVenta).Take(4)
                            .Select(v => "Venta " + v.CodigoVenta + " · " + v.FechaVenta.ToString("dd/MM/yyyy") + " · Q " + v.Total.ToString("N2")))
                        : "No hay ventas registradas.");
        }
        private void CargarCatalogosFK()
        {
            try
            {
                ComboFK.Cargar(cmbCodigoSucursal, relacional.ListarCatalogo<SucursalesEntidad>(), x => x.CodigoSucursal, x => x.Nombre, false);
                ComboFK.Cargar(cmbCodigoCliente, relacional.ListarCatalogo<ClientesEntidad>(), x => x.CodigoCliente, x => x.Nombre + " | " + x.Telefono, false);
                ComboFK.Cargar(cmbCodigoEmpleado, relacional.ListarCatalogo<EmpleadosEntidad>(), x => x.CodigoEmpleado, x => x.NombreEmpleado, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
