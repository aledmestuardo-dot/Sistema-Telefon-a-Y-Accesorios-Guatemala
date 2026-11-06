using System;

namespace SistemaTelefonia.Entidad
{
    public class InventarioEntidad
    {
        private int codigoInventario;
        private int codigoEquipo;
        private int codigoSucursal;
        private int stock;
        private int stockMinimo;
        private decimal precioUnitario;
        private string observaciones;
        private DateTime fechaActualizacion;
        private bool estado;

        public int CodigoInventario
        {
            get { return codigoInventario; }
            set { codigoInventario = value; }
        }

        public int CodigoEquipo
        {
            get { return codigoEquipo; }
            set { codigoEquipo = value; }
        }

        public int CodigoSucursal
        {
            get { return codigoSucursal; }
            set { codigoSucursal = value; }
        }

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public int StockMinimo
        {
            get { return stockMinimo; }
            set { stockMinimo = value; }
        }

        public decimal PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public DateTime FechaActualizacion
        {
            get { return fechaActualizacion; }
            set { fechaActualizacion = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public InventarioEntidad()
        {
            codigoInventario = 0;
            codigoEquipo = 0;
            codigoSucursal = 0;
            stock = 0;
            stockMinimo = 0;
            precioUnitario = 0m;
            observaciones = string.Empty;
            fechaActualizacion = DateTime.MinValue;
            estado = false;
        }

        public InventarioEntidad(int codigoInventario, int codigoEquipo, int codigoSucursal, int stock, int stockMinimo, decimal precioUnitario, string observaciones, DateTime fechaActualizacion, bool estado)
        {
            this.codigoInventario = codigoInventario;
            this.codigoEquipo = codigoEquipo;
            this.codigoSucursal = codigoSucursal;
            this.stock = stock;
            this.stockMinimo = stockMinimo;
            this.precioUnitario = precioUnitario;
            this.observaciones = observaciones;
            this.fechaActualizacion = fechaActualizacion;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public InventarioEntidad(int codigoEquipo, int codigoSucursal, int stock, int stockMinimo, decimal precioUnitario, string observaciones, DateTime fechaActualizacion, bool estado)
        {
            this.codigoInventario = 0;
            this.codigoEquipo = codigoEquipo;
            this.codigoSucursal = codigoSucursal;
            this.stock = stock;
            this.stockMinimo = stockMinimo;
            this.precioUnitario = precioUnitario;
            this.observaciones = observaciones;
            this.fechaActualizacion = fechaActualizacion;
            this.estado = estado;
        }
    }
}