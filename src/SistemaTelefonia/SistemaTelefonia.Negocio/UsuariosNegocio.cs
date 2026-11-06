using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>Valida usuarios y delega la persistencia en Datos.</summary>
    public class UsuariosNegocio
    {
        private const int IteracionesHash = 120000;
        private const string PrefijoHash = "PBKDF2$";
        private readonly UsuariosDatos datos = new UsuariosDatos();
        private readonly ConexionSupabaseDatos autenticacionSupabase = new ConexionSupabaseDatos();
        private readonly UsuariosSupabaseDatos usuariosSupabase = new UsuariosSupabaseDatos();

        public static UsuariosEntidad UsuarioActual { get; private set; }

        public List<UsuariosEntidad> Listar()
        {
            return OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase
                ? usuariosSupabase.Listar()
                : datos.Listar();
        }

        public List<UsuariosEntidad> Buscar(string criterio)
        {
            string texto = (criterio ?? string.Empty).Trim();
            if (OrigenBaseDatosDatos.Leer() != OrigenBaseDatos.Supabase) return datos.Buscar(texto);
            List<UsuariosEntidad> lista = usuariosSupabase.Listar();
            if (texto.Length == 0) return lista;
            return lista.FindAll(u => u.CodigoUsuario.ToString().IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0
                || (u.TipoUsuario ?? string.Empty).IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) >= 0
                || (u.Nombre ?? string.Empty).IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) >= 0
                || (u.CorreoElectronico ?? string.Empty).IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }

        /// <summary>Autentica una cuenta activa y registra la fecha del acceso correcto.</summary>
        public UsuariosEntidad Autenticar(string nombre, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrEmpty(contrasenia)) return null;
            UsuarioActual = null;

            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                UsuarioActual = autenticacionSupabase.Autenticar(nombre, contrasenia);
                return UsuarioActual;
            }

            UsuariosEntidad usuario = datos.BuscarActivoPorNombre(nombre.Trim());
            if (usuario == null || !VerificarContrasenia(contrasenia, usuario.Contrasenia)) return null;

            DateTime ahora = DateTime.Now;
            datos.ActualizarUltimoAcceso(usuario.CodigoUsuario, ahora);
            usuario.FechaUltimoAcceso = ahora;
            UsuarioActual = usuario;
            return usuario;
        }

        /// <summary>Cierra y descarta la sesión temporal de Supabase.</summary>
        public void CerrarSesionSupabase()
        {
            autenticacionSupabase.CerrarSesionActual();
            UsuarioActual = null;
        }

        public int Agregar(UsuariosEntidad usuario)
        {
            Validar(usuario);
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
                return usuariosSupabase.Agregar(usuario);
            usuario.Contrasenia = AsegurarHash(usuario.Contrasenia);
            return datos.Agregar(usuario);
        }

        public void Editar(UsuariosEntidad usuario)
        {
            Validar(usuario, false);
            if (usuario.CodigoUsuario <= 0)
                throw new ArgumentException("Seleccione un usuario válido para editar.");
            if (UsuarioActual != null && UsuarioActual.CodigoUsuario == usuario.CodigoUsuario && !usuario.Estado)
                throw new InvalidOperationException("No puedes inactivar la cuenta que tiene la sesión abierta.");
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                usuariosSupabase.Editar(usuario);
                return;
            }
            if (!string.IsNullOrWhiteSpace(usuario.Contrasenia)) usuario.Contrasenia = AsegurarHash(usuario.Contrasenia);
            datos.Editar(usuario);
        }

        /// <summary>Protege contraseñas con PBKDF2 antes de guardarlas.</summary>
        private static string AsegurarHash(string contrasenia)
        {
            if (contrasenia.StartsWith(PrefijoHash, StringComparison.Ordinal)) return contrasenia;

            byte[] sal = new byte[16];
            using (RandomNumberGenerator generador = RandomNumberGenerator.Create())
                generador.GetBytes(sal);

            byte[] hash;
            using (Rfc2898DeriveBytes derivador = new Rfc2898DeriveBytes(contrasenia, sal, IteracionesHash))
                hash = derivador.GetBytes(32);

            return PrefijoHash + IteracionesHash + "$" + Convert.ToBase64String(sal) + "$" + Convert.ToBase64String(hash);
        }

        /// <summary>Compara la contraseña ingresada con el hash persistido.</summary>
        private static bool VerificarContrasenia(string contrasenia, string almacenada)
        {
            if (string.IsNullOrEmpty(almacenada) || !almacenada.StartsWith(PrefijoHash, StringComparison.Ordinal)) return false;
            string[] partes = almacenada.Split('$');
            if (partes.Length != 4 || partes[0] != "PBKDF2") return false;

            int iteraciones;
            byte[] sal, hashEsperado;
            try
            {
                iteraciones = int.Parse(partes[1]);
                sal = Convert.FromBase64String(partes[2]);
                hashEsperado = Convert.FromBase64String(partes[3]);
            }
            catch (FormatException) { return false; }
            catch (OverflowException) { return false; }
            if (iteraciones < 10000 || iteraciones > 1000000 || hashEsperado.Length != 32) return false;

            byte[] hashIngresado;
            using (Rfc2898DeriveBytes derivador = new Rfc2898DeriveBytes(contrasenia, sal, iteraciones))
                hashIngresado = derivador.GetBytes(hashEsperado.Length);

            int diferencia = 0;
            for (int i = 0; i < hashEsperado.Length; i++) diferencia |= hashEsperado[i] ^ hashIngresado[i];
            return diferencia == 0;
        }

        public void Eliminar(int codigoUsuario)
        {
            if (codigoUsuario <= 0)
                throw new ArgumentException("Seleccione un usuario válido para eliminar.");
            if (UsuarioActual != null && UsuarioActual.CodigoUsuario == codigoUsuario)
                throw new InvalidOperationException("No puedes inactivar la cuenta que tiene la sesión abierta.");
            if (OrigenBaseDatosDatos.Leer() == OrigenBaseDatos.Supabase)
            {
                usuariosSupabase.Inactivar(codigoUsuario);
                return;
            }
            datos.Eliminar(codigoUsuario);
        }

        private static void Validar(UsuariosEntidad u, bool contraseniaRequerida = true)
        {
            if (u == null) throw new ArgumentNullException("usuario");
            Requerido(u.TipoUsuario, 100, "Tipo de usuario");
            Requerido(u.Nombre, 100, "Nombre");
            if (contraseniaRequerida || !string.IsNullOrWhiteSpace(u.Contrasenia))
                Requerido(u.Contrasenia, 100, "Contraseña");
            Requerido(u.CorreoElectronico, 100, "Correo electrónico");
            if (u.FechaCreacion == DateTime.MinValue)
                throw new ArgumentException("La fecha de creación es obligatoria.");
        }

        private static void Requerido(string valor, int maximo, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor)) throw new ArgumentException("El campo " + campo + " es obligatorio.");
            if (valor.Trim().Length > maximo)
                throw new ArgumentException("El campo " + campo + " no puede superar " + maximo + " caracteres.");
        }
    }
}
