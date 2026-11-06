using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmDetalleCompra : Form
    {
        private readonly BindingList<DetalleCompraEntidad> detalleCompra = new BindingList<DetalleCompraEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;

        public FrmDetalleCompra()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = detalleCompra;
            dgvDetalleCompra.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvDetalleCompra);

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
                MostrarDetalleCompra(detalleCompra.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarDetalleCompra();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarDetalleCompra();
                    e.SuppressKeyPress = true;
                }
            };

            dgvDetalleCompra.SelectionChanged += DgvDetalleCompra_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;
            LectorCodigoBarras.Conectar(cmbCodigoEquipo, relacional, equipo => nudCantidad.Focus());

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoCompra.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoEquipo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCantidad.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCostoUnitario.CheckedChanged += (s, e) => ActualizarColumnas();
            chkSubtotal.CheckedChanged += (s, e) => ActualizarColumnas();
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
            CambiarVisibilidad("CodigoDetalleCompra", chkCodigo.Checked);
            CambiarVisibilidad("CodigoCompra", chkCodigoCompra.Checked);
            CambiarVisibilidad("CodigoEquipo", chkCodigoEquipo.Checked);
            CambiarVisibilidad("Cantidad", chkCantidad.Checked);
            CambiarVisibilidad("CostoUnitario", chkCostoUnitario.Checked);
            CambiarVisibilidad("Subtotal", chkSubtotal.Checked);
            CambiarVisibilidad("Observaciones", chkObservaciones.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<DetalleCompraEntidad> registros = relacional.Listar<DetalleCompraEntidad>();
                detalleCompra.Clear();
                foreach (DetalleCompraEntidad registro in registros) detalleCompra.Add(registro);
                MostrarDetalleCompra(detalleCompra.ToList());
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
            if (dgvDetalleCompra.Columns.Contains(columna))
                dgvDetalleCompra.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvDetalleCompra.Rows.Count > 0)
                {
                    int indice = dgvDetalleCompra.CurrentRow == null ? 0 : dgvDetalleCompra.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvDetalleCompra.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvDetalleCompra.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvDetalleCompra.Rows.Count)
                return;

            dgvDetalleCompra.ClearSelection();

            DataGridViewCell celda = dgvDetalleCompra.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvDetalleCompra.CurrentCell = celda;

            dgvDetalleCompra.Rows[indice].Selected = true;
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
            DetalleCompraEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoDetalleCompra, CrearEntidad());
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
            DetalleCompraEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoDetalleCompra + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<DetalleCompraEntidad>(seleccionado.CodigoDetalleCompra);
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

        private DetalleCompraEntidad CrearEntidad()
        {
            return new DetalleCompraEntidad(
                ComboFK.Obtener(cmbCodigoCompra).Value,
                ComboFK.Obtener(cmbCodigoEquipo).Value,
                decimal.ToInt32(nudCantidad.Value),
                nudCostoUnitario.Value,
                nudSubtotal.Value,
                ValorONulo(txtObservaciones.Text),
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoCompra).HasValue)
            {
                MessageBox.Show("El campo Cód. compra debe ser mayor que cero.");
                cmbCodigoCompra.Focus();
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

            if (nudCostoUnitario.Value <= 0)
            {
                MessageBox.Show("El campo Costo unitario debe ser mayor que cero.");
                nudCostoUnitario.Focus();
                return false;
            }

            if (nudSubtotal.Value <= 0)
            {
                MessageBox.Show("El campo Subtotal debe ser mayor que cero.");
                nudSubtotal.Focus();
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

        private DetalleCompraEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvDetalleCompra.CurrentRow == null)
                return null;

            return dgvDetalleCompra.CurrentRow.DataBoundItem as DetalleCompraEntidad;
        }

        private void DgvDetalleCompra_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            DetalleCompraEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoDetalleCompra.ToString();
            ComboFK.Seleccionar(cmbCodigoCompra, registro.CodigoCompra);
            ComboFK.Seleccionar(cmbCodigoEquipo, registro.CodigoEquipo);
            nudCantidad.Value = Limitar(nudCantidad, registro.Cantidad);
            nudCostoUnitario.Value = Limitar(nudCostoUnitario, registro.CostoUnitario);
            nudSubtotal.Value = Limitar(nudSubtotal, registro.Subtotal);
            txtObservaciones.Text = registro.Observaciones;
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
        }

        private decimal Limitar(NumericUpDown control, decimal valor)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, valor));
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

        private void BuscarDetalleCompra()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarDetalleCompra(detalleCompra.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<DetalleCompraEntidad> resultado = relacional.Buscar<DetalleCompraEntidad>(criterio);
                MostrarDetalleCompra(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + detalleCompra.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalleCompra(List<DetalleCompraEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvDetalleCompra.DataSource = origen;
            ActualizarColumnas();
            dgvDetalleCompra.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoCompra, null);
            ComboFK.Seleccionar(cmbCodigoEquipo, null);
            nudCantidad.Value = 0;
            nudCostoUnitario.Value = 0;
            nudSubtotal.Value = 0;
            txtObservaciones.Clear();
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvDetalleCompra.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoCompra.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de detalles de compra registrados: " + detalleCompra.Count;
        }
        private void CargarCatalogosFK()
        {
            try
            {
                ComboFK.Cargar(cmbCodigoCompra, relacional.ListarCatalogo<ComprasEntidad>(), x => x.CodigoCompra, x => "Compra | " + x.NumeroDocumento, false);
                ComboFK.Cargar(cmbCodigoEquipo, relacional.ListarCatalogo<EquiposEntidad>(), x => x.CodigoEquipo, x => "Equipo " + x.Modelo + " | serie " + x.NumeroSerie, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
