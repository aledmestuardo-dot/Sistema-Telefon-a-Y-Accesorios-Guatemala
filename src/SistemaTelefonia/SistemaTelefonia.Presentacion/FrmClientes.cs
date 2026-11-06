using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmClientes : Form
    {
        private const int RegistrosPorPagina = 10;
        private readonly ClientesNegocio negocio = new ClientesNegocio();
        private List<ClientesEntidad> clientes = new List<ClientesEntidad>();
        private List<ClientesEntidad> clientesFiltrados = new List<ClientesEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private bool cargando = false;
        private int paginaActual = 0;
        private Panel pnlPaginacion;
        private Label lblPagina;
        private Button btnPaginaAnterior;
        private Button btnPaginaSiguiente;
        private DashboardCrudSimple dashboard;
        private TableLayoutPanel layoutBloquesClientes;
        private const int FilasMaximasVisibles = 10;
        private const int AnchoBotonPaginaMinimo = 72;
        private const int AltoBotonPagina = 42;
        private const int AltoFormularioClientes = 320;

        public FrmClientes()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConfigurarDisenoAdaptable();
            ConfigurarDashboard();
            ConstruirOrdenBloques();
            ConstruirNavegacionLateral();
            CargarDepartamentos();

            AjustesDisenoCrud.ConfigurarTabla(dgvClientes);
            ConfigurarColumnasGrilla();
            TemaVisualCrud.Aplicar(this);
            ConfigurarResaltadoCampos();
            AjustesDisenoCrud.AplicarCatalogoModeloPagos(this, pnlHeader, lblTitulo, lblSubtitulo,
                pnlBusqueda, txtBuscar, btnBuscar, pnlColumnas, lblColumnas, selector, chkSeleccionar);
            ConstruirPaginacion();
            ConectarEventos();
            ActualizarColumnas();
            LimpiarFormulario();
            dgvClientes.DataBindingComplete += (s, e) => AjustarInterfazClientes();
            Shown += (s, e) => BeginInvoke((MethodInvoker)delegate { AjustarInterfazClientes(); });
            CargarClientes();
            ComboBox filtroEstadoComun = Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault();
            if (filtroEstadoComun != null)
                filtroEstadoComun.SelectedIndexChanged += (s, e) => AplicarFiltro();
        }

        // Aplica a Clientes las proporciones del catálogo de Usuarios y fija las
        // dimensiones de las acciones para que mantengan el mismo tamaño.
        private void ConfigurarDisenoAdaptable()
        {
            pnlHeader.Height = 108;
            pnlContenido.Padding = new Padding(14);
            pnlContenido.AutoScroll = true;
            pnlFormulario.Height = AltoFormularioClientes;
            pnlAcciones.Height = 58;
            pnlFormulario.Dock = DockStyle.Top;
            pnlAcciones.Dock = DockStyle.Top;
            pnlCatalogo.Dock = DockStyle.Top;
            lblFormularioTitulo.Height = 40;
            lblCatalogoTitulo.Height = 40;
            lblColumnas.Height = 40;
            pnlBusqueda.Height = 54;
            pnlColumnas.Height = 68;
            selector.AutoScroll = false;
            chkSeleccionar.Text = "Seleccionar registro";
            dgvClientes.Dock = DockStyle.Top;
            dgvClientes.RowTemplate.Height = 29;
            dgvClientes.ColumnHeadersHeight = 34;
            dgvClientes.ScrollBars = ScrollBars.Horizontal;

            ConfigurarTamanoBotonesAccion();
            pnlContenido.SizeChanged += (s, e) => AjustarInterfazClientes();
            pnlCatalogo.SizeChanged += (s, e) => AjustarInterfazClientes();
            selector.SizeChanged += (s, e) => AjustarInterfazClientes();
        }

        private void ConfigurarTamanoBotonesAccion()
        {
            int ancho = Math.Max(AnchoBotonPaginaMinimo, btnNuevo.Width);
            const int alto = AltoBotonPagina;
            layoutBotones.SuspendLayout();
            layoutBotones.AutoSize = false;
            layoutBotones.Dock = DockStyle.None;
            layoutBotones.ColumnCount = 5;
            layoutBotones.RowCount = 1;
            layoutBotones.Size = new Size(ancho * 5 + 20, alto + 8);
            for (int columna = 0; columna < 5; columna++)
            {
                layoutBotones.ColumnStyles[columna].SizeType = SizeType.Absolute;
                layoutBotones.ColumnStyles[columna].Width = ancho;
            }
            layoutBotones.RowStyles[0].SizeType = SizeType.Absolute;
            layoutBotones.RowStyles[0].Height = alto + 8;
            foreach (Button boton in new[] { btnNuevo, btnGuardar, btnEditar, btnEliminar, btnLimpiar })
            {
                boton.Dock = DockStyle.None;
                boton.Anchor = AnchorStyles.None;
                boton.Size = new Size(ancho - 8, alto);
                boton.Margin = new Padding(4);
            }
            layoutBotones.ResumeLayout(true);
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
        }

        private void ConstruirNavegacionLateral()
        {
            AjustesDisenoCrud.ConstruirBarraLateralModelo(this);
        }

        // Mantiene en Clientes el mismo acabado de foco y hover que Usuarios y Sucursales.
        private void ConfigurarResaltadoCampos()
        {
            Control[] campos = { txtNombre, txtNit, txtCorreoElectronico, txtTelefono, txtDireccion,
                cboDepartamento, cboMunicipio };
            foreach (Control campo in campos)
            {
                CampoRedondeado.Aplicar(campo, 7);
                campo.MouseEnter += CampoCliente_MouseEnter;
                campo.MouseLeave += CampoCliente_MouseLeave;
                campo.Enter += CampoCliente_Enter;
                campo.Leave += CampoCliente_Leave;
                campo.EnabledChanged += (s, e) => RestaurarColorCampoCliente(campo);
            }
            CampoRedondeado.Aplicar(txtCodigo, 7);
        }

        private void CampoCliente_MouseEnter(object sender, EventArgs e)
        {
            Control campo = sender as Control;
            if (campo != null && campo.Enabled && !campo.Focused)
                AplicarColorResaltadoCliente(campo, Color.FromArgb(205, 243, 255));
        }

        private void CampoCliente_MouseLeave(object sender, EventArgs e)
        {
            RestaurarColorCampoCliente(sender as Control);
        }

        private void CampoCliente_Enter(object sender, EventArgs e)
        {
            Control campo = sender as Control;
            if (campo != null && campo.Enabled)
                AplicarColorResaltadoCliente(campo, ColorTranslator.FromHtml("#80DEFF"));
        }

        private void CampoCliente_Leave(object sender, EventArgs e)
        {
            RestaurarColorCampoCliente(sender as Control);
        }

        private void AplicarColorResaltadoCliente(Control campo, Color fondo)
        {
            campo.BackColor = fondo;
            campo.ForeColor = Color.FromArgb(7, 35, 66);
        }

        private void RestaurarColorCampoCliente(Control campo)
        {
            if (campo == null) return;
            if (!campo.Enabled)
            {
                campo.BackColor = Color.FromArgb(215, 222, 230);
                campo.ForeColor = Color.FromArgb(104, 116, 128);
            }
            else if (PreferenciasVisuales.Tema == TemaInterfaz.Negro)
            {
                campo.BackColor = Color.FromArgb(45, 49, 53);
                campo.ForeColor = Color.FromArgb(238, 242, 246);
            }
            else if (PreferenciasVisuales.Tema == TemaInterfaz.Claro)
            {
                campo.BackColor = Color.White;
                campo.ForeColor = Color.FromArgb(7, 35, 66);
            }
            else
            {
                campo.BackColor = Color.FromArgb(225, 236, 246);
                campo.ForeColor = Color.FromArgb(7, 35, 66);
            }
        }

        private void ConfigurarDashboard()
        {
            dashboard = new DashboardCrudSimple("Dashboard del sistema", "Clientes por departamento", ObtenerDatosDashboard);
        }

        private void ConstruirOrdenBloques()
        {
            int altoFormulario = pnlFormulario.Height;
            int altoAcciones = pnlAcciones.Height;
            int altoCatalogo = pnlCatalogo.Height;
            layoutBloquesClientes = new TableLayoutPanel
            {
                Name = "layoutBloquesClientes", Dock = DockStyle.Top, ColumnCount = 1,
                RowCount = 5, Padding = Padding.Empty, Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesClientes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesClientes.RowStyles.Add(new RowStyle(SizeType.Absolute, dashboard.Height));
            layoutBloquesClientes.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutBloquesClientes.RowStyles.Add(new RowStyle(SizeType.Absolute, altoFormulario));
            layoutBloquesClientes.RowStyles.Add(new RowStyle(SizeType.Absolute, altoAcciones));
            layoutBloquesClientes.RowStyles.Add(new RowStyle(SizeType.Absolute, altoCatalogo));

            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesClientes);
            pnlFormulario.Dock = DockStyle.Fill;
            pnlAcciones.Dock = DockStyle.Fill;
            pnlCatalogo.Dock = DockStyle.Fill;
            dashboard.Dock = DockStyle.Fill;
            layoutBloquesClientes.Controls.Add(dashboard, 0, 0);
            layoutBloquesClientes.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesClientes.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesClientes.Controls.Add(pnlCatalogo, 0, 4);
            int altoTotal = dashboard.Height + 10 + altoFormulario + altoAcciones + altoCatalogo;
            layoutBloquesClientes.Height = altoTotal;
            pnlContenido.AutoScroll = true;
        }

        private DashboardCrudDatos ObtenerDatosDashboard()
        {
            List<ClientesEntidad> lista = negocio.Listar();
            List<ClientesEntidad> activos = lista.Where(c => c.Estado).ToList();
            int sinTelefono = activos.Count(c => string.IsNullOrWhiteSpace(c.Telefono));
            int sinCorreo = activos.Count(c => string.IsNullOrWhiteSpace(c.CorreoElectronico));
            string actividad = activos.Count == 0 ? "No hay clientes activos." : string.Join(Environment.NewLine,
                activos.OrderBy(c => c.Nombre).Take(4).Select(c => "• " + c.Nombre + " · " + c.Municipio));

            return new DashboardCrudDatos
            {
                TitulosIndicadores = new[] { "CLIENTES REGISTRADOS", "CLIENTES ACTIVOS", "INACTIVOS", "DEPARTAMENTOS" },
                ValoresIndicadores = new[]
                {
                    lista.Count.ToString("N0"), activos.Count.ToString("N0"),
                    lista.Count(c => !c.Estado).ToString("N0"),
                    activos.Select(c => c.Departamento).Where(d => !string.IsNullOrWhiteSpace(d)).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString("N0")
                },
                Serie = activos.Where(c => !string.IsNullOrWhiteSpace(c.Departamento))
                    .GroupBy(c => c.Departamento.Trim(), StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(g => g.Count()).ThenBy(g => g.Key)
                    .Take(8).Select(g => new KeyValuePair<string, int>(g.Key, g.Count())).ToList(),
                Alertas = "• Sin teléfono: " + sinTelefono + Environment.NewLine + "• Sin correo: " + sinCorreo,
                Actividad = actividad
            };
        }

        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                AplicarFiltro();
                LimpiarFormulario();
            };

            // Las cuatro acciones se habilitan de forma secuencial según la validación del estudiante.
            btnGuardar.Enabled = true;
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Enabled = true;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Enabled = true;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarClientes();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarClientes();
                    e.SuppressKeyPress = true;
                }
            };

            dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;
            dgvClientes.KeyDown += DgvClientes_KeyDown;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNombre.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNit.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCorreoElectronico.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTelefono.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDireccion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDepartamento.CheckedChanged += (s, e) => ActualizarColumnas();
            chkMunicipio.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();

            cboDepartamento.SelectedIndexChanged += (s, e) => CargarMunicipios(cboDepartamento.SelectedItem as string, null);
        }

        // Separa los totales y la navegación en una barra inferior con espacio propio.
        private void ConstruirPaginacion()
        {
            pnlPaginacion = new Panel
            {
                Name = "pnlPaginacionClientes",
                Height = btnNuevo.Height + 16,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(7, 35, 66)
            };

            pnlCatalogo.Controls.Remove(lblRegistros);
            lblRegistros.Dock = DockStyle.None;
            lblRegistros.TextAlign = ContentAlignment.MiddleLeft;
            lblRegistros.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            pnlPaginacion.Controls.Add(lblRegistros);

            btnPaginaAnterior = CrearBotonPagina("‹ Anterior", "btnPaginaAnteriorClientes");
            btnPaginaSiguiente = CrearBotonPagina("Siguiente ›", "btnPaginaSiguienteClientes");
            btnPaginaAnterior.Width = btnNuevo.Width;
            btnPaginaSiguiente.Width = btnNuevo.Width;
            btnPaginaAnterior.Enabled = false;
            btnPaginaSiguiente.Enabled = false;
            lblPagina = new Label
            {
                Name = "lblPagina",
                Height = btnNuevo.Height,
                Width = 132,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Font = btnNuevo.Font,
                Text = "Página 0 de 0"
            };

            lblRegistros.Dock = DockStyle.None;
            lblRegistros.Width = 360;
            lblRegistros.AutoEllipsis = true;
            lblRegistros.TextAlign = ContentAlignment.MiddleLeft;

            btnPaginaAnterior.Click += (s, e) => CambiarPagina(paginaActual - 1);
            btnPaginaSiguiente.Click += (s, e) => CambiarPagina(paginaActual + 1);
            pnlPaginacion.Controls.Add(btnPaginaAnterior);
            pnlPaginacion.Controls.Add(lblPagina);
            pnlPaginacion.Controls.Add(btnPaginaSiguiente);
            pnlPaginacion.Resize += (s, e) => AjustarInterfazClientes();
            pnlCatalogo.Controls.Add(pnlPaginacion);
            pnlPaginacion.BringToFront();
            AcomodarPaginacion();
        }

        private Button CrearBotonPagina(string texto, string nombre)
        {
            return new Button
            {
                Name = nombre,
                Text = texto,
                Size = new Size(Math.Max(AnchoBotonPaginaMinimo, btnNuevo.Width), AltoBotonPagina),
                FlatStyle = FlatStyle.Flat,
                BackColor = btnBuscar.BackColor,
                ForeColor = btnBuscar.ForeColor,
                Font = btnBuscar.Font,
                Cursor = Cursors.Hand
            };
        }

        private void AcomodarPaginacion()
        {
            if (pnlPaginacion == null || btnPaginaAnterior == null ||
                btnPaginaSiguiente == null || lblPagina == null || lblRegistros == null)
                return;

            int alto = AltoBotonPagina;
            int y = Math.Max(0, (pnlPaginacion.ClientSize.Height - alto) / 2);
            const int separacion = 8;
            int anchoBoton = Math.Max(AnchoBotonPaginaMinimo, btnNuevo.Width);
            int derecha = pnlPaginacion.ClientSize.Width - 8;
            btnPaginaSiguiente.SetBounds(derecha - anchoBoton, y, anchoBoton, alto);
            lblPagina.SetBounds(btnPaginaSiguiente.Left - separacion - 132, y, 132, alto);
            btnPaginaAnterior.SetBounds(lblPagina.Left - separacion - anchoBoton, y, anchoBoton, alto);
            lblRegistros.SetBounds(8, y, Math.Max(120, btnPaginaAnterior.Left - separacion - 8), alto);
        }

        // Mantiene visibles diez filas y reserva una barra inferior para la navegación.
        private void AjustarInterfazClientes()
        {
            if (IsDisposed || pnlCatalogo == null || dgvClientes == null) return;
            if (pnlFormulario.Height != AltoFormularioClientes)
                pnlFormulario.Height = AltoFormularioClientes;
            if (pnlAcciones.Height != 58) pnlAcciones.Height = 58;
            int ancho = Math.Min(195, Math.Max(148, (int)(ClientSize.Width * .18)));
            pnlMenu.Width = ancho;
            pnlHeader.SetBounds(ancho, 0, Math.Max(0, ClientSize.Width - ancho), 108);
            pnlContenido.SetBounds(ancho, 108, Math.Max(0, ClientSize.Width - ancho), Math.Max(0, ClientSize.Height - 108));
            int altoFila = Math.Max(26, dgvClientes.RowTemplate.Height);
            dgvClientes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            foreach (DataGridViewRow fila in dgvClientes.Rows)
                if (!fila.IsNewRow && fila.Height != altoFila) fila.Height = altoFila;
            int altoGrilla = dgvClientes.ColumnHeadersHeight + FilasMaximasVisibles * altoFila + 2;
            if (dgvClientes.Height != altoGrilla) dgvClientes.Height = altoGrilla;
            if (pnlPaginacion != null)
            {
                pnlPaginacion.Height = AltoBotonPagina + 16;
                int anchoBotonPagina = Math.Max(AnchoBotonPaginaMinimo, btnNuevo.Width);
                btnPaginaAnterior.Size = new Size(anchoBotonPagina, AltoBotonPagina);
                btnPaginaSiguiente.Size = new Size(anchoBotonPagina, AltoBotonPagina);
                AcomodarPaginacion();
            }
            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height +
                pnlBusqueda.Height + pnlColumnas.Height + dgvClientes.Height +
                (pnlPaginacion == null ? 0 : pnlPaginacion.Height);
            if (pnlCatalogo.Height != altoCatalogo) pnlCatalogo.Height = altoCatalogo;

            // El dashboard aprovecha el espacio libre igual que en Usuarios y conserva
            // una altura mínima legible cuando el catálogo requiere desplazamiento.
            int altoBloquesFijos = pnlContenido.Padding.Vertical + pnlFormulario.Height +
                pnlAcciones.Height + altoCatalogo + 10;
            int altoDashboard = Math.Max(360, pnlContenido.ClientSize.Height - altoBloquesFijos);
            if (dashboard != null && dashboard.Height != altoDashboard) dashboard.Height = altoDashboard;

            if (layoutBloquesClientes == null)
            {
                pnlContenido.PerformLayout();
            }
            else if (layoutBloquesClientes.RowStyles.Count > 4)
            {
                layoutBloquesClientes.RowStyles[0].Height = altoDashboard;
                layoutBloquesClientes.RowStyles[2].Height = AltoFormularioClientes;
                layoutBloquesClientes.RowStyles[3].Height = 58;
                layoutBloquesClientes.RowStyles[4].Height = altoCatalogo;
                float altoSeparacion = layoutBloquesClientes.RowStyles[1].Height;
                float altoFormulario = layoutBloquesClientes.RowStyles[2].Height;
                float altoAcciones = layoutBloquesClientes.RowStyles[3].Height;
                int altoTotal = (int)Math.Ceiling(altoDashboard + altoSeparacion + altoFormulario + altoAcciones + altoCatalogo);
                if (layoutBloquesClientes.Height != altoTotal) layoutBloquesClientes.Height = altoTotal;
                layoutBloquesClientes.PerformLayout();
            }
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Label opcion = sender as Label;
            if (opcion != null && opcion.Tag != null)
                Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoCliente", chkCodigo.Checked);
            CambiarVisibilidad("Nombre", chkNombre.Checked);
            CambiarVisibilidad("Nit", chkNit.Checked);
            CambiarVisibilidad("CorreoElectronico", chkCorreoElectronico.Checked);
            CambiarVisibilidad("Telefono", chkTelefono.Checked);
            CambiarVisibilidad("Direccion", chkDireccion.Checked);
            CambiarVisibilidad("Departamento", chkDepartamento.Checked);
            CambiarVisibilidad("Municipio", chkMunicipio.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        // Usa anchos proporcionales y encabezados legibles en ambas fuentes de datos.
        private void ConfigurarColumnasGrilla()
        {
            Action actualizar = () =>
            {
                EstablecerEncabezadoCliente("NumeroFila", "N.º");
                EstablecerEncabezadoCliente("CodigoCliente", "Código");
                EstablecerEncabezadoCliente("Nombre", "Nombre");
                EstablecerEncabezadoCliente("Nit", "NIT");
                EstablecerEncabezadoCliente("CorreoElectronico", "Correo electrónico");
                EstablecerEncabezadoCliente("Telefono", "Teléfono");
                EstablecerEncabezadoCliente("Direccion", "Dirección");
                EstablecerEncabezadoCliente("Departamento", "Departamento");
                EstablecerEncabezadoCliente("Municipio", "Municipio");
                EstablecerEncabezadoCliente("Estado", "Estado");
                EstablecerAnchoColumnaCliente("NumeroFila", 45F);
                EstablecerAnchoColumnaCliente("CodigoCliente", 65F);
                EstablecerAnchoColumnaCliente("Nombre", 135F);
                EstablecerAnchoColumnaCliente("Nit", 78F);
                EstablecerAnchoColumnaCliente("CorreoElectronico", 150F);
                EstablecerAnchoColumnaCliente("Telefono", 82F);
                EstablecerAnchoColumnaCliente("Direccion", 130F);
                EstablecerAnchoColumnaCliente("Departamento", 95F);
                EstablecerAnchoColumnaCliente("Municipio", 95F);
                EstablecerAnchoColumnaCliente("Estado", 60F);

                if (dgvClientes.Columns.Contains("NumeroFila"))
                    dgvClientes.Columns["NumeroFila"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvClientes.Columns.Contains("CodigoCliente"))
                    dgvClientes.Columns["CodigoCliente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvClientes.Columns.Contains("Estado"))
                    dgvClientes.Columns["Estado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            };
            dgvClientes.DataBindingComplete += (s, e) => actualizar();
            dgvClientes.ColumnAdded += (s, e) => actualizar();
            actualizar();
        }

        private void EstablecerEncabezadoCliente(string nombre, string texto)
        {
            if (dgvClientes.Columns.Contains(nombre))
                dgvClientes.Columns[nombre].HeaderText = texto;
        }

        private void EstablecerAnchoColumnaCliente(string nombre, float peso)
        {
            if (!dgvClientes.Columns.Contains(nombre)) return;
            DataGridViewColumn columna = dgvClientes.Columns[nombre];
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.FillWeight = peso;
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvClientes.Columns.Contains(columna))
                dgvClientes.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvClientes.Rows.Count > 0)
                {
                    int indice = dgvClientes.CurrentRow == null ? 0 : dgvClientes.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvClientes.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvClientes.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvClientes.Rows.Count)
                return;

            dgvClientes.ClearSelection();

            DataGridViewCell celda = dgvClientes.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvClientes.CurrentCell = celda;

            dgvClientes.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            try
            {
                int codigo = negocio.Agregar(CrearEntidad());
                txtBuscar.Clear();
                CargarClientes();
                LimpiarFormulario();
                MessageBox.Show("Cliente agregado correctamente. Código: " + codigo,
                    "Agregar cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (!AjustesDisenoCrud.PermitirEdicion(this)) return;
            ClientesEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }

            if (!ValidarFormulario())
                return;

            try
            {
                // CodigoCliente es la PK real de dbo.Tbl_Clientes.
                seleccionado.CodigoCliente = Convert.ToInt32(txtCodigo.Text);
                seleccionado.Nombre = txtNombre.Text.Trim();
                seleccionado.Nit = ValorONulo(txtNit.Text);
                seleccionado.CorreoElectronico = ValorONulo(txtCorreoElectronico.Text);
                seleccionado.Telefono = txtTelefono.Text.Trim();
                seleccionado.Direccion = txtDireccion.Text.Trim();
                seleccionado.Departamento = cboDepartamento.SelectedItem as string;
                seleccionado.Municipio = cboMunicipio.SelectedItem as string;
                seleccionado.Estado = rdoActivo.Checked;

                negocio.Editar(seleccionado);
                CargarClientes();
                LimpiarFormulario();
                MessageBox.Show("Cliente actualizado correctamente.",
                    "Editar cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al editar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            ClientesEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Deseas marcar como inactivo al cliente " + seleccionado.CodigoCliente + "?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                // CodigoCliente identifica la fila; la eliminación conserva las relaciones.
                negocio.Eliminar(seleccionado.CodigoCliente);
                CargarClientes();
                LimpiarFormulario();
                MessageBox.Show("Cliente marcado como inactivo.",
                    "Eliminar cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ClientesEntidad CrearEntidad()
        {
            return new ClientesEntidad(
                txtNombre.Text.Trim(),
                ValorONulo(txtNit.Text),
                ValorONulo(txtCorreoElectronico.Text),
                txtTelefono.Text.Trim(),
                txtDireccion.Text.Trim(),
                cboDepartamento.SelectedItem as string,
                cboMunicipio.SelectedItem as string,
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
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

            if (txtNit.Text.Trim().Length > 45)
            {
                MessageBox.Show("El campo NIT no puede superar 45 caracteres.");
                txtNit.Focus();
                return false;
            }

            if (txtCorreoElectronico.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo Correo electrónico no puede superar 100 caracteres.");
                txtCorreoElectronico.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El campo Teléfono es obligatorio.");
                txtTelefono.Focus();
                return false;
            }

            if (txtTelefono.Text.Trim().Length > 20)
            {
                MessageBox.Show("El campo Teléfono no puede superar 20 caracteres.");
                txtTelefono.Focus();
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

            if (cboDepartamento.SelectedIndex < 0)
            {
                MessageBox.Show("El campo Departamento es obligatorio.");
                cboDepartamento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboMunicipio.SelectedItem as string))
            {
                MessageBox.Show("Selecciona un municipio para el departamento elegido.");
                cboMunicipio.Focus();
                return false;
            }

            return true;
        }

        private ClientesEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvClientes.CurrentRow == null)
                return null;

            return dgvClientes.CurrentRow.DataBoundItem as ClientesEntidad;
        }

        private void DgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            ClientesEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoCliente.ToString();
            txtNombre.Text = registro.Nombre;
            txtNit.Text = registro.Nit;
            txtCorreoElectronico.Text = registro.CorreoElectronico;
            txtTelefono.Text = registro.Telefono;
            txtDireccion.Text = registro.Direccion;
            cboDepartamento.SelectedItem = registro.Departamento;
            CargarMunicipios(registro.Departamento, registro.Municipio);
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
        }

        // La grilla usa AvPág y RePág para cambiar de página sin alterar el diseño.
        private void DgvClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
            {
                CambiarPagina(paginaActual + 1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                CambiarPagina(paginaActual - 1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
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

        private void BuscarClientes()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                AplicarFiltro();
                return;
            }

            try
            {
            clientesFiltrados = FiltrarPorEstado(negocio.Buscar(criterio)).ToList();
                paginaActual = 0;
                MostrarPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarClientes()
        {
            try
            {
                clientes = negocio.Listar();
                if (dashboard != null) CargarDashboardClientesAsync();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                clientes = new List<ClientesEntidad>();
                clientesFiltrados = new List<ClientesEntidad>();
                paginaActual = 0;
                MostrarPagina();
                MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarDashboardClientesAsync()
        {
            await dashboard.CargarAsync();
        }

        private void AplicarFiltro()
        {
            string criterio = txtBuscar.Text.Trim();
            IEnumerable<ClientesEntidad> consulta = clientes;

            if (criterio.Length > 0)
            {
                consulta = clientes.Where(r =>
                    r.CodigoCliente.ToString().Contains(criterio) ||
                    Contiene(r.Nombre, criterio) || Contiene(r.Nit, criterio) ||
                    Contiene(r.CorreoElectronico, criterio) || Contiene(r.Telefono, criterio) ||
                    Contiene(r.Direccion, criterio) || Contiene(r.Departamento, criterio) ||
                    Contiene(r.Municipio, criterio));
            }

            clientesFiltrados = FiltrarPorEstado(consulta).ToList();
            paginaActual = 0;
            MostrarPagina();
        }

        // Aplica Todos/Activos/Inactivos a los resultados del listado y de la búsqueda.
        private IEnumerable<ClientesEntidad> FiltrarPorEstado(IEnumerable<ClientesEntidad> registros)
        {
            ComboBox filtroEstado = Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault();
            if (filtroEstado == null || filtroEstado.SelectedIndex <= 0)
                return registros;

            bool estadoActivo = filtroEstado.SelectedIndex == 1;
            return registros.Where(registro => registro.Estado == estadoActivo);
        }

        private void MostrarPagina()
        {
            int paginas = (int)Math.Ceiling(clientesFiltrados.Count / (double)RegistrosPorPagina);
            if (paginas == 0)
                paginaActual = 0;
            else if (paginaActual >= paginas)
                paginaActual = paginas - 1;

            List<ClientesEntidad> registrosPagina = clientesFiltrados
                .Skip(paginaActual * RegistrosPorPagina)
                .Take(RegistrosPorPagina)
                .ToList();

            cargando = true;
            origen.DataSource = null;
            origen.DataSource = registrosPagina;
            dgvClientes.DataSource = origen;
            ActualizarColumnas();
            dgvClientes.ClearSelection();
            cargando = false;
            if (dgvClientes.Columns.Contains("NumeroFila"))
                dgvClientes.Columns.Remove("NumeroFila");
            DataGridViewTextBoxColumn numero = new DataGridViewTextBoxColumn
            {
                Name = "NumeroFila", HeaderText = "N.º", ReadOnly = true,
                Width = 52, SortMode = DataGridViewColumnSortMode.NotSortable
            };
            dgvClientes.Columns.Insert(0, numero);
            for (int i = 0; i < dgvClientes.Rows.Count; i++)
                dgvClientes.Rows[i].Cells["NumeroFila"].Value = i + 1;
            btnPaginaAnterior.Enabled = paginaActual > 0;
            btnPaginaSiguiente.Enabled = paginaActual < paginas - 1;
            ActualizarContador();
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            AjustarInterfazClientes();
        }

        private void CambiarPagina(int pagina)
        {
            int paginas = (int)Math.Ceiling(clientesFiltrados.Count / (double)RegistrosPorPagina);
            if (pagina < 0 || pagina >= paginas || pagina == paginaActual)
                return;

            paginaActual = pagina;
            MostrarPagina();
        }

        private void ActualizarContador()
        {
            int paginas = (int)Math.Ceiling(clientesFiltrados.Count / (double)RegistrosPorPagina);
            int paginaVisible = paginas == 0 ? 0 : paginaActual + 1;
            // Los totales cuentan todos los resultados, independientemente de la página visible.
            lblRegistros.Text = "Resultados: " + clientesFiltrados.Count +
                "     Total: " + clientes.Count;
            lblPagina.Text = "Página " + paginaVisible + " de " + paginas;
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            txtNombre.Clear();
            txtNit.Clear();
            txtCorreoElectronico.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            cboDepartamento.SelectedIndex = -1;
            CargarMunicipios(null, null);
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvClientes.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            txtNombre.Focus();
        }

        private void CargarDepartamentos()
        {
            cboDepartamento.Items.Clear();
            cboDepartamento.Items.AddRange(UbicacionesGuatemala.ObtenerDepartamentos());
            cboDepartamento.SelectedIndex = -1;
            CargarMunicipios(null, null);
        }

        private void CargarMunicipios(string departamento, string municipioSeleccionado)
        {
            cboMunicipio.BeginUpdate();
            cboMunicipio.Items.Clear();
            if (!string.IsNullOrWhiteSpace(departamento))
                cboMunicipio.Items.AddRange(UbicacionesGuatemala.ObtenerMunicipios(departamento));
            cboMunicipio.SelectedIndex = -1;
            cboMunicipio.Enabled = cboDepartamento.SelectedIndex >= 0;
            if (!string.IsNullOrWhiteSpace(municipioSeleccionado))
                cboMunicipio.SelectedItem = municipioSeleccionado;
            cboMunicipio.EndUpdate();
        }

    }
}
