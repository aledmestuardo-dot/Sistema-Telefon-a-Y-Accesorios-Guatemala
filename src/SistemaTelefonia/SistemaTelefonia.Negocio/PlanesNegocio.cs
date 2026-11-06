using System;
using System.Collections.Generic;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>Valida los campos del plan antes de guardarlo.</summary>
    public class PlanesNegocio
    {
        private readonly PlanesDatos datos = new PlanesDatos();

        public List<PlanesEntidad> Listar()
        {
            return datos.Listar();
        }

        public List<PlanesEntidad> Buscar(string criterio)
        {
            return datos.Buscar((criterio ?? string.Empty).Trim());
        }

        public int Agregar(PlanesEntidad plan)
        {
            if (plan == null)
                throw new ArgumentNullException("plan");
            if (string.IsNullOrWhiteSpace(plan.Nombre) || plan.Nombre.Length > 100)
                throw new ArgumentException("El nombre es obligatorio y admite hasta 100 caracteres.");
            if (string.IsNullOrWhiteSpace(plan.Descripcion) || plan.Descripcion.Length > 200)
                throw new ArgumentException("La descripción es obligatoria y admite hasta 200 caracteres.");
            if (plan.CostoMensual <= 0 || plan.CostoMensual > 99999999.99m ||
                decimal.Round(plan.CostoMensual, 2) != plan.CostoMensual)
                throw new ArgumentException("El costo debe ser positivo y tener como máximo dos decimales.");
            if (plan.CantidadDatos <= 0)
                throw new ArgumentException("La cantidad de datos debe ser mayor que cero.");
            if (plan.MinutosIncluidos < 0)
                throw new ArgumentException("Los minutos incluidos no pueden ser negativos.");
            if (plan.VigenciaMeses <= 0)
                throw new ArgumentException("La vigencia debe ser mayor que cero.");

            return datos.Agregar(plan);
        }

        public void Editar(PlanesEntidad plan)
        {
            if (plan == null)
                throw new ArgumentNullException("plan");
            if (plan.CodigoPlan <= 0)
                throw new ArgumentException("Seleccione un plan valido para editar.");
            if (string.IsNullOrWhiteSpace(plan.Nombre) || plan.Nombre.Length > 100)
                throw new ArgumentException("El nombre es obligatorio y admite hasta 100 caracteres.");
            if (string.IsNullOrWhiteSpace(plan.Descripcion) || plan.Descripcion.Length > 200)
                throw new ArgumentException("La descripcion es obligatoria y admite hasta 200 caracteres.");
            if (plan.CostoMensual <= 0 || plan.CostoMensual > 99999999.99m ||
                decimal.Round(plan.CostoMensual, 2) != plan.CostoMensual)
                throw new ArgumentException("El costo debe ser positivo y tener como maximo dos decimales.");
            if (plan.CantidadDatos <= 0)
                throw new ArgumentException("La cantidad de datos debe ser mayor que cero.");
            if (plan.MinutosIncluidos < 0)
                throw new ArgumentException("Los minutos incluidos no pueden ser negativos.");
            if (plan.VigenciaMeses <= 0)
                throw new ArgumentException("La vigencia debe ser mayor que cero.");

            datos.Editar(plan);
        }

        public void Eliminar(int codigoPlan)
        {
            if (codigoPlan <= 0)
                throw new ArgumentException("Seleccione un plan válido para eliminar.");

            datos.Eliminar(codigoPlan);
        }
    }
}
