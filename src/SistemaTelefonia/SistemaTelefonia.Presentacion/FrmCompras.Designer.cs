using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    partial class FrmCompras
    {
        private IContainer components = null;
        private Panel pnlMenu, pnlLogo, pnlItemsMenu, pnlHeader, pnlContenido;
        private Panel pnlFormulario, pnlCatalogo, pnlBusqueda, pnlColumnas, pnlAcciones;
        private Label lblLogo, lblSistema, lblUsuario, lblUsuarioRol, lblTitulo, lblSubtitulo;
        private Label lblFormularioTitulo, lblCatalogoTitulo, lblRegistros, lblColumnas;
        private Label lblCodigo;
        private Label lblCodigoProveedor;
        private Label lblNumeroDocumento;
        private Label lblFechaCompra;
        private Label lblSubtotal;
        private Label lblIva;
        private Label lblTotal;
        private Label lblObservaciones;
        private Label lblEstado;
        private Label lblMenuUsuarios, lblMenuSucursales, lblMenuEmpleados, lblMenuClientes, lblMenuPlanes, lblMenuLineas;
        private Label lblMenuProveedores, lblMenuMarcas, lblMenuEquipos, lblMenuCompras, lblMenuDetalleCompra, lblMenuInventario;
        private Label lblMenuVentas, lblMenuDetalleVenta, lblMenuFacturacion, lblMenuDetalleFactura, lblMenuPagos, lblMenuCardex, lblMenuReclamos;
        private TextBox txtCodigo;
        private TextBox txtNumeroDocumento;
        private TextBox txtObservaciones;
        private ComboBox cmbCodigoProveedor;
        private NumericUpDown nudSubtotal;
        private NumericUpDown nudIva;
        private NumericUpDown nudTotal;
        private DateTimePicker dtpFechaCompra;
        private FlowLayoutPanel selector, panelEstado;
        private RadioButton rdoActivo, rdoInactivo;
        private CheckBox chkSeleccionar;
        private CheckBox chkCodigo;
        private CheckBox chkCodigoProveedor;
        private CheckBox chkNumeroDocumento;
        private CheckBox chkFechaCompra;
        private CheckBox chkSubtotal;
        private CheckBox chkIva;
        private CheckBox chkTotal;
        private CheckBox chkObservaciones;
        private CheckBox chkEstado;
        private TextBox txtBuscar;
        private Button btnVolverMenu, btnCerrarSesion, btnNuevo, btnGuardar, btnEditar, btnEliminar, btnLimpiar, btnBuscar;
        private DataGridView dgvCompras;
        private TableLayoutPanel layoutCampos, layoutBotones;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblSistema = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.lblUsuarioRol = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlCatalogo = new System.Windows.Forms.Panel();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.pnlColumnas = new System.Windows.Forms.Panel();
            this.selector = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSeleccionar = new System.Windows.Forms.CheckBox();
            this.chkCodigo = new System.Windows.Forms.CheckBox();
            this.chkCodigoProveedor = new System.Windows.Forms.CheckBox();
            this.chkNumeroDocumento = new System.Windows.Forms.CheckBox();
            this.chkFechaCompra = new System.Windows.Forms.CheckBox();
            this.chkSubtotal = new System.Windows.Forms.CheckBox();
            this.chkIva = new System.Windows.Forms.CheckBox();
            this.chkTotal = new System.Windows.Forms.CheckBox();
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
            this.lblCodigoProveedor = new System.Windows.Forms.Label();
            this.cmbCodigoProveedor = new System.Windows.Forms.ComboBox();
            this.lblNumeroDocumento = new System.Windows.Forms.Label();
            this.txtNumeroDocumento = new System.Windows.Forms.TextBox();
            this.lblFechaCompra = new System.Windows.Forms.Label();
            this.dtpFechaCompra = new System.Windows.Forms.DateTimePicker();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.nudSubtotal = new System.Windows.Forms.NumericUpDown();
            this.lblIva = new System.Windows.Forms.Label();
            this.nudIva = new System.Windows.Forms.NumericUpDown();
            this.lblTotal = new System.Windows.Forms.Label();
            this.nudTotal = new System.Windows.Forms.NumericUpDown();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.pnlColumnas.SuspendLayout();
            this.selector.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.layoutBotones.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.layoutCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubtotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).BeginInit();
            this.panelEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(25)))), ((int)(((byte)(47)))));
            this.pnlMenu.Controls.Add(this.pnlItemsMenu);
            this.pnlMenu.Controls.Add(this.btnVolverMenu);
            this.pnlMenu.Controls.Add(this.pnlLogo);
            this.pnlMenu.Controls.Add(this.btnCerrarSesion);
            this.pnlMenu.Controls.Add(this.lblUsuarioRol);
            this.pnlMenu.Controls.Add(this.lblUsuario);
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
            this.pnlItemsMenu.Location = new System.Drawing.Point(8, 146);
            this.pnlItemsMenu.Name = "pnlItemsMenu";
            this.pnlItemsMenu.Size = new System.Drawing.Size(179, 556);
            this.pnlItemsMenu.TabIndex = 0;
            // 
            // lblMenuUsuarios
            // 
            this.lblMenuUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblMenuUsuarios.Location = new System.Drawing.Point(0, 0);
            this.lblMenuUsuarios.Name = "lblMenuUsuarios";
            this.lblMenuUsuarios.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuUsuarios.Size = new System.Drawing.Size(160, 34);
            this.lblMenuUsuarios.TabIndex = 0;
            this.lblMenuUsuarios.Tag = "Usuarios";
            this.lblMenuUsuarios.Text = "›   Usuarios";
            this.lblMenuUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuSucursales
            // 
            this.lblMenuSucursales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuSucursales.ForeColor = System.Drawing.Color.White;
            this.lblMenuSucursales.Location = new System.Drawing.Point(0, 34);
            this.lblMenuSucursales.Name = "lblMenuSucursales";
            this.lblMenuSucursales.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuSucursales.Size = new System.Drawing.Size(160, 34);
            this.lblMenuSucursales.TabIndex = 1;
            this.lblMenuSucursales.Tag = "Sucursales";
            this.lblMenuSucursales.Text = "›   Sucursales";
            this.lblMenuSucursales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuEmpleados
            // 
            this.lblMenuEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuEmpleados.ForeColor = System.Drawing.Color.White;
            this.lblMenuEmpleados.Location = new System.Drawing.Point(0, 68);
            this.lblMenuEmpleados.Name = "lblMenuEmpleados";
            this.lblMenuEmpleados.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuEmpleados.Size = new System.Drawing.Size(160, 34);
            this.lblMenuEmpleados.TabIndex = 2;
            this.lblMenuEmpleados.Tag = "Empleados";
            this.lblMenuEmpleados.Text = "›   Empleados";
            this.lblMenuEmpleados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuClientes
            // 
            this.lblMenuClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuClientes.ForeColor = System.Drawing.Color.White;
            this.lblMenuClientes.Location = new System.Drawing.Point(0, 102);
            this.lblMenuClientes.Name = "lblMenuClientes";
            this.lblMenuClientes.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuClientes.Size = new System.Drawing.Size(160, 34);
            this.lblMenuClientes.TabIndex = 3;
            this.lblMenuClientes.Tag = "Clientes";
            this.lblMenuClientes.Text = "›   Clientes";
            this.lblMenuClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuPlanes
            // 
            this.lblMenuPlanes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuPlanes.ForeColor = System.Drawing.Color.White;
            this.lblMenuPlanes.Location = new System.Drawing.Point(0, 136);
            this.lblMenuPlanes.Name = "lblMenuPlanes";
            this.lblMenuPlanes.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuPlanes.Size = new System.Drawing.Size(160, 34);
            this.lblMenuPlanes.TabIndex = 4;
            this.lblMenuPlanes.Tag = "Planes";
            this.lblMenuPlanes.Text = "›   Planes";
            this.lblMenuPlanes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuLineas
            // 
            this.lblMenuLineas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuLineas.ForeColor = System.Drawing.Color.White;
            this.lblMenuLineas.Location = new System.Drawing.Point(0, 170);
            this.lblMenuLineas.Name = "lblMenuLineas";
            this.lblMenuLineas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuLineas.Size = new System.Drawing.Size(160, 34);
            this.lblMenuLineas.TabIndex = 5;
            this.lblMenuLineas.Tag = "Lineas";
            this.lblMenuLineas.Text = "›   Líneas";
            this.lblMenuLineas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuProveedores
            // 
            this.lblMenuProveedores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuProveedores.ForeColor = System.Drawing.Color.White;
            this.lblMenuProveedores.Location = new System.Drawing.Point(0, 204);
            this.lblMenuProveedores.Name = "lblMenuProveedores";
            this.lblMenuProveedores.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuProveedores.Size = new System.Drawing.Size(160, 34);
            this.lblMenuProveedores.TabIndex = 6;
            this.lblMenuProveedores.Tag = "Proveedores";
            this.lblMenuProveedores.Text = "›   Proveedores";
            this.lblMenuProveedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuMarcas
            // 
            this.lblMenuMarcas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuMarcas.ForeColor = System.Drawing.Color.White;
            this.lblMenuMarcas.Location = new System.Drawing.Point(0, 238);
            this.lblMenuMarcas.Name = "lblMenuMarcas";
            this.lblMenuMarcas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuMarcas.Size = new System.Drawing.Size(160, 34);
            this.lblMenuMarcas.TabIndex = 7;
            this.lblMenuMarcas.Tag = "Marcas";
            this.lblMenuMarcas.Text = "›   Marcas";
            this.lblMenuMarcas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuEquipos
            // 
            this.lblMenuEquipos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuEquipos.ForeColor = System.Drawing.Color.White;
            this.lblMenuEquipos.Location = new System.Drawing.Point(0, 272);
            this.lblMenuEquipos.Name = "lblMenuEquipos";
            this.lblMenuEquipos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuEquipos.Size = new System.Drawing.Size(160, 34);
            this.lblMenuEquipos.TabIndex = 8;
            this.lblMenuEquipos.Tag = "Equipos";
            this.lblMenuEquipos.Text = "›   Equipos";
            this.lblMenuEquipos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuCompras
            // 
            this.lblMenuCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuCompras.ForeColor = System.Drawing.Color.White;
            this.lblMenuCompras.Location = new System.Drawing.Point(0, 306);
            this.lblMenuCompras.Name = "lblMenuCompras";
            this.lblMenuCompras.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuCompras.Size = new System.Drawing.Size(160, 34);
            this.lblMenuCompras.TabIndex = 9;
            this.lblMenuCompras.Tag = "Compras";
            this.lblMenuCompras.Text = "›   Compras";
            this.lblMenuCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleCompra
            // 
            this.lblMenuDetalleCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuDetalleCompra.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleCompra.Location = new System.Drawing.Point(0, 340);
            this.lblMenuDetalleCompra.Name = "lblMenuDetalleCompra";
            this.lblMenuDetalleCompra.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuDetalleCompra.Size = new System.Drawing.Size(160, 34);
            this.lblMenuDetalleCompra.TabIndex = 10;
            this.lblMenuDetalleCompra.Tag = "DetalleCompra";
            this.lblMenuDetalleCompra.Text = "›   Detalle compra";
            this.lblMenuDetalleCompra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuInventario
            // 
            this.lblMenuInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuInventario.ForeColor = System.Drawing.Color.White;
            this.lblMenuInventario.Location = new System.Drawing.Point(0, 374);
            this.lblMenuInventario.Name = "lblMenuInventario";
            this.lblMenuInventario.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuInventario.Size = new System.Drawing.Size(160, 34);
            this.lblMenuInventario.TabIndex = 11;
            this.lblMenuInventario.Tag = "Inventario";
            this.lblMenuInventario.Text = "›   Inventario";
            this.lblMenuInventario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuVentas
            // 
            this.lblMenuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuVentas.ForeColor = System.Drawing.Color.White;
            this.lblMenuVentas.Location = new System.Drawing.Point(0, 408);
            this.lblMenuVentas.Name = "lblMenuVentas";
            this.lblMenuVentas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuVentas.Size = new System.Drawing.Size(160, 34);
            this.lblMenuVentas.TabIndex = 12;
            this.lblMenuVentas.Tag = "Ventas";
            this.lblMenuVentas.Text = "›   Ventas";
            this.lblMenuVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleVenta
            // 
            this.lblMenuDetalleVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuDetalleVenta.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleVenta.Location = new System.Drawing.Point(0, 442);
            this.lblMenuDetalleVenta.Name = "lblMenuDetalleVenta";
            this.lblMenuDetalleVenta.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuDetalleVenta.Size = new System.Drawing.Size(160, 34);
            this.lblMenuDetalleVenta.TabIndex = 13;
            this.lblMenuDetalleVenta.Tag = "DetalleVenta";
            this.lblMenuDetalleVenta.Text = "›   Detalle venta";
            this.lblMenuDetalleVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuFacturacion
            // 
            this.lblMenuFacturacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuFacturacion.ForeColor = System.Drawing.Color.White;
            this.lblMenuFacturacion.Location = new System.Drawing.Point(0, 476);
            this.lblMenuFacturacion.Name = "lblMenuFacturacion";
            this.lblMenuFacturacion.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuFacturacion.Size = new System.Drawing.Size(160, 34);
            this.lblMenuFacturacion.TabIndex = 14;
            this.lblMenuFacturacion.Tag = "Facturacion";
            this.lblMenuFacturacion.Text = "›   Facturación";
            this.lblMenuFacturacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleFactura
            // 
            this.lblMenuDetalleFactura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuDetalleFactura.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleFactura.Location = new System.Drawing.Point(0, 510);
            this.lblMenuDetalleFactura.Name = "lblMenuDetalleFactura";
            this.lblMenuDetalleFactura.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuDetalleFactura.Size = new System.Drawing.Size(160, 34);
            this.lblMenuDetalleFactura.TabIndex = 15;
            this.lblMenuDetalleFactura.Tag = "DetalleFactura";
            this.lblMenuDetalleFactura.Text = "›   Detalle factura";
            this.lblMenuDetalleFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuPagos
            // 
            this.lblMenuPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuPagos.ForeColor = System.Drawing.Color.White;
            this.lblMenuPagos.Location = new System.Drawing.Point(0, 544);
            this.lblMenuPagos.Name = "lblMenuPagos";
            this.lblMenuPagos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuPagos.Size = new System.Drawing.Size(160, 34);
            this.lblMenuPagos.TabIndex = 16;
            this.lblMenuPagos.Tag = "Pagos";
            this.lblMenuPagos.Text = "›   Pagos";
            this.lblMenuPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuCardex
            // 
            this.lblMenuCardex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuCardex.ForeColor = System.Drawing.Color.White;
            this.lblMenuCardex.Location = new System.Drawing.Point(0, 578);
            this.lblMenuCardex.Name = "lblMenuCardex";
            this.lblMenuCardex.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuCardex.Size = new System.Drawing.Size(160, 34);
            this.lblMenuCardex.TabIndex = 17;
            this.lblMenuCardex.Tag = "Cardex";
            this.lblMenuCardex.Text = "›   Cardex";
            this.lblMenuCardex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuReclamos
            // 
            this.lblMenuReclamos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuReclamos.ForeColor = System.Drawing.Color.White;
            this.lblMenuReclamos.Location = new System.Drawing.Point(0, 612);
            this.lblMenuReclamos.Name = "lblMenuReclamos";
            this.lblMenuReclamos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblMenuReclamos.Size = new System.Drawing.Size(160, 34);
            this.lblMenuReclamos.TabIndex = 18;
            this.lblMenuReclamos.Tag = "Reclamos";
            this.lblMenuReclamos.Text = "›   Reclamos";
            this.lblMenuReclamos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.btnVolverMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVolverMenu.FlatAppearance.BorderSize = 0;
            this.btnVolverMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolverMenu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVolverMenu.ForeColor = System.Drawing.Color.White;
            this.btnVolverMenu.Location = new System.Drawing.Point(8, 108);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(179, 38);
            this.btnVolverMenu.TabIndex = 1;
            this.btnVolverMenu.Text = "⌂  Volver al menú";
            this.btnVolverMenu.UseVisualStyleBackColor = false;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.pnlLogo.Controls.Add(this.lblSistema);
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(8, 8);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(179, 100);
            this.pnlLogo.TabIndex = 2;
            // 
            // lblSistema
            // 
            this.lblSistema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSistema.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSistema.ForeColor = System.Drawing.Color.White;
            this.lblSistema.Location = new System.Drawing.Point(0, 55);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(179, 45);
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
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(55)))), ((int)(((byte)(62)))));
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(8, 702);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(179, 36);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "↪  Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            // 
            // lblUsuarioRol
            // 
            this.lblUsuarioRol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblUsuarioRol.Location = new System.Drawing.Point(8, 738);
            this.lblUsuarioRol.Name = "lblUsuarioRol";
            this.lblUsuarioRol.Size = new System.Drawing.Size(179, 24);
            this.lblUsuarioRol.TabIndex = 4;
            this.lblUsuarioRol.Text = "Usuario del sistema";
            this.lblUsuarioRol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(8, 762);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(179, 24);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "Administrador";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(22, 50);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblSubtitulo.Size = new System.Drawing.Size(996, 38);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Administra las compras y sus documentos";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(996, 42);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de compras";
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
            this.pnlCatalogo.Controls.Add(this.dgvCompras);
            this.pnlCatalogo.Controls.Add(this.lblRegistros);
            this.pnlCatalogo.Controls.Add(this.pnlColumnas);
            this.pnlCatalogo.Controls.Add(this.pnlBusqueda);
            this.pnlCatalogo.Controls.Add(this.lblCatalogoTitulo);
            this.pnlCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCatalogo.Location = new System.Drawing.Point(15, 377);
            this.pnlCatalogo.Name = "pnlCatalogo";
            this.pnlCatalogo.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCatalogo.Size = new System.Drawing.Size(1003, 306);
            this.pnlCatalogo.TabIndex = 0;
            // 
            // dgvCompras
            // 
            this.dgvCompras.AllowUserToAddRows = false;
            this.dgvCompras.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(248)))));
            this.dgvCompras.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCompras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCompras.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompras.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCompras.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCompras.ColumnHeadersHeight = 34;
            this.dgvCompras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompras.EnableHeadersVisualStyles = false;
            this.dgvCompras.Location = new System.Drawing.Point(10, 163);
            this.dgvCompras.MultiSelect = false;
            this.dgvCompras.Name = "dgvCompras";
            this.dgvCompras.ReadOnly = true;
            this.dgvCompras.RowHeadersVisible = false;
            this.dgvCompras.RowHeadersWidth = 62;
            this.dgvCompras.RowTemplate.Height = 29;
            this.dgvCompras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompras.Size = new System.Drawing.Size(983, 109);
            this.dgvCompras.TabIndex = 0;
            // 
            // lblRegistros
            // 
            this.lblRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblRegistros.Location = new System.Drawing.Point(10, 272);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(983, 24);
            this.lblRegistros.TabIndex = 1;
            this.lblRegistros.Text = "Total de compras registradas: 0";
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlColumnas
            // 
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
            this.selector.Controls.Add(this.chkCodigoProveedor);
            this.selector.Controls.Add(this.chkNumeroDocumento);
            this.selector.Controls.Add(this.chkFechaCompra);
            this.selector.Controls.Add(this.chkSubtotal);
            this.selector.Controls.Add(this.chkIva);
            this.selector.Controls.Add(this.chkTotal);
            this.selector.Controls.Add(this.chkObservaciones);
            this.selector.Controls.Add(this.chkEstado);
            this.selector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selector.Location = new System.Drawing.Point(0, 24);
            this.selector.Name = "selector";
            this.selector.Size = new System.Drawing.Size(983, 61);
            this.selector.TabIndex = 0;
            // 
            // chkSeleccionar
            // 
            this.chkSeleccionar.AutoSize = true;
            this.chkSeleccionar.ForeColor = System.Drawing.Color.White;
            this.chkSeleccionar.Location = new System.Drawing.Point(4, 3);
            this.chkSeleccionar.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkSeleccionar.Name = "chkSeleccionar";
            this.chkSeleccionar.Size = new System.Drawing.Size(126, 29);
            this.chkSeleccionar.TabIndex = 0;
            this.chkSeleccionar.Text = "Seleccionar";
            // 
            // chkCodigo
            // 
            this.chkCodigo.AutoSize = true;
            this.chkCodigo.Checked = true;
            this.chkCodigo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigo.ForeColor = System.Drawing.Color.White;
            this.chkCodigo.Location = new System.Drawing.Point(144, 3);
            this.chkCodigo.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkCodigo.Name = "chkCodigo";
            this.chkCodigo.Size = new System.Drawing.Size(97, 29);
            this.chkCodigo.TabIndex = 1;
            this.chkCodigo.Text = "Código";
            // 
            // chkCodigoProveedor
            // 
            this.chkCodigoProveedor.AutoSize = true;
            this.chkCodigoProveedor.Checked = true;
            this.chkCodigoProveedor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoProveedor.ForeColor = System.Drawing.Color.White;
            this.chkCodigoProveedor.Location = new System.Drawing.Point(255, 3);
            this.chkCodigoProveedor.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkCodigoProveedor.Name = "chkCodigoProveedor";
            this.chkCodigoProveedor.Size = new System.Drawing.Size(163, 29);
            this.chkCodigoProveedor.TabIndex = 2;
            this.chkCodigoProveedor.Text = "Cód. proveedor";
            // 
            // chkNumeroDocumento
            // 
            this.chkNumeroDocumento.AutoSize = true;
            this.chkNumeroDocumento.Checked = true;
            this.chkNumeroDocumento.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNumeroDocumento.ForeColor = System.Drawing.Color.White;
            this.chkNumeroDocumento.Location = new System.Drawing.Point(432, 3);
            this.chkNumeroDocumento.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkNumeroDocumento.Name = "chkNumeroDocumento";
            this.chkNumeroDocumento.Size = new System.Drawing.Size(225, 29);
            this.chkNumeroDocumento.TabIndex = 3;
            this.chkNumeroDocumento.Text = "Número de documento";
            // 
            // chkFechaCompra
            // 
            this.chkFechaCompra.AutoSize = true;
            this.chkFechaCompra.Checked = true;
            this.chkFechaCompra.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFechaCompra.ForeColor = System.Drawing.Color.White;
            this.chkFechaCompra.Location = new System.Drawing.Point(671, 3);
            this.chkFechaCompra.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkFechaCompra.Name = "chkFechaCompra";
            this.chkFechaCompra.Size = new System.Drawing.Size(174, 29);
            this.chkFechaCompra.TabIndex = 4;
            this.chkFechaCompra.Text = "Fecha de compra";
            // 
            // chkSubtotal
            // 
            this.chkSubtotal.AutoSize = true;
            this.chkSubtotal.Checked = true;
            this.chkSubtotal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSubtotal.ForeColor = System.Drawing.Color.White;
            this.chkSubtotal.Location = new System.Drawing.Point(859, 3);
            this.chkSubtotal.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkSubtotal.Name = "chkSubtotal";
            this.chkSubtotal.Size = new System.Drawing.Size(105, 29);
            this.chkSubtotal.TabIndex = 5;
            this.chkSubtotal.Text = "Subtotal";
            // 
            // chkIva
            // 
            this.chkIva.AutoSize = true;
            this.chkIva.Checked = true;
            this.chkIva.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIva.ForeColor = System.Drawing.Color.White;
            this.chkIva.Location = new System.Drawing.Point(4, 38);
            this.chkIva.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkIva.Name = "chkIva";
            this.chkIva.Size = new System.Drawing.Size(65, 29);
            this.chkIva.TabIndex = 6;
            this.chkIva.Text = "IVA";
            // 
            // chkTotal
            // 
            this.chkTotal.AutoSize = true;
            this.chkTotal.Checked = true;
            this.chkTotal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTotal.ForeColor = System.Drawing.Color.White;
            this.chkTotal.Location = new System.Drawing.Point(83, 38);
            this.chkTotal.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkTotal.Name = "chkTotal";
            this.chkTotal.Size = new System.Drawing.Size(75, 29);
            this.chkTotal.TabIndex = 7;
            this.chkTotal.Text = "Total";
            // 
            // chkObservaciones
            // 
            this.chkObservaciones.AutoSize = true;
            this.chkObservaciones.Checked = true;
            this.chkObservaciones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkObservaciones.ForeColor = System.Drawing.Color.White;
            this.chkObservaciones.Location = new System.Drawing.Point(172, 38);
            this.chkObservaciones.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkObservaciones.Name = "chkObservaciones";
            this.chkObservaciones.Size = new System.Drawing.Size(154, 29);
            this.chkObservaciones.TabIndex = 8;
            this.chkObservaciones.Text = "Observaciones";
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.ForeColor = System.Drawing.Color.White;
            this.chkEstado.Location = new System.Drawing.Point(340, 38);
            this.chkEstado.Margin = new System.Windows.Forms.Padding(4, 3, 10, 3);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(92, 29);
            this.chkEstado.TabIndex = 9;
            this.chkEstado.Text = "Estado";
            // 
            // lblColumnas
            // 
            this.lblColumnas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblColumnas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblColumnas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblColumnas.Location = new System.Drawing.Point(0, 0);
            this.lblColumnas.Name = "lblColumnas";
            this.lblColumnas.Size = new System.Drawing.Size(983, 24);
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
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(0, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(883, 34);
            this.txtBuscar.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.btnBuscar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(883, 0);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 38);
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
            this.lblCatalogoTitulo.Text = "Compras registradas";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlAcciones.Controls.Add(this.layoutBotones);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAcciones.Location = new System.Drawing.Point(15, 315);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(1003, 62);
            this.pnlAcciones.TabIndex = 1;
            // 
            // layoutBotones
            // 
            this.layoutBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
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
            this.pnlFormulario.Padding = new System.Windows.Forms.Padding(14);
            this.pnlFormulario.Size = new System.Drawing.Size(1003, 300);
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
            this.layoutCampos.Controls.Add(this.lblNumeroDocumento, 0, 1);
            this.layoutCampos.Controls.Add(this.txtNumeroDocumento, 1, 1);
            this.layoutCampos.Controls.Add(this.lblFechaCompra, 2, 1);
            this.layoutCampos.Controls.Add(this.dtpFechaCompra, 3, 1);
            this.layoutCampos.Controls.Add(this.lblSubtotal, 0, 2);
            this.layoutCampos.Controls.Add(this.nudSubtotal, 1, 2);
            this.layoutCampos.Controls.Add(this.lblIva, 2, 2);
            this.layoutCampos.Controls.Add(this.nudIva, 3, 2);
            this.layoutCampos.Controls.Add(this.lblTotal, 0, 3);
            this.layoutCampos.Controls.Add(this.nudTotal, 1, 3);
            this.layoutCampos.Controls.Add(this.lblObservaciones, 2, 3);
            this.layoutCampos.Controls.Add(this.txtObservaciones, 3, 3);
            this.layoutCampos.Controls.Add(this.lblEstado, 0, 4);
            this.layoutCampos.Controls.Add(this.panelEstado, 1, 4);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCampos.Location = new System.Drawing.Point(14, 46);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.RowCount = 5;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.Size = new System.Drawing.Size(975, 240);
            this.layoutCampos.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.White;
            this.lblCodigo.Location = new System.Drawing.Point(3, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(228, 48);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(242)))));
            this.txtCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCodigo.Location = new System.Drawing.Point(238, 7);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(245, 34);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblCodigoProveedor
            // 
            this.lblCodigoProveedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoProveedor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoProveedor.ForeColor = System.Drawing.Color.White;
            this.lblCodigoProveedor.Location = new System.Drawing.Point(490, 0);
            this.lblCodigoProveedor.Name = "lblCodigoProveedor";
            this.lblCodigoProveedor.Size = new System.Drawing.Size(228, 48);
            this.lblCodigoProveedor.TabIndex = 2;
            this.lblCodigoProveedor.Text = "Proveedor:";
            this.lblCodigoProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoProveedor
            // 
            this.cmbCodigoProveedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoProveedor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoProveedor.Location = new System.Drawing.Point(725, 7);
            this.cmbCodigoProveedor.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.cmbCodigoProveedor.Name = "cmbCodigoProveedor";
            this.cmbCodigoProveedor.Size = new System.Drawing.Size(246, 36);
            this.cmbCodigoProveedor.TabIndex = 3;
            // 
            // lblNumeroDocumento
            // 
            this.lblNumeroDocumento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumeroDocumento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumeroDocumento.ForeColor = System.Drawing.Color.White;
            this.lblNumeroDocumento.Location = new System.Drawing.Point(3, 48);
            this.lblNumeroDocumento.Name = "lblNumeroDocumento";
            this.lblNumeroDocumento.Size = new System.Drawing.Size(228, 48);
            this.lblNumeroDocumento.TabIndex = 4;
            this.lblNumeroDocumento.Text = "Número de documento:";
            this.lblNumeroDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNumeroDocumento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumeroDocumento.Location = new System.Drawing.Point(238, 55);
            this.txtNumeroDocumento.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Size = new System.Drawing.Size(245, 34);
            this.txtNumeroDocumento.TabIndex = 5;
            // 
            // lblFechaCompra
            // 
            this.lblFechaCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaCompra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaCompra.ForeColor = System.Drawing.Color.White;
            this.lblFechaCompra.Location = new System.Drawing.Point(490, 48);
            this.lblFechaCompra.Name = "lblFechaCompra";
            this.lblFechaCompra.Size = new System.Drawing.Size(228, 48);
            this.lblFechaCompra.TabIndex = 6;
            this.lblFechaCompra.Text = "Fecha de compra:";
            this.lblFechaCompra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaCompra
            // 
            this.dtpFechaCompra.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaCompra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCompra.Location = new System.Drawing.Point(725, 55);
            this.dtpFechaCompra.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.dtpFechaCompra.Name = "dtpFechaCompra";
            this.dtpFechaCompra.Size = new System.Drawing.Size(246, 31);
            this.dtpFechaCompra.TabIndex = 7;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.ForeColor = System.Drawing.Color.White;
            this.lblSubtotal.Location = new System.Drawing.Point(3, 96);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(228, 48);
            this.lblSubtotal.TabIndex = 8;
            this.lblSubtotal.Text = "Subtotal:";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudSubtotal
            // 
            this.nudSubtotal.DecimalPlaces = 2;
            this.nudSubtotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudSubtotal.Location = new System.Drawing.Point(238, 103);
            this.nudSubtotal.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.nudSubtotal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudSubtotal.Name = "nudSubtotal";
            this.nudSubtotal.Size = new System.Drawing.Size(245, 34);
            this.nudSubtotal.TabIndex = 9;
            this.nudSubtotal.ThousandsSeparator = true;
            // 
            // lblIva
            // 
            this.lblIva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIva.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblIva.ForeColor = System.Drawing.Color.White;
            this.lblIva.Location = new System.Drawing.Point(490, 96);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(228, 48);
            this.lblIva.TabIndex = 10;
            this.lblIva.Text = "IVA:";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudIva
            // 
            this.nudIva.DecimalPlaces = 2;
            this.nudIva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudIva.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudIva.Location = new System.Drawing.Point(725, 103);
            this.nudIva.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.nudIva.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudIva.Name = "nudIva";
            this.nudIva.Size = new System.Drawing.Size(246, 34);
            this.nudIva.TabIndex = 11;
            this.nudIva.ThousandsSeparator = true;
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(3, 144);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(228, 48);
            this.lblTotal.TabIndex = 12;
            this.lblTotal.Text = "Total:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudTotal
            // 
            this.nudTotal.DecimalPlaces = 2;
            this.nudTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudTotal.Location = new System.Drawing.Point(238, 151);
            this.nudTotal.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.nudTotal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudTotal.Name = "nudTotal";
            this.nudTotal.Size = new System.Drawing.Size(245, 34);
            this.nudTotal.TabIndex = 13;
            this.nudTotal.ThousandsSeparator = true;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblObservaciones.ForeColor = System.Drawing.Color.White;
            this.lblObservaciones.Location = new System.Drawing.Point(490, 144);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(228, 48);
            this.lblObservaciones.TabIndex = 14;
            this.lblObservaciones.Text = "Observaciones:";
            this.lblObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtObservaciones.Location = new System.Drawing.Point(725, 151);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(246, 34);
            this.txtObservaciones.TabIndex = 15;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.White;
            this.lblEstado.Location = new System.Drawing.Point(3, 192);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(228, 48);
            this.lblEstado.TabIndex = 16;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelEstado
            // 
            this.panelEstado.Controls.Add(this.rdoActivo);
            this.panelEstado.Controls.Add(this.rdoInactivo);
            this.panelEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstado.Location = new System.Drawing.Point(237, 195);
            this.panelEstado.Name = "panelEstado";
            this.panelEstado.Size = new System.Drawing.Size(247, 42);
            this.panelEstado.TabIndex = 17;
            this.panelEstado.WrapContents = false;
            // 
            // rdoActivo
            // 
            this.rdoActivo.AutoSize = true;
            this.rdoActivo.Checked = true;
            this.rdoActivo.ForeColor = System.Drawing.Color.White;
            this.rdoActivo.Location = new System.Drawing.Point(3, 7);
            this.rdoActivo.Margin = new System.Windows.Forms.Padding(3, 7, 18, 3);
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
            this.rdoInactivo.Location = new System.Drawing.Point(111, 7);
            this.rdoInactivo.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.rdoInactivo.Name = "rdoInactivo";
            this.rdoInactivo.Size = new System.Drawing.Size(99, 29);
            this.rdoInactivo.TabIndex = 1;
            this.rdoInactivo.Text = "Inactivo";
            // 
            // lblFormularioTitulo
            // 
            this.lblFormularioTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormularioTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblFormularioTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblFormularioTitulo.Location = new System.Drawing.Point(14, 14);
            this.lblFormularioTitulo.Name = "lblFormularioTitulo";
            this.lblFormularioTitulo.Size = new System.Drawing.Size(975, 32);
            this.lblFormularioTitulo.TabIndex = 1;
            this.lblFormularioTitulo.Text = "Información de la compra";
            // 
            // FrmCompras
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
            this.Name = "FrmCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Telefonía - gestión de compras";
            this.pnlMenu.ResumeLayout(false);
            this.pnlItemsMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.nudSubtotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).EndInit();
            this.panelEstado.ResumeLayout(false);
            this.panelEstado.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

