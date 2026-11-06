using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmMarcas : Form
    {
        // Colección local enlazada a la grilla; los datos se cargan desde la capa de Negocio.
        private readonly BindingList<MarcasEntidad> marcas = new BindingList<MarcasEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;
        private TableLayoutPanel layoutBloquesMarcas;

        public FrmMarcas()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            // La PK se muestra como dato informativo generado por la base; no recibe foco ni selección.
            txtCodigo.Enabled = false;
            txtCodigo.TabStop = false;
            CargarCatalogosFK();
            Activated += (s, e) => CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = marcas;
            dgvMarcas.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvMarcas);
            ConstruirLayoutDesplazable();
            dgvMarcas.DataBindingComplete += (s, e) => AjustarInterfazMarcas();
            Shown += (s, e) => AjustarInterfazMarcas();
            SizeChanged += (s, e) => AjustarInterfazMarcas();

            ConectarEventos();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
            AjustarInterfazMarcas();
        }

        private void ConstruirLayoutDesplazable()
        {
            int altoFormulario = pnlFormulario.Height;
            int altoAcciones = pnlAcciones.Height;

            pnlContenido.AutoScroll = true;
            pnlFormulario.Dock = DockStyle.Fill;
            pnlAcciones.Dock = DockStyle.Fill;
            pnlCatalogo.Dock = DockStyle.Fill;
            dgvMarcas.Dock = DockStyle.Top;
            dgvMarcas.ScrollBars = ScrollBars.Both;

            layoutBloquesMarcas = new TableLayoutPanel
            {
                Name = "layoutBloquesMarcas",
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 3,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesMarcas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesMarcas.RowStyles.Add(new RowStyle(SizeType.Absolute, altoFormulario));
            layoutBloquesMarcas.RowStyles.Add(new RowStyle(SizeType.Absolute, altoAcciones));
            layoutBloquesMarcas.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));

            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesMarcas);
            layoutBloquesMarcas.Controls.Add(pnlFormulario, 0, 0);
            layoutBloquesMarcas.Controls.Add(pnlAcciones, 0, 1);
            layoutBloquesMarcas.Controls.Add(pnlCatalogo, 0, 2);
            AjustarInterfazMarcas();
        }

        private void AjustarInterfazMarcas()
        {
            if (IsDisposed || layoutBloquesMarcas == null || pnlCatalogo.Width <= 0) return;

            dgvMarcas.Dock = DockStyle.Top;
            dgvMarcas.ScrollBars = ScrollBars.Both;
            int altoGrilla = dgvMarcas.ColumnHeadersHeight + 10 * dgvMarcas.RowTemplate.Height + 2;
            if (dgvMarcas.Height != altoGrilla) dgvMarcas.Height = altoGrilla;

            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height +
                pnlColumnas.Height + altoGrilla +
                (pnlCatalogo.Controls.Find("pnlPaginacion", false).Length > 0 ? 48 : lblRegistros.Height);
            if (pnlCatalogo.Height != altoCatalogo) pnlCatalogo.Height = altoCatalogo;
            if (layoutBloquesMarcas.RowStyles[2].Height != altoCatalogo)
                layoutBloquesMarcas.RowStyles[2].Height = altoCatalogo;

            int altoTotal = pnlFormulario.Height + pnlAcciones.Height + altoCatalogo;
            if (layoutBloquesMarcas.Height != altoTotal) layoutBloquesMarcas.Height = altoTotal;
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
                MostrarMarcas(marcas.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarMarcas();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarMarcas();
                    e.SuppressKeyPress = true;
                }
            };

            dgvMarcas.SelectionChanged += DgvMarcas_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoProveedor.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNombre.CheckedChanged += (s, e) => ActualizarColumnas();
            chkPaisOrigen.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDescripcion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkSitioWeb.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaRegistro.CheckedChanged += (s, e) => ActualizarColumnas();
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
            CambiarVisibilidad("CodigoMarca", chkCodigo.Checked);
            CambiarVisibilidad("CodigoProveedor", chkCodigoProveedor.Checked);
            CambiarVisibilidad("Nombre", chkNombre.Checked);
            CambiarVisibilidad("PaisOrigen", chkPaisOrigen.Checked);
            CambiarVisibilidad("Descripcion", chkDescripcion.Checked);
            CambiarVisibilidad("SitioWeb", chkSitioWeb.Checked);
            CambiarVisibilidad("FechaRegistro", chkFechaRegistro.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<MarcasEntidad> registros = relacional.Listar<MarcasEntidad>();
                marcas.Clear();
                foreach (MarcasEntidad registro in registros) marcas.Add(registro);
                MostrarMarcas(marcas.ToList());
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
            if (dgvMarcas.Columns.Contains(columna))
                dgvMarcas.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvMarcas.Rows.Count > 0)
                {
                    int indice = dgvMarcas.CurrentRow == null ? 0 : dgvMarcas.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvMarcas.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvMarcas.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvMarcas.Rows.Count)
                return;

            dgvMarcas.ClearSelection();

            DataGridViewCell celda = dgvMarcas.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvMarcas.CurrentCell = celda;

            dgvMarcas.Rows[indice].Selected = true;
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
            MarcasEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoMarca, CrearEntidad());
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
            MarcasEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoMarca + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.InactivarMarca(seleccionado.CodigoMarca);
                ConsultarRelacional();
                LimpiarFormulario();
                MessageBox.Show("La marca quedó inactiva y se conservó en la base de datos.", "Eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private MarcasEntidad CrearEntidad()
        {
            return new MarcasEntidad(
                ComboFK.Obtener(cmbCodigoProveedor).Value,
                txtNombre.Text.Trim(),
                ValorONulo(txtPaisOrigen.Text),
                ValorONulo(txtDescripcion.Text),
                ValorONulo(txtSitioWeb.Text),
                dtpFechaRegistro.Value,
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoProveedor).HasValue)
            {
                MessageBox.Show("El campo Cód. proveedor debe ser mayor que cero.");
                cmbCodigoProveedor.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.");
                txtNombre.Focus();
                return false;
            }

            if (txtNombre.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Nombre no puede superar 100 caracteres.");
                txtNombre.Focus();
                return false;
            }

            if (txtPaisOrigen.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo País de origen no puede superar 100 caracteres.");
                txtPaisOrigen.Focus();
                return false;
            }

            if (txtDescripcion.Text.Trim().Length > 200)
            {
                MessageBox.Show("El campo Descripción no puede superar 200 caracteres.");
                txtDescripcion.Focus();
                return false;
            }

            if (txtSitioWeb.Text.Trim().Length > 150)
            {
                MessageBox.Show("El campo Sitio web no puede superar 150 caracteres.");
                txtSitioWeb.Focus();
                return false;
            }

            return true;
        }

        private MarcasEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvMarcas.CurrentRow == null)
                return null;

            return dgvMarcas.CurrentRow.DataBoundItem as MarcasEntidad;
        }

        private void DgvMarcas_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            MarcasEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoMarca.ToString();
            ComboFK.Seleccionar(cmbCodigoProveedor, registro.CodigoProveedor);
            txtNombre.Text = registro.Nombre;
            txtPaisOrigen.Text = registro.PaisOrigen;
            txtDescripcion.Text = registro.Descripcion;
            txtSitioWeb.Text = registro.SitioWeb;
            dtpFechaRegistro.Value = LimitarFecha(dtpFechaRegistro, registro.FechaRegistro);
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

        private void BuscarMarcas()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarMarcas(marcas.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<MarcasEntidad> resultado = relacional.Buscar<MarcasEntidad>(criterio);
                MostrarMarcas(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + marcas.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarMarcas(List<MarcasEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvMarcas.DataSource = origen;
            ActualizarColumnas();
            dgvMarcas.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoProveedor, null);
            txtNombre.Clear();
            txtPaisOrigen.Clear();
            txtDescripcion.Clear();
            txtSitioWeb.Clear();
            dtpFechaRegistro.Value = DateTime.Now;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvMarcas.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoProveedor.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de marcas registradas: " + marcas.Count;
        }
        private void CargarCatalogosFK()
        {
            try
            {
                int? proveedorSeleccionado = ComboFK.Obtener(cmbCodigoProveedor);
                ComboFK.Cargar(cmbCodigoProveedor, relacional.ListarCatalogo<ProveedoresEntidad>(), x => x.CodigoProveedor, x => x.Nombre, false);
                ComboFK.Seleccionar(cmbCodigoProveedor, proveedorSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
