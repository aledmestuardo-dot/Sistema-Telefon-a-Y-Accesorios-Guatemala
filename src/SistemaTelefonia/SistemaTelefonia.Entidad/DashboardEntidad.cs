using System;

namespace SistemaTelefonia.Entidad
{
    /// <summary>Resumen y puntos de la serie temporal mostrada en el dashboard.</summary>
    public class DashboardEntidad
    {
        public decimal TotalVentas { get; set; }
        public decimal TotalCompras { get; set; }
        public int CantidadVentas { get; set; }
        public int CantidadCompras { get; set; }
        public int ProductosBajoMinimo { get; set; }
        public decimal TotalVentasPeriodoAnterior { get; set; }
        public decimal TotalComprasPeriodoAnterior { get; set; }
        public DateTime Fecha { get; set; }
        public decimal VentasPorFecha { get; set; }
        public decimal ComprasPorFecha { get; set; }
        public string Producto { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public int UnidadesVendidas { get; set; }
    }
}
