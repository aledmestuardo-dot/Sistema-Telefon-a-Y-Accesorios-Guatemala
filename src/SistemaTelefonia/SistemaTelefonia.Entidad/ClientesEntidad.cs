using System;

namespace SistemaTelefonia.Entidad
{
    public class ClientesEntidad
    {
        private int codigoCliente;
        private string nombre;
        private string nit;
        private string correoElectronico;
        private string telefono;
        private string direccion;
        private string departamento;
        private string municipio;
        private bool estado;

        public int CodigoCliente
        {
            get { return codigoCliente; }
            set { codigoCliente = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Nit
        {
            get { return nit; }
            set { nit = value; }
        }

        public string CorreoElectronico
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }

        public string Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public ClientesEntidad()
        {
            codigoCliente = 0;
            nombre = string.Empty;
            nit = string.Empty;
            correoElectronico = string.Empty;
            telefono = string.Empty;
            direccion = string.Empty;
            departamento = string.Empty;
            municipio = string.Empty;
            estado = false;
        }

        public ClientesEntidad(int codigoCliente, string nombre, string nit, string correoElectronico, string telefono, string direccion, string departamento, string municipio, bool estado)
        {
            this.codigoCliente = codigoCliente;
            this.nombre = nombre;
            this.nit = nit;
            this.correoElectronico = correoElectronico;
            this.telefono = telefono;
            this.direccion = direccion;
            this.departamento = departamento;
            this.municipio = municipio;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public ClientesEntidad(string nombre, string nit, string correoElectronico, string telefono, string direccion, string departamento, string municipio, bool estado)
        {
            this.codigoCliente = 0;
            this.nombre = nombre;
            this.nit = nit;
            this.correoElectronico = correoElectronico;
            this.telefono = telefono;
            this.direccion = direccion;
            this.departamento = departamento;
            this.municipio = municipio;
            this.estado = estado;
        }
    }
}