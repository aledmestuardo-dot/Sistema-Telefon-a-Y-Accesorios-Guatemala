-- Ajusta los privilegios actuales del esquema public para el cliente de la app.
-- Ejecutar una sola vez en Supabase SQL Editor después de revisar el contenido.
-- No cambia datos, tablas, claves, políticas RLS ni perfiles.

BEGIN;

-- Retira permisos amplios que no debe tener el cliente (incluidos TRUNCATE,
-- TRIGGER y REFERENCES). Las políticas RLS permanecen intactas.
REVOKE ALL PRIVILEGES ON ALL TABLES IN SCHEMA public FROM anon, authenticated;
REVOKE ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public FROM anon, authenticated;

-- El rol autenticado usa el esquema y las secuencias necesarias para insertar
-- registros con identificadores generados por la base de datos.
GRANT USAGE ON SCHEMA public TO authenticated;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO authenticated;

-- Tbl_Usuarios solo se consulta directamente. Su administración debe pasar
-- por la función de servidor protegida, nunca por permisos de escritura cliente.
GRANT SELECT ON TABLE public."Tbl_Usuarios" TO authenticated;

-- Las tablas de negocio permiten las operaciones amparadas por sus políticas RLS.
GRANT SELECT, INSERT, UPDATE ON TABLE
    public."Tbl_Cardex",
    public."Tbl_Clientes",
    public."Tbl_Compras",
    public."Tbl_DetalleCompra",
    public."Tbl_DetalleFactura",
    public."Tbl_DetalleVenta",
    public."Tbl_Empleados",
    public."Tbl_Equipos",
    public."Tbl_Facturacion",
    public."Tbl_Inventario",
    public."Tbl_Lineas",
    public."Tbl_Marcas",
    public."Tbl_Pagos",
    public."Tbl_Planes",
    public."Tbl_Proveedores",
    public."Tbl_Reclamos",
    public."Tbl_Sucursales",
    public."Tbl_Ventas"
TO authenticated;

COMMIT;
