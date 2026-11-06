using System;

namespace SistemaTelefonia.Entidad
{
    public class DetalleCompraEntidad
    {
        private int codigoDetalleCompra;
        private int codigoCompra;
        private int codigoEquipo;
        private int cantidad;
        private decimal costoUnitario;
        private decimal subtotal;
        private string observaciones;
        private bool estado;

        public int CodigoDetalleCompra
        {
            get { return codigoDetalleCompra; }
            set { codigoDetalleCompra = value; }
        }

        public int CodigoCompra
        {
            get { return codigoCompra; }
            set { codigoCompra = value; }
        }

        public int CodigoEquipo
        {
            get { return codigoEquipo; }
            set { codigoEquipo = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public decimal CostoUnitario
        {
            get { return costoUnitario; }
            set { costoUnitario = value; }
        }

        public decimal Subtotal
        {
            get { return subtotal; }
            set { subtotal = value; }
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

        public DetalleCompraEntidad()
        {
            codigoDetalleCompra = 0;
            codigoCompra = 0;
            codigoEquipo = 0;
            cantidad = 0;
            costoUnitario = 0m;
            subtotal = 0m;
            observaciones = string.Empty;
            estado = false;
        }

        public DetalleCompraEntidad(int codigoDetalleCompra, int codigoCompra, int codigoEquipo, int cantidad, decimal costoUnitario, decimal subtotal, string observaciones, bool estado)
        {
            this.codigoDetalleCompra = codigoDetalleCompra;
            this.codigoCompra = codigoCompra;
            this.codigoEquipo = codigoEquipo;
            this.cantidad = cantidad;
            this.costoUnitario = costoUnitario;
            this.subtotal = subtotal;
            this.observaciones = observaciones;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public DetalleCompraEntidad(int codigoCompra, int codigoEquipo, int cantidad, decimal costoUnitario, decimal subtotal, string observaciones, bool estado)
        {
            this.codigoDetalleCompra = 0;
            this.codigoCompra = codigoCompra;
            this.codigoEquipo = codigoEquipo;
            this.cantidad = cantidad;
            this.costoUnitario = costoUnitario;
            this.subtotal = subtotal;
            this.observaciones = observaciones;
            this.estado = estado;
        }
    }
}