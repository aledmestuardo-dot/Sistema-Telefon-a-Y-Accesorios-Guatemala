using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    partial class FrmCardex
    {
        private IContainer components = null;
        private Panel pnlMenu, pnlLogo, pnlItemsMenu, pnlHeader, pnlContenido;
        private Panel pnlFormulario, pnlCatalogo, pnlBusqueda, pnlColumnas, pnlAcciones;
        private Label lblLogo, lblSistema, lblUsuario, lblUsuarioRol, lblTitulo, lblSubtitulo;
        private Label lblFormularioTitulo, lblCatalogoTitulo, lblRegistros, lblColumnas;
        private Label lblCodigo;
        private Label lblCodigoInventario;
        private Label lblCodigoCompra;
        private Label lblCodigoVenta;
        private Label lblFechaActualizacion;
        private Label lblTipoMovimiento;
        private Label lblCantidad;
        private Label lblStockResultante;
        private Label lblObservaciones;
        private Label lblEstado;
        private Label lblMenuUsuarios, lblMenuSucursales, lblMenuEmpleados, lblMenuClientes, lblMenuPlanes, lblMenuLineas;
        private Label lblMenuProveedores, lblMenuMarcas, lblMenuEquipos, lblMenuCompras, lblMenuDetalleCompra, lblMenuInventario;
        private Label lblMenuVentas, lblMenuDetalleVenta, lblMenuFacturacion, lblMenuDetalleFactura, lblMenuPagos, lblMenuCardex, lblMenuReclamos;
        private TextBox txtCodigo;
        private TextBox txtObservaciones;
        private ComboBox cmbCodigoInventario;
        private ComboBox cmbCodigoCompra;
        private ComboBox cmbCodigoVenta;
        private NumericUpDown nudCantidad;
        private NumericUpDown nudStockResultante;
        private DateTimePicker dtpFechaActualizacion;
        private ComboBox cmbTipoMovimiento;
        private FlowLayoutPanel selector, panelEstado;
        private RadioButton rdoActivo, rdoInactivo;
        private CheckBox chkSeleccionar;
        private CheckBox chkCodigo;
        private CheckBox chkCodigoInventario;
        private CheckBox chkCodigoCompra;
        private CheckBox chkCodigoVenta;
        private CheckBox chkFechaActualizacion;
        private CheckBox chkTipoMovimiento;
        private CheckBox chkCantidad;
        private CheckBox chkStockResultante;
        private CheckBox chkObservaciones;
        private CheckBox chkEstado;
        private TextBox txtBuscar;
        private Button btnVolverMenu, btnCerrarSesion, btnNuevo, btnGuardar, btnEditar, btnEliminar, btnLimpiar, btnBuscar;
        private DataGridView dgvCardex;
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
            this.dgvCardex = new System.Windows.Forms.DataGridView();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.pnlColumnas = new System.Windows.Forms.Panel();
            this.selector = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSeleccionar = new System.Windows.Forms.CheckBox();
            this.chkCodigo = new System.Windows.Forms.CheckBox();
            this.chkCodigoInventario = new System.Windows.Forms.CheckBox();
            this.chkCodigoCompra = new System.Windows.Forms.CheckBox();
            this.chkCodigoVenta = new System.Windows.Forms.CheckBox();
            this.chkFechaActualizacion = new System.Windows.Forms.CheckBox();
            this.chkTipoMovimiento = new System.Windows.Forms.CheckBox();
            this.chkCantidad = new System.Windows.Forms.CheckBox();
            this.chkStockResultante = new System.Windows.Forms.CheckBox();
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
            this.lblCodigoInventario = new System.Windows.Forms.Label();
            this.cmbCodigoInventario = new System.Windows.Forms.ComboBox();
            this.lblCodigoCompra = new System.Windows.Forms.Label();
            this.cmbCodigoCompra = new System.Windows.Forms.ComboBox();
            this.lblCodigoVenta = new System.Windows.Forms.Label();
            this.cmbCodigoVenta = new System.Windows.Forms.ComboBox();
            this.lblFechaActualizacion = new System.Windows.Forms.Label();
            this.dtpFechaActualizacion = new System.Windows.Forms.DateTimePicker();
            this.lblTipoMovimiento = new System.Windows.Forms.Label();
            this.cmbTipoMovimiento = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblStockResultante = new System.Windows.Forms.Label();
            this.nudStockResultante = new System.Windows.Forms.NumericUpDown();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardex)).BeginInit();
            this.pnlColumnas.SuspendLayout();
            this.selector.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.layoutBotones.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.layoutCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockResultante)).BeginInit();
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
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
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
            this.pnlItemsMenu.Location = new System.Drawing.Point(7, 122);
            this.pnlItemsMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlItemsMenu.Name = "pnlItemsMenu";
            this.pnlItemsMenu.Size = new System.Drawing.Size(148, 463);
            this.pnlItemsMenu.TabIndex = 0;
            // 
            // lblMenuUsuarios
            // 
            this.lblMenuUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblMenuUsuarios.Location = new System.Drawing.Point(0, 0);
            this.lblMenuUsuarios.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuUsuarios.Name = "lblMenuUsuarios";
            this.lblMenuUsuarios.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuUsuarios.Size = new System.Drawing.Size(133, 28);
            this.lblMenuUsuarios.TabIndex = 0;
            this.lblMenuUsuarios.Tag = "Usuarios";
            this.lblMenuUsuarios.Text = "›   Usuarios";
            this.lblMenuUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuSucursales
            // 
            this.lblMenuSucursales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuSucursales.ForeColor = System.Drawing.Color.White;
            this.lblMenuSucursales.Location = new System.Drawing.Point(0, 28);
            this.lblMenuSucursales.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuSucursales.Name = "lblMenuSucursales";
            this.lblMenuSucursales.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuSucursales.Size = new System.Drawing.Size(133, 28);
            this.lblMenuSucursales.TabIndex = 1;
            this.lblMenuSucursales.Tag = "Sucursales";
            this.lblMenuSucursales.Text = "›   Sucursales";
            this.lblMenuSucursales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuEmpleados
            // 
            this.lblMenuEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuEmpleados.ForeColor = System.Drawing.Color.White;
            this.lblMenuEmpleados.Location = new System.Drawing.Point(0, 57);
            this.lblMenuEmpleados.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuEmpleados.Name = "lblMenuEmpleados";
            this.lblMenuEmpleados.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuEmpleados.Size = new System.Drawing.Size(133, 28);
            this.lblMenuEmpleados.TabIndex = 2;
            this.lblMenuEmpleados.Tag = "Empleados";
            this.lblMenuEmpleados.Text = "›   Empleados";
            this.lblMenuEmpleados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuClientes
            // 
            this.lblMenuClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuClientes.ForeColor = System.Drawing.Color.White;
            this.lblMenuClientes.Location = new System.Drawing.Point(0, 85);
            this.lblMenuClientes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuClientes.Name = "lblMenuClientes";
            this.lblMenuClientes.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuClientes.Size = new System.Drawing.Size(133, 28);
            this.lblMenuClientes.TabIndex = 3;
            this.lblMenuClientes.Tag = "Clientes";
            this.lblMenuClientes.Text = "›   Clientes";
            this.lblMenuClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuPlanes
            // 
            this.lblMenuPlanes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuPlanes.ForeColor = System.Drawing.Color.White;
            this.lblMenuPlanes.Location = new System.Drawing.Point(0, 113);
            this.lblMenuPlanes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuPlanes.Name = "lblMenuPlanes";
            this.lblMenuPlanes.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuPlanes.Size = new System.Drawing.Size(133, 28);
            this.lblMenuPlanes.TabIndex = 4;
            this.lblMenuPlanes.Tag = "Planes";
            this.lblMenuPlanes.Text = "›   Planes";
            this.lblMenuPlanes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuLineas
            // 
            this.lblMenuLineas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuLineas.ForeColor = System.Drawing.Color.White;
            this.lblMenuLineas.Location = new System.Drawing.Point(0, 142);
            this.lblMenuLineas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuLineas.Name = "lblMenuLineas";
            this.lblMenuLineas.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuLineas.Size = new System.Drawing.Size(133, 28);
            this.lblMenuLineas.TabIndex = 5;
            this.lblMenuLineas.Tag = "Lineas";
            this.lblMenuLineas.Text = "›   Líneas";
            this.lblMenuLineas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuProveedores
            // 
            this.lblMenuProveedores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuProveedores.ForeColor = System.Drawing.Color.White;
            this.lblMenuProveedores.Location = new System.Drawing.Point(0, 170);
            this.lblMenuProveedores.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuProveedores.Name = "lblMenuProveedores";
            this.lblMenuProveedores.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuProveedores.Size = new System.Drawing.Size(133, 28);
            this.lblMenuProveedores.TabIndex = 6;
            this.lblMenuProveedores.Tag = "Proveedores";
            this.lblMenuProveedores.Text = "›   Proveedores";
            this.lblMenuProveedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuMarcas
            // 
            this.lblMenuMarcas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuMarcas.ForeColor = System.Drawing.Color.White;
            this.lblMenuMarcas.Location = new System.Drawing.Point(0, 198);
            this.lblMenuMarcas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuMarcas.Name = "lblMenuMarcas";
            this.lblMenuMarcas.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuMarcas.Size = new System.Drawing.Size(133, 28);
            this.lblMenuMarcas.TabIndex = 7;
            this.lblMenuMarcas.Tag = "Marcas";
            this.lblMenuMarcas.Text = "›   Marcas";
            this.lblMenuMarcas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuEquipos
            // 
            this.lblMenuEquipos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuEquipos.ForeColor = System.Drawing.Color.White;
            this.lblMenuEquipos.Location = new System.Drawing.Point(0, 227);
            this.lblMenuEquipos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuEquipos.Name = "lblMenuEquipos";
            this.lblMenuEquipos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuEquipos.Size = new System.Drawing.Size(133, 28);
            this.lblMenuEquipos.TabIndex = 8;
            this.lblMenuEquipos.Tag = "Equipos";
            this.lblMenuEquipos.Text = "›   Equipos";
            this.lblMenuEquipos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuCompras
            // 
            this.lblMenuCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuCompras.ForeColor = System.Drawing.Color.White;
            this.lblMenuCompras.Location = new System.Drawing.Point(0, 255);
            this.lblMenuCompras.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuCompras.Name = "lblMenuCompras";
            this.lblMenuCompras.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuCompras.Size = new System.Drawing.Size(133, 28);
            this.lblMenuCompras.TabIndex = 9;
            this.lblMenuCompras.Tag = "Compras";
            this.lblMenuCompras.Text = "›   Compras";
            this.lblMenuCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleCompra
            // 
            this.lblMenuDetalleCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuDetalleCompra.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleCompra.Location = new System.Drawing.Point(0, 283);
            this.lblMenuDetalleCompra.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuDetalleCompra.Name = "lblMenuDetalleCompra";
            this.lblMenuDetalleCompra.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuDetalleCompra.Size = new System.Drawing.Size(133, 28);
            this.lblMenuDetalleCompra.TabIndex = 10;
            this.lblMenuDetalleCompra.Tag = "DetalleCompra";
            this.lblMenuDetalleCompra.Text = "›   Detalle compra";
            this.lblMenuDetalleCompra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuInventario
            // 
            this.lblMenuInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuInventario.ForeColor = System.Drawing.Color.White;
            this.lblMenuInventario.Location = new System.Drawing.Point(0, 312);
            this.lblMenuInventario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuInventario.Name = "lblMenuInventario";
            this.lblMenuInventario.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuInventario.Size = new System.Drawing.Size(133, 28);
            this.lblMenuInventario.TabIndex = 11;
            this.lblMenuInventario.Tag = "Inventario";
            this.lblMenuInventario.Text = "›   Inventario";
            this.lblMenuInventario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuVentas
            // 
            this.lblMenuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuVentas.ForeColor = System.Drawing.Color.White;
            this.lblMenuVentas.Location = new System.Drawing.Point(0, 340);
            this.lblMenuVentas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuVentas.Name = "lblMenuVentas";
            this.lblMenuVentas.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuVentas.Size = new System.Drawing.Size(133, 28);
            this.lblMenuVentas.TabIndex = 12;
            this.lblMenuVentas.Tag = "Ventas";
            this.lblMenuVentas.Text = "›   Ventas";
            this.lblMenuVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleVenta
            // 
            this.lblMenuDetalleVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuDetalleVenta.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleVenta.Location = new System.Drawing.Point(0, 368);
            this.lblMenuDetalleVenta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuDetalleVenta.Name = "lblMenuDetalleVenta";
            this.lblMenuDetalleVenta.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuDetalleVenta.Size = new System.Drawing.Size(133, 28);
            this.lblMenuDetalleVenta.TabIndex = 13;
            this.lblMenuDetalleVenta.Tag = "DetalleVenta";
            this.lblMenuDetalleVenta.Text = "›   Detalle venta";
            this.lblMenuDetalleVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuFacturacion
            // 
            this.lblMenuFacturacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuFacturacion.ForeColor = System.Drawing.Color.White;
            this.lblMenuFacturacion.Location = new System.Drawing.Point(0, 397);
            this.lblMenuFacturacion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuFacturacion.Name = "lblMenuFacturacion";
            this.lblMenuFacturacion.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuFacturacion.Size = new System.Drawing.Size(133, 28);
            this.lblMenuFacturacion.TabIndex = 14;
            this.lblMenuFacturacion.Tag = "Facturacion";
            this.lblMenuFacturacion.Text = "›   Facturación";
            this.lblMenuFacturacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDetalleFactura
            // 
            this.lblMenuDetalleFactura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuDetalleFactura.ForeColor = System.Drawing.Color.White;
            this.lblMenuDetalleFactura.Location = new System.Drawing.Point(0, 425);
            this.lblMenuDetalleFactura.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuDetalleFactura.Name = "lblMenuDetalleFactura";
            this.lblMenuDetalleFactura.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuDetalleFactura.Size = new System.Drawing.Size(133, 28);
            this.lblMenuDetalleFactura.TabIndex = 15;
            this.lblMenuDetalleFactura.Tag = "DetalleFactura";
            this.lblMenuDetalleFactura.Text = "›   Detalle factura";
            this.lblMenuDetalleFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuPagos
            // 
            this.lblMenuPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuPagos.ForeColor = System.Drawing.Color.White;
            this.lblMenuPagos.Location = new System.Drawing.Point(0, 453);
            this.lblMenuPagos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuPagos.Name = "lblMenuPagos";
            this.lblMenuPagos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuPagos.Size = new System.Drawing.Size(133, 28);
            this.lblMenuPagos.TabIndex = 16;
            this.lblMenuPagos.Tag = "Pagos";
            this.lblMenuPagos.Text = "›   Pagos";
            this.lblMenuPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuCardex
            // 
            this.lblMenuCardex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuCardex.ForeColor = System.Drawing.Color.White;
            this.lblMenuCardex.Location = new System.Drawing.Point(0, 482);
            this.lblMenuCardex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuCardex.Name = "lblMenuCardex";
            this.lblMenuCardex.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuCardex.Size = new System.Drawing.Size(133, 28);
            this.lblMenuCardex.TabIndex = 17;
            this.lblMenuCardex.Tag = "Cardex";
            this.lblMenuCardex.Text = "›   Cardex";
            this.lblMenuCardex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuReclamos
            // 
            this.lblMenuReclamos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.lblMenuReclamos.ForeColor = System.Drawing.Color.White;
            this.lblMenuReclamos.Location = new System.Drawing.Point(0, 510);
            this.lblMenuReclamos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuReclamos.Name = "lblMenuReclamos";
            this.lblMenuReclamos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuReclamos.Size = new System.Drawing.Size(133, 28);
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
            this.btnVolverMenu.Location = new System.Drawing.Point(7, 90);
            this.btnVolverMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(148, 32);
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
            this.pnlLogo.Location = new System.Drawing.Point(7, 7);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(148, 83);
            this.pnlLogo.TabIndex = 2;
            // 
            // lblSistema
            // 
            this.lblSistema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSistema.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSistema.ForeColor = System.Drawing.Color.White;
            this.lblSistema.Location = new System.Drawing.Point(0, 47);
            this.lblSistema.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(148, 36);
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
            this.lblLogo.Size = new System.Drawing.Size(148, 47);
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
            this.btnCerrarSesion.Location = new System.Drawing.Point(7, 585);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(148, 30);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "↪  Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            // 
            // lblUsuarioRol
            // 
            this.lblUsuarioRol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblUsuarioRol.Location = new System.Drawing.Point(7, 615);
            this.lblUsuarioRol.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsuarioRol.Name = "lblUsuarioRol";
            this.lblUsuarioRol.Size = new System.Drawing.Size(148, 20);
            this.lblUsuarioRol.TabIndex = 4;
            this.lblUsuarioRol.Text = "Usuario del sistema";
            this.lblUsuarioRol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(7, 635);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(148, 20);
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
            this.pnlHeader.Location = new System.Drawing.Point(162, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(18, 7, 12, 7);
            this.pnlHeader.Size = new System.Drawing.Size(861, 80);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(18, 42);
            this.lblSubtitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lblSubtitulo.Size = new System.Drawing.Size(831, 31);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Consulta los movimientos y saldos de inventario";
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
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Cardex";
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlContenido.Controls.Add(this.pnlCatalogo);
            this.pnlContenido.Controls.Add(this.pnlAcciones);
            this.pnlContenido.Controls.Add(this.pnlFormulario);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(162, 80);
            this.pnlContenido.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.pnlContenido.Size = new System.Drawing.Size(861, 582);
            this.pnlContenido.TabIndex = 0;
            // 
            // pnlCatalogo
            // 
            this.pnlCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.pnlCatalogo.Controls.Add(this.dgvCardex);
            this.pnlCatalogo.Controls.Add(this.lblRegistros);
            this.pnlCatalogo.Controls.Add(this.pnlColumnas);
            this.pnlCatalogo.Controls.Add(this.pnlBusqueda);
            this.pnlCatalogo.Controls.Add(this.lblCatalogoTitulo);
            this.pnlCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCatalogo.Location = new System.Drawing.Point(12, 314);
            this.pnlCatalogo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlCatalogo.Name = "pnlCatalogo";
            this.pnlCatalogo.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.pnlCatalogo.Size = new System.Drawing.Size(837, 256);
            this.pnlCatalogo.TabIndex = 0;
            // 
            // dgvCardex
            // 
            this.dgvCardex.AllowUserToAddRows = false;
            this.dgvCardex.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(248)))));
            this.dgvCardex.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCardex.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCardex.BackgroundColor = System.Drawing.Color.White;
            this.dgvCardex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCardex.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCardex.ColumnHeadersHeight = 34;
            this.dgvCardex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCardex.EnableHeadersVisualStyles = false;
            this.dgvCardex.Location = new System.Drawing.Point(8, 137);
            this.dgvCardex.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvCardex.MultiSelect = false;
            this.dgvCardex.Name = "dgvCardex";
            this.dgvCardex.ReadOnly = true;
            this.dgvCardex.RowHeadersVisible = false;
            this.dgvCardex.RowHeadersWidth = 62;
            this.dgvCardex.RowTemplate.Height = 29;
            this.dgvCardex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCardex.Size = new System.Drawing.Size(821, 91);
            this.dgvCardex.TabIndex = 0;
            // 
            // lblRegistros
            // 
            this.lblRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.lblRegistros.Location = new System.Drawing.Point(8, 228);
            this.lblRegistros.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(821, 20);
            this.lblRegistros.TabIndex = 1;
            this.lblRegistros.Text = "Total de movimientos registrados: 0";
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlColumnas
            // 
            this.pnlColumnas.Controls.Add(this.selector);
            this.pnlColumnas.Controls.Add(this.lblColumnas);
            this.pnlColumnas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColumnas.Location = new System.Drawing.Point(8, 65);
            this.pnlColumnas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlColumnas.Name = "pnlColumnas";
            this.pnlColumnas.Size = new System.Drawing.Size(821, 72);
            this.pnlColumnas.TabIndex = 2;
            // 
            // selector
            // 
            this.selector.AutoScroll = true;
            this.selector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.selector.Controls.Add(this.chkSeleccionar);
            this.selector.Controls.Add(this.chkCodigo);
            this.selector.Controls.Add(this.chkCodigoInventario);
            this.selector.Controls.Add(this.chkCodigoCompra);
            this.selector.Controls.Add(this.chkCodigoVenta);
            this.selector.Controls.Add(this.chkFechaActualizacion);
            this.selector.Controls.Add(this.chkTipoMovimiento);
            this.selector.Controls.Add(this.chkCantidad);
            this.selector.Controls.Add(this.chkStockResultante);
            this.selector.Controls.Add(this.chkObservaciones);
            this.selector.Controls.Add(this.chkEstado);
            this.selector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selector.Location = new System.Drawing.Point(0, 20);
            this.selector.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.selector.Name = "selector";
            this.selector.Size = new System.Drawing.Size(821, 52);
            this.selector.TabIndex = 0;
            // 
            // chkSeleccionar
            // 
            this.chkSeleccionar.AutoSize = true;
            this.chkSeleccionar.ForeColor = System.Drawing.Color.White;
            this.chkSeleccionar.Location = new System.Drawing.Point(3, 2);
            this.chkSeleccionar.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkSeleccionar.Name = "chkSeleccionar";
            this.chkSeleccionar.Size = new System.Drawing.Size(107, 24);
            this.chkSeleccionar.TabIndex = 0;
            this.chkSeleccionar.Text = "Seleccionar";
            // 
            // chkCodigo
            // 
            this.chkCodigo.AutoSize = true;
            this.chkCodigo.Checked = true;
            this.chkCodigo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigo.ForeColor = System.Drawing.Color.White;
            this.chkCodigo.Location = new System.Drawing.Point(121, 2);
            this.chkCodigo.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkCodigo.Name = "chkCodigo";
            this.chkCodigo.Size = new System.Drawing.Size(80, 24);
            this.chkCodigo.TabIndex = 1;
            this.chkCodigo.Text = "Código";
            // 
            // chkCodigoInventario
            // 
            this.chkCodigoInventario.AutoSize = true;
            this.chkCodigoInventario.Checked = true;
            this.chkCodigoInventario.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoInventario.ForeColor = System.Drawing.Color.White;
            this.chkCodigoInventario.Location = new System.Drawing.Point(212, 2);
            this.chkCodigoInventario.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkCodigoInventario.Name = "chkCodigoInventario";
            this.chkCodigoInventario.Size = new System.Drawing.Size(131, 24);
            this.chkCodigoInventario.TabIndex = 2;
            this.chkCodigoInventario.Text = "Cód. inventario";
            // 
            // chkCodigoCompra
            // 
            this.chkCodigoCompra.AutoSize = true;
            this.chkCodigoCompra.Checked = true;
            this.chkCodigoCompra.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoCompra.ForeColor = System.Drawing.Color.White;
            this.chkCodigoCompra.Location = new System.Drawing.Point(354, 2);
            this.chkCodigoCompra.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkCodigoCompra.Name = "chkCodigoCompra";
            this.chkCodigoCompra.Size = new System.Drawing.Size(116, 24);
            this.chkCodigoCompra.TabIndex = 3;
            this.chkCodigoCompra.Text = "Cód. compra";
            // 
            // chkCodigoVenta
            // 
            this.chkCodigoVenta.AutoSize = true;
            this.chkCodigoVenta.Checked = true;
            this.chkCodigoVenta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoVenta.ForeColor = System.Drawing.Color.White;
            this.chkCodigoVenta.Location = new System.Drawing.Point(481, 2);
            this.chkCodigoVenta.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkCodigoVenta.Name = "chkCodigoVenta";
            this.chkCodigoVenta.Size = new System.Drawing.Size(101, 24);
            this.chkCodigoVenta.TabIndex = 4;
            this.chkCodigoVenta.Text = "Cód. venta";
            // 
            // chkFechaActualizacion
            // 
            this.chkFechaActualizacion.AutoSize = true;
            this.chkFechaActualizacion.Checked = true;
            this.chkFechaActualizacion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFechaActualizacion.ForeColor = System.Drawing.Color.White;
            this.chkFechaActualizacion.Location = new System.Drawing.Point(593, 2);
            this.chkFechaActualizacion.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkFechaActualizacion.Name = "chkFechaActualizacion";
            this.chkFechaActualizacion.Size = new System.Drawing.Size(181, 24);
            this.chkFechaActualizacion.TabIndex = 5;
            this.chkFechaActualizacion.Text = "Fecha de actualización";
            // 
            // chkTipoMovimiento
            // 
            this.chkTipoMovimiento.AutoSize = true;
            this.chkTipoMovimiento.Checked = true;
            this.chkTipoMovimiento.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTipoMovimiento.ForeColor = System.Drawing.Color.White;
            this.chkTipoMovimiento.Location = new System.Drawing.Point(3, 30);
            this.chkTipoMovimiento.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkTipoMovimiento.Name = "chkTipoMovimiento";
            this.chkTipoMovimiento.Size = new System.Drawing.Size(166, 24);
            this.chkTipoMovimiento.TabIndex = 6;
            this.chkTipoMovimiento.Text = "Tipo de movimiento";
            // 
            // chkCantidad
            // 
            this.chkCantidad.AutoSize = true;
            this.chkCantidad.Checked = true;
            this.chkCantidad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCantidad.ForeColor = System.Drawing.Color.White;
            this.chkCantidad.Location = new System.Drawing.Point(180, 30);
            this.chkCantidad.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkCantidad.Name = "chkCantidad";
            this.chkCantidad.Size = new System.Drawing.Size(91, 24);
            this.chkCantidad.TabIndex = 7;
            this.chkCantidad.Text = "Cantidad";
            // 
            // chkStockResultante
            // 
            this.chkStockResultante.AutoSize = true;
            this.chkStockResultante.Checked = true;
            this.chkStockResultante.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStockResultante.ForeColor = System.Drawing.Color.White;
            this.chkStockResultante.Location = new System.Drawing.Point(282, 30);
            this.chkStockResultante.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkStockResultante.Name = "chkStockResultante";
            this.chkStockResultante.Size = new System.Drawing.Size(136, 24);
            this.chkStockResultante.TabIndex = 8;
            this.chkStockResultante.Text = "Stock resultante";
            // 
            // chkObservaciones
            // 
            this.chkObservaciones.AutoSize = true;
            this.chkObservaciones.Checked = true;
            this.chkObservaciones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkObservaciones.ForeColor = System.Drawing.Color.White;
            this.chkObservaciones.Location = new System.Drawing.Point(429, 30);
            this.chkObservaciones.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkObservaciones.Name = "chkObservaciones";
            this.chkObservaciones.Size = new System.Drawing.Size(127, 24);
            this.chkObservaciones.TabIndex = 9;
            this.chkObservaciones.Text = "Observaciones";
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.ForeColor = System.Drawing.Color.White;
            this.chkEstado.Location = new System.Drawing.Point(567, 30);
            this.chkEstado.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(76, 24);
            this.chkEstado.TabIndex = 10;
            this.chkEstado.Text = "Estado";
            // 
            // lblColumnas
            // 
            this.lblColumnas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblColumnas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblColumnas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblColumnas.Location = new System.Drawing.Point(0, 0);
            this.lblColumnas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblColumnas.Name = "lblColumnas";
            this.lblColumnas.Size = new System.Drawing.Size(821, 20);
            this.lblColumnas.TabIndex = 1;
            this.lblColumnas.Text = "Opciones de visualización:";
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.txtBuscar);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusqueda.Location = new System.Drawing.Point(8, 33);
            this.pnlBusqueda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(821, 32);
            this.pnlBusqueda.TabIndex = 3;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(0, 0);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(738, 30);
            this.txtBuscar.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.btnBuscar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(738, 0);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(83, 32);
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
            this.lblCatalogoTitulo.Text = "Movimientos registrados";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlAcciones.Controls.Add(this.layoutBotones);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAcciones.Location = new System.Drawing.Point(12, 262);
            this.pnlAcciones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(837, 52);
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
            this.layoutBotones.Location = new System.Drawing.Point(42, 3);
            this.layoutBotones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.pnlFormulario.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlFormulario.Name = "pnlFormulario";
            this.pnlFormulario.Padding = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.pnlFormulario.Size = new System.Drawing.Size(837, 250);
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
            this.layoutCampos.Controls.Add(this.lblCodigoInventario, 2, 0);
            this.layoutCampos.Controls.Add(this.cmbCodigoInventario, 3, 0);
            this.layoutCampos.Controls.Add(this.lblCodigoCompra, 0, 1);
            this.layoutCampos.Controls.Add(this.cmbCodigoCompra, 1, 1);
            this.layoutCampos.Controls.Add(this.lblCodigoVenta, 2, 1);
            this.layoutCampos.Controls.Add(this.cmbCodigoVenta, 3, 1);
            this.layoutCampos.Controls.Add(this.lblFechaActualizacion, 0, 2);
            this.layoutCampos.Controls.Add(this.dtpFechaActualizacion, 1, 2);
            this.layoutCampos.Controls.Add(this.lblTipoMovimiento, 2, 2);
            this.layoutCampos.Controls.Add(this.cmbTipoMovimiento, 3, 2);
            this.layoutCampos.Controls.Add(this.lblCantidad, 0, 3);
            this.layoutCampos.Controls.Add(this.nudCantidad, 1, 3);
            this.layoutCampos.Controls.Add(this.lblStockResultante, 2, 3);
            this.layoutCampos.Controls.Add(this.nudStockResultante, 3, 3);
            this.layoutCampos.Controls.Add(this.lblObservaciones, 0, 4);
            this.layoutCampos.Controls.Add(this.txtObservaciones, 1, 4);
            this.layoutCampos.Controls.Add(this.lblEstado, 2, 4);
            this.layoutCampos.Controls.Add(this.panelEstado, 3, 4);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCampos.Location = new System.Drawing.Point(12, 39);
            this.layoutCampos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.RowCount = 5;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutCampos.Size = new System.Drawing.Size(813, 199);
            this.layoutCampos.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.White;
            this.lblCodigo.Location = new System.Drawing.Point(2, 0);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(191, 39);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(242)))));
            this.txtCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCodigo.Location = new System.Drawing.Point(198, 7);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(205, 30);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblCodigoInventario
            // 
            this.lblCodigoInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoInventario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoInventario.ForeColor = System.Drawing.Color.White;
            this.lblCodigoInventario.Location = new System.Drawing.Point(408, 0);
            this.lblCodigoInventario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCodigoInventario.Name = "lblCodigoInventario";
            this.lblCodigoInventario.Size = new System.Drawing.Size(191, 39);
            this.lblCodigoInventario.TabIndex = 2;
            this.lblCodigoInventario.Text = "Cód. inventario:";
            this.lblCodigoInventario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoInventario
            // 
            this.cmbCodigoInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoInventario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoInventario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoInventario.Location = new System.Drawing.Point(604, 7);
            this.cmbCodigoInventario.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.cmbCodigoInventario.Name = "cmbCodigoInventario";
            this.cmbCodigoInventario.Size = new System.Drawing.Size(206, 31);
            this.cmbCodigoInventario.TabIndex = 3;
            // 
            // lblCodigoCompra
            // 
            this.lblCodigoCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoCompra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoCompra.ForeColor = System.Drawing.Color.White;
            this.lblCodigoCompra.Location = new System.Drawing.Point(2, 39);
            this.lblCodigoCompra.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCodigoCompra.Name = "lblCodigoCompra";
            this.lblCodigoCompra.Size = new System.Drawing.Size(191, 39);
            this.lblCodigoCompra.TabIndex = 4;
            this.lblCodigoCompra.Text = "Cód. compra:";
            this.lblCodigoCompra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoCompra
            // 
            this.cmbCodigoCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoCompra.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoCompra.Location = new System.Drawing.Point(198, 46);
            this.cmbCodigoCompra.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.cmbCodigoCompra.Name = "cmbCodigoCompra";
            this.cmbCodigoCompra.Size = new System.Drawing.Size(205, 31);
            this.cmbCodigoCompra.TabIndex = 5;
            // 
            // lblCodigoVenta
            // 
            this.lblCodigoVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoVenta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoVenta.ForeColor = System.Drawing.Color.White;
            this.lblCodigoVenta.Location = new System.Drawing.Point(408, 39);
            this.lblCodigoVenta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCodigoVenta.Name = "lblCodigoVenta";
            this.lblCodigoVenta.Size = new System.Drawing.Size(191, 39);
            this.lblCodigoVenta.TabIndex = 6;
            this.lblCodigoVenta.Text = "Cód. venta:";
            this.lblCodigoVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoVenta
            // 
            this.cmbCodigoVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoVenta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoVenta.Location = new System.Drawing.Point(604, 46);
            this.cmbCodigoVenta.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.cmbCodigoVenta.Name = "cmbCodigoVenta";
            this.cmbCodigoVenta.Size = new System.Drawing.Size(206, 31);
            this.cmbCodigoVenta.TabIndex = 7;
            // 
            // lblFechaActualizacion
            // 
            this.lblFechaActualizacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaActualizacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaActualizacion.ForeColor = System.Drawing.Color.White;
            this.lblFechaActualizacion.Location = new System.Drawing.Point(2, 78);
            this.lblFechaActualizacion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFechaActualizacion.Name = "lblFechaActualizacion";
            this.lblFechaActualizacion.Size = new System.Drawing.Size(191, 39);
            this.lblFechaActualizacion.TabIndex = 8;
            this.lblFechaActualizacion.Text = "Fecha de actualización:";
            this.lblFechaActualizacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaActualizacion
            // 
            this.dtpFechaActualizacion.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaActualizacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaActualizacion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaActualizacion.Location = new System.Drawing.Point(198, 85);
            this.dtpFechaActualizacion.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.dtpFechaActualizacion.Name = "dtpFechaActualizacion";
            this.dtpFechaActualizacion.Size = new System.Drawing.Size(205, 27);
            this.dtpFechaActualizacion.TabIndex = 9;
            // 
            // lblTipoMovimiento
            // 
            this.lblTipoMovimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTipoMovimiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTipoMovimiento.ForeColor = System.Drawing.Color.White;
            this.lblTipoMovimiento.Location = new System.Drawing.Point(408, 78);
            this.lblTipoMovimiento.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTipoMovimiento.Name = "lblTipoMovimiento";
            this.lblTipoMovimiento.Size = new System.Drawing.Size(191, 39);
            this.lblTipoMovimiento.TabIndex = 10;
            this.lblTipoMovimiento.Text = "Tipo de movimiento:";
            this.lblTipoMovimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTipoMovimiento
            // 
            this.cmbTipoMovimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTipoMovimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoMovimiento.Location = new System.Drawing.Point(604, 85);
            this.cmbTipoMovimiento.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.cmbTipoMovimiento.Name = "cmbTipoMovimiento";
            this.cmbTipoMovimiento.Size = new System.Drawing.Size(206, 31);
            this.cmbTipoMovimiento.TabIndex = 11;
            // 
            // lblCantidad
            // 
            this.lblCantidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.ForeColor = System.Drawing.Color.White;
            this.lblCantidad.Location = new System.Drawing.Point(2, 117);
            this.lblCantidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(191, 39);
            this.lblCantidad.TabIndex = 12;
            this.lblCantidad.Text = "Cantidad:";
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudCantidad
            // 
            this.nudCantidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCantidad.Location = new System.Drawing.Point(198, 124);
            this.nudCantidad.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.nudCantidad.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(205, 30);
            this.nudCantidad.TabIndex = 13;
            // 
            // lblStockResultante
            // 
            this.lblStockResultante.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStockResultante.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStockResultante.ForeColor = System.Drawing.Color.White;
            this.lblStockResultante.Location = new System.Drawing.Point(408, 117);
            this.lblStockResultante.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStockResultante.Name = "lblStockResultante";
            this.lblStockResultante.Size = new System.Drawing.Size(191, 39);
            this.lblStockResultante.TabIndex = 14;
            this.lblStockResultante.Text = "Stock resultante:";
            this.lblStockResultante.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudStockResultante
            // 
            this.nudStockResultante.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudStockResultante.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudStockResultante.Location = new System.Drawing.Point(604, 124);
            this.nudStockResultante.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.nudStockResultante.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudStockResultante.Name = "nudStockResultante";
            this.nudStockResultante.Size = new System.Drawing.Size(206, 30);
            this.nudStockResultante.TabIndex = 15;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblObservaciones.ForeColor = System.Drawing.Color.White;
            this.lblObservaciones.Location = new System.Drawing.Point(2, 156);
            this.lblObservaciones.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(191, 43);
            this.lblObservaciones.TabIndex = 16;
            this.lblObservaciones.Text = "Observaciones:";
            this.lblObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtObservaciones.Location = new System.Drawing.Point(198, 163);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(205, 30);
            this.txtObservaciones.TabIndex = 17;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.White;
            this.lblEstado.Location = new System.Drawing.Point(408, 156);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(191, 43);
            this.lblEstado.TabIndex = 18;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelEstado
            // 
            this.panelEstado.Controls.Add(this.rdoActivo);
            this.panelEstado.Controls.Add(this.rdoInactivo);
            this.panelEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstado.Location = new System.Drawing.Point(603, 158);
            this.panelEstado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelEstado.Name = "panelEstado";
            this.panelEstado.Size = new System.Drawing.Size(208, 39);
            this.panelEstado.TabIndex = 19;
            this.panelEstado.WrapContents = false;
            // 
            // rdoActivo
            // 
            this.rdoActivo.AutoSize = true;
            this.rdoActivo.Checked = true;
            this.rdoActivo.ForeColor = System.Drawing.Color.White;
            this.rdoActivo.Location = new System.Drawing.Point(2, 7);
            this.rdoActivo.Margin = new System.Windows.Forms.Padding(2, 7, 15, 2);
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
            this.rdoInactivo.Location = new System.Drawing.Point(91, 7);
            this.rdoInactivo.Margin = new System.Windows.Forms.Padding(2, 7, 2, 2);
            this.rdoInactivo.Name = "rdoInactivo";
            this.rdoInactivo.Size = new System.Drawing.Size(82, 24);
            this.rdoInactivo.TabIndex = 1;
            this.rdoInactivo.Text = "Inactivo";
            // 
            // lblFormularioTitulo
            // 
            this.lblFormularioTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormularioTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblFormularioTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblFormularioTitulo.Location = new System.Drawing.Point(12, 12);
            this.lblFormularioTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormularioTitulo.Name = "lblFormularioTitulo";
            this.lblFormularioTitulo.Size = new System.Drawing.Size(813, 27);
            this.lblFormularioTitulo.TabIndex = 1;
            this.lblFormularioTitulo.Text = "Información del movimiento";
            // 
            // FrmCardex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1023, 662);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(750, 585);
            this.Name = "FrmCardex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Telefonía - gestión de cardex";
            this.pnlMenu.ResumeLayout(false);
            this.pnlItemsMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardex)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.nudStockResultante)).EndInit();
            this.panelEstado.ResumeLayout(false);
            this.panelEstado.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

