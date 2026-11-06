using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones ADO.NET de dbo.Tbl_Proveedores.</summary>
    public class ProveedoresDatos
    {
        private readonly ProveedoresSupabaseDatos proveedoresSupabase = new ProveedoresSupabaseDatos();
        private const string Columnas = @"CodigoProveedor, Nombre, Nit, Direccion, Telefono,
CorreoElectronico, NombreContacto, Descripcion, Estado";

        public List<ProveedoresEntidad> Listar()
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return proveedoresSupabase.Listar();
            return EjecutarConsulta("SELECT " + Columnas + " FROM dbo.Tbl_Proveedores ORDER BY CodigoProveedor;");
        }

        public List<ProveedoresEntidad> Buscar(string criterio)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return proveedoresSupabase.Buscar(criterio);
            const string sql = @"SELECT CodigoProveedor, Nombre, Nit, Direccion, Telefono,
CorreoElectronico, NombreContacto, Descripcion, Estado
FROM dbo.Tbl_Proveedores
WHERE CONVERT(varchar(20), CodigoProveedor) LIKE @Criterio OR Nombre LIKE @Criterio
   OR Nit LIKE @Criterio OR Direccion LIKE @Criterio OR Telefono LIKE @Criterio
   OR CorreoElectronico LIKE @Criterio OR NombreContacto LIKE @Criterio OR Descripcion LIKE @Criterio
ORDER BY CodigoProveedor;";
            return EjecutarConsulta(sql, criterio);
        }

        private List<ProveedoresEntidad> EjecutarConsulta(string sql, string criterio = null)
        {
            List<ProveedoresEntidad> lista = new List<ProveedoresEntidad>();
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    if (criterio != null)
                        comando.Parameters.Add("@Criterio", SqlDbType.VarChar, 2000).Value = "%" + criterio.Trim() + "%";
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            lista.Add(new ProveedoresEntidad(
                                lector.GetInt32(0), lector.GetString(1), TextoNulo(lector, 2),
                                lector.GetString(3), lector.GetString(4), TextoNulo(lector, 5),
                                TextoNulo(lector, 6), TextoNulo(lector, 7), lector.GetBoolean(8)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar proveedores en SQL Server.", ex);
            }
            return lista;
        }

        private static string TextoNulo(SqlDataReader lector, int indice)
        {
            return lector.IsDBNull(indice) ? null : lector.GetString(indice);
        }

        public int Agregar(ProveedoresEntidad proveedor)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return proveedoresSupabase.Agregar(proveedor);
            const string sql = @"INSERT INTO dbo.Tbl_Proveedores
(Nombre, Nit, Direccion, Telefono, CorreoElectronico, NombreContacto, Descripcion, Estado)
OUTPUT INSERTED.CodigoProveedor
VALUES (@Nombre, @Nit, @Direccion, @Telefono, @CorreoElectronico, @NombreContacto, @Descripcion, @Estado);";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    AgregarParametros(comando, proveedor);
                    conexion.Open();
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar el proveedor en SQL Server.", ex);
            }
        }

        public void Editar(ProveedoresEntidad proveedor)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                proveedoresSupabase.Editar(proveedor);
                return;
            }
            const string sql = @"UPDATE dbo.Tbl_Proveedores SET Nombre=@Nombre, Nit=@Nit,
Direccion=@Direccion, Telefono=@Telefono, CorreoElectronico=@CorreoElectronico,
NombreContacto=@NombreContacto, Descripcion=@Descripcion, Estado=@Estado
WHERE CodigoProveedor=@CodigoProveedor;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    AgregarParametros(comando, proveedor);
                    comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = proveedor.CodigoProveedor;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró el proveedor para editar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar el proveedor en SQL Server.", ex);
            }
        }

        public void Eliminar(int codigoProveedor)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                proveedoresSupabase.Inactivar(codigoProveedor);
                return;
            }
            const string sql = "UPDATE dbo.Tbl_Proveedores SET Estado=@Estado WHERE CodigoProveedor=@CodigoProveedor;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                    comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = codigoProveedor;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró el proveedor para eliminar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible eliminar el proveedor en SQL Server.", ex);
            }
        }

        private static void AgregarParametros(SqlCommand comando, ProveedoresEntidad proveedor)
        {
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = proveedor.Nombre;
            comando.Parameters.Add("@Nit", SqlDbType.VarChar, 45).Value = (object)proveedor.Nit ?? DBNull.Value;
            comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = proveedor.Direccion;
            comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = proveedor.Telefono;
            comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 100).Value = (object)proveedor.CorreoElectronico ?? DBNull.Value;
            comando.Parameters.Add("@NombreContacto", SqlDbType.VarChar, 100).Value = (object)proveedor.NombreContacto ?? DBNull.Value;
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = (object)proveedor.Descripcion ?? DBNull.Value;
            comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = proveedor.Estado;
        }
    }
}
