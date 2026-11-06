using System;

namespace SistemaTelefonia.Entidad
{
    public class ReclamosEntidad
    {
        private int codigoReclamo;
        private int codigoCliente;
        private int codigoLinea;
        private int codigoDetalleVenta;
        private DateTime fecha;
        private string descripcion;
        private string tipoReclamo;
        private bool estado;

        public int CodigoReclamo
        {
            get { return codigoReclamo; }
            set { codigoReclamo = value; }
        }

        public int CodigoCliente
        {
            get { return codigoCliente; }
            set { codigoCliente = value; }
        }

        public int CodigoLinea
        {
            get { return codigoLinea; }
            set { codigoLinea = value; }
        }

        public int CodigoDetalleVenta
        {
            get { return codigoDetalleVenta; }
            set { codigoDetalleVenta = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string TipoReclamo
        {
            get { return tipoReclamo; }
            set { tipoReclamo = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public ReclamosEntidad()
        {
            codigoReclamo = 0;
            codigoCliente = 0;
            codigoLinea = 0;
            codigoDetalleVenta = 0;
            fecha = DateTime.MinValue;
            descripcion = string.Empty;
            tipoReclamo = string.Empty;
            estado = false;
        }

        public ReclamosEntidad(int codigoReclamo, int codigoCliente, int codigoLinea, int codigoDetalleVenta, DateTime fecha, string descripcion, string tipoReclamo, bool estado)
        {
            this.codigoReclamo = codigoReclamo;
            this.codigoCliente = codigoCliente;
            this.codigoLinea = codigoLinea;
            this.codigoDetalleVenta = codigoDetalleVenta;
            this.fecha = fecha;
            this.descripcion = descripcion;
            this.tipoReclamo = tipoReclamo;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public ReclamosEntidad(int codigoCliente, int codigoLinea, int codigoDetalleVenta, DateTime fecha, string descripcion, string tipoReclamo, bool estado)
        {
            this.codigoReclamo = 0;
            this.codigoCliente = codigoCliente;
            this.codigoLinea = codigoLinea;
            this.codigoDetalleVenta = codigoDetalleVenta;
            this.fecha = fecha;
            this.descripcion = descripcion;
            this.tipoReclamo = tipoReclamo;
            this.estado = estado;
        }
    }
}