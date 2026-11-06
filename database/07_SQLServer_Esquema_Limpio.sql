-- Inicialización limpia de SQL Server basada en los metadatos verificados el 2026-10-26.
-- Crea BD_TelefoniaAccesoriosSA si no existe, y solo crea tablas/FK ausentes.
-- No inserta datos, no cambia objetos existentes y no incluye credenciales.
-- Ejecutar conectado a master con permisos para crear bases y objetos.
SET NOCOUNT ON;
IF DB_ID(N'BD_TelefoniaAccesoriosSA') IS NULL
BEGIN
    EXEC(N'CREATE DATABASE [BD_TelefoniaAccesoriosSA]');
END;
GO
USE [BD_TelefoniaAccesoriosSA];
GO
SET NOCOUNT ON;

IF OBJECT_ID(N'dbo.Tbl_Cardex', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Cardex] (
        [CodigoCardex] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Cardex] PRIMARY KEY CLUSTERED,
        [CodigoInventario] int NOT NULL,
        [CodigoCompra] int NULL,
        [CodigoVenta] int NULL,
        [FechaActualizacion] datetime NOT NULL,
        [TipoMovimiento] varchar(45) NOT NULL,
        [Cantidad] int NOT NULL,
        [StockResultante] int NOT NULL,
        [Observaciones] varchar(200) NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Clientes', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Clientes] (
        [CodigoCliente] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Clientes] PRIMARY KEY CLUSTERED,
        [Nombre] varchar(100) NOT NULL,
        [Nit] varchar(45) NULL,
        [CorreoElectronico] varchar(100) NULL,
        [Telefono] varchar(20) NOT NULL,
        [Direccion] varchar(100) NOT NULL,
        [Departamento] varchar(100) NOT NULL,
        [Municipio] varchar(100) NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Compras', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Compras] (
        [CodigoCompra] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Compras] PRIMARY KEY CLUSTERED,
        [CodigoProveedor] int NOT NULL,
        [NumeroDocumento] varchar(50) NOT NULL,
        [FechaCompra] datetime NOT NULL,
        [Subtotal] decimal(10,2) NOT NULL,
        [Iva] decimal(10,2) NOT NULL,
        [Total] decimal(10,2) NOT NULL,
        [Observaciones] varchar(200) NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_DetalleCompra', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_DetalleCompra] (
        [CodigoDetalleCompra] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_DetalleCompra] PRIMARY KEY CLUSTERED,
        [CodigoCompra] int NOT NULL,
        [CodigoEquipo] int NOT NULL,
        [Cantidad] int NOT NULL,
        [CostoUnitario] decimal(10,2) NOT NULL,
        [Subtotal] decimal(10,2) NOT NULL,
        [Observaciones] varchar(200) NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_DetalleFactura', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_DetalleFactura] (
        [CodigoDetalleFactura] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_DetalleFactura] PRIMARY KEY CLUSTERED,
        [CodigoFactura] int NOT NULL,
        [Descripcion] varchar(200) NOT NULL,
        [Cantidad] int NOT NULL,
        [PrecioUnitario] decimal(10,2) NOT NULL,
        [Descuento] decimal(10,2) NOT NULL,
        [Iva] decimal(10,2) NOT NULL,
        [Subtotal] decimal(10,2) NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_DetalleVenta', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_DetalleVenta] (
        [CodigoDetalleVenta] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_DetalleVenta] PRIMARY KEY CLUSTERED,
        [CodigoVenta] int NOT NULL,
        [CodigoEquipo] int NOT NULL,
        [CodigoPlan] int NULL,
        [Cantidad] int NOT NULL,
        [PrecioUnitario] decimal(10,2) NOT NULL,
        [Descuento] decimal(10,2) NOT NULL,
        [Subtotal] decimal(10,2) NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Empleados', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Empleados] (
        [CodigoEmpleado] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Empleados] PRIMARY KEY CLUSTERED,
        [CodigoSucursal] int NOT NULL,
        [CodigoUsuario] int NOT NULL,
        [NombreEmpleado] varchar(100) NOT NULL,
        [FechaNacimiento] datetime NOT NULL,
        [Salario] decimal(10,2) NOT NULL,
        [FechaContratacion] datetime NOT NULL,
        [Telefono] varchar(20) NULL,
        [Observaciones] varchar(255) NULL,
        [Estado] bit NOT NULL,
        [Dpi] varchar(25) NOT NULL,
        [Direccion] varchar(100) NOT NULL,
        [TipoEmpleado] varchar(50) NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Equipos', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Equipos] (
        [CodigoEquipo] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Equipos] PRIMARY KEY CLUSTERED,
        [CodigoMarca] int NOT NULL,
        [Categorias] varchar(100) NOT NULL,
        [Modelo] varchar(100) NOT NULL,
        [Descripcion] varchar(200) NOT NULL,
        [Precio] decimal(10,2) NOT NULL,
        [NumeroSerie] varchar(100) NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Facturacion', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Facturacion] (
        [CodigoFactura] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Facturacion] PRIMARY KEY CLUSTERED,
        [CodigoVenta] int NOT NULL,
        [NumeroFactura] varchar(100) NOT NULL,
        [FechaEmision] datetime NOT NULL,
        [NombreCliente] varchar(100) NOT NULL,
        [Nit] varchar(45) NULL,
        [Total] decimal(10,2) NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Inventario', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Inventario] (
        [CodigoInventario] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Inventario] PRIMARY KEY CLUSTERED,
        [CodigoEquipo] int NOT NULL,
        [CodigoSucursal] int NOT NULL,
        [Stock] int NOT NULL,
        [StockMinimo] int NOT NULL,
        [PrecioUnitario] decimal(10,2) NOT NULL,
        [Observaciones] varchar(200) NULL,
        [FechaActualizacion] datetime NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Lineas', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Lineas] (
        [CodigoLinea] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Lineas] PRIMARY KEY CLUSTERED,
        [CodigoCliente] int NOT NULL,
        [CodigoPlan] int NOT NULL,
        [NumeroTelefonico] varchar(20) NOT NULL,
        [TipoLinea] varchar(20) NOT NULL,
        [FechaActivacion] datetime NULL,
        [FechaVencimiento] datetime NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Marcas', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Marcas] (
        [CodigoMarca] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Marcas] PRIMARY KEY CLUSTERED,
        [CodigoProveedor] int NOT NULL,
        [Nombre] varchar(100) NOT NULL,
        [PaisOrigen] varchar(100) NULL,
        [Descripcion] varchar(200) NULL,
        [SitioWeb] varchar(150) NULL,
        [FechaRegistro] datetime NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Pagos', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Pagos] (
        [CodigoPago] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Pagos] PRIMARY KEY CLUSTERED,
        [CodigoFactura] int NOT NULL,
        [TipoPago] varchar(100) NOT NULL,
        [Monto] decimal(10,2) NOT NULL,
        [Moneda] varchar(10) NOT NULL,
        [FechaPago] datetime NOT NULL,
        [Referencia] varchar(100) NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Planes', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Planes] (
        [CodigoPlan] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Planes] PRIMARY KEY CLUSTERED,
        [Nombre] varchar(100) NOT NULL,
        [Descripcion] varchar(200) NOT NULL,
        [CostoMensual] decimal(10,2) NOT NULL,
        [CantidadDatos] int NOT NULL,
        [MinutosIncluidos] int NOT NULL,
        [VigenciaMeses] int NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Proveedores', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Proveedores] (
        [CodigoProveedor] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Proveedores] PRIMARY KEY CLUSTERED,
        [Nombre] varchar(100) NOT NULL,
        [Nit] varchar(45) NULL,
        [Direccion] varchar(200) NOT NULL,
        [Telefono] varchar(20) NOT NULL,
        [CorreoElectronico] varchar(100) NULL,
        [NombreContacto] varchar(100) NULL,
        [Descripcion] varchar(200) NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Reclamos', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Reclamos] (
        [CodigoReclamo] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Reclamos] PRIMARY KEY CLUSTERED,
        [CodigoCliente] int NOT NULL,
        [CodigoLinea] int NOT NULL,
        [CodigoDetalleVenta] int NOT NULL,
        [Fecha] datetime NOT NULL,
        [Descripcion] varchar(200) NOT NULL,
        [TipoReclamo] varchar(45) NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Sucursales', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Sucursales] (
        [CodigoSucursal] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Sucursales] PRIMARY KEY CLUSTERED,
        [Nombre] varchar(100) NOT NULL,
        [Direccion] varchar(200) NOT NULL,
        [Municipio] varchar(100) NOT NULL,
        [Departamento] varchar(100) NOT NULL,
        [Telefono] varchar(20) NOT NULL,
        [HorarioAtencion] varchar(100) NOT NULL,
        [CorreoElectronico] varchar(100) NULL,
        [FechaApertura] datetime NOT NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Usuarios', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Usuarios] (
        [CodigoUsuario] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Usuarios] PRIMARY KEY CLUSTERED,
        [TipoUsuario] varchar(100) NOT NULL,
        [Nombre] varchar(100) NOT NULL,
        [Contrasenia] varchar(100) NOT NULL,
        [CorreoElectronico] varchar(100) NOT NULL,
        [FechaCreacion] datetime NOT NULL,
        [FechaUltimoAcceso] datetime NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Tbl_Ventas', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Ventas] (
        [CodigoVenta] int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Tbl_Ventas] PRIMARY KEY CLUSTERED,
        [CodigoSucursal] int NOT NULL,
        [CodigoCliente] int NOT NULL,
        [CodigoEmpleado] int NOT NULL,
        [FechaVenta] datetime NOT NULL,
        [Subtotal] decimal(10,2) NOT NULL,
        [Iva] decimal(10,2) NOT NULL,
        [Total] decimal(10,2) NOT NULL,
        [Observaciones] varchar(200) NULL,
        [Estado] bit NOT NULL
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Cardex_CodigoInventario' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Cardex'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Cardex] WITH CHECK ADD CONSTRAINT [FK_Tbl_Cardex_CodigoInventario] FOREIGN KEY ([CodigoInventario]) REFERENCES [dbo].[Tbl_Inventario] ([CodigoInventario]);
    ALTER TABLE [dbo].[Tbl_Cardex] CHECK CONSTRAINT [FK_Tbl_Cardex_CodigoInventario];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Cardex_CodigoCompra' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Cardex'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Cardex] WITH CHECK ADD CONSTRAINT [FK_Tbl_Cardex_CodigoCompra] FOREIGN KEY ([CodigoCompra]) REFERENCES [dbo].[Tbl_Compras] ([CodigoCompra]);
    ALTER TABLE [dbo].[Tbl_Cardex] CHECK CONSTRAINT [FK_Tbl_Cardex_CodigoCompra];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Cardex_CodigoVenta' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Cardex'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Cardex] WITH CHECK ADD CONSTRAINT [FK_Tbl_Cardex_CodigoVenta] FOREIGN KEY ([CodigoVenta]) REFERENCES [dbo].[Tbl_Ventas] ([CodigoVenta]);
    ALTER TABLE [dbo].[Tbl_Cardex] CHECK CONSTRAINT [FK_Tbl_Cardex_CodigoVenta];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Compras_CodigoProveedor' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Compras'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Compras] WITH CHECK ADD CONSTRAINT [FK_Tbl_Compras_CodigoProveedor] FOREIGN KEY ([CodigoProveedor]) REFERENCES [dbo].[Tbl_Proveedores] ([CodigoProveedor]);
    ALTER TABLE [dbo].[Tbl_Compras] CHECK CONSTRAINT [FK_Tbl_Compras_CodigoProveedor];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_DetalleCompra_CodigoCompra' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_DetalleCompra'))
BEGIN
    ALTER TABLE [dbo].[Tbl_DetalleCompra] WITH CHECK ADD CONSTRAINT [FK_Tbl_DetalleCompra_CodigoCompra] FOREIGN KEY ([CodigoCompra]) REFERENCES [dbo].[Tbl_Compras] ([CodigoCompra]);
    ALTER TABLE [dbo].[Tbl_DetalleCompra] CHECK CONSTRAINT [FK_Tbl_DetalleCompra_CodigoCompra];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_DetalleCompra_CodigoEquipo' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_DetalleCompra'))
BEGIN
    ALTER TABLE [dbo].[Tbl_DetalleCompra] WITH CHECK ADD CONSTRAINT [FK_Tbl_DetalleCompra_CodigoEquipo] FOREIGN KEY ([CodigoEquipo]) REFERENCES [dbo].[Tbl_Equipos] ([CodigoEquipo]);
    ALTER TABLE [dbo].[Tbl_DetalleCompra] CHECK CONSTRAINT [FK_Tbl_DetalleCompra_CodigoEquipo];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_DetalleFactura_CodigoFactura' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_DetalleFactura'))
BEGIN
    ALTER TABLE [dbo].[Tbl_DetalleFactura] WITH CHECK ADD CONSTRAINT [FK_Tbl_DetalleFactura_CodigoFactura] FOREIGN KEY ([CodigoFactura]) REFERENCES [dbo].[Tbl_Facturacion] ([CodigoFactura]);
    ALTER TABLE [dbo].[Tbl_DetalleFactura] CHECK CONSTRAINT [FK_Tbl_DetalleFactura_CodigoFactura];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_DetalleVenta_CodigoVenta' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_DetalleVenta'))
BEGIN
    ALTER TABLE [dbo].[Tbl_DetalleVenta] WITH CHECK ADD CONSTRAINT [FK_Tbl_DetalleVenta_CodigoVenta] FOREIGN KEY ([CodigoVenta]) REFERENCES [dbo].[Tbl_Ventas] ([CodigoVenta]);
    ALTER TABLE [dbo].[Tbl_DetalleVenta] CHECK CONSTRAINT [FK_Tbl_DetalleVenta_CodigoVenta];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_DetalleVenta_CodigoEquipo' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_DetalleVenta'))
BEGIN
    ALTER TABLE [dbo].[Tbl_DetalleVenta] WITH CHECK ADD CONSTRAINT [FK_Tbl_DetalleVenta_CodigoEquipo] FOREIGN KEY ([CodigoEquipo]) REFERENCES [dbo].[Tbl_Equipos] ([CodigoEquipo]);
    ALTER TABLE [dbo].[Tbl_DetalleVenta] CHECK CONSTRAINT [FK_Tbl_DetalleVenta_CodigoEquipo];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_DetalleVenta_CodigoPlan' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_DetalleVenta'))
BEGIN
    ALTER TABLE [dbo].[Tbl_DetalleVenta] WITH CHECK ADD CONSTRAINT [FK_Tbl_DetalleVenta_CodigoPlan] FOREIGN KEY ([CodigoPlan]) REFERENCES [dbo].[Tbl_Planes] ([CodigoPlan]);
    ALTER TABLE [dbo].[Tbl_DetalleVenta] CHECK CONSTRAINT [FK_Tbl_DetalleVenta_CodigoPlan];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Empleados_CodigoSucursal' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Empleados'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Empleados] WITH CHECK ADD CONSTRAINT [FK_Tbl_Empleados_CodigoSucursal] FOREIGN KEY ([CodigoSucursal]) REFERENCES [dbo].[Tbl_Sucursales] ([CodigoSucursal]);
    ALTER TABLE [dbo].[Tbl_Empleados] CHECK CONSTRAINT [FK_Tbl_Empleados_CodigoSucursal];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Empleados_CodigoUsuario' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Empleados'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Empleados] WITH CHECK ADD CONSTRAINT [FK_Tbl_Empleados_CodigoUsuario] FOREIGN KEY ([CodigoUsuario]) REFERENCES [dbo].[Tbl_Usuarios] ([CodigoUsuario]);
    ALTER TABLE [dbo].[Tbl_Empleados] CHECK CONSTRAINT [FK_Tbl_Empleados_CodigoUsuario];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Equipos_CodigoMarca' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Equipos'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Equipos] WITH CHECK ADD CONSTRAINT [FK_Tbl_Equipos_CodigoMarca] FOREIGN KEY ([CodigoMarca]) REFERENCES [dbo].[Tbl_Marcas] ([CodigoMarca]);
    ALTER TABLE [dbo].[Tbl_Equipos] CHECK CONSTRAINT [FK_Tbl_Equipos_CodigoMarca];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Facturacion_CodigoVenta' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Facturacion'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Facturacion] WITH CHECK ADD CONSTRAINT [FK_Tbl_Facturacion_CodigoVenta] FOREIGN KEY ([CodigoVenta]) REFERENCES [dbo].[Tbl_Ventas] ([CodigoVenta]);
    ALTER TABLE [dbo].[Tbl_Facturacion] CHECK CONSTRAINT [FK_Tbl_Facturacion_CodigoVenta];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Inventario_CodigoEquipo' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Inventario'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Inventario] WITH CHECK ADD CONSTRAINT [FK_Tbl_Inventario_CodigoEquipo] FOREIGN KEY ([CodigoEquipo]) REFERENCES [dbo].[Tbl_Equipos] ([CodigoEquipo]);
    ALTER TABLE [dbo].[Tbl_Inventario] CHECK CONSTRAINT [FK_Tbl_Inventario_CodigoEquipo];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Inventario_CodigoSucursal' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Inventario'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Inventario] WITH CHECK ADD CONSTRAINT [FK_Tbl_Inventario_CodigoSucursal] FOREIGN KEY ([CodigoSucursal]) REFERENCES [dbo].[Tbl_Sucursales] ([CodigoSucursal]);
    ALTER TABLE [dbo].[Tbl_Inventario] CHECK CONSTRAINT [FK_Tbl_Inventario_CodigoSucursal];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Lineas_CodigoCliente' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Lineas'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Lineas] WITH CHECK ADD CONSTRAINT [FK_Tbl_Lineas_CodigoCliente] FOREIGN KEY ([CodigoCliente]) REFERENCES [dbo].[Tbl_Clientes] ([CodigoCliente]);
    ALTER TABLE [dbo].[Tbl_Lineas] CHECK CONSTRAINT [FK_Tbl_Lineas_CodigoCliente];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Lineas_CodigoPlan' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Lineas'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Lineas] WITH CHECK ADD CONSTRAINT [FK_Tbl_Lineas_CodigoPlan] FOREIGN KEY ([CodigoPlan]) REFERENCES [dbo].[Tbl_Planes] ([CodigoPlan]);
    ALTER TABLE [dbo].[Tbl_Lineas] CHECK CONSTRAINT [FK_Tbl_Lineas_CodigoPlan];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Marcas_CodigoProveedor' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Marcas'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Marcas] WITH CHECK ADD CONSTRAINT [FK_Tbl_Marcas_CodigoProveedor] FOREIGN KEY ([CodigoProveedor]) REFERENCES [dbo].[Tbl_Proveedores] ([CodigoProveedor]);
    ALTER TABLE [dbo].[Tbl_Marcas] CHECK CONSTRAINT [FK_Tbl_Marcas_CodigoProveedor];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Pagos_CodigoFactura' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Pagos'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Pagos] WITH CHECK ADD CONSTRAINT [FK_Tbl_Pagos_CodigoFactura] FOREIGN KEY ([CodigoFactura]) REFERENCES [dbo].[Tbl_Facturacion] ([CodigoFactura]);
    ALTER TABLE [dbo].[Tbl_Pagos] CHECK CONSTRAINT [FK_Tbl_Pagos_CodigoFactura];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Reclamos_CodigoCliente' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Reclamos'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Reclamos] WITH CHECK ADD CONSTRAINT [FK_Tbl_Reclamos_CodigoCliente] FOREIGN KEY ([CodigoCliente]) REFERENCES [dbo].[Tbl_Clientes] ([CodigoCliente]);
    ALTER TABLE [dbo].[Tbl_Reclamos] CHECK CONSTRAINT [FK_Tbl_Reclamos_CodigoCliente];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Reclamos_CodigoLinea' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Reclamos'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Reclamos] WITH CHECK ADD CONSTRAINT [FK_Tbl_Reclamos_CodigoLinea] FOREIGN KEY ([CodigoLinea]) REFERENCES [dbo].[Tbl_Lineas] ([CodigoLinea]);
    ALTER TABLE [dbo].[Tbl_Reclamos] CHECK CONSTRAINT [FK_Tbl_Reclamos_CodigoLinea];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Reclamos_CodigoDetalleVenta' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Reclamos'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Reclamos] WITH CHECK ADD CONSTRAINT [FK_Tbl_Reclamos_CodigoDetalleVenta] FOREIGN KEY ([CodigoDetalleVenta]) REFERENCES [dbo].[Tbl_DetalleVenta] ([CodigoDetalleVenta]);
    ALTER TABLE [dbo].[Tbl_Reclamos] CHECK CONSTRAINT [FK_Tbl_Reclamos_CodigoDetalleVenta];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Ventas_CodigoSucursal' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Ventas'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Ventas] WITH CHECK ADD CONSTRAINT [FK_Tbl_Ventas_CodigoSucursal] FOREIGN KEY ([CodigoSucursal]) REFERENCES [dbo].[Tbl_Sucursales] ([CodigoSucursal]);
    ALTER TABLE [dbo].[Tbl_Ventas] CHECK CONSTRAINT [FK_Tbl_Ventas_CodigoSucursal];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Ventas_CodigoCliente' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Ventas'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Ventas] WITH CHECK ADD CONSTRAINT [FK_Tbl_Ventas_CodigoCliente] FOREIGN KEY ([CodigoCliente]) REFERENCES [dbo].[Tbl_Clientes] ([CodigoCliente]);
    ALTER TABLE [dbo].[Tbl_Ventas] CHECK CONSTRAINT [FK_Tbl_Ventas_CodigoCliente];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Tbl_Ventas_CodigoEmpleado' AND parent_object_id = OBJECT_ID(N'dbo.Tbl_Ventas'))
BEGIN
    ALTER TABLE [dbo].[Tbl_Ventas] WITH CHECK ADD CONSTRAINT [FK_Tbl_Ventas_CodigoEmpleado] FOREIGN KEY ([CodigoEmpleado]) REFERENCES [dbo].[Tbl_Empleados] ([CodigoEmpleado]);
    ALTER TABLE [dbo].[Tbl_Ventas] CHECK CONSTRAINT [FK_Tbl_Ventas_CodigoEmpleado];
END;
GO

IF (SELECT COUNT(*) FROM sys.tables WHERE schema_id = SCHEMA_ID(N'dbo') AND name LIKE N'Tbl[_]%') < 19
BEGIN
    ;THROW 51000, 'No se completaron las 19 tablas esperadas de dbo.', 1;
END;
PRINT 'Esquema SQL Server listo. No se insertaron datos de negocio ni usuarios.';
GO
