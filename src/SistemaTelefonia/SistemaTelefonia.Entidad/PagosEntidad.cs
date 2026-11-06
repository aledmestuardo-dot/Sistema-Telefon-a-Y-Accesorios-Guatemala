using System;

namespace SistemaTelefonia.Entidad
{
    public class PagosEntidad
    {
        private int codigoPago;
        private int codigoFactura;
        private string tipoPago;
        private decimal monto;
        private string moneda;
        private DateTime fechaPago;
        private string referencia;
        private bool estado;

        public int CodigoPago
        {
            get { return codigoPago; }
            set { codigoPago = value; }
        }

        public int CodigoFactura
        {
            get { return codigoFactura; }
            set { codigoFactura = value; }
        }

        public string TipoPago
        {
            get { return tipoPago; }
            set { tipoPago = value; }
        }

        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public string Moneda
        {
            get { return moneda; }
            set { moneda = value; }
        }

        public DateTime FechaPago
        {
            get { return fechaPago; }
            set { fechaPago = value; }
        }

        public string Referencia
        {
            get { return referencia; }
            set { referencia = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public PagosEntidad()
        {
            codigoPago = 0;
            codigoFactura = 0;
            tipoPago = string.Empty;
            monto = 0m;
            moneda = string.Empty;
            fechaPago = DateTime.MinValue;
            referencia = string.Empty;
            estado = false;
        }

        public PagosEntidad(int codigoPago, int codigoFactura, string tipoPago, decimal monto, string moneda, DateTime fechaPago, string referencia, bool estado)
        {
            this.codigoPago = codigoPago;
            this.codigoFactura = codigoFactura;
            this.tipoPago = tipoPago;
            this.monto = monto;
            this.moneda = moneda;
            this.fechaPago = fechaPago;
            this.referencia = referencia;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public PagosEntidad(int codigoFactura, string tipoPago, decimal monto, string moneda, DateTime fechaPago, string referencia, bool estado)
        {
            this.codigoPago = 0;
            this.codigoFactura = codigoFactura;
            this.tipoPago = tipoPago;
            this.monto = monto;
            this.moneda = moneda;
            this.fechaPago = fechaPago;
            this.referencia = referencia;
            this.estado = estado;
        }
    }
}