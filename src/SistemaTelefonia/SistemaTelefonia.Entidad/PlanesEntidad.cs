using System;

namespace SistemaTelefonia.Entidad
{
    public class PlanesEntidad
    {
        private int codigoPlan;
        private string nombre;
        private string descripcion;
        private decimal costoMensual;
        private int cantidadDatos;
        private int minutosIncluidos;
        private int vigenciaMeses;
        private bool estado;

        public int CodigoPlan
        {
            get { return codigoPlan; }
            set { codigoPlan = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public decimal CostoMensual
        {
            get { return costoMensual; }
            set { costoMensual = value; }
        }

        public int CantidadDatos
        {
            get { return cantidadDatos; }
            set { cantidadDatos = value; }
        }

        public int MinutosIncluidos
        {
            get { return minutosIncluidos; }
            set { minutosIncluidos = value; }
        }

        public int VigenciaMeses
        {
            get { return vigenciaMeses; }
            set { vigenciaMeses = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public PlanesEntidad()
        {
            codigoPlan = 0;
            nombre = string.Empty;
            descripcion = string.Empty;
            costoMensual = 0m;
            cantidadDatos = 0;
            minutosIncluidos = 0;
            vigenciaMeses = 0;
            estado = false;
        }

        public PlanesEntidad(int codigoPlan, string nombre, string descripcion, decimal costoMensual, int cantidadDatos, int minutosIncluidos, int vigenciaMeses, bool estado)
        {
            this.codigoPlan = codigoPlan;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.costoMensual = costoMensual;
            this.cantidadDatos = cantidadDatos;
            this.minutosIncluidos = minutosIncluidos;
            this.vigenciaMeses = vigenciaMeses;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public PlanesEntidad(string nombre, string descripcion, decimal costoMensual, int cantidadDatos, int minutosIncluidos, int vigenciaMeses, bool estado)
        {
            this.codigoPlan = 0;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.costoMensual = costoMensual;
            this.cantidadDatos = cantidadDatos;
            this.minutosIncluidos = minutosIncluidos;
            this.vigenciaMeses = vigenciaMeses;
            this.estado = estado;
        }
    }
}