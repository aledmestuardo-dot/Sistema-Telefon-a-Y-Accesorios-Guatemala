using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmDetalleVenta : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<DetalleVentaEntidad> detalleVenta = new BindingList<DetalleVentaEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;

        public FrmDetalleVenta()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = detalleVenta;
            dgvDetalleVenta.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvDetalleVenta);

            ConectarEventos();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
        }

        private void ConstruirNavegacionLateral()
        {
            foreach (Control control in pnlItemsMenu.Controls)
            {
                Label opcion = control as Label;
                if (opcion == null || opcion.Tag == null)
                    continue;

                opcion.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                opcion.Width = 160;
                opcion.Cursor = Cursors.Hand;
                opcion.Click += MenuOpcion_Click;
            }

            AjustesDisenoCrud.DistribuirPestanas(pnlItemsMenu);
            AjustesDisenoCrud.ResaltarPestanaActiva(pnlItemsMenu, GetType().Name);

            btnVolverMenu.Click += (s, e) => Navegacion.VolverAlMenu(this);
            btnCerrarSesion.Click += (s, e) => Navegacion.CerrarSesion(this);
        }
        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                MostrarDetalleVenta(detalleVenta.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarDetalleVenta();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarDetalleVenta();
                    e.SuppressKeyPress = true;
                }
            };

            dgvDetalleVenta.SelectionChanged += DgvDetalleVenta_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;
            LectorCodigoBarras.Conectar(cmbCodigoEquipo, relacional, equipo =>
            {
                nudPrecioUnitario.Value = Limitar(nudPrecioUnitario, equipo.Precio);
                nudCantidad.Focus();
            });

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoVenta.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoEquipo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoPlan.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCantidad.CheckedChanged += (s, e) => ActualizarColumnas();
            chkPrecioUnitario.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDescuento.CheckedChanged += (s, e) => ActualizarColumnas();
            chkSubtotal.CheckedChanged += (s, e) => ActualizarColumnas();
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
            CambiarVisibilidad("CodigoDetalleVenta", chkCodigo.Checked);
            CambiarVisibilidad("CodigoVenta", chkCodigoVenta.Checked);
            CambiarVisibilidad("CodigoEquipo", chkCodigoEquipo.Checked);
            CambiarVisibilidad("CodigoPlan", chkCodigoPlan.Checked);
            CambiarVisibilidad("Cantidad", chkCantidad.Checked);
            CambiarVisibilidad("PrecioUnitario", chkPrecioUnitario.Checked);
            CambiarVisibilidad("Descuento", chkDescuento.Checked);
            CambiarVisibilidad("Subtotal", chkSubtotal.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<DetalleVentaEntidad> registros = relacional.Listar<DetalleVentaEntidad>();
                detalleVenta.Clear();
                foreach (DetalleVentaEntidad registro in registros) detalleVenta.Add(registro);
                MostrarDetalleVenta(detalleVenta.ToList());
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
            if (dgvDetalleVenta.Columns.Contains(columna))
                dgvDetalleVenta.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvDetalleVenta.Rows.Count > 0)
                {
                    int indice = dgvDetalleVenta.CurrentRow == null ? 0 : dgvDetalleVenta.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvDetalleVenta.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvDetalleVenta.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvDetalleVenta.Rows.Count)
                return;

            dgvDetalleVenta.ClearSelection();

            DataGridViewCell celda = dgvDetalleVenta.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvDetalleVenta.CurrentCell = celda;

            dgvDetalleVenta.Rows[indice].Selected = true;
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
            DetalleVentaEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoDetalleVenta, CrearEntidad());
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
            DetalleVentaEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoDetalleVenta + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<DetalleVentaEntidad>(seleccionado.CodigoDetalleVenta);
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

        private DetalleVentaEntidad CrearEntidad()
        {
            return new DetalleVentaEntidad(
                ComboFK.Obtener(cmbCodigoVenta).Value,
                ComboFK.Obtener(cmbCodigoEquipo).Value,
                ComboFK.Obtener(cmbCodigoPlan),
                decimal.ToInt32(nudCantidad.Value),
                nudPrecioUnitario.Value,
                nudDescuento.Value,
                nudSubtotal.Value,
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

            if (!ComboFK.Obtener(cmbCodigoEquipo).HasValue)
            {
                MessageBox.Show("El campo Cód. equipo debe ser mayor que cero.");
                cmbCodigoEquipo.Focus();
                return false;
            }

            if (nudCantidad.Value <= 0)
            {
                MessageBox.Show("El campo Cantidad debe ser mayor que cero.");
                nudCantidad.Focus();
                return false;
            }

            if (nudPrecioUnitario.Value <= 0)
            {
                MessageBox.Show("El campo Precio unitario debe ser mayor que cero.");
                nudPrecioUnitario.Focus();
                return false;
            }

            if (nudSubtotal.Value <= 0)
            {
                MessageBox.Show("El campo Subtotal debe ser mayor que cero.");
                nudSubtotal.Focus();
                return false;
            }

            return true;
        }

        private DetalleVentaEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvDetalleVenta.CurrentRow == null)
                return null;

            return dgvDetalleVenta.CurrentRow.DataBoundItem as DetalleVentaEntidad;
        }

        private void DgvDetalleVenta_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            DetalleVentaEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoDetalleVenta.ToString();
            ComboFK.Seleccionar(cmbCodigoVenta, registro.CodigoVenta);
            ComboFK.Seleccionar(cmbCodigoEquipo, registro.CodigoEquipo);
            ComboFK.Seleccionar(cmbCodigoPlan, registro.CodigoPlan ?? 0);
            nudCantidad.Value = Limitar(nudCantidad, registro.Cantidad);
            nudPrecioUnitario.Value = Limitar(nudPrecioUnitario, registro.PrecioUnitario);
            nudDescuento.Value = Limitar(nudDescuento, registro.Descuento);
            nudSubtotal.Value = Limitar(nudSubtotal, registro.Subtotal);
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
        }

        private decimal Limitar(NumericUpDown control, decimal valor)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, valor));
        }

        private void BuscarDetalleVenta()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarDetalleVenta(detalleVenta.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<DetalleVentaEntidad> resultado = relacional.Buscar<DetalleVentaEntidad>(criterio);
                MostrarDetalleVenta(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + detalleVenta.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalleVenta(List<DetalleVentaEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvDetalleVenta.DataSource = origen;
            ActualizarColumnas();
            dgvDetalleVenta.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoVenta, null);
            ComboFK.Seleccionar(cmbCodigoEquipo, null);
            ComboFK.Seleccionar(cmbCodigoPlan, null);
            nudCantidad.Value = 0;
            nudPrecioUnitario.Value = 0;
            nudDescuento.Value = 0;
            nudSubtotal.Value = 0;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvDetalleVenta.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoVenta.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de detalles de venta registrados: " + detalleVenta.Count;
        }
        private void CargarCatalogosFK()
        {
            try
            {
                ComboFK.Cargar(cmbCodigoVenta, relacional.ListarCatalogo<VentasEntidad>(), x => x.CodigoVenta, x => "Venta | " + x.FechaVenta.ToString("dd/MM/yyyy"), false);
                ComboFK.Cargar(cmbCodigoEquipo, relacional.ListarCatalogo<EquiposEntidad>(), x => x.CodigoEquipo, x => "Equipo " + x.Modelo + " | serie " + x.NumeroSerie, false);
                ComboFK.Cargar(cmbCodigoPlan, relacional.ListarCatalogo<PlanesEntidad>(), x => x.CodigoPlan, x => "Plan " + x.Nombre, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
