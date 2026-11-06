using System;

namespace SistemaTelefonia.Entidad
{
    public class ProveedoresEntidad
    {
        private int codigoProveedor;
        private string nombre;
        private string nit;
        private string direccion;
        private string telefono;
        private string correoElectronico;
        private string nombreContacto;
        private string descripcion;
        private bool estado;

        public int CodigoProveedor
        {
            get { return codigoProveedor; }
            set { codigoProveedor = value; }
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

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string CorreoElectronico
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }

        public string NombreContacto
        {
            get { return nombreContacto; }
            set { nombreContacto = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public ProveedoresEntidad()
        {
            codigoProveedor = 0;
            nombre = string.Empty;
            nit = string.Empty;
            direccion = string.Empty;
            telefono = string.Empty;
            correoElectronico = string.Empty;
            nombreContacto = string.Empty;
            descripcion = string.Empty;
            estado = false;
        }

        public ProveedoresEntidad(int codigoProveedor, string nombre, string nit, string direccion, string telefono, string correoElectronico, string nombreContacto, string descripcion, bool estado)
        {
            this.codigoProveedor = codigoProveedor;
            this.nombre = nombre;
            this.nit = nit;
            this.direccion = direccion;
            this.telefono = telefono;
            this.correoElectronico = correoElectronico;
            this.nombreContacto = nombreContacto;
            this.descripcion = descripcion;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public ProveedoresEntidad(string nombre, string nit, string direccion, string telefono, string correoElectronico, string nombreContacto, string descripcion, bool estado)
        {
            this.codigoProveedor = 0;
            this.nombre = nombre;
            this.nit = nit;
            this.direccion = direccion;
            this.telefono = telefono;
            this.correoElectronico = correoElectronico;
            this.nombreContacto = nombreContacto;
            this.descripcion = descripcion;
            this.estado = estado;
        }
    }
}