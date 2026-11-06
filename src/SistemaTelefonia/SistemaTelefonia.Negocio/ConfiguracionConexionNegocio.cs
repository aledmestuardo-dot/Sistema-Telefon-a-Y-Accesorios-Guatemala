using System;
using SistemaTelefonia.Datos;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Negocio
{
    /// <summary>Coordina la selección y prueba del origen de datos elegido.</summary>
    public class ConfiguracionConexionNegocio
    {
        private readonly ConexionSupabaseDatos conexionSupabase = new ConexionSupabaseDatos();

        public OrigenBaseDatos ObtenerOrigenElegido()
        {
            return OrigenBaseDatosDatos.Leer();
        }

        public void GuardarOrigenElegido(OrigenBaseDatos origen)
        {
            OrigenBaseDatosDatos.Guardar(origen);
        }

        public bool ProbarConexion(OrigenBaseDatos origen, out string mensaje)
        {
            if (origen == OrigenBaseDatos.SqlServer)
                return ConexionSQLServer.ProbarConexion(out mensaje);
            if (origen == OrigenBaseDatos.Supabase)
                return conexionSupabase.ProbarConexion(out mensaje);

            mensaje = "Selecciona un origen de datos válido.";
            return false;
        }
    }
}
