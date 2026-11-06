using System;
using System.Collections.Generic;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>Valida el rango y coordina las consultas del dashboard.</summary>
    public class DashboardNegocio
    {
        private readonly DashboardDatos datos = new DashboardDatos();

        public DashboardEntidad ObtenerResumen(DateTime desde, DateTime hasta, string agrupacion)
        {
            if (desde.Date > hasta.Date) throw new ArgumentException("La fecha inicial no puede ser posterior a la fecha final.");
            int cantidadDias = (hasta.Date - desde.Date).Days + 1;
            DateTime hastaAnterior = desde.Date.AddDays(-1);
            DateTime desdeAnterior = hastaAnterior.AddDays(-cantidadDias + 1);
            return datos.ObtenerResumen(desde, hasta, desdeAnterior, hastaAnterior);
        }

        public List<DashboardEntidad> ObtenerSerie(DateTime desde, DateTime hasta, string agrupacion)
        {
            if (desde.Date > hasta.Date) throw new ArgumentException("La fecha inicial no puede ser posterior a la fecha final.");
            return datos.ObtenerSerie(desde, hasta, agrupacion);
        }

        public List<DashboardEntidad> ObtenerProductosBajoMinimo() { return datos.ObtenerProductosBajoMinimo(); }

        public List<DashboardEntidad> ObtenerMasVendidos(DateTime desde, DateTime hasta)
        {
            if (desde.Date > hasta.Date) throw new ArgumentException("La fecha inicial no puede ser posterior a la fecha final.");
            return datos.ObtenerMasVendidos(desde, hasta);
        }
    }
}
