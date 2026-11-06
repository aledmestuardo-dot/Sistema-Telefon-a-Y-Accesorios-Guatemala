using System;

namespace SistemaTelefonia.Entidad
{
    public class SucursalesEntidad
    {
        private int codigoSucursal;
        private string nombre;
        private string direccion;
        private string municipio;
        private string departamento;
        private string telefono;
        private string horarioAtencion;
        private string correoElectronico;
        private DateTime fechaApertura;
        private bool estado;

        public int CodigoSucursal
        {
            get { return codigoSucursal; }
            set { codigoSucursal = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }

        public string Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string HorarioAtencion
        {
            get { return horarioAtencion; }
            set { horarioAtencion = value; }
        }

        public string CorreoElectronico
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }

        public DateTime FechaApertura
        {
            get { return fechaApertura; }
            set { fechaApertura = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public SucursalesEntidad()
        {
            codigoSucursal = 0;
            nombre = string.Empty;
            direccion = string.Empty;
            municipio = string.Empty;
            departamento = string.Empty;
            telefono = string.Empty;
            horarioAtencion = string.Empty;
            correoElectronico = string.Empty;
            fechaApertura = DateTime.MinValue;
            estado = false;
        }

        public SucursalesEntidad(int codigoSucursal, string nombre, string direccion, string municipio, string departamento, string telefono, string horarioAtencion, string correoElectronico, DateTime fechaApertura, bool estado)
        {
            this.codigoSucursal = codigoSucursal;
            this.nombre = nombre;
            this.direccion = direccion;
            this.municipio = municipio;
            this.departamento = departamento;
            this.telefono = telefono;
            this.horarioAtencion = horarioAtencion;
            this.correoElectronico = correoElectronico;
            this.fechaApertura = fechaApertura;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public SucursalesEntidad(string nombre, string direccion, string municipio, string departamento, string telefono, string horarioAtencion, string correoElectronico, DateTime fechaApertura, bool estado)
        {
            this.codigoSucursal = 0;
            this.nombre = nombre;
            this.direccion = direccion;
            this.municipio = municipio;
            this.departamento = departamento;
            this.telefono = telefono;
            this.horarioAtencion = horarioAtencion;
            this.correoElectronico = correoElectronico;
            this.fechaApertura = fechaApertura;
            this.estado = estado;
        }
    }
}