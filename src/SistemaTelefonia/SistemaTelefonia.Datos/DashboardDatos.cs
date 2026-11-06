using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Datos
{
    /// <summary>Consultas agregadas del panel general.</summary>
    public class DashboardDatos
    {
        public DashboardEntidad ObtenerResumen(DateTime desde, DateTime hasta, DateTime desdeAnterior, DateTime hastaAnterior)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return ObtenerResumenSupabase(desde, hasta, desdeAnterior, hastaAnterior);

            DashboardEntidad resumen = new DashboardEntidad();
            const string consulta = @"
SELECT
 (SELECT ISNULL(SUM(Total),0) FROM dbo.Tbl_Ventas WHERE Estado=1 AND FechaVenta>=@Desde AND FechaVenta<@HastaExclusivo),
 (SELECT ISNULL(SUM(Total),0) FROM dbo.Tbl_Compras WHERE Estado=1 AND FechaCompra>=@Desde AND FechaCompra<@HastaExclusivo),
 (SELECT COUNT(1) FROM dbo.Tbl_Ventas WHERE Estado=1 AND FechaVenta>=@Desde AND FechaVenta<@HastaExclusivo),
 (SELECT COUNT(1) FROM dbo.Tbl_Compras WHERE Estado=1 AND FechaCompra>=@Desde AND FechaCompra<@HastaExclusivo),
 (SELECT COUNT(1) FROM dbo.Tbl_Inventario WHERE Estado=1 AND Stock<=StockMinimo),
 (SELECT ISNULL(SUM(Total),0) FROM dbo.Tbl_Ventas WHERE Estado=1 AND FechaVenta>=@DesdeAnterior AND FechaVenta<@HastaAnteriorExclusivo),
 (SELECT ISNULL(SUM(Total),0) FROM dbo.Tbl_Compras WHERE Estado=1 AND FechaCompra>=@DesdeAnterior AND FechaCompra<@HastaAnteriorExclusivo);";
            using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                AgregarFechas(comando, desde, hasta, desdeAnterior, hastaAnterior);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                    if (lector.Read())
                    {
                        resumen.TotalVentas = lector.GetDecimal(0); resumen.TotalCompras = lector.GetDecimal(1);
                        resumen.CantidadVentas = lector.GetInt32(2); resumen.CantidadCompras = lector.GetInt32(3);
                        resumen.ProductosBajoMinimo = lector.GetInt32(4);
                        resumen.TotalVentasPeriodoAnterior = lector.GetDecimal(5); resumen.TotalComprasPeriodoAnterior = lector.GetDecimal(6);
                    }
            }
            return resumen;
        }

        public List<DashboardEntidad> ObtenerSerie(DateTime desde, DateTime hasta, string agrupacion)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return ObtenerSerieSupabase(desde, hasta, agrupacion);

            List<DashboardEntidad> serie = new List<DashboardEntidad>();
            string fechaVenta = ExpresionFecha(agrupacion, "FechaVenta");
            string fechaCompra = ExpresionFecha(agrupacion, "FechaCompra");
            string consulta = @"SELECT Periodo, SUM(Ventas), SUM(Compras) FROM (
 SELECT " + fechaVenta + @" Periodo, SUM(Total) Ventas, CAST(0 AS decimal(18,2)) Compras
 FROM dbo.Tbl_Ventas WHERE Estado=1 AND FechaVenta>=@Desde AND FechaVenta<@HastaExclusivo GROUP BY " + fechaVenta + @"
 UNION ALL
 SELECT " + fechaCompra + @" Periodo, CAST(0 AS decimal(18,2)), SUM(Total)
 FROM dbo.Tbl_Compras WHERE Estado=1 AND FechaCompra>=@Desde AND FechaCompra<@HastaExclusivo GROUP BY " + fechaCompra + @"
) movimientos GROUP BY Periodo ORDER BY Periodo;";
            using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde.Date;
                comando.Parameters.Add("@HastaExclusivo", SqlDbType.DateTime).Value = hasta.Date.AddDays(1);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                    while (lector.Read()) serie.Add(new DashboardEntidad { Fecha = lector.GetDateTime(0), VentasPorFecha = lector.GetDecimal(1), ComprasPorFecha = lector.GetDecimal(2) });
            }
            return serie;
        }

        public List<DashboardEntidad> ObtenerProductosBajoMinimo()
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return ObtenerProductosBajoMinimoSupabase();

            List<DashboardEntidad> productos = new List<DashboardEntidad>();
            const string consulta = @"SELECT e.Modelo, i.Stock, i.StockMinimo
FROM dbo.Tbl_Inventario i INNER JOIN dbo.Tbl_Equipos e ON e.CodigoEquipo=i.CodigoEquipo
WHERE i.Estado=1 AND e.Estado=1 AND i.Stock<=i.StockMinimo ORDER BY i.Stock ASC, e.Modelo;";
            using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                    while (lector.Read()) productos.Add(new DashboardEntidad { Producto = lector.GetString(0), Stock = lector.GetInt32(1), StockMinimo = lector.GetInt32(2) });
            }
            return productos;
        }

        public List<DashboardEntidad> ObtenerMasVendidos(DateTime desde, DateTime hasta)
        {
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return ObtenerMasVendidosSupabase(desde, hasta);

            List<DashboardEntidad> productos = new List<DashboardEntidad>();
            const string consulta = @"SELECT TOP 5 e.Modelo, SUM(d.Cantidad)
FROM dbo.Tbl_DetalleVenta d INNER JOIN dbo.Tbl_Ventas v ON v.CodigoVenta=d.CodigoVenta
INNER JOIN dbo.Tbl_Equipos e ON e.CodigoEquipo=d.CodigoEquipo
WHERE d.Estado=1 AND v.Estado=1 AND e.Estado=1 AND v.FechaVenta>=@Desde AND v.FechaVenta<@HastaExclusivo
GROUP BY e.Modelo ORDER BY SUM(d.Cantidad) DESC, e.Modelo;";
            using (SqlConnection conexion = ConexionSQLServer.CrearConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde.Date;
                comando.Parameters.Add("@HastaExclusivo", SqlDbType.DateTime).Value = hasta.Date.AddDays(1);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                    while (lector.Read()) productos.Add(new DashboardEntidad { Producto = lector.GetString(0), UnidadesVendidas = lector.GetInt32(1) });
            }
            return productos;
        }

        /// <summary>Calcula los mismos indicadores usando las tablas accesibles por PostgREST/RLS.</summary>
        private static DashboardEntidad ObtenerResumenSupabase(DateTime desde, DateTime hasta,
            DateTime desdeAnterior, DateTime hastaAnterior)
        {
            List<VentasEntidad> ventas = Listar<VentasEntidad>();
            List<ComprasEntidad> compras = Listar<ComprasEntidad>();
            Dictionary<int, bool> equiposActivos = Listar<EquiposEntidad>()
                .GroupBy(e => e.CodigoEquipo).ToDictionary(g => g.Key, g => g.First().Estado);
            List<InventarioEntidad> inventario = Listar<InventarioEntidad>()
                .Where(i => i.Estado && equiposActivos.ContainsKey(i.CodigoEquipo) && equiposActivos[i.CodigoEquipo])
                .ToList();

            List<VentasEntidad> ventasPeriodo = ventas.FindAll(v => v.Estado && EnRango(v.FechaVenta, desde, hasta));
            List<ComprasEntidad> comprasPeriodo = compras.FindAll(c => c.Estado && EnRango(c.FechaCompra, desde, hasta));
            List<VentasEntidad> ventasAnteriores = ventas.FindAll(v => v.Estado && EnRango(v.FechaVenta, desdeAnterior, hastaAnterior));
            List<ComprasEntidad> comprasAnteriores = compras.FindAll(c => c.Estado && EnRango(c.FechaCompra, desdeAnterior, hastaAnterior));

            return new DashboardEntidad
            {
                TotalVentas = ventasPeriodo.Sum(v => v.Total),
                TotalCompras = comprasPeriodo.Sum(c => c.Total),
                CantidadVentas = ventasPeriodo.Count,
                CantidadCompras = comprasPeriodo.Count,
                ProductosBajoMinimo = inventario.Count(i => i.Stock <= i.StockMinimo),
                TotalVentasPeriodoAnterior = ventasAnteriores.Sum(v => v.Total),
                TotalComprasPeriodoAnterior = comprasAnteriores.Sum(c => c.Total)
            };
        }

        private static List<DashboardEntidad> ObtenerSerieSupabase(DateTime desde, DateTime hasta, string agrupacion)
        {
            List<DashboardEntidad> resultado = new List<DashboardEntidad>();
            List<VentasEntidad> ventas = Listar<VentasEntidad>()
                .FindAll(v => v.Estado && EnRango(v.FechaVenta, desde, hasta));
            List<ComprasEntidad> compras = Listar<ComprasEntidad>()
                .FindAll(c => c.Estado && EnRango(c.FechaCompra, desde, hasta));
            HashSet<DateTime> periodos = new HashSet<DateTime>();
            foreach (VentasEntidad venta in ventas) periodos.Add(AgruparFecha(venta.FechaVenta, agrupacion));
            foreach (ComprasEntidad compra in compras) periodos.Add(AgruparFecha(compra.FechaCompra, agrupacion));

            foreach (DateTime periodo in periodos.OrderBy(fecha => fecha))
            {
                resultado.Add(new DashboardEntidad
                {
                    Fecha = periodo,
                    VentasPorFecha = ventas.Where(v => AgruparFecha(v.FechaVenta, agrupacion) == periodo).Sum(v => v.Total),
                    ComprasPorFecha = compras.Where(c => AgruparFecha(c.FechaCompra, agrupacion) == periodo).Sum(c => c.Total)
                });
            }
            return resultado;
        }

        private static List<DashboardEntidad> ObtenerProductosBajoMinimoSupabase()
        {
            Dictionary<int, string> equipos = Listar<EquiposEntidad>().Where(e => e.Estado)
                .GroupBy(e => e.CodigoEquipo).ToDictionary(g => g.Key, g => g.First().Modelo);
            return Listar<InventarioEntidad>()
                .Where(i => i.Estado && i.Stock <= i.StockMinimo && equipos.ContainsKey(i.CodigoEquipo))
                .OrderBy(i => i.Stock)
                .ThenBy(i => equipos[i.CodigoEquipo])
                .Select(i => new DashboardEntidad
                {
                    Producto = equipos[i.CodigoEquipo],
                    Stock = i.Stock,
                    StockMinimo = i.StockMinimo
                }).ToList();
        }

        private static List<DashboardEntidad> ObtenerMasVendidosSupabase(DateTime desde, DateTime hasta)
        {
            HashSet<int> ventasActivas = new HashSet<int>(Listar<VentasEntidad>()
                .Where(v => v.Estado && EnRango(v.FechaVenta, desde, hasta)).Select(v => v.CodigoVenta));
            Dictionary<int, string> equipos = Listar<EquiposEntidad>().Where(e => e.Estado)
                .GroupBy(e => e.CodigoEquipo).ToDictionary(g => g.Key, g => g.First().Modelo);

            return Listar<DetalleVentaEntidad>()
                .Where(d => d.Estado && ventasActivas.Contains(d.CodigoVenta) && equipos.ContainsKey(d.CodigoEquipo))
                .GroupBy(d => d.CodigoEquipo)
                .Select(g => new DashboardEntidad
                {
                    Producto = equipos[g.Key],
                    UnidadesVendidas = g.Sum(d => d.Cantidad)
                })
                .OrderByDescending(p => p.UnidadesVendidas)
                .ThenBy(p => p.Producto)
                .Take(5)
                .ToList();
        }

        private static List<T> Listar<T>() where T : new()
        {
            return new CrudRelacionalDatos().Listar<T>();
        }

        private static bool EnRango(DateTime fecha, DateTime desde, DateTime hasta)
        {
            return fecha >= desde.Date && fecha < hasta.Date.AddDays(1);
        }

        private static DateTime AgruparFecha(DateTime fecha, string agrupacion)
        {
            if (agrupacion == "Mes") return new DateTime(fecha.Year, fecha.Month, 1);
            if (agrupacion == "Semana")
            {
                int diferencia = ((int)fecha.DayOfWeek + 6) % 7;
                return fecha.Date.AddDays(-diferencia);
            }
            return fecha.Date;
        }

        private static string ExpresionFecha(string agrupacion, string columna)
        {
            if (agrupacion == "Mes") return "DATEFROMPARTS(YEAR(" + columna + "), MONTH(" + columna + "), 1)";
            if (agrupacion == "Semana") return "DATEADD(day, -((DATEPART(weekday, " + columna + ")+@@DATEFIRST-2)%7), CAST(" + columna + " AS date))";
            return "CAST(" + columna + " AS date)";
        }

        private static void AgregarFechas(SqlCommand comando, DateTime desde, DateTime hasta, DateTime desdeAnterior, DateTime hastaAnterior)
        {
            comando.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde.Date;
            comando.Parameters.Add("@HastaExclusivo", SqlDbType.DateTime).Value = hasta.Date.AddDays(1);
            comando.Parameters.Add("@DesdeAnterior", SqlDbType.DateTime).Value = desdeAnterior.Date;
            comando.Parameters.Add("@HastaAnteriorExclusivo", SqlDbType.DateTime).Value = hastaAnterior.Date.AddDays(1);
        }
    }
}
