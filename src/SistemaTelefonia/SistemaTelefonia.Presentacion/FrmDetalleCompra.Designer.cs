using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    partial class FrmDetalleCompra
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
        private Label lblCodigoCompra;
        private Label lblCodigoEquipo;
        private Label lblCantidad;
        private Label lblCostoUnitario;
        private Label lblSubtotal;
        private Label lblObservaciones;
        private Label lblEstado;
        private TextBox txtCodigo;
        private TextBox txtObservaciones;
        private TextBox txtBuscar;
        private ComboBox cmbCodigoCompra;
        private ComboBox cmbCodigoEquipo;
        private NumericUpDown nudCantidad;
        private NumericUpDown nudCostoUnitario;
        private NumericUpDown nudSubtotal;
        private RadioButton rdoActivo;
        private RadioButton rdoInactivo;
        private CheckBox chkSeleccionar;
        private CheckBox chkCodigo;
        private CheckBox chkCodigoCompra;
        private CheckBox chkCodigoEquipo;
        private CheckBox chkCantidad;
        private CheckBox chkCostoUnitario;
        private CheckBox chkSubtotal;
        private CheckBox chkObservaciones;
        private CheckBox chkEstado;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnBuscar;
        private DataGridView dgvDetalleCompra;
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
            this.dgvDetalleCompra = new System.Windows.Forms.DataGridView();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.pnlColumnas = new System.Windows.Forms.Panel();
            this.selector = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSeleccionar = new System.Windows.Forms.CheckBox();
            this.chkCodigo = new System.Windows.Forms.CheckBox();
            this.chkCodigoCompra = new System.Windows.Forms.CheckBox();
            this.chkCodigoEquipo = new System.Windows.Forms.CheckBox();
            this.chkCantidad = new System.Windows.Forms.CheckBox();
            this.chkCostoUnitario = new System.Windows.Forms.CheckBox();
            this.chkSubtotal = new System.Windows.Forms.CheckBox();
            this.chkObservaciones = new System.Windows.Forms.CheckBox();
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
            this.lblCodigoCompra = new System.Windows.Forms.Label();
            this.cmbCodigoCompra = new System.Windows.Forms.ComboBox();
            this.lblCodigoEquipo = new System.Windows.Forms.Label();
            this.cmbCodigoEquipo = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblCostoUnitario = new System.Windows.Forms.Label();
            this.nudCostoUnitario = new System.Windows.Forms.NumericUpDown();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.nudSubtotal = new System.Windows.Forms.NumericUpDown();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleCompra)).BeginInit();
            this.pnlColumnas.SuspendLayout();
            this.selector.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.layoutBotones.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.layoutCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCostoUnitario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubtotal)).BeginInit();
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
            this.lblSubtitulo.Text = "Administra los equipos incluidos en cada compra";
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
            this.lblTitulo.Text = "Gestión de detalle de compras";
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
            this.pnlCatalogo.Controls.Add(this.dgvDetalleCompra);
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
            // dgvDetalleCompra
            // 
            this.dgvDetalleCompra.AllowUserToAddRows = false;
            this.dgvDetalleCompra.AllowUserToDeleteRows = false;
            this.dgvDetalleCompra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleCompra.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalleCompra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalleCompra.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetalleCompra.ColumnHeadersHeight = 34;
            this.dgvDetalleCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalleCompra.EnableHeadersVisualStyles = false;
            this.dgvDetalleCompra.Location = new System.Drawing.Point(10, 163);
            this.dgvDetalleCompra.MultiSelect = false;
            this.dgvDetalleCompra.Name = "dgvDetalleCompra";
            this.dgvDetalleCompra.ReadOnly = true;
            this.dgvDetalleCompra.RowHeadersVisible = false;
            this.dgvDetalleCompra.RowHeadersWidth = 62;
            this.dgvDetalleCompra.RowTemplate.Height = 29;
            this.dgvDetalleCompra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleCompra.Size = new System.Drawing.Size(983, 150);
            this.dgvDetalleCompra.TabIndex = 0;
            // 
            // lblRegistros
            // 
            this.lblRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblRegistros.Location = new System.Drawing.Point(10, 313);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(983, 23);
            this.lblRegistros.TabIndex = 1;
            this.lblRegistros.Text = "Total de detalles de compra registrados: 0";
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
            this.selector.Controls.Add(this.chkCodigoCompra);
            this.selector.Controls.Add(this.chkCodigoEquipo);
            this.selector.Controls.Add(this.chkCantidad);
            this.selector.Controls.Add(this.chkCostoUnitario);
            this.selector.Controls.Add(this.chkSubtotal);
            this.selector.Controls.Add(this.chkObservaciones);
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
            // chkCodigoCompra
            // 
            this.chkCodigoCompra.AutoSize = true;
            this.chkCodigoCompra.BackColor = System.Drawing.Color.Transparent;
            this.chkCodigoCompra.Checked = true;
            this.chkCodigoCompra.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoCompra.ForeColor = System.Drawing.Color.White;
            this.chkCodigoCompra.Location = new System.Drawing.Point(256, 3);
            this.chkCodigoCompra.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkCodigoCompra.Name = "chkCodigoCompra";
            this.chkCodigoCompra.Size = new System.Drawing.Size(166, 29);
            this.chkCodigoCompra.TabIndex = 2;
            this.chkCodigoCompra.Text = "Codigo Compra";
            this.chkCodigoCompra.UseVisualStyleBackColor = false;
            // 
            // chkCodigoEquipo
            // 
            this.chkCodigoEquipo.AutoSize = true;
            this.chkCodigoEquipo.BackColor = System.Drawing.Color.Transparent;
            this.chkCodigoEquipo.Checked = true;
            this.chkCodigoEquipo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoEquipo.ForeColor = System.Drawing.Color.White;
            this.chkCodigoEquipo.Location = new System.Drawing.Point(436, 3);
            this.chkCodigoEquipo.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkCodigoEquipo.Name = "chkCodigoEquipo";
            this.chkCodigoEquipo.Size = new System.Drawing.Size(158, 29);
            this.chkCodigoEquipo.TabIndex = 3;
            this.chkCodigoEquipo.Text = "Codigo Equipo";
            this.chkCodigoEquipo.UseVisualStyleBackColor = false;
            // 
            // chkCantidad
            // 
            this.chkCantidad.AutoSize = true;
            this.chkCantidad.BackColor = System.Drawing.Color.Transparent;
            this.chkCantidad.Checked = true;
            this.chkCantidad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCantidad.ForeColor = System.Drawing.Color.White;
            this.chkCantidad.Location = new System.Drawing.Point(608, 3);
            this.chkCantidad.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkCantidad.Name = "chkCantidad";
            this.chkCantidad.Size = new System.Drawing.Size(109, 29);
            this.chkCantidad.TabIndex = 4;
            this.chkCantidad.Text = "Cantidad";
            this.chkCantidad.UseVisualStyleBackColor = false;
            // 
            // chkCostoUnitario
            // 
            this.chkCostoUnitario.AutoSize = true;
            this.chkCostoUnitario.BackColor = System.Drawing.Color.Transparent;
            this.chkCostoUnitario.Checked = true;
            this.chkCostoUnitario.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCostoUnitario.ForeColor = System.Drawing.Color.White;
            this.chkCostoUnitario.Location = new System.Drawing.Point(731, 3);
            this.chkCostoUnitario.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkCostoUnitario.Name = "chkCostoUnitario";
            this.chkCostoUnitario.Size = new System.Drawing.Size(152, 29);
            this.chkCostoUnitario.TabIndex = 5;
            this.chkCostoUnitario.Text = "Costo Unitario";
            this.chkCostoUnitario.UseVisualStyleBackColor = false;
            // 
            // chkSubtotal
            // 
            this.chkSubtotal.AutoSize = true;
            this.chkSubtotal.BackColor = System.Drawing.Color.Transparent;
            this.chkSubtotal.Checked = true;
            this.chkSubtotal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSubtotal.ForeColor = System.Drawing.Color.White;
            this.chkSubtotal.Location = new System.Drawing.Point(5, 38);
            this.chkSubtotal.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkSubtotal.Name = "chkSubtotal";
            this.chkSubtotal.Size = new System.Drawing.Size(105, 29);
            this.chkSubtotal.TabIndex = 6;
            this.chkSubtotal.Text = "Subtotal";
            this.chkSubtotal.UseVisualStyleBackColor = false;
            // 
            // chkObservaciones
            // 
            this.chkObservaciones.AutoSize = true;
            this.chkObservaciones.BackColor = System.Drawing.Color.Transparent;
            this.chkObservaciones.Checked = true;
            this.chkObservaciones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkObservaciones.ForeColor = System.Drawing.Color.White;
            this.chkObservaciones.Location = new System.Drawing.Point(124, 38);
            this.chkObservaciones.Margin = new System.Windows.Forms.Padding(5, 3, 9, 3);
            this.chkObservaciones.Name = "chkObservaciones";
            this.chkObservaciones.Size = new System.Drawing.Size(154, 29);
            this.chkObservaciones.TabIndex = 7;
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
            this.chkEstado.Location = new System.Drawing.Point(292, 38);
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
            this.lblCatalogoTitulo.Text = "Detalles de compra registrados";
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
            this.layoutCampos.Controls.Add(this.lblCodigoCompra, 2, 0);
            this.layoutCampos.Controls.Add(this.cmbCodigoCompra, 3, 0);
            this.layoutCampos.Controls.Add(this.lblCodigoEquipo, 0, 1);
            this.layoutCampos.Controls.Add(this.cmbCodigoEquipo, 1, 1);
            this.layoutCampos.Controls.Add(this.lblCantidad, 2, 1);
            this.layoutCampos.Controls.Add(this.nudCantidad, 3, 1);
            this.layoutCampos.Controls.Add(this.lblCostoUnitario, 0, 2);
            this.layoutCampos.Controls.Add(this.nudCostoUnitario, 1, 2);
            this.layoutCampos.Controls.Add(this.lblSubtotal, 2, 2);
            this.layoutCampos.Controls.Add(this.nudSubtotal, 3, 2);
            this.layoutCampos.Controls.Add(this.lblObservaciones, 0, 3);
            this.layoutCampos.Controls.Add(this.txtObservaciones, 1, 3);
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
            // lblCodigoCompra
            // 
            this.lblCodigoCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoCompra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoCompra.ForeColor = System.Drawing.Color.White;
            this.lblCodigoCompra.Location = new System.Drawing.Point(492, 4);
            this.lblCodigoCompra.Margin = new System.Windows.Forms.Padding(4);
            this.lblCodigoCompra.Name = "lblCodigoCompra";
            this.lblCodigoCompra.Size = new System.Drawing.Size(226, 43);
            this.lblCodigoCompra.TabIndex = 2;
            this.lblCodigoCompra.Text = "Cód. compra:";
            this.lblCodigoCompra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoCompra
            // 
            this.cmbCodigoCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoCompra.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoCompra.Location = new System.Drawing.Point(726, 4);
            this.cmbCodigoCompra.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCodigoCompra.Name = "cmbCodigoCompra";
            this.cmbCodigoCompra.Size = new System.Drawing.Size(249, 36);
            this.cmbCodigoCompra.TabIndex = 3;
            // 
            // lblCodigoEquipo
            // 
            this.lblCodigoEquipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoEquipo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoEquipo.ForeColor = System.Drawing.Color.White;
            this.lblCodigoEquipo.Location = new System.Drawing.Point(4, 55);
            this.lblCodigoEquipo.Margin = new System.Windows.Forms.Padding(4);
            this.lblCodigoEquipo.Name = "lblCodigoEquipo";
            this.lblCodigoEquipo.Size = new System.Drawing.Size(226, 43);
            this.lblCodigoEquipo.TabIndex = 4;
            this.lblCodigoEquipo.Text = "Cód. equipo:";
            this.lblCodigoEquipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoEquipo
            // 
            this.cmbCodigoEquipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoEquipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoEquipo.Location = new System.Drawing.Point(238, 55);
            this.cmbCodigoEquipo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCodigoEquipo.Name = "cmbCodigoEquipo";
            this.cmbCodigoEquipo.Size = new System.Drawing.Size(246, 36);
            this.cmbCodigoEquipo.TabIndex = 5;
            // 
            // lblCantidad
            // 
            this.lblCantidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.ForeColor = System.Drawing.Color.White;
            this.lblCantidad.Location = new System.Drawing.Point(492, 55);
            this.lblCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(226, 43);
            this.lblCantidad.TabIndex = 6;
            this.lblCantidad.Text = "Cantidad:";
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudCantidad
            // 
            this.nudCantidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCantidad.Location = new System.Drawing.Point(726, 55);
            this.nudCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.nudCantidad.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(249, 34);
            this.nudCantidad.TabIndex = 7;
            this.nudCantidad.ThousandsSeparator = true;
            // 
            // lblCostoUnitario
            // 
            this.lblCostoUnitario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCostoUnitario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCostoUnitario.ForeColor = System.Drawing.Color.White;
            this.lblCostoUnitario.Location = new System.Drawing.Point(4, 106);
            this.lblCostoUnitario.Margin = new System.Windows.Forms.Padding(4);
            this.lblCostoUnitario.Name = "lblCostoUnitario";
            this.lblCostoUnitario.Size = new System.Drawing.Size(226, 43);
            this.lblCostoUnitario.TabIndex = 8;
            this.lblCostoUnitario.Text = "Costo unitario:";
            this.lblCostoUnitario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudCostoUnitario
            // 
            this.nudCostoUnitario.DecimalPlaces = 2;
            this.nudCostoUnitario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudCostoUnitario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCostoUnitario.Location = new System.Drawing.Point(238, 106);
            this.nudCostoUnitario.Margin = new System.Windows.Forms.Padding(4);
            this.nudCostoUnitario.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudCostoUnitario.Name = "nudCostoUnitario";
            this.nudCostoUnitario.Size = new System.Drawing.Size(246, 34);
            this.nudCostoUnitario.TabIndex = 9;
            this.nudCostoUnitario.ThousandsSeparator = true;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.ForeColor = System.Drawing.Color.White;
            this.lblSubtotal.Location = new System.Drawing.Point(492, 106);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(4);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(226, 43);
            this.lblSubtotal.TabIndex = 10;
            this.lblSubtotal.Text = "Subtotal:";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudSubtotal
            // 
            this.nudSubtotal.DecimalPlaces = 2;
            this.nudSubtotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudSubtotal.Location = new System.Drawing.Point(726, 106);
            this.nudSubtotal.Margin = new System.Windows.Forms.Padding(4);
            this.nudSubtotal.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudSubtotal.Name = "nudSubtotal";
            this.nudSubtotal.Size = new System.Drawing.Size(249, 34);
            this.nudSubtotal.TabIndex = 11;
            this.nudSubtotal.ThousandsSeparator = true;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblObservaciones.ForeColor = System.Drawing.Color.White;
            this.lblObservaciones.Location = new System.Drawing.Point(4, 157);
            this.lblObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(226, 43);
            this.lblObservaciones.TabIndex = 12;
            this.lblObservaciones.Text = "Observaciones:";
            this.lblObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtObservaciones.Location = new System.Drawing.Point(238, 157);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(246, 34);
            this.txtObservaciones.TabIndex = 13;
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
            this.lblFormularioTitulo.Text = "Información del detalle de compra";
            // 
            // FrmDetalleCompra
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
            this.Name = "FrmDetalleCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Telefonía - Detalle de compra";
            this.pnlMenu.ResumeLayout(false);
            this.pnlItemsMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleCompra)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCostoUnitario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubtotal)).EndInit();
            this.panelEstado.ResumeLayout(false);
            this.panelEstado.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
