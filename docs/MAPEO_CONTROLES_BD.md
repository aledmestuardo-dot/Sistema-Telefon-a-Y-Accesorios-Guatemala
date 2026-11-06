# Mapeo de formularios a SQL Server

**Base inspeccionada:** `BD_TelefoniaAccesoriosSA`  
**Fecha de inspección:** 2026-10-25  
**Fuente del esquema:** metadatos de SQL Server (`sys.tables`, `sys.columns`, `sys.types`, `sys.key_constraints`, `sys.foreign_keys` y `sys.foreign_key_columns`).  
**Fuente de controles:** clases `Frm*.cs`, diseñadores `Frm*.Designer.cs` y recursos `Frm*.resx` bajo `src/SistemaTelefonia/SistemaTelefonia.Presentacion/`.

El esquema confirmado tiene 19 tablas, 169 columnas, 19 claves primarias y 26 claves foráneas. Los tipos y la nulabilidad son los informados por SQL Server. `PK, IDENTITY` marca las claves primarias autoincrementales. Las observaciones FK reflejan restricciones existentes, no relaciones inferidas por nombres. Los controles listados se identificaron en el formulario indicado; este inventario no certifica por sí solo que cada ruta de persistencia del formulario esté probada.

Los `.resx` de los formularios contienen entradas genéricas de ejemplo, no mapeos de base de datos. `FrmCompraCompleta` crea sus controles en código y se incluye junto con los formularios de compras y detalle. Los formularios de navegación y dashboard no tienen una captura directa de una sola tabla. `FrmLogin` no se asocia a `Tbl_Usuarios`: su código actual usa autenticación codificada y no consulta esa tabla.

Tras la autorización del estudiante, los 25 controles FK que eran `NumericUpDown` en los formularios CRUD genéricos se cambiaron a listas desplegables que conservan la PK seleccionada y muestran datos descriptivos del registro relacionado. Las FK anulables de Cardex (`CodigoCompra`, `CodigoVenta`) y DetalleVenta (`CodigoPlan`) ofrecen la opción `(Sin seleccionar)` y se convierten a `NULL`; los PK autogenerados, importes y cantidades no se cambiaron. La compilación se verificó; no se ejecutó la aplicación para comprobar la carga de catálogos en pantalla.

| Tabla | Columna | Tipo | Control | Observación |
|---|---|---|---|---|
| dbo.Tbl_Cardex | CodigoCardex | int | FrmCardex.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Cardex | CodigoInventario | int | FrmCardex.cmbCodigoInventario | FK → dbo.Tbl_Inventario.CodigoInventario |
| dbo.Tbl_Cardex | CodigoCompra | int | FrmCardex.cmbCodigoCompra | FK → dbo.Tbl_Compras.CodigoCompra; NULL; la lista ofrece "(Sin seleccionar)" y se guarda como NULL |
| dbo.Tbl_Cardex | CodigoVenta | int | FrmCardex.cmbCodigoVenta | FK → dbo.Tbl_Ventas.CodigoVenta; NULL; la lista ofrece "(Sin seleccionar)" y se guarda como NULL |
| dbo.Tbl_Cardex | FechaActualizacion | datetime | FrmCardex.dtpFechaActualizacion | |
| dbo.Tbl_Cardex | TipoMovimiento | varchar(45) | FrmCardex.cmbTipoMovimiento | |
| dbo.Tbl_Cardex | Cantidad | int | FrmCardex.nudCantidad | |
| dbo.Tbl_Cardex | StockResultante | int | FrmCardex.nudStockResultante | |
| dbo.Tbl_Cardex | Observaciones | varchar(200) | FrmCardex.txtObservaciones | NULL |
| dbo.Tbl_Cardex | Estado | bit | FrmCardex.rdoActivo / rdoInactivo | |
| dbo.Tbl_Clientes | CodigoCliente | int | FrmClientes.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Clientes | Nombre | varchar(100) | FrmClientes.txtNombre | |
| dbo.Tbl_Clientes | Nit | varchar(45) | FrmClientes.txtNit | NULL |
| dbo.Tbl_Clientes | CorreoElectronico | varchar(100) | FrmClientes.txtCorreoElectronico | NULL |
| dbo.Tbl_Clientes | Telefono | varchar(20) | FrmClientes.txtTelefono | |
| dbo.Tbl_Clientes | Direccion | varchar(100) | FrmClientes.txtDireccion | |
| dbo.Tbl_Clientes | Departamento | varchar(100) | FrmClientes.cboDepartamento | |
| dbo.Tbl_Clientes | Municipio | varchar(100) | FrmClientes.cboMunicipio | |
| dbo.Tbl_Clientes | Estado | bit | FrmClientes.rdoActivo / rdoInactivo | |
| dbo.Tbl_Compras | CodigoCompra | int | FrmCompras.txtCodigo; FrmCompraCompleta.txtCodigoCompra | PK, IDENTITY |
| dbo.Tbl_Compras | CodigoProveedor | int | FrmCompras.cmbCodigoProveedor; FrmCompraCompleta.cmbProveedor | FK → dbo.Tbl_Proveedores.CodigoProveedor |
| dbo.Tbl_Compras | NumeroDocumento | varchar(50) | FrmCompras.txtNumeroDocumento; FrmCompraCompleta.txtDocumento | |
| dbo.Tbl_Compras | FechaCompra | datetime | FrmCompras.dtpFechaCompra; FrmCompraCompleta.dtpFecha | |
| dbo.Tbl_Compras | Subtotal | decimal(10,2) | FrmCompras.nudSubtotal; FrmCompraCompleta.nudSubtotalCompra | |
| dbo.Tbl_Compras | Iva | decimal(10,2) | FrmCompras.nudIva; FrmCompraCompleta.nudIva | |
| dbo.Tbl_Compras | Total | decimal(10,2) | FrmCompras.nudTotal; FrmCompraCompleta.nudTotalCompra | |
| dbo.Tbl_Compras | Observaciones | varchar(200) | FrmCompras.txtObservaciones; FrmCompraCompleta.txtObservacionesCompra | NULL |
| dbo.Tbl_Compras | Estado | bit | FrmCompras.rdoActivo / rdoInactivo; FrmCompraCompleta.rdoCompraActiva / rdoCompraInactiva | |
| dbo.Tbl_DetalleCompra | CodigoDetalleCompra | int | FrmDetalleCompra.txtCodigo; FrmCompraCompleta.txtCodigoDetalle | PK, IDENTITY |
| dbo.Tbl_DetalleCompra | CodigoCompra | int | FrmDetalleCompra.cmbCodigoCompra; FrmCompraCompleta.txtCodigoCompraDetalle | FK → dbo.Tbl_Compras.CodigoCompra |
| dbo.Tbl_DetalleCompra | CodigoEquipo | int | FrmDetalleCompra.cmbCodigoEquipo; FrmCompraCompleta.cmbEquipo | FK → dbo.Tbl_Equipos.CodigoEquipo |
| dbo.Tbl_DetalleCompra | Cantidad | int | FrmDetalleCompra.nudCantidad; FrmCompraCompleta.nudCantidad | |
| dbo.Tbl_DetalleCompra | CostoUnitario | decimal(10,2) | FrmDetalleCompra.nudCostoUnitario; FrmCompraCompleta.nudCosto | |
| dbo.Tbl_DetalleCompra | Subtotal | decimal(10,2) | FrmDetalleCompra.nudSubtotal; FrmCompraCompleta.nudSubtotalDetalle | |
| dbo.Tbl_DetalleCompra | Observaciones | varchar(200) | FrmDetalleCompra.txtObservaciones; FrmCompraCompleta.txtObservacionesDetalle | NULL |
| dbo.Tbl_DetalleCompra | Estado | bit | FrmDetalleCompra.rdoActivo / rdoInactivo; FrmCompraCompleta.rdoDetalleActivo / rdoDetalleInactivo | |
| dbo.Tbl_DetalleFactura | CodigoDetalleFactura | int | FrmDetalleFactura.txtCodigo | PK, IDENTITY |
| dbo.Tbl_DetalleFactura | CodigoFactura | int | FrmDetalleFactura.cmbCodigoFactura | FK → dbo.Tbl_Facturacion.CodigoFactura |
| dbo.Tbl_DetalleFactura | Descripcion | varchar(200) | FrmDetalleFactura.txtDescripcion | |
| dbo.Tbl_DetalleFactura | Cantidad | int | FrmDetalleFactura.nudCantidad | |
| dbo.Tbl_DetalleFactura | PrecioUnitario | decimal(10,2) | FrmDetalleFactura.nudPrecioUnitario | |
| dbo.Tbl_DetalleFactura | Descuento | decimal(10,2) | FrmDetalleFactura.nudDescuento | |
| dbo.Tbl_DetalleFactura | Iva | decimal(10,2) | FrmDetalleFactura.nudIva | |
| dbo.Tbl_DetalleFactura | Subtotal | decimal(10,2) | FrmDetalleFactura.nudSubtotal | |
| dbo.Tbl_DetalleFactura | Estado | bit | FrmDetalleFactura.rdoActivo / rdoInactivo | |
| dbo.Tbl_DetalleVenta | CodigoDetalleVenta | int | FrmDetalleVenta.txtCodigo | PK, IDENTITY |
| dbo.Tbl_DetalleVenta | CodigoVenta | int | FrmDetalleVenta.cmbCodigoVenta | FK → dbo.Tbl_Ventas.CodigoVenta |
| dbo.Tbl_DetalleVenta | CodigoEquipo | int | FrmDetalleVenta.cmbCodigoEquipo | FK → dbo.Tbl_Equipos.CodigoEquipo |
| dbo.Tbl_DetalleVenta | CodigoPlan | int | FrmDetalleVenta.cmbCodigoPlan | FK → dbo.Tbl_Planes.CodigoPlan; NULL; la lista ofrece "(Sin seleccionar)" y se guarda como NULL |
| dbo.Tbl_DetalleVenta | Cantidad | int | FrmDetalleVenta.nudCantidad | |
| dbo.Tbl_DetalleVenta | PrecioUnitario | decimal(10,2) | FrmDetalleVenta.nudPrecioUnitario | |
| dbo.Tbl_DetalleVenta | Descuento | decimal(10,2) | FrmDetalleVenta.nudDescuento | |
| dbo.Tbl_DetalleVenta | Subtotal | decimal(10,2) | FrmDetalleVenta.nudSubtotal | |
| dbo.Tbl_DetalleVenta | Estado | bit | FrmDetalleVenta.rdoActivo / rdoInactivo | |
| dbo.Tbl_Empleados | CodigoEmpleado | int | FrmEmpleados.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Empleados | CodigoSucursal | int | FrmEmpleados.cmbCodigoSucursal | FK → dbo.Tbl_Sucursales.CodigoSucursal |
| dbo.Tbl_Empleados | CodigoUsuario | int | FrmEmpleados.cmbCodigoUsuario | FK → dbo.Tbl_Usuarios.CodigoUsuario |
| dbo.Tbl_Empleados | NombreEmpleado | varchar(100) | FrmEmpleados.txtNombreEmpleado | |
| dbo.Tbl_Empleados | FechaNacimiento | datetime | FrmEmpleados.dtpFechaNacimiento | |
| dbo.Tbl_Empleados | Salario | decimal(10,2) | FrmEmpleados.nudSalario | |
| dbo.Tbl_Empleados | FechaContratacion | datetime | FrmEmpleados.dtpFechaContratacion | |
| dbo.Tbl_Empleados | Telefono | varchar(20) | FrmEmpleados.txtTelefono | NULL |
| dbo.Tbl_Empleados | Observaciones | varchar(255) | FrmEmpleados.txtObservaciones | NULL |
| dbo.Tbl_Empleados | Estado | bit | FrmEmpleados.rdoActivo / rdoInactivo | |
| dbo.Tbl_Empleados | Dpi | varchar(25) | FrmEmpleados.txtDpi | |
| dbo.Tbl_Empleados | Direccion | varchar(100) | FrmEmpleados.txtDireccion | |
| dbo.Tbl_Empleados | TipoEmpleado | varchar(50) | FrmEmpleados.cmbTipoEmpleado | |
| dbo.Tbl_Equipos | CodigoEquipo | int | FrmEquipos.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Equipos | CodigoMarca | int | FrmEquipos.cmbCodigoMarca | FK → dbo.Tbl_Marcas.CodigoMarca |
| dbo.Tbl_Equipos | Categorias | varchar(100) | FrmEquipos.cmbCategorias | |
| dbo.Tbl_Equipos | Modelo | varchar(100) | FrmEquipos.txtModelo | |
| dbo.Tbl_Equipos | Descripcion | varchar(200) | FrmEquipos.txtDescripcion | |
| dbo.Tbl_Equipos | Precio | decimal(10,2) | FrmEquipos.nudPrecio | |
| dbo.Tbl_Equipos | NumeroSerie | varchar(100) | FrmEquipos.txtNumeroSerie | |
| dbo.Tbl_Equipos | Estado | bit | FrmEquipos.rdoActivo / rdoInactivo | |
| dbo.Tbl_Facturacion | CodigoFactura | int | FrmFacturacion.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Facturacion | CodigoVenta | int | FrmFacturacion.cmbCodigoVenta | FK → dbo.Tbl_Ventas.CodigoVenta |
| dbo.Tbl_Facturacion | NumeroFactura | varchar(100) | FrmFacturacion.txtNumeroFactura | |
| dbo.Tbl_Facturacion | FechaEmision | datetime | FrmFacturacion.dtpFechaEmision | |
| dbo.Tbl_Facturacion | NombreCliente | varchar(100) | FrmFacturacion.txtNombreCliente | |
| dbo.Tbl_Facturacion | Nit | varchar(45) | FrmFacturacion.txtNit | NULL |
| dbo.Tbl_Facturacion | Total | decimal(10,2) | FrmFacturacion.nudTotal | |
| dbo.Tbl_Facturacion | Estado | bit | FrmFacturacion.rdoActivo / rdoInactivo | |
| dbo.Tbl_Inventario | CodigoInventario | int | FrmInventario.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Inventario | CodigoEquipo | int | FrmInventario.cmbCodigoEquipo | FK → dbo.Tbl_Equipos.CodigoEquipo |
| dbo.Tbl_Inventario | CodigoSucursal | int | FrmInventario.cmbCodigoSucursal | FK → dbo.Tbl_Sucursales.CodigoSucursal |
| dbo.Tbl_Inventario | Stock | int | FrmInventario.nudStock | |
| dbo.Tbl_Inventario | StockMinimo | int | FrmInventario.nudStockMinimo | |
| dbo.Tbl_Inventario | PrecioUnitario | decimal(10,2) | FrmInventario.nudPrecioUnitario | |
| dbo.Tbl_Inventario | Observaciones | varchar(200) | FrmInventario.txtObservaciones | NULL |
| dbo.Tbl_Inventario | FechaActualizacion | datetime | FrmInventario.dtpFechaActualizacion | |
| dbo.Tbl_Inventario | Estado | bit | FrmInventario.rdoActivo / rdoInactivo | |
| dbo.Tbl_Lineas | CodigoLinea | int | FrmLineas.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Lineas | CodigoCliente | int | FrmLineas.cmbCodigoCliente | FK → dbo.Tbl_Clientes.CodigoCliente |
| dbo.Tbl_Lineas | CodigoPlan | int | FrmLineas.cmbCodigoPlan | FK → dbo.Tbl_Planes.CodigoPlan |
| dbo.Tbl_Lineas | NumeroTelefonico | varchar(20) | FrmLineas.txtNumeroTelefonico | |
| dbo.Tbl_Lineas | TipoLinea | varchar(20) | FrmLineas.cmbTipoLinea | |
| dbo.Tbl_Lineas | FechaActivacion | datetime | FrmLineas.dtpFechaActivacion | NULL; el código usa Checked para representar NULL |
| dbo.Tbl_Lineas | FechaVencimiento | datetime | FrmLineas.dtpFechaVencimiento | NULL; el código usa Checked para representar NULL |
| dbo.Tbl_Lineas | Estado | bit | FrmLineas.rdoActivo / rdoInactivo | |
| dbo.Tbl_Marcas | CodigoMarca | int | FrmMarcas.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Marcas | CodigoProveedor | int | FrmMarcas.cmbCodigoProveedor | FK → dbo.Tbl_Proveedores.CodigoProveedor |
| dbo.Tbl_Marcas | Nombre | varchar(100) | FrmMarcas.txtNombre | |
| dbo.Tbl_Marcas | PaisOrigen | varchar(100) | FrmMarcas.txtPaisOrigen | NULL |
| dbo.Tbl_Marcas | Descripcion | varchar(200) | FrmMarcas.txtDescripcion | NULL |
| dbo.Tbl_Marcas | SitioWeb | varchar(150) | FrmMarcas.txtSitioWeb | NULL |
| dbo.Tbl_Marcas | FechaRegistro | datetime | FrmMarcas.dtpFechaRegistro | |
| dbo.Tbl_Marcas | Estado | bit | FrmMarcas.rdoActivo / rdoInactivo | |
| dbo.Tbl_Pagos | CodigoPago | int | FrmPagos.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Pagos | CodigoFactura | int | FrmPagos.cmbCodigoFactura | FK → dbo.Tbl_Facturacion.CodigoFactura |
| dbo.Tbl_Pagos | TipoPago | varchar(100) | FrmPagos.cmbTipoPago | |
| dbo.Tbl_Pagos | Monto | decimal(10,2) | FrmPagos.nudMonto | |
| dbo.Tbl_Pagos | Moneda | varchar(10) | FrmPagos.cmbMoneda | |
| dbo.Tbl_Pagos | FechaPago | datetime | FrmPagos.dtpFechaPago | |
| dbo.Tbl_Pagos | Referencia | varchar(100) | FrmPagos.txtReferencia | NULL |
| dbo.Tbl_Pagos | Estado | bit | FrmPagos.rdoActivo / rdoInactivo | |
| dbo.Tbl_Planes | CodigoPlan | int | FrmPlanes.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Planes | Nombre | varchar(100) | FrmPlanes.txtNombre | |
| dbo.Tbl_Planes | Descripcion | varchar(200) | FrmPlanes.txtDescripcion | |
| dbo.Tbl_Planes | CostoMensual | decimal(10,2) | FrmPlanes.nudCostoMensual | |
| dbo.Tbl_Planes | CantidadDatos | int | FrmPlanes.nudCantidadDatos | |
| dbo.Tbl_Planes | MinutosIncluidos | int | FrmPlanes.nudMinutosIncluidos | |
| dbo.Tbl_Planes | VigenciaMeses | int | FrmPlanes.nudVigenciaMeses | |
| dbo.Tbl_Planes | Estado | bit | FrmPlanes.rdoActivo / rdoInactivo | |
| dbo.Tbl_Proveedores | CodigoProveedor | int | FrmProveedores.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Proveedores | Nombre | varchar(100) | FrmProveedores.txtNombre | |
| dbo.Tbl_Proveedores | Nit | varchar(45) | FrmProveedores.txtNit | NULL |
| dbo.Tbl_Proveedores | Direccion | varchar(200) | FrmProveedores.txtDireccion | |
| dbo.Tbl_Proveedores | Telefono | varchar(20) | FrmProveedores.txtTelefono | |
| dbo.Tbl_Proveedores | CorreoElectronico | varchar(100) | FrmProveedores.txtCorreoElectronico | NULL |
| dbo.Tbl_Proveedores | NombreContacto | varchar(100) | FrmProveedores.txtNombreContacto | NULL |
| dbo.Tbl_Proveedores | Descripcion | varchar(200) | FrmProveedores.txtDescripcion | NULL |
| dbo.Tbl_Proveedores | Estado | bit | FrmProveedores.rdoActivo / rdoInactivo | |
| dbo.Tbl_Reclamos | CodigoReclamo | int | FrmReclamos.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Reclamos | CodigoCliente | int | FrmReclamos.cmbCodigoCliente | FK → dbo.Tbl_Clientes.CodigoCliente |
| dbo.Tbl_Reclamos | CodigoLinea | int | FrmReclamos.cmbCodigoLinea | FK → dbo.Tbl_Lineas.CodigoLinea |
| dbo.Tbl_Reclamos | CodigoDetalleVenta | int | FrmReclamos.cmbCodigoDetalleVenta | FK → dbo.Tbl_DetalleVenta.CodigoDetalleVenta |
| dbo.Tbl_Reclamos | Fecha | datetime | FrmReclamos.dtpFecha | |
| dbo.Tbl_Reclamos | Descripcion | varchar(200) | FrmReclamos.txtDescripcion | |
| dbo.Tbl_Reclamos | TipoReclamo | varchar(45) | FrmReclamos.cmbTipoReclamo | |
| dbo.Tbl_Reclamos | Estado | bit | FrmReclamos.rdoActivo / rdoInactivo | |
| dbo.Tbl_Sucursales | CodigoSucursal | int | FrmSucursales.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Sucursales | Nombre | varchar(100) | FrmSucursales.txtNombre | |
| dbo.Tbl_Sucursales | Direccion | varchar(200) | FrmSucursales.txtDireccion | |
| dbo.Tbl_Sucursales | Municipio | varchar(100) | FrmSucursales.cboMunicipio | |
| dbo.Tbl_Sucursales | Departamento | varchar(100) | FrmSucursales.cboDepartamento | |
| dbo.Tbl_Sucursales | Telefono | varchar(20) | FrmSucursales.txtTelefono | |
| dbo.Tbl_Sucursales | HorarioAtencion | varchar(100) | FrmSucursales.txtHorarioAtencion | |
| dbo.Tbl_Sucursales | CorreoElectronico | varchar(100) | FrmSucursales.txtCorreoElectronico | NULL |
| dbo.Tbl_Sucursales | FechaApertura | datetime | FrmSucursales.dtpFechaApertura | |
| dbo.Tbl_Sucursales | Estado | bit | FrmSucursales.rdoActivo / rdoInactivo | |
| dbo.Tbl_Usuarios | CodigoUsuario | int | FrmUsuarios.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Usuarios | TipoUsuario | varchar(100) | FrmUsuarios.cmbTipoUsuario | |
| dbo.Tbl_Usuarios | Nombre | varchar(100) | FrmUsuarios.txtNombre | |
| dbo.Tbl_Usuarios | Contrasenia | varchar(100) | FrmUsuarios.txtContrasenia | NOT NULL; el tipo no permite inferir si se almacena texto o hash |
| dbo.Tbl_Usuarios | CorreoElectronico | varchar(100) | FrmUsuarios.txtCorreoElectronico | |
| dbo.Tbl_Usuarios | FechaCreacion | datetime | FrmUsuarios.dtpFechaCreacion | |
| dbo.Tbl_Usuarios | FechaUltimoAcceso | datetime | FrmUsuarios.dtpFechaUltimoAcceso | NULL; el código usa Checked para representar NULL |
| dbo.Tbl_Usuarios | Estado | bit | FrmUsuarios.rdoActivo / rdoInactivo | |
| dbo.Tbl_Ventas | CodigoVenta | int | FrmVentas.txtCodigo | PK, IDENTITY |
| dbo.Tbl_Ventas | CodigoSucursal | int | FrmVentas.cmbCodigoSucursal | FK → dbo.Tbl_Sucursales.CodigoSucursal |
| dbo.Tbl_Ventas | CodigoCliente | int | FrmVentas.cmbCodigoCliente | FK → dbo.Tbl_Clientes.CodigoCliente |
| dbo.Tbl_Ventas | CodigoEmpleado | int | FrmVentas.cmbCodigoEmpleado | FK → dbo.Tbl_Empleados.CodigoEmpleado |
| dbo.Tbl_Ventas | FechaVenta | datetime | FrmVentas.dtpFechaVenta | |
| dbo.Tbl_Ventas | Subtotal | decimal(10,2) | FrmVentas.nudSubtotal | |
| dbo.Tbl_Ventas | Iva | decimal(10,2) | FrmVentas.nudIva | |
| dbo.Tbl_Ventas | Total | decimal(10,2) | FrmVentas.nudTotal | |
| dbo.Tbl_Ventas | Observaciones | varchar(200) | FrmVentas.txtObservaciones | NULL |
| dbo.Tbl_Ventas | Estado | bit | FrmVentas.rdoActivo / rdoInactivo | |
