using System;

namespace SistemaTelefonia.Entidad
{
    public class DetalleVentaEntidad
    {
        private int codigoDetalleVenta;
        private int codigoVenta;
        private int codigoEquipo;
        private int? codigoPlan;
        private int cantidad;
        private decimal precioUnitario;
        private decimal descuento;
        private decimal subtotal;
        private bool estado;

        public int CodigoDetalleVenta
        {
            get { return codigoDetalleVenta; }
            set { codigoDetalleVenta = value; }
        }

        public int CodigoVenta
        {
            get { return codigoVenta; }
            set { codigoVenta = value; }
        }

        public int CodigoEquipo
        {
            get { return codigoEquipo; }
            set { codigoEquipo = value; }
        }

        public int? CodigoPlan
        {
            get { return codigoPlan; }
            set { codigoPlan = value; }
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

        public DetalleVentaEntidad()
        {
            codigoDetalleVenta = 0;
            codigoVenta = 0;
            codigoEquipo = 0;
            codigoPlan = null;
            cantidad = 0;
            precioUnitario = 0m;
            descuento = 0m;
            subtotal = 0m;
            estado = false;
        }

        public DetalleVentaEntidad(int codigoDetalleVenta, int codigoVenta, int codigoEquipo, int? codigoPlan, int cantidad, decimal precioUnitario, decimal descuento, decimal subtotal, bool estado)
        {
            this.codigoDetalleVenta = codigoDetalleVenta;
            this.codigoVenta = codigoVenta;
            this.codigoEquipo = codigoEquipo;
            this.codigoPlan = codigoPlan;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
            this.descuento = descuento;
            this.subtotal = subtotal;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public DetalleVentaEntidad(int codigoVenta, int codigoEquipo, int? codigoPlan, int cantidad, decimal precioUnitario, decimal descuento, decimal subtotal, bool estado)
        {
            this.codigoDetalleVenta = 0;
            this.codigoVenta = codigoVenta;
            this.codigoEquipo = codigoEquipo;
            this.codigoPlan = codigoPlan;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
            this.descuento = descuento;
            this.subtotal = subtotal;
            this.estado = estado;
        }
    }
}