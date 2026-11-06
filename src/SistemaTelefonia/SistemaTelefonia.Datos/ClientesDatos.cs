using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>
    /// Consulta los clientes de SQL Server usando ADO.NET.
    /// </summary>
    public class ClientesDatos
    {
        private readonly ClientesSupabaseDatos clientesSupabase = new ClientesSupabaseDatos();

        //Hola gaby..
        public int Agregar(ClientesEntidad cliente)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return clientesSupabase.Agregar(cliente);
            const string consulta = @"INSERT INTO dbo.Tbl_Clientes
(Nombre, Nit, CorreoElectronico, Telefono, Direccion, Departamento, Municipio, Estado)
OUTPUT INSERTED.CodigoCliente
VALUES (@Nombre, @Nit, @CorreoElectronico, @Telefono, @Direccion, @Departamento, @Municipio, @Estado);";

            try
            {
                // La PK es IDENTITY, por eso no se incluye en el INSERT.
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = cliente.Nombre;
                    comando.Parameters.Add("@Nit", SqlDbType.VarChar, 45).Value = (object)cliente.Nit ?? DBNull.Value;
                    comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 100).Value =
                        (object)cliente.CorreoElectronico ?? DBNull.Value;
                    comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = cliente.Telefono;
                    comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = cliente.Direccion;
                    comando.Parameters.Add("@Departamento", SqlDbType.VarChar, 100).Value = cliente.Departamento;
                    comando.Parameters.Add("@Municipio", SqlDbType.VarChar, 100).Value = cliente.Municipio;
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = cliente.Estado;

                    conexion.Open();
                    return (int)comando.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar el cliente en SQL Server.", ex);
            }
        }

        public List<ClientesEntidad> Listar()
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return clientesSupabase.Listar();
            List<ClientesEntidad> clientes = new List<ClientesEntidad>();
            const string consulta = @"SELECT CodigoCliente, Nombre, Nit, CorreoElectronico,
Telefono, Direccion, Departamento, Municipio, Estado
FROM dbo.Tbl_Clientes
ORDER BY CodigoCliente;";

            try
            {
                // La conexión se obtiene de la configuración creada en el Documento 01.
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            clientes.Add(new ClientesEntidad(
                                lector.GetInt32(0),
                                lector.GetString(1),
                                lector.IsDBNull(2) ? null : lector.GetString(2),
                                lector.IsDBNull(3) ? null : lector.GetString(3),
                                lector.GetString(4),
                                lector.GetString(5),
                                lector.GetString(6),
                                lector.GetString(7),
                                lector.GetBoolean(8)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar los clientes en SQL Server.", ex);
            }

            return clientes;
        }

        public void Editar(ClientesEntidad cliente)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                clientesSupabase.Editar(cliente);
                return;
            }
            const string consulta = @"UPDATE dbo.Tbl_Clientes SET
Nombre = @Nombre, Nit = @Nit, CorreoElectronico = @CorreoElectronico,
Telefono = @Telefono, Direccion = @Direccion, Departamento = @Departamento,
Municipio = @Municipio, Estado = @Estado
WHERE CodigoCliente = @CodigoCliente;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = cliente.Nombre;
                    comando.Parameters.Add("@Nit", SqlDbType.VarChar, 45).Value = (object)cliente.Nit ?? DBNull.Value;
                    comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 100).Value =
                        (object)cliente.CorreoElectronico ?? DBNull.Value;
                    comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = cliente.Telefono;
                    comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = cliente.Direccion;
                    comando.Parameters.Add("@Departamento", SqlDbType.VarChar, 100).Value = cliente.Departamento;
                    comando.Parameters.Add("@Municipio", SqlDbType.VarChar, 100).Value = cliente.Municipio;
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = cliente.Estado;
                    comando.Parameters.Add("@CodigoCliente", SqlDbType.Int).Value = cliente.CodigoCliente;

                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró el cliente para editar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar el cliente en SQL Server.", ex);
            }
        }

        public void Eliminar(int codigoCliente)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                clientesSupabase.Inactivar(codigoCliente);
                return;
            }
            // Baja lógica: conserva el registro y sus relaciones históricas.
            const string consulta = "UPDATE dbo.Tbl_Clientes SET Estado = @Estado WHERE CodigoCliente = @CodigoCliente;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                    comando.Parameters.Add("@CodigoCliente", SqlDbType.Int).Value = codigoCliente;

                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró el cliente para eliminar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible eliminar el cliente en SQL Server.", ex);
            }
        }

        public List<ClientesEntidad> Buscar(string criterio)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return clientesSupabase.Buscar(criterio);
            List<ClientesEntidad> clientes = new List<ClientesEntidad>();
            const string consulta = @"SELECT CodigoCliente, Nombre, Nit, CorreoElectronico,
Telefono, Direccion, Departamento, Municipio, Estado
FROM dbo.Tbl_Clientes
WHERE CONVERT(varchar(20), CodigoCliente) LIKE @Criterio
   OR Nombre LIKE @Criterio
   OR Nit LIKE @Criterio
   OR CorreoElectronico LIKE @Criterio
   OR Telefono LIKE @Criterio
   OR Direccion LIKE @Criterio
   OR Departamento LIKE @Criterio
   OR Municipio LIKE @Criterio
ORDER BY CodigoCliente;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    // El criterio se envía como parámetro y se busca dentro de cada campo.
                    comando.Parameters.Add("@Criterio", System.Data.SqlDbType.VarChar, 2000)
                        .Value = "%" + (criterio ?? string.Empty).Trim() + "%";
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            clientes.Add(new ClientesEntidad(
                                lector.GetInt32(0),
                                lector.GetString(1),
                                lector.IsDBNull(2) ? null : lector.GetString(2),
                                lector.IsDBNull(3) ? null : lector.GetString(3),
                                lector.GetString(4),
                                lector.GetString(5),
                                lector.GetString(6),
                                lector.GetString(7),
                                lector.GetBoolean(8)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible buscar clientes en SQL Server.", ex);
            }

            return clientes;
        }
    }
}
