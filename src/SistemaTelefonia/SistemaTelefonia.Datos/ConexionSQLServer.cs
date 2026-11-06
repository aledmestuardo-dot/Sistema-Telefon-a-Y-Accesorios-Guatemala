using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SistemaTelefonia.Datos
{
    /// <summary>
    /// Proporciona la conexion configurada con SQL Server para la capa Datos.
    /// </summary>
    public static class ConexionSQLServer
    {
        private const string NombreVariableEnAppConfig = "ConexionTelefoniaVariableEntorno";
        private const string NombreBaseEntrega = "BD_TelefoniaAccesoriosSA";
        private const string RecursoEsquema = "SistemaTelefonia.Datos.07_SQLServer_Esquema_Limpio.sql";
        private const string ArchivoSeedUsuarios = "08_SQLServer_Usuarios_Iniciales.sql";

        /// <summary>
        /// Crea una conexion sin abrirla. No implementa operaciones CRUD.
        /// La cadena se obtiene de una variable de entorno o del .env local
        /// ignorado por Git; no guarda secretos en App.config ni en el codigo fuente.
        /// </summary>
        public static SqlConnection CrearConexion()
        {
            try
            {
                string nombreVariable = ConfigurationManager.AppSettings[NombreVariableEnAppConfig];
                string cadenaConexion = string.IsNullOrWhiteSpace(nombreVariable)
                    ? null
                    : ConfiguracionLocalDatos.Obtener(nombreVariable);

                if (string.IsNullOrWhiteSpace(cadenaConexion))
                {
                    cadenaConexion = LeerCadenaConexionLocal();
                }

                if (string.IsNullOrWhiteSpace(cadenaConexion))
                {
                    throw new InvalidOperationException(
                        "Configure la variable de entorno PROG2_SQL_CONNECTION_STRING o copie .env.example a .env y complete sus valores locales.");
                }

                // Evita filtrar credenciales o claves del .env en mensajes de error.
                return new SqlConnection(PrepararCadenaConexion(cadenaConexion, LeerNombreBaseLocal()));
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "No fue posible preparar la conexión con SQL Server. Verifique la configuración local. Detalle: " + ex.Message, ex);
            }
        }

        private static string LeerCadenaConexionLocal()
        {
            string cadenaCompleta = ConfiguracionLocalDatos.Obtener("PROG2_SQL_CONNECTION_STRING");
            if (!string.IsNullOrWhiteSpace(cadenaCompleta)) return cadenaCompleta;

            string servidor = ConfiguracionLocalDatos.Obtener("DB_SERVER");
            string puerto = ConfiguracionLocalDatos.Obtener("DB_PORT");
            string baseDatos = ConfiguracionLocalDatos.Obtener("DB_NAME");
            string usuario = ConfiguracionLocalDatos.Obtener("DB_USER");
            string contrasena = ConfiguracionLocalDatos.Obtener("DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(servidor) || string.IsNullOrWhiteSpace(baseDatos)
                || string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                return null;
            }

            string servidorConPuerto = string.IsNullOrWhiteSpace(puerto) || servidor.Contains(",")
                ? servidor
                : servidor + "," + puerto;

            SqlConnectionStringBuilder constructor = new SqlConnectionStringBuilder
            {
                DataSource = servidorConPuerto,
                InitialCatalog = baseDatos,
                UserID = usuario,
                Password = contrasena,
                Encrypt = true,
                TrustServerCertificate = true
            };
            return constructor.ConnectionString;
        }

        /// <summary>
        /// Quita Command Timeout, que corresponde a SqlCommand y no es una opción de SqlConnection.
        /// Si falta la base inicial en una cadena completa, usa DB_NAME del archivo local.
        /// </summary>
        private static string PrepararCadenaConexion(string cadena, string baseDatosLocal)
        {
            string compatible = Regex.Replace(cadena, @"(?i)(?:^|;)\s*Command\s+Timeout\s*=\s*[^;]*", string.Empty)
                .Trim().Trim(';');
            SqlConnectionStringBuilder constructor;
            try
            {
                constructor = new SqlConnectionStringBuilder(compatible);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException("La cadena local de SQL Server no tiene un formato válido.", ex);
            }

            if (string.IsNullOrWhiteSpace(constructor.InitialCatalog) && !string.IsNullOrWhiteSpace(baseDatosLocal))
                constructor.InitialCatalog = baseDatosLocal;

            return constructor.ConnectionString;
        }

        private static string LeerNombreBaseLocal()
        {
            return ConfiguracionLocalDatos.Obtener("DB_NAME");
        }

        private static string BuscarArchivoLocal(string nombreArchivo)
        {
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    string rutaDatos = Path.Combine(ApplicationDeployment.CurrentDeployment.DataDirectory, nombreArchivo);
                    if (File.Exists(rutaDatos)) return rutaDatos;
                }
            }
            catch (InvalidDeploymentException)
            {
                // En ejecución local de desarrollo se continúa buscando desde la carpeta del programa.
            }

            DirectoryInfo directorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (directorio != null)
            {
                string ruta = Path.Combine(directorio.FullName, nombreArchivo);
                if (File.Exists(ruta))
                {
                    return ruta;
                }

                directorio = directorio.Parent;
            }

            return null;
        }

        /// <summary>
        /// Comprueba SQL Server y prepara el esquema/usuarios del paquete cuando hacen falta.
        /// </summary>
        public static bool ProbarConexion(out string mensaje)
        {
            try
            {
                PrepararBaseDeEntrega();

                mensaje = "Conexión con SQL Server establecida y esquema listo.";
                return true;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 18456)
                    mensaje = "SQL Server rechazó las credenciales. Verifica el usuario y la contraseña configurados.";
                else if (ex.Number == 4060)
                    mensaje = "No se encontró la base de datos seleccionada. Revisa su nombre y que esté inicializada.";
                else
                    mensaje = "No se pudo conectar con SQL Server. Comprueba que Docker esté activo y revisa el servidor y el puerto.";
                return false;
            }
            catch (ConfigurationErrorsException)
            {
                mensaje = "No se pudo leer la configuración local de conexión.";
                return false;
            }
            catch (InvalidOperationException ex)
            {
                mensaje = ex.Message;
                return false;
            }
            catch (Exception)
            {
                mensaje = "No se pudo probar la conexión con SQL Server. Revisa la configuración e inténtalo de nuevo.";
                return false;
            }
        }

        /// <summary>
        /// Prepara solo el esquema local cuando se elige SQL Server. En una base nueva,
        /// importa los hashes de usuarios incluidos en el paquete privado de entrega.
        /// Nunca escribe ni registra contraseñas en texto plano.
        /// </summary>
        private static void PrepararBaseDeEntrega()
        {
            using (SqlConnection conexionConfigurada = CrearConexion())
            {
                SqlConnectionStringBuilder constructor = new SqlConnectionStringBuilder(conexionConfigurada.ConnectionString);
                if (!string.Equals(constructor.InitialCatalog, NombreBaseEntrega, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("DB_NAME debe ser " + NombreBaseEntrega + " para inicializar la base de entrega.");

                constructor.InitialCatalog = "master";
                using (SqlConnection conexion = new SqlConnection(constructor.ConnectionString))
                {
                    conexion.Open();
                    EjecutarLotes(conexion, LeerEsquemaIncluido());

                    int usuariosActivos = ContarUsuariosActivos(conexion);
                    if (usuariosActivos == 0)
                    {
                        string rutaSeed = BuscarArchivoLocal(Path.Combine(".local-delivery", ArchivoSeedUsuarios));
                        if (rutaSeed == null)
                            throw new InvalidOperationException(
                                "La base SQL Server quedó con el esquema vacío. Falta incluir el seed privado de usuarios de Login en .local-delivery.");

                        EjecutarLotes(conexion, File.ReadAllText(rutaSeed));
                        usuariosActivos = ContarUsuariosActivos(conexion);
                        if (usuariosActivos == 0)
                            throw new InvalidOperationException("El seed no agregó usuarios activos a dbo.Tbl_Usuarios.");
                    }
                }
                constructor.Clear();
            }
        }

        private static string LeerEsquemaIncluido()
        {
            using (Stream recurso = Assembly.GetExecutingAssembly().GetManifestResourceStream(RecursoEsquema))
            {
                if (recurso == null)
                    throw new InvalidOperationException("No se encontró el esquema SQL Server integrado en el programa.");
                using (StreamReader lector = new StreamReader(recurso))
                    return lector.ReadToEnd();
            }
        }

        private static void EjecutarLotes(SqlConnection conexion, string script)
        {
            string[] lotes = Regex.Split(script, @"(?im)^\s*GO\s*(?:--.*)?$");
            foreach (string lote in lotes)
            {
                if (string.IsNullOrWhiteSpace(lote)) continue;
                using (SqlCommand comando = new SqlCommand(lote, conexion))
                {
                    comando.CommandTimeout = 60;
                    comando.ExecuteNonQuery();
                }
            }
        }

        private static int ContarUsuariosActivos(SqlConnection conexion)
        {
            const string sql = @"SELECT COUNT(*)
FROM [BD_TelefoniaAccesoriosSA].[dbo].[Tbl_Usuarios]
WHERE [Estado] = 1;";
            using (SqlCommand comando = new SqlCommand(sql, conexion))
                return Convert.ToInt32(comando.ExecuteScalar());
        }
    }
}
