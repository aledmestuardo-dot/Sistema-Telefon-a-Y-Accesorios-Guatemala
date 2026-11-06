using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    partial class FrmEmpleados
    {
        private IContainer components = null;
        private Panel pnlMenu;
        private Panel pnlLogo;
        private Panel pnlItemsMenu;
        private Panel pnlHeader;
        private Panel pnlContenido;
        private Panel pnlFormulario;
        private Panel pnlCatalogo;
        private Panel pnlBusqueda;
        private Panel pnlColumnas;
        private Label lblLogo;
        private Label lblSistema;
        private Label lblUsuario;
        private Label lblUsuarioRol;
        private Label lblMenuUsuarios;
        private Label lblMenuSucursales;
        private Label lblMenuEmpleados;
        private Label lblMenuClientes;
        private Label lblMenuPlanes;
        private Label lblMenuLineas;
        private Label lblMenuProveedores;
        private Label lblMenuMarcas;
        private Label lblMenuEquipos;
        private Label lblMenuCompras;
        private Label lblMenuDetalleCompra;
        private Label lblMenuInventario;
        private Label lblMenuVentas;
        private Label lblMenuDetalleVenta;
        private Label lblMenuFacturacion;
        private Label lblMenuDetalleFactura;
        private Label lblMenuPagos;
        private Label lblMenuCardex;
        private Label lblMenuReclamos;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblFormularioTitulo;
        private Label lblCatalogoTitulo;
        private Label lblRegistros;
        private Label lblColumnas;
        private Label lblCodigo;
        private Label lblCodigoSucursal;
        private Label lblCodigoUsuario;
        private Label lblNombreEmpleado;
        private Label lblFechaNacimiento;
        private Label lblSalario;
        private Label lblFechaContratacion;
        private Label lblTelefono;
        private Label lblObservaciones;
        private Label lblEstado;
        private Label lblDpi;
        private Label lblDireccion;
        private Label lblTipoEmpleado;
        private TextBox txtCodigo;
        private TextBox txtNombreEmpleado;
        private TextBox txtTelefono;
        private TextBox txtObservaciones;
        private TextBox txtDpi;
        private TextBox txtDireccion;
        private TextBox txtBuscar;
        private ComboBox cmbCodigoSucursal;
        private ComboBox cmbCodigoUsuario;
        private NumericUpDown nudSalario;
        private ComboBox cmbTipoEmpleado;
        private DateTimePicker dtpFechaNacimiento;
        private DateTimePicker dtpFechaContratacion;
        private RadioButton rdoActivo;
        private RadioButton rdoInactivo;
        private CheckBox chkSeleccionar;
        private CheckBox chkCodigo;
        private CheckBox chkCodigoSucursal;
        private CheckBox chkCodigoUsuario;
        private CheckBox chkNombreEmpleado;
        private CheckBox chkFechaNacimiento;
        private CheckBox chkSalario;
        private CheckBox chkFechaContratacion;
        private CheckBox chkTelefono;
        private CheckBox chkObservaciones;
        private CheckBox chkEstado;
        private CheckBox chkDpi;
        private CheckBox chkDireccion;
        private CheckBox chkTipoEmpleado;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnBuscar;
        private DataGridView dgvEmpleados;
        private TableLayoutPanel layoutCampos;
        private TableLayoutPanel layoutBotones;
        private Button btnVolverMenu;
        private Button btnCerrarSesion;
        private FlowLayoutPanel selector;
        private FlowLayoutPanel panelEstado;
        private Panel pnlAcciones;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlItemsMenu = new System.Windows.Forms.Panel();
            this.lblMenuUsuarios = new System.Windows.Forms.Label();
            this.lblMenuSucursales = new System.Windows.Forms.Label();
            this.lblMenuEmpleados = new System.Windows.Forms.Label();
            this.lblMenuClientes = new System.Windows.Forms.Label();
            this.lblMenuPlanes = new System.Windows.Forms.Label();
            this.lblMenuLineas = new System.Windows.Forms.Label();
            this.lblMenuProveedores = new System.Windows.Forms.Label();
            this.lblMenuMarcas = new System.Windows.Forms.Label();
            this.lblMenuEquipos = new System.Windows.Forms.Label();
            this.lblMenuCompras = new System.Windows.Forms.Label();
            this.lblMenuDetalleCompra = new System.Windows.Forms.Label();
            this.lblMenuInventario = new System.Windows.Forms.Label();
            this.lblMenuVentas = new System.Windows.Forms.Label();
            this.lblMenuDetalleVenta = new System.Windows.Forms.Label();
            this.lblMenuFacturacion = new System.Windows.Forms.Label();
            this.lblMenuDetalleFactura = new System.Windows.Forms.Label();
            this.lblMenuPagos = new System.Windows.Forms.Label();
            this.lblMenuCardex = new System.Windows.Forms.Label();
            this.lblMenuReclamos = new System.Windows.Forms.Label();
            this.btnVolverMenu = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblUsuarioRol = new System.Windows.Forms.Label();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblSistema = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlCatalogo = new System.Windows.Forms.Panel();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.pnlColumnas = new System.Windows.Forms.Panel();
            this.selector = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSeleccionar = new System.Windows.Forms.CheckBox();
            this.chkCodigo = new System.Windows.Forms.CheckBox();
            this.chkCodigoSucursal = new System.Windows.Forms.CheckBox();
            this.chkCodigoUsuario = new System.Windows.Forms.CheckBox();
            this.chkNombreEmpleado = new System.Windows.Forms.CheckBox();
            this.chkFechaNacimiento = new System.Windows.Forms.CheckBox();
            this.chkSalario = new System.Windows.Forms.CheckBox();
            this.chkFechaContratacion = new System.Windows.Forms.CheckBox();
            this.chkTelefono = new System.Windows.Forms.CheckBox();
            this.chkObservaciones = new System.Windows.Forms.CheckBox();
            this.chkEstado = new System.Windows.Forms.CheckBox();
            this.chkDpi = new System.Windows.Forms.CheckBox();
            this.chkDireccion = new System.Windows.Forms.CheckBox();
            this.chkTipoEmpleado = new System.Windows.Forms.CheckBox();
            this.lblColumnas = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblCatalogoTitulo = new System.Windows.Forms.Label();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.layoutBotones = new System.Windows.Forms.TableLayoutPanel();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.pnlFormulario = new System.Windows.Forms.Panel();
            this.layoutCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigoSucursal = new System.Windows.Forms.Label();
            this.cmbCodigoSucursal = new System.Windows.Forms.ComboBox();
            this.lblCodigoUsuario = new System.Windows.Forms.Label();
            this.cmbCodigoUsuario = new System.Windows.Forms.ComboBox();
            this.lblNombreEmpleado = new System.Windows.Forms.Label();
            this.txtNombreEmpleado = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblSalario = new System.Windows.Forms.Label();
            this.nudSalario = new System.Windows.Forms.NumericUpDown();
            this.lblFechaContratacion = new System.Windows.Forms.Label();
            this.dtpFechaContratacion = new System.Windows.Forms.DateTimePicker();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelEstado = new System.Windows.Forms.FlowLayoutPanel();
            this.rdoActivo = new System.Windows.Forms.RadioButton();
            this.rdoInactivo = new System.Windows.Forms.RadioButton();
            this.lblDpi = new System.Windows.Forms.Label();
            this.txtDpi = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblTipoEmpleado = new System.Windows.Forms.Label();
            this.cmbTipoEmpleado = new System.Windows.Forms.ComboBox();
            this.lblFormularioTitulo = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.pnlItemsMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlCatalogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.pnlColumnas.SuspendLayout();
            this.selector.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.layoutBotones.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.layoutCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSalario)).BeginInit();
            this.panelEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.pnlMenu.Controls.Add(this.pnlItemsMenu);
            this.pnlMenu.Controls.Add(this.btnVolverMenu);
            this.pnlMenu.Controls.Add(this.btnCerrarSesion);
            this.pnlMenu.Controls.Add(this.lblUsuario);
            this.pnlMenu.Controls.Add(this.lblUsuarioRol);
            this.pnlMenu.Controls.Add(this.pnlLogo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(8);
            this.pnlMenu.Size = new System.Drawing.Size(195, 794);
            this.pnlMenu.TabIndex = 2;
            // 
            // pnlItemsMenu
            // 
            this.pnlItemsMenu.AutoScroll = true;
            this.pnlItemsMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.pnlItemsMenu.Controls.Add(this.lblMenuUsuarios);
            this.pnlItemsMenu.Controls.Add(this.lblMenuSucursales);
            this.pnlItemsMenu.Controls.Add(this.lblMenuEmpleados);
            this.pnlItemsMenu.Controls.Add(this.lblMenuClientes);
            this.pnlItemsMenu.Controls.Add(this.lblMenuPlanes);
            this.pnlItemsMenu.Controls.Add(this.lblMenuLineas);
            this.pnlItemsMenu.Controls.Add(this.lblMenuProveedores);
            this.pnlItemsMenu.Controls.Add(this.lblMenuMarcas);
            this.pnlItemsMenu.Controls.Add(this.lblMenuEquipos);
            this.pnlItemsMenu.Controls.Add(this.lblMenuCompras);
            this.pnlItemsMenu.Controls.Add(this.lblMenuDetalleCompra);
            this.pnlItemsMenu.Controls.Add(this.lblMenuInventario);
            this.pnlItemsMenu.Controls.Add(this.lblMenuVentas);
            this.pnlItemsMenu.Controls.Add(this.lblMenuDetalleVenta);
            this.pnlItemsMenu.Controls.Add(this.lblMenuFacturacion);
            this.pnlItemsMenu.Controls.Add(this.lblMenuDetalleFactura);
            this.pnlItemsMenu.Controls.Add(this.lblMenuPagos);
            this.pnlItemsMenu.Controls.Add(this.lblMenuCardex);
            this.pnlItemsMenu.Controls.Add(this.lblMenuReclamos);
            this.pnlItemsMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlItemsMenu.Location = new System.Drawing.Point(8, 184);
            this.pnlItemsMenu.Name = "pnlItemsMenu";
            this.pnlItemsMenu.Size = new System.Drawing.Size(179, 514);
            this.pnlItemsMenu.TabIndex = 0;
            // 
            // lblMenuUsuarios
            // 
            this.lblMenuUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblMenuUsuarios.Location = new System.Drawing.Point(0, 0);
            this.lblMenuUsuarios.Name = "lblMenuUsuarios";
            this.lblMenuUsuarios.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuUsuarios.Size = new System.Drawing.Size(160, 34);
            this.lblMenuUsuarios.TabIndex = 0;
            this.lblMenuUsuarios.Tag = "FrmUsuarios";
            this.lblMenuUsuarios.Text = "›  Usuarios";
            this.lblMenuUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuSucursales
            // 
            this.lblMenuSucursales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuSucursales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuSucursales.ForeColor = System.Drawing.Color.White;
            this.lblMenuSucursales.Location = new System.Drawing.Point(0, 36);
            this.lblMenuSucursales.Name = "lblMenuSucursales";
            this.lblMenuSucursales.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuSucursales.Size = new System.Drawing.Size(160, 34);
            this.lblMenuSucursales.TabIndex = 1;
            this.lblMenuSucursales.Tag = "FrmSucursales";
            this.lblMenuSucursales.Text = "›  Sucursales";
            this.lblMenuSucursales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuEmpleados
            // 
            this.lblMenuEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuEmpleados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuEmpleados.ForeColor = System.Drawing.Color.White;
            this.lblMenuEmpleados.Location = new System.Drawing.Point(0, 72);
            this.lblMenuEmpleados.Name = "lblMenuEmpleados";
            this.lblMenuEmpleados.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuEmpleados.Size = new System.Drawing.Size(160, 34);
            this.lblMenuEmpleados.TabIndex = 2;
            this.lblMenuEmpleados.Tag = "FrmEmpleados";
            this.lblMenuEmpleados.Text = "›  Empleados";
            this.lblMenuEmpleados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuClientes
            // 
            this.lblMenuClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuClientes.ForeColor = System.Drawing.Color.White;
            this.lblMenuClientes.Location = new System.Drawing.Point(0, 108);
            this.lblMenuClientes.Name = "lblMenuClientes";
            this.lblMenuClientes.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuClientes.Size = new System.Drawing.Size(160, 34);
            this.lblMenuClientes.TabIndex = 3;
            this.lblMenuClientes.Tag = "FrmClientes";
            this.lblMenuClientes.Text = "›  Clientes";
            this.lblMenuClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuPlanes
            // 
            this.lblMenuPlanes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuPlanes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuPlanes.ForeColor = System.Drawing.Color.White;
            this.lblMenuPlanes.Location = new System.Drawing.Point(0, 144);
            this.lblMenuPlanes.Name = "lblMenuPlanes";
            this.lblMenuPlanes.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuPlanes.Size = new System.Drawing.Size(160, 34);
            this.lblMenuPlanes.TabIndex = 4;
            this.lblMenuPlanes.Tag = "FrmPlanes";
            this.lblMenuPlanes.Text = "›  Planes";
            this.lblMenuPlanes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuLineas
            // 
            this.lblMenuLineas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuLineas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuLineas.ForeColor = System.Drawing.Color.White;
            this.lblMenuLineas.Location = new System.Drawing.Point(0, 180);
            this.lblMenuLineas.Name = "lblMenuLineas";
            this.lblMenuLineas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuLineas.Size = new System.Drawing.Size(160, 34);
            this.lblMenuLineas.TabIndex = 5;
            this.lblMenuLineas.Tag = "FrmLineas";
            this.lblMenuLineas.Text = "›  Líneas";
            this.lblMenuLineas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuProveedores
            // 
            this.lblMenuProveedores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuProveedores.ForeColor = System.Drawing.Color.White;
            this.lblMenuProveedores.Location = new System.Drawing.Point(0, 216);
            this.lblMenuProveedores.Name = "lblMenuProveedores";
            this.lblMenuProveedores.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuProveedores.Size = new System.Drawing.Size(160, 34);
            this.lblMenuProveedores.TabIndex = 6;
            this.lblMenuProveedores.Tag = "FrmProveedores";
            this.lblMenuProveedores.Text = "›  Proveedores";
            this.lblMenuProveedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuMarcas
            // 
            this.lblMenuMarcas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuMarcas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuMarcas.ForeColor = System.Drawing.Color.White;
            this.lblMenuMarcas.Location = new System.Drawing.Point(0, 252);
            this.lblMenuMarcas.Name = "lblMenuMarcas";
            this.lblMenuMarcas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuMarcas.Size = new System.Drawing.Size(160, 34);
            this.lblMenuMarcas.TabIndex = 7;
            this.lblMenuMarcas.Tag = "FrmMarcas";
            this.lblMenuMarcas.Text = "›  Marcas";
            this.lblMenuMarcas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuEquipos
            // 
            this.lblMenuEquipos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuEquipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuEquipos.ForeColor = System.Drawing.Color.White;
            this.lblMenuEquipos.Location = new System.Drawing.Point(0, 288);
            this.lblMenuEquipos.Name = "lblMenuEquipos";
            this.lblMenuEquipos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuEquipos.Size = new System.Drawing.Size(160, 34);
            this.lblMenuEquipos.TabIndex = 8;
            this.lblMenuEquipos.Tag = "FrmEquipos";
            this.lblMenuEquipos.Text = "›  Equipos";
            this.lblMenuEquipos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuCompras
            // 
            this.lblMenuCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuCompras.ForeColor = System.Drawing.Color.White;
            this.lblMenuCompras.Location = new System.Drawing.Point(0, 324);
            this.lblMenuCompras.Name = "lblMenuCompras";
            this.lblMenuCompras.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuCompras.Size = new System.Drawing.Size(160, 34);
            this.lblMenuCompras.TabIndex = 9;
            this.lblMenuCompras.Tag = "FrmCompras";
            this.lblMenuCompras.Text = "›  Compras";
            this.lblMenuCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleCompra
            // 
            this.lblMenuDetalleCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuDetalleCompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuDetalleCompra.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleCompra.Location = new System.Drawing.Point(0, 360);
            this.lblMenuDetalleCompra.Name = "lblMenuDetalleCompra";
            this.lblMenuDetalleCompra.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuDetalleCompra.Size = new System.Drawing.Size(160, 34);
            this.lblMenuDetalleCompra.TabIndex = 10;
            this.lblMenuDetalleCompra.Tag = "FrmDetalleCompra";
            this.lblMenuDetalleCompra.Text = "›  Detalle de compra";
            this.lblMenuDetalleCompra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuInventario
            // 
            this.lblMenuInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuInventario.ForeColor = System.Drawing.Color.White;
            this.lblMenuInventario.Location = new System.Drawing.Point(0, 396);
            this.lblMenuInventario.Name = "lblMenuInventario";
            this.lblMenuInventario.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuInventario.Size = new System.Drawing.Size(160, 34);
            this.lblMenuInventario.TabIndex = 11;
            this.lblMenuInventario.Tag = "FrmInventario";
            this.lblMenuInventario.Text = "›  Inventario";
            this.lblMenuInventario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuVentas
            // 
            this.lblMenuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuVentas.ForeColor = System.Drawing.Color.White;
            this.lblMenuVentas.Location = new System.Drawing.Point(0, 432);
            this.lblMenuVentas.Name = "lblMenuVentas";
            this.lblMenuVentas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuVentas.Size = new System.Drawing.Size(160, 34);
            this.lblMenuVentas.TabIndex = 12;
            this.lblMenuVentas.Tag = "FrmVentas";
            this.lblMenuVentas.Text = "›  Ventas";
            this.lblMenuVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleVenta
            // 
            this.lblMenuDetalleVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuDetalleVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuDetalleVenta.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleVenta.Location = new System.Drawing.Point(0, 468);
            this.lblMenuDetalleVenta.Name = "lblMenuDetalleVenta";
            this.lblMenuDetalleVenta.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuDetalleVenta.Size = new System.Drawing.Size(160, 34);
            this.lblMenuDetalleVenta.TabIndex = 13;
            this.lblMenuDetalleVenta.Tag = "FrmDetalleVenta";
            this.lblMenuDetalleVenta.Text = "›  Detalle de venta";
            this.lblMenuDetalleVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuFacturacion
            // 
            this.lblMenuFacturacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuFacturacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuFacturacion.ForeColor = System.Drawing.Color.White;
            this.lblMenuFacturacion.Location = new System.Drawing.Point(0, 504);
            this.lblMenuFacturacion.Name = "lblMenuFacturacion";
            this.lblMenuFacturacion.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuFacturacion.Size = new System.Drawing.Size(160, 34);
            this.lblMenuFacturacion.TabIndex = 14;
            this.lblMenuFacturacion.Tag = "FrmFacturacion";
            this.lblMenuFacturacion.Text = "›  Facturación";
            this.lblMenuFacturacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleFactura
            // 
            this.lblMenuDetalleFactura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuDetalleFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuDetalleFactura.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleFactura.Location = new System.Drawing.Point(0, 540);
            this.lblMenuDetalleFactura.Name = "lblMenuDetalleFactura";
            this.lblMenuDetalleFactura.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuDetalleFactura.Size = new System.Drawing.Size(160, 34);
            this.lblMenuDetalleFactura.TabIndex = 15;
            this.lblMenuDetalleFactura.Tag = "FrmDetalleFactura";
            this.lblMenuDetalleFactura.Text = "›  Detalle de factura";
            this.lblMenuDetalleFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuPagos
            // 
            this.lblMenuPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuPagos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuPagos.ForeColor = System.Drawing.Color.White;
            this.lblMenuPagos.Location = new System.Drawing.Point(0, 576);
            this.lblMenuPagos.Name = "lblMenuPagos";
            this.lblMenuPagos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuPagos.Size = new System.Drawing.Size(160, 34);
            this.lblMenuPagos.TabIndex = 16;
            this.lblMenuPagos.Tag = "FrmPagos";
            this.lblMenuPagos.Text = "›  Pagos";
            this.lblMenuPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuCardex
            // 
            this.lblMenuCardex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuCardex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuCardex.ForeColor = System.Drawing.Color.White;
            this.lblMenuCardex.Location = new System.Drawing.Point(0, 612);
            this.lblMenuCardex.Name = "lblMenuCardex";
            this.lblMenuCardex.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuCardex.Size = new System.Drawing.Size(160, 34);
            this.lblMenuCardex.TabIndex = 17;
            this.lblMenuCardex.Tag = "FrmCardex";
            this.lblMenuCardex.Text = "›  Cardex";
            this.lblMenuCardex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuReclamos
            // 
            this.lblMenuReclamos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuReclamos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuReclamos.ForeColor = System.Drawing.Color.White;
            this.lblMenuReclamos.Location = new System.Drawing.Point(0, 648);
            this.lblMenuReclamos.Name = "lblMenuReclamos";
            this.lblMenuReclamos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuReclamos.Size = new System.Drawing.Size(160, 34);
            this.lblMenuReclamos.TabIndex = 18;
            this.lblMenuReclamos.Tag = "FrmReclamos";
            this.lblMenuReclamos.Text = "›  Reclamos";
            this.lblMenuReclamos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.btnVolverMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVolverMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolverMenu.ForeColor = System.Drawing.Color.White;
            this.btnVolverMenu.Location = new System.Drawing.Point(8, 146);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(179, 38);
            this.btnVolverMenu.TabIndex = 1;
            this.btnVolverMenu.Text = "⌂  Volver al menú";
            this.btnVolverMenu.UseVisualStyleBackColor = false;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(8, 698);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(179, 38);
            this.btnCerrarSesion.TabIndex = 5;
            this.btnCerrarSesion.Text = "↪  Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(8, 736);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(179, 25);
            this.lblUsuario.TabIndex = 4;
            this.lblUsuario.Text = "Administrador";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsuarioRol
            // 
            this.lblUsuarioRol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblUsuarioRol.Location = new System.Drawing.Point(8, 761);
            this.lblUsuarioRol.Name = "lblUsuarioRol";
            this.lblUsuarioRol.Size = new System.Drawing.Size(179, 25);
            this.lblUsuarioRol.TabIndex = 3;
            this.lblUsuarioRol.Text = "Usuario del sistema";
            this.lblUsuarioRol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.pnlLogo.Controls.Add(this.lblSistema);
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(8, 8);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(179, 138);
            this.pnlLogo.TabIndex = 2;
            // 
            // lblSistema
            // 
            this.lblSistema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSistema.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSistema.ForeColor = System.Drawing.Color.White;
            this.lblSistema.Location = new System.Drawing.Point(0, 55);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(179, 83);
            this.lblSistema.TabIndex = 0;
            this.lblSistema.Text = "SISTEMA\r\n\r\nTELEFONÍA";
            this.lblSistema.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(179, 55);
            this.lblLogo.TabIndex = 1;
            this.lblLogo.Text = "ST";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.pnlHeader.Controls.Add(this.lblSubtitulo);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(195, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(22, 8, 15, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1033, 96);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(22, 50);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(996, 38);
            this.lblSubtitulo.TabIndex = 0;
            this.lblSubtitulo.Text = "Administra el personal de la empresa";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(996, 42);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Gestión de empleados";
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlContenido.Controls.Add(this.pnlCatalogo);
            this.pnlContenido.Controls.Add(this.pnlAcciones);
            this.pnlContenido.Controls.Add(this.pnlFormulario);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(195, 96);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(15);
            this.pnlContenido.Size = new System.Drawing.Size(1033, 698);
            this.pnlContenido.TabIndex = 0;
            // 
            // pnlCatalogo
            // 
            this.pnlCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.pnlCatalogo.Controls.Add(this.dgvEmpleados);
            this.pnlCatalogo.Controls.Add(this.lblRegistros);
            this.pnlCatalogo.Controls.Add(this.pnlColumnas);
            this.pnlCatalogo.Controls.Add(this.pnlBusqueda);
            this.pnlCatalogo.Controls.Add(this.lblCatalogoTitulo);
            this.pnlCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCatalogo.Location = new System.Drawing.Point(15, 457);
            this.pnlCatalogo.Name = "pnlCatalogo";
            this.pnlCatalogo.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCatalogo.Size = new System.Drawing.Size(1003, 226);
            this.pnlCatalogo.TabIndex = 0;
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmpleados.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmpleados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpleados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmpleados.ColumnHeadersHeight = 34;
            this.dgvEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpleados.EnableHeadersVisualStyles = false;
            this.dgvEmpleados.Location = new System.Drawing.Point(10, 163);
            this.dgvEmpleados.MultiSelect = false;
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.ReadOnly = true;
            this.dgvEmpleados.RowHeadersVisible = false;
            this.dgvEmpleados.RowHeadersWidth = 62;
            this.dgvEmpleados.RowTemplate.Height = 29;
            this.dgvEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpleados.Size = new System.Drawing.Size(983, 30);
            this.dgvEmpleados.TabIndex = 0;
            // 
            // lblRegistros
            // 
            this.lblRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblRegistros.Location = new System.Drawing.Point(10, 193);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(983, 23);
            this.lblRegistros.TabIndex = 1;
            this.lblRegistros.Text = "Total de empleados registrados: 0";
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlColumnas
            // 
            this.pnlColumnas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.pnlColumnas.Controls.Add(this.selector);
            this.pnlColumnas.Controls.Add(this.lblColumnas);
            this.pnlColumnas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColumnas.Location = new System.Drawing.Point(10, 78);
            this.pnlColumnas.Name = "pnlColumnas";
            this.pnlColumnas.Size = new System.Drawing.Size(983, 85);
            this.pnlColumnas.TabIndex = 2;
            // 
            // selector
            // 
            this.selector.AutoScroll = true;
            this.selector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.selector.Controls.Add(this.chkSeleccionar);
            this.selector.Controls.Add(this.chkCodigo);
            this.selector.Controls.Add(this.chkCodigoSucursal);
            this.selector.Controls.Add(this.chkCodigoUsuario);
            this.selector.Controls.Add(this.chkNombreEmpleado);
            this.selector.Controls.Add(this.chkFechaNacimiento);
            this.selector.Controls.Add(this.chkSalario);
            this.selector.Controls.Add(this.chkFechaContratacion);
            this.selector.Controls.Add(this.chkTelefono);
            this.selector.Controls.Add(this.chkObservaciones);
            this.selector.Controls.Add(this.chkEstado);
            this.selector.Controls.Add(this.chkDpi);
            this.selector.Controls.Add(this.chkDireccion);
            this.selector.Controls.Add(this.chkTipoEmpleado);
            this.selector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selector.Location = new System.Drawing.Point(0, 23);
            this.selector.Name = "selector";
            this.selector.Size = new System.Drawing.Size(983, 62);
            this.selector.TabIndex = 0;
            // 
            // chkSeleccionar
            // 
            this.chkSeleccionar.AutoSize = true;
            this.chkSeleccionar.BackColor = System.Drawing.Color.Transparent;
            this.chkSeleccionar.ForeColor = System.Drawing.Color.White;
            this.chkSeleccionar.Location = new System.Drawing.Point(5, 3);
            this.chkSeleccionar.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkSeleccionar.Name = "chkSeleccionar";
            this.chkSeleccionar.Size = new System.Drawing.Size(126, 29);
            this.chkSeleccionar.TabIndex = 0;
            this.chkSeleccionar.Text = "Seleccionar";
            this.chkSeleccionar.UseVisualStyleBackColor = false;
            // 
            // chkCodigo
            // 
            this.chkCodigo.AutoSize = true;
            this.chkCodigo.BackColor = System.Drawing.Color.Transparent;
            this.chkCodigo.Checked = true;
            this.chkCodigo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigo.ForeColor = System.Drawing.Color.White;
            this.chkCodigo.Location = new System.Drawing.Point(145, 3);
            this.chkCodigo.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkCodigo.Name = "chkCodigo";
            this.chkCodigo.Size = new System.Drawing.Size(97, 29);
            this.chkCodigo.TabIndex = 1;
            this.chkCodigo.Text = "Codigo";
            this.chkCodigo.UseVisualStyleBackColor = false;
            // 
            // chkCodigoSucursal
            // 
            this.chkCodigoSucursal.AutoSize = true;
            this.chkCodigoSucursal.BackColor = System.Drawing.Color.Transparent;
            this.chkCodigoSucursal.Checked = true;
            this.chkCodigoSucursal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoSucursal.ForeColor = System.Drawing.Color.White;
            this.chkCodigoSucursal.Location = new System.Drawing.Point(256, 3);
            this.chkCodigoSucursal.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkCodigoSucursal.Name = "chkCodigoSucursal";
            this.chkCodigoSucursal.Size = new System.Drawing.Size(167, 29);
            this.chkCodigoSucursal.TabIndex = 2;
            this.chkCodigoSucursal.Text = "Codigo Sucursal";
            this.chkCodigoSucursal.UseVisualStyleBackColor = false;
            // 
            // chkCodigoUsuario
            // 
            this.chkCodigoUsuario.AutoSize = true;
            this.chkCodigoUsuario.BackColor = System.Drawing.Color.Transparent;
            this.chkCodigoUsuario.Checked = true;
            this.chkCodigoUsuario.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoUsuario.ForeColor = System.Drawing.Color.White;
            this.chkCodigoUsuario.Location = new System.Drawing.Point(437, 3);
            this.chkCodigoUsuario.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkCodigoUsuario.Name = "chkCodigoUsuario";
            this.chkCodigoUsuario.Size = new System.Drawing.Size(162, 29);
            this.chkCodigoUsuario.TabIndex = 3;
            this.chkCodigoUsuario.Text = "Codigo Usuario";
            this.chkCodigoUsuario.UseVisualStyleBackColor = false;
            // 
            // chkNombreEmpleado
            // 
            this.chkNombreEmpleado.AutoSize = true;
            this.chkNombreEmpleado.BackColor = System.Drawing.Color.Transparent;
            this.chkNombreEmpleado.Checked = true;
            this.chkNombreEmpleado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNombreEmpleado.ForeColor = System.Drawing.Color.White;
            this.chkNombreEmpleado.Location = new System.Drawing.Point(613, 3);
            this.chkNombreEmpleado.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkNombreEmpleado.Name = "chkNombreEmpleado";
            this.chkNombreEmpleado.Size = new System.Drawing.Size(189, 29);
            this.chkNombreEmpleado.TabIndex = 4;
            this.chkNombreEmpleado.Text = "Nombre Empleado";
            this.chkNombreEmpleado.UseVisualStyleBackColor = false;
            // 
            // chkFechaNacimiento
            // 
            this.chkFechaNacimiento.AutoSize = true;
            this.chkFechaNacimiento.BackColor = System.Drawing.Color.Transparent;
            this.chkFechaNacimiento.Checked = true;
            this.chkFechaNacimiento.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFechaNacimiento.ForeColor = System.Drawing.Color.White;
            this.chkFechaNacimiento.Location = new System.Drawing.Point(5, 38);
            this.chkFechaNacimiento.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkFechaNacimiento.Name = "chkFechaNacimiento";
            this.chkFechaNacimiento.Size = new System.Drawing.Size(178, 29);
            this.chkFechaNacimiento.TabIndex = 5;
            this.chkFechaNacimiento.Text = "Fecha Nacimiento";
            this.chkFechaNacimiento.UseVisualStyleBackColor = false;
            // 
            // chkSalario
            // 
            this.chkSalario.AutoSize = true;
            this.chkSalario.BackColor = System.Drawing.Color.Transparent;
            this.chkSalario.Checked = true;
            this.chkSalario.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSalario.ForeColor = System.Drawing.Color.White;
            this.chkSalario.Location = new System.Drawing.Point(197, 38);
            this.chkSalario.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkSalario.Name = "chkSalario";
            this.chkSalario.Size = new System.Drawing.Size(91, 29);
            this.chkSalario.TabIndex = 6;
            this.chkSalario.Text = "Salario";
            this.chkSalario.UseVisualStyleBackColor = false;
            // 
            // chkFechaContratacion
            // 
            this.chkFechaContratacion.AutoSize = true;
            this.chkFechaContratacion.BackColor = System.Drawing.Color.Transparent;
            this.chkFechaContratacion.Checked = true;
            this.chkFechaContratacion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFechaContratacion.ForeColor = System.Drawing.Color.White;
            this.chkFechaContratacion.Location = new System.Drawing.Point(302, 38);
            this.chkFechaContratacion.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkFechaContratacion.Name = "chkFechaContratacion";
            this.chkFechaContratacion.Size = new System.Drawing.Size(189, 29);
            this.chkFechaContratacion.TabIndex = 7;
            this.chkFechaContratacion.Text = "Fecha Contratacion";
            this.chkFechaContratacion.UseVisualStyleBackColor = false;
            // 
            // chkTelefono
            // 
            this.chkTelefono.AutoSize = true;
            this.chkTelefono.BackColor = System.Drawing.Color.Transparent;
            this.chkTelefono.Checked = true;
            this.chkTelefono.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTelefono.ForeColor = System.Drawing.Color.White;
            this.chkTelefono.Location = new System.Drawing.Point(505, 38);
            this.chkTelefono.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkTelefono.Name = "chkTelefono";
            this.chkTelefono.Size = new System.Drawing.Size(105, 29);
            this.chkTelefono.TabIndex = 8;
            this.chkTelefono.Text = "Telefono";
            this.chkTelefono.UseVisualStyleBackColor = false;
            // 
            // chkObservaciones
            // 
            this.chkObservaciones.AutoSize = true;
            this.chkObservaciones.BackColor = System.Drawing.Color.Transparent;
            this.chkObservaciones.Checked = true;
            this.chkObservaciones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkObservaciones.ForeColor = System.Drawing.Color.White;
            this.chkObservaciones.Location = new System.Drawing.Point(624, 38);
            this.chkObservaciones.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkObservaciones.Name = "chkObservaciones";
            this.chkObservaciones.Size = new System.Drawing.Size(154, 29);
            this.chkObservaciones.TabIndex = 9;
            this.chkObservaciones.Text = "Observaciones";
            this.chkObservaciones.UseVisualStyleBackColor = false;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.BackColor = System.Drawing.Color.Transparent;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.ForeColor = System.Drawing.Color.White;
            this.chkEstado.Location = new System.Drawing.Point(792, 38);
            this.chkEstado.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(92, 29);
            this.chkEstado.TabIndex = 10;
            this.chkEstado.Text = "Estado";
            this.chkEstado.UseVisualStyleBackColor = false;
            // 
            // chkDpi
            // 
            this.chkDpi.AutoSize = true;
            this.chkDpi.BackColor = System.Drawing.Color.Transparent;
            this.chkDpi.Checked = true;
            this.chkDpi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDpi.ForeColor = System.Drawing.Color.White;
            this.chkDpi.Location = new System.Drawing.Point(5, 73);
            this.chkDpi.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkDpi.Name = "chkDpi";
            this.chkDpi.Size = new System.Drawing.Size(66, 29);
            this.chkDpi.TabIndex = 11;
            this.chkDpi.Text = "Dpi";
            this.chkDpi.UseVisualStyleBackColor = false;
            // 
            // chkDireccion
            // 
            this.chkDireccion.AutoSize = true;
            this.chkDireccion.BackColor = System.Drawing.Color.Transparent;
            this.chkDireccion.Checked = true;
            this.chkDireccion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDireccion.ForeColor = System.Drawing.Color.White;
            this.chkDireccion.Location = new System.Drawing.Point(85, 73);
            this.chkDireccion.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkDireccion.Name = "chkDireccion";
            this.chkDireccion.Size = new System.Drawing.Size(111, 29);
            this.chkDireccion.TabIndex = 12;
            this.chkDireccion.Text = "Direccion";
            this.chkDireccion.UseVisualStyleBackColor = false;
            // 
            // chkTipoEmpleado
            // 
            this.chkTipoEmpleado.AutoSize = true;
            this.chkTipoEmpleado.BackColor = System.Drawing.Color.Transparent;
            this.chkTipoEmpleado.Checked = true;
            this.chkTipoEmpleado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTipoEmpleado.ForeColor = System.Drawing.Color.White;
            this.chkTipoEmpleado.Location = new System.Drawing.Point(210, 73);
            this.chkTipoEmpleado.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkTipoEmpleado.Name = "chkTipoEmpleado";
            this.chkTipoEmpleado.Size = new System.Drawing.Size(158, 29);
            this.chkTipoEmpleado.TabIndex = 13;
            this.chkTipoEmpleado.Text = "Tipo Empleado";
            this.chkTipoEmpleado.UseVisualStyleBackColor = false;
            // 
            // lblColumnas
            // 
            this.lblColumnas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblColumnas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblColumnas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblColumnas.Location = new System.Drawing.Point(0, 0);
            this.lblColumnas.Name = "lblColumnas";
            this.lblColumnas.Size = new System.Drawing.Size(983, 23);
            this.lblColumnas.TabIndex = 1;
            this.lblColumnas.Text = "Opciones de visualización:";
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.txtBuscar);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusqueda.Location = new System.Drawing.Point(10, 40);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(983, 38);
            this.pnlBusqueda.TabIndex = 3;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Location = new System.Drawing.Point(0, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(901, 31);
            this.txtBuscar.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.btnBuscar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(901, 0);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(82, 38);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // lblCatalogoTitulo
            // 
            this.lblCatalogoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCatalogoTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCatalogoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblCatalogoTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblCatalogoTitulo.Name = "lblCatalogoTitulo";
            this.lblCatalogoTitulo.Size = new System.Drawing.Size(983, 30);
            this.lblCatalogoTitulo.TabIndex = 4;
            this.lblCatalogoTitulo.Text = "Empleados registrados";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlAcciones.Controls.Add(this.layoutBotones);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAcciones.Location = new System.Drawing.Point(15, 395);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(1003, 62);
            this.pnlAcciones.TabIndex = 1;
            // 
            // layoutBotones
            // 
            this.layoutBotones.ColumnCount = 5;
            this.layoutBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutBotones.Controls.Add(this.btnNuevo, 0, 0);
            this.layoutBotones.Controls.Add(this.btnGuardar, 1, 0);
            this.layoutBotones.Controls.Add(this.btnEditar, 2, 0);
            this.layoutBotones.Controls.Add(this.btnEliminar, 3, 0);
            this.layoutBotones.Controls.Add(this.btnLimpiar, 4, 0);
            this.layoutBotones.Location = new System.Drawing.Point(51, 4);
            this.layoutBotones.Name = "layoutBotones";
            this.layoutBotones.RowCount = 1;
            this.layoutBotones.Size = new System.Drawing.Size(900, 54);
            this.layoutBotones.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnNuevo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(3, 3);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(174, 48);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(110)))));
            this.btnGuardar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(183, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(174, 48);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(145)))), ((int)(((byte)(30)))));
            this.btnEditar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(363, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(174, 48);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.btnEliminar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(543, 3);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(174, 48);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(100)))), ((int)(((byte)(125)))));
            this.btnLimpiar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(723, 3);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(174, 48);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // pnlFormulario
            // 
            this.pnlFormulario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.pnlFormulario.Controls.Add(this.layoutCampos);
            this.pnlFormulario.Controls.Add(this.lblFormularioTitulo);
            this.pnlFormulario.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFormulario.Location = new System.Drawing.Point(15, 15);
            this.pnlFormulario.Name = "pnlFormulario";
            this.pnlFormulario.Padding = new System.Windows.Forms.Padding(12);
            this.pnlFormulario.Size = new System.Drawing.Size(1003, 380);
            this.pnlFormulario.TabIndex = 2;
            // 
            // layoutCampos
            // 
            this.layoutCampos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.layoutCampos.ColumnCount = 4;
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.layoutCampos.Controls.Add(this.lblCodigo, 0, 0);
            this.layoutCampos.Controls.Add(this.txtCodigo, 1, 0);
            this.layoutCampos.Controls.Add(this.lblCodigoSucursal, 2, 0);
            this.layoutCampos.Controls.Add(this.cmbCodigoSucursal, 3, 0);
            this.layoutCampos.Controls.Add(this.lblCodigoUsuario, 0, 1);
            this.layoutCampos.Controls.Add(this.cmbCodigoUsuario, 1, 1);
            this.layoutCampos.Controls.Add(this.lblNombreEmpleado, 2, 1);
            this.layoutCampos.Controls.Add(this.txtNombreEmpleado, 3, 1);
            this.layoutCampos.Controls.Add(this.lblFechaNacimiento, 0, 2);
            this.layoutCampos.Controls.Add(this.dtpFechaNacimiento, 1, 2);
            this.layoutCampos.Controls.Add(this.lblSalario, 2, 2);
            this.layoutCampos.Controls.Add(this.nudSalario, 3, 2);
            this.layoutCampos.Controls.Add(this.lblFechaContratacion, 0, 3);
            this.layoutCampos.Controls.Add(this.dtpFechaContratacion, 1, 3);
            this.layoutCampos.Controls.Add(this.lblTelefono, 2, 3);
            this.layoutCampos.Controls.Add(this.txtTelefono, 3, 3);
            this.layoutCampos.Controls.Add(this.lblObservaciones, 0, 4);
            this.layoutCampos.Controls.Add(this.txtObservaciones, 1, 4);
            this.layoutCampos.Controls.Add(this.lblEstado, 2, 4);
            this.layoutCampos.Controls.Add(this.panelEstado, 3, 4);
            this.layoutCampos.Controls.Add(this.lblDpi, 0, 5);
            this.layoutCampos.Controls.Add(this.txtDpi, 1, 5);
            this.layoutCampos.Controls.Add(this.lblDireccion, 2, 5);
            this.layoutCampos.Controls.Add(this.txtDireccion, 3, 5);
            this.layoutCampos.Controls.Add(this.lblTipoEmpleado, 0, 6);
            this.layoutCampos.Controls.Add(this.cmbTipoEmpleado, 1, 6);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCampos.Location = new System.Drawing.Point(12, 44);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.RowCount = 7;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.286F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.286F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.286F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.286F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.286F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.286F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.286F));
            this.layoutCampos.Size = new System.Drawing.Size(979, 324);
            this.layoutCampos.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.White;
            this.lblCodigo.Location = new System.Drawing.Point(4, 4);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(226, 38);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(231)))), ((int)(((byte)(241)))));
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCodigo.Location = new System.Drawing.Point(238, 4);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(246, 34);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblCodigoSucursal
            // 
            this.lblCodigoSucursal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoSucursal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoSucursal.ForeColor = System.Drawing.Color.White;
            this.lblCodigoSucursal.Location = new System.Drawing.Point(492, 4);
            this.lblCodigoSucursal.Margin = new System.Windows.Forms.Padding(4);
            this.lblCodigoSucursal.Name = "lblCodigoSucursal";
            this.lblCodigoSucursal.Size = new System.Drawing.Size(226, 38);
            this.lblCodigoSucursal.TabIndex = 2;
            this.lblCodigoSucursal.Text = "Cód. sucursal:";
            this.lblCodigoSucursal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoSucursal
            // 
            this.cmbCodigoSucursal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoSucursal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoSucursal.Location = new System.Drawing.Point(726, 4);
            this.cmbCodigoSucursal.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCodigoSucursal.Name = "cmbCodigoSucursal";
            this.cmbCodigoSucursal.Size = new System.Drawing.Size(249, 36);
            this.cmbCodigoSucursal.TabIndex = 3;
            // 
            // lblCodigoUsuario
            // 
            this.lblCodigoUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoUsuario.ForeColor = System.Drawing.Color.White;
            this.lblCodigoUsuario.Location = new System.Drawing.Point(4, 50);
            this.lblCodigoUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.lblCodigoUsuario.Name = "lblCodigoUsuario";
            this.lblCodigoUsuario.Size = new System.Drawing.Size(226, 38);
            this.lblCodigoUsuario.TabIndex = 4;
            this.lblCodigoUsuario.Text = "Cód. usuario:";
            this.lblCodigoUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoUsuario
            // 
            this.cmbCodigoUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoUsuario.Location = new System.Drawing.Point(238, 50);
            this.cmbCodigoUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCodigoUsuario.Name = "cmbCodigoUsuario";
            this.cmbCodigoUsuario.Size = new System.Drawing.Size(246, 36);
            this.cmbCodigoUsuario.TabIndex = 5;
            // 
            // lblNombreEmpleado
            // 
            this.lblNombreEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombreEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombreEmpleado.ForeColor = System.Drawing.Color.White;
            this.lblNombreEmpleado.Location = new System.Drawing.Point(492, 50);
            this.lblNombreEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.lblNombreEmpleado.Name = "lblNombreEmpleado";
            this.lblNombreEmpleado.Size = new System.Drawing.Size(226, 38);
            this.lblNombreEmpleado.TabIndex = 6;
            this.lblNombreEmpleado.Text = "Nombre del empleado:";
            this.lblNombreEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNombreEmpleado
            // 
            this.txtNombreEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombreEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombreEmpleado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombreEmpleado.Location = new System.Drawing.Point(726, 50);
            this.txtNombreEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombreEmpleado.Name = "txtNombreEmpleado";
            this.txtNombreEmpleado.Size = new System.Drawing.Size(249, 34);
            this.txtNombreEmpleado.TabIndex = 7;
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.White;
            this.lblFechaNacimiento.Location = new System.Drawing.Point(4, 96);
            this.lblFechaNacimiento.Margin = new System.Windows.Forms.Padding(4);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(226, 38);
            this.lblFechaNacimiento.TabIndex = 8;
            this.lblFechaNacimiento.Text = "Fecha de nacimiento:";
            this.lblFechaNacimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaNacimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(238, 96);
            this.dtpFechaNacimiento.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(246, 34);
            this.dtpFechaNacimiento.TabIndex = 9;
            // 
            // lblSalario
            // 
            this.lblSalario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSalario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalario.ForeColor = System.Drawing.Color.White;
            this.lblSalario.Location = new System.Drawing.Point(492, 96);
            this.lblSalario.Margin = new System.Windows.Forms.Padding(4);
            this.lblSalario.Name = "lblSalario";
            this.lblSalario.Size = new System.Drawing.Size(226, 38);
            this.lblSalario.TabIndex = 10;
            this.lblSalario.Text = "Salario:";
            this.lblSalario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudSalario
            // 
            this.nudSalario.DecimalPlaces = 2;
            this.nudSalario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudSalario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudSalario.Location = new System.Drawing.Point(726, 96);
            this.nudSalario.Margin = new System.Windows.Forms.Padding(4);
            this.nudSalario.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudSalario.Name = "nudSalario";
            this.nudSalario.Size = new System.Drawing.Size(249, 34);
            this.nudSalario.TabIndex = 11;
            this.nudSalario.ThousandsSeparator = true;
            // 
            // lblFechaContratacion
            // 
            this.lblFechaContratacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaContratacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaContratacion.ForeColor = System.Drawing.Color.White;
            this.lblFechaContratacion.Location = new System.Drawing.Point(4, 142);
            this.lblFechaContratacion.Margin = new System.Windows.Forms.Padding(4);
            this.lblFechaContratacion.Name = "lblFechaContratacion";
            this.lblFechaContratacion.Size = new System.Drawing.Size(226, 38);
            this.lblFechaContratacion.TabIndex = 12;
            this.lblFechaContratacion.Text = "Fecha de contratación:";
            this.lblFechaContratacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaContratacion
            // 
            this.dtpFechaContratacion.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaContratacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaContratacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaContratacion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaContratacion.Location = new System.Drawing.Point(238, 142);
            this.dtpFechaContratacion.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaContratacion.Name = "dtpFechaContratacion";
            this.dtpFechaContratacion.Size = new System.Drawing.Size(246, 34);
            this.dtpFechaContratacion.TabIndex = 13;
            // 
            // lblTelefono
            // 
            this.lblTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTelefono.ForeColor = System.Drawing.Color.White;
            this.lblTelefono.Location = new System.Drawing.Point(492, 142);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(4);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(226, 38);
            this.lblTelefono.TabIndex = 14;
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTelefono
            // 
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTelefono.Location = new System.Drawing.Point(726, 142);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(4);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(249, 34);
            this.txtTelefono.TabIndex = 15;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblObservaciones.ForeColor = System.Drawing.Color.White;
            this.lblObservaciones.Location = new System.Drawing.Point(4, 188);
            this.lblObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(226, 38);
            this.lblObservaciones.TabIndex = 16;
            this.lblObservaciones.Text = "Observaciones:";
            this.lblObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtObservaciones.Location = new System.Drawing.Point(238, 188);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(246, 34);
            this.txtObservaciones.TabIndex = 17;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.White;
            this.lblEstado.Location = new System.Drawing.Point(492, 188);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(226, 38);
            this.lblEstado.TabIndex = 18;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelEstado
            // 
            this.panelEstado.Controls.Add(this.rdoActivo);
            this.panelEstado.Controls.Add(this.rdoInactivo);
            this.panelEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstado.Location = new System.Drawing.Point(726, 188);
            this.panelEstado.Margin = new System.Windows.Forms.Padding(4);
            this.panelEstado.Name = "panelEstado";
            this.panelEstado.Size = new System.Drawing.Size(249, 38);
            this.panelEstado.TabIndex = 19;
            this.panelEstado.WrapContents = false;
            // 
            // rdoActivo
            // 
            this.rdoActivo.AutoSize = true;
            this.rdoActivo.Checked = true;
            this.rdoActivo.ForeColor = System.Drawing.Color.White;
            this.rdoActivo.Location = new System.Drawing.Point(3, 3);
            this.rdoActivo.Name = "rdoActivo";
            this.rdoActivo.Size = new System.Drawing.Size(87, 29);
            this.rdoActivo.TabIndex = 0;
            this.rdoActivo.TabStop = true;
            this.rdoActivo.Text = "Activo";
            // 
            // rdoInactivo
            // 
            this.rdoInactivo.AutoSize = true;
            this.rdoInactivo.ForeColor = System.Drawing.Color.White;
            this.rdoInactivo.Location = new System.Drawing.Point(96, 3);
            this.rdoInactivo.Name = "rdoInactivo";
            this.rdoInactivo.Size = new System.Drawing.Size(99, 29);
            this.rdoInactivo.TabIndex = 1;
            this.rdoInactivo.Text = "Inactivo";
            // 
            // lblDpi
            // 
            this.lblDpi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDpi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDpi.ForeColor = System.Drawing.Color.White;
            this.lblDpi.Location = new System.Drawing.Point(4, 234);
            this.lblDpi.Margin = new System.Windows.Forms.Padding(4);
            this.lblDpi.Name = "lblDpi";
            this.lblDpi.Size = new System.Drawing.Size(226, 38);
            this.lblDpi.TabIndex = 20;
            this.lblDpi.Text = "DPI:";
            this.lblDpi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDpi
            // 
            this.txtDpi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDpi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDpi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDpi.Location = new System.Drawing.Point(238, 234);
            this.txtDpi.Margin = new System.Windows.Forms.Padding(4);
            this.txtDpi.Name = "txtDpi";
            this.txtDpi.Size = new System.Drawing.Size(246, 34);
            this.txtDpi.TabIndex = 21;
            // 
            // lblDireccion
            // 
            this.lblDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDireccion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDireccion.ForeColor = System.Drawing.Color.White;
            this.lblDireccion.Location = new System.Drawing.Point(492, 234);
            this.lblDireccion.Margin = new System.Windows.Forms.Padding(4);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(226, 38);
            this.lblDireccion.TabIndex = 22;
            this.lblDireccion.Text = "Dirección:";
            this.lblDireccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDireccion
            // 
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDireccion.Location = new System.Drawing.Point(726, 234);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(4);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(249, 34);
            this.txtDireccion.TabIndex = 23;
            // 
            // lblTipoEmpleado
            // 
            this.lblTipoEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTipoEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTipoEmpleado.ForeColor = System.Drawing.Color.White;
            this.lblTipoEmpleado.Location = new System.Drawing.Point(4, 280);
            this.lblTipoEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.lblTipoEmpleado.Name = "lblTipoEmpleado";
            this.lblTipoEmpleado.Size = new System.Drawing.Size(226, 40);
            this.lblTipoEmpleado.TabIndex = 24;
            this.lblTipoEmpleado.Text = "Tipo de empleado:";
            this.lblTipoEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTipoEmpleado
            // 
            this.cmbTipoEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTipoEmpleado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoEmpleado.Items.AddRange(new object[] {
            "Administrador",
            "Vendedor",
            "Bodeguero",
            "Gerente"});
            this.cmbTipoEmpleado.Location = new System.Drawing.Point(238, 280);
            this.cmbTipoEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTipoEmpleado.Name = "cmbTipoEmpleado";
            this.cmbTipoEmpleado.Size = new System.Drawing.Size(246, 36);
            this.cmbTipoEmpleado.TabIndex = 25;
            // 
            // lblFormularioTitulo
            // 
            this.lblFormularioTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormularioTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormularioTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblFormularioTitulo.Location = new System.Drawing.Point(12, 12);
            this.lblFormularioTitulo.Name = "lblFormularioTitulo";
            this.lblFormularioTitulo.Size = new System.Drawing.Size(979, 32);
            this.lblFormularioTitulo.TabIndex = 1;
            this.lblFormularioTitulo.Text = "Información del empleado";
            // 
            // FrmEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1228, 794);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 700);
            this.Name = "FrmEmpleados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Telefonía - Empleados";
            this.pnlMenu.ResumeLayout(false);
            this.pnlItemsMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.pnlColumnas.ResumeLayout(false);
            this.selector.ResumeLayout(false);
            this.selector.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.layoutBotones.ResumeLayout(false);
            this.pnlFormulario.ResumeLayout(false);
            this.layoutCampos.ResumeLayout(false);
            this.layoutCampos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSalario)).EndInit();
            this.panelEstado.ResumeLayout(false);
            this.panelEstado.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
