using System;

namespace SistemaTelefonia.Entidad
{
    public class EmpleadosEntidad
    {
        private int codigoEmpleado;
        private int codigoSucursal;
        private int codigoUsuario;
        private string nombreEmpleado;
        private DateTime fechaNacimiento;
        private decimal salario;
        private DateTime fechaContratacion;
        private string telefono;
        private string observaciones;
        private bool estado;
        private string dpi;
        private string direccion;
        private string tipoEmpleado;

        public int CodigoEmpleado
        {
            get { return codigoEmpleado; }
            set { codigoEmpleado = value; }
        }

        public int CodigoSucursal
        {
            get { return codigoSucursal; }
            set { codigoSucursal = value; }
        }

        public int CodigoUsuario
        {
            get { return codigoUsuario; }
            set { codigoUsuario = value; }
        }

        public string NombreEmpleado
        {
            get { return nombreEmpleado; }
            set { nombreEmpleado = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }

        public decimal Salario
        {
            get { return salario; }
            set { salario = value; }
        }

        public DateTime FechaContratacion
        {
            get { return fechaContratacion; }
            set { fechaContratacion = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
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

        public string Dpi
        {
            get { return dpi; }
            set { dpi = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string TipoEmpleado
        {
            get { return tipoEmpleado; }
            set { tipoEmpleado = value; }
        }

        public EmpleadosEntidad()
        {
            codigoEmpleado = 0;
            codigoSucursal = 0;
            codigoUsuario = 0;
            nombreEmpleado = string.Empty;
            fechaNacimiento = DateTime.MinValue;
            salario = 0m;
            fechaContratacion = DateTime.MinValue;
            telefono = string.Empty;
            observaciones = string.Empty;
            estado = false;
            dpi = string.Empty;
            direccion = string.Empty;
            tipoEmpleado = string.Empty;
        }

        public EmpleadosEntidad(int codigoEmpleado, int codigoSucursal, int codigoUsuario, string nombreEmpleado, DateTime fechaNacimiento, decimal salario, DateTime fechaContratacion, string telefono, string observaciones, bool estado, string dpi, string direccion, string tipoEmpleado)
        {
            this.codigoEmpleado = codigoEmpleado;
            this.codigoSucursal = codigoSucursal;
            this.codigoUsuario = codigoUsuario;
            this.nombreEmpleado = nombreEmpleado;
            this.fechaNacimiento = fechaNacimiento;
            this.salario = salario;
            this.fechaContratacion = fechaContratacion;
            this.telefono = telefono;
            this.observaciones = observaciones;
            this.estado = estado;
            this.dpi = dpi;
            this.direccion = direccion;
            this.tipoEmpleado = tipoEmpleado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public EmpleadosEntidad(int codigoSucursal, int codigoUsuario, string nombreEmpleado, DateTime fechaNacimiento, decimal salario, DateTime fechaContratacion, string telefono, string observaciones, bool estado, string dpi, string direccion, string tipoEmpleado)
        {
            this.codigoEmpleado = 0;
            this.codigoSucursal = codigoSucursal;
            this.codigoUsuario = codigoUsuario;
            this.nombreEmpleado = nombreEmpleado;
            this.fechaNacimiento = fechaNacimiento;
            this.salario = salario;
            this.fechaContratacion = fechaContratacion;
            this.telefono = telefono;
            this.observaciones = observaciones;
            this.estado = estado;
            this.dpi = dpi;
            this.direccion = direccion;
            this.tipoEmpleado = tipoEmpleado;
        }
    }
}