using System;
using System.Collections.Generic;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>Valida proveedores y delega la persistencia en Datos.</summary>
    public class ProveedoresNegocio
    {
        private readonly ProveedoresDatos datos = new ProveedoresDatos();

        public List<ProveedoresEntidad> Listar() { return datos.Listar(); }
        public List<ProveedoresEntidad> Buscar(string criterio) { return datos.Buscar((criterio ?? string.Empty).Trim()); }

        public int Agregar(ProveedoresEntidad proveedor)
        {
            Validar(proveedor);
            return datos.Agregar(proveedor);
        }

        public void Editar(ProveedoresEntidad proveedor)
        {
            Validar(proveedor);
            if (proveedor.CodigoProveedor <= 0)
                throw new ArgumentException("Seleccione un proveedor válido para editar.");
            datos.Editar(proveedor);
        }

        public void Eliminar(int codigoProveedor)
        {
            if (codigoProveedor <= 0)
                throw new ArgumentException("Seleccione un proveedor válido para eliminar.");
            datos.Eliminar(codigoProveedor);
        }

        private static void Validar(ProveedoresEntidad p)
        {
            if (p == null) throw new ArgumentNullException("proveedor");
            Requerido(p.Nombre, 100, "Nombre");
            Requerido(p.Direccion, 200, "Dirección");
            Requerido(p.Telefono, 20, "Teléfono");
            Maximo(p.Nit, 45, "NIT");
            Maximo(p.CorreoElectronico, 100, "Correo electrónico");
            Maximo(p.NombreContacto, 100, "Nombre de contacto");
            Maximo(p.Descripcion, 200, "Descripción");
        }

        private static void Requerido(string valor, int maximo, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor)) throw new ArgumentException("El campo " + campo + " es obligatorio.");
            Maximo(valor, maximo, campo);
        }

        private static void Maximo(string valor, int maximo, string campo)
        {
            if (valor != null && valor.Trim().Length > maximo)
                throw new ArgumentException("El campo " + campo + " no puede superar " + maximo + " caracteres.");
        }
    }
}
