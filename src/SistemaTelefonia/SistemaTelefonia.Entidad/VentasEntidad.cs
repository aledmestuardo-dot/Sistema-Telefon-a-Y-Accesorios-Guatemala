using System;

namespace SistemaTelefonia.Entidad
{
    public class VentasEntidad
    {
        private int codigoVenta;
        private int codigoSucursal;
        private int codigoCliente;
        private int codigoEmpleado;
        private DateTime fechaVenta;
        private decimal subtotal;
        private decimal iva;
        private decimal total;
        private string observaciones;
        private bool estado;

        public int CodigoVenta
        {
            get { return codigoVenta; }
            set { codigoVenta = value; }
        }

        public int CodigoSucursal
        {
            get { return codigoSucursal; }
            set { codigoSucursal = value; }
        }

        public int CodigoCliente
        {
            get { return codigoCliente; }
            set { codigoCliente = value; }
        }

        public int CodigoEmpleado
        {
            get { return codigoEmpleado; }
            set { codigoEmpleado = value; }
        }

        public DateTime FechaVenta
        {
            get { return fechaVenta; }
            set { fechaVenta = value; }
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

        public VentasEntidad()
        {
            codigoVenta = 0;
            codigoSucursal = 0;
            codigoCliente = 0;
            codigoEmpleado = 0;
            fechaVenta = DateTime.MinValue;
            subtotal = 0m;
            iva = 0m;
            total = 0m;
            observaciones = string.Empty;
            estado = false;
        }

        public VentasEntidad(int codigoVenta, int codigoSucursal, int codigoCliente, int codigoEmpleado, DateTime fechaVenta, decimal subtotal, decimal iva, decimal total, string observaciones, bool estado)
        {
            this.codigoVenta = codigoVenta;
            this.codigoSucursal = codigoSucursal;
            this.codigoCliente = codigoCliente;
            this.codigoEmpleado = codigoEmpleado;
            this.fechaVenta = fechaVenta;
            this.subtotal = subtotal;
            this.iva = iva;
            this.total = total;
            this.observaciones = observaciones;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public VentasEntidad(int codigoSucursal, int codigoCliente, int codigoEmpleado, DateTime fechaVenta, decimal subtotal, decimal iva, decimal total, string observaciones, bool estado)
        {
            this.codigoVenta = 0;
            this.codigoSucursal = codigoSucursal;
            this.codigoCliente = codigoCliente;
            this.codigoEmpleado = codigoEmpleado;
            this.fechaVenta = fechaVenta;
            this.subtotal = subtotal;
            this.iva = iva;
            this.total = total;
            this.observaciones = observaciones;
            this.estado = estado;
        }
    }
}