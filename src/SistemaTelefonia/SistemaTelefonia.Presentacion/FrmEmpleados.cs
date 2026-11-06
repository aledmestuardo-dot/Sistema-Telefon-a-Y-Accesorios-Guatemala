using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmEmpleados : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<EmpleadosEntidad> empleados = new BindingList<EmpleadosEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private readonly SucursalesNegocio sucursalesNegocio = new SucursalesNegocio();
        private readonly UsuariosNegocio usuariosNegocio = new UsuariosNegocio();
        private readonly List<SucursalesEntidad> sucursalesDisponibles = new List<SucursalesEntidad>();
        private readonly List<UsuariosEntidad> usuariosDisponibles = new List<UsuariosEntidad>();
        private bool cargando = false;
        private DashboardCrudSimple dashboard;
        private TableLayoutPanel layoutBloquesEmpleados;
        private TableLayoutPanel layoutBusquedaEmpleados;
        private TableLayoutPanel layoutCatalogoEmpleados;
        private ComboBox cboFiltroEstadoEmpleados;

        public FrmEmpleados()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            ConfigurarDashboard();
            ConstruirOrdenBloques();
            CargarCatalogosFK();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConstruirNavegacionLateral();

            origen.DataSource = empleados;
            dgvEmpleados.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvEmpleados);
            ConfigurarBusquedaModeloEmpleados();
            ConstruirLayoutCatalogoEmpleados();

            ConectarEventos();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
        }

        private void ConfigurarDashboard()
        {
            dashboard = new DashboardCrudSimple("Dashboard del sistema", "Empleados por puesto", ObtenerDatosDashboard);
        }

        private void ConfigurarBusquedaModeloEmpleados()
        {
            cboFiltroEstadoEmpleados = Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault();
            if (cboFiltroEstadoEmpleados == null) return;

            Panel panelFiltro = Controls.Find("pnlFiltroEstadoComun", true).OfType<Panel>().FirstOrDefault();
            if (panelFiltro != null)
            {
                panelFiltro.Controls.Remove(cboFiltroEstadoEmpleados);
                panelFiltro.Controls.Remove(btnBuscar);
                pnlBusqueda.Controls.Remove(panelFiltro);
                panelFiltro.Dispose();
            }
            pnlBusqueda.Controls.Remove(txtBuscar);
            pnlBusqueda.Controls.Clear();
            pnlBusqueda.Height = 54;

            layoutBusquedaEmpleados = new TableLayoutPanel
            {
                Name = "layoutBusquedaEmpleados", Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlBusqueda.BackColor
            };
            layoutBusquedaEmpleados.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBusquedaEmpleados.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutBusquedaEmpleados.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBusquedaEmpleados.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Math.Max(96, btnNuevo.Width)));
            cboFiltroEstadoEmpleados.Dock = DockStyle.Fill;
            txtBuscar.Dock = DockStyle.Fill;
            btnBuscar.Dock = DockStyle.Fill;
            cboFiltroEstadoEmpleados.Margin = new Padding(0, 0, 8, 0);
            txtBuscar.Margin = new Padding(0, 0, 8, 0);
            btnBuscar.Margin = Padding.Empty;
            layoutBusquedaEmpleados.Controls.Add(cboFiltroEstadoEmpleados, 0, 0);
            layoutBusquedaEmpleados.Controls.Add(txtBuscar, 1, 0);
            layoutBusquedaEmpleados.Controls.Add(btnBuscar, 2, 0);
            pnlBusqueda.Controls.Add(layoutBusquedaEmpleados);
            pnlAcciones.SizeChanged += (s, e) =>
            {
                if (layoutBusquedaEmpleados != null && layoutBusquedaEmpleados.ColumnStyles.Count == 3)
                    layoutBusquedaEmpleados.ColumnStyles[2].Width = Math.Max(72, btnNuevo.Width);
            };
        }

        private void ConstruirLayoutCatalogoEmpleados()
        {
            Panel paginacion = Controls.Find("pnlPaginacion", true).OfType<Panel>().FirstOrDefault();
            if (paginacion == null) return;

            layoutCatalogoEmpleados = new TableLayoutPanel
            {
                Name = "layoutCatalogoEmpleados", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 5,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlCatalogo.BackColor,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutCatalogoEmpleados.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCatalogoEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutCatalogoEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlBusqueda.Height));
            layoutCatalogoEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlColumnas.Height));
            layoutCatalogoEmpleados.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCatalogoEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));

            pnlCatalogo.Controls.Remove(lblCatalogoTitulo);
            pnlCatalogo.Controls.Remove(pnlBusqueda);
            pnlCatalogo.Controls.Remove(pnlColumnas);
            pnlCatalogo.Controls.Remove(dgvEmpleados);
            pnlCatalogo.Controls.Remove(paginacion);
            lblCatalogoTitulo.Dock = DockStyle.Fill;
            pnlBusqueda.Dock = DockStyle.Fill;
            pnlColumnas.Dock = DockStyle.Fill;
            dgvEmpleados.Dock = DockStyle.Fill;
            paginacion.Dock = DockStyle.Fill;
            paginacion.Height = 48;
            layoutCatalogoEmpleados.Controls.Add(lblCatalogoTitulo, 0, 0);
            layoutCatalogoEmpleados.Controls.Add(pnlBusqueda, 0, 1);
            layoutCatalogoEmpleados.Controls.Add(pnlColumnas, 0, 2);
            layoutCatalogoEmpleados.Controls.Add(dgvEmpleados, 0, 3);
            layoutCatalogoEmpleados.Controls.Add(paginacion, 0, 4);
            pnlCatalogo.Controls.Add(layoutCatalogoEmpleados);

            int altoGrilla = dgvEmpleados.ColumnHeadersHeight + 10 * dgvEmpleados.RowTemplate.Height + 2;
            int altoCatalogo = pnlCatalogo.Padding.Vertical + 40 + pnlBusqueda.Height + pnlColumnas.Height + altoGrilla + paginacion.Height;
            pnlCatalogo.Height = altoCatalogo;
            layoutBloquesEmpleados.RowStyles[4].Height = altoCatalogo;
            layoutBloquesEmpleados.Height = layoutBloquesEmpleados.RowStyles.Cast<RowStyle>().Sum(fila =>
                fila.SizeType == SizeType.Absolute ? (int)fila.Height : 0);
            pnlCatalogo.PerformLayout();
            layoutBloquesEmpleados.PerformLayout();
        }

        private void ConstruirOrdenBloques()
        {
            int altoFormulario = pnlFormulario.Height;
            int altoAcciones = pnlAcciones.Height;
            int altoCatalogo = pnlCatalogo.Height;
            layoutBloquesEmpleados = new TableLayoutPanel
            {
                Name = "layoutBloquesEmpleados", Dock = DockStyle.Top, ColumnCount = 1,
                RowCount = 5, Padding = Padding.Empty, Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesEmpleados.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, dashboard.Height));
            layoutBloquesEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutBloquesEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, altoFormulario));
            layoutBloquesEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, altoAcciones));
            layoutBloquesEmpleados.RowStyles.Add(new RowStyle(SizeType.Absolute, altoCatalogo));

            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesEmpleados);
            pnlFormulario.Dock = DockStyle.Fill;
            pnlAcciones.Dock = DockStyle.Fill;
            pnlCatalogo.Dock = DockStyle.Fill;
            dashboard.Dock = DockStyle.Fill;
            layoutBloquesEmpleados.Controls.Add(dashboard, 0, 0);
            layoutBloquesEmpleados.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesEmpleados.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesEmpleados.Controls.Add(pnlCatalogo, 0, 4);
            int altoTotal = dashboard.Height + 10 + altoFormulario + altoAcciones + altoCatalogo;
            layoutBloquesEmpleados.Height = altoTotal;
            pnlContenido.AutoScroll = true;
        }

        private DashboardCrudDatos ObtenerDatosDashboard()
        {
            List<EmpleadosEntidad> lista = relacional.Listar<EmpleadosEntidad>();
            List<EmpleadosEntidad> activos = lista.Where(e => e.Estado).ToList();
            decimal nomina = activos.Sum(e => e.Salario);
            int sinTelefono = activos.Count(e => string.IsNullOrWhiteSpace(e.Telefono));
            string actividad = activos.Count == 0 ? "No hay empleados activos." : string.Join(Environment.NewLine,
                activos.OrderBy(e => e.NombreEmpleado).Take(4).Select(e => "• " + e.NombreEmpleado + " · " + e.TipoEmpleado));

            return new DashboardCrudDatos
            {
                TitulosIndicadores = new[] { "EMPLEADOS REGISTRADOS", "EMPLEADOS ACTIVOS", "INACTIVOS", "NÓMINA ACTIVA" },
                ValoresIndicadores = new[]
                {
                    lista.Count.ToString("N0"), activos.Count.ToString("N0"),
                    lista.Count(e => !e.Estado).ToString("N0"),
                    nomina.ToString("C0", CultureInfo.GetCultureInfo("es-GT"))
                },
                Serie = activos.Where(e => !string.IsNullOrWhiteSpace(e.TipoEmpleado))
                    .GroupBy(e => e.TipoEmpleado.Trim(), StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(g => g.Count()).ThenBy(g => g.Key)
                    .Take(8).Select(g => new KeyValuePair<string, int>(g.Key, g.Count())).ToList(),
                Alertas = "• Sin teléfono: " + sinTelefono + Environment.NewLine + "• Sin sucursal: " + activos.Count(e => e.CodigoSucursal <= 0),
                Actividad = actividad
            };
        }

        private void SeleccionarSucursal(int codigo)
        {
            if (!sucursalesDisponibles.Any(s => s.CodigoSucursal == codigo))
            {
                SucursalesEntidad sucursal = sucursalesNegocio.Listar().FirstOrDefault(s => s.CodigoSucursal == codigo);
                if (sucursal != null)
                {
                    sucursalesDisponibles.Add(sucursal);
                    ComboFK.Cargar(cmbCodigoSucursal, sucursalesDisponibles, x => x.CodigoSucursal, x => x.Nombre);
                }
            }
            ComboFK.Seleccionar(cmbCodigoSucursal, codigo);
        }

        private void SeleccionarUsuario(int codigo)
        {
            if (!usuariosDisponibles.Any(u => u.CodigoUsuario == codigo))
            {
                UsuariosEntidad usuario = usuariosNegocio.Listar().FirstOrDefault(u => u.CodigoUsuario == codigo);
                if (usuario != null)
                {
                    usuariosDisponibles.Add(usuario);
                    ComboFK.Cargar(cmbCodigoUsuario, usuariosDisponibles, x => x.CodigoUsuario, x => x.Nombre + " | " + x.TipoUsuario);
                }
            }
            ComboFK.Seleccionar(cmbCodigoUsuario, codigo);
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
                MostrarEmpleados(empleados.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarEmpleados();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarEmpleados();
                    e.SuppressKeyPress = true;
                }
            };

            dgvEmpleados.SelectionChanged += DgvEmpleados_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoSucursal.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoUsuario.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNombreEmpleado.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaNacimiento.CheckedChanged += (s, e) => ActualizarColumnas();
            chkSalario.CheckedChanged += (s, e) => ActualizarColumnas();
            chkFechaContratacion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTelefono.CheckedChanged += (s, e) => ActualizarColumnas();
            chkObservaciones.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDpi.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDireccion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTipoEmpleado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Label opcion = sender as Label;
            if (opcion != null && opcion.Tag != null)
                Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoEmpleado", chkCodigo.Checked);
            CambiarVisibilidad("CodigoSucursal", chkCodigoSucursal.Checked);
            CambiarVisibilidad("CodigoUsuario", chkCodigoUsuario.Checked);
            CambiarVisibilidad("NombreEmpleado", chkNombreEmpleado.Checked);
            CambiarVisibilidad("FechaNacimiento", chkFechaNacimiento.Checked);
            CambiarVisibilidad("Salario", chkSalario.Checked);
            CambiarVisibilidad("FechaContratacion", chkFechaContratacion.Checked);
            CambiarVisibilidad("Telefono", chkTelefono.Checked);
            CambiarVisibilidad("Observaciones", chkObservaciones.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
            CambiarVisibilidad("Dpi", chkDpi.Checked);
            CambiarVisibilidad("Direccion", chkDireccion.Checked);
            CambiarVisibilidad("TipoEmpleado", chkTipoEmpleado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<EmpleadosEntidad> registros = relacional.Listar<EmpleadosEntidad>();
                empleados.Clear();
                foreach (EmpleadosEntidad registro in registros) empleados.Add(registro);
                if (dashboard != null) _ = dashboard.CargarAsync();
                MostrarEmpleados(empleados.ToList());
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
            if (dgvEmpleados.Columns.Contains(columna))
                dgvEmpleados.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvEmpleados.Rows.Count > 0)
                {
                    int indice = dgvEmpleados.CurrentRow == null ? 0 : dgvEmpleados.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvEmpleados.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvEmpleados.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvEmpleados.Rows.Count)
                return;

            dgvEmpleados.ClearSelection();

            DataGridViewCell celda = dgvEmpleados.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvEmpleados.CurrentCell = celda;

            dgvEmpleados.Rows[indice].Selected = true;
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
            EmpleadosEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            // Conserva relaciones inactivas preexistentes durante la edición.
            SeleccionarSucursal(seleccionado.CodigoSucursal);
            SeleccionarUsuario(seleccionado.CodigoUsuario);
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoEmpleado, CrearEntidad());
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
            EmpleadosEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoEmpleado + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<EmpleadosEntidad>(seleccionado.CodigoEmpleado);
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

        private EmpleadosEntidad CrearEntidad()
        {
            return new EmpleadosEntidad(
                ComboFK.Obtener(cmbCodigoSucursal).Value,
                ComboFK.Obtener(cmbCodigoUsuario).Value,
                txtNombreEmpleado.Text.Trim(),
                dtpFechaNacimiento.Value,
                nudSalario.Value,
                dtpFechaContratacion.Value,
                ValorONulo(txtTelefono.Text),
                ValorONulo(txtObservaciones.Text),
                rdoActivo.Checked,
                txtDpi.Text.Trim(),
                txtDireccion.Text.Trim(),
                cmbTipoEmpleado.Text.Trim());
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoSucursal).HasValue)
            {
                MessageBox.Show("El campo Cód. sucursal debe ser mayor que cero.");
                cmbCodigoSucursal.Focus();
                return false;
            }

            if (!ComboFK.Obtener(cmbCodigoUsuario).HasValue)
            {
                MessageBox.Show("El campo Cód. usuario debe ser mayor que cero.");
                cmbCodigoUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreEmpleado.Text))
            {
                MessageBox.Show("El campo Nombre del empleado es obligatorio.");
                txtNombreEmpleado.Focus();
                return false;
            }

            if (txtNombreEmpleado.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Nombre del empleado no puede superar 100 caracteres.");
                txtNombreEmpleado.Focus();
                return false;
            }

            if (nudSalario.Value <= 0)
            {
                MessageBox.Show("El campo Salario debe ser mayor que cero.");
                nudSalario.Focus();
                return false;
            }

            if (txtTelefono.Text.Trim().Length > 20)
            {
                MessageBox.Show("El campo Teléfono no puede superar 20 caracteres.");
                txtTelefono.Focus();
                return false;
            }

            if (txtObservaciones.Text.Trim().Length > 255)
            {
                MessageBox.Show("El campo Observaciones no puede superar 255 caracteres.");
                txtObservaciones.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDpi.Text))
            {
                MessageBox.Show("El campo DPI es obligatorio.");
                txtDpi.Focus();
                return false;
            }

            if (txtDpi.Text.Trim().Length > 25)
            {
                MessageBox.Show("El campo DPI no puede superar 25 caracteres.");
                txtDpi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("El campo Dirección es obligatorio.");
                txtDireccion.Focus();
                return false;
            }

            if (txtDireccion.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Dirección no puede superar 100 caracteres.");
                txtDireccion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbTipoEmpleado.Text))
            {
                MessageBox.Show("El campo Tipo de empleado es obligatorio.");
                cmbTipoEmpleado.Focus();
                return false;
            }

            if (cmbTipoEmpleado.Text.Trim().Length > 50)
            {
                MessageBox.Show("El campo Tipo de empleado no puede superar 50 caracteres.");
                cmbTipoEmpleado.Focus();
                return false;
            }

            return true;
        }

        private EmpleadosEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvEmpleados.CurrentRow == null)
                return null;

            return dgvEmpleados.CurrentRow.DataBoundItem as EmpleadosEntidad;
        }

        private void DgvEmpleados_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            EmpleadosEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoEmpleado.ToString();
            ComboFK.Seleccionar(cmbCodigoSucursal, registro.CodigoSucursal);
            ComboFK.Seleccionar(cmbCodigoUsuario, registro.CodigoUsuario);
            txtNombreEmpleado.Text = registro.NombreEmpleado;
            dtpFechaNacimiento.Value = LimitarFecha(dtpFechaNacimiento, registro.FechaNacimiento);
            nudSalario.Value = Limitar(nudSalario, registro.Salario);
            dtpFechaContratacion.Value = LimitarFecha(dtpFechaContratacion, registro.FechaContratacion);
            txtTelefono.Text = registro.Telefono;
            txtObservaciones.Text = registro.Observaciones;
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;
            txtDpi.Text = registro.Dpi;
            txtDireccion.Text = registro.Direccion;
            cmbTipoEmpleado.Text = registro.TipoEmpleado;

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

        private void BuscarEmpleados()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarEmpleados(empleados.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<EmpleadosEntidad> resultado = relacional.Buscar<EmpleadosEntidad>(criterio);
                MostrarEmpleados(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + empleados.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarEmpleados(List<EmpleadosEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvEmpleados.DataSource = origen;
            ActualizarColumnas();
            dgvEmpleados.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoSucursal, null);
            ComboFK.Seleccionar(cmbCodigoUsuario, null);
            txtNombreEmpleado.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            nudSalario.Value = 0;
            dtpFechaContratacion.Value = DateTime.Now;
            txtTelefono.Clear();
            txtObservaciones.Clear();
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;
            txtDpi.Clear();
            txtDireccion.Clear();
            cmbTipoEmpleado.Text = string.Empty;

            dgvEmpleados.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoSucursal.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de empleados registrados: " + empleados.Count;
        }
        private void CargarCatalogosFK()
        {
            try
            {
                sucursalesDisponibles.Clear();
                sucursalesDisponibles.AddRange(relacional.ListarCatalogo<SucursalesEntidad>().Where(x => x.Estado));
                usuariosDisponibles.Clear();
                usuariosDisponibles.AddRange(relacional.ListarCatalogo<UsuariosEntidad>().Where(x => x.Estado));
                ComboFK.Cargar(cmbCodigoSucursal, sucursalesDisponibles, x => x.CodigoSucursal, x => x.Nombre);
                ComboFK.Cargar(cmbCodigoUsuario, usuariosDisponibles, x => x.CodigoUsuario, x => x.Nombre + " | " + x.TipoUsuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
