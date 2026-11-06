using System;
using System.Collections.Generic;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>Validaciones básicas y coordinación del CRUD para entidades con FK.</summary>
    public class CrudRelacionalNegocio
    {
        private readonly CrudRelacionalDatos datos = new CrudRelacionalDatos();

        public List<FacturacionEntidad> ListarFacturasPendientes() { return datos.ListarFacturasPendientes(); }

        public List<T> Listar<T>() where T : new() { return datos.Listar<T>(); }

        /// <summary>Lista registros para combos FK usando consultas de lectura existentes.</summary>
        public List<T> ListarCatalogo<T>() where T : new()
        {
            object lista;
            if (typeof(T) == typeof(ClientesEntidad)) lista = new ClientesNegocio().Listar();
            else if (typeof(T) == typeof(ComprasEntidad)) lista = new ComprasNegocio().Listar();
            else if (typeof(T) == typeof(PlanesEntidad)) lista = new PlanesNegocio().Listar();
            else if (typeof(T) == typeof(ProveedoresEntidad)) lista = new ProveedoresNegocio().Listar();
            else if (typeof(T) == typeof(SucursalesEntidad)) lista = new SucursalesNegocio().Listar();
            else if (typeof(T) == typeof(UsuariosEntidad))
                lista = new ConfiguracionConexionNegocio().ObtenerOrigenElegido() == OrigenBaseDatos.Supabase
                    ? datos.ListarUsuariosCatalogo()
                    : new UsuariosNegocio().Listar();
            else lista = datos.Listar<T>();
            return (List<T>)lista;
        }

        public List<T> Buscar<T>(string criterio) where T : new()
        {
            if (criterio == null) throw new ArgumentNullException("criterio");
            return datos.Buscar<T>(criterio.Trim());
        }

        public int Agregar<T>(T entidad) where T : new()
        {
            if (ReferenceEquals(entidad, null)) throw new ArgumentNullException("entidad");
            return datos.Agregar(entidad);
        }

        public void Editar<T>(int clave, T entidad) where T : new()
        {
            if (clave <= 0) throw new ArgumentException("Seleccione un registro válido.", "clave");
            if (ReferenceEquals(entidad, null)) throw new ArgumentNullException("entidad");
            datos.Editar(clave, entidad);
        }

        public void Eliminar<T>(int clave) where T : new()
        {
            if (clave <= 0) throw new ArgumentException("Seleccione un registro válido.", "clave");
            datos.Eliminar<T>(clave);
        }

        public void InactivarMarca(int codigoMarca)
        {
            if (codigoMarca <= 0) throw new ArgumentException("Seleccione una marca válida.", "codigoMarca");
            datos.InactivarMarca(codigoMarca);
        }

        public List<TDetalle> ListarRelacionados<TDetalle>(string claveForanea, int claveMaestro) where TDetalle : new()
        {
            if (claveMaestro <= 0) throw new ArgumentException("Seleccione un encabezado vÃ¡lido.", "claveMaestro");
            return datos.ListarRelacionados<TDetalle>(claveForanea, claveMaestro);
        }

        public int AgregarConDetalles<TMaestro, TDetalle>(TMaestro maestro, IList<TDetalle> detalles, string claveForanea)
            where TMaestro : new() where TDetalle : new()
        {
            if (ReferenceEquals(maestro, null)) throw new ArgumentNullException("maestro");
            if (detalles == null || detalles.Count == 0) throw new ArgumentException("Agrega al menos una lÃ­nea de detalle.", "detalles");
            return datos.AgregarConDetalles(maestro, detalles, claveForanea);
        }

        public void EditarConDetalles<TMaestro, TDetalle>(int claveMaestro, TMaestro maestro, IList<TDetalle> detalles, string claveForanea)
            where TMaestro : new() where TDetalle : new()
        {
            if (claveMaestro <= 0) throw new ArgumentException("Seleccione un registro vÃ¡lido.", "claveMaestro");
            if (ReferenceEquals(maestro, null)) throw new ArgumentNullException("maestro");
            if (detalles == null || detalles.Count == 0) throw new ArgumentException("El registro debe conservar al menos una lÃ­nea de detalle.", "detalles");
            datos.EditarConDetalles(claveMaestro, maestro, detalles, claveForanea);
        }

        public void EliminarConDetalles<TMaestro, TDetalle>(int claveMaestro, string claveForanea)
            where TMaestro : new() where TDetalle : new()
        {
            if (claveMaestro <= 0) throw new ArgumentException("Seleccione un registro vÃ¡lido.", "claveMaestro");
            datos.EliminarConDetalles<TMaestro, TDetalle>(claveMaestro, claveForanea);
        }
    }
}
