using System;
using System.Collections.Generic;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>Valida sucursales y delega la persistencia en Datos.</summary>
    public class SucursalesNegocio
    {
        private readonly SucursalesDatos datos = new SucursalesDatos();

        public List<SucursalesEntidad> Listar() { return datos.Listar(); }
        public List<SucursalesEntidad> Buscar(string criterio) { return datos.Buscar((criterio ?? string.Empty).Trim()); }

        public int Agregar(SucursalesEntidad sucursal)
        {
            Validar(sucursal);
            return datos.Agregar(sucursal);
        }

        public void Editar(SucursalesEntidad sucursal)
        {
            Validar(sucursal);
            if (sucursal.CodigoSucursal <= 0)
                throw new ArgumentException("Seleccione una sucursal válida para editar.");
            datos.Editar(sucursal);
        }

        public void Eliminar(int codigoSucursal)
        {
            if (codigoSucursal <= 0)
                throw new ArgumentException("Seleccione una sucursal válida para eliminar.");
            datos.Eliminar(codigoSucursal);
        }

        private static void Validar(SucursalesEntidad s)
        {
            if (s == null) throw new ArgumentNullException("sucursal");
            Requerido(s.Nombre, 100, "Nombre");
            Requerido(s.Direccion, 200, "Dirección");
            Requerido(s.Municipio, 100, "Municipio");
            Requerido(s.Departamento, 100, "Departamento");
            Requerido(s.Telefono, 20, "Teléfono");
            Requerido(s.HorarioAtencion, 100, "Horario de atención");
            if (s.CorreoElectronico != null && s.CorreoElectronico.Trim().Length > 100)
                throw new ArgumentException("El correo electrónico no puede superar 100 caracteres.");
            if (s.FechaApertura == DateTime.MinValue)
                throw new ArgumentException("La fecha de apertura es obligatoria.");
        }

        private static void Requerido(string valor, int maximo, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor)) throw new ArgumentException("El campo " + campo + " es obligatorio.");
            if (valor.Trim().Length > maximo)
                throw new ArgumentException("El campo " + campo + " no puede superar " + maximo + " caracteres.");
        }
    }
}
