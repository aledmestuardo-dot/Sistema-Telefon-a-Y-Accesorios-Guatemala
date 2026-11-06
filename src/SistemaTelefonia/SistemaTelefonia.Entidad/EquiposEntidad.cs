using System;

namespace SistemaTelefonia.Entidad
{
    public class EquiposEntidad
    {
        private int codigoEquipo;
        private int codigoMarca;
        private string categorias;
        private string modelo;
        private string descripcion;
        private decimal precio;
        private string numeroSerie;
        private bool estado;

        public int CodigoEquipo
        {
            get { return codigoEquipo; }
            set { codigoEquipo = value; }
        }

        public int CodigoMarca
        {
            get { return codigoMarca; }
            set { codigoMarca = value; }
        }

        public string Categorias
        {
            get { return categorias; }
            set { categorias = value; }
        }

        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public string NumeroSerie
        {
            get { return numeroSerie; }
            set { numeroSerie = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public EquiposEntidad()
        {
            codigoEquipo = 0;
            codigoMarca = 0;
            categorias = string.Empty;
            modelo = string.Empty;
            descripcion = string.Empty;
            precio = 0m;
            numeroSerie = string.Empty;
            estado = false;
        }

        public EquiposEntidad(int codigoEquipo, int codigoMarca, string categorias, string modelo, string descripcion, decimal precio, string numeroSerie, bool estado)
        {
            this.codigoEquipo = codigoEquipo;
            this.codigoMarca = codigoMarca;
            this.categorias = categorias;
            this.modelo = modelo;
            this.descripcion = descripcion;
            this.precio = precio;
            this.numeroSerie = numeroSerie;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public EquiposEntidad(int codigoMarca, string categorias, string modelo, string descripcion, decimal precio, string numeroSerie, bool estado)
        {
            this.codigoEquipo = 0;
            this.codigoMarca = codigoMarca;
            this.categorias = categorias;
            this.modelo = modelo;
            this.descripcion = descripcion;
            this.precio = precio;
            this.numeroSerie = numeroSerie;
            this.estado = estado;
        }
    }
}