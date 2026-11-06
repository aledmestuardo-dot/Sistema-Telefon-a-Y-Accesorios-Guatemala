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
    public partial class FrmCompras : Form
    {
        // Mantiene en memoria el resultado completo para buscar y conservar los totales.
        private readonly BindingList<ComprasEntidad> compras = new BindingList<ComprasEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly ComprasNegocio negocio = new ComprasNegocio();
        private readonly ProveedoresNegocio proveedoresNegocio = new ProveedoresNegocio();
        private List<ProveedoresEntidad> proveedores = new List<ProveedoresEntidad>();
        private Panel pnlDashboard;
        private Label lblDashboardTotal, lblDashboardMonto, lblDashboardActivas;
        private Button btnAgregarNuevaCompra, btnAbrirCompraSeleccionada;
        private DashboardFacturacionVisual dashboardFacturacionCompras;
        private bool cargando = false;
        private const int RegistrosPorPagina = 10;
        private readonly List<ComprasEntidad> resultadosCompras = new List<ComprasEntidad>();
        private int paginaCompras = 1;
        private Panel pnlPaginacionCompras;
        private Label lblPaginaCompras;
        private Button btnPaginaAnteriorCompras, btnPaginaSiguienteCompras;
        private TableLayoutPanel layoutBloquesCompras;

        public FrmCompras()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            ConfigurarDisenoCompras();
            ConstruirNavegacionLateral();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirDashboardCompras();
            ConstruirLayoutCompras();

            origen.DataSource = compras;
            dgvCompras.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvCompras);

            CargarProveedores();
            ConectarEventos();
            ConfigurarPaginacionCompras();
            AjustarInterfazCompras();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarCompras();
        }

        private void ConfigurarDisenoCompras()
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
            dgvCompras.Dock = DockStyle.Top;
            dgvCompras.RowTemplate.Height = 29;
            dgvCompras.ColumnHeadersHeight = 34;
            dgvCompras.ScrollBars = ScrollBars.Horizontal;
        }

        private void ConstruirLayoutCompras()
        {
            layoutBloquesCompras = new TableLayoutPanel
            {
                Name = "layoutBloquesCompras", Dock = DockStyle.Top, ColumnCount = 1,
                RowCount = 3, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesCompras.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesCompras.RowStyles.Add(new RowStyle(SizeType.Absolute, 360F));
            layoutBloquesCompras.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutBloquesCompras.RowStyles.Add(new RowStyle(SizeType.Absolute, 400F));
            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Remove(pnlDashboard);
            pnlContenido.Controls.Add(layoutBloquesCompras);
            pnlDashboard.Dock = DockStyle.Fill;
            pnlCatalogo.Dock = DockStyle.Fill;
            pnlCatalogo.Margin = Padding.Empty;
            layoutBloquesCompras.Controls.Add(pnlDashboard, 0, 0);
            Panel separador = new Panel { Dock = DockStyle.Fill, Margin = Padding.Empty, BackColor = pnlContenido.BackColor };
            layoutBloquesCompras.Controls.Add(separador, 0, 1);
            layoutBloquesCompras.Controls.Add(pnlCatalogo, 0, 2);
            pnlContenido.Resize += (s, e) => AjustarInterfazCompras();
            AjustarInterfazCompras();
        }

        private void ConfigurarPaginacionCompras()
        {
            pnlPaginacionCompras = new Panel { Name = "pnlPaginacionCompras", Dock = DockStyle.Bottom, Height = 58, BackColor = ColorTranslator.FromHtml("#08243A") };
            QuitarPaginacionComunCompras();
            if (lblRegistros.Parent != null) lblRegistros.Parent.Controls.Remove(lblRegistros);
            lblRegistros.Dock = DockStyle.None;
            lblPaginaCompras = new Label { Name = "lblPaginaCompras", ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter };
            btnPaginaAnteriorCompras = CrearBotonPaginaCompras("‹ Anterior", "btnPaginaAnteriorCompras");
            btnPaginaSiguienteCompras = CrearBotonPaginaCompras("Siguiente ›", "btnPaginaSiguienteCompras");
            btnPaginaAnteriorCompras.Click += (s, e) => CambiarPaginaCompras(paginaCompras - 1);
            btnPaginaSiguienteCompras.Click += (s, e) => CambiarPaginaCompras(paginaCompras + 1);
            pnlPaginacionCompras.Controls.Add(lblRegistros);
            pnlPaginacionCompras.Controls.Add(lblPaginaCompras);
            pnlPaginacionCompras.Controls.Add(btnPaginaAnteriorCompras);
            pnlPaginacionCompras.Controls.Add(btnPaginaSiguienteCompras);
            pnlCatalogo.Controls.Add(pnlPaginacionCompras);
            pnlPaginacionCompras.BringToFront();
            pnlPaginacionCompras.Resize += (s, e) => AcomodarPaginacionCompras();
            AjustarInterfazCompras();
        }

        private void QuitarPaginacionComunCompras()
        {
            foreach (Control control in Controls.Find("pnlPaginacion", true))
                if (control.Parent != null) control.Parent.Controls.Remove(control);
        }

        private static Button CrearBotonPaginaCompras(string texto, string nombre)
        {
            return new Button { Name = nombre, Text = texto, Size = new Size(132, 42), FlatStyle = FlatStyle.Flat,
                BackColor = ColorTranslator.FromHtml("#0B6E91"), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand };
        }

        private void AcomodarPaginacionCompras()
        {
            if (pnlPaginacionCompras == null || pnlPaginacionCompras.ClientSize.Width <= 0) return;
            const int separacion = 8;
            int y = Math.Max(0, (pnlPaginacionCompras.ClientSize.Height - 42) / 2);
            int derecha = pnlPaginacionCompras.ClientSize.Width - 8;
            btnPaginaSiguienteCompras.SetBounds(derecha - 132, y, 132, 42);
            lblPaginaCompras.SetBounds(btnPaginaSiguienteCompras.Left - separacion - 132, y, 132, 42);
            btnPaginaAnteriorCompras.SetBounds(lblPaginaCompras.Left - separacion - 132, y, 132, 42);
            lblRegistros.SetBounds(8, y, Math.Max(120, btnPaginaAnteriorCompras.Left - 16), 42);
        }

        private void AjustarInterfazCompras()
        {
            if (IsDisposed || pnlDashboard == null || dgvCompras == null || layoutBloquesCompras == null) return;
            int ancho = Math.Max(0, pnlContenido.ClientSize.Width - 30);
            int altoDashboard = Math.Max(380, Math.Min(430, pnlContenido.ClientSize.Height - 390));
            int altoFila = Math.Max(26, dgvCompras.RowTemplate.Height);
            dgvCompras.Height = dgvCompras.ColumnHeadersHeight + RegistrosPorPagina * altoFila + 2;
            if (pnlPaginacionCompras != null) pnlPaginacionCompras.Height = 58;
            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height +
                pnlColumnas.Height + dgvCompras.Height + (pnlPaginacionCompras == null ? 58 : pnlPaginacionCompras.Height);
            layoutBloquesCompras.RowStyles[0].Height = altoDashboard;
            layoutBloquesCompras.RowStyles[2].Height = altoCatalogo;
            layoutBloquesCompras.Height = altoDashboard + 10 + altoCatalogo;
            AcomodarPaginacionCompras();
        }

        private void EstablecerResultadosCompras(IEnumerable<ComprasEntidad> lista)
        {
            resultadosCompras.Clear();
            resultadosCompras.AddRange(lista);
            paginaCompras = 1;
            MostrarPaginaCompras();
        }

        private void CambiarPaginaCompras(int nuevaPagina)
        {
            int totalPaginas = (int)Math.Ceiling(resultadosCompras.Count / (double)RegistrosPorPagina);
            if (nuevaPagina < 1 || nuevaPagina > totalPaginas) return;
            paginaCompras = nuevaPagina;
            MostrarPaginaCompras();
        }

        private void MostrarPaginaCompras()
        {
            int totalPaginas = (int)Math.Ceiling(resultadosCompras.Count / (double)RegistrosPorPagina);
            if (totalPaginas == 0) paginaCompras = 1;
            else if (paginaCompras > totalPaginas) paginaCompras = totalPaginas;
            List<ComprasEntidad> pagina = resultadosCompras.Skip((paginaCompras - 1) * RegistrosPorPagina).Take(RegistrosPorPagina).ToList();
            origen.DataSource = null;
            origen.DataSource = pagina;
            dgvCompras.DataSource = origen;
            ActualizarColumnas();
            if (dgvCompras.Columns.Contains("NumeroFila")) dgvCompras.Columns.Remove("NumeroFila");
            dgvCompras.Columns.Insert(0, new DataGridViewTextBoxColumn { Name = "NumeroFila", HeaderText = "N.º", ReadOnly = true, Width = 52, SortMode = DataGridViewColumnSortMode.NotSortable });
            for (int i = 0; i < dgvCompras.Rows.Count; i++) dgvCompras.Rows[i].Cells["NumeroFila"].Value = i + 1;
            dgvCompras.ClearSelection();
            lblPaginaCompras.Text = "Página " + (totalPaginas == 0 ? 0 : paginaCompras) + " de " + totalPaginas;
            btnPaginaAnteriorCompras.Enabled = paginaCompras > 1;
            btnPaginaSiguienteCompras.Enabled = paginaCompras < totalPaginas;
            lblRegistros.Text = "Resultados: " + resultadosCompras.Count + " | Total: " + compras.Count;
            AcomodarPaginacionCompras();
        }

        private void ConstruirNavegacionLateral()
        {
            AjustesDisenoCrud.ConstruirBarraLateralModelo(this);
        }

        // Conserva la distribución lateral de Pagos y el scroll si no caben las pestañas.
        private void AjustarAlturaMenuLateral(List<Label> opciones)
        {
            if (opciones.Count == 0) return;

            int altoFila = Math.Max(34, pnlItemsMenu.ClientSize.Height / opciones.Count);
            int posicionY = 0;
            foreach (Label opcion in opciones)
            {
                opcion.SetBounds(0, posicionY, 160, altoFila);
                posicionY += altoFila;
            }

            pnlItemsMenu.AutoScrollMinSize = new Size(0, posicionY);
        }

        // Centra la fila de acciones y limita su ancho en ventanas grandes.
        private void CentrarBotonesAccion()
        {
            layoutBotones.Dock = DockStyle.None;
            layoutBotones.Anchor = AnchorStyles.Top;

            Action centrar = () =>
            {
                int anchoDisponible = Math.Max(0, pnlAcciones.ClientSize.Width);
                int anchoBotones = Math.Min(900, anchoDisponible);
                layoutBotones.SetBounds(
                    Math.Max(0, (anchoDisponible - anchoBotones) / 2),
                    Math.Max(0, (pnlAcciones.ClientSize.Height - layoutBotones.Height) / 2),
                    anchoBotones,
                    layoutBotones.Height);
            };

            pnlAcciones.SizeChanged += (s, e) => centrar();
            centrar();
        }
        private void ConectarEventos()
        {
            btnAgregarNuevaCompra.Click += (s, e) => AbrirCapturaCompra();
            btnAbrirCompraSeleccionada.Click += (s, e) =>
            {
                ComprasEntidad compra = ObtenerSeleccionado();
                if (compra == null)
                {
                    MessageBox.Show("Activa Seleccionar y marca una compra en la grilla.", "Seleccionar compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                AbrirCapturaCompra(compra);
            };
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                EstablecerResultadosCompras(compras);
                LimpiarFormulario();
                ActualizarContador();
                ActualizarDashboard();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarCompras();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarCompras();
                    e.SuppressKeyPress = true;
                }
            };

            dgvCompras.SelectionChanged += DgvCompras_SelectionChanged;
            dgvCompras.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                ComprasEntidad compra = dgvCompras.Rows[e.RowIndex].DataBoundItem as ComprasEntidad;
                if (compra != null) AbrirCapturaCompra(compra);
            };
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoProveedor.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNumeroDocumento.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaCompra.CheckedChanged += (s, e) => ActualizarColumnas();
            chkSubtotal.CheckedChanged += (s, e) => ActualizarColumnas();
            chkIva.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTotal.CheckedChanged += (s, e) => ActualizarColumnas();
            chkObservaciones.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void CargarProveedores()
        {
            try
            {
                proveedores = proveedoresNegocio.Listar().Where(p => p.Estado).ToList();
                ComboFK.Cargar(cmbCodigoProveedor, proveedores, p => p.CodigoProveedor, p => p.Nombre);
            }
            catch (Exception ex)
            {
                proveedores = new List<ProveedoresEntidad>();
                ComboFK.Cargar(cmbCodigoProveedor, proveedores, p => p.CodigoProveedor, p => p.Nombre);
                MessageBox.Show(MensajesError.Resumir(ex), "Error al cargar proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SeleccionarProveedor(int codigoProveedor)
        {
            if (!proveedores.Any(p => p.CodigoProveedor == codigoProveedor))
            {
                // Permite consultar/editar compras históricas aunque el proveedor esté inactivo.
                ProveedoresEntidad proveedor = proveedoresNegocio.Listar().FirstOrDefault(p => p.CodigoProveedor == codigoProveedor);
                if (proveedor != null)
                {
                    proveedores.Add(proveedor);
                    ComboFK.Cargar(cmbCodigoProveedor, proveedores, p => p.CodigoProveedor, p => p.Nombre);
                }
            }
            ComboFK.Seleccionar(cmbCodigoProveedor, codigoProveedor);
        }

        private int CodigoProveedorSeleccionado()
        {
            return ComboFK.Obtener(cmbCodigoProveedor) ?? 0;
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Label opcion = sender as Label;
            if (opcion != null && opcion.Tag != null)
                Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoCompra", chkCodigo.Checked);
            CambiarVisibilidad("CodigoProveedor", chkCodigoProveedor.Checked);
            CambiarVisibilidad("NumeroDocumento", chkNumeroDocumento.Checked);
            CambiarVisibilidad("FechaCompra", chkFechaCompra.Checked);
            CambiarVisibilidad("Subtotal", chkSubtotal.Checked);
            CambiarVisibilidad("Iva", chkIva.Checked);
            CambiarVisibilidad("Total", chkTotal.Checked);
            CambiarVisibilidad("Observaciones", chkObservaciones.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        // Presentación solicita la lista completa a Negocio; el paginador solo afecta la grilla.
        private void ConsultarCompras()
        {
            try
            {
                List<ComprasEntidad> resultado = negocio.Listar();
                compras.Clear();
                foreach (ComprasEntidad compra in resultado)
                    compras.Add(compra);

                // Cambiar el origen notifica al paginador y conserva todas las filas para los totales.
                EstablecerResultadosCompras(compras);
                ActualizarContador();
                ActualizarDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    MensajesError.Resumir(ex),
                    "Error al consultar compras",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ActualizarContador();
            }
        }

        private void ConstruirDashboardCompras()
        {
            pnlFormulario.Visible = false;
            pnlAcciones.Visible = false;
            pnlCatalogo.Dock = DockStyle.None;
            pnlCatalogo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            pnlDashboard = new Panel { Name = "pnlDashboardCompras", BackColor = Color.FromArgb(7, 35, 66), Padding = new Padding(14) };
            Label titulo = new Label { Text = "Resumen de compras", ForeColor = Color.FromArgb(0, 190, 230), Font = new Font("Segoe UI", 12F, FontStyle.Bold), Location = new Point(14, 8), Size = new Size(320, 28) };
            pnlDashboard.Controls.Add(titulo);

            lblDashboardTotal = CrearValorDashboard("Compras registradas", 14);
            lblDashboardMonto = CrearValorDashboard("Monto total", 238);
            lblDashboardActivas = CrearValorDashboard("Compras activas", 462);

            btnAgregarNuevaCompra = new Button
            {
                Name = "btnAgregarNuevaCompra", Text = "+  Agregar nueva compra", BackColor = Color.FromArgb(0, 150, 110),
                ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(240, 44), Anchor = AnchorStyles.Top | AnchorStyles.Right, Cursor = Cursors.Hand
            };
            btnAgregarNuevaCompra.FlatAppearance.BorderColor = Color.White;
            pnlDashboard.Controls.Add(btnAgregarNuevaCompra);

            btnAbrirCompraSeleccionada = new Button
            {
                Name = "btnAbrirCompraSeleccionada", Text = "Ver / editar seleccionada", BackColor = Color.FromArgb(30, 105, 165),
                ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Size = new Size(220, 44), Anchor = AnchorStyles.Top | AnchorStyles.Right, Cursor = Cursors.Hand, Enabled = false
            };
            pnlDashboard.Controls.Add(btnAbrirCompraSeleccionada);

            dashboardFacturacionCompras = new DashboardFacturacionVisual("compras",
                new Control[] { btnAgregarNuevaCompra, btnAbrirCompraSeleccionada },
                (s, e) => ConsultarCompras());
            pnlDashboard = dashboardFacturacionCompras.Panel;
            lblDashboardTotal = dashboardFacturacionCompras.Valores[0];
            lblDashboardMonto = dashboardFacturacionCompras.Valores[1];
            lblDashboardActivas = dashboardFacturacionCompras.Valores[2];
            MoverAccionReporteCompras("btnImportarExcel");
            MoverAccionReporteCompras("btnExportarExcel");
            MoverAccionReporteCompras("btnImprimir");

            pnlContenido.Controls.Add(pnlDashboard);
            lblMenuDetalleCompra.Visible = false;
            lblMenuDetalleVenta.Visible = false;
            lblMenuDetalleFactura.Visible = false;
        }

        private Label CrearValorDashboard(string titulo, int left)
        {
            Panel tarjeta = new Panel { BackColor = Color.FromArgb(10, 48, 84), Location = new Point(left, 42), Size = new Size(210, 86), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            tarjeta.Controls.Add(new Label { Text = titulo, ForeColor = Color.FromArgb(170, 200, 225), Location = new Point(12, 8), Size = new Size(190, 24) });
            Label valor = new Label { Text = "0", ForeColor = Color.White, Font = new Font("Segoe UI", 18F, FontStyle.Bold), Location = new Point(12, 32), Size = new Size(190, 42), Tag = tarjeta };
            tarjeta.Controls.Add(valor);
            pnlDashboard.Controls.Add(tarjeta);
            return valor;
        }

        private void ActualizarDashboard()
        {
            if (lblDashboardTotal == null) return;
            int activas = compras.Count(c => c.Estado);
            int inactivas = compras.Count(c => !c.Estado);
            decimal monto = compras.Sum(c => c.Total);
            lblDashboardTotal.Text = compras.Count.ToString("N0");
            lblDashboardMonto.Text = "Q " + monto.ToString("N2");
            lblDashboardActivas.Text = activas.ToString("N0");
            if (dashboardFacturacionCompras != null)
                dashboardFacturacionCompras.Actualizar(
                    new[] { "COMPRAS REGISTRADAS", "MONTO COMPRADO", "COMPRAS ACTIVAS", "COMPRAS INACTIVAS" },
                    new[] { compras.Count.ToString("N0"), "Q " + monto.ToString("N2"), activas.ToString("N0"), inactivas.ToString("N0") },
                    compras.GroupBy(c => new { c.FechaCompra.Year, c.FechaCompra.Month })
                        .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                        .Select(g => new KeyValuePair<string, double>(new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"), (double)g.Sum(c => c.Total))),
                    "Activas: " + activas + Environment.NewLine + "Inactivas: " + inactivas,
                    compras.OrderByDescending(c => c.FechaCompra).Take(4).Any()
                        ? string.Join(Environment.NewLine, compras.OrderByDescending(c => c.FechaCompra).Take(4)
                            .Select(c => "Compra " + c.CodigoCompra + " · " + c.FechaCompra.ToString("dd/MM/yyyy") + " · Q " + c.Total.ToString("N2")))
                        : "No hay compras registradas.");
        }

        private void MoverAccionReporteCompras(string nombre)
        {
            Button boton = Controls.Find(nombre, true).OfType<Button>().FirstOrDefault();
            if (boton == null || boton.Parent == null) return;
            boton.Parent.Controls.Remove(boton);
            boton.Dock = DockStyle.None;
            boton.Size = new Size(nombre == "btnImprimir" ? 132 : 158, 36);
            boton.Margin = new Padding(4, 9, 4, 9);
            boton.TextAlign = ContentAlignment.MiddleCenter;
            dashboardFacturacionCompras.Acciones.Controls.Add(boton);
        }

        private void AbrirCapturaCompra()
        {
            AbrirCapturaCompra(null);
        }

        private void AbrirCapturaCompra(ComprasEntidad compra)
        {
            using (FrmCompraCompleta formulario = new FrmCompraCompleta(compra))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                    ConsultarCompras();
            }
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvCompras.Columns.Contains(columna))
                dgvCompras.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvCompras.Rows.Count > 0)
                {
                    int indice = dgvCompras.CurrentRow == null ? 0 : dgvCompras.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvCompras.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvCompras.ClearSelection();
            }
            ActualizarBotonAbrirCompra();
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvCompras.Rows.Count)
                return;

            dgvCompras.ClearSelection();

            DataGridViewCell celda = dgvCompras.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvCompras.CurrentCell = celda;

            dgvCompras.Rows[indice].Selected = true;
            ActualizarBotonAbrirCompra();
        }

        private void ActualizarBotonAbrirCompra()
        {
            if (btnAbrirCompraSeleccionada != null)
                btnAbrirCompraSeleccionada.Enabled = chkSeleccionar.Checked && dgvCompras.CurrentRow != null &&
                    dgvCompras.CurrentRow.DataBoundItem is ComprasEntidad;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            // En SQL Server, CodigoCompra debe omitirse en INSERT porque es IDENTITY.
            try
            {
                int codigoGenerado = negocio.Agregar(CrearEntidad());
                ConsultarCompras();
                LimpiarFormulario();
                MessageBox.Show(
                    "Compra agregada correctamente. Código: " + codigoGenerado,
                    "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al agregar compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (!AjustesDisenoCrud.PermitirEdicion(this)) return;
            ComprasEntidad seleccionado = ObtenerSeleccionado();

            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }

            if (!ValidarFormulario())
                return;

            try
            {
                // CodigoCompra es la PK real y determina exactamente qué fila se actualiza.
                negocio.Editar(seleccionado.CodigoCompra, CrearEntidad());
                ConsultarCompras();
                LimpiarFormulario();
                MessageBox.Show("Compra actualizada correctamente.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al editar compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            ComprasEntidad seleccionado = ObtenerSeleccionado();

            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoCompra + " de Compras?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                // Se inactivan la compra y sus detalles en una transacción.
                negocio.Eliminar(seleccionado.CodigoCompra);
                ConsultarCompras();
                LimpiarFormulario();
                MessageBox.Show("La compra y sus detalles quedaron inactivos.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ComprasEntidad CrearEntidad()
        {
            return new ComprasEntidad(
                CodigoProveedorSeleccionado(),
                txtNumeroDocumento.Text.Trim(),
                dtpFechaCompra.Value,
                nudSubtotal.Value,
                nudIva.Value,
                nudTotal.Value,
                ValorONulo(txtObservaciones.Text),
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (CodigoProveedorSeleccionado() <= 0)
            {
                MessageBox.Show("Selecciona un proveedor.");
                cmbCodigoProveedor.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroDocumento.Text))
            {
                MessageBox.Show("El campo Número de documento es obligatorio.");
                txtNumeroDocumento.Focus();
                return false;
            }

            if (txtNumeroDocumento.Text.Trim().Length > 50)
            {
                MessageBox.Show("El campo Número de documento no puede superar 50 caracteres.");
                txtNumeroDocumento.Focus();
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

        private ComprasEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvCompras.CurrentRow == null)
                return null;

            return dgvCompras.CurrentRow.DataBoundItem as ComprasEntidad;
        }

        private void DgvCompras_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotonAbrirCompra();
            if (cargando || !chkSeleccionar.Checked)
                return;

            ComprasEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoCompra.ToString();
            SeleccionarProveedor(registro.CodigoProveedor);
            txtNumeroDocumento.Text = registro.NumeroDocumento;
            dtpFechaCompra.Value = LimitarFecha(dtpFechaCompra, registro.FechaCompra);
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

        private void BuscarCompras()
        {
            string criterio = txtBuscar.Text.Trim();

            if (criterio.Length == 0)
            {
                EstablecerResultadosCompras(compras);
                ActualizarContador();
                return;
            }

            try
            {
                // La búsqueda llega a SQL mediante Negocio; la paginación sigue limitada a la grilla.
                List<ComprasEntidad> resultado = negocio.Buscar(criterio);
                EstablecerResultadosCompras(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarCompras(List<ComprasEntidad> lista)
        {
            EstablecerResultadosCompras(lista);
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            cmbCodigoProveedor.SelectedIndex = -1;
            txtNumeroDocumento.Clear();
            dtpFechaCompra.Value = DateTime.Now;
            nudSubtotal.Value = 0;
            nudIva.Value = 0;
            nudTotal.Value = 0;
            txtObservaciones.Clear();
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvCompras.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoProveedor.Focus();
        }

        private void ActualizarContador()
        {
            if (lblRegistros != null && pnlPaginacionCompras == null)
                lblRegistros.Text = "Total de compras registradas: " + compras.Count;
            ActualizarDashboard();
        }
    }
}
