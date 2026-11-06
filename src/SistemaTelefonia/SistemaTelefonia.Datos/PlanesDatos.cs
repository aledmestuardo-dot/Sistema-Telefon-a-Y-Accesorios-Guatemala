using System;
using System.Data;
using System.Data.SqlClient;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Guarda planes en SQL Server usando ADO.NET.</summary>
    public class PlanesDatos
    {
        private readonly PlanesSupabaseDatos planesSupabase = new PlanesSupabaseDatos();

        public System.Collections.Generic.List<PlanesEntidad> Listar()
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return planesSupabase.Listar();
            System.Collections.Generic.List<PlanesEntidad> planes =
                new System.Collections.Generic.List<PlanesEntidad>();
            const string consulta = @"SELECT CodigoPlan, Nombre, Descripcion, CostoMensual,
CantidadDatos, MinutosIncluidos, VigenciaMeses, Estado
FROM dbo.Tbl_Planes
ORDER BY CodigoPlan;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            planes.Add(new PlanesEntidad(
                                lector.GetInt32(0),
                                lector.GetString(1),
                                lector.GetString(2),
                                lector.GetDecimal(3),
                                lector.GetInt32(4),
                                lector.GetInt32(5),
                                lector.GetInt32(6),
                                lector.GetBoolean(7)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible consultar los planes en SQL Server.", ex);
            }

            return planes;
        }

        public System.Collections.Generic.List<PlanesEntidad> Buscar(string criterio)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return planesSupabase.Buscar(criterio);
            System.Collections.Generic.List<PlanesEntidad> planes =
                new System.Collections.Generic.List<PlanesEntidad>();
            const string consulta = @"SELECT CodigoPlan, Nombre, Descripcion, CostoMensual,
CantidadDatos, MinutosIncluidos, VigenciaMeses, Estado
FROM dbo.Tbl_Planes
WHERE CONVERT(varchar(20), CodigoPlan) LIKE @Criterio
   OR Nombre LIKE @Criterio
   OR Descripcion LIKE @Criterio
ORDER BY CodigoPlan;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    // El valor se envia como parametro y se busca dentro de los campos admitidos.
                    comando.Parameters.Add("@Criterio", SqlDbType.VarChar, 2000)
                        .Value = "%" + (criterio ?? string.Empty).Trim() + "%";
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            planes.Add(new PlanesEntidad(
                                lector.GetInt32(0),
                                lector.GetString(1),
                                lector.GetString(2),
                                lector.GetDecimal(3),
                                lector.GetInt32(4),
                                lector.GetInt32(5),
                                lector.GetInt32(6),
                                lector.GetBoolean(7)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible buscar planes en SQL Server.", ex);
            }

            return planes;
        }

        public void Editar(PlanesEntidad plan)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                planesSupabase.Editar(plan);
                return;
            }
            const string consulta = @"UPDATE dbo.Tbl_Planes SET
Nombre = @Nombre,
Descripcion = @Descripcion,
CostoMensual = @CostoMensual,
CantidadDatos = @CantidadDatos,
MinutosIncluidos = @MinutosIncluidos,
VigenciaMeses = @VigenciaMeses,
Estado = @Estado
WHERE CodigoPlan = @CodigoPlan;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = plan.Nombre;
                    comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = plan.Descripcion;
                    SqlParameter costo = comando.Parameters.Add("@CostoMensual", SqlDbType.Decimal);
                    costo.Precision = 10;
                    costo.Scale = 2;
                    costo.Value = plan.CostoMensual;
                    comando.Parameters.Add("@CantidadDatos", SqlDbType.Int).Value = plan.CantidadDatos;
                    comando.Parameters.Add("@MinutosIncluidos", SqlDbType.Int).Value = plan.MinutosIncluidos;
                    comando.Parameters.Add("@VigenciaMeses", SqlDbType.Int).Value = plan.VigenciaMeses;
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = plan.Estado;
                    comando.Parameters.Add("@CodigoPlan", SqlDbType.Int).Value = plan.CodigoPlan;

                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontro el plan para editar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible editar el plan en SQL Server.", ex);
            }
        }

        public void Eliminar(int codigoPlan)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                planesSupabase.Inactivar(codigoPlan);
                return;
            }
            // Baja lógica: conserva el plan y sus referencias históricas.
            const string consulta = @"UPDATE dbo.Tbl_Planes
SET Estado = @Estado
WHERE CodigoPlan = @CodigoPlan;";

            try
            {
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = false;
                    comando.Parameters.Add("@CodigoPlan", SqlDbType.Int).Value = codigoPlan;

                    conexion.Open();
                    if (comando.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No se encontró el plan para eliminar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible marcar el plan como inactivo en SQL Server.", ex);
            }
        }

        public int Agregar(PlanesEntidad plan)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return planesSupabase.Agregar(plan);
            const string consulta = @"INSERT INTO dbo.Tbl_Planes
(Nombre, Descripcion, CostoMensual, CantidadDatos, MinutosIncluidos, VigenciaMeses, Estado)
OUTPUT INSERTED.CodigoPlan
VALUES (@Nombre, @Descripcion, @CostoMensual, @CantidadDatos, @MinutosIncluidos, @VigenciaMeses, @Estado);";

            try
            {
                // CodigoPlan es IDENTITY; SQL Server genera y devuelve la PK.
                using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = plan.Nombre;
                    comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = plan.Descripcion;
                    SqlParameter costo = comando.Parameters.Add("@CostoMensual", SqlDbType.Decimal);
                    costo.Precision = 10;
                    costo.Scale = 2;
                    costo.Value = plan.CostoMensual;
                    comando.Parameters.Add("@CantidadDatos", SqlDbType.Int).Value = plan.CantidadDatos;
                    comando.Parameters.Add("@MinutosIncluidos", SqlDbType.Int).Value = plan.MinutosIncluidos;
                    comando.Parameters.Add("@VigenciaMeses", SqlDbType.Int).Value = plan.VigenciaMeses;
                    comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = plan.Estado;

                    conexion.Open();
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No fue posible agregar el plan en SQL Server.", ex);
            }
        }
    }
}
