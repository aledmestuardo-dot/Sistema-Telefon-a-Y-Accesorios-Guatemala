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
    public partial class FrmInventario : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<InventarioEntidad> inventario = new BindingList<InventarioEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;
        private Panel pnlDashboardInventario;
        private TableLayoutPanel tablaResumenInventario;
        private TableLayoutPanel tablaAnaliticaInventario;
        private Label[] valoresResumenInventario;
        private Label[] detallesResumenInventario;
        private FlowLayoutPanel listaStockPorSucursal;
        private FlowLayoutPanel listaReposicion;

        public FrmInventario()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            CargarCatalogosFK();
            Activated += (s, e) => CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = inventario;
            dgvInventario.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvInventario);
            LectorCodigoBarras.Conectar(cmbCodigoEquipo, relacional, equipo => cmbCodigoSucursal.Focus());
            ConstruirDashboardInventario();
            TemaVisualCrud.Aplicar(this);

            ConectarEventos();
            btnNuevo.Click += (s, e) =>
            {
                pnlFormulario.Visible = true;
                cmbCodigoEquipo.Focus();
            };
            btnEditar.Click += (s, e) =>
            {
                if (ObtenerSeleccionado() != null) pnlFormulario.Visible = true;
            };
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
        }

        private void ConstruirDashboardInventario()
        {
            pnlDashboardInventario = new Panel
            {
                Name = "pnlDashboardInventario",
                Dock = DockStyle.Top,
                Height = 278,
                Padding = new Padding(0, 8, 0, 10),
                BackColor = Color.Transparent
            };

            tablaResumenInventario = new TableLayoutPanel
            {
                Name = "tablaResumenInventario",
                Dock = DockStyle.Top,
                Height = 94,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = Padding.Empty
            };
            valoresResumenInventario = new Label[4];
            detallesResumenInventario = new Label[4];
            string[] titulos = { "UNIDADES EN STOCK", "VALOR DEL INVENTARIO", "EXISTENCIAS BAJO MÍNIMO", "ÚLTIMA ACTUALIZACIÓN" };
            for (int i = 0; i < titulos.Length; i++)
            {
                tablaResumenInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                Panel tarjeta = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12, 7, 10, 5), Margin = new Padding(0, 0, 8, 0) };
                Label titulo = new Label { Text = titulos[i], Dock = DockStyle.Top, Height = 23, Font = new Font("Segoe UI", 8F, FontStyle.Bold), AutoEllipsis = true };
                valoresResumenInventario[i] = new Label { Name = "lblInventarioResumen" + i, Text = "—", Dock = DockStyle.Top, Height = 34, Font = new Font("Segoe UI", 17F, FontStyle.Bold), AutoEllipsis = true };
                detallesResumenInventario[i] = new Label { Name = "lblInventarioDetalle" + i, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 8F), AutoEllipsis = true };
                tarjeta.Controls.Add(detallesResumenInventario[i]);
                tarjeta.Controls.Add(valoresResumenInventario[i]);
                tarjeta.Controls.Add(titulo);
                tablaResumenInventario.Controls.Add(tarjeta, i, 0);
            }

            tablaAnaliticaInventario = new TableLayoutPanel
            {
                Name = "tablaAnaliticaInventario",
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = new Padding(0, 10, 0, 0)
            };
            tablaAnaliticaInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tablaAnaliticaInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tablaAnaliticaInventario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Panel panelSucursales = CrearPanelAnalitico("Stock por sucursal", out listaStockPorSucursal);
            Panel panelReposicion = CrearPanelAnalitico("Necesitan reposición", out listaReposicion);
            panelSucursales.Margin = new Padding(0, 0, 6, 0);
            panelReposicion.Margin = new Padding(6, 0, 0, 0);
            tablaAnaliticaInventario.Controls.Add(panelSucursales, 0, 0);
            tablaAnaliticaInventario.Controls.Add(panelReposicion, 1, 0);
            pnlDashboardInventario.Controls.Add(tablaAnaliticaInventario);
            pnlDashboardInventario.Controls.Add(tablaResumenInventario);
            listaStockPorSucursal.SizeChanged += (s, e) => AjustarAnchoFilasInventario(listaStockPorSucursal);
            listaReposicion.SizeChanged += (s, e) => AjustarAnchoFilasInventario(listaReposicion);

            pnlContenido.SuspendLayout();
            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(pnlCatalogo);
            pnlContenido.Controls.Add(pnlDashboardInventario);
            pnlContenido.Controls.Add(pnlFormulario);
            pnlContenido.Controls.Add(pnlAcciones);
            pnlContenido.Controls.SetChildIndex(pnlCatalogo, 0);
            pnlContenido.Controls.SetChildIndex(pnlDashboardInventario, 1);
            pnlContenido.Controls.SetChildIndex(pnlFormulario, 2);
            pnlContenido.Controls.SetChildIndex(pnlAcciones, 3);
            pnlFormulario.Visible = false;
            pnlContenido.ResumeLayout(true);
            pnlContenido.SizeChanged += (s, e) => AjustarDashboardInventario();
            AjustarDashboardInventario();
        }

        private Panel CrearPanelAnalitico(string titulo, out FlowLayoutPanel contenido)
        {
            Panel panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10, 6, 10, 8), Margin = new Padding(4) };
            Label encabezado = new Label { Text = titulo, Dock = DockStyle.Top, Height = 28, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            contenido = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(2)
            };
            panel.Controls.Add(contenido);
            panel.Controls.Add(encabezado);
            return panel;
        }

        private void AjustarDashboardInventario()
        {
            if (pnlDashboardInventario == null || pnlContenido.ClientSize.Width <= 0) return;
            int ancho = Math.Max(360, pnlContenido.ClientSize.Width - pnlContenido.Padding.Horizontal - 8);
            pnlDashboardInventario.Width = ancho;
            tablaResumenInventario.Width = ancho;
            tablaAnaliticaInventario.Width = ancho;
            bool compacto = ancho < 800;
            tablaResumenInventario.ColumnCount = compacto ? 2 : 4;
            tablaResumenInventario.RowCount = compacto ? 2 : 1;
            tablaResumenInventario.Height = compacto ? 168 : 94;
            tablaResumenInventario.ColumnStyles.Clear();
            for (int i = 0; i < tablaResumenInventario.ColumnCount; i++)
                tablaResumenInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / tablaResumenInventario.ColumnCount));
            tablaResumenInventario.RowStyles.Clear();
            for (int i = 0; i < tablaResumenInventario.RowCount; i++)
                tablaResumenInventario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / tablaResumenInventario.RowCount));
            for (int i = 0; i < valoresResumenInventario.Length; i++)
                tablaResumenInventario.SetCellPosition(valoresResumenInventario[i].Parent,
                    new TableLayoutPanelCellPosition(i % tablaResumenInventario.ColumnCount, i / tablaResumenInventario.ColumnCount));
            pnlDashboardInventario.Height = (compacto ? 352 : 278);
            AjustarAnchoFilasInventario(listaStockPorSucursal);
            AjustarAnchoFilasInventario(listaReposicion);
        }

        private static void AjustarAnchoFilasInventario(FlowLayoutPanel lista)
        {
            if (lista == null || lista.ClientSize.Width <= 0) return;
            int ancho = Math.Max(160, lista.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 6);
            foreach (Control fila in lista.Controls) fila.Width = ancho;
        }

        private Control CrearFilaSucursal(int codigoSucursal, int total, int maximo)
        {
            TableLayoutPanel fila = new TableLayoutPanel { ColumnCount = 3, RowCount = 1, Height = 32, Margin = new Padding(0, 1, 0, 1), Padding = Padding.Empty };
            fila.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            fila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fila.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            fila.Controls.Add(new Label { Text = "Sucursal " + codigoSucursal, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true }, 0, 0);
            Label barra = new Label { Dock = DockStyle.Fill, Height = 8, Margin = new Padding(4, 12, 4, 12), BackColor = Color.FromArgb(58, 65, 72) };
            Label relleno = new Label { Dock = DockStyle.Left, BackColor = Color.FromArgb(0, 203, 255), Width = maximo <= 0 ? 0 : Math.Max(2, (int)((fila.Width - 160) * Math.Min(1F, total / (float)maximo))) };
            barra.Controls.Add(relleno);
            barra.SizeChanged += (s, e) => relleno.Width = maximo <= 0 ? 0 : Math.Max(2, (int)(barra.ClientSize.Width * Math.Min(1F, total / (float)maximo)));
            fila.Controls.Add(barra, 1, 0);
            fila.Controls.Add(new Label { Text = total.ToString("N0") + " u.", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Font = new Font("Segoe UI", 8F, FontStyle.Bold) }, 2, 0);
            return fila;
        }

        private Control CrearFilaReposicion(InventarioEntidad registro)
        {
            TableLayoutPanel fila = new TableLayoutPanel { ColumnCount = 2, RowCount = 1, Height = 42, Margin = new Padding(0, 1, 0, 3), Padding = Padding.Empty };
            fila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fila.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 82F));
            fila.Controls.Add(new Label
            {
                Text = "Equipo " + registro.CodigoEquipo + " · Sucursal " + registro.CodigoSucursal + " · mínimo " + registro.StockMinimo,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true,
                Font = new Font("Segoe UI", 8F)
            }, 0, 0);
            fila.Controls.Add(new Label
            {
                Text = registro.Stock + " unidades",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = Color.FromArgb(190, 88, 0),
                ForeColor = Color.White
            }, 1, 0);
            return fila;
        }

        private void ConstruirNavegacionLateral()
        {
            AjustesDisenoCrud.ConstruirBarraLateralModelo(this);
        }
        private void ConectarEventos()
        {
            txtObservaciones.ReadOnly = true;
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                MostrarInventario(inventario.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarInventario();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarInventario();
                    e.SuppressKeyPress = true;
                }
            };

            dgvInventario.SelectionChanged += DgvInventario_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoEquipo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoSucursal.CheckedChanged += (s, e) => ActualizarColumnas();
            nudStock.ValueChanged += (s, e) =>
            {
                if (!cargando) ActualizarObservacionesStock();
            };
            nudStockMinimo.ValueChanged += (s, e) =>
            {
                if (!cargando) ActualizarObservacionesStock();
            };
            chkStock.CheckedChanged += (s, e) => ActualizarColumnas();
            chkStockMinimo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkPrecioUnitario.CheckedChanged += (s, e) => ActualizarColumnas();
            chkObservaciones.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaActualizacion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Label opcion = sender as Label;
            if (opcion != null && opcion.Tag != null)
                Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoInventario", chkCodigo.Checked);
            CambiarVisibilidad("CodigoEquipo", chkCodigoEquipo.Checked);
            CambiarVisibilidad("CodigoSucursal", chkCodigoSucursal.Checked);
            CambiarVisibilidad("Stock", chkStock.Checked);
            CambiarVisibilidad("StockMinimo", chkStockMinimo.Checked);
            CambiarVisibilidad("PrecioUnitario", chkPrecioUnitario.Checked);
            CambiarVisibilidad("Observaciones", chkObservaciones.Checked);
            CambiarVisibilidad("FechaActualizacion", chkFechaActualizacion.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<InventarioEntidad> registros = relacional.Listar<InventarioEntidad>();
                inventario.Clear();
                foreach (InventarioEntidad registro in registros) inventario.Add(registro);
                MostrarInventario(inventario.ToList());
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
            if (dgvInventario.Columns.Contains(columna))
                dgvInventario.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvInventario.Rows.Count > 0)
                {
                    int indice = dgvInventario.CurrentRow == null ? 0 : dgvInventario.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvInventario.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvInventario.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvInventario.Rows.Count)
                return;

            dgvInventario.ClearSelection();

            DataGridViewCell celda = dgvInventario.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvInventario.CurrentCell = celda;

            dgvInventario.Rows[indice].Selected = true;
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
            InventarioEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoInventario, CrearEntidad());
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
            InventarioEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoInventario + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<InventarioEntidad>(seleccionado.CodigoInventario);
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

        private InventarioEntidad CrearEntidad()
        {
            return new InventarioEntidad(
                ComboFK.Obtener(cmbCodigoEquipo).Value,
                ComboFK.Obtener(cmbCodigoSucursal).Value,
                decimal.ToInt32(nudStock.Value),
                decimal.ToInt32(nudStockMinimo.Value),
                nudPrecioUnitario.Value,
                ValorONulo(txtObservaciones.Text),
                dtpFechaActualizacion.Value,
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoEquipo).HasValue)
            {
                MessageBox.Show("El campo Cód. equipo debe ser mayor que cero.");
                cmbCodigoEquipo.Focus();
                return false;
            }

            if (!ComboFK.Obtener(cmbCodigoSucursal).HasValue)
            {
                MessageBox.Show("El campo Cód. sucursal debe ser mayor que cero.");
                cmbCodigoSucursal.Focus();
                return false;
            }

            if (nudPrecioUnitario.Value <= 0)
            {
                MessageBox.Show("El campo Precio unitario debe ser mayor que cero.");
                nudPrecioUnitario.Focus();
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

        private InventarioEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvInventario.CurrentRow == null)
                return null;

            return dgvInventario.CurrentRow.DataBoundItem as InventarioEntidad;
        }

        private void DgvInventario_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            InventarioEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoInventario.ToString();
            ComboFK.Seleccionar(cmbCodigoEquipo, registro.CodigoEquipo);
            ComboFK.Seleccionar(cmbCodigoSucursal, registro.CodigoSucursal);
            nudStock.Value = Limitar(nudStock, registro.Stock);
            nudStockMinimo.Value = Limitar(nudStockMinimo, registro.StockMinimo);
            nudPrecioUnitario.Value = Limitar(nudPrecioUnitario, registro.PrecioUnitario);
            txtObservaciones.Text = registro.Observaciones;
            dtpFechaActualizacion.Value = LimitarFecha(dtpFechaActualizacion, registro.FechaActualizacion);
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
            ActualizarObservacionesStock();
        }

        private void ActualizarObservacionesStock()
        {
            txtObservaciones.Text = nudStock.Value <= nudStockMinimo.Value
                ? "Stock bajo, reponer"
                : "Stock suficiente";
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

        private void BuscarInventario()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarInventario(inventario.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<InventarioEntidad> resultado = relacional.Buscar<InventarioEntidad>(criterio);
                MostrarInventario(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + inventario.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarInventario(List<InventarioEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvInventario.DataSource = origen;
            ActualizarColumnas();
            dgvInventario.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoEquipo, null);
            ComboFK.Seleccionar(cmbCodigoSucursal, null);
            nudStock.Value = 0;
            nudStockMinimo.Value = 0;
            nudPrecioUnitario.Value = 0;
            txtObservaciones.Clear();
            dtpFechaActualizacion.Value = DateTime.Now;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvInventario.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            if (pnlFormulario != null) pnlFormulario.Visible = false;
            cmbCodigoEquipo.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de registros de inventario registrados: " + inventario.Count;
            ActualizarDashboardInventario();
        }

        private void ActualizarDashboardInventario()
        {
            if (valoresResumenInventario == null || listaStockPorSucursal == null || listaReposicion == null) return;

            List<InventarioEntidad> activos = inventario.Where(i => i.Estado).ToList();
            int inactivos = inventario.Count - activos.Count;
            int unidades = activos.Sum(i => Math.Max(0, i.Stock));
            decimal valor = activos.Sum(i => i.PrecioUnitario * Math.Max(0, i.Stock));
            List<InventarioEntidad> bajos = activos
                .Where(i => i.Stock <= i.StockMinimo)
                .OrderBy(i => i.Stock)
                .ToList();
            DateTime? ultimaActualizacion = activos
                .Where(i => i.FechaActualizacion != DateTime.MinValue)
                .Select(i => (DateTime?)i.FechaActualizacion)
                .Max();

            valoresResumenInventario[0].Text = unidades.ToString("N0");
            detallesResumenInventario[0].Text = "en " + activos.Select(i => i.CodigoSucursal).Distinct().Count() + " sucursales activas";
            valoresResumenInventario[1].Text = valor.ToString("C2");
            detallesResumenInventario[1].Text = "costo unitario × existencias";
            valoresResumenInventario[2].Text = bajos.Count.ToString("N0");
            detallesResumenInventario[2].Text = bajos.Count == 0 ? "sin reposición pendiente" : "requieren reposición";
            valoresResumenInventario[3].Text = ultimaActualizacion.HasValue ? ultimaActualizacion.Value.ToString("d/M/yyyy") : "—";
            detallesResumenInventario[3].Text = activos.Count + " activos · " + inactivos + " inactivos";

            listaStockPorSucursal.SuspendLayout();
            LimpiarFilasAnaliticas(listaStockPorSucursal);
            int maximo = activos.GroupBy(i => i.CodigoSucursal)
                .Select(g => g.Sum(i => Math.Max(0, i.Stock)))
                .DefaultIfEmpty(1)
                .Max();
            foreach (IGrouping<int, InventarioEntidad> grupo in activos.GroupBy(i => i.CodigoSucursal).OrderBy(g => g.Key))
            {
                int totalSucursal = grupo.Sum(i => Math.Max(0, i.Stock));
                listaStockPorSucursal.Controls.Add(CrearFilaSucursal(grupo.Key, totalSucursal, maximo));
            }
            if (activos.Count == 0)
                listaStockPorSucursal.Controls.Add(new Label { Text = "No hay existencias activas para resumir.", AutoSize = true, Padding = new Padding(4, 8, 0, 0) });
            listaStockPorSucursal.ResumeLayout(true);

            listaReposicion.SuspendLayout();
            LimpiarFilasAnaliticas(listaReposicion);
            foreach (InventarioEntidad registro in bajos)
                listaReposicion.Controls.Add(CrearFilaReposicion(registro));
            if (bajos.Count == 0)
                listaReposicion.Controls.Add(new Label { Text = "Todo el inventario tiene stock suficiente.", AutoSize = true, Padding = new Padding(4, 8, 0, 0) });
            listaReposicion.ResumeLayout(true);
            AjustarAnchoFilasInventario(listaStockPorSucursal);
            AjustarAnchoFilasInventario(listaReposicion);
        }

        private static void LimpiarFilasAnaliticas(FlowLayoutPanel lista)
        {
            foreach (Control anterior in lista.Controls.Cast<Control>().ToList())
            {
                lista.Controls.Remove(anterior);
                anterior.Dispose();
            }
        }
        private void CargarCatalogosFK()
        {
            try
            {
                int? equipoSeleccionado = ComboFK.Obtener(cmbCodigoEquipo);
                int? sucursalSeleccionada = ComboFK.Obtener(cmbCodigoSucursal);
                ComboFK.Cargar(cmbCodigoEquipo, relacional.ListarCatalogo<EquiposEntidad>(), x => x.CodigoEquipo, x => "Equipo " + x.Modelo + " | serie " + x.NumeroSerie, false);
                ComboFK.Seleccionar(cmbCodigoEquipo, equipoSeleccionado);
                ComboFK.Cargar(cmbCodigoSucursal, relacional.ListarCatalogo<SucursalesEntidad>(), x => x.CodigoSucursal, x => x.Nombre, false);
                ComboFK.Seleccionar(cmbCodigoSucursal, sucursalSeleccionada);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
