using System;

namespace SistemaTelefonia.Entidad
{
    public class UsuariosEntidad
    {
        private int codigoUsuario;
        private string tipoUsuario;
        private string nombre;
        private string contrasenia;
        private string correoElectronico;
        private DateTime fechaCreacion;
        private DateTime? fechaUltimoAcceso;
        private bool estado;
        private bool esDueno;
        private string authUserId;

        public int CodigoUsuario
        {
            get { return codigoUsuario; }
            set { codigoUsuario = value; }
        }

        public string TipoUsuario
        {
            get { return tipoUsuario; }
            set { tipoUsuario = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Contrasenia
        {
            get { return contrasenia; }
            set { contrasenia = value; }
        }

        public string CorreoElectronico
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }

        public DateTime? FechaUltimoAcceso
        {
            get { return fechaUltimoAcceso; }
            set { fechaUltimoAcceso = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        /// <summary>Indica la cuenta propietaria protegida en Supabase.</summary>
        public bool EsDueno
        {
            get { return esDueno; }
            set { esDueno = value; }
        }

        /// <summary>Identificador de la cuenta autenticada asociada al perfil.</summary>
        public string AuthUserId
        {
            get { return authUserId; }
            set { authUserId = value; }
        }

        public UsuariosEntidad()
        {
            codigoUsuario = 0;
            tipoUsuario = string.Empty;
            nombre = string.Empty;
            contrasenia = string.Empty;
            correoElectronico = string.Empty;
            fechaCreacion = DateTime.MinValue;
            fechaUltimoAcceso = null;
            estado = false;
            esDueno = false;
            authUserId = string.Empty;
        }

        public UsuariosEntidad(int codigoUsuario, string tipoUsuario, string nombre, string contrasenia, string correoElectronico, DateTime fechaCreacion, DateTime? fechaUltimoAcceso, bool estado)
        {
            this.codigoUsuario = codigoUsuario;
            this.tipoUsuario = tipoUsuario;
            this.nombre = nombre;
            this.contrasenia = contrasenia;
            this.correoElectronico = correoElectronico;
            this.fechaCreacion = fechaCreacion;
            this.fechaUltimoAcceso = fechaUltimoAcceso;
            this.estado = estado;
        }

        // Constructor para preparar un registro nuevo; omite la PK IDENTITY.
        public UsuariosEntidad(string tipoUsuario, string nombre, string contrasenia, string correoElectronico, DateTime fechaCreacion, DateTime? fechaUltimoAcceso, bool estado)
        {
            this.codigoUsuario = 0;
            this.tipoUsuario = tipoUsuario;
            this.nombre = nombre;
            this.contrasenia = contrasenia;
            this.correoElectronico = correoElectronico;
            this.fechaCreacion = fechaCreacion;
            this.fechaUltimoAcceso = fechaUltimoAcceso;
            this.estado = estado;
        }
    }
}
