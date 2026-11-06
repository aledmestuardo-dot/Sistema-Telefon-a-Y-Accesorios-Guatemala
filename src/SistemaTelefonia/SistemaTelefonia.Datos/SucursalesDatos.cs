using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones ADO.NET de dbo.Tbl_Sucursales.</summary>
    public class SucursalesDatos
    {
        private readonly SucursalesSupabaseDatos sucursalesSupabase = new SucursalesSupabaseDatos();
        private const string Columnas = @"CodigoSucursal, Nombre, Direccion, Municipio, Departamento,
Telefono, HorarioAtencion, CorreoElectronico, FechaApertura, Estado";

        public List<SucursalesEntidad> Listar()
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return sucursalesSupabase.Listar();
            return EjecutarConsulta("SELECT " + Columnas + " FROM dbo.Tbl_Sucursales ORDER BY CodigoSucursal;");
        }

        public List<SucursalesEntidad> Buscar(string criterio)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return sucursalesSupabase.Buscar(criterio);
            const string sql = @"SELECT CodigoSucursal, Nombre, Direccion, Municipio, Departamento,
Telefono, HorarioAtencion, CorreoElectronico, FechaApertura, Estado
FROM dbo.Tbl_Sucursales
WHERE CONVERT(varchar(20), CodigoSucursal) LIKE @Criterio OR Nombre LIKE @Criterio
   OR Direccion LIKE @Criterio OR Municipio LIKE @Criterio OR Departamento LIKE @Criterio
   OR Telefono LIKE @Criterio OR HorarioAtencion LIKE @Criterio OR CorreoElectronico LIKE @Criterio
ORDER BY CodigoSucursal;";
            return EjecutarConsulta(sql, criterio);
        }

        private List<SucursalesEntidad> EjecutarConsulta(string sql, string criterio = null)
        {
            List<SucursalesEntidad> lista = new List<SucursalesEntidad>();
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
                            lista.Add(new SucursalesEntidad(
                                lector.GetInt32(0), lector.GetString(1), lector.GetString(2),
                                lector.GetString(3), lector.GetString(4), lector.GetString(5),
                                lector.GetString(6), TextoNulo(lector, 7), lector.GetDateTime(8), lector.GetBoolean(9)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar sucursales en SQL Server.", ex);
            }
            return lista;
        }

        private static string TextoNulo(SqlDataReader lector, int indice)
        {
            return lector.IsDBNull(indice) ? null : lector.GetString(indice);
        }

        public int Agregar(SucursalesEntidad sucursal)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return sucursalesSupabase.Agregar(sucursal);
            const string sql = @"INSERT INTO dbo.Tbl_Sucursales
(Nombre, Direccion, Municipio, Departamento, Telefono, HorarioAtencion, CorreoElectronico, FechaApertura, Estado)
OUTPUT INSERTED.CodigoSucursal
VALUES (@Nombre, @Direccion, @Municipio, @Departamento, @Telefono, @HorarioAtencion, @CorreoElectronico, @FechaApertura, @Estado);";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    AgregarParametros(comando, sucursal);
                    conexion.Open();
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar la sucursal en SQL Server.", ex);
            }
        }

        public void Editar(SucursalesEntidad sucursal)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                sucursalesSupabase.Editar(sucursal);
                return;
            }
            const string sql = @"UPDATE dbo.Tbl_Sucursales SET Nombre=@Nombre, Direccion=@Direccion,
Municipio=@Municipio, Departamento=@Departamento, Telefono=@Telefono, HorarioAtencion=@HorarioAtencion,
CorreoElectronico=@CorreoElectronico, FechaApertura=@FechaApertura, Estado=@Estado
WHERE CodigoSucursal=@CodigoSucursal;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    AgregarParametros(comando, sucursal);
                    comando.Parameters.Add("@CodigoSucursal", SqlDbType.Int).Value = sucursal.CodigoSucursal;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró la sucursal para editar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar la sucursal en SQL Server.", ex);
            }
        }

        public void Eliminar(int codigoSucursal)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                sucursalesSupabase.Inactivar(codigoSucursal);
                return;
            }
            const string sql = "UPDATE dbo.Tbl_Sucursales SET Estado=@Estado WHERE CodigoSucursal=@CodigoSucursal;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                    comando.Parameters.Add("@CodigoSucursal", SqlDbType.Int).Value = codigoSucursal;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró la sucursal para eliminar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible eliminar la sucursal en SQL Server.", ex);
            }
        }

        private static void AgregarParametros(SqlCommand comando, SucursalesEntidad sucursal)
        {
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = sucursal.Nombre;
            comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = sucursal.Direccion;
            comando.Parameters.Add("@Municipio", SqlDbType.VarChar, 100).Value = sucursal.Municipio;
            comando.Parameters.Add("@Departamento", SqlDbType.VarChar, 100).Value = sucursal.Departamento;
            comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = sucursal.Telefono;
            comando.Parameters.Add("@HorarioAtencion", SqlDbType.VarChar, 100).Value = sucursal.HorarioAtencion;
            comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 100).Value = (object)sucursal.CorreoElectronico ?? DBNull.Value;
            comando.Parameters.Add("@FechaApertura", SqlDbType.DateTime).Value = sucursal.FechaApertura;
            comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = sucursal.Estado;
        }
    }
}
