using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    partial class FrmMarcas
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
        private Label lblCodigoProveedor;
        private Label lblNombre;
        private Label lblPaisOrigen;
        private Label lblDescripcion;
        private Label lblSitioWeb;
        private Label lblFechaRegistro;
        private Label lblEstado;
        private TextBox txtCodigo;
        private TextBox txtNombre;
        private TextBox txtPaisOrigen;
        private TextBox txtDescripcion;
        private TextBox txtSitioWeb;
        private TextBox txtBuscar;
        private ComboBox cmbCodigoProveedor;
        private DateTimePicker dtpFechaRegistro;
        private RadioButton rdoActivo;
        private RadioButton rdoInactivo;
        private CheckBox chkSeleccionar;
        private CheckBox chkCodigo;
        private CheckBox chkCodigoProveedor;
        private CheckBox chkNombre;
        private CheckBox chkPaisOrigen;
        private CheckBox chkDescripcion;
        private CheckBox chkSitioWeb;
        private CheckBox chkFechaRegistro;
        private CheckBox chkEstado;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnBuscar;
        private DataGridView dgvMarcas;
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
            this.dgvMarcas = new System.Windows.Forms.DataGridView();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.pnlColumnas = new System.Windows.Forms.Panel();
            this.selector = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSeleccionar = new System.Windows.Forms.CheckBox();
            this.chkCodigo = new System.Windows.Forms.CheckBox();
            this.chkCodigoProveedor = new System.Windows.Forms.CheckBox();
            this.chkNombre = new System.Windows.Forms.CheckBox();
            this.chkPaisOrigen = new System.Windows.Forms.CheckBox();
            this.chkDescripcion = new System.Windows.Forms.CheckBox();
            this.chkSitioWeb = new System.Windows.Forms.CheckBox();
            this.chkFechaRegistro = new System.Windows.Forms.CheckBox();
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
            this.lblCodigoProveedor = new System.Windows.Forms.Label();
            this.cmbCodigoProveedor = new System.Windows.Forms.ComboBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblPaisOrigen = new System.Windows.Forms.Label();
            this.txtPaisOrigen = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblSitioWeb = new System.Windows.Forms.Label();
            this.txtSitioWeb = new System.Windows.Forms.TextBox();
            this.lblFechaRegistro = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarcas)).BeginInit();
            this.pnlColumnas.SuspendLayout();
            this.selector.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.layoutBotones.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.layoutCampos.SuspendLayout();
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
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(7);
            this.pnlMenu.Size = new System.Drawing.Size(162, 662);
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
            this.pnlItemsMenu.Location = new System.Drawing.Point(7, 154);
            this.pnlItemsMenu.Margin = new System.Windows.Forms.Padding(2);
            this.pnlItemsMenu.Name = "pnlItemsMenu";
            this.pnlItemsMenu.Size = new System.Drawing.Size(148, 427);
            this.pnlItemsMenu.TabIndex = 0;
            // 
            // lblMenuUsuarios
            // 
            this.lblMenuUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.lblMenuUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblMenuUsuarios.Location = new System.Drawing.Point(0, 0);
            this.lblMenuUsuarios.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuUsuarios.Name = "lblMenuUsuarios";
            this.lblMenuUsuarios.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuUsuarios.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuSucursales.Location = new System.Drawing.Point(0, 30);
            this.lblMenuSucursales.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuSucursales.Name = "lblMenuSucursales";
            this.lblMenuSucursales.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuSucursales.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuEmpleados.Location = new System.Drawing.Point(0, 60);
            this.lblMenuEmpleados.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuEmpleados.Name = "lblMenuEmpleados";
            this.lblMenuEmpleados.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuEmpleados.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuClientes.Location = new System.Drawing.Point(0, 90);
            this.lblMenuClientes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuClientes.Name = "lblMenuClientes";
            this.lblMenuClientes.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuClientes.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuPlanes.Location = new System.Drawing.Point(0, 120);
            this.lblMenuPlanes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuPlanes.Name = "lblMenuPlanes";
            this.lblMenuPlanes.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuPlanes.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuLineas.Location = new System.Drawing.Point(0, 150);
            this.lblMenuLineas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuLineas.Name = "lblMenuLineas";
            this.lblMenuLineas.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuLineas.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuProveedores.Location = new System.Drawing.Point(0, 180);
            this.lblMenuProveedores.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuProveedores.Name = "lblMenuProveedores";
            this.lblMenuProveedores.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuProveedores.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuMarcas.Location = new System.Drawing.Point(0, 210);
            this.lblMenuMarcas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuMarcas.Name = "lblMenuMarcas";
            this.lblMenuMarcas.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuMarcas.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuEquipos.Location = new System.Drawing.Point(0, 240);
            this.lblMenuEquipos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuEquipos.Name = "lblMenuEquipos";
            this.lblMenuEquipos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuEquipos.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuCompras.Location = new System.Drawing.Point(0, 270);
            this.lblMenuCompras.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuCompras.Name = "lblMenuCompras";
            this.lblMenuCompras.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuCompras.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuDetalleCompra.Location = new System.Drawing.Point(0, 300);
            this.lblMenuDetalleCompra.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuDetalleCompra.Name = "lblMenuDetalleCompra";
            this.lblMenuDetalleCompra.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuDetalleCompra.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuInventario.Location = new System.Drawing.Point(0, 330);
            this.lblMenuInventario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuInventario.Name = "lblMenuInventario";
            this.lblMenuInventario.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuInventario.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuVentas.Location = new System.Drawing.Point(0, 360);
            this.lblMenuVentas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuVentas.Name = "lblMenuVentas";
            this.lblMenuVentas.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuVentas.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuDetalleVenta.Location = new System.Drawing.Point(0, 390);
            this.lblMenuDetalleVenta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuDetalleVenta.Name = "lblMenuDetalleVenta";
            this.lblMenuDetalleVenta.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuDetalleVenta.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuFacturacion.Location = new System.Drawing.Point(0, 420);
            this.lblMenuFacturacion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuFacturacion.Name = "lblMenuFacturacion";
            this.lblMenuFacturacion.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuFacturacion.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuDetalleFactura.Location = new System.Drawing.Point(0, 450);
            this.lblMenuDetalleFactura.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuDetalleFactura.Name = "lblMenuDetalleFactura";
            this.lblMenuDetalleFactura.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuDetalleFactura.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuPagos.Location = new System.Drawing.Point(0, 480);
            this.lblMenuPagos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuPagos.Name = "lblMenuPagos";
            this.lblMenuPagos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuPagos.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuCardex.Location = new System.Drawing.Point(0, 510);
            this.lblMenuCardex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuCardex.Name = "lblMenuCardex";
            this.lblMenuCardex.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuCardex.Size = new System.Drawing.Size(133, 28);
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
            this.lblMenuReclamos.Location = new System.Drawing.Point(0, 540);
            this.lblMenuReclamos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuReclamos.Name = "lblMenuReclamos";
            this.lblMenuReclamos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuReclamos.Size = new System.Drawing.Size(133, 28);
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
            this.btnVolverMenu.Location = new System.Drawing.Point(7, 122);
            this.btnVolverMenu.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(148, 32);
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
            this.btnCerrarSesion.Location = new System.Drawing.Point(7, 581);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(2);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(148, 32);
            this.btnCerrarSesion.TabIndex = 5;
            this.btnCerrarSesion.Text = "↪  Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(7, 613);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(148, 21);
            this.lblUsuario.TabIndex = 4;
            this.lblUsuario.Text = "Administrador";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsuarioRol
            // 
            this.lblUsuarioRol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblUsuarioRol.Location = new System.Drawing.Point(7, 634);
            this.lblUsuarioRol.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsuarioRol.Name = "lblUsuarioRol";
            this.lblUsuarioRol.Size = new System.Drawing.Size(148, 21);
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
            this.pnlLogo.Location = new System.Drawing.Point(7, 7);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(2);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(148, 115);
            this.pnlLogo.TabIndex = 2;
            // 
            // lblSistema
            // 
            this.lblSistema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSistema.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSistema.ForeColor = System.Drawing.Color.White;
            this.lblSistema.Location = new System.Drawing.Point(0, 46);
            this.lblSistema.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(148, 69);
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
            this.lblLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(148, 46);
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
            this.pnlHeader.Location = new System.Drawing.Point(162, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(18, 7, 12, 7);
            this.pnlHeader.Size = new System.Drawing.Size(861, 80);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(18, 42);
            this.lblSubtitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(831, 31);
            this.lblSubtitulo.TabIndex = 0;
            this.lblSubtitulo.Text = "Administra las marcas y el proveedor que las distribuye";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(18, 7);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(831, 35);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Gestión de marcas";
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlContenido.Controls.Add(this.pnlCatalogo);
            this.pnlContenido.Controls.Add(this.pnlAcciones);
            this.pnlContenido.Controls.Add(this.pnlFormulario);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(162, 80);
            this.pnlContenido.Margin = new System.Windows.Forms.Padding(2);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(12);
            this.pnlContenido.Size = new System.Drawing.Size(861, 582);
            this.pnlContenido.TabIndex = 0;
            // 
            // pnlCatalogo
            // 
            this.pnlCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.pnlCatalogo.Controls.Add(this.dgvMarcas);
            this.pnlCatalogo.Controls.Add(this.lblRegistros);
            this.pnlCatalogo.Controls.Add(this.pnlColumnas);
            this.pnlCatalogo.Controls.Add(this.pnlBusqueda);
            this.pnlCatalogo.Controls.Add(this.lblCatalogoTitulo);
            this.pnlCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCatalogo.Location = new System.Drawing.Point(12, 281);
            this.pnlCatalogo.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCatalogo.Name = "pnlCatalogo";
            this.pnlCatalogo.Padding = new System.Windows.Forms.Padding(8);
            this.pnlCatalogo.Size = new System.Drawing.Size(837, 289);
            this.pnlCatalogo.TabIndex = 0;
            // 
            // dgvMarcas
            // 
            this.dgvMarcas.AllowUserToAddRows = false;
            this.dgvMarcas.AllowUserToDeleteRows = false;
            this.dgvMarcas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMarcas.BackgroundColor = System.Drawing.Color.White;
            this.dgvMarcas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMarcas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMarcas.ColumnHeadersHeight = 34;
            this.dgvMarcas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMarcas.EnableHeadersVisualStyles = false;
            this.dgvMarcas.Location = new System.Drawing.Point(8, 136);
            this.dgvMarcas.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMarcas.MultiSelect = false;
            this.dgvMarcas.Name = "dgvMarcas";
            this.dgvMarcas.ReadOnly = true;
            this.dgvMarcas.RowHeadersVisible = false;
            this.dgvMarcas.RowHeadersWidth = 62;
            this.dgvMarcas.RowTemplate.Height = 29;
            this.dgvMarcas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMarcas.Size = new System.Drawing.Size(821, 126);
            this.dgvMarcas.TabIndex = 0;
            // 
            // lblRegistros
            // 
            this.lblRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblRegistros.Location = new System.Drawing.Point(8, 262);
            this.lblRegistros.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(821, 19);
            this.lblRegistros.TabIndex = 1;
            this.lblRegistros.Text = "Total de marcas registradas: 0";
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlColumnas
            // 
            this.pnlColumnas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.pnlColumnas.Controls.Add(this.selector);
            this.pnlColumnas.Controls.Add(this.lblColumnas);
            this.pnlColumnas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColumnas.Location = new System.Drawing.Point(8, 65);
            this.pnlColumnas.Margin = new System.Windows.Forms.Padding(2);
            this.pnlColumnas.Name = "pnlColumnas";
            this.pnlColumnas.Size = new System.Drawing.Size(821, 71);
            this.pnlColumnas.TabIndex = 2;
            // 
            // selector
            // 
            this.selector.AutoScroll = true;
            this.selector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.selector.Controls.Add(this.chkSeleccionar);
            this.selector.Controls.Add(this.chkCodigo);
            this.selector.Controls.Add(this.chkCodigoProveedor);
            this.selector.Controls.Add(this.chkNombre);
            this.selector.Controls.Add(this.chkPaisOrigen);
            this.selector.Controls.Add(this.chkDescripcion);
            this.selector.Controls.Add(this.chkSitioWeb);
            this.selector.Controls.Add(this.chkFechaRegistro);
            this.selector.Controls.Add(this.chkEstado);
            this.selector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selector.Location = new System.Drawing.Point(0, 19);
            this.selector.Margin = new System.Windows.Forms.Padding(2);
            this.selector.Name = "selector";
            this.selector.Size = new System.Drawing.Size(821, 52);
            this.selector.TabIndex = 0;
            // 
            // chkSeleccionar
            // 
            this.chkSeleccionar.AutoSize = true;
            this.chkSeleccionar.BackColor = System.Drawing.Color.Transparent;
            this.chkSeleccionar.ForeColor = System.Drawing.Color.White;
            this.chkSeleccionar.Location = new System.Drawing.Point(4, 2);
            this.chkSeleccionar.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkSeleccionar.Name = "chkSeleccionar";
            this.chkSeleccionar.Size = new System.Drawing.Size(107, 24);
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
            this.chkCodigo.Location = new System.Drawing.Point(123, 2);
            this.chkCodigo.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkCodigo.Name = "chkCodigo";
            this.chkCodigo.Size = new System.Drawing.Size(80, 24);
            this.chkCodigo.TabIndex = 1;
            this.chkCodigo.Text = "Codigo";
            this.chkCodigo.UseVisualStyleBackColor = false;
            // 
            // chkCodigoProveedor
            // 
            this.chkCodigoProveedor.AutoSize = true;
            this.chkCodigoProveedor.BackColor = System.Drawing.Color.Transparent;
            this.chkCodigoProveedor.Checked = true;
            this.chkCodigoProveedor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoProveedor.ForeColor = System.Drawing.Color.White;
            this.chkCodigoProveedor.Location = new System.Drawing.Point(215, 2);
            this.chkCodigoProveedor.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkCodigoProveedor.Name = "chkCodigoProveedor";
            this.chkCodigoProveedor.Size = new System.Drawing.Size(152, 24);
            this.chkCodigoProveedor.TabIndex = 2;
            this.chkCodigoProveedor.Text = "Codigo Proveedor";
            this.chkCodigoProveedor.UseVisualStyleBackColor = false;
            // 
            // chkNombre
            // 
            this.chkNombre.AutoSize = true;
            this.chkNombre.BackColor = System.Drawing.Color.Transparent;
            this.chkNombre.Checked = true;
            this.chkNombre.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNombre.ForeColor = System.Drawing.Color.White;
            this.chkNombre.Location = new System.Drawing.Point(379, 2);
            this.chkNombre.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkNombre.Name = "chkNombre";
            this.chkNombre.Size = new System.Drawing.Size(86, 24);
            this.chkNombre.TabIndex = 3;
            this.chkNombre.Text = "Nombre";
            this.chkNombre.UseVisualStyleBackColor = false;
            // 
            // chkPaisOrigen
            // 
            this.chkPaisOrigen.AutoSize = true;
            this.chkPaisOrigen.BackColor = System.Drawing.Color.Transparent;
            this.chkPaisOrigen.Checked = true;
            this.chkPaisOrigen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPaisOrigen.ForeColor = System.Drawing.Color.White;
            this.chkPaisOrigen.Location = new System.Drawing.Point(477, 2);
            this.chkPaisOrigen.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkPaisOrigen.Name = "chkPaisOrigen";
            this.chkPaisOrigen.Size = new System.Drawing.Size(105, 24);
            this.chkPaisOrigen.TabIndex = 4;
            this.chkPaisOrigen.Text = "Pais Origen";
            this.chkPaisOrigen.UseVisualStyleBackColor = false;
            // 
            // chkDescripcion
            // 
            this.chkDescripcion.AutoSize = true;
            this.chkDescripcion.BackColor = System.Drawing.Color.Transparent;
            this.chkDescripcion.Checked = true;
            this.chkDescripcion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDescripcion.ForeColor = System.Drawing.Color.White;
            this.chkDescripcion.Location = new System.Drawing.Point(594, 2);
            this.chkDescripcion.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkDescripcion.Name = "chkDescripcion";
            this.chkDescripcion.Size = new System.Drawing.Size(109, 24);
            this.chkDescripcion.TabIndex = 5;
            this.chkDescripcion.Text = "Descripcion";
            this.chkDescripcion.UseVisualStyleBackColor = false;
            // 
            // chkSitioWeb
            // 
            this.chkSitioWeb.AutoSize = true;
            this.chkSitioWeb.BackColor = System.Drawing.Color.Transparent;
            this.chkSitioWeb.Checked = true;
            this.chkSitioWeb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSitioWeb.ForeColor = System.Drawing.Color.White;
            this.chkSitioWeb.Location = new System.Drawing.Point(715, 2);
            this.chkSitioWeb.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkSitioWeb.Name = "chkSitioWeb";
            this.chkSitioWeb.Size = new System.Drawing.Size(95, 24);
            this.chkSitioWeb.TabIndex = 6;
            this.chkSitioWeb.Text = "Sitio Web";
            this.chkSitioWeb.UseVisualStyleBackColor = false;
            // 
            // chkFechaRegistro
            // 
            this.chkFechaRegistro.AutoSize = true;
            this.chkFechaRegistro.BackColor = System.Drawing.Color.Transparent;
            this.chkFechaRegistro.Checked = true;
            this.chkFechaRegistro.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFechaRegistro.ForeColor = System.Drawing.Color.White;
            this.chkFechaRegistro.Location = new System.Drawing.Point(4, 30);
            this.chkFechaRegistro.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkFechaRegistro.Name = "chkFechaRegistro";
            this.chkFechaRegistro.Size = new System.Drawing.Size(128, 24);
            this.chkFechaRegistro.TabIndex = 7;
            this.chkFechaRegistro.Text = "Fecha Registro";
            this.chkFechaRegistro.UseVisualStyleBackColor = false;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.BackColor = System.Drawing.Color.Transparent;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.ForeColor = System.Drawing.Color.White;
            this.chkEstado.Location = new System.Drawing.Point(144, 30);
            this.chkEstado.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(76, 24);
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
            this.lblColumnas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblColumnas.Name = "lblColumnas";
            this.lblColumnas.Size = new System.Drawing.Size(821, 19);
            this.lblColumnas.TabIndex = 1;
            this.lblColumnas.Text = "Opciones de visualización:";
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.txtBuscar);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusqueda.Location = new System.Drawing.Point(8, 33);
            this.pnlBusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(821, 32);
            this.pnlBusqueda.TabIndex = 3;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Location = new System.Drawing.Point(0, 0);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(753, 27);
            this.txtBuscar.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.btnBuscar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(753, 0);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(68, 32);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // lblCatalogoTitulo
            // 
            this.lblCatalogoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCatalogoTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCatalogoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblCatalogoTitulo.Location = new System.Drawing.Point(8, 8);
            this.lblCatalogoTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCatalogoTitulo.Name = "lblCatalogoTitulo";
            this.lblCatalogoTitulo.Size = new System.Drawing.Size(821, 25);
            this.lblCatalogoTitulo.TabIndex = 4;
            this.lblCatalogoTitulo.Text = "Marcas registradas";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlAcciones.Controls.Add(this.layoutBotones);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAcciones.Location = new System.Drawing.Point(12, 229);
            this.pnlAcciones.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(837, 52);
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
            this.layoutBotones.Location = new System.Drawing.Point(42, 3);
            this.layoutBotones.Margin = new System.Windows.Forms.Padding(2);
            this.layoutBotones.Name = "layoutBotones";
            this.layoutBotones.RowCount = 1;
            this.layoutBotones.Size = new System.Drawing.Size(750, 45);
            this.layoutBotones.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnNuevo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(2, 2);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(146, 41);
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
            this.btnGuardar.Location = new System.Drawing.Point(152, 2);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(146, 41);
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
            this.btnEditar.Location = new System.Drawing.Point(302, 2);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(146, 41);
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
            this.btnEliminar.Location = new System.Drawing.Point(452, 2);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(146, 41);
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
            this.btnLimpiar.Location = new System.Drawing.Point(602, 2);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(146, 41);
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
            this.pnlFormulario.Location = new System.Drawing.Point(12, 12);
            this.pnlFormulario.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFormulario.Name = "pnlFormulario";
            this.pnlFormulario.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFormulario.Size = new System.Drawing.Size(837, 217);
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
            this.layoutCampos.Controls.Add(this.lblCodigoProveedor, 2, 0);
            this.layoutCampos.Controls.Add(this.cmbCodigoProveedor, 3, 0);
            this.layoutCampos.Controls.Add(this.lblNombre, 0, 1);
            this.layoutCampos.Controls.Add(this.txtNombre, 1, 1);
            this.layoutCampos.Controls.Add(this.lblPaisOrigen, 2, 1);
            this.layoutCampos.Controls.Add(this.txtPaisOrigen, 3, 1);
            this.layoutCampos.Controls.Add(this.lblDescripcion, 0, 2);
            this.layoutCampos.Controls.Add(this.txtDescripcion, 1, 2);
            this.layoutCampos.Controls.Add(this.lblSitioWeb, 2, 2);
            this.layoutCampos.Controls.Add(this.txtSitioWeb, 3, 2);
            this.layoutCampos.Controls.Add(this.lblFechaRegistro, 0, 3);
            this.layoutCampos.Controls.Add(this.dtpFechaRegistro, 1, 3);
            this.layoutCampos.Controls.Add(this.lblEstado, 2, 3);
            this.layoutCampos.Controls.Add(this.panelEstado, 3, 3);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCampos.Location = new System.Drawing.Point(10, 37);
            this.layoutCampos.Margin = new System.Windows.Forms.Padding(2);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.RowCount = 4;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.Size = new System.Drawing.Size(817, 170);
            this.layoutCampos.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.White;
            this.lblCodigo.Location = new System.Drawing.Point(3, 3);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(3);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(190, 36);
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
            this.txtCodigo.Location = new System.Drawing.Point(199, 3);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(206, 30);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblCodigoProveedor
            // 
            this.lblCodigoProveedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoProveedor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoProveedor.ForeColor = System.Drawing.Color.White;
            this.lblCodigoProveedor.Location = new System.Drawing.Point(411, 3);
            this.lblCodigoProveedor.Margin = new System.Windows.Forms.Padding(3);
            this.lblCodigoProveedor.Name = "lblCodigoProveedor";
            this.lblCodigoProveedor.Size = new System.Drawing.Size(190, 36);
            this.lblCodigoProveedor.TabIndex = 2;
            this.lblCodigoProveedor.Text = "Cód. proveedor:";
            this.lblCodigoProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoProveedor
            // 
            this.cmbCodigoProveedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoProveedor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoProveedor.Location = new System.Drawing.Point(607, 3);
            this.cmbCodigoProveedor.Name = "cmbCodigoProveedor";
            this.cmbCodigoProveedor.Size = new System.Drawing.Size(207, 31);
            this.cmbCodigoProveedor.TabIndex = 3;
            // 
            // lblNombre
            // 
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(3, 45);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(3);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(190, 36);
            this.lblNombre.TabIndex = 4;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNombre
            // 
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(199, 45);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(206, 30);
            this.txtNombre.TabIndex = 5;
            // 
            // lblPaisOrigen
            // 
            this.lblPaisOrigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPaisOrigen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaisOrigen.ForeColor = System.Drawing.Color.White;
            this.lblPaisOrigen.Location = new System.Drawing.Point(411, 45);
            this.lblPaisOrigen.Margin = new System.Windows.Forms.Padding(3);
            this.lblPaisOrigen.Name = "lblPaisOrigen";
            this.lblPaisOrigen.Size = new System.Drawing.Size(190, 36);
            this.lblPaisOrigen.TabIndex = 6;
            this.lblPaisOrigen.Text = "País de origen:";
            this.lblPaisOrigen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPaisOrigen
            // 
            this.txtPaisOrigen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPaisOrigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPaisOrigen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPaisOrigen.Location = new System.Drawing.Point(607, 45);
            this.txtPaisOrigen.Name = "txtPaisOrigen";
            this.txtPaisOrigen.Size = new System.Drawing.Size(207, 30);
            this.txtPaisOrigen.TabIndex = 7;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescripcion.ForeColor = System.Drawing.Color.White;
            this.lblDescripcion.Location = new System.Drawing.Point(3, 87);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(3);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(190, 36);
            this.lblDescripcion.TabIndex = 8;
            this.lblDescripcion.Text = "Descripción:";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcion.Location = new System.Drawing.Point(199, 87);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(206, 30);
            this.txtDescripcion.TabIndex = 9;
            // 
            // lblSitioWeb
            // 
            this.lblSitioWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSitioWeb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSitioWeb.ForeColor = System.Drawing.Color.White;
            this.lblSitioWeb.Location = new System.Drawing.Point(411, 87);
            this.lblSitioWeb.Margin = new System.Windows.Forms.Padding(3);
            this.lblSitioWeb.Name = "lblSitioWeb";
            this.lblSitioWeb.Size = new System.Drawing.Size(190, 36);
            this.lblSitioWeb.TabIndex = 10;
            this.lblSitioWeb.Text = "Sitio web:";
            this.lblSitioWeb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSitioWeb
            // 
            this.txtSitioWeb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSitioWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSitioWeb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSitioWeb.Location = new System.Drawing.Point(607, 87);
            this.txtSitioWeb.Name = "txtSitioWeb";
            this.txtSitioWeb.Size = new System.Drawing.Size(207, 30);
            this.txtSitioWeb.TabIndex = 11;
            // 
            // lblFechaRegistro
            // 
            this.lblFechaRegistro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaRegistro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaRegistro.ForeColor = System.Drawing.Color.White;
            this.lblFechaRegistro.Location = new System.Drawing.Point(3, 129);
            this.lblFechaRegistro.Margin = new System.Windows.Forms.Padding(3);
            this.lblFechaRegistro.Name = "lblFechaRegistro";
            this.lblFechaRegistro.Size = new System.Drawing.Size(190, 38);
            this.lblFechaRegistro.TabIndex = 12;
            this.lblFechaRegistro.Text = "Fecha de registro:";
            this.lblFechaRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaRegistro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaRegistro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(199, 129);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(206, 30);
            this.dtpFechaRegistro.TabIndex = 13;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.White;
            this.lblEstado.Location = new System.Drawing.Point(411, 129);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(3);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(190, 38);
            this.lblEstado.TabIndex = 14;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelEstado
            // 
            this.panelEstado.Controls.Add(this.rdoActivo);
            this.panelEstado.Controls.Add(this.rdoInactivo);
            this.panelEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstado.Location = new System.Drawing.Point(607, 129);
            this.panelEstado.Name = "panelEstado";
            this.panelEstado.Size = new System.Drawing.Size(207, 38);
            this.panelEstado.TabIndex = 15;
            this.panelEstado.WrapContents = false;
            // 
            // rdoActivo
            // 
            this.rdoActivo.AutoSize = true;
            this.rdoActivo.Checked = true;
            this.rdoActivo.ForeColor = System.Drawing.Color.White;
            this.rdoActivo.Location = new System.Drawing.Point(2, 2);
            this.rdoActivo.Margin = new System.Windows.Forms.Padding(2);
            this.rdoActivo.Name = "rdoActivo";
            this.rdoActivo.Size = new System.Drawing.Size(72, 24);
            this.rdoActivo.TabIndex = 0;
            this.rdoActivo.TabStop = true;
            this.rdoActivo.Text = "Activo";
            // 
            // rdoInactivo
            // 
            this.rdoInactivo.AutoSize = true;
            this.rdoInactivo.ForeColor = System.Drawing.Color.White;
            this.rdoInactivo.Location = new System.Drawing.Point(78, 2);
            this.rdoInactivo.Margin = new System.Windows.Forms.Padding(2);
            this.rdoInactivo.Name = "rdoInactivo";
            this.rdoInactivo.Size = new System.Drawing.Size(82, 24);
            this.rdoInactivo.TabIndex = 1;
            this.rdoInactivo.Text = "Inactivo";
            // 
            // lblFormularioTitulo
            // 
            this.lblFormularioTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormularioTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormularioTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblFormularioTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblFormularioTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormularioTitulo.Name = "lblFormularioTitulo";
            this.lblFormularioTitulo.Size = new System.Drawing.Size(817, 27);
            this.lblFormularioTitulo.TabIndex = 1;
            this.lblFormularioTitulo.Text = "Información de la marca";
            // 
            // FrmMarcas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1023, 662);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(753, 591);
            this.Name = "FrmMarcas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Telefonía - Marcas";
            this.pnlMenu.ResumeLayout(false);
            this.pnlItemsMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarcas)).EndInit();
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
            this.panelEstado.ResumeLayout(false);
            this.panelEstado.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
