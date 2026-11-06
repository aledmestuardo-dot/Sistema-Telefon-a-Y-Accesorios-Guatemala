using System;

namespace SistemaTelefonia.Entidad
{
    public class FacturacionEntidad
    {
        private int codigoFactura;
        private int codigoVenta;
        private string numeroFactura;
        private DateTime fechaEmision;
        private string nombreCliente;
        private string nit;
        private decimal total;
        private bool estado;

        public int CodigoFactura
        {
            get { return codigoFactura; }
            set { codigoFactura = value; }
        }

        public int CodigoVenta
        {
            get { return codigoVenta; }
            set { codigoVenta = value; }
        }

        public string NumeroFactura
        {
            get { return numeroFactura; }
            set { numeroFactura = value; }
        }

        public DateTime FechaEmision
        {
            get { return fechaEmision; }
            set { fechaEmision = value; }
        }

        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }

        public string Nit
        {
            get { return nit; }
            set { nit = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public FacturacionEntidad()
        {
            codigoFactura = 0;
            codigoVenta = 0;
            numeroFactura = string.Empty;
            fechaEmision = DateTime.MinValue;
            nombreCliente = string.Empty;
            nit = string.Empty;
            total = 0m;
            estado = false;
        }

        public FacturacionEntidad(int codigoFactura, int codigoVenta, string numeroFactura, DateTime fechaEmision, string nombreCliente, string nit, decimal total, bool estado)
        {
            this.codigoFactura = codigoFactura;
            this.codigoVenta = codigoVenta;
            this.numeroFactura = numeroFactura;
            this.fechaEmision = fechaEmision;
            this.nombreCliente = nombreCliente;
            this.nit = nit;
            this.total = total;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public FacturacionEntidad(int codigoVenta, string numeroFactura, DateTime fechaEmision, string nombreCliente, string nit, decimal total, bool estado)
        {
            this.codigoFactura = 0;
            this.codigoVenta = codigoVenta;
            this.numeroFactura = numeroFactura;
            this.fechaEmision = fechaEmision;
            this.nombreCliente = nombreCliente;
            this.nit = nit;
            this.total = total;
            this.estado = estado;
        }
    }
}