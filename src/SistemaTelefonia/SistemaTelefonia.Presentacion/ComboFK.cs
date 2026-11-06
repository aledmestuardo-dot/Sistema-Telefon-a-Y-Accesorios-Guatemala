using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Prepara listas desplegables para elegir claves foráneas existentes.</summary>
    internal static class ComboFK
    {
        private sealed class Opcion
        {
            public int Codigo { get; set; }
            public string Texto { get; set; }
        }

        public static void Cargar<T>(ComboBox combo, IEnumerable<T> registros, Func<T, int> clave,
            Func<T, string> texto, bool permiteNulo = false)
        {
            // El código cero es una opción visual, nunca una FK válida. Obtener la
            // selección devuelve null y la validación decide si esa FK es obligatoria.
            List<Opcion> opciones = new List<Opcion>
            {
                new Opcion { Codigo = 0, Texto = "- Seleccione -" }
            };
            foreach (T registro in registros)
            {
                int codigoRegistro = clave(registro);
                string descripcion = texto(registro);
                opciones.Add(new Opcion
                {
                    Codigo = codigoRegistro,
                    Texto = codigoRegistro.ToString("00", CultureInfo.InvariantCulture) + " - " + descripcion
                });
            }

            combo.DataSource = null;
            combo.DisplayMember = "Texto";
            combo.ValueMember = "Codigo";
            combo.DataSource = opciones;
            combo.SelectedValue = 0;
        }

        public static int? Obtener(ComboBox combo)
        {
            int codigo;
            return combo.SelectedValue != null && int.TryParse(Convert.ToString(combo.SelectedValue), out codigo) && codigo > 0
                ? (int?)codigo : null;
        }

        public static void Seleccionar(ComboBox combo, int? codigo)
        {
            if (combo.DataSource == null) return;
            combo.SelectedIndex = -1;
            if (codigo.HasValue && codigo.Value > 0) combo.SelectedValue = codigo.Value;
            else if (combo.Items.Count > 0 && combo.Items[0] is Opcion && ((Opcion)combo.Items[0]).Codigo == 0)
                combo.SelectedValue = 0;
        }
    }
}
