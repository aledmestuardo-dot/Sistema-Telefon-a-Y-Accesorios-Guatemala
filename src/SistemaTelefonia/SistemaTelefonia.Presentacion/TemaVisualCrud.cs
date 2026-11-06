using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Aplica paletas visuales sin cambiar los eventos ni las operaciones de los formularios.</summary>
    internal static class TemaVisualCrud
    {
        private sealed class BotonConEsquinas
        {
        }

        private static readonly ConditionalWeakTable<Button, BotonConEsquinas> BotonesConRegion =
            new ConditionalWeakTable<Button, BotonConEsquinas>();

        private sealed class Paleta
        {
            public Color Fondo;
            public Color Navegacion;
            public Color Superficie;
            public Color Barra;
            public Color Texto;
            public Color TextoSecundario;
            public Color Campo;
            public Color CampoTexto;
            public Color Acento;
            public Color Borde;
            public Color Fila;
            public Color FilaAlterna;
            public Color Seleccion;
        }

        public static void Aplicar(Form formulario)
        {
            Aplicar(formulario, PreferenciasVisuales.Tema);
        }

        public static void Aplicar(Form formulario, TemaInterfaz tema)
        {
            if (formulario == null || formulario.IsDisposed) return;
            Paleta colores = ObtenerPaleta(tema);

            // Unifica también paneles y controles secundarios de formularios especiales.
            foreach (Control control in Controles<Control>(formulario))
            {
                if (DentroDelMenu(control) || EsPanelDeEstado(control.Name)) continue;

                if (control is Panel)
                    control.BackColor = colores.Superficie;
                else if (control is Label)
                {
                    bool subtitulo = control.Name.IndexOf("Subtitulo", System.StringComparison.OrdinalIgnoreCase) >= 0;
                    control.ForeColor = subtitulo ? colores.TextoSecundario : colores.Texto;
                }
                else if (control is CheckBox || control is RadioButton)
                    control.ForeColor = colores.Texto;
                else if (control is TextBoxBase || control is ComboBox || control is NumericUpDown ||
                    control is DateTimePicker)
                {
                    control.BackColor = colores.Campo;
                    control.ForeColor = colores.CampoTexto;
                }
            }

            AplicarColor(formulario, "pnlContenido", colores.Fondo);
            AplicarColor(formulario, "pnlMenu", colores.Navegacion);
            AplicarColor(formulario, "pnlItemsMenu", colores.Navegacion);
            AplicarColor(formulario, "pnlLogo", colores.Barra);
            AplicarColor(formulario, "pnlHeader", colores.Barra);
            AplicarColor(formulario, "pnlFormulario", colores.Superficie);
            AplicarColor(formulario, "pnlCatalogo", colores.Superficie);
            AplicarColor(formulario, "layoutCampos", colores.Superficie);
            AplicarColor(formulario, "pnlBusqueda", colores.Barra);
            AplicarColor(formulario, "pnlColumnas", colores.Barra);
            AplicarColor(formulario, "pnlAcciones", colores.Navegacion);

            Panel encabezado = Buscar<Panel>(formulario, "pnlHeader");
            if (encabezado != null)
            {
                foreach (Label etiqueta in Controles<Label>(encabezado))
                {
                    bool subtitulo = etiqueta.Name.IndexOf("Subtitulo", System.StringComparison.OrdinalIgnoreCase) >= 0;
                    etiqueta.ForeColor = subtitulo ? colores.TextoSecundario : colores.Texto;
                }
            }

            Panel ficha = Buscar<Panel>(formulario, "pnlFormulario");
            if (ficha != null)
            {
                foreach (Label etiqueta in Controles<Label>(ficha))
                {
                    etiqueta.ForeColor = colores.Texto;
                    if (etiqueta.Name == "lblFormularioTitulo")
                    {
                        etiqueta.ForeColor = colores.Acento;
                        etiqueta.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                    }
                }
                foreach (RadioButton opcion in Controles<RadioButton>(ficha))
                    opcion.ForeColor = colores.Texto;
            }

            Panel catalogo = Buscar<Panel>(formulario, "pnlCatalogo");
            if (catalogo != null)
            {
                foreach (Label etiqueta in Controles<Label>(catalogo))
                    etiqueta.ForeColor = colores.Texto;
                foreach (CheckBox casilla in Controles<CheckBox>(catalogo))
                {
                    casilla.ForeColor = colores.Texto;
                    casilla.BackColor = Color.Transparent;
                }
            }

            TableLayoutPanel campos = Buscar<TableLayoutPanel>(formulario, "layoutCampos");
            if (campos != null)
            {
                foreach (Control control in Controles<Control>(campos))
                {
                    if (control is TextBoxBase || control is ComboBox || control is NumericUpDown ||
                        control is DateTimePicker)
                    {
                        control.BackColor = colores.Campo;
                        control.ForeColor = colores.CampoTexto;
                    }
                }
            }

            DataGridView tabla = Controles<DataGridView>(formulario).FirstOrDefault();
            if (tabla != null)
            {
                tabla.BackgroundColor = colores.Superficie;
                tabla.BorderStyle = BorderStyle.None;
                tabla.GridColor = colores.Borde;
                tabla.EnableHeadersVisualStyles = false;
                tabla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                tabla.ColumnHeadersDefaultCellStyle.BackColor = colores.Acento;
                tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                tabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                tabla.DefaultCellStyle.BackColor = colores.Fila;
                tabla.DefaultCellStyle.ForeColor = colores.Texto;
                tabla.DefaultCellStyle.SelectionBackColor = colores.Seleccion;
                tabla.DefaultCellStyle.SelectionForeColor = colores.Texto;
                tabla.AlternatingRowsDefaultCellStyle.BackColor = colores.FilaAlterna;
                tabla.AlternatingRowsDefaultCellStyle.ForeColor = colores.Texto;
                tabla.RowHeadersDefaultCellStyle.BackColor = colores.Barra;
                tabla.RowHeadersDefaultCellStyle.ForeColor = colores.Texto;
            }

            foreach (Button boton in Controles<Button>(formulario))
            {
                if (!boton.Name.StartsWith("btn", System.StringComparison.OrdinalIgnoreCase)) continue;
                // Excluir por pertenencia al panel lateral y no por nombre del
                // contenedor: cada formulario puede organizar su navegación distinto.
                if (DentroDelMenu(boton)) continue;
                if (boton.Name == "btnVolverMenu" || boton.Name == "btnCerrarSesion")
                {
                    boton.BackColor = boton.Name == "btnCerrarSesion"
                        ? Color.FromArgb(150, 55, 60) : colores.Barra;
                    boton.ForeColor = Color.White;
                    continue;
                }

                bool peligro = boton.Name == "btnEliminar";
                boton.FlatStyle = FlatStyle.Flat;
                boton.FlatAppearance.BorderSize = 0;
                boton.BackColor = peligro ? Color.FromArgb(192, 57, 63) : colores.Acento;
                boton.ForeColor = Color.White;
                boton.FlatAppearance.MouseOverBackColor = peligro
                    ? Color.FromArgb(165, 45, 51) : Darken(colores.Acento);
            }

            if (formulario.Name == "FrmUsuarios") AplicarEstiloUsuarios(formulario, colores);
            else AplicarBotonesModeloCrud(formulario, colores);
        }

        /// <summary>Aplica el estilo CRUD a cualquier formulario que use los nombres estándar de botones.</summary>
        public static void ActualizarBotonesModeloCrud(Form formulario)
        {
            if (formulario == null || formulario.IsDisposed) return;
            AplicarBotonesModeloCrud(formulario, ObtenerPaleta(PreferenciasVisuales.Tema));
        }

        private static void AplicarEstiloUsuarios(Form formulario, Paleta colores)
        {
            PanelRedondeado encabezado = Buscar<PanelRedondeado>(formulario, "pnlHeader");
            PanelRedondeado ficha = Buscar<PanelRedondeado>(formulario, "pnlFormulario");
            PanelRedondeado catalogo = Buscar<PanelRedondeado>(formulario, "pnlCatalogo");
            DataGridViewRedondeado grilla = Buscar<DataGridViewRedondeado>(formulario, "dgvUsuarios");
            if (encabezado != null) encabezado.RadioEsquinas = 12;
            if (ficha != null) ficha.RadioEsquinas = 12;
            if (catalogo != null) catalogo.RadioEsquinas = 12;
            if (grilla != null)
            {
                grilla.RadioEsquinas = 10;
                grilla.BackgroundColor = colores.Superficie;
                grilla.BorderStyle = BorderStyle.None;
                grilla.GridColor = colores.Borde;
                grilla.EnableHeadersVisualStyles = false;
                grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                grilla.ColumnHeadersHeight = 40;
                grilla.ColumnHeadersDefaultCellStyle.BackColor = colores.Acento;
                grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                grilla.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                grilla.DefaultCellStyle.BackColor = colores.Fila;
                grilla.DefaultCellStyle.ForeColor = colores.Texto;
                grilla.DefaultCellStyle.SelectionBackColor = colores.Seleccion;
                grilla.DefaultCellStyle.SelectionForeColor = colores.Texto;
                grilla.AlternatingRowsDefaultCellStyle.BackColor = colores.FilaAlterna;
                grilla.AlternatingRowsDefaultCellStyle.ForeColor = colores.Texto;
                grilla.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
                grilla.RowTemplate.Height = 32;
                grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            }

            Label titulo = Buscar<Label>(formulario, "lblTitulo");
            Label subtitulo = Buscar<Label>(formulario, "lblSubtitulo");
            Label encabezadoFormulario = Buscar<Label>(formulario, "lblFormularioTitulo");
            Label encabezadoCatalogo = Buscar<Label>(formulario, "lblCatalogoTitulo");
            Label opcionesColumnas = Buscar<Label>(formulario, "lblColumnas");
            Label resultados = Buscar<Label>(formulario, "lblRegistros");
            if (titulo != null) titulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            if (subtitulo != null) subtitulo.Font = new Font("Segoe UI", 10F);
            if (encabezadoFormulario != null) encabezadoFormulario.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            if (encabezadoCatalogo != null) encabezadoCatalogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            if (opcionesColumnas != null) opcionesColumnas.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            if (resultados != null) resultados.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            foreach (Label etiqueta in Controles<Label>(formulario))
                if (etiqueta.Name.StartsWith("lbl", System.StringComparison.OrdinalIgnoreCase) &&
                    (etiqueta.Name.StartsWith("lblMenu", System.StringComparison.OrdinalIgnoreCase) ||
                     etiqueta.Name == "lblUsuario" || etiqueta.Name == "lblUsuarioRol"))
                    etiqueta.Font = new Font("Segoe UI", etiqueta.Name.StartsWith("lblMenu", System.StringComparison.OrdinalIgnoreCase) ? 9.5F : 8.5F,
                        etiqueta.Name == "lblUsuario" ? FontStyle.Bold : FontStyle.Regular);

            foreach (CheckBox casilla in Controles<CheckBox>(formulario))
            {
                casilla.Font = new Font("Segoe UI", 9.5F);
                casilla.ForeColor = colores.Texto;
                casilla.BackColor = Color.Transparent;
            }

            foreach (Control campo in Controles<Control>(formulario).Where(c =>
                c.Name == "txtCodigo" || c.Name == "txtNombre" || c.Name == "txtContrasenia" ||
                c.Name == "txtCorreoElectronico" || c.Name == "cmbTipoUsuario" ||
                c.Name == "dtpFechaCreacion" || c.Name == "dtpFechaUltimoAcceso"))
            {
                campo.Font = new Font("Segoe UI", 9.5F);
                campo.BackColor = campo.Enabled ? colores.Campo : Color.FromArgb(215, 222, 230);
                campo.ForeColor = campo.Enabled ? colores.CampoTexto : Color.FromArgb(104, 116, 128);
            }

            RadioButton activo = Buscar<RadioButton>(formulario, "rdoActivo");
            RadioButton inactivo = Buscar<RadioButton>(formulario, "rdoInactivo");
            if (activo != null) activo.ForeColor = Color.FromArgb(55, 180, 120);
            if (inactivo != null) inactivo.ForeColor = Color.FromArgb(220, 105, 110);

            AplicarBotonesModeloCrud(formulario, colores);
        }

        private static void AplicarBotonesModeloCrud(Form formulario, Paleta colores)
        {
            foreach (Button boton in Controles<Button>(formulario))
            {
                Color normal;
                Color hover;
                switch (boton.Name)
                {
                    case "btnNuevo": normal = Color.FromArgb(32, 142, 96); hover = Color.FromArgb(0, 203, 255); break;
                    case "btnGuardar": normal = Color.FromArgb(0, 112, 210); hover = Color.FromArgb(0, 145, 225); break;
                    case "btnEditar": normal = Color.FromArgb(0, 130, 170); hover = Color.FromArgb(0, 203, 255); break;
                    case "btnEliminar": normal = Color.FromArgb(190, 58, 66); hover = Color.FromArgb(220, 76, 82); break;
                    case "btnLimpiar": normal = Color.FromArgb(91, 108, 126); hover = Color.FromArgb(0, 145, 190); break;
                    case "btnImportarExcel": normal = Color.FromArgb(112, 83, 174); hover = Color.FromArgb(0, 170, 210); break;
                    case "btnExportarExcel": normal = Color.FromArgb(0, 132, 128); hover = Color.FromArgb(0, 180, 190); break;
                    case "btnImprimir": normal = Color.FromArgb(44, 78, 113); hover = Color.FromArgb(0, 145, 190); break;
                    case "btnBuscar": normal = colores.Acento; hover = Color.FromArgb(0, 155, 225); break;
                    case "btnPaginaAnterior":
                    case "btnPaginaSiguiente":
                    case "btnPaginaAnteriorUsuarios":
                    case "btnPaginaSiguienteUsuarios": normal = Color.FromArgb(0, 100, 160); hover = Color.FromArgb(0, 155, 210); break;
                    default:
                        if (boton.Name == "btnVolverMenu" || boton.Name == "btnCerrarSesion") continue;
                        continue;
                }

                boton.FlatStyle = FlatStyle.Flat;
                boton.FlatAppearance.BorderSize = 0;
                boton.BackColor = boton.Enabled ? normal : Color.FromArgb(85, 97, 108);
                boton.ForeColor = boton.Enabled ? Color.White : Color.FromArgb(190, 198, 205);
                boton.FlatAppearance.MouseOverBackColor = boton.Enabled ? hover : boton.BackColor;
                boton.FlatAppearance.MouseDownBackColor = boton.Enabled ? Darken(normal) : boton.BackColor;
                boton.Cursor = boton.Enabled ? Cursors.Hand : Cursors.Default;
                boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                AplicarRegionBoton(boton);
            }

            Button volver = Buscar<Button>(formulario, "btnVolverMenu");
            Button cerrarSesion = Buscar<Button>(formulario, "btnCerrarSesion");
            if (volver != null) { volver.FlatStyle = FlatStyle.Flat; volver.FlatAppearance.BorderSize = 0; }
            if (cerrarSesion != null) { cerrarSesion.FlatStyle = FlatStyle.Flat; cerrarSesion.FlatAppearance.BorderSize = 0; }
        }

        private static void AplicarRegionBoton(Button boton)
        {
            if (!BotonesConRegion.TryGetValue(boton, out BotonConEsquinas marcador))
            {
                marcador = new BotonConEsquinas();
                BotonesConRegion.Add(boton, marcador);
                boton.Resize += (s, e) => ActualizarRegionBoton((Button)s);
            }
            ActualizarRegionBoton(boton);
        }

        private static void ActualizarRegionBoton(Button boton)
        {
            if (boton == null || boton.IsDisposed || boton.Width < 2 || boton.Height < 2) return;
            Region anterior = boton.Region;
            using (GraphicsPath ruta = PanelRedondeado.CrearRuta(new Rectangle(0, 0, boton.Width, boton.Height), 8))
                boton.Region = new Region(ruta);
            if (anterior != null) anterior.Dispose();
        }

        private static Paleta ObtenerPaleta(TemaInterfaz tema)
        {
            if (tema == TemaInterfaz.Negro)
            {
                return new Paleta
                {
                    Fondo = Color.FromArgb(17, 19, 21), Navegacion = Color.FromArgb(12, 14, 16),
                    Superficie = Color.FromArgb(28, 31, 34), Barra = Color.FromArgb(38, 42, 46),
                    Texto = Color.FromArgb(232, 237, 242), TextoSecundario = Color.FromArgb(165, 176, 187),
                    Campo = Color.FromArgb(45, 49, 53), CampoTexto = Color.FromArgb(238, 242, 246),
                    Acento = Color.FromArgb(0, 119, 150),
                    Borde = Color.FromArgb(58, 65, 72), Fila = Color.FromArgb(31, 35, 39),
                    FilaAlterna = Color.FromArgb(38, 43, 48), Seleccion = Color.FromArgb(48, 75, 91)
                };
            }

            if (tema == TemaInterfaz.Claro)
            {
                return new Paleta
                {
                    Fondo = Color.FromArgb(239, 245, 251), Navegacion = Color.FromArgb(2, 25, 47),
                    Superficie = Color.White, Barra = Color.FromArgb(230, 239, 248),
                    Texto = Color.FromArgb(7, 35, 66), TextoSecundario = Color.FromArgb(72, 101, 130),
                    Campo = Color.White, CampoTexto = Color.FromArgb(7, 35, 66),
                    Acento = Color.FromArgb(0, 112, 210),
                    Borde = Color.FromArgb(215, 227, 239), Fila = Color.White,
                    FilaAlterna = Color.FromArgb(244, 248, 252), Seleccion = Color.FromArgb(215, 236, 252)
                };
            }

            return new Paleta
            {
                Fondo = Color.FromArgb(3, 22, 43), Navegacion = Color.FromArgb(2, 25, 47),
                Superficie = Color.FromArgb(7, 35, 66), Barra = Color.FromArgb(3, 43, 83),
                Texto = Color.FromArgb(235, 242, 249), TextoSecundario = Color.FromArgb(170, 190, 210),
                Campo = Color.FromArgb(225, 236, 246), CampoTexto = Color.FromArgb(7, 35, 66),
                Acento = Color.FromArgb(0, 112, 210),
                Borde = Color.FromArgb(20, 70, 110), Fila = Color.FromArgb(7, 35, 66),
                FilaAlterna = Color.FromArgb(10, 44, 78), Seleccion = Color.FromArgb(17, 75, 119)
            };
        }

        private static Color Darken(Color color)
        {
            return Color.FromArgb(color.A, (int)(color.R * 0.82), (int)(color.G * 0.82), (int)(color.B * 0.82));
        }

        private static bool DentroDelMenu(Control control)
        {
            for (Control actual = control; actual != null; actual = actual.Parent)
                if (actual.Name == "pnlMenu" || actual.Name == "pnlItemsMenu" || actual.Name == "pnlLogo")
                    return true;
            return false;
        }

        private static bool EsPanelDeEstado(string nombre)
        {
            return nombre.IndexOf("Estado", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                nombre.IndexOf("Activo", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                nombre.IndexOf("Inactivo", System.StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void AplicarColor(Control raiz, string nombre, Color color)
        {
            Control control = Buscar<Control>(raiz, nombre);
            if (control != null) control.BackColor = color;
        }

        private static T Buscar<T>(Control raiz, string nombre) where T : Control
        {
            return Controles<T>(raiz).FirstOrDefault(c => c.Name == nombre);
        }

        private static System.Collections.Generic.IEnumerable<T> Controles<T>(Control raiz) where T : Control
        {
            foreach (Control control in raiz.Controls)
            {
                T tipo = control as T;
                if (tipo != null) yield return tipo;
                foreach (T hijo in Controles<T>(control)) yield return hijo;
            }
        }
    }
}
