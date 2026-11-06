using System.Collections.Generic;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>
    /// Expone la consulta de clientes para la capa de Presentación.
    /// </summary>
    public class ClientesNegocio
    {
        private readonly ClientesDatos datos = new ClientesDatos();

        public int Agregar(ClientesEntidad cliente)
        {
            if (cliente == null)
                throw new System.ArgumentNullException("cliente");

            // Comprueba que el texto respete las columnas de dbo.Tbl_Clientes.
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || cliente.Nombre.Length > 100)
                throw new System.ArgumentException("El nombre es obligatorio y admite hasta 100 caracteres.");
            if (cliente.Nit != null && cliente.Nit.Length > 45)
                throw new System.ArgumentException("El NIT admite hasta 45 caracteres.");
            if (cliente.CorreoElectronico != null && cliente.CorreoElectronico.Length > 100)
                throw new System.ArgumentException("El correo admite hasta 100 caracteres.");
            if (string.IsNullOrWhiteSpace(cliente.Telefono) || cliente.Telefono.Length > 20)
                throw new System.ArgumentException("El teléfono es obligatorio y admite hasta 20 caracteres.");
            if (string.IsNullOrWhiteSpace(cliente.Direccion) || cliente.Direccion.Length > 100)
                throw new System.ArgumentException("La dirección es obligatoria y admite hasta 100 caracteres.");
            if (string.IsNullOrWhiteSpace(cliente.Departamento) || cliente.Departamento.Length > 100)
                throw new System.ArgumentException("El departamento es obligatorio y admite hasta 100 caracteres.");
            if (string.IsNullOrWhiteSpace(cliente.Municipio) || cliente.Municipio.Length > 100)
                throw new System.ArgumentException("El municipio es obligatorio y admite hasta 100 caracteres.");

            return datos.Agregar(cliente);
        }

        public List<ClientesEntidad> Listar()
        {
            return datos.Listar();
        }

        public List<ClientesEntidad> Buscar(string criterio)
        {
            return datos.Buscar(criterio);
        }

        public void Editar(ClientesEntidad cliente)
        {
            if (cliente == null)
                throw new System.ArgumentNullException("cliente");
            if (cliente.CodigoCliente <= 0)
                throw new System.ArgumentException("Seleccione un cliente válido para editar.");
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || cliente.Nombre.Length > 100)
                throw new System.ArgumentException("El nombre es obligatorio y admite hasta 100 caracteres.");
            if (cliente.Nit != null && cliente.Nit.Length > 45)
                throw new System.ArgumentException("El NIT admite hasta 45 caracteres.");
            if (cliente.CorreoElectronico != null && cliente.CorreoElectronico.Length > 100)
                throw new System.ArgumentException("El correo admite hasta 100 caracteres.");
            if (string.IsNullOrWhiteSpace(cliente.Telefono) || cliente.Telefono.Length > 20)
                throw new System.ArgumentException("El teléfono es obligatorio y admite hasta 20 caracteres.");
            if (string.IsNullOrWhiteSpace(cliente.Direccion) || cliente.Direccion.Length > 100)
                throw new System.ArgumentException("La dirección es obligatoria y admite hasta 100 caracteres.");
            if (string.IsNullOrWhiteSpace(cliente.Departamento) || cliente.Departamento.Length > 100)
                throw new System.ArgumentException("El departamento es obligatorio y admite hasta 100 caracteres.");
            if (string.IsNullOrWhiteSpace(cliente.Municipio) || cliente.Municipio.Length > 100)
                throw new System.ArgumentException("El municipio es obligatorio y admite hasta 100 caracteres.");

            datos.Editar(cliente);
        }

        public void Eliminar(int codigoCliente)
        {
            if (codigoCliente <= 0)
                throw new System.ArgumentException("Seleccione un cliente válido para eliminar.");

            datos.Eliminar(codigoCliente);
        }
    }
}
