using System;

namespace SistemaTelefonia.Entidad
{
    public class LineasEntidad
    {
        private int codigoLinea;
        private int codigoCliente;
        private int codigoPlan;
        private string numeroTelefonico;
        private string tipoLinea;
        private DateTime? fechaActivacion;
        private DateTime? fechaVencimiento;
        private bool estado;

        public int CodigoLinea
        {
            get { return codigoLinea; }
            set { codigoLinea = value; }
        }

        public int CodigoCliente
        {
            get { return codigoCliente; }
            set { codigoCliente = value; }
        }

        public int CodigoPlan
        {
            get { return codigoPlan; }
            set { codigoPlan = value; }
        }

        public string NumeroTelefonico
        {
            get { return numeroTelefonico; }
            set { numeroTelefonico = value; }
        }

        public string TipoLinea
        {
            get { return tipoLinea; }
            set { tipoLinea = value; }
        }

        public DateTime? FechaActivacion
        {
            get { return fechaActivacion; }
            set { fechaActivacion = value; }
        }

        public DateTime? FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public LineasEntidad()
        {
            codigoLinea = 0;
            codigoCliente = 0;
            codigoPlan = 0;
            numeroTelefonico = string.Empty;
            tipoLinea = string.Empty;
            fechaActivacion = null;
            fechaVencimiento = null;
            estado = false;
        }

        public LineasEntidad(int codigoLinea, int codigoCliente, int codigoPlan, string numeroTelefonico, string tipoLinea, DateTime? fechaActivacion, DateTime? fechaVencimiento, bool estado)
        {
            this.codigoLinea = codigoLinea;
            this.codigoCliente = codigoCliente;
            this.codigoPlan = codigoPlan;
            this.numeroTelefonico = numeroTelefonico;
            this.tipoLinea = tipoLinea;
            this.fechaActivacion = fechaActivacion;
            this.fechaVencimiento = fechaVencimiento;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public LineasEntidad(int codigoCliente, int codigoPlan, string numeroTelefonico, string tipoLinea, DateTime? fechaActivacion, DateTime? fechaVencimiento, bool estado)
        {
            this.codigoLinea = 0;
            this.codigoCliente = codigoCliente;
            this.codigoPlan = codigoPlan;
            this.numeroTelefonico = numeroTelefonico;
            this.tipoLinea = tipoLinea;
            this.fechaActivacion = fechaActivacion;
            this.fechaVencimiento = fechaVencimiento;
            this.estado = estado;
        }
    }
}