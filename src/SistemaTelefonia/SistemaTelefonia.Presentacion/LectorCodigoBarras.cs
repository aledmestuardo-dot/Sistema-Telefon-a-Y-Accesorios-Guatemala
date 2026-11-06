using System;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Conecta un lector tipo teclado al código numérico interno del equipo.</summary>
    internal static class LectorCodigoBarras
    {
        public static void Conectar(ComboBox campoCodigoEquipo, CrudRelacionalNegocio relacional, Action<EquiposEntidad> alEncontrar)
        {
            if (campoCodigoEquipo == null || relacional == null || alEncontrar == null) return;
            // El lector actúa como teclado: permite escribir el código y luego
            // vuelve a seleccionar el equipo existente del catálogo FK.
            campoCodigoEquipo.DropDownStyle = ComboBoxStyle.DropDown;
            campoCodigoEquipo.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                try
                {
                    int codigo;
                    if (!int.TryParse(campoCodigoEquipo.Text.Trim(), out codigo) || codigo <= 0)
                    {
                        MessageBox.Show("Lee o escribe un código numérico de equipo válido.", "Lectura de código", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        campoCodigoEquipo.Focus();
                        return;
                    }
                    EquiposEntidad equipo = relacional.Listar<EquiposEntidad>()
                        .FirstOrDefault(item => item.CodigoEquipo == codigo && item.Estado);
                    if (equipo == null)
                    {
                        MessageBox.Show("No se encontró un equipo activo con el código " + codigo + ".", "Lectura de código", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        campoCodigoEquipo.SelectAll();
                        return;
                    }
                    ComboFK.Seleccionar(campoCodigoEquipo, equipo.CodigoEquipo);
                    alEncontrar(equipo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo consultar el código leído: " + MensajesError.Resumir(ex), "Lectura de código", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }
    }
}
