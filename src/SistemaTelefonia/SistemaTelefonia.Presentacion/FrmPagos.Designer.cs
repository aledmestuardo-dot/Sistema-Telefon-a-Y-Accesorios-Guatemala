using System.Drawing;
using System;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    partial class FrmPagos
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlMenu, pnlLogo, pnlItemsMenu, pnlHeader, pnlContenido, pnlAcciones;
        private Panel pnlFormulario, pnlCatalogo, pnlBusqueda, pnlColumnas;
        private Label lblLogo, lblSistema, lblUsuario, lblUsuarioRol;
        private Label lblMenuUsuarios, lblMenuSucursales, lblMenuEmpleados, lblMenuClientes, lblMenuPlanes;
        private Label lblMenuLineas, lblMenuProveedores, lblMenuMarcas, lblMenuEquipos, lblMenuCompras;
        private Label lblMenuDetalleCompra, lblMenuInventario, lblMenuVentas, lblMenuDetalleVenta, lblMenuFacturacion;
        private Label lblMenuDetalleFactura, lblMenuPagos, lblMenuCardex, lblMenuReclamos;
        private Label lblTitulo, lblSubtitulo, lblFormularioTitulo;
        private Label lblCatalogoTitulo, lblRegistros, lblColumnas;
        private Label lblCodigo, lblCodigoFactura, lblTipoPago, lblMonto, lblMoneda, lblFechaPago, lblReferencia, lblEstado;

        private TextBox txtCodigo, txtReferencia, txtBuscar;
        private ComboBox cmbCodigoFactura;
        private NumericUpDown nudMonto;
        private ComboBox cmbTipoPago, cmbMoneda;
        private DateTimePicker dtpFechaPago;
        private RadioButtonTextoBlanco rdoActivo, rdoInactivo;

        private CheckBox chkSeleccionar, chkCodigo, chkCodigoFactura, chkTipoPago, chkMonto, chkMoneda, chkFechaPago, chkReferencia, chkEstado;

        private Button btnNuevo, btnGuardar, btnEditar, btnEliminar, btnLimpiar, btnBuscar;
        private Button btnVolverMenu, btnCerrarSesion;
        private DataGridView dgvPagos;
        private TableLayoutPanel layoutCampos, layoutBotones;
        private FlowLayoutPanel panelEstado, selector;

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
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.pnlColumnas = new System.Windows.Forms.Panel();
            this.selector = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSeleccionar = new System.Windows.Forms.CheckBox();
            this.chkCodigo = new System.Windows.Forms.CheckBox();
            this.chkCodigoFactura = new System.Windows.Forms.CheckBox();
            this.chkTipoPago = new System.Windows.Forms.CheckBox();
            this.chkMonto = new System.Windows.Forms.CheckBox();
            this.chkMoneda = new System.Windows.Forms.CheckBox();
            this.chkFechaPago = new System.Windows.Forms.CheckBox();
            this.chkReferencia = new System.Windows.Forms.CheckBox();
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
            this.lblCodigoFactura = new System.Windows.Forms.Label();
            this.cmbCodigoFactura = new System.Windows.Forms.ComboBox();
            this.lblTipoPago = new System.Windows.Forms.Label();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.nudMonto = new System.Windows.Forms.NumericUpDown();
            this.lblMoneda = new System.Windows.Forms.Label();
            this.cmbMoneda = new System.Windows.Forms.ComboBox();
            this.lblFechaPago = new System.Windows.Forms.Label();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelEstado = new System.Windows.Forms.FlowLayoutPanel();
            this.rdoActivo = new SistemaTelefonia.Presentacion.RadioButtonTextoBlanco();
            this.rdoInactivo = new SistemaTelefonia.Presentacion.RadioButtonTextoBlanco();
            this.lblFormularioTitulo = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.pnlItemsMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlCatalogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            this.pnlColumnas.SuspendLayout();
            this.selector.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.layoutBotones.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.layoutCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).BeginInit();
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
            this.pnlItemsMenu.Location = new System.Drawing.Point(7, 154);
            this.pnlItemsMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlItemsMenu.Name = "pnlItemsMenu";
            this.pnlItemsMenu.Size = new System.Drawing.Size(148, 427);
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
            this.lblMenuUsuarios.Text = "▸   Usuarios";
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
            this.lblMenuSucursales.Text = "▸   Sucursales";
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
            this.lblMenuEmpleados.Text = "▸   Empleados";
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
            this.lblMenuClientes.Text = "▸   Clientes";
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
            this.lblMenuPlanes.Text = "▸   Planes";
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
            this.lblMenuLineas.Text = "▸   Líneas";
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
            this.lblMenuProveedores.Text = "▸   Proveedores";
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
            this.lblMenuMarcas.Text = "▸   Marcas";
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
            this.lblMenuEquipos.Text = "▸   Equipos";
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
            this.lblMenuCompras.Text = "▸   Compras";
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
            this.lblMenuDetalleCompra.Text = "▸   Detalle compra";
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
            this.lblMenuInventario.Text = "▸   Inventario";
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
            this.lblMenuVentas.Text = "▸   Ventas";
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
            this.lblMenuDetalleVenta.Text = "▸   Detalle venta";
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
            this.lblMenuFacturacion.Text = "▸   Facturación";
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
            this.lblMenuDetalleFactura.Text = "▸   Detalle factura";
            this.lblMenuDetalleFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuPagos
            // 
            this.lblMenuPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.lblMenuPagos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblMenuPagos.Location = new System.Drawing.Point(0, 453);
            this.lblMenuPagos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMenuPagos.Name = "lblMenuPagos";
            this.lblMenuPagos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMenuPagos.Size = new System.Drawing.Size(133, 28);
            this.lblMenuPagos.TabIndex = 16;
            this.lblMenuPagos.Tag = "Pagos";
            this.lblMenuPagos.Text = "▸   Pagos";
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
            this.lblMenuCardex.Text = "▸   Cardex";
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
            this.lblMenuReclamos.Text = "▸   Reclamos";
            this.lblMenuReclamos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.btnVolverMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolverMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVolverMenu.FlatAppearance.BorderSize = 0;
            this.btnVolverMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolverMenu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVolverMenu.ForeColor = System.Drawing.Color.White;
            this.btnVolverMenu.Location = new System.Drawing.Point(7, 122);
            this.btnVolverMenu.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(148, 32);
            this.btnVolverMenu.TabIndex = 1;
            this.btnVolverMenu.Text = "⌂  Volver al menú";
            this.btnVolverMenu.UseVisualStyleBackColor = false;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(7, 581);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(148, 32);
            this.btnCerrarSesion.TabIndex = 2;
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
            this.lblUsuario.TabIndex = 3;
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
            this.lblUsuarioRol.TabIndex = 4;
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
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(148, 115);
            this.pnlLogo.TabIndex = 5;
            // 
            // lblSistema
            // 
            this.lblSistema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSistema.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSistema.ForeColor = System.Drawing.Color.White;
            this.lblSistema.Location = new System.Drawing.Point(0, 47);
            this.lblSistema.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(148, 68);
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
            this.lblSubtitulo.TabIndex = 0;
            this.lblSubtitulo.Text = "Administra los pagos asociados a las facturas";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Margin = System.Windows.Forms.Padding.Empty;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(996, 42);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Gestión de pagos";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.pnlCatalogo.Controls.Add(this.dgvPagos);
            this.pnlCatalogo.Controls.Add(this.lblRegistros);
            this.pnlCatalogo.Controls.Add(this.pnlColumnas);
            this.pnlCatalogo.Controls.Add(this.pnlBusqueda);
            this.pnlCatalogo.Controls.Add(this.lblCatalogoTitulo);
            this.pnlCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCatalogo.Location = new System.Drawing.Point(12, 281);
            this.pnlCatalogo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlCatalogo.Name = "pnlCatalogo";
            this.pnlCatalogo.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.pnlCatalogo.Size = new System.Drawing.Size(837, 289);
            this.pnlCatalogo.TabIndex = 0;
            // 
            // dgvPagos
            // 
            this.dgvPagos.AllowUserToAddRows = false;
            this.dgvPagos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(248)))));
            this.dgvPagos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPagos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPagos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPagos.ColumnHeadersHeight = 34;
            this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPagos.EnableHeadersVisualStyles = false;
            this.dgvPagos.Location = new System.Drawing.Point(8, 136);
            this.dgvPagos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvPagos.MultiSelect = false;
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.ReadOnly = true;
            this.dgvPagos.RowHeadersVisible = false;
            this.dgvPagos.RowHeadersWidth = 62;
            this.dgvPagos.RowTemplate.Height = 29;
            this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagos.Size = new System.Drawing.Size(821, 126);
            this.dgvPagos.TabIndex = 0;
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
            this.lblRegistros.Text = "Total de pagos registrados: 0";
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlColumnas
            // 
            this.pnlColumnas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.pnlColumnas.Controls.Add(this.selector);
            this.pnlColumnas.Controls.Add(this.lblColumnas);
            this.pnlColumnas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColumnas.Location = new System.Drawing.Point(8, 65);
            this.pnlColumnas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlColumnas.Name = "pnlColumnas";
            this.pnlColumnas.Padding = new System.Windows.Forms.Padding(2);
            this.pnlColumnas.Size = new System.Drawing.Size(821, 71);
            this.pnlColumnas.TabIndex = 2;
            // 
            // selector
            // 
            this.selector.AutoScroll = false;
            this.selector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.selector.Controls.Add(this.chkSeleccionar);
            this.selector.Controls.Add(this.chkCodigo);
            this.selector.Controls.Add(this.chkCodigoFactura);
            this.selector.Controls.Add(this.chkTipoPago);
            this.selector.Controls.Add(this.chkMonto);
            this.selector.Controls.Add(this.chkMoneda);
            this.selector.Controls.Add(this.chkFechaPago);
            this.selector.Controls.Add(this.chkReferencia);
            this.selector.Controls.Add(this.chkEstado);
            this.selector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selector.Location = new System.Drawing.Point(2, 21);
            this.selector.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.selector.Name = "selector";
            this.selector.Size = new System.Drawing.Size(817, 48);
            this.selector.TabIndex = 0;
            this.selector.WrapContents = false;
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
            this.chkCodigo.Text = "Código";
            this.chkCodigo.UseVisualStyleBackColor = false;
            // 
            // chkCodigoFactura
            // 
            this.chkCodigoFactura.AutoSize = true;
            this.chkCodigoFactura.BackColor = System.Drawing.Color.Transparent;
            this.chkCodigoFactura.Checked = true;
            this.chkCodigoFactura.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoFactura.ForeColor = System.Drawing.Color.White;
            this.chkCodigoFactura.Location = new System.Drawing.Point(215, 2);
            this.chkCodigoFactura.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkCodigoFactura.Name = "chkCodigoFactura";
            this.chkCodigoFactura.Size = new System.Drawing.Size(111, 24);
            this.chkCodigoFactura.TabIndex = 2;
            this.chkCodigoFactura.Text = "Cód. factura";
            this.chkCodigoFactura.UseVisualStyleBackColor = false;
            // 
            // chkTipoPago
            // 
            this.chkTipoPago.AutoSize = true;
            this.chkTipoPago.BackColor = System.Drawing.Color.Transparent;
            this.chkTipoPago.Checked = true;
            this.chkTipoPago.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTipoPago.ForeColor = System.Drawing.Color.White;
            this.chkTipoPago.Location = new System.Drawing.Point(338, 2);
            this.chkTipoPago.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkTipoPago.Name = "chkTipoPago";
            this.chkTipoPago.Size = new System.Drawing.Size(121, 24);
            this.chkTipoPago.TabIndex = 3;
            this.chkTipoPago.Text = "Tipo de pago";
            this.chkTipoPago.UseVisualStyleBackColor = false;
            // 
            // chkMonto
            // 
            this.chkMonto.AutoSize = true;
            this.chkMonto.BackColor = System.Drawing.Color.Transparent;
            this.chkMonto.Checked = true;
            this.chkMonto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMonto.ForeColor = System.Drawing.Color.White;
            this.chkMonto.Location = new System.Drawing.Point(471, 2);
            this.chkMonto.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkMonto.Name = "chkMonto";
            this.chkMonto.Size = new System.Drawing.Size(75, 24);
            this.chkMonto.TabIndex = 4;
            this.chkMonto.Text = "Monto";
            this.chkMonto.UseVisualStyleBackColor = false;
            // 
            // chkMoneda
            // 
            this.chkMoneda.AutoSize = true;
            this.chkMoneda.BackColor = System.Drawing.Color.Transparent;
            this.chkMoneda.Checked = true;
            this.chkMoneda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMoneda.ForeColor = System.Drawing.Color.White;
            this.chkMoneda.Location = new System.Drawing.Point(558, 2);
            this.chkMoneda.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkMoneda.Name = "chkMoneda";
            this.chkMoneda.Size = new System.Drawing.Size(86, 24);
            this.chkMoneda.TabIndex = 5;
            this.chkMoneda.Text = "Moneda";
            this.chkMoneda.UseVisualStyleBackColor = false;
            // 
            // chkFechaPago
            // 
            this.chkFechaPago.AutoSize = true;
            this.chkFechaPago.BackColor = System.Drawing.Color.Transparent;
            this.chkFechaPago.Checked = true;
            this.chkFechaPago.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFechaPago.ForeColor = System.Drawing.Color.White;
            this.chkFechaPago.Location = new System.Drawing.Point(656, 2);
            this.chkFechaPago.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkFechaPago.Name = "chkFechaPago";
            this.chkFechaPago.Size = new System.Drawing.Size(129, 24);
            this.chkFechaPago.TabIndex = 6;
            this.chkFechaPago.Text = "Fecha de pago";
            this.chkFechaPago.UseVisualStyleBackColor = false;
            // 
            // chkReferencia
            // 
            this.chkReferencia.AutoSize = true;
            this.chkReferencia.BackColor = System.Drawing.Color.Transparent;
            this.chkReferencia.Checked = true;
            this.chkReferencia.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReferencia.ForeColor = System.Drawing.Color.White;
            this.chkReferencia.Location = new System.Drawing.Point(797, 2);
            this.chkReferencia.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.chkReferencia.Name = "chkReferencia";
            this.chkReferencia.Size = new System.Drawing.Size(101, 24);
            this.chkReferencia.TabIndex = 7;
            this.chkReferencia.Text = "Referencia";
            this.chkReferencia.UseVisualStyleBackColor = false;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.BackColor = System.Drawing.Color.Transparent;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.ForeColor = System.Drawing.Color.White;
            this.chkEstado.Location = new System.Drawing.Point(910, 2);
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
            this.lblColumnas.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblColumnas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.lblColumnas.Location = new System.Drawing.Point(2, 2);
            this.lblColumnas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblColumnas.Name = "lblColumnas";
            this.lblColumnas.Size = new System.Drawing.Size(817, 19);
            this.lblColumnas.TabIndex = 1;
            this.lblColumnas.Text = "Opciones de visualización:";
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.BackColor = System.Drawing.Color.Transparent;
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
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(0, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(738, 30);
            this.txtBuscar.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(43)))), ((int)(((byte)(83)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.lblCatalogoTitulo.Text = "Pagos registrados";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.pnlAcciones.Controls.Add(this.layoutBotones);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAcciones.Location = new System.Drawing.Point(12, 229);
            this.pnlAcciones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(837, 52);
            this.pnlAcciones.TabIndex = 3;
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
            this.layoutBotones.TabIndex = 1;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.pnlFormulario.Size = new System.Drawing.Size(837, 217);
            this.pnlFormulario.TabIndex = 2;
            // 
            // layoutCampos
            // 
            this.layoutCampos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.layoutCampos.ColumnCount = 4;
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31F));
            this.layoutCampos.Controls.Add(this.lblCodigo, 0, 0);
            this.layoutCampos.Controls.Add(this.txtCodigo, 1, 0);
            this.layoutCampos.Controls.Add(this.lblCodigoFactura, 2, 0);
            this.layoutCampos.Controls.Add(this.cmbCodigoFactura, 3, 0);
            this.layoutCampos.Controls.Add(this.lblTipoPago, 0, 1);
            this.layoutCampos.Controls.Add(this.cmbTipoPago, 1, 1);
            this.layoutCampos.Controls.Add(this.lblMonto, 2, 1);
            this.layoutCampos.Controls.Add(this.nudMonto, 3, 1);
            this.layoutCampos.Controls.Add(this.lblMoneda, 0, 2);
            this.layoutCampos.Controls.Add(this.cmbMoneda, 1, 2);
            this.layoutCampos.Controls.Add(this.lblFechaPago, 2, 2);
            this.layoutCampos.Controls.Add(this.dtpFechaPago, 3, 2);
            this.layoutCampos.Controls.Add(this.lblReferencia, 0, 3);
            this.layoutCampos.Controls.Add(this.txtReferencia, 1, 3);
            this.layoutCampos.Controls.Add(this.lblEstado, 2, 3);
            this.layoutCampos.Controls.Add(this.panelEstado, 3, 3);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCampos.Location = new System.Drawing.Point(12, 39);
            this.layoutCampos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.Padding = new System.Windows.Forms.Padding(0, 7, 0, 3);
            this.layoutCampos.RowCount = 4;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutCampos.Size = new System.Drawing.Size(813, 166);
            this.layoutCampos.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoEllipsis = true;
            this.lblCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.White;
            this.lblCodigo.Location = new System.Drawing.Point(5, 13);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(5, 6, 7, 6);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(142, 27);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCodigo.Location = new System.Drawing.Point(159, 12);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(5, 5, 8, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(239, 30);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblCodigoFactura
            // 
            this.lblCodigoFactura.AutoEllipsis = true;
            this.lblCodigoFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoFactura.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigoFactura.ForeColor = System.Drawing.Color.White;
            this.lblCodigoFactura.Location = new System.Drawing.Point(411, 13);
            this.lblCodigoFactura.Margin = new System.Windows.Forms.Padding(5, 6, 7, 6);
            this.lblCodigoFactura.Name = "lblCodigoFactura";
            this.lblCodigoFactura.Size = new System.Drawing.Size(142, 27);
            this.lblCodigoFactura.TabIndex = 2;
            this.lblCodigoFactura.Text = "Cód. factura:";
            this.lblCodigoFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCodigoFactura
            // 
            this.cmbCodigoFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCodigoFactura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigoFactura.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigoFactura.Location = new System.Drawing.Point(565, 12);
            this.cmbCodigoFactura.Margin = new System.Windows.Forms.Padding(5, 5, 8, 5);
            this.cmbCodigoFactura.Name = "cmbCodigoFactura";
            this.cmbCodigoFactura.Size = new System.Drawing.Size(240, 31);
            this.cmbCodigoFactura.TabIndex = 3;
            // 
            // lblTipoPago
            // 
            this.lblTipoPago.AutoEllipsis = true;
            this.lblTipoPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTipoPago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTipoPago.ForeColor = System.Drawing.Color.White;
            this.lblTipoPago.Location = new System.Drawing.Point(5, 52);
            this.lblTipoPago.Margin = new System.Windows.Forms.Padding(5, 6, 7, 6);
            this.lblTipoPago.Name = "lblTipoPago";
            this.lblTipoPago.Size = new System.Drawing.Size(142, 27);
            this.lblTipoPago.TabIndex = 4;
            this.lblTipoPago.Text = "Tipo de pago:";
            this.lblTipoPago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTipoPago.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta de credito",
            "Tarjeta de debito",
            "Transferencia"});
            this.cmbTipoPago.Location = new System.Drawing.Point(159, 51);
            this.cmbTipoPago.Margin = new System.Windows.Forms.Padding(5, 5, 8, 5);
            this.cmbTipoPago.MaxLength = 100;
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(239, 31);
            this.cmbTipoPago.TabIndex = 5;
            // 
            // lblMonto
            // 
            this.lblMonto.AutoEllipsis = true;
            this.lblMonto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMonto.ForeColor = System.Drawing.Color.White;
            this.lblMonto.Location = new System.Drawing.Point(411, 52);
            this.lblMonto.Margin = new System.Windows.Forms.Padding(5, 6, 7, 6);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(142, 27);
            this.lblMonto.TabIndex = 6;
            this.lblMonto.Text = "Monto:";
            this.lblMonto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudMonto
            // 
            this.nudMonto.DecimalPlaces = 2;
            this.nudMonto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudMonto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudMonto.Location = new System.Drawing.Point(565, 51);
            this.nudMonto.Margin = new System.Windows.Forms.Padding(5, 5, 8, 5);
            this.nudMonto.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudMonto.Name = "nudMonto";
            this.nudMonto.Size = new System.Drawing.Size(240, 30);
            this.nudMonto.TabIndex = 7;
            this.nudMonto.ThousandsSeparator = true;
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoEllipsis = true;
            this.lblMoneda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMoneda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMoneda.ForeColor = System.Drawing.Color.White;
            this.lblMoneda.Location = new System.Drawing.Point(5, 91);
            this.lblMoneda.Margin = new System.Windows.Forms.Padding(5, 6, 7, 6);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(142, 27);
            this.lblMoneda.TabIndex = 8;
            this.lblMoneda.Text = "Moneda:";
            this.lblMoneda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMoneda.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMoneda.Items.AddRange(new object[] {
            "GTQ"});
            this.cmbMoneda.Location = new System.Drawing.Point(159, 90);
            this.cmbMoneda.Margin = new System.Windows.Forms.Padding(5, 5, 8, 5);
            this.cmbMoneda.MaxLength = 10;
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(239, 31);
            this.cmbMoneda.TabIndex = 9;
            // 
            // lblFechaPago
            // 
            this.lblFechaPago.AutoEllipsis = true;
            this.lblFechaPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaPago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaPago.ForeColor = System.Drawing.Color.White;
            this.lblFechaPago.Location = new System.Drawing.Point(411, 91);
            this.lblFechaPago.Margin = new System.Windows.Forms.Padding(5, 6, 7, 6);
            this.lblFechaPago.Name = "lblFechaPago";
            this.lblFechaPago.Size = new System.Drawing.Size(142, 27);
            this.lblFechaPago.TabIndex = 10;
            this.lblFechaPago.Text = "Fecha de pago:";
            this.lblFechaPago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaPago.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaPago.Location = new System.Drawing.Point(565, 90);
            this.dtpFechaPago.Margin = new System.Windows.Forms.Padding(5, 5, 8, 5);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(240, 30);
            this.dtpFechaPago.TabIndex = 11;
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoEllipsis = true;
            this.lblReferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReferencia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReferencia.ForeColor = System.Drawing.Color.White;
            this.lblReferencia.Location = new System.Drawing.Point(5, 130);
            this.lblReferencia.Margin = new System.Windows.Forms.Padding(5, 6, 7, 6);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(142, 27);
            this.lblReferencia.TabIndex = 12;
            this.lblReferencia.Text = "Referencia:";
            this.lblReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReferencia
            // 
            this.txtReferencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReferencia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferencia.Location = new System.Drawing.Point(159, 129);
            this.txtReferencia.Margin = new System.Windows.Forms.Padding(5, 5, 8, 5);
            this.txtReferencia.MaxLength = 100;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(239, 30);
            this.txtReferencia.TabIndex = 13;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoEllipsis = true;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.White;
            this.lblEstado.Location = new System.Drawing.Point(411, 130);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(5, 6, 7, 6);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(142, 27);
            this.lblEstado.TabIndex = 14;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelEstado
            // 
            this.panelEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.panelEstado.Controls.Add(this.rdoActivo);
            this.panelEstado.Controls.Add(this.rdoInactivo);
            this.panelEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstado.Location = new System.Drawing.Point(565, 128);
            this.panelEstado.Margin = new System.Windows.Forms.Padding(5, 4, 8, 4);
            this.panelEstado.Name = "panelEstado";
            this.panelEstado.Size = new System.Drawing.Size(240, 31);
            this.panelEstado.TabIndex = 15;
            this.panelEstado.WrapContents = false;
            // 
            // rdoActivo
            // 
            this.rdoActivo.AutoSize = true;
            this.rdoActivo.Checked = true;
            this.rdoActivo.ForeColor = System.Drawing.Color.White;
            this.rdoActivo.Location = new System.Drawing.Point(2, 6);
            this.rdoActivo.Margin = new System.Windows.Forms.Padding(2, 6, 12, 2);
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
            this.rdoInactivo.Location = new System.Drawing.Point(88, 6);
            this.rdoInactivo.Margin = new System.Windows.Forms.Padding(2, 6, 2, 2);
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
            this.lblFormularioTitulo.Location = new System.Drawing.Point(12, 12);
            this.lblFormularioTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormularioTitulo.Name = "lblFormularioTitulo";
            this.lblFormularioTitulo.Size = new System.Drawing.Size(813, 27);
            this.lblFormularioTitulo.TabIndex = 1;
            this.lblFormularioTitulo.Text = "Información del pago";
            // 
            // FrmPagos
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
            this.MinimumSize = new System.Drawing.Size(753, 591);
            this.Name = "FrmPagos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Telefonía - Gestión de pagos";
            this.pnlMenu.ResumeLayout(false);
            this.pnlItemsMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).EndInit();
            this.panelEstado.ResumeLayout(false);
            this.panelEstado.PerformLayout();
            this.ResumeLayout(false);

        }

 
        private void InitializeComponentLegacy()
        {
            components = new System.ComponentModel.Container();

            Color fondo = Color.FromArgb(3, 22, 43);
            Color menu = Color.FromArgb(2, 25, 47);
            Color header = Color.FromArgb(3, 43, 83);
            Color panel = Color.FromArgb(7, 35, 66);
            Color cian = Color.FromArgb(0, 203, 255);
            Color blanco = Color.White;
            Color gris = Color.FromArgb(170, 190, 210);

            SuspendLayout();

            Name = "FrmPagos";
            Text = "Sistema de Telefonía - Gestión de pagos";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(900, 700);
            Size = new Size(1250, 850);
            BackColor = fondo;
            Font = new Font("Segoe UI", 9F);
            AutoScaleMode = AutoScaleMode.Dpi;

            pnlMenu = new Panel { Dock = DockStyle.Left, Width = 195, BackColor = menu, Padding = new Padding(8) };
            pnlLogo = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = header };

            lblLogo = new Label
            {
                Text = "ST",
                Dock = DockStyle.Top,
                Height = 55,
                ForeColor = cian,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblSistema = new Label
            {
                Text = "SISTEMA TELEFONÍA",
                Dock = DockStyle.Fill,
                ForeColor = blanco,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlLogo.Controls.Add(lblSistema);
            pnlLogo.Controls.Add(lblLogo);

            pnlItemsMenu = new Panel { Dock = DockStyle.Fill, BackColor = menu, AutoScroll = true };

            lblUsuario = new Label { Text = "Administrador", Dock = DockStyle.Bottom, Height = 25, ForeColor = blanco, TextAlign = ContentAlignment.MiddleCenter };
            lblUsuarioRol = new Label { Text = "Usuario del sistema", Dock = DockStyle.Bottom, Height = 25, ForeColor = gris, TextAlign = ContentAlignment.MiddleCenter };
            // Acciones de navegación ubicadas en la barra lateral, fuera del área de trabajo.
            btnVolverMenu = new Button
            {
                Text = "⌂  Volver al menú", Dock = DockStyle.Top, Height = 38,
                FlatStyle = FlatStyle.Flat, BackColor = header, ForeColor = blanco,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnVolverMenu.FlatAppearance.BorderSize = 0;

            btnCerrarSesion = new Button
            {
                Text = "↪  Cerrar sesión", Dock = DockStyle.Bottom, Height = 38,
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(150, 55, 55),
                ForeColor = blanco, Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCerrarSesion.FlatAppearance.BorderSize = 0;

            pnlMenu.Controls.Add(pnlItemsMenu);
            pnlMenu.Controls.Add(btnVolverMenu);
            pnlMenu.Controls.Add(btnCerrarSesion);
            pnlMenu.Controls.Add(lblUsuario);
            pnlMenu.Controls.Add(lblUsuarioRol);
            pnlMenu.Controls.Add(pnlLogo);

            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 85, BackColor = header, Padding = new Padding(22, 8, 15, 8) };
            lblTitulo = new Label
            {
                Text = "Gestión de pagos",
                Dock = DockStyle.Top,
                Height = 38,
                ForeColor = blanco,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold)
            };
            lblSubtitulo = new Label
            {
                Text = "Administra los pagos asociados a las facturas",
                Dock = DockStyle.Fill,
                ForeColor = gris
            };
            pnlHeader.Controls.Add(lblSubtitulo);
            pnlHeader.Controls.Add(lblTitulo);

            pnlContenido = new Panel { Dock = DockStyle.Fill, BackColor = fondo, Padding = new Padding(15) };

            pnlFormulario = new Panel { Dock = DockStyle.Top, Height = 230, BackColor = panel, Padding = new Padding(12) };
            lblFormularioTitulo = new Label
            {
                Text = "Información del pago",
                Dock = DockStyle.Top,
                Height = 32,
                ForeColor = cian,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            layoutCampos = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 4,
                BackColor = panel,
                Padding = new Padding(0, 5, 0, 0)
            };
            layoutCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
            layoutCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31F));
            layoutCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
            layoutCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31F));
            for (int i = 0; i < 4; i++) layoutCampos.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0F));

            lblCodigo = CrearEtiqueta("Código:");
            lblCodigoFactura = CrearEtiqueta("Cód. factura:");
            lblTipoPago = CrearEtiqueta("Tipo de pago:");
            lblMonto = CrearEtiqueta("Monto:");
            lblMoneda = CrearEtiqueta("Moneda:");
            lblFechaPago = CrearEtiqueta("Fecha de pago:");
            lblReferencia = CrearEtiqueta("Referencia:");
            lblEstado = CrearEtiqueta("Estado:");

            txtCodigo = CrearTexto();
            txtCodigo.ReadOnly = true;
            txtCodigo.BackColor = Color.FromArgb(220, 230, 240);
            cmbCodigoFactura = CrearCombo(0, true);
            cmbTipoPago = CrearCombo(100, false, "Efectivo", "Tarjeta de credito", "Tarjeta de debito", "Transferencia");
            nudMonto = CrearNumero(99999999.99m, 2, true);
            cmbMoneda = CrearCombo(10, false, "GTQ");
            dtpFechaPago = CrearFecha(false);
            txtReferencia = CrearTexto();
            txtReferencia.MaxLength = 100;

            FlowLayoutPanel panelEstado = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(5),
                BackColor = panel
            };
            rdoActivo = new RadioButtonTextoBlanco { Text = "Activo", AutoSize = true, ForeColor = blanco, Checked = true, Margin = new Padding(3, 7, 15, 3) };
            rdoInactivo = new RadioButtonTextoBlanco { Text = "Inactivo", AutoSize = true, ForeColor = blanco, Margin = new Padding(3, 7, 3, 3) };
            panelEstado.Controls.Add(rdoActivo);
            panelEstado.Controls.Add(rdoInactivo);

            // Campos organizados en el orden definido en SQL Server (tabla Pagos).
            AgregarCampo(layoutCampos, lblCodigo, txtCodigo, 0, 0);
            AgregarCampo(layoutCampos, lblCodigoFactura, cmbCodigoFactura, 0, 2);
            AgregarCampo(layoutCampos, lblTipoPago, cmbTipoPago, 1, 0);
            AgregarCampo(layoutCampos, lblMonto, nudMonto, 1, 2);
            AgregarCampo(layoutCampos, lblMoneda, cmbMoneda, 2, 0);
            AgregarCampo(layoutCampos, lblFechaPago, dtpFechaPago, 2, 2);
            AgregarCampo(layoutCampos, lblReferencia, txtReferencia, 3, 0);
            AgregarCampo(layoutCampos, lblEstado, panelEstado, 3, 2);

            pnlFormulario.Controls.Add(layoutCampos);
            pnlFormulario.Controls.Add(lblFormularioTitulo);

            layoutBotones = new TableLayoutPanel { Dock = DockStyle.Top, Height = 54, ColumnCount = 5, RowCount = 1, BackColor = fondo };
            for (int i = 0; i < 5; i++) layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            btnNuevo = CrearBoton("Nuevo", Color.FromArgb(30, 136, 229));
            btnGuardar = CrearBoton("Guardar", Color.FromArgb(0, 150, 110));
            btnEditar = CrearBoton("Editar", Color.FromArgb(230, 145, 30));
            btnEliminar = CrearBoton("Eliminar", Color.FromArgb(210, 65, 75));
            btnLimpiar = CrearBoton("Limpiar", Color.FromArgb(75, 100, 125));

            layoutBotones.Controls.Add(btnNuevo, 0, 0);
            layoutBotones.Controls.Add(btnGuardar, 1, 0);
            layoutBotones.Controls.Add(btnEditar, 2, 0);
            layoutBotones.Controls.Add(btnEliminar, 3, 0);
            layoutBotones.Controls.Add(btnLimpiar, 4, 0);

            pnlCatalogo = new Panel { Dock = DockStyle.Fill, BackColor = panel, Padding = new Padding(10) };
            lblCatalogoTitulo = new Label
            {
                Text = "Pagos registrados",
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = cian,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            pnlBusqueda = new Panel { Dock = DockStyle.Top, Height = 38 };
            txtBuscar = CrearTexto();
            txtBuscar.Dock = DockStyle.Fill;
            btnBuscar = CrearBoton("Buscar", header);
            btnBuscar.Dock = DockStyle.Right;
            btnBuscar.Width = 100;
            pnlBusqueda.Controls.Add(txtBuscar);
            pnlBusqueda.Controls.Add(btnBuscar);

            pnlColumnas = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = panel, Padding = new Padding(2) };
            lblColumnas = new Label
            {
                Text = "Opciones de visualización:",
                Dock = DockStyle.Top,
                Height = 23,
                ForeColor = cian,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            FlowLayoutPanel selector = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = true,
                AutoScroll = true,
                BackColor = panel
            };

            chkSeleccionar = CrearCheck("Seleccionar", false);
            chkCodigo = CrearCheck("Código", true);
            chkCodigoFactura = CrearCheck("Cód. factura", true);
            chkTipoPago = CrearCheck("Tipo de pago", true);
            chkMonto = CrearCheck("Monto", true);
            chkMoneda = CrearCheck("Moneda", true);
            chkFechaPago = CrearCheck("Fecha de pago", true);
            chkReferencia = CrearCheck("Referencia", true);
            chkEstado = CrearCheck("Estado", true);

            selector.Controls.Add(chkSeleccionar);
            selector.Controls.Add(chkCodigo);
            selector.Controls.Add(chkCodigoFactura);
            selector.Controls.Add(chkTipoPago);
            selector.Controls.Add(chkMonto);
            selector.Controls.Add(chkMoneda);
            selector.Controls.Add(chkFechaPago);
            selector.Controls.Add(chkReferencia);
            selector.Controls.Add(chkEstado);
            pnlColumnas.Controls.Add(selector);
            pnlColumnas.Controls.Add(lblColumnas);

            dgvPagos = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = blanco,
                ForeColor = Color.FromArgb(25, 45, 65),
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false
            };
            dgvPagos.EnableHeadersVisualStyles = false;
            dgvPagos.ColumnHeadersDefaultCellStyle.BackColor = header;
            dgvPagos.ColumnHeadersDefaultCellStyle.ForeColor = blanco;
            dgvPagos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvPagos.ColumnHeadersHeight = 34;
            dgvPagos.RowTemplate.Height = 29;
            dgvPagos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 242, 248);

            // Las 8 columnas explícitas, en el mismo orden del script SQL.
            AgregarColumna("CodigoPago", "Código", typeof(int));
            AgregarColumna("CodigoFactura", "Cód. factura", typeof(int));
            AgregarColumna("TipoPago", "Tipo de pago", typeof(string));
            AgregarColumna("Monto", "Monto", typeof(decimal), "N2");
            AgregarColumna("Moneda", "Moneda", typeof(string));
            AgregarColumna("FechaPago", "Fecha de pago", typeof(DateTime), "dd/MM/yyyy HH:mm");
            AgregarColumna("Referencia", "Referencia", typeof(string));
            AgregarColumna("Estado", "Estado", typeof(bool));

            lblRegistros = new Label
            {
                Text = "Total de pagos registrados: 0",
                Dock = DockStyle.Bottom,
                Height = 23,
                ForeColor = gris,
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlCatalogo.Controls.Add(dgvPagos);
            pnlCatalogo.Controls.Add(lblRegistros);
            pnlCatalogo.Controls.Add(pnlColumnas);
            pnlCatalogo.Controls.Add(pnlBusqueda);
            pnlCatalogo.Controls.Add(lblCatalogoTitulo);

            pnlContenido.Controls.Add(pnlCatalogo);
            pnlContenido.Controls.Add(layoutBotones);
            pnlContenido.Controls.Add(pnlFormulario);

            Controls.Add(pnlContenido);
            Controls.Add(pnlHeader);
            Controls.Add(pnlMenu);

            ResumeLayout(false);
        }

        private void AgregarColumna(string propiedad, string encabezado, System.Type tipo, string formato = null)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn
            {
                Name = propiedad,
                DataPropertyName = propiedad,
                HeaderText = encabezado,
                ValueType = tipo,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            if (!string.IsNullOrEmpty(formato))
                columna.DefaultCellStyle.Format = formato;
            dgvPagos.Columns.Add(columna);
        }

        private Label CrearMenu(string texto)
        {
            return new Label
            {
                Text = texto,
                Dock = DockStyle.Top,
                Height = 34,
                Padding = new Padding(10, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
        }

        private Label CrearEtiqueta(string texto)
        {
            return new Label
            {
                Text = texto,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(4),
                AutoEllipsis = true
            };
        }

        private TextBox CrearTexto()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(4),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10F)
            };
        }

        private ComboBox CrearCombo(int maxLength, bool soloLista, params string[] opciones)
        {
            ComboBox combo = new ComboBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(4),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = soloLista ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDown,
                MaxLength = maxLength
            };
            combo.Items.AddRange(opciones);
            return combo;
        }

        private NumericUpDown CrearNumero(decimal maximo, int decimales, bool separadorMiles)
        {
            return new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(4),
                Maximum = maximo,
                Minimum = 0,
                DecimalPlaces = decimales,
                ThousandsSeparator = separadorMiles,
                Font = new Font("Segoe UI", 10F)
            };
        }

        private DateTimePicker CrearFecha(bool permiteNulo)
        {
            return new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(4),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                ShowCheckBox = permiteNulo,
                Font = new Font("Segoe UI", 10F)
            };
        }

        private Button CrearBoton(string texto, Color color)
        {
            return new Button
            {
                Text = texto,
                Dock = DockStyle.Fill,
                Margin = new Padding(3),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private CheckBox CrearCheck(string texto, bool marcado)
        {
            return new CheckBox
            {
                Text = texto,
                Checked = marcado,
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Margin = new Padding(5, 3, 9, 3)
            };
        }

        private void AgregarCampo(TableLayoutPanel tabla, Label etiqueta, Control control, int fila, int columna)
        {
            tabla.Controls.Add(etiqueta, columna, fila);
            tabla.Controls.Add(control, columna + 1, fila);
        }
    }
}
