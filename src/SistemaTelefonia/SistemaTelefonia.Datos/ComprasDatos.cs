using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Consulta registros de dbo.Tbl_Compras usando ADO.NET.</summary>
    public class ComprasDatos
    {
        private readonly CrudRelacionalDatos relacional = new CrudRelacionalDatos();

        public List<ComprasEntidad> Buscar(string criterio)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacional.Buscar<ComprasEntidad>(criterio);
            const string sql = @"SELECT CodigoCompra, CodigoProveedor, NumeroDocumento,
FechaCompra, Subtotal, Iva, Total, Observaciones, Estado
FROM dbo.Tbl_Compras
WHERE CONVERT(varchar(20), CodigoCompra) LIKE @Criterio
   OR CONVERT(varchar(20), CodigoProveedor) LIKE @Criterio
   OR NumeroDocumento LIKE @Criterio
   OR Observaciones LIKE @Criterio
ORDER BY CodigoCompra;";

            List<ComprasEntidad> compras = new List<ComprasEntidad>();
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Criterio", SqlDbType.VarChar, 252).Value = "%" + criterio.Trim() + "%";
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            compras.Add(new ComprasEntidad(
                                lector.GetInt32(0),
                                lector.GetInt32(1),
                                lector.GetString(2),
                                lector.GetDateTime(3),
                                lector.GetDecimal(4),
                                lector.GetDecimal(5),
                                lector.GetDecimal(6),
                                lector.IsDBNull(7) ? null : lector.GetString(7),
                                lector.GetBoolean(8)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible buscar compras en SQL Server.", ex);
            }

            return compras;
        }

        public List<ComprasEntidad> Listar()
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacional.Listar<ComprasEntidad>();
            const string sql = @"SELECT CodigoCompra, CodigoProveedor, NumeroDocumento,
FechaCompra, Subtotal, Iva, Total, Observaciones, Estado
FROM dbo.Tbl_Compras
ORDER BY CodigoCompra;";

            List<ComprasEntidad> compras = new List<ComprasEntidad>();
            try
            {
                // Se reutiliza la conexión común definida para el proyecto.
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            compras.Add(new ComprasEntidad(
                                lector.GetInt32(0),
                                lector.GetInt32(1),
                                lector.GetString(2),
                                lector.GetDateTime(3),
                                lector.GetDecimal(4),
                                lector.GetDecimal(5),
                                lector.GetDecimal(6),
                                lector.IsDBNull(7) ? null : lector.GetString(7),
                                lector.GetBoolean(8)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar las compras en SQL Server.", ex);
            }

            return compras;
        }

        public int Agregar(ComprasEntidad compra)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacional.Agregar(compra);
            const string sqlProveedor = "SELECT COUNT(1) FROM dbo.Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor;";
            const string sqlInsertar = @"INSERT INTO dbo.Tbl_Compras
(CodigoProveedor, NumeroDocumento, FechaCompra, Subtotal, Iva, Total, Observaciones, Estado)
OUTPUT INSERTED.CodigoCompra
VALUES (@CodigoProveedor, @NumeroDocumento, @FechaCompra, @Subtotal, @Iva, @Total, @Observaciones, @Estado);";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                {
                    conexion.Open();
                    // Comprueba que el proveedor exista antes de insertar la fila relacionada.
                    using (SqlCommand validarProveedor = new SqlCommand(sqlProveedor, conexion))
                    {
                        validarProveedor.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = compra.CodigoProveedor;
                        if (Convert.ToInt32(validarProveedor.ExecuteScalar()) == 0)
                            throw new InvalidOperationException("El código de proveedor no existe.");
                    }

                    using (SqlCommand comando = new SqlCommand(sqlInsertar, conexion))
                    {
                        comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = compra.CodigoProveedor;
                        comando.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 50).Value = compra.NumeroDocumento;
                        comando.Parameters.Add("@FechaCompra", SqlDbType.DateTime).Value = compra.FechaCompra;
                        AgregarParametroDecimal(comando, "@Subtotal", compra.Subtotal);
                        AgregarParametroDecimal(comando, "@Iva", compra.Iva);
                        AgregarParametroDecimal(comando, "@Total", compra.Total);
                        comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value =
                            (object)compra.Observaciones ?? DBNull.Value;
                        comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = compra.Estado;
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar la compra en SQL Server.", ex);
            }
        }

        /// <summary>Guarda el encabezado y sus detalles dentro de una sola transacción.</summary>
        public int AgregarConDetalles(ComprasEntidad compra, IList<DetalleCompraEntidad> detalles)
        {
            if (compra == null) throw new ArgumentNullException("compra");
            if (detalles == null || detalles.Count == 0)
                throw new ArgumentException("Agrega al menos un equipo al detalle de la compra.", "detalles");
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacional.AgregarConDetalles<ComprasEntidad, DetalleCompraEntidad>(compra, detalles, "CodigoCompra");

            const string validarProveedor = "SELECT COUNT(1) FROM dbo.Tbl_Proveedores WHERE CodigoProveedor = @Codigo;";
            const string validarEquipo = "SELECT COUNT(1) FROM dbo.Tbl_Equipos WHERE CodigoEquipo = @Codigo;";
            const string insertarCompra = @"INSERT INTO dbo.Tbl_Compras
(CodigoProveedor, NumeroDocumento, FechaCompra, Subtotal, Iva, Total, Observaciones, Estado)
OUTPUT INSERTED.CodigoCompra
VALUES (@CodigoProveedor, @NumeroDocumento, @FechaCompra, @Subtotal, @Iva, @Total, @Observaciones, @Estado);";
            const string insertarDetalle = @"INSERT INTO dbo.Tbl_DetalleCompra
(CodigoCompra, CodigoEquipo, Cantidad, CostoUnitario, Subtotal, Observaciones, Estado)
VALUES (@CodigoCompra, @CodigoEquipo, @Cantidad, @CostoUnitario, @Subtotal, @Observaciones, @Estado);";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                {
                    conexion.Open();
                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand validar = new SqlCommand(validarProveedor, conexion, transaccion))
                            {
                                validar.Parameters.Add("@Codigo", SqlDbType.Int).Value = compra.CodigoProveedor;
                                if (Convert.ToInt32(validar.ExecuteScalar()) == 0)
                                    throw new InvalidOperationException("El código de proveedor no existe.");
                            }

                            int codigoCompra;
                            using (SqlCommand comando = new SqlCommand(insertarCompra, conexion, transaccion))
                            {
                                comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = compra.CodigoProveedor;
                                comando.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 50).Value = compra.NumeroDocumento;
                                comando.Parameters.Add("@FechaCompra", SqlDbType.DateTime).Value = compra.FechaCompra;
                                AgregarParametroDecimal(comando, "@Subtotal", compra.Subtotal);
                                AgregarParametroDecimal(comando, "@Iva", compra.Iva);
                                AgregarParametroDecimal(comando, "@Total", compra.Total);
                                comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value = (object)compra.Observaciones ?? DBNull.Value;
                                comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = compra.Estado;
                                codigoCompra = Convert.ToInt32(comando.ExecuteScalar());
                            }

                            foreach (DetalleCompraEntidad detalle in detalles)
                            {
                                using (SqlCommand validar = new SqlCommand(validarEquipo, conexion, transaccion))
                                {
                                    validar.Parameters.Add("@Codigo", SqlDbType.Int).Value = detalle.CodigoEquipo;
                                    if (Convert.ToInt32(validar.ExecuteScalar()) == 0)
                                        throw new InvalidOperationException("No existe el equipo con código " + detalle.CodigoEquipo + ".");
                                }

                                using (SqlCommand comando = new SqlCommand(insertarDetalle, conexion, transaccion))
                                {
                                    comando.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                                    comando.Parameters.Add("@CodigoEquipo", SqlDbType.Int).Value = detalle.CodigoEquipo;
                                    comando.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                                    AgregarParametroDecimal(comando, "@CostoUnitario", detalle.CostoUnitario);
                                    AgregarParametroDecimal(comando, "@Subtotal", detalle.Subtotal);
                                    comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value = (object)detalle.Observaciones ?? DBNull.Value;
                                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = detalle.Estado;
                                    comando.ExecuteNonQuery();
                                }
                            }

                            transaccion.Commit();
                            return codigoCompra;
                        }
                        catch
                        {
                            transaccion.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible guardar la compra y sus detalles en SQL Server.", ex);
            }
        }

        public void Editar(int codigoCompra, ComprasEntidad compra)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                relacional.Editar(codigoCompra, compra);
                return;
            }
            const string sqlProveedor = "SELECT COUNT(1) FROM dbo.Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor;";
            const string sqlActualizar = @"UPDATE dbo.Tbl_Compras
SET CodigoProveedor = @CodigoProveedor,
    NumeroDocumento = @NumeroDocumento,
    FechaCompra = @FechaCompra,
    Subtotal = @Subtotal,
    Iva = @Iva,
    Total = @Total,
    Observaciones = @Observaciones,
    Estado = @Estado
WHERE CodigoCompra = @CodigoCompra;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                {
                    conexion.Open();
                    // Comprueba que la nueva referencia conserve la relación válida con Proveedores.
                    using (SqlCommand validarProveedor = new SqlCommand(sqlProveedor, conexion))
                    {
                        validarProveedor.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = compra.CodigoProveedor;
                        if (Convert.ToInt32(validarProveedor.ExecuteScalar()) == 0)
                            throw new InvalidOperationException("El código de proveedor no existe.");
                    }

                    using (SqlCommand comando = new SqlCommand(sqlActualizar, conexion))
                    {
                        comando.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                        comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = compra.CodigoProveedor;
                        comando.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 50).Value = compra.NumeroDocumento;
                        comando.Parameters.Add("@FechaCompra", SqlDbType.DateTime).Value = compra.FechaCompra;
                        AgregarParametroDecimal(comando, "@Subtotal", compra.Subtotal);
                        AgregarParametroDecimal(comando, "@Iva", compra.Iva);
                        AgregarParametroDecimal(comando, "@Total", compra.Total);
                        comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value =
                            (object)compra.Observaciones ?? DBNull.Value;
                        comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = compra.Estado;

                        if (comando.ExecuteNonQuery() == 0)
                            throw new InvalidOperationException("No existe la compra seleccionada para editar.");
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar la compra en SQL Server.", ex);
            }
        }

        public List<DetalleCompraEntidad> ListarDetalles(int codigoCompra)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return relacional.ListarRelacionados<DetalleCompraEntidad>("CodigoCompra", codigoCompra);
            const string sql = @"SELECT CodigoDetalleCompra, CodigoCompra, CodigoEquipo, Cantidad,
CostoUnitario, Subtotal, Observaciones, Estado
FROM dbo.Tbl_DetalleCompra WHERE CodigoCompra = @CodigoCompra
ORDER BY CodigoDetalleCompra;";
            List<DetalleCompraEntidad> resultado = new List<DetalleCompraEntidad>();
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                            resultado.Add(new DetalleCompraEntidad(lector.GetInt32(0), lector.GetInt32(1), lector.GetInt32(2),
                                lector.GetInt32(3), lector.GetDecimal(4), lector.GetDecimal(5),
                                lector.IsDBNull(6) ? null : lector.GetString(6), lector.GetBoolean(7)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar los detalles de la compra.", ex);
            }
            return resultado;
        }

        /// <summary>Devuelve el costo más reciente de compra registrado para un equipo.</summary>
        public decimal? ObtenerUltimoCostoPorEquipo(int codigoEquipo)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                List<ComprasEntidad> compras = relacional.Listar<ComprasEntidad>().Where(c => c.Estado).ToList();
                Dictionary<int, DateTime> fechas = compras.ToDictionary(c => c.CodigoCompra, c => c.FechaCompra);
                return relacional.Listar<DetalleCompraEntidad>()
                    .Where(d => d.Estado && d.CodigoEquipo == codigoEquipo && fechas.ContainsKey(d.CodigoCompra))
                    .OrderByDescending(d => fechas[d.CodigoCompra])
                    .ThenByDescending(d => d.CodigoCompra)
                    .ThenByDescending(d => d.CodigoDetalleCompra)
                    .Select(d => (decimal?)d.CostoUnitario).FirstOrDefault();
            }
            const string sql = @"SELECT TOP (1) d.CostoUnitario
FROM dbo.Tbl_DetalleCompra d
INNER JOIN dbo.Tbl_Compras c ON c.CodigoCompra = d.CodigoCompra
WHERE d.CodigoEquipo = @CodigoEquipo AND d.Estado = 1 AND c.Estado = 1
ORDER BY c.FechaCompra DESC, c.CodigoCompra DESC, d.CodigoDetalleCompra DESC;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@CodigoEquipo", SqlDbType.Int).Value = codigoEquipo;
                    conexion.Open();
                    object resultado = comando.ExecuteScalar();
                    return resultado == null || resultado == DBNull.Value ? (decimal?)null : Convert.ToDecimal(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar el último costo de compra del equipo.", ex);
            }
        }

        public void EditarConDetalles(int codigoCompra, ComprasEntidad compra, IList<DetalleCompraEntidad> detalles)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                relacional.EditarConDetalles<ComprasEntidad, DetalleCompraEntidad>(
                    codigoCompra, compra, detalles, "CodigoCompra");
                return;
            }
            const string actualizarCompra = @"UPDATE dbo.Tbl_Compras SET CodigoProveedor=@Proveedor,
NumeroDocumento=@Documento, FechaCompra=@Fecha, Subtotal=@Subtotal, Iva=@Iva, Total=@Total,
Observaciones=@Observaciones, Estado=@Estado WHERE CodigoCompra=@CodigoCompra;";
            const string validarProveedor = "SELECT COUNT(1) FROM dbo.Tbl_Proveedores WHERE CodigoProveedor=@Proveedor;";
            const string validarEquipo = "SELECT COUNT(1) FROM dbo.Tbl_Equipos WHERE CodigoEquipo=@Equipo;";
            const string actualizarDetalle = @"UPDATE dbo.Tbl_DetalleCompra SET CodigoEquipo=@Equipo, Cantidad=@Cantidad,
CostoUnitario=@Costo, Subtotal=@DetalleSubtotal, Observaciones=@DetalleObservaciones, Estado=@DetalleEstado
WHERE CodigoDetalleCompra=@CodigoDetalle AND CodigoCompra=@CodigoCompra;";
            const string insertarDetalle = @"INSERT INTO dbo.Tbl_DetalleCompra
(CodigoCompra,CodigoEquipo,Cantidad,CostoUnitario,Subtotal,Observaciones,Estado)
VALUES (@CodigoCompra,@Equipo,@Cantidad,@Costo,@DetalleSubtotal,@DetalleObservaciones,@DetalleEstado);";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                {
                    conexion.Open();
                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand validar = new SqlCommand(validarProveedor, conexion, transaccion))
                            {
                                validar.Parameters.Add("@Proveedor", SqlDbType.Int).Value = compra.CodigoProveedor;
                                if (Convert.ToInt32(validar.ExecuteScalar()) == 0) throw new InvalidOperationException("El código de proveedor no existe.");
                            }
                            using (SqlCommand comando = new SqlCommand(actualizarCompra, conexion, transaccion))
                            {
                                comando.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                                comando.Parameters.Add("@Proveedor", SqlDbType.Int).Value = compra.CodigoProveedor;
                                comando.Parameters.Add("@Documento", SqlDbType.VarChar, 50).Value = compra.NumeroDocumento;
                                comando.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = compra.FechaCompra;
                                AgregarParametroDecimal(comando, "@Subtotal", compra.Subtotal);
                                AgregarParametroDecimal(comando, "@Iva", compra.Iva);
                                AgregarParametroDecimal(comando, "@Total", compra.Total);
                                comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value = (object)compra.Observaciones ?? DBNull.Value;
                                comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = compra.Estado;
                                if (comando.ExecuteNonQuery() == 0) throw new InvalidOperationException("No existe la compra seleccionada.");
                            }

                            HashSet<int> conservar = new HashSet<int>();
                            foreach (DetalleCompraEntidad detalle in detalles)
                            {
                                using (SqlCommand validar = new SqlCommand(validarEquipo, conexion, transaccion))
                                {
                                    validar.Parameters.Add("@Equipo", SqlDbType.Int).Value = detalle.CodigoEquipo;
                                    if (Convert.ToInt32(validar.ExecuteScalar()) == 0) throw new InvalidOperationException("No existe el equipo con código " + detalle.CodigoEquipo + ".");
                                }
                                using (SqlCommand comando = new SqlCommand(detalle.CodigoDetalleCompra > 0 ? actualizarDetalle : insertarDetalle, conexion, transaccion))
                                {
                                    comando.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                                    comando.Parameters.Add("@Equipo", SqlDbType.Int).Value = detalle.CodigoEquipo;
                                    comando.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                                    AgregarParametroDecimal(comando, "@Costo", detalle.CostoUnitario);
                                    AgregarParametroDecimal(comando, "@DetalleSubtotal", detalle.Subtotal);
                                    comando.Parameters.Add("@DetalleObservaciones", SqlDbType.VarChar, 200).Value = (object)detalle.Observaciones ?? DBNull.Value;
                                    comando.Parameters.Add("@DetalleEstado", SqlDbType.Bit).Value = detalle.Estado;
                                    if (detalle.CodigoDetalleCompra > 0)
                                    {
                                        comando.Parameters.Add("@CodigoDetalle", SqlDbType.Int).Value = detalle.CodigoDetalleCompra;
                                        if (comando.ExecuteNonQuery() == 0) throw new InvalidOperationException("Un detalle ya no pertenece a esta compra.");
                                        conservar.Add(detalle.CodigoDetalleCompra);
                                    }
                                    else comando.ExecuteNonQuery();
                                }
                            }
                            HashSet<int> existentes = new HashSet<int>();
                            using (SqlCommand listar = new SqlCommand(
                                "SELECT CodigoDetalleCompra FROM dbo.Tbl_DetalleCompra WHERE CodigoCompra=@CodigoCompra;",
                                conexion, transaccion))
                            {
                                listar.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                                using (SqlDataReader lector = listar.ExecuteReader())
                                    while (lector.Read()) existentes.Add(lector.GetInt32(0));
                            }
                            foreach (int id in existentes)
                            {
                                if (conservar.Contains(id)) continue;
                                using (SqlCommand inactivar = new SqlCommand(
                                    "UPDATE dbo.Tbl_DetalleCompra SET Estado=@Estado WHERE CodigoCompra=@CodigoCompra AND CodigoDetalleCompra=@CodigoDetalle;",
                                    conexion, transaccion))
                                {
                                    inactivar.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                                    inactivar.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                                    inactivar.Parameters.Add("@CodigoDetalle", SqlDbType.Int).Value = id;
                                    inactivar.ExecuteNonQuery();
                                }
                            }
                            transaccion.Commit();
                        }
                        catch { transaccion.Rollback(); throw; }
                    }
                }
            }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex) { throw new InvalidOperationException("No fue posible actualizar la compra y sus detalles.", ex); }
        }

        public void Eliminar(int codigoCompra)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                relacional.EliminarConDetalles<ComprasEntidad, DetalleCompraEntidad>(codigoCompra, "CodigoCompra");
                return;
            }
            const string sqlDetalles = "UPDATE dbo.Tbl_DetalleCompra SET Estado=@Estado WHERE CodigoCompra = @CodigoCompra;";
            const string sqlCompra = "UPDATE dbo.Tbl_Compras SET Estado=@Estado WHERE CodigoCompra = @CodigoCompra;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                {
                    conexion.Open();
                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand detalles = new SqlCommand(sqlDetalles, conexion, transaccion))
                            {
                                detalles.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                                detalles.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                                detalles.ExecuteNonQuery();
                            }
                            using (SqlCommand compra = new SqlCommand(sqlCompra, conexion, transaccion))
                            {
                                compra.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                                compra.Parameters.Add("@CodigoCompra", SqlDbType.Int).Value = codigoCompra;
                                if (compra.ExecuteNonQuery() == 0) throw new InvalidOperationException("No existe la compra seleccionada para inactivar.");
                            }
                            transaccion.Commit();
                        }
                        catch { transaccion.Rollback(); throw; }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "No fue posible marcar la compra y sus detalles como inactivos.", ex);
            }
        }

        private static void AgregarParametroDecimal(SqlCommand comando, string nombre, decimal valor)
        {
            SqlParameter parametro = comando.Parameters.Add(nombre, SqlDbType.Decimal);
            parametro.Precision = 10;
            parametro.Scale = 2;
            parametro.Value = valor;
        }
    }
}
