using System;

namespace SistemaTelefonia.Entidad
{
    public class MarcasEntidad
    {
        private int codigoMarca;
        private int codigoProveedor;
        private string nombre;
        private string paisOrigen;
        private string descripcion;
        private string sitioWeb;
        private DateTime fechaRegistro;
        private bool estado;

        public int CodigoMarca
        {
            get { return codigoMarca; }
            set { codigoMarca = value; }
        }

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

        public string PaisOrigen
        {
            get { return paisOrigen; }
            set { paisOrigen = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string SitioWeb
        {
            get { return sitioWeb; }
            set { sitioWeb = value; }
        }

        public DateTime FechaRegistro
        {
            get { return fechaRegistro; }
            set { fechaRegistro = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public MarcasEntidad()
        {
            codigoMarca = 0;
            codigoProveedor = 0;
            nombre = string.Empty;
            paisOrigen = string.Empty;
            descripcion = string.Empty;
            sitioWeb = string.Empty;
            fechaRegistro = DateTime.MinValue;
            estado = false;
        }

        public MarcasEntidad(int codigoMarca, int codigoProveedor, string nombre, string paisOrigen, string descripcion, string sitioWeb, DateTime fechaRegistro, bool estado)
        {
            this.codigoMarca = codigoMarca;
            this.codigoProveedor = codigoProveedor;
            this.nombre = nombre;
            this.paisOrigen = paisOrigen;
            this.descripcion = descripcion;
            this.sitioWeb = sitioWeb;
            this.fechaRegistro = fechaRegistro;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public MarcasEntidad(int codigoProveedor, string nombre, string paisOrigen, string descripcion, string sitioWeb, DateTime fechaRegistro, bool estado)
        {
            this.codigoMarca = 0;
            this.codigoProveedor = codigoProveedor;
            this.nombre = nombre;
            this.paisOrigen = paisOrigen;
            this.descripcion = descripcion;
            this.sitioWeb = sitioWeb;
            this.fechaRegistro = fechaRegistro;
            this.estado = estado;
        }
    }
}