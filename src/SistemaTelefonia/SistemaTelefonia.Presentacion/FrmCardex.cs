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
    public partial class FrmCardex : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<CardexEntidad> cardex = new BindingList<CardexEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;

        public FrmCardex()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            CargarCatalogosFK();
            bool omitirPrimeraActivacion = true;
            Activated += (s, e) =>
            {
                if (omitirPrimeraActivacion)
                {
                    omitirPrimeraActivacion = false;
                    return;
                }

                CargarCatalogosFK();
            };
            ConstruirNavegacionLateral();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);

            origen.DataSource = cardex;
            dgvCardex.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvCardex);

            ConectarEventos();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
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
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                MostrarCardex(cardex.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarCardex();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarCardex();
                    e.SuppressKeyPress = true;
                }
            };

            dgvCardex.SelectionChanged += DgvCardex_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoInventario.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoCompra.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoVenta.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaActualizacion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTipoMovimiento.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCantidad.CheckedChanged += (s, e) => ActualizarColumnas();
            chkStockResultante.CheckedChanged += (s, e) => ActualizarColumnas();
            chkObservaciones.CheckedChanged += (s, e) => ActualizarColumnas();
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
            CambiarVisibilidad("CodigoCardex", chkCodigo.Checked);
            CambiarVisibilidad("CodigoInventario", chkCodigoInventario.Checked);
            CambiarVisibilidad("CodigoCompra", chkCodigoCompra.Checked);
            CambiarVisibilidad("CodigoVenta", chkCodigoVenta.Checked);
            CambiarVisibilidad("FechaActualizacion", chkFechaActualizacion.Checked);
            CambiarVisibilidad("TipoMovimiento", chkTipoMovimiento.Checked);
            CambiarVisibilidad("Cantidad", chkCantidad.Checked);
            CambiarVisibilidad("StockResultante", chkStockResultante.Checked);
            CambiarVisibilidad("Observaciones", chkObservaciones.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<CardexEntidad> registros = relacional.Listar<CardexEntidad>();
                cardex.Clear();
                foreach (CardexEntidad registro in registros) cardex.Add(registro);
                MostrarCardex(cardex.ToList());
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
            if (dgvCardex.Columns.Contains(columna))
                dgvCardex.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvCardex.Rows.Count > 0)
                {
                    int indice = dgvCardex.CurrentRow == null ? 0 : dgvCardex.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvCardex.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvCardex.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvCardex.Rows.Count)
                return;

            dgvCardex.ClearSelection();

            DataGridViewCell celda = dgvCardex.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvCardex.CurrentCell = celda;

            dgvCardex.Rows[indice].Selected = true;
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
            CardexEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoCardex, CrearEntidad());
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
            CardexEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoCardex + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<CardexEntidad>(seleccionado.CodigoCardex);
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

        private CardexEntidad CrearEntidad()
        {
            return new CardexEntidad(
                ComboFK.Obtener(cmbCodigoInventario).Value,
                ComboFK.Obtener(cmbCodigoCompra),
                ComboFK.Obtener(cmbCodigoVenta),
                dtpFechaActualizacion.Value,
                cmbTipoMovimiento.Text.Trim(),
                decimal.ToInt32(nudCantidad.Value),
                decimal.ToInt32(nudStockResultante.Value),
                ValorONulo(txtObservaciones.Text),
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoInventario).HasValue)
            {
                MessageBox.Show("El campo Cód. inventario debe ser mayor que cero.");
                cmbCodigoInventario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbTipoMovimiento.Text))
            {
                MessageBox.Show("El campo Tipo de movimiento es obligatorio.");
                cmbTipoMovimiento.Focus();
                return false;
            }

            if (cmbTipoMovimiento.Text.Trim().Length > 45)
            {
                MessageBox.Show("El campo Tipo de movimiento no puede superar 45 caracteres.");
                cmbTipoMovimiento.Focus();
                return false;
            }

            if (nudCantidad.Value <= 0)
            {
                MessageBox.Show("El campo Cantidad debe ser mayor que cero.");
                nudCantidad.Focus();
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

        private CardexEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvCardex.CurrentRow == null)
                return null;

            return dgvCardex.CurrentRow.DataBoundItem as CardexEntidad;
        }

        private void DgvCardex_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            CardexEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoCardex.ToString();
            ComboFK.Seleccionar(cmbCodigoInventario, registro.CodigoInventario);
            ComboFK.Seleccionar(cmbCodigoCompra, registro.CodigoCompra ?? 0);
            ComboFK.Seleccionar(cmbCodigoVenta, registro.CodigoVenta ?? 0);
            dtpFechaActualizacion.Value = LimitarFecha(dtpFechaActualizacion, registro.FechaActualizacion);
            cmbTipoMovimiento.SelectedIndex = cmbTipoMovimiento.FindStringExact(registro.TipoMovimiento ?? string.Empty);
            nudCantidad.Value = Limitar(nudCantidad, registro.Cantidad);
            nudStockResultante.Value = Limitar(nudStockResultante, registro.StockResultante);
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

        private void BuscarCardex()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarCardex(cardex.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<CardexEntidad> resultado = relacional.Buscar<CardexEntidad>(criterio);
                MostrarCardex(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + cardex.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarCardex(List<CardexEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvCardex.DataSource = origen;
            ActualizarColumnas();
            dgvCardex.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoInventario, null);
            ComboFK.Seleccionar(cmbCodigoCompra, null);
            ComboFK.Seleccionar(cmbCodigoVenta, null);
            dtpFechaActualizacion.Value = DateTime.Now;
            cmbTipoMovimiento.SelectedIndex = -1;
            nudCantidad.Value = 0;
            nudStockResultante.Value = 0;
            txtObservaciones.Clear();
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvCardex.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoInventario.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de movimientos registrados: " + cardex.Count;
        }
        private void CargarCatalogosFK()
        {
            try
            {
                int? inventarioSeleccionado = ComboFK.Obtener(cmbCodigoInventario);
                ComboFK.Cargar(cmbCodigoInventario, relacional.ListarCatalogo<InventarioEntidad>(), x => x.CodigoInventario, x => "Inventario | equipo " + x.CodigoEquipo + " | sucursal " + x.CodigoSucursal, false);
                ComboFK.Seleccionar(cmbCodigoInventario, inventarioSeleccionado);
                ComboFK.Cargar(cmbCodigoCompra, relacional.ListarCatalogo<ComprasEntidad>(), x => x.CodigoCompra, x => "Compra | " + x.NumeroDocumento, true);
                ComboFK.Cargar(cmbCodigoVenta, relacional.ListarCatalogo<VentasEntidad>(), x => x.CodigoVenta, x => "Venta | " + x.FechaVenta.ToString("dd/MM/yyyy"), true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
