using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmEquipos : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<EquiposEntidad> equipos = new BindingList<EquiposEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;

        public FrmEquipos()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            CargarCatalogosFK();
            Activated += (s, e) => CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = equipos;
            dgvEquipos.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvEquipos);

            ConectarEventos();
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
                MostrarEquipos(equipos.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarEquipos();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarEquipos();
                    e.SuppressKeyPress = true;
                }
            };

            dgvEquipos.SelectionChanged += DgvEquipos_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoMarca.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCategorias.CheckedChanged += (s, e) => ActualizarColumnas();
            chkModelo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDescripcion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkPrecio.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNumeroSerie.CheckedChanged += (s, e) => ActualizarColumnas();
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
            CambiarVisibilidad("CodigoEquipo", chkCodigo.Checked);
            CambiarVisibilidad("CodigoMarca", chkCodigoMarca.Checked);
            CambiarVisibilidad("Categorias", chkCategorias.Checked);
            CambiarVisibilidad("Modelo", chkModelo.Checked);
            CambiarVisibilidad("Descripcion", chkDescripcion.Checked);
            CambiarVisibilidad("Precio", chkPrecio.Checked);
            CambiarVisibilidad("NumeroSerie", chkNumeroSerie.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<EquiposEntidad> registros = relacional.Listar<EquiposEntidad>();
                equipos.Clear();
                foreach (EquiposEntidad registro in registros) equipos.Add(registro);
                MostrarEquipos(equipos.ToList());
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
            if (dgvEquipos.Columns.Contains(columna))
                dgvEquipos.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvEquipos.Rows.Count > 0)
                {
                    int indice = dgvEquipos.CurrentRow == null ? 0 : dgvEquipos.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvEquipos.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvEquipos.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvEquipos.Rows.Count)
                return;

            dgvEquipos.ClearSelection();

            DataGridViewCell celda = dgvEquipos.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvEquipos.CurrentCell = celda;

            dgvEquipos.Rows[indice].Selected = true;
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
            EquiposEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoEquipo, CrearEntidad());
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
            EquiposEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoEquipo + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<EquiposEntidad>(seleccionado.CodigoEquipo);
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

        private EquiposEntidad CrearEntidad()
        {
            return new EquiposEntidad(
                ComboFK.Obtener(cmbCodigoMarca).Value,
                cmbCategorias.Text.Trim(),
                txtModelo.Text.Trim(),
                txtDescripcion.Text.Trim(),
                nudPrecio.Value,
                txtNumeroSerie.Text.Trim(),
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoMarca).HasValue)
            {
                MessageBox.Show("El campo Cód. marca debe ser mayor que cero.");
                cmbCodigoMarca.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbCategorias.Text))
            {
                MessageBox.Show("El campo Categoría es obligatorio.");
                cmbCategorias.Focus();
                return false;
            }

            if (cmbCategorias.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Categoría no puede superar 100 caracteres.");
                cmbCategorias.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("El campo Modelo es obligatorio.");
                txtModelo.Focus();
                return false;
            }

            if (txtModelo.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Modelo no puede superar 100 caracteres.");
                txtModelo.Focus();
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

            if (nudPrecio.Value <= 0)
            {
                MessageBox.Show("El campo Precio debe ser mayor que cero.");
                nudPrecio.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroSerie.Text))
            {
                MessageBox.Show("El campo Número de serie es obligatorio.");
                txtNumeroSerie.Focus();
                return false;
            }

            if (txtNumeroSerie.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Número de serie no puede superar 100 caracteres.");
                txtNumeroSerie.Focus();
                return false;
            }

            return true;
        }

        private EquiposEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvEquipos.CurrentRow == null)
                return null;

            return dgvEquipos.CurrentRow.DataBoundItem as EquiposEntidad;
        }

        private void DgvEquipos_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            EquiposEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoEquipo.ToString();
            ComboFK.Seleccionar(cmbCodigoMarca, registro.CodigoMarca);
            cmbCategorias.Text = registro.Categorias;
            txtModelo.Text = registro.Modelo;
            txtDescripcion.Text = registro.Descripcion;
            nudPrecio.Value = Limitar(nudPrecio, registro.Precio);
            txtNumeroSerie.Text = registro.NumeroSerie;
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
        }

        private decimal Limitar(NumericUpDown control, decimal valor)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, valor));
        }

        private static bool Contiene(string valor, string criterio)
        {
            return valor != null && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void BuscarEquipos()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarEquipos(equipos.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<EquiposEntidad> resultado = relacional.Buscar<EquiposEntidad>(criterio);
                MostrarEquipos(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + equipos.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarEquipos(List<EquiposEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvEquipos.DataSource = origen;
            ActualizarColumnas();
            dgvEquipos.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoMarca, null);
            cmbCategorias.Text = string.Empty;
            txtModelo.Clear();
            txtDescripcion.Clear();
            nudPrecio.Value = 0;
            txtNumeroSerie.Clear();
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvEquipos.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoMarca.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de equipos registrados: " + equipos.Count;
        }
        private void CargarCatalogosFK()
        {
            try
            {
                int? marcaSeleccionada = ComboFK.Obtener(cmbCodigoMarca);
                ComboFK.Cargar(cmbCodigoMarca, relacional.ListarCatalogo<MarcasEntidad>(), x => x.CodigoMarca, x => x.Nombre, false);
                ComboFK.Seleccionar(cmbCodigoMarca, marcaSeleccionada);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
