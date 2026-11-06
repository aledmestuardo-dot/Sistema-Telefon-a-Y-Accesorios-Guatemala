using System;

namespace SistemaTelefonia.Entidad
{
    public class DetalleFacturaEntidad
    {
        private int codigoDetalleFactura;
        private int codigoFactura;
        private string descripcion;
        private int cantidad;
        private decimal precioUnitario;
        private decimal descuento;
        private decimal iva;
        private decimal subtotal;
        private bool estado;

        public int CodigoDetalleFactura
        {
            get { return codigoDetalleFactura; }
            set { codigoDetalleFactura = value; }
        }

        public int CodigoFactura
        {
            get { return codigoFactura; }
            set { codigoFactura = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public decimal PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }

        public decimal Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }

        public decimal Iva
        {
            get { return iva; }
            set { iva = value; }
        }

        public decimal Subtotal
        {
            get { return subtotal; }
            set { subtotal = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public DetalleFacturaEntidad()
        {
            codigoDetalleFactura = 0;
            codigoFactura = 0;
            descripcion = string.Empty;
            cantidad = 0;
            precioUnitario = 0m;
            descuento = 0m;
            iva = 0m;
            subtotal = 0m;
            estado = false;
        }

        public DetalleFacturaEntidad(int codigoDetalleFactura, int codigoFactura, string descripcion, int cantidad, decimal precioUnitario, decimal descuento, decimal iva, decimal subtotal, bool estado)
        {
            this.codigoDetalleFactura = codigoDetalleFactura;
            this.codigoFactura = codigoFactura;
            this.descripcion = descripcion;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
            this.descuento = descuento;
            this.iva = iva;
            this.subtotal = subtotal;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public DetalleFacturaEntidad(int codigoFactura, string descripcion, int cantidad, decimal precioUnitario, decimal descuento, decimal iva, decimal subtotal, bool estado)
        {
            this.codigoDetalleFactura = 0;
            this.codigoFactura = codigoFactura;
            this.descripcion = descripcion;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
            this.descuento = descuento;
            this.iva = iva;
            this.subtotal = subtotal;
            this.estado = estado;
        }
    }
}