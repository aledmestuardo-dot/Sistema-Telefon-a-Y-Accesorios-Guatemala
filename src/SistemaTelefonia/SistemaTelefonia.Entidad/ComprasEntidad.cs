using System;

namespace SistemaTelefonia.Entidad
{
    public class ComprasEntidad
    {
        private int codigoCompra;
        private int codigoProveedor;
        private string numeroDocumento;
        private DateTime fechaCompra;
        private decimal subtotal;
        private decimal iva;
        private decimal total;
        private string observaciones;
        private bool estado;

        public int CodigoCompra
        {
            get { return codigoCompra; }
            set { codigoCompra = value; }
        }

        public int CodigoProveedor
        {
            get { return codigoProveedor; }
            set { codigoProveedor = value; }
        }

        public string NumeroDocumento
        {
            get { return numeroDocumento; }
            set { numeroDocumento = value; }
        }

        public DateTime FechaCompra
        {
            get { return fechaCompra; }
            set { fechaCompra = value; }
        }

        public decimal Subtotal
        {
            get { return subtotal; }
            set { subtotal = value; }
        }

        public decimal Iva
        {
            get { return iva; }
            set { iva = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
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

        public ComprasEntidad()
        {
            codigoCompra = 0;
            codigoProveedor = 0;
            numeroDocumento = string.Empty;
            fechaCompra = DateTime.MinValue;
            subtotal = 0m;
            iva = 0m;
            total = 0m;
            observaciones = string.Empty;
            estado = false;
        }

        public ComprasEntidad(int codigoCompra, int codigoProveedor, string numeroDocumento, DateTime fechaCompra, decimal subtotal, decimal iva, decimal total, string observaciones, bool estado)
        {
            this.codigoCompra = codigoCompra;
            this.codigoProveedor = codigoProveedor;
            this.numeroDocumento = numeroDocumento;
            this.fechaCompra = fechaCompra;
            this.subtotal = subtotal;
            this.iva = iva;
            this.total = total;
            this.observaciones = observaciones;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public ComprasEntidad(int codigoProveedor, string numeroDocumento, DateTime fechaCompra, decimal subtotal, decimal iva, decimal total, string observaciones, bool estado)
        {
            this.codigoCompra = 0;
            this.codigoProveedor = codigoProveedor;
            this.numeroDocumento = numeroDocumento;
            this.fechaCompra = fechaCompra;
            this.subtotal = subtotal;
            this.iva = iva;
            this.total = total;
            this.observaciones = observaciones;
            this.estado = estado;
        }
    }
}