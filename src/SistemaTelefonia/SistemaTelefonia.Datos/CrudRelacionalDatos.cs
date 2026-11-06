using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Acceso ADO.NET reutilizable para las tablas relacionadas del sistema.</summary>
    public class CrudRelacionalDatos
    {
        private readonly MarcasSupabaseDatos marcasSupabase = new MarcasSupabaseDatos();
        private readonly CrudRelacionalSupabaseDatos relacionalesSupabase = new CrudRelacionalSupabaseDatos();

        private sealed class Tabla
        {
            public string Nombre;
            public string Clave;
            public Dictionary<string, string> Referencias = new Dictionary<string, string>();
            public HashSet<string> Nulos = new HashSet<string>();
        }

        private static Tabla Definir(string nombre, string clave, string referencias = null, string nulos = null)
        {
            Tabla tabla = new Tabla { Nombre = nombre, Clave = clave };
            if (!string.IsNullOrEmpty(referencias))
                foreach (string par in referencias.Split(';'))
                {
                    string[] partes = par.Split('=');
                    tabla.Referencias.Add(partes[0], partes[1]);
                }
            if (!string.IsNullOrEmpty(nulos))
                foreach (string columna in nulos.Split(';')) tabla.Nulos.Add(columna);
            return tabla;
        }

        private static Tabla Esquema(Type tipo)
        {
            if (tipo == typeof(CardexEntidad)) return Definir("Tbl_Cardex", "CodigoCardex", "CodigoInventario=Tbl_Inventario;CodigoCompra=Tbl_Compras;CodigoVenta=Tbl_Ventas", "CodigoCompra;CodigoVenta;Observaciones");
            if (tipo == typeof(ComprasEntidad)) return Definir("Tbl_Compras", "CodigoCompra", "CodigoProveedor=Tbl_Proveedores", "Observaciones");
            if (tipo == typeof(DetalleCompraEntidad)) return Definir("Tbl_DetalleCompra", "CodigoDetalleCompra", "CodigoCompra=Tbl_Compras;CodigoEquipo=Tbl_Equipos", "Observaciones");
            if (tipo == typeof(DetalleFacturaEntidad)) return Definir("Tbl_DetalleFactura", "CodigoDetalleFactura", "CodigoFactura=Tbl_Facturacion");
            if (tipo == typeof(DetalleVentaEntidad)) return Definir("Tbl_DetalleVenta", "CodigoDetalleVenta", "CodigoVenta=Tbl_Ventas;CodigoEquipo=Tbl_Equipos;CodigoPlan=Tbl_Planes", "CodigoPlan");
            if (tipo == typeof(EmpleadosEntidad)) return Definir("Tbl_Empleados", "CodigoEmpleado", "CodigoSucursal=Tbl_Sucursales;CodigoUsuario=Tbl_Usuarios", "Telefono;Observaciones");
            if (tipo == typeof(EquiposEntidad)) return Definir("Tbl_Equipos", "CodigoEquipo", "CodigoMarca=Tbl_Marcas");
            if (tipo == typeof(FacturacionEntidad)) return Definir("Tbl_Facturacion", "CodigoFactura", "CodigoVenta=Tbl_Ventas", "Nit");
            if (tipo == typeof(InventarioEntidad)) return Definir("Tbl_Inventario", "CodigoInventario", "CodigoEquipo=Tbl_Equipos;CodigoSucursal=Tbl_Sucursales", "Observaciones");
            if (tipo == typeof(LineasEntidad)) return Definir("Tbl_Lineas", "CodigoLinea", "CodigoCliente=Tbl_Clientes;CodigoPlan=Tbl_Planes", "FechaActivacion;FechaVencimiento");
            if (tipo == typeof(MarcasEntidad)) return Definir("Tbl_Marcas", "CodigoMarca", "CodigoProveedor=Tbl_Proveedores", "PaisOrigen;Descripcion;SitioWeb");
            if (tipo == typeof(PagosEntidad)) return Definir("Tbl_Pagos", "CodigoPago", "CodigoFactura=Tbl_Facturacion", "Referencia");
            if (tipo == typeof(ReclamosEntidad)) return Definir("Tbl_Reclamos", "CodigoReclamo", "CodigoCliente=Tbl_Clientes;CodigoLinea=Tbl_Lineas;CodigoDetalleVenta=Tbl_DetalleVenta");
            if (tipo == typeof(VentasEntidad)) return Definir("Tbl_Ventas", "CodigoVenta", "CodigoSucursal=Tbl_Sucursales;CodigoCliente=Tbl_Clientes;CodigoEmpleado=Tbl_Empleados", "Observaciones");
            throw new ArgumentException("La entidad no pertenece a una tabla relacional registrada.", "tipo");
        }

        public List<T> Listar<T>() where T : new()
        {
            Tabla tabla = Esquema(typeof(T));
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase && typeof(T) == typeof(MarcasEntidad))
                return (List<T>)(object)marcasSupabase.Listar();
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacionalesSupabase.Listar<T>(tabla.Nombre, tabla.Clave);
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand("SELECT * FROM dbo." + Q(tabla.Nombre) + " ORDER BY " + Q(tabla.Clave) + ";", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader()) return Materializar<T>(lector);
                }
            }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible consultar " + tabla.Nombre + " en SQL Server.", ex); }
        }

        /// <summary>Catálogo de lectura mínimo para la FK de empleados en Supabase.</summary>
        public List<UsuariosEntidad> ListarUsuariosCatalogo()
        {
            return relacionalesSupabase.ListarUsuariosCatalogo();
        }

        /// <summary>Devuelve facturas activas que aún no tienen un pago activo registrado.</summary>
        public List<FacturacionEntidad> ListarFacturasPendientes()
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                List<FacturacionEntidad> facturas = relacionalesSupabase.Listar<FacturacionEntidad>("Tbl_Facturacion", "CodigoFactura");
                HashSet<int> facturasPagadas = new HashSet<int>(relacionalesSupabase
                    .Listar<PagosEntidad>("Tbl_Pagos", "CodigoPago")
                    .Where(p => p.Estado).Select(p => p.CodigoFactura));
                return facturas.Where(f => f.Estado && !facturasPagadas.Contains(f.CodigoFactura)).ToList();
            }
            const string consulta = @"SELECT f.* FROM dbo.Tbl_Facturacion f
WHERE f.Estado = 1 AND NOT EXISTS
 (SELECT 1 FROM dbo.Tbl_Pagos p WHERE p.CodigoFactura = f.CodigoFactura AND p.Estado = 1)
ORDER BY f.CodigoFactura;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader()) return Materializar<FacturacionEntidad>(lector);
                }
            }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible consultar las facturas pendientes en SQL Server.", ex); }
        }

        public List<T> Buscar<T>(string criterio) where T : new()
        {
            Tabla tabla = Esquema(typeof(T));
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase && typeof(T) == typeof(MarcasEntidad))
                return (List<T>)(object)marcasSupabase.Buscar(criterio);
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacionalesSupabase.Buscar<T>(tabla.Nombre, tabla.Clave, criterio);
            PropertyInfo[] campos = Campos(typeof(T));
            List<PropertyInfo> buscables = campos.Where(p => TipoBase(p.PropertyType) == typeof(string) || TipoBase(p.PropertyType) == typeof(int)).ToList();
            string where = string.Join(" OR ", buscables.Select(p => "CONVERT(varchar(100), " + Q(p.Name) + ") LIKE @Criterio"));
            string sql = "SELECT * FROM dbo." + Q(tabla.Nombre) + " WHERE " + where + " ORDER BY " + Q(tabla.Clave) + ";";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Criterio", SqlDbType.VarChar, 4100).Value = "%" + criterio.Trim() + "%";
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader()) return Materializar<T>(lector);
                }
            }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible buscar en " + tabla.Nombre + " en SQL Server.", ex); }
        }

        public int Agregar<T>(T entidad) where T : new()
        {
            Tabla tabla = Esquema(typeof(T));
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase && typeof(T) == typeof(MarcasEntidad))
                return marcasSupabase.Agregar((MarcasEntidad)(object)entidad);
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacionalesSupabase.Agregar(tabla.Nombre, tabla.Clave, tabla.Nulos, entidad);
            PropertyInfo[] campos = Campos(typeof(T)).Where(p => p.Name != tabla.Clave).ToArray();
            string nombres = string.Join(", ", campos.Select(p => Q(p.Name)));
            string parametros = string.Join(", ", campos.Select(p => "@" + p.Name));
            string sql = "INSERT INTO dbo." + Q(tabla.Nombre) + " (" + nombres + ") OUTPUT INSERTED." + Q(tabla.Clave) + " VALUES (" + parametros + ");";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    conexion.Open();
                    ValidarReferencias(conexion, tabla, entidad);
                    foreach (PropertyInfo campo in campos) Parametro(comando, tabla, campo, entidad);
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible agregar el registro en " + tabla.Nombre + ".", ex); }
        }

        public void Editar<T>(int clave, T entidad) where T : new()
        {
            Tabla tabla = Esquema(typeof(T));
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase && typeof(T) == typeof(MarcasEntidad))
            {
                marcasSupabase.Editar(clave, (MarcasEntidad)(object)entidad);
                return;
            }
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                relacionalesSupabase.Editar(tabla.Nombre, tabla.Clave, clave, tabla.Nulos, entidad);
                return;
            }
            PropertyInfo[] campos = Campos(typeof(T)).Where(p => p.Name != tabla.Clave).ToArray();
            string asignaciones = string.Join(", ", campos.Select(p => Q(p.Name) + " = @" + p.Name));
            string sql = "UPDATE dbo." + Q(tabla.Nombre) + " SET " + asignaciones + " WHERE " + Q(tabla.Clave) + " = @Clave;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    conexion.Open();
                    ValidarReferencias(conexion, tabla, entidad);
                    foreach (PropertyInfo campo in campos) Parametro(comando, tabla, campo, entidad);
                    comando.Parameters.Add("@Clave", SqlDbType.Int).Value = clave;
                    if (comando.ExecuteNonQuery() == 0) throw new InvalidOperationException("No existe el registro seleccionado en " + tabla.Nombre + ".");
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible editar el registro en " + tabla.Nombre + ".", ex); }
        }

        public void Eliminar<T>(int clave) where T : new()
        {
            Tabla tabla = Esquema(typeof(T));
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase && typeof(T) == typeof(MarcasEntidad))
            {
                marcasSupabase.Inactivar(clave);
                return;
            }
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                relacionalesSupabase.Inactivar(tabla.Nombre, tabla.Clave, clave);
                return;
            }
            PropertyInfo estado = typeof(T).GetProperty("Estado");
            if (estado == null || TipoBase(estado.PropertyType) != typeof(bool))
                throw new InvalidOperationException("La entidad " + typeof(T).Name + " no tiene un estado lógico compatible.");

            string sql = "UPDATE dbo." + Q(tabla.Nombre) + " SET [Estado] = @Estado WHERE " + Q(tabla.Clave) + " = @Clave;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                    comando.Parameters.Add("@Clave", SqlDbType.Int).Value = clave;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0) throw new InvalidOperationException("No existe el registro seleccionado en " + tabla.Nombre + ".");
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible marcar el registro como inactivo en " + tabla.Nombre + ".", ex); }
        }

        /// <summary>Marca una marca como inactiva sin borrar físicamente su registro.</summary>
        public void InactivarMarca(int codigoMarca)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                marcasSupabase.Inactivar(codigoMarca);
                return;
            }
            const string sql = "UPDATE dbo.Tbl_Marcas SET Estado = @Estado WHERE CodigoMarca = @CodigoMarca;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                    comando.Parameters.Add("@CodigoMarca", SqlDbType.Int).Value = codigoMarca;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No existe la marca seleccionada en Tbl_Marcas.");
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible marcar la marca como inactiva.", ex); }
        }

        /// <summary>Lista las filas dependientes de un encabezado usando su clave foránea.</summary>
        public List<TDetalle> ListarRelacionados<TDetalle>(string claveForanea, int claveMaestro) where TDetalle : new()
        {
            Tabla detalle = Esquema(typeof(TDetalle));
            if (!detalle.Referencias.ContainsKey(claveForanea))
                throw new ArgumentException("La relación indicada no está definida para " + detalle.Nombre + ".", "claveForanea");
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacionalesSupabase.ListarRelacionados<TDetalle>(detalle.Nombre, detalle.Clave, claveForanea, claveMaestro);
            string sql = "SELECT * FROM dbo." + Q(detalle.Nombre) + " WHERE " + Q(claveForanea) + " = @Clave ORDER BY " + Q(detalle.Clave) + ";";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Clave", SqlDbType.Int).Value = claveMaestro;
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader()) return Materializar<TDetalle>(lector);
                }
            }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible consultar los detalles relacionados.", ex); }
        }

        /// <summary>Inserta encabezado y detalles en una sola transacción.</summary>
        public int AgregarConDetalles<TMaestro, TDetalle>(TMaestro maestro, IList<TDetalle> detalles, string claveForanea)
            where TMaestro : new() where TDetalle : new()
        {
            if (ReferenceEquals(maestro, null)) throw new ArgumentNullException("maestro");
            if (detalles == null || detalles.Count == 0) throw new ArgumentException("Agrega al menos una línea de detalle.", "detalles");
            Tabla tablaMaestro = Esquema(typeof(TMaestro));
            Tabla tablaDetalle = Esquema(typeof(TDetalle));
            ValidarRelacion(tablaMaestro, tablaDetalle, claveForanea);
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacionalesSupabase.GuardarMaestroDetalle(tablaMaestro.Nombre, tablaMaestro.Clave,
                    tablaDetalle.Nombre, tablaDetalle.Clave, claveForanea, 0, maestro, detalles, "agregar",
                    tablaMaestro.Nulos, tablaDetalle.Nulos);
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                {
                    conexion.Open();
                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            ValidarReferencias(conexion, transaccion, tablaMaestro, maestro);
                            int clave = Insertar(conexion, transaccion, tablaMaestro, maestro);
                            PropertyInfo fk = typeof(TDetalle).GetProperty(claveForanea);
                            foreach (TDetalle detalle in detalles)
                            {
                                if (ReferenceEquals(detalle, null)) throw new ArgumentException("La lista contiene un detalle vacío.");
                                fk.SetValue(detalle, Convert.ChangeType(clave, TipoBase(fk.PropertyType)), null);
                                ValidarReferencias(conexion, transaccion, tablaDetalle, detalle);
                                Insertar(conexion, transaccion, tablaDetalle, detalle);
                            }
                            transaccion.Commit();
                            return clave;
                        }
                        catch { transaccion.Rollback(); throw; }
                    }
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible guardar el encabezado y sus detalles.", ex); }
        }

        /// <summary>Actualiza el encabezado y sincroniza las líneas sin cambiar las PK existentes.</summary>
        public void EditarConDetalles<TMaestro, TDetalle>(int claveMaestro, TMaestro maestro, IList<TDetalle> detalles, string claveForanea)
            where TMaestro : new() where TDetalle : new()
        {
            if (claveMaestro <= 0) throw new ArgumentException("Seleccione un registro válido.", "claveMaestro");
            if (ReferenceEquals(maestro, null)) throw new ArgumentNullException("maestro");
            if (detalles == null || detalles.Count == 0) throw new ArgumentException("El registro debe conservar al menos una línea de detalle.", "detalles");
            Tabla tablaMaestro = Esquema(typeof(TMaestro));
            Tabla tablaDetalle = Esquema(typeof(TDetalle));
            ValidarRelacion(tablaMaestro, tablaDetalle, claveForanea);
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                relacionalesSupabase.GuardarMaestroDetalle(tablaMaestro.Nombre, tablaMaestro.Clave,
                    tablaDetalle.Nombre, tablaDetalle.Clave, claveForanea, claveMaestro, maestro, detalles, "editar",
                    tablaMaestro.Nulos, tablaDetalle.Nulos);
                return;
            }
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                {
                    conexion.Open();
                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            ValidarReferencias(conexion, transaccion, tablaMaestro, maestro);
                            if (Actualizar(conexion, transaccion, tablaMaestro, claveMaestro, maestro) == 0)
                                throw new InvalidOperationException("No existe el registro seleccionado.");

                            PropertyInfo propiedadClave = typeof(TDetalle).GetProperty(tablaDetalle.Clave);
                            PropertyInfo fk = typeof(TDetalle).GetProperty(claveForanea);
                            HashSet<int> conservar = new HashSet<int>();
                            foreach (TDetalle detalle in detalles)
                            {
                                if (ReferenceEquals(detalle, null)) throw new ArgumentException("La lista contiene un detalle vacío.");
                                fk.SetValue(detalle, Convert.ChangeType(claveMaestro, TipoBase(fk.PropertyType)), null);
                                ValidarReferencias(conexion, transaccion, tablaDetalle, detalle);
                                int claveDetalle = Convert.ToInt32(propiedadClave.GetValue(detalle, null));
                                if (claveDetalle > 0)
                                {
                                    if (Actualizar(conexion, transaccion, tablaDetalle, claveDetalle, detalle) == 0)
                                        throw new InvalidOperationException("La línea seleccionada no pertenece al registro actual.");
                                    conservar.Add(claveDetalle);
                                }
                                else
                                {
                                    int nuevaClave = Insertar(conexion, transaccion, tablaDetalle, detalle);
                                    propiedadClave.SetValue(detalle, nuevaClave, null);
                                    conservar.Add(nuevaClave);
                                }
                            }

                            HashSet<int> existentes = new HashSet<int>();
                            string listar = "SELECT " + Q(tablaDetalle.Clave) + " FROM dbo." + Q(tablaDetalle.Nombre) + " WHERE " + Q(claveForanea) + " = @Clave;";
                            using (SqlCommand comando = new SqlCommand(listar, conexion, transaccion))
                            {
                                comando.Parameters.Add("@Clave", SqlDbType.Int).Value = claveMaestro;
                                using (SqlDataReader lector = comando.ExecuteReader())
                                    while (lector.Read()) existentes.Add(lector.GetInt32(0));
                            }
                            foreach (int id in existentes)
                                if (!conservar.Contains(id))
                                {
                                    using (SqlCommand comando = new SqlCommand("UPDATE dbo." + Q(tablaDetalle.Nombre) + " SET [Estado]=@Estado WHERE " + Q(tablaDetalle.Clave) + "=@Id AND " + Q(claveForanea) + "=@Clave;", conexion, transaccion))
                                    {
                                        comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                                        comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                                        comando.Parameters.Add("@Clave", SqlDbType.Int).Value = claveMaestro;
                                        comando.ExecuteNonQuery();
                                    }
                                }
                            transaccion.Commit();
                        }
                        catch { transaccion.Rollback(); throw; }
                    }
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible actualizar el encabezado y sus detalles.", ex); }
        }

        /// <summary>Inactiva los detalles y su encabezado en una transacción.</summary>
        public void EliminarConDetalles<TMaestro, TDetalle>(int claveMaestro, string claveForanea)
            where TMaestro : new() where TDetalle : new()
        {
            Tabla tablaMaestro = Esquema(typeof(TMaestro));
            Tabla tablaDetalle = Esquema(typeof(TDetalle));
            ValidarRelacion(tablaMaestro, tablaDetalle, claveForanea);
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                relacionalesSupabase.GuardarMaestroDetalle(tablaMaestro.Nombre, tablaMaestro.Clave,
                    tablaDetalle.Nombre, tablaDetalle.Clave, claveForanea, claveMaestro, null,
                    new object[0], "inactivar", tablaMaestro.Nulos, tablaDetalle.Nulos);
                return;
            }
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                {
                    conexion.Open();
                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand comando = new SqlCommand("UPDATE dbo." + Q(tablaDetalle.Nombre) + " SET [Estado]=@Estado WHERE " + Q(claveForanea) + "=@Clave;", conexion, transaccion))
                            {
                                comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                                comando.Parameters.Add("@Clave", SqlDbType.Int).Value = claveMaestro;
                                comando.ExecuteNonQuery();
                            }
                            using (SqlCommand comando = new SqlCommand("UPDATE dbo." + Q(tablaMaestro.Nombre) + " SET [Estado]=@Estado WHERE " + Q(tablaMaestro.Clave) + "=@Clave;", conexion, transaccion))
                            {
                                comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                                comando.Parameters.Add("@Clave", SqlDbType.Int).Value = claveMaestro;
                                if (comando.ExecuteNonQuery() == 0) throw new InvalidOperationException("No existe el registro seleccionado.");
                            }
                            transaccion.Commit();
                        }
                        catch { transaccion.Rollback(); throw; }
                    }
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible marcar el registro y sus detalles como inactivos.", ex); }
        }

        private static void ValidarRelacion(Tabla maestro, Tabla detalle, string claveForanea)
        {
            if (string.IsNullOrWhiteSpace(claveForanea) || !detalle.Referencias.ContainsKey(claveForanea) ||
                !string.Equals(detalle.Referencias[claveForanea], maestro.Nombre, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("La clave foránea no corresponde al encabezado indicado.", "claveForanea");
        }

        private static int Insertar<T>(SqlConnection conexion, SqlTransaction transaccion, Tabla tabla, T entidad)
        {
            PropertyInfo[] campos = Campos(typeof(T)).Where(p => p.Name != tabla.Clave).ToArray();
            string nombres = string.Join(", ", campos.Select(p => Q(p.Name)));
            string parametros = string.Join(", ", campos.Select(p => "@" + p.Name));
            string sql = "INSERT INTO dbo." + Q(tabla.Nombre) + " (" + nombres + ") OUTPUT INSERTED." + Q(tabla.Clave) + " VALUES (" + parametros + ");";
            using (SqlCommand comando = new SqlCommand(sql, conexion, transaccion))
            {
                foreach (PropertyInfo campo in campos) Parametro(comando, tabla, campo, entidad);
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }

        private static int Actualizar<T>(SqlConnection conexion, SqlTransaction transaccion, Tabla tabla, int clave, T entidad)
        {
            PropertyInfo[] campos = Campos(typeof(T)).Where(p => p.Name != tabla.Clave).ToArray();
            string asignaciones = string.Join(", ", campos.Select(p => Q(p.Name) + "=@" + p.Name));
            using (SqlCommand comando = new SqlCommand("UPDATE dbo." + Q(tabla.Nombre) + " SET " + asignaciones + " WHERE " + Q(tabla.Clave) + "=@Clave;", conexion, transaccion))
            {
                foreach (PropertyInfo campo in campos) Parametro(comando, tabla, campo, entidad);
                comando.Parameters.Add("@Clave", SqlDbType.Int).Value = clave;
                return comando.ExecuteNonQuery();
            }
        }

        private static void ValidarReferencias<T>(SqlConnection conexion, Tabla tabla, T entidad)
        {
            ValidarReferencias(conexion, null, tabla, entidad);
        }

        private static void ValidarReferencias<T>(SqlConnection conexion, SqlTransaction transaccion, Tabla tabla, T entidad)
        {
            foreach (KeyValuePair<string, string> referencia in tabla.Referencias)
            {
                PropertyInfo propiedad = typeof(T).GetProperty(referencia.Key);
                object valor = propiedad.GetValue(entidad, null);
                if (tabla.Nulos.Contains(referencia.Key) && (valor == null || Convert.ToInt32(valor) == 0)) continue;
                if (valor == null || Convert.ToInt32(valor) <= 0)
                    throw new InvalidOperationException("Seleccione un valor válido para " + referencia.Key + ".");
                string sql = "SELECT COUNT(1) FROM dbo." + Q(referencia.Value) + " WHERE " + Q(referencia.Key) + " = @Id;";
                using (SqlCommand comando = new SqlCommand(sql, conexion, transaccion))
                {
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = valor;
                    if (Convert.ToInt32(comando.ExecuteScalar()) == 0)
                        throw new InvalidOperationException("No existe la referencia " + referencia.Key + " indicada.");
                }
            }
        }

        private static void Parametro<T>(SqlCommand comando, Tabla tabla, PropertyInfo campo, T entidad)
        {
            object valor = campo.GetValue(entidad, null);
            if (tabla.Nulos.Contains(campo.Name) && (valor == null || (valor is int && (int)valor == 0))) valor = null;
            Type tipo = TipoBase(campo.PropertyType);
            SqlDbType sqlTipo = tipo == typeof(int) ? SqlDbType.Int : tipo == typeof(decimal) ? SqlDbType.Decimal :
                tipo == typeof(DateTime) ? SqlDbType.DateTime : tipo == typeof(bool) ? SqlDbType.Bit : SqlDbType.VarChar;
            SqlParameter parametro = comando.Parameters.Add("@" + campo.Name, sqlTipo,
                sqlTipo == SqlDbType.VarChar ? 4000 : 0);
            if (sqlTipo == SqlDbType.Decimal) { parametro.Precision = 10; parametro.Scale = 2; }
            parametro.Value = valor ?? DBNull.Value;
        }

        private static List<T> Materializar<T>(SqlDataReader lector) where T : new()
        {
            List<T> resultado = new List<T>();
            PropertyInfo[] propiedades = Campos(typeof(T));
            while (lector.Read())
            {
                T entidad = new T();
                foreach (PropertyInfo propiedad in propiedades)
                {
                    int indice = lector.GetOrdinal(propiedad.Name);
                    object valor = lector.IsDBNull(indice) ? null : lector.GetValue(indice);
                    if (valor == null)
                    {
                        if (propiedad.PropertyType.IsValueType && Nullable.GetUnderlyingType(propiedad.PropertyType) == null)
                        {
                            throw new InvalidOperationException("La columna " + propiedad.Name
                                + " devolvio NULL, pero la propiedad de la entidad no admite valores nulos.");
                        }

                        propiedad.SetValue(entidad, null, null);
                        continue;
                    }
                    if (valor != null && propiedad.PropertyType != valor.GetType())
                        valor = Convert.ChangeType(valor, TipoBase(propiedad.PropertyType));
                    propiedad.SetValue(entidad, valor, null);
                }
                resultado.Add(entidad);
            }
            return resultado;
        }

        private static PropertyInfo[] Campos(Type tipo)
        {
            return tipo.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite && p.GetIndexParameters().Length == 0).ToArray();
        }

        private static Type TipoBase(Type tipo)
        {
            return Nullable.GetUnderlyingType(tipo) ?? tipo;
        }

        private static string Q(string identificador)
        {
            return "[" + identificador.Replace("]", "]]" ) + "]";
        }
    }
}
