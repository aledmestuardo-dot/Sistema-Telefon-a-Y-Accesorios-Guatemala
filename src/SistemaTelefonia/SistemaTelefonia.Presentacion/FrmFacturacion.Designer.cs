using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    partial class FrmFacturacion
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
        private Label lblCodigoVenta;
        private Label lblNumeroFactura;
        private Label lblFechaEmision;
        private Label lblNombreCliente;
        private Label lblNit;
        private Label lblTotal;
        private Label lblEstado;
        private TextBox txtCodigo;
        private TextBox txtNumeroFactura;
        private TextBox txtNombreCliente;
        private TextBox txtNit;
        private TextBox txtBuscar;
        private ComboBox cmbCodigoVenta;
        private NumericUpDown nudTotal;
        private DateTimePicker dtpFechaEmision;
        private RadioButton rdoActivo;
        private RadioButton rdoInactivo;
        private CheckBox chkSeleccionar;
        private CheckBox chkCodigo;
        private CheckBox chkCodigoVenta;
        private CheckBox chkNumeroFactura;
        private CheckBox chkFechaEmision;
        private CheckBox chkNombreCliente;
        private CheckBox chkNit;
        private CheckBox chkTotal;
        private CheckBox chkEstado;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnBuscar;
        private DataGridView dgvFacturacion;
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
            this.dgvFacturacion = new System.Windows.Forms.DataGridView();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.pnlColumnas = new System.Windows.Forms.Panel();
            this.selector = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSeleccionar = new System.Windows.Forms.CheckBox();
            this.chkCodigo = new System.Windows.Forms.CheckBox();
            this.chkCodigoVenta = new System.Windows.Forms.CheckBox();
            this.chkNumeroFactura = new System.Windows.Forms.CheckBox();
            this.chkFechaEmision = new System.Windows.Forms.CheckBox();
            this.chkNombreCliente = new System.Windows.Forms.CheckBox();
            this.chkNit = new System.Windows.Forms.CheckBox();
            this.chkTotal = new System.Windows.Forms.CheckBox();
            this.chkEstado = new System.Windows.Forms.CheckBox();
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
            this.lblCodigoVenta = new System.Windows.Forms.Label();
            this.cmbCodigoVenta = new System.Windows.Forms.ComboBox();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.txtNumeroFactura = new System.Windows.Forms.TextBox();
            this.lblFechaEmision = new System.Windows.Forms.Label();
            this.dtpFechaEmision = new System.Windows.Forms.DateTimePicker();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.lblNit = new System.Windows.Forms.Label();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.nudTotal = new System.Windows.Forms.NumericUpDown();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelEstado = new System.Windows.Forms.FlowLayoutPanel();
            this.rdoActivo = new System.Windows.Forms.RadioButton();
            this.rdoInactivo = new System.Windows.Forms.RadioButton();
            this.lblFormularioTitulo = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.pnlItemsMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlCatalogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturacion)).BeginInit();
            this.pnlColumnas.SuspendLayout();
            this.selector.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.layoutBotones.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.layoutCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).BeginInit();
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
            this.lblSubtitulo.Text = "Administra las facturas emitidas a partir de las ventas";
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
            this.lblTitulo.Text = "Gestión de facturación";
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
            this.pnlCatalogo.Controls.Add(this.dgvFacturacion);
            this.pnlCatalogo.Controls.Add(this.lblRegistros);
            this.pnlCatalogo.Controls.Add(this.pnlColumnas);
            this.pnlCatalogo.Controls.Add(this.pnlBusqueda);
            this.pnlCatalogo.Controls.Add(this.lblCatalogoTitulo);
            this.pnlCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCatalogo.Location = new System.Drawing.Point(15, 337);
            this.pnlCatalogo.Name = "pnlCatalogo";
            this.pnlCatalogo.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCatalogo.Size = new System.Drawing.Size(1003, 346);
            this.pnlCatalogo.TabIndex = 0;
            // 
            // dgvFacturacion
            // 
            this.dgvFacturacion.AllowUserToAddRows = false;
            this.dgvFacturacion.AllowUserToDeleteRows = false;
            this.dgvFacturacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFacturacion.BackgroundColor = System.Drawing.Color.White;
            this.dgvFacturacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFacturacion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFacturacion.ColumnHeadersHeight = 34;
            this.dgvFacturacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFacturacion.EnableHeadersVisualStyles = false;
            this.dgvFacturacion.Location = new System.Drawing.Point(10, 163);
            this.dgvFacturacion.MultiSelect = false;
            this.dgvFacturacion.Name = "dgvFacturacion";
            this.dgvFacturacion.ReadOnly = true;
            this.dgvFacturacion.RowHeadersVisible = false;
            this.dgvFacturacion.RowHeadersWidth = 62;
            this.dgvFacturacion.RowTemplate.Height = 29;
            this.dgvFacturacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturacion.Size = new System.Drawing.Size(983, 150);
            this.dgvFacturacion.TabIndex = 0;
            // 
            // lblRegistros
            // 
            this.lblRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblRegistros.Location = new System.Drawing.Point(10, 313);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(983, 23);
            this.lblRegistros.TabIndex = 1;
            this.lblRegistros.Text = "Total de facturas registradas: 0";
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
            this.selector.Controls.Add(this.chkCodigoVenta);
            this.selector.Controls.Add(this.chkNumeroFactura);
            this.selector.Controls.Add(this.chkFechaEmision);
            this.selector.Controls.Add(this.chkNombreCliente);
            this.selector.Controls.Add(this.chkNit);
            this.selector.Controls.Add(this.chkTotal);
            this.selector.Controls.Add(this.chkEstado);
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
            // chkCodigoVenta
            // 
            this.chkCodigoVenta.AutoSize = true;
            this.chkCodigoVenta.BackColor = System.Drawing.Color.Transparent;
            this.chkCodigoVenta.Checked = true;
            this.chkCodigoVenta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoVenta.ForeColor = System.Drawing.Color.White;
            this.chkCodigoVenta.Location = new System.Drawing.Point(256, 3);
            this.chkCodigoVenta.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkCodigoVenta.Name = "chkCodigoVenta";
            this.chkCodigoVenta.Size = new System.Drawing.Size(146, 29);
            this.chkCodigoVenta.TabIndex = 2;
            this.chkCodigoVenta.Text = "Codigo Venta";
            this.chkCodigoVenta.UseVisualStyleBackColor = false;
            // 
            // chkNumeroFactura
            // 
            this.chkNumeroFactura.AutoSize = true;
            this.chkNumeroFactura.BackColor = System.Drawing.Color.Transparent;
            this.chkNumeroFactura.Checked = true;
            this.chkNumeroFactura.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNumeroFactura.ForeColor = System.Drawing.Color.White;
            this.chkNumeroFactura.Location = new System.Drawing.Point(416, 3);
            this.chkNumeroFactura.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkNumeroFactura.Name = "chkNumeroFactura";
            this.chkNumeroFactura.Size = new System.Drawing.Size(164, 29);
            this.chkNumeroFactura.TabIndex = 3;
            this.chkNumeroFactura.Text = "Numero Factura";
            this.chkNumeroFactura.UseVisualStyleBackColor = false;
            // 
            // chkFechaEmision
            // 
            this.chkFechaEmision.AutoSize = true;
            this.chkFechaEmision.BackColor = System.Drawing.Color.Transparent;
            this.chkFechaEmision.Checked = true;
            this.chkFechaEmision.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFechaEmision.ForeColor = System.Drawing.Color.White;
            this.chkFechaEmision.Location = new System.Drawing.Point(594, 3);
            this.chkFechaEmision.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkFechaEmision.Name = "chkFechaEmision";
            this.chkFechaEmision.Size = new System.Drawing.Size(150, 29);
            this.chkFechaEmision.TabIndex = 4;
            this.chkFechaEmision.Text = "Fecha Emision";
            this.chkFechaEmision.UseVisualStyleBackColor = false;
            // 
            // chkNombreCliente
            // 
            this.chkNombreCliente.AutoSize = true;
            this.chkNombreCliente.BackColor = System.Drawing.Color.Transparent;
            this.chkNombreCliente.Checked = true;
            this.chkNombreCliente.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNombreCliente.ForeColor = System.Drawing.Color.White;
            this.chkNombreCliente.Location = new System.Drawing.Point(758, 3);
            this.chkNombreCliente.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkNombreCliente.Name = "chkNombreCliente";
            this.chkNombreCliente.Size = new System.Drawing.Size(162, 29);
            this.chkNombreCliente.TabIndex = 5;
            this.chkNombreCliente.Text = "Nombre Cliente";
            this.chkNombreCliente.UseVisualStyleBackColor = false;
            // 
            // chkNit
            // 
            this.chkNit.AutoSize = true;
            this.chkNit.BackColor = System.Drawing.Color.Transparent;
            this.chkNit.Checked = true;
            this.chkNit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNit.ForeColor = System.Drawing.Color.White;
            this.chkNit.Location = new System.Drawing.Point(5, 38);
            this.chkNit.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkNit.Name = "chkNit";
            this.chkNit.Size = new System.Drawing.Size(61, 29);
            this.chkNit.TabIndex = 6;
            this.chkNit.Text = "Nit";
            this.chkNit.UseVisualStyleBackColor = false;
            // 
            // chkTotal
            // 
            this.chkTotal.AutoSize = true;
            this.chkTotal.BackColor = System.Drawing.Color.Transparent;
            this.chkTotal.Checked = true;
            this.chkTotal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTotal.ForeColor = System.Drawing.Color.White;
            this.chkTotal.Location = new System.Drawing.Point(80, 38);
            this.chkTotal.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkTotal.Name = "chkTotal";
            this.chkTotal.Size = new System.Drawing.Size(75, 29);
            this.chkTotal.TabIndex = 7;
            this.chkTotal.Text = "Total";
            this.chkTotal.UseVisualStyleBackColor = false;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.BackColor = System.Drawing.Color.Transparent;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.ForeColor = System.Drawing.Color.White;
            this.chkEstado.Location = new System.Drawing.Point(169, 38);
            this.chkEstado.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(92, 29);
            this.chkEstado.TabIndex = 8;
            this.chkEstado.Text = "Estado";
            this.chkEstado.UseVisualStyleBackColor = false;
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
            this.lblCatalogoTitulo.Text = "Facturas registradas";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlAcciones.Controls.Add(this.layoutBotones);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAcciones.Location = new System.Drawing.Point(15, 275);
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
            this.pnlFormulario.Size = new System.Drawing.Size(1003, 260);
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
            this.layoutCampos.Controls.Add(this.lblCodigoVenta, 2, 0);
            this.layoutCampos.Controls.Add(this.cmbCodigoVenta, 3, 0);
            this.layoutCampos.Controls.Add(this.lblNumeroFactura, 0, 1);
            this.layoutCampos.Controls.Add(this.txtNumeroFactura, 1, 1);
            this.layoutCampos.Controls.Add(this.lblFechaEmision, 2, 1);
            this.layoutCampos.Controls.Add(this.dtpFechaEmision, 3, 1);
            this.layoutCampos.Controls.Add(this.lblNombreCliente, 0, 2);
            this.layoutCampos.Controls.Add(this.txtNombreCliente, 1, 2);
            this.layoutCampos.Controls.Add(this.lblNit, 2, 2);
            this.layoutCampos.Controls.Add(this.txtNit, 3, 2);
            this.layoutCampos.Controls.Add(this.lblTotal, 0, 3);
            this.layoutCampos.Controls.Add(this.nudTotal, 1, 3);
            this.layoutCampos.Controls.Add(this.lblEstado, 2, 3);
            this.layoutCampos.Controls.Add(this.panelEstado, 3, 3);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCampos.Location = new System.Drawing.Point(12, 44);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.RowCount = 4;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.Size = new System.Drawing.Size(979, 204);
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
            this.lblCodigo.Size = new System.Drawing.Size(226, 43);
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
            // lblCodigoVenta
            // 
            this.lblCodigoVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoVenta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoVenta.ForeColor = System.Drawing.Color.White;
            this.lblCodigoVenta.Location = new System.Drawing.Point(492, 4);
            this.lblCodigoVenta.Margin = new System.Windows.Forms.Padding(4);
            this.lblCodigoVenta.Name = "lblCodigoVenta";
            this.lblCodigoVenta.Size = new System.Drawing.Size(226, 43);
            this.lblCodigoVenta.TabIndex = 2;
            this.lblCodigoVenta.Text = "Cód. venta:";
            this.lblCodigoVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoVenta
            // 
            this.cmbCodigoVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoVenta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoVenta.Location = new System.Drawing.Point(726, 4);
            this.cmbCodigoVenta.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCodigoVenta.Name = "cmbCodigoVenta";
            this.cmbCodigoVenta.Size = new System.Drawing.Size(249, 36);
            this.cmbCodigoVenta.TabIndex = 3;
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumeroFactura.ForeColor = System.Drawing.Color.White;
            this.lblNumeroFactura.Location = new System.Drawing.Point(4, 55);
            this.lblNumeroFactura.Margin = new System.Windows.Forms.Padding(4);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(226, 43);
            this.lblNumeroFactura.TabIndex = 4;
            this.lblNumeroFactura.Text = "Número de factura:";
            this.lblNumeroFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNumeroFactura
            // 
            this.txtNumeroFactura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumeroFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumeroFactura.Location = new System.Drawing.Point(238, 55);
            this.txtNumeroFactura.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroFactura.Name = "txtNumeroFactura";
            this.txtNumeroFactura.Size = new System.Drawing.Size(246, 34);
            this.txtNumeroFactura.TabIndex = 5;
            // 
            // lblFechaEmision
            // 
            this.lblFechaEmision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaEmision.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaEmision.ForeColor = System.Drawing.Color.White;
            this.lblFechaEmision.Location = new System.Drawing.Point(492, 55);
            this.lblFechaEmision.Margin = new System.Windows.Forms.Padding(4);
            this.lblFechaEmision.Name = "lblFechaEmision";
            this.lblFechaEmision.Size = new System.Drawing.Size(226, 43);
            this.lblFechaEmision.TabIndex = 6;
            this.lblFechaEmision.Text = "Fecha de emisión:";
            this.lblFechaEmision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaEmision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaEmision.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaEmision.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaEmision.Location = new System.Drawing.Point(726, 55);
            this.dtpFechaEmision.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.Size = new System.Drawing.Size(249, 34);
            this.dtpFechaEmision.TabIndex = 7;
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombreCliente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombreCliente.ForeColor = System.Drawing.Color.White;
            this.lblNombreCliente.Location = new System.Drawing.Point(4, 106);
            this.lblNombreCliente.Margin = new System.Windows.Forms.Padding(4);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(226, 43);
            this.lblNombreCliente.TabIndex = 8;
            this.lblNombreCliente.Text = "Nombre del cliente:";
            this.lblNombreCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombreCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombreCliente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombreCliente.Location = new System.Drawing.Point(238, 106);
            this.txtNombreCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(246, 34);
            this.txtNombreCliente.TabIndex = 9;
            // 
            // lblNit
            // 
            this.lblNit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNit.ForeColor = System.Drawing.Color.White;
            this.lblNit.Location = new System.Drawing.Point(492, 106);
            this.lblNit.Margin = new System.Windows.Forms.Padding(4);
            this.lblNit.Name = "lblNit";
            this.lblNit.Size = new System.Drawing.Size(226, 43);
            this.lblNit.TabIndex = 10;
            this.lblNit.Text = "NIT:";
            this.lblNit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNit
            // 
            this.txtNit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNit.Location = new System.Drawing.Point(726, 106);
            this.txtNit.Margin = new System.Windows.Forms.Padding(4);
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(249, 34);
            this.txtNit.TabIndex = 11;
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(4, 157);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(226, 43);
            this.lblTotal.TabIndex = 12;
            this.lblTotal.Text = "Total:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudTotal
            // 
            this.nudTotal.DecimalPlaces = 2;
            this.nudTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudTotal.Location = new System.Drawing.Point(238, 157);
            this.nudTotal.Margin = new System.Windows.Forms.Padding(4);
            this.nudTotal.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudTotal.Name = "nudTotal";
            this.nudTotal.Size = new System.Drawing.Size(246, 34);
            this.nudTotal.TabIndex = 13;
            this.nudTotal.ThousandsSeparator = true;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.White;
            this.lblEstado.Location = new System.Drawing.Point(492, 157);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(226, 43);
            this.lblEstado.TabIndex = 14;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelEstado
            // 
            this.panelEstado.Controls.Add(this.rdoActivo);
            this.panelEstado.Controls.Add(this.rdoInactivo);
            this.panelEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstado.Location = new System.Drawing.Point(726, 157);
            this.panelEstado.Margin = new System.Windows.Forms.Padding(4);
            this.panelEstado.Name = "panelEstado";
            this.panelEstado.Size = new System.Drawing.Size(249, 43);
            this.panelEstado.TabIndex = 15;
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
            // lblFormularioTitulo
            // 
            this.lblFormularioTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormularioTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormularioTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblFormularioTitulo.Location = new System.Drawing.Point(12, 12);
            this.lblFormularioTitulo.Name = "lblFormularioTitulo";
            this.lblFormularioTitulo.Size = new System.Drawing.Size(979, 32);
            this.lblFormularioTitulo.TabIndex = 1;
            this.lblFormularioTitulo.Text = "Información de la factura";
            // 
            // FrmFacturacion
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
            this.Name = "FrmFacturacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Telefonía - Facturación";
            this.pnlMenu.ResumeLayout(false);
            this.pnlItemsMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturacion)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).EndInit();
            this.panelEstado.ResumeLayout(false);
            this.panelEstado.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
