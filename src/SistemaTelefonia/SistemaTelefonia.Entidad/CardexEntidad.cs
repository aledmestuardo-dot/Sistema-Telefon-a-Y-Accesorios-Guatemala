using System;

namespace SistemaTelefonia.Entidad
{
    public class CardexEntidad
    {
        private int codigoCardex;
        private int codigoInventario;
        private int? codigoCompra;
        private int? codigoVenta;
        private DateTime fechaActualizacion;
        private string tipoMovimiento;
        private int cantidad;
        private int stockResultante;
        private string observaciones;
        private bool estado;

        public int CodigoCardex
        {
            get { return codigoCardex; }
            set { codigoCardex = value; }
        }

        public int CodigoInventario
        {
            get { return codigoInventario; }
            set { codigoInventario = value; }
        }

        public int? CodigoCompra
        {
            get { return codigoCompra; }
            set { codigoCompra = value; }
        }

        public int? CodigoVenta
        {
            get { return codigoVenta; }
            set { codigoVenta = value; }
        }

        public DateTime FechaActualizacion
        {
            get { return fechaActualizacion; }
            set { fechaActualizacion = value; }
        }

        public string TipoMovimiento
        {
            get { return tipoMovimiento; }
            set { tipoMovimiento = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public int StockResultante
        {
            get { return stockResultante; }
            set { stockResultante = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public CardexEntidad()
        {
            codigoCardex = 0;
            codigoInventario = 0;
            codigoCompra = null;
            codigoVenta = null;
            fechaActualizacion = DateTime.MinValue;
            tipoMovimiento = string.Empty;
            cantidad = 0;
            stockResultante = 0;
            observaciones = string.Empty;
            estado = false;
        }

        public CardexEntidad(int codigoCardex, int codigoInventario, int? codigoCompra, int? codigoVenta, DateTime fechaActualizacion, string tipoMovimiento, int cantidad, int stockResultante, string observaciones, bool estado)
        {
            this.codigoCardex = codigoCardex;
            this.codigoInventario = codigoInventario;
            this.codigoCompra = codigoCompra;
            this.codigoVenta = codigoVenta;
            this.fechaActualizacion = fechaActualizacion;
            this.tipoMovimiento = tipoMovimiento;
            this.cantidad = cantidad;
            this.stockResultante = stockResultante;
            this.observaciones = observaciones;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public CardexEntidad(int codigoInventario, int? codigoCompra, int? codigoVenta, DateTime fechaActualizacion, string tipoMovimiento, int cantidad, int stockResultante, string observaciones, bool estado)
        {
            this.codigoCardex = 0;
            this.codigoInventario = codigoInventario;
            this.codigoCompra = codigoCompra;
            this.codigoVenta = codigoVenta;
            this.fechaActualizacion = fechaActualizacion;
            this.tipoMovimiento = tipoMovimiento;
            this.cantidad = cantidad;
            this.stockResultante = stockResultante;
            this.observaciones = observaciones;
            this.estado = estado;
        }
    }
}