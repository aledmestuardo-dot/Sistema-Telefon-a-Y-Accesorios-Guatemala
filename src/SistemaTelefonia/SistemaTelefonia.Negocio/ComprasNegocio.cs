using System.Collections.Generic;
using System;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>Coordina la consulta de compras y delega el acceso en Datos.</summary>
    public class ComprasNegocio
    {
        /// <summary>Porcentaje de IVA usado para compras en Guatemala.</summary>
        public const decimal TasaIvaGuatemala = 0.12m;

        private readonly ComprasDatos datos = new ComprasDatos();

        public static decimal CalcularIva(decimal subtotal)
        {
            if (subtotal < 0) throw new ArgumentOutOfRangeException("subtotal");
            return decimal.Round(subtotal * TasaIvaGuatemala, 2, MidpointRounding.AwayFromZero);
        }

        public List<ComprasEntidad> Listar()
        {
            return datos.Listar();
        }

        public List<ComprasEntidad> Buscar(string criterio)
        {
            if (criterio == null)
                throw new System.ArgumentNullException("criterio");

            return datos.Buscar(criterio.Trim());
        }

        public int Agregar(ComprasEntidad compra)
        {
            if (compra == null)
                throw new System.ArgumentNullException("compra");
            if (compra.CodigoProveedor <= 0)
                throw new System.ArgumentException("Seleccione un proveedor vÃ¡lido.");
            if (string.IsNullOrWhiteSpace(compra.NumeroDocumento) || compra.NumeroDocumento.Trim().Length > 50)
                throw new System.ArgumentException("El nÃºmero de documento es obligatorio y no puede superar 50 caracteres.");
            if (compra.Subtotal <= 0 || compra.Total <= 0 || compra.Iva < 0)
                throw new System.ArgumentException("Revise los valores de subtotal, IVA y total.");
            if (compra.Observaciones != null && compra.Observaciones.Trim().Length > 200)
                throw new System.ArgumentException("Observaciones no puede superar 200 caracteres.");

            return datos.Agregar(compra);
        }

        public int AgregarConDetalles(ComprasEntidad compra, IList<DetalleCompraEntidad> detalles)
        {
            if (compra == null) throw new System.ArgumentNullException("compra");
            if (detalles == null || detalles.Count == 0)
                throw new System.ArgumentException("Agrega al menos un equipo al detalle de la compra.", "detalles");
            if (compra.CodigoProveedor <= 0)
                throw new System.ArgumentException("Seleccione un proveedor válido.");
            if (string.IsNullOrWhiteSpace(compra.NumeroDocumento) || compra.NumeroDocumento.Trim().Length > 50)
                throw new System.ArgumentException("El número de documento es obligatorio y no puede superar 50 caracteres.");
            if (compra.Subtotal <= 0 || compra.Total < compra.Subtotal || compra.Iva < 0)
                throw new System.ArgumentException("Revise el subtotal, el IVA y el total de la compra.");
            foreach (DetalleCompraEntidad detalle in detalles)
            {
                if (detalle == null || detalle.CodigoEquipo <= 0 || detalle.Cantidad <= 0 || detalle.CostoUnitario <= 0)
                    throw new System.ArgumentException("Cada detalle debe incluir un equipo, cantidad y costo unitario válidos.");
                if (detalle.Observaciones != null && detalle.Observaciones.Trim().Length > 200)
                    throw new System.ArgumentException("Las observaciones del detalle no pueden superar 200 caracteres.");
            }

            return datos.AgregarConDetalles(compra, detalles);
        }

        public List<DetalleCompraEntidad> ListarDetalles(int codigoCompra)
        {
            if (codigoCompra <= 0) throw new System.ArgumentException("Seleccione una compra válida.", "codigoCompra");
            return datos.ListarDetalles(codigoCompra);
        }

        public decimal? ObtenerUltimoCostoPorEquipo(int codigoEquipo)
        {
            if (codigoEquipo <= 0) throw new ArgumentException("Seleccione un equipo válido.", "codigoEquipo");
            return datos.ObtenerUltimoCostoPorEquipo(codigoEquipo);
        }

        public void EditarConDetalles(int codigoCompra, ComprasEntidad compra, IList<DetalleCompraEntidad> detalles)
        {
            if (codigoCompra <= 0) throw new System.ArgumentException("Seleccione una compra válida.", "codigoCompra");
            if (compra == null) throw new System.ArgumentNullException("compra");
            if (detalles == null || detalles.Count == 0) throw new System.ArgumentException("La compra debe conservar al menos un detalle.", "detalles");
            if (compra.CodigoProveedor <= 0) throw new System.ArgumentException("Seleccione un proveedor válido.");
            if (string.IsNullOrWhiteSpace(compra.NumeroDocumento) || compra.NumeroDocumento.Trim().Length > 50)
                throw new System.ArgumentException("El número de documento es obligatorio y no puede superar 50 caracteres.");
            if (compra.Subtotal <= 0 || compra.Total < compra.Subtotal || compra.Iva < 0)
                throw new System.ArgumentException("Revise el subtotal, el IVA y el total de la compra.");
            foreach (DetalleCompraEntidad detalle in detalles)
                if (detalle == null || detalle.CodigoEquipo <= 0 || detalle.Cantidad <= 0 || detalle.CostoUnitario <= 0)
                    throw new System.ArgumentException("Cada detalle debe incluir un equipo, cantidad y costo unitario válidos.");
            datos.EditarConDetalles(codigoCompra, compra, detalles);
        }

        public void Editar(int codigoCompra, ComprasEntidad compra)
        {
            if (codigoCompra <= 0)
                throw new System.ArgumentException("Seleccione una compra válida.", "codigoCompra");
            if (compra == null)
                throw new System.ArgumentNullException("compra");
            if (compra.CodigoProveedor <= 0)
                throw new System.ArgumentException("Seleccione un proveedor válido.");
            if (string.IsNullOrWhiteSpace(compra.NumeroDocumento) || compra.NumeroDocumento.Trim().Length > 50)
                throw new System.ArgumentException("El número de documento es obligatorio y no puede superar 50 caracteres.");
            if (compra.Subtotal <= 0 || compra.Total <= 0 || compra.Iva < 0)
                throw new System.ArgumentException("Revise los valores de subtotal, IVA y total.");
            if (compra.Observaciones != null && compra.Observaciones.Trim().Length > 200)
                throw new System.ArgumentException("Observaciones no puede superar 200 caracteres.");

            datos.Editar(codigoCompra, compra);
        }

        public void Eliminar(int codigoCompra)
        {
            if (codigoCompra <= 0)
                throw new System.ArgumentException("Seleccione una compra válida.", "codigoCompra");

            datos.Eliminar(codigoCompra);
        }
    }
}
