using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Operaciones ADO.NET de dbo.Tbl_Usuarios.</summary>
    public class UsuariosDatos
    {
        private const string Columnas = @"CodigoUsuario, TipoUsuario, Nombre,
CorreoElectronico, FechaCreacion, FechaUltimoAcceso, Estado";

        public List<UsuariosEntidad> Listar()
        {
            return EjecutarConsulta("SELECT " + Columnas + " FROM dbo.Tbl_Usuarios ORDER BY CodigoUsuario;");
        }

        public List<UsuariosEntidad> Buscar(string criterio)
        {
            const string sql = @"SELECT CodigoUsuario, TipoUsuario, Nombre,
CorreoElectronico, FechaCreacion, FechaUltimoAcceso, Estado
FROM dbo.Tbl_Usuarios
WHERE CONVERT(varchar(20), CodigoUsuario) LIKE @Criterio OR TipoUsuario LIKE @Criterio
   OR Nombre LIKE @Criterio OR CorreoElectronico LIKE @Criterio
ORDER BY CodigoUsuario;";
            return EjecutarConsulta(sql, criterio);
        }

        /// <summary>Obtiene una cuenta activa por su nombre para validar el acceso.</summary>
        public UsuariosEntidad BuscarActivoPorNombre(string nombre)
        {
            const string sql = @"SELECT TOP (2) CodigoUsuario, TipoUsuario, Nombre, Contrasenia,
CorreoElectronico, FechaCreacion, FechaUltimoAcceso, Estado
FROM dbo.Tbl_Usuarios
WHERE Nombre = @Nombre AND Estado = 1
ORDER BY CodigoUsuario;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombre.Trim();
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read()) return null;
                        UsuariosEntidad usuario = new UsuariosEntidad(
                            lector.GetInt32(0), lector.GetString(1), lector.GetString(2), lector.GetString(3),
                            lector.GetString(4), lector.GetDateTime(5),
                            lector.IsDBNull(6) ? (DateTime?)null : lector.GetDateTime(6), lector.GetBoolean(7));
                        if (lector.Read())
                            throw new InvalidOperationException("Hay más de una cuenta activa con ese nombre. Contacta al administrador.");
                        return usuario;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible validar el usuario en SQL Server.", ex);
            }
        }

        /// <summary>Guarda el último acceso solo si la cuenta sigue activa.</summary>
        public void ActualizarUltimoAcceso(int codigoUsuario, DateTime fechaAcceso)
        {
            const string sql = @"UPDATE dbo.Tbl_Usuarios
SET FechaUltimoAcceso = @FechaUltimoAcceso
WHERE CodigoUsuario = @CodigoUsuario AND Estado = 1;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@FechaUltimoAcceso", SqlDbType.DateTime).Value = fechaAcceso;
                    comando.Parameters.Add("@CodigoUsuario", SqlDbType.Int).Value = codigoUsuario;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("La cuenta ya no está activa. Vuelve a intentarlo.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible registrar el último acceso.", ex);
            }
        }

        private List<UsuariosEntidad> EjecutarConsulta(string sql, string criterio = null)
        {
            List<UsuariosEntidad> lista = new List<UsuariosEntidad>();
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
                            lista.Add(new UsuariosEntidad(
                                lector.GetInt32(0), lector.GetString(1), lector.GetString(2), string.Empty,
                                lector.GetString(3), lector.GetDateTime(4),
                                lector.IsDBNull(5) ? (DateTime?)null : lector.GetDateTime(5), lector.GetBoolean(6)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar usuarios en SQL Server.", ex);
            }
            return lista;
        }

        public int Agregar(UsuariosEntidad usuario)
        {
            const string sql = @"INSERT INTO dbo.Tbl_Usuarios
(TipoUsuario, Nombre, Contrasenia, CorreoElectronico, FechaCreacion, FechaUltimoAcceso, Estado)
OUTPUT INSERTED.CodigoUsuario
VALUES (@TipoUsuario, @Nombre, @Contrasenia, @CorreoElectronico, @FechaCreacion, @FechaUltimoAcceso, @Estado);";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    AgregarParametros(comando, usuario);
                    conexion.Open();
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar el usuario en SQL Server.", ex);
            }
        }

        public void Editar(UsuariosEntidad usuario)
        {
            const string sql = @"UPDATE dbo.Tbl_Usuarios SET TipoUsuario=@TipoUsuario, Nombre=@Nombre,
Contrasenia=CASE WHEN NULLIF(@Contrasenia, '') IS NULL THEN Contrasenia ELSE @Contrasenia END,
CorreoElectronico=@CorreoElectronico, FechaCreacion=@FechaCreacion,
FechaUltimoAcceso=@FechaUltimoAcceso, Estado=@Estado
WHERE CodigoUsuario=@CodigoUsuario;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    AgregarParametros(comando, usuario);
                    comando.Parameters.Add("@CodigoUsuario", SqlDbType.Int).Value = usuario.CodigoUsuario;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró el usuario para editar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar el usuario en SQL Server.", ex);
            }
        }

        public void Eliminar(int codigoUsuario)
        {
            const string sql = "UPDATE dbo.Tbl_Usuarios SET Estado=@Estado WHERE CodigoUsuario=@CodigoUsuario;";
            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                    comando.Parameters.Add("@CodigoUsuario", SqlDbType.Int).Value = codigoUsuario;
                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró el usuario para eliminar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible eliminar el usuario en SQL Server.", ex);
            }
        }

        private static void AgregarParametros(SqlCommand comando, UsuariosEntidad usuario)
        {
            comando.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, 100).Value = usuario.TipoUsuario;
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = usuario.Nombre;
            comando.Parameters.Add("@Contrasenia", SqlDbType.VarChar, 100).Value = usuario.Contrasenia;
            comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 100).Value = usuario.CorreoElectronico;
            comando.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = usuario.FechaCreacion;
            comando.Parameters.Add("@FechaUltimoAcceso", SqlDbType.DateTime).Value = (object)usuario.FechaUltimoAcceso ?? DBNull.Value;
            comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = usuario.Estado;
        }
    }
}
