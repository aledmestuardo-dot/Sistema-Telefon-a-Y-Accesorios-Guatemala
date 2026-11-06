using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>
    /// Aplica a los formularios CRUD las reglas adaptables del modelo visual de Pagos.
    /// </summary>
    internal static class AjustesDisenoCrud
    {
        private const int RegistrosPorPagina = 10;
        private const int AnchoMenuLateralEstandar = 195;
        private const int AnchoMenuLateralMinimo = 148;
        private static readonly ConditionalWeakTable<Form, EstadoBotonesCrud> EstadosBotones =
            new ConditionalWeakTable<Form, EstadoBotonesCrud>();

        /// <summary>Limita el tamaño inicial al área útil de la pantalla donde se abrirá el formulario.</summary>
        public static void AjustarVentana(Form formulario, Form referencia = null)
        {
            if (formulario == null || formulario.IsDisposed ||
                LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            Screen pantalla = referencia != null && !referencia.IsDisposed
                ? Screen.FromControl(referencia) : Screen.FromControl(formulario);
            Rectangle area = pantalla.WorkingArea;
            if (area.Width <= 0 || area.Height <= 0) return;

            int minimoAncho = Math.Min(formulario.MinimumSize.Width, area.Width);
            int minimoAlto = Math.Min(formulario.MinimumSize.Height, area.Height);
            formulario.MinimumSize = new Size(Math.Max(320, minimoAncho), Math.Max(240, minimoAlto));

            if (formulario.WindowState != FormWindowState.Normal) return;

            int ancho = Math.Min(formulario.Width, area.Width);
            int alto = Math.Min(formulario.Height, area.Height);
            int x = Math.Max(area.Left, Math.Min(formulario.Left, area.Right - ancho));
            int y = Math.Max(area.Top, Math.Min(formulario.Top, area.Bottom - alto));
            formulario.SetBounds(x, y, ancho, alto);
        }

        private sealed class EstadoBotonesCrud
        {
            public List<Control> Campos;
            public bool EnAlta;
            public bool Editando;
        }

        /// <summary>Aplica el estado inicial y el flujo común a formularios CRUD aún no estandarizados.</summary>
        public static void ConfigurarBotonesCrud(Form formulario)
        {
            EstadoBotonesCrud existente;
            if (formulario == null || EstadosBotones.TryGetValue(formulario, out existente))
                return;

            EstadoBotonesCrud estado = new EstadoBotonesCrud
            {
                Campos = ObtenerControles<Control>(formulario)
                    .Where(EsCampoDeCaptura)
                    .ToList()
            };
            EstadosBotones.Add(formulario, estado);
            BloquearCampos(estado);
            ActualizarBotones(formulario, estado);

            Button nuevo = BuscarControl<Button>(formulario, "btnNuevo");
            Button guardar = BuscarControl<Button>(formulario, "btnGuardar");
            Button editar = BuscarControl<Button>(formulario, "btnEditar");
            Button eliminar = BuscarControl<Button>(formulario, "btnEliminar");
            Button limpiar = BuscarControl<Button>(formulario, "btnLimpiar");
            DataGridView tabla = ObtenerControles<DataGridView>(formulario).FirstOrDefault();
            CheckBox seleccionar = BuscarControl<CheckBox>(formulario, "chkSeleccionar");

            // Se conecta después de los eventos existentes: Nuevo primero limpia, luego habilita.
            if (nuevo != null)
                nuevo.Click += (s, e) => IniciarAlta(formulario, estado);
            if (limpiar != null)
                limpiar.Click += (s, e) => Restablecer(formulario, estado);
            if (tabla != null)
                tabla.SelectionChanged += (s, e) => ActualizarBotones(formulario, estado);
            if (seleccionar != null)
                seleccionar.CheckedChanged += (s, e) => ActualizarBotones(formulario, estado);

            // Tras guardar o editar, los formularios que limpiaron la ficha vuelven al estado inicial.
            EventHandler revisarEstado = (s, e) =>
            {
                if (FormularioVacio(estado) && (tabla == null || tabla.CurrentRow == null ||
                    (seleccionar != null && !seleccionar.Checked)))
                    Restablecer(formulario, estado);
                else
                    ActualizarBotones(formulario, estado);
            };
            if (guardar != null) guardar.Click += revisarEstado;
            if (editar != null) editar.Click += revisarEstado;
            if (eliminar != null) eliminar.Click += revisarEstado;
        }

        /// <summary>En el primer clic de Editar habilita campos; el segundo aplica el cambio.</summary>
        public static bool PermitirEdicion(Form formulario)
        {
            EstadoBotonesCrud estado;
            if (formulario == null || !EstadosBotones.TryGetValue(formulario, out estado))
                return true;

            if (!HaySeleccion(formulario))
                return true; // El evento existente muestra el aviso de que se debe elegir una fila.

            if (!estado.Editando)
            {
                estado.EnAlta = false;
                estado.Editando = true;
                HabilitarCampos(estado);
                ActualizarBotones(formulario, estado);
                EnfocarPrimerCampo(estado);
                MessageBox.Show("Modifica los campos y pulsa Editar nuevamente para aplicar los cambios.",
                    "Editar registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private static void IniciarAlta(Form formulario, EstadoBotonesCrud estado)
        {
            estado.EnAlta = true;
            estado.Editando = false;
            HabilitarCampos(estado);
            ActualizarBotones(formulario, estado);
            EnfocarPrimerCampo(estado);
        }

        private static void Restablecer(Form formulario, EstadoBotonesCrud estado)
        {
            estado.EnAlta = false;
            estado.Editando = false;
            BloquearCampos(estado);
            ActualizarBotones(formulario, estado);
        }

        private static void ActualizarBotones(Form formulario, EstadoBotonesCrud estado)
        {
            bool seleccion = HaySeleccion(formulario);
            Button nuevo = BuscarControl<Button>(formulario, "btnNuevo");
            Button guardar = BuscarControl<Button>(formulario, "btnGuardar");
            Button editar = BuscarControl<Button>(formulario, "btnEditar");
            Button eliminar = BuscarControl<Button>(formulario, "btnEliminar");

            if (nuevo != null) nuevo.Enabled = !estado.EnAlta && !estado.Editando;
            if (guardar != null) guardar.Enabled = estado.EnAlta;
            if (editar != null) editar.Enabled = !estado.EnAlta && (estado.Editando || seleccion);
            if (eliminar != null) eliminar.Enabled = !estado.EnAlta && !estado.Editando && seleccion;
            TemaVisualCrud.ActualizarBotonesModeloCrud(formulario);
        }

        private static bool HaySeleccion(Form formulario)
        {
            CheckBox seleccionar = BuscarControl<CheckBox>(formulario, "chkSeleccionar");
            DataGridView tabla = ObtenerControles<DataGridView>(formulario).FirstOrDefault();
            return seleccionar != null && seleccionar.Checked && tabla != null && tabla.CurrentRow != null;
        }

        private static void BloquearCampos(EstadoBotonesCrud estado)
        {
            foreach (Control campo in estado.Campos) campo.Enabled = false;
        }

        private static void HabilitarCampos(EstadoBotonesCrud estado)
        {
            foreach (Control campo in estado.Campos) campo.Enabled = true;
        }

        private static void EnfocarPrimerCampo(EstadoBotonesCrud estado)
        {
            Control campo = estado.Campos.FirstOrDefault(c => c.Enabled && c.Visible && c.TabStop);
            if (campo != null) campo.Focus();
        }

        private static bool FormularioVacio(EstadoBotonesCrud estado)
        {
            foreach (Control campo in estado.Campos)
            {
                TextBoxBase texto = campo as TextBoxBase;
                if (texto != null && !string.IsNullOrWhiteSpace(texto.Text)) return false;
                NumericUpDown numero = campo as NumericUpDown;
                if (numero != null && numero.Value != numero.Minimum) return false;
                ComboBox lista = campo as ComboBox;
                if (lista != null && lista.SelectedIndex >= 0) return false;
            }
            return true;
        }

        private static bool EsCampoDeCaptura(Control control)
        {
            if (control.Name == "txtCodigo" || control.Name.StartsWith("txtBuscar", StringComparison.OrdinalIgnoreCase))
                return false;
            return control is TextBoxBase || control is NumericUpDown || control is ComboBox ||
                   control is DateTimePicker || control is RadioButton;
        }

        private static T BuscarControl<T>(Control raiz, string nombre) where T : Control
        {
            return ObtenerControles<T>(raiz).FirstOrDefault(c =>
                string.Equals(c.Name, nombre, StringComparison.OrdinalIgnoreCase));
        }

        private static IEnumerable<T> ObtenerControles<T>(Control raiz) where T : Control
        {
            foreach (Control control in raiz.Controls)
            {
                T tipo = control as T;
                if (tipo != null) yield return tipo;
                foreach (T hijo in ObtenerControles<T>(control)) yield return hijo;
            }
        }

        public static void CentrarAcciones(Panel franja, TableLayoutPanel botones)
        {
            botones.Dock = DockStyle.None;
            botones.Anchor = AnchorStyles.Top;
            Button[] acciones = botones.Controls.OfType<Button>().ToArray();
            botones.ColumnCount = acciones.Length;
            botones.RowCount = 1;
            botones.ColumnStyles.Clear();
            float porcentaje = acciones.Length == 0 ? 100F : 100F / acciones.Length;
            for (int i = 0; i < acciones.Length; i++)
                botones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, porcentaje));
            botones.RowStyles.Clear();
            botones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            botones.Height = 54;

            foreach (Button boton in acciones)
            {
                boton.Dock = DockStyle.Fill;
                boton.Margin = new Padding(3);
            }

            Form propietario = botones.FindForm();
            if (propietario != null && !botones.Controls.ContainsKey("btnImportarExcel"))
            {
                DataGridView tablaListado = ObtenerControles<DataGridView>(propietario).FirstOrDefault();
                Button importar = new Button { Name = "btnImportarExcel", Text = "Importar Excel", BackColor = Color.FromArgb(0, 112, 150), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                importar.Click += (s, e) =>
                {
                    FrmFacturacion formularioFacturacion = propietario as FrmFacturacion;
                    if (formularioFacturacion != null)
                    {
                        formularioFacturacion.ImportarFacturasExcel();
                        return;
                    }
                    DataTable datos; string nombreArchivo;
                    try { datos = ArchivosExcel.ImportarTabla(out nombreArchivo); }
                    catch (Exception ex) { MessageBox.Show("No fue posible leer el archivo: " + MensajesError.Resumir(ex), "Importación", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (datos == null) return;
                    using (Form vista = new Form { Text = "Vista previa: " + nombreArchivo + " · " + datos.Rows.Count + " filas", Size = new Size(900, 600), StartPosition = FormStartPosition.CenterParent })
                    using (TableLayoutPanel raiz = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1 })
                    using (DataGridView tabla = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells, DataSource = datos })
                    {
                        raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
                        Button confirmar = new Button { Text = "Importar filas", Dock = DockStyle.Right, Width = 150, BackColor = Color.FromArgb(0, 125, 100), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                        confirmar.Click += (sender, args) => ImportarFilasExcel(datos, tablaListado, propietario, vista);
                        raiz.Controls.Add(tabla, 0, 0); raiz.Controls.Add(confirmar, 0, 1); vista.Controls.Add(raiz);
                        vista.ShowDialog(propietario);
                    }
                };
                Button exportar = new Button { Name = "btnExportarExcel", Text = "Exportar Excel", BackColor = Color.FromArgb(0, 125, 100), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                exportar.Click += (s, e) =>
                {
                    if (tablaListado == null) return;
                    string ruta;
                    try { ArchivosExcel.ExportarTabla(tablaListado, propietario.Text, out ruta); if (!string.IsNullOrEmpty(ruta)) MessageBox.Show("Archivo exportado para Excel:", "Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    catch (Exception ex) { MessageBox.Show("No fue posible exportar el listado: " + MensajesError.Resumir(ex), "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                };
                Button imprimir = new Button { Name = "btnImprimir", Text = "Imprimir", BackColor = Color.FromArgb(75, 90, 105), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                imprimir.Click += (s, e) => ImprimirTabla(propietario);
                botones.Controls.Add(importar, 5, 0);
                botones.Controls.Add(exportar, 6, 0);
                botones.Controls.Add(imprimir, 7, 0);
            }

            // Todos los formularios CRUD comparten la misma fila de ocho acciones.
            bool usaModeloCrudSimple = true;
            if (usaModeloCrudSimple)
            {
                botones.ColumnCount = 8;
                botones.ColumnStyles.Clear();
                for (int i = 0; i < botones.ColumnCount; i++)
                    botones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
                foreach (Button boton in botones.Controls.OfType<Button>())
                {
                    boton.Dock = DockStyle.Fill;
                    boton.Margin = new Padding(4, 3, 4, 3);
                }
            }

            Action centrar = () =>
            {
                int anchoMaximo = usaModeloCrudSimple ? 1264 : 1120;
                int ancho = Math.Min(anchoMaximo, franja.ClientSize.Width);
                int alto = Math.Min(54, franja.ClientSize.Height);
                float fuente = franja.ClientSize.Width < 950 ? 8F : 9F;
                foreach (Button boton in botones.Controls.OfType<Button>())
                    if (Math.Abs(boton.Font.Size - fuente) > 0.1F)
                        boton.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                botones.SetBounds(
                    Math.Max(0, (franja.ClientSize.Width - ancho) / 2),
                    Math.Max(0, (franja.ClientSize.Height - alto) / 2),
                    ancho,
                    alto);
            };

            franja.SizeChanged += (s, e) => centrar();
            centrar();
        }

        /// <summary>Aplica el encabezado y las franjas de búsqueda/columnas del modelo visual de Pagos.</summary>
        public static void AplicarCatalogoModeloPagos(Form formulario, Panel encabezado, Label titulo,
            Label subtitulo, Panel busqueda, TextBox textoBusqueda, Button botonBuscar,
            Panel columnas, Label tituloColumnas, FlowLayoutPanel selector, CheckBox seleccionar)
        {
            if (formulario == null || encabezado == null || titulo == null || subtitulo == null ||
                busqueda == null || textoBusqueda == null || botonBuscar == null || columnas == null ||
                tituloColumnas == null || selector == null || seleccionar == null) return;

            encabezado.Height = 108;
            encabezado.Dock = DockStyle.None;
            encabezado.Padding = new Padding(22, 8, 15, 8);
            encabezado.Controls.Remove(titulo);
            encabezado.Controls.Remove(subtitulo);
            TableLayoutPanel layoutEncabezado = new TableLayoutPanel
            {
                Name = "layoutEncabezadoModeloCrud", Dock = DockStyle.Fill, ColumnCount = 1,
                RowCount = 2, Margin = Padding.Empty, Padding = Padding.Empty,
                BackColor = encabezado.BackColor, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezado.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutEncabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            titulo.Dock = DockStyle.Fill;
            titulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            titulo.Margin = Padding.Empty;
            titulo.Padding = Padding.Empty;
            titulo.TextAlign = ContentAlignment.MiddleLeft;
            subtitulo.Dock = DockStyle.Fill;
            subtitulo.Font = new Font("Segoe UI", 10F);
            subtitulo.Margin = Padding.Empty;
            subtitulo.Padding = Padding.Empty;
            subtitulo.TextAlign = ContentAlignment.MiddleLeft;
            layoutEncabezado.Controls.Add(titulo, 0, 0);
            layoutEncabezado.Controls.Add(subtitulo, 0, 1);
            encabezado.Controls.Add(layoutEncabezado);

            Panel contenido = formulario.Controls.Find("pnlContenido", true).OfType<Panel>().FirstOrDefault();
            Panel menu = formulario.Controls.Find("pnlMenu", true).OfType<Panel>().FirstOrDefault();
            if (contenido != null)
            {
                contenido.Dock = DockStyle.None;
                contenido.Padding = new Padding(14);
                contenido.AutoScroll = true;
                contenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            encabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Action acomodarVentana = () =>
            {
                int ancho = Math.Min(195, Math.Max(148, (int)(formulario.ClientSize.Width * .18)));
                if (menu != null) menu.Width = ancho;
                encabezado.SetBounds(ancho, 0, Math.Max(0, formulario.ClientSize.Width - ancho), 108);
                if (contenido != null)
                    contenido.SetBounds(ancho, 108, Math.Max(0, formulario.ClientSize.Width - ancho),
                        Math.Max(0, formulario.ClientSize.Height - 108));
            };
            formulario.SizeChanged += (s, e) => acomodarVentana();
            acomodarVentana();

            FormatearTextoBusqueda(formulario, busqueda, textoBusqueda, botonBuscar);
            FormatearOpcionesColumnas(formulario, columnas, tituloColumnas, selector, seleccionar);
        }

        private static void FormatearTextoBusqueda(Form formulario, Panel busqueda, TextBox texto, Button buscar)
        {
            ComboBox estado = formulario.Controls.Find("cboFiltroEstadoComun", true).OfType<ComboBox>().FirstOrDefault() ??
                formulario.Controls.Find("cboFiltroEstado", true).OfType<ComboBox>().FirstOrDefault();
            if (estado != null && estado.Parent != null) estado.Parent.Controls.Remove(estado);
            if (texto.Parent != null) texto.Parent.Controls.Remove(texto);
            if (buscar.Parent != null) buscar.Parent.Controls.Remove(buscar);
            busqueda.Controls.Clear();
            busqueda.Height = 54;

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Name = "layoutBusquedaModeloCrud", Dock = DockStyle.Fill, ColumnCount = estado == null ? 2 : 3,
                RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = busqueda.BackColor
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            if (estado != null)
            {
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
                estado.Dock = DockStyle.Fill;
                estado.Font = new Font("Segoe UI", 9.5F);
                estado.Margin = new Padding(0, 0, 8, 0);
                layout.Controls.Add(estado, 0, 0);
            }
            else layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            if (estado != null) layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Math.Max(96F, buscar.Width)));
            texto.Dock = DockStyle.Fill;
            buscar.Dock = DockStyle.Fill;
            texto.Margin = new Padding(0, 0, 8, 0);
            buscar.Margin = Padding.Empty;
            layout.Controls.Add(texto, estado == null ? 0 : 1, 0);
            layout.Controls.Add(buscar, estado == null ? 1 : 2, 0);
            busqueda.Controls.Add(layout);
        }

        private static void FormatearOpcionesColumnas(Form formulario, Panel columnas, Label titulo,
            FlowLayoutPanel selector, CheckBox seleccionar)
        {
            CheckBox activar = formulario.Controls.Find("chkActivarFiltros", true).OfType<CheckBox>().FirstOrDefault() ??
                formulario.Controls.Find("chkActivarTodosFiltros", true).OfType<CheckBox>().FirstOrDefault();
            if (activar == null)
            {
                activar = new CheckBox
                {
                    Name = "chkActivarFiltros", Text = "Activar todos los filtros", AutoSize = true,
                    BackColor = Color.Transparent, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F),
                    Margin = new Padding(5, 3, 9, 3), UseVisualStyleBackColor = false
                };
            }

            if (seleccionar.Parent != null) seleccionar.Parent.Controls.Remove(seleccionar);
            if (activar.Parent != null) activar.Parent.Controls.Remove(activar);
            selector.Controls.Remove(seleccionar);
            if (selector.Parent != null) selector.Parent.Controls.Remove(selector);
            if (titulo.Parent != null) titulo.Parent.Controls.Remove(titulo);
            columnas.Controls.Clear();
            columnas.Height = 76;
            seleccionar.Text = "Seleccionar registro";
            seleccionar.AutoSize = true;
            seleccionar.Font = new Font("Segoe UI", 9.5F);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Name = "layoutOpcionesColumnasModeloCrud", Dock = DockStyle.Fill, ColumnCount = 3,
                RowCount = 2, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = columnas.BackColor
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            titulo.Dock = DockStyle.Fill;
            titulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titulo.TextAlign = ContentAlignment.MiddleLeft;
            activar.Anchor = AnchorStyles.Left;
            seleccionar.Anchor = AnchorStyles.Right;
            selector.Dock = DockStyle.Fill;
            selector.WrapContents = false;
            selector.AutoScroll = true;
            selector.FlowDirection = FlowDirection.LeftToRight;
            foreach (CheckBox filtro in selector.Controls.OfType<CheckBox>())
            {
                filtro.AutoSize = true;
                filtro.Font = new Font("Segoe UI", 9F);
                filtro.Margin = new Padding(2, 3, 5, 3);
            }
            layout.Controls.Add(titulo, 0, 0);
            layout.SetColumnSpan(titulo, 3);
            layout.Controls.Add(activar, 0, 1);
            layout.Controls.Add(selector, 1, 1);
            layout.Controls.Add(seleccionar, 2, 1);
            columnas.Controls.Add(layout);

            bool sincronizando = false;
            activar.Checked = selector.Controls.OfType<CheckBox>().All(c => c.Checked);
            activar.CheckedChanged += (s, e) =>
            {
                if (sincronizando) return;
                sincronizando = true;
                try { foreach (CheckBox filtro in selector.Controls.OfType<CheckBox>()) filtro.Checked = activar.Checked; }
                finally { sincronizando = false; }
            };
            foreach (CheckBox filtro in selector.Controls.OfType<CheckBox>())
                filtro.CheckedChanged += (s, e) =>
                {
                    if (sincronizando) return;
                    sincronizando = true;
                    try { activar.Checked = selector.Controls.OfType<CheckBox>().All(c => c.Checked); }
                    finally { sincronizando = false; }
                };
            columnas.SizeChanged += (s, e) =>
            {
                int anchoActivar = TextRenderer.MeasureText(activar.Text, activar.Font).Width + 42;
                int anchoSeleccionar = TextRenderer.MeasureText(seleccionar.Text, seleccionar.Font).Width + 42;
                if (layout.ColumnStyles.Count == 3)
                {
                    layout.ColumnStyles[0].Width = anchoActivar;
                    layout.ColumnStyles[2].Width = anchoSeleccionar;
                }
            };
        }

        private static void ImportarFilasExcel(DataTable datos, DataGridView tablaListado, Form propietario, Form vista)
        {
            object origen = tablaListado == null ? null : tablaListado.DataSource;
            BindingSource binding = origen as BindingSource;
            if (binding != null) origen = binding.DataSource;
            Type tipoEntidad = ObtenerTipoFila(origen);
            if (tipoEntidad == null)
            {
                MessageBox.Show("No se pudo determinar el tipo de registro de esta pantalla.", "Importar Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tipoEntidad == typeof(ComprasEntidad) || tipoEntidad == typeof(VentasEntidad) || tipoEntidad == typeof(FacturacionEntidad))
            {
                MessageBox.Show("Esta pantalla registra encabezados de transacciones. Para evitar crear compras, ventas o facturas sin sus detalles relacionados, el archivo solo puede revisarse aquí.", "Importación de transacciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CrudRelacionalNegocio negocio = new CrudRelacionalNegocio();
            MethodInfo agregar = typeof(CrudRelacionalNegocio).GetMethod("Agregar").MakeGenericMethod(tipoEntidad);
            int correctos = 0;
            List<string> errores = new List<string>();
            for (int i = 0; i < datos.Rows.Count; i++)
            {
                try
                {
                    object entidad = ArchivosExcel.CrearEntidad(datos.Rows[i], tipoEntidad);
                    agregar.Invoke(negocio, new[] { entidad });
                    correctos++;
                }
                catch (Exception ex)
                {
                    Exception causa = ex is TargetInvocationException && ex.InnerException != null ? ex.InnerException : ex;
                    if (errores.Count < 5) errores.Add("Fila " + (i + 2) + ": " + MensajesError.Resumir(causa));
                }
            }
            string detalle = errores.Count == 0 ? string.Empty : "\nErrores (primeros 5):\n" + string.Join("\n", errores);
            MessageBox.Show("Filas importadas: " + correctos + " de " + datos.Rows.Count + "." + detalle,
                "Resultado de importación", MessageBoxButtons.OK, errores.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correctos > 0)
            {
                vista.Close();
                Button limpiar = ObtenerControles<Button>(propietario).FirstOrDefault(b => b.Name == "btnLimpiar");
                if (limpiar != null) limpiar.PerformClick();
            }
        }

        private static void ImprimirTabla(Form formulario)
        {
            DataGridView tabla = ObtenerControles<DataGridView>(formulario).FirstOrDefault();
            if (tabla == null) return;
            using (PrintPreviewDialog vista = new PrintPreviewDialog())
            using (System.Drawing.Printing.PrintDocument documento = new System.Drawing.Printing.PrintDocument())
            {
                documento.PrintPage += (s, e) =>
                {
                    float y = 40; e.Graphics.DrawString(formulario.Text, formulario.Font, Brushes.Black, 40, y); y += 30;
                    foreach (DataGridViewColumn columna in tabla.Columns)
                        if (columna.Visible) { e.Graphics.DrawString(columna.HeaderText, tabla.Font, Brushes.Black, 40 + columna.DisplayIndex * 110, y); }
                    y += 24;
                    foreach (DataGridViewRow fila in tabla.Rows)
                    {
                        if (fila.IsNewRow || y > e.MarginBounds.Bottom) continue;
                        foreach (DataGridViewColumn columna in tabla.Columns)
                            if (columna.Visible) e.Graphics.DrawString(Convert.ToString(fila.Cells[columna.Index].FormattedValue), tabla.Font, Brushes.Black, 40 + columna.DisplayIndex * 110, y);
                        y += 20;
                    }
                };
                vista.Document = documento; vista.ShowDialog(formulario);
            }
        }

        /// <summary>
        /// Da a todas las tablas el ancho de columna, el alto de encabezado y el
        /// tamaño de fila usados por el formulario de Pagos.
        /// </summary>
        public static void ConfigurarTabla(DataGridView tabla)
        {
            if (tabla == null)
                return;

            // Los formularios simples ya tienen su propio flujo validado y conservan esa lógica.
            Form formulario = tabla.FindForm();
            if (formulario != null)
            {
                TemaVisualCrud.Aplicar(formulario);
                // Ajusta tamaño antes de mostrar para respetar el área útil de cada monitor.
                formulario.Load += (s, e) => AjustarVentana(formulario);

                if (formulario.Name != "FrmPlanes" && formulario.Name != "FrmProveedores" &&
                    formulario.Name != "FrmSucursales" && formulario.Name != "FrmUsuarios")
                {
                    // Espera a que cada pantalla conecte sus eventos propios antes de observar su resultado.
                    formulario.Shown += (s, e) => ConfigurarBotonesCrud(formulario);
                }
            }

            ConfigurarMenuLateral(tabla);
            ComboBox filtroEstado = ConfigurarFiltroEstado(tabla);

            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tabla.ColumnHeadersHeight = 34;
            tabla.RowTemplate.Height = 29;
            tabla.RowHeadersVisible = false;
            // Los registros se recorren por páginas de diez; la barra vertical
            // agrega flechas del sistema y altera la altura visible de la grilla.
            tabla.ScrollBars = ScrollBars.Horizontal;

            Action ajustarColumnas = () =>
            {
                // ColumnAdded puede disparar este ajuste mientras DataGridView aún
                // modifica Columns durante el enlace de datos. Iterar una copia evita
                // invalidar el enumerador en ese momento.
                foreach (DataGridViewColumn columna in tabla.Columns.Cast<DataGridViewColumn>().ToList())
                {
                    columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    columna.FillWeight = 100F;
                      columna.MinimumWidth = formulario != null &&
                          (formulario.Name == "FrmUsuarios" || formulario.Name == "FrmClientes") ? 72 : 110;
                    columna.DividerWidth = 1;
                }
            };

            tabla.DataBindingComplete += (s, e) => ajustarColumnas();
            tabla.ColumnAdded += (s, e) => ajustarColumnas();
            ajustarColumnas();

            // Clientes ya administra su paginación explícitamente.
            if (filtroEstado != null && formulario != null)
                TemaVisualCrud.Aplicar(formulario);

            bool paginacionPropia = formulario != null && formulario.Name == "FrmUsuarios";
            if (!paginacionPropia && !string.Equals(tabla.Name, "dgvClientes", StringComparison.OrdinalIgnoreCase))
            {
                ConfigurarBarraPaginacion(tabla, filtroEstado);
                AjustarBarraPaginacion(tabla);
            }

            if (filtroEstado == null && formulario != null)
            {
                DataGridViewBindingCompleteEventHandler reintentarFiltro = null;
                reintentarFiltro = (s, e) =>
                {
                    if (!tabla.IsHandleCreated || tabla.DataSource == null) return;
                    ComboBox agregado = ConfigurarFiltroEstado(tabla);
                    if (agregado != null)
                    {
                        // Clientes administra la paginación dentro de su propio layout;
                        // crear aquí otra barra duplica botones y reduce el área del catálogo.
                        if (!string.Equals(tabla.Name, "dgvClientes", StringComparison.OrdinalIgnoreCase))
                        {
                            ConfigurarBarraPaginacion(tabla, agregado);
                            AjustarBarraPaginacion(tabla);
                        }
                        tabla.DataBindingComplete -= reintentarFiltro;
                    }
                };
                tabla.DataBindingComplete += reintentarFiltro;
            }
        }

        private static ComboBox ConfigurarFiltroEstado(DataGridView tabla)
        {
            Form formulario = tabla.FindForm();
            if (formulario == null || formulario.Controls.Find("cboFiltroEstadoComun", true).Length > 0 ||
                formulario.Controls.Find("cboFiltroEstado", true).Length > 0)
                return null;

            BindingSource origen = tabla.DataSource as BindingSource;
            object fuente = origen == null ? tabla.DataSource : origen.DataSource;
            Type tipoFila = ObtenerTipoFila(fuente);
            if (tipoFila == null && origen != null && origen.Count > 0)
                tipoFila = origen[0].GetType();
            PropertyInfo estado = tipoFila == null ? null : tipoFila.GetProperty("Estado");
            if (estado == null || (estado.PropertyType != typeof(bool) && estado.PropertyType != typeof(bool?)))
                return null;

            Panel busqueda = formulario.Controls.Find("pnlBusqueda", true).OfType<Panel>().FirstOrDefault();
            TextBox texto = formulario.Controls.Find("txtBuscar", true).OfType<TextBox>().FirstOrDefault();
            Button buscar = formulario.Controls.Find("btnBuscar", true).OfType<Button>().FirstOrDefault();
            if (texto == null || buscar == null)
                return null;

            if (busqueda == null)
            {
                Control host = texto.Parent;
                if (host == null || host != buscar.Parent) return null;
                Panel envoltura = new Panel { Name = "pnlBusquedaEstadoComun", Dock = DockStyle.Top, Height = Math.Max(38, host.Height) };
                int indiceTexto = host.Controls.GetChildIndex(texto);
                host.Controls.Remove(texto); host.Controls.Remove(buscar);
                envoltura.Controls.Add(texto); envoltura.Controls.Add(buscar);
                host.Controls.Add(envoltura);
                host.Controls.SetChildIndex(envoltura, Math.Min(indiceTexto, host.Controls.Count - 1));
                busqueda = envoltura;
            }

            Panel panelFiltro = new Panel { Name = "pnlFiltroEstadoComun", Dock = DockStyle.Right, Width = 250 };
            ComboBox combo = new ComboBox
            {
                Name = "cboFiltroEstadoComun",
                AccessibleName = "Filtrar registros por estado",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };
            combo.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            combo.SelectedIndex = 0;

            busqueda.Controls.Remove(buscar);
            panelFiltro.Controls.Add(combo);
            panelFiltro.Controls.Add(buscar);
            buscar.Dock = DockStyle.Right;
            buscar.Width = 96;
            busqueda.Controls.Add(panelFiltro);

            Action acomodar = () =>
            {
                panelFiltro.Width = Math.Min(250, Math.Max(205, busqueda.ClientSize.Width - 160));
                buscar.Height = busqueda.ClientSize.Height;
                texto.Location = Point.Empty;
                texto.Height = Math.Max(30, busqueda.ClientSize.Height - 4);
                texto.Width = Math.Max(100, busqueda.ClientSize.Width - panelFiltro.Width);
                texto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            };
            busqueda.SizeChanged += (s, e) => acomodar();
            acomodar();
            return combo;
        }

        private static void ConfigurarBarraPaginacion(DataGridView tabla, ComboBox filtroEstado)
        {
            Form formulario = tabla.FindForm();
            Control catalogo = tabla.Parent;
            Label lblRegistros = null;
            while (catalogo != null && lblRegistros == null)
            {
                lblRegistros = catalogo.Controls.Find("lblRegistros", true)
                    .OfType<Label>().FirstOrDefault();
                if (lblRegistros == null)
                    catalogo = catalogo.Parent;
            }

            if (catalogo == null || lblRegistros == null ||
                catalogo.Controls.Find("pnlPaginacion", true).Length > 0)
                return;

            BindingSource origen = tabla.DataSource as BindingSource;
            if (origen == null)
                return;

            Panel barra = new Panel
            {
                Name = "pnlPaginacion",
                Height = 36,
                Dock = DockStyle.Bottom,
                BackColor = catalogo.BackColor
            };

            catalogo.Controls.Remove(lblRegistros);
            lblRegistros.Dock = DockStyle.None;
            lblRegistros.TextAlign = ContentAlignment.MiddleLeft;
            barra.Controls.Add(lblRegistros);

            Button anterior = CrearBotonPaginacion("< Anterior", "btnPaginaAnterior", tabla);
            Button siguiente = CrearBotonPaginacion("Siguiente >", "btnPaginaSiguiente", tabla);
            Label pagina = new Label
            {
                Name = "lblPagina",
                Size = new Size(100, 26),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = lblRegistros.ForeColor,
                Font = lblRegistros.Font
            };

            barra.Controls.Add(anterior);
            barra.Controls.Add(pagina);
            barra.Controls.Add(siguiente);
            catalogo.Controls.Add(barra);
            barra.BringToFront();

            var todos = new System.Collections.Generic.List<object>();
            int paginaActual = 0;
            bool actualizando = false;
            PropertyInfo propiedadEstado = ObtenerPropiedadEstado(origen.DataSource);

            Func<System.Collections.Generic.List<object>> obtenerFiltrados = () =>
            {
                if (filtroEstado == null || filtroEstado.SelectedIndex == 0 || propiedadEstado == null)
                    return new System.Collections.Generic.List<object>(todos);
                bool activos = filtroEstado.SelectedIndex == 1;
                return todos.Where(fila => Convert.ToBoolean(propiedadEstado.GetValue(fila, null) ?? false) == activos).ToList();
            };

            Action acomodar = () =>
            {
                const int margen = 6;
                int y = Math.Max(0, (barra.ClientSize.Height - 26) / 2);
                int siguienteX = barra.ClientSize.Width - margen - siguiente.Width;
                int paginaX = siguienteX - margen - pagina.Width;
                int anteriorX = paginaX - margen - anterior.Width;
                anterior.Location = new Point(anteriorX, y);
                pagina.Location = new Point(paginaX, y);
                siguiente.Location = new Point(siguienteX, y);
                lblRegistros.Location = new Point(margen, y);
                lblRegistros.Size = new Size(Math.Max(0, anteriorX - margen * 2), 26);
            };

            Action mostrar = () =>
            {
                System.Collections.Generic.List<object> registros = obtenerFiltrados();
                int paginas = (int)Math.Ceiling(registros.Count / (double)RegistrosPorPagina);
                if (paginas == 0)
                    paginaActual = 0;
                else if (paginaActual >= paginas)
                    paginaActual = paginas - 1;

                object origenActual = origen.DataSource;
                Type tipoFila = registros.Count > 0 ? registros[0].GetType() : ObtenerTipoFila(origenActual);
                if (tipoFila != null)
                {
                    int inicio = paginaActual * RegistrosPorPagina;
                    int cantidad = Math.Min(RegistrosPorPagina, Math.Max(0, registros.Count - inicio));
                    Array filas = Array.CreateInstance(tipoFila, cantidad);
                    for (int i = 0; i < cantidad; i++)
                        filas.SetValue(registros[inicio + i], i);

                    actualizando = true;
                    origen.DataSource = filas;
                    actualizando = false;
                }

                pagina.Text = "Página " + (paginas == 0 ? 0 : paginaActual + 1) + " de " + paginas;
                anterior.Enabled = paginaActual > 0;
                siguiente.Enabled = paginaActual + 1 < paginas;
                TemaVisualCrud.ActualizarBotonesModeloCrud(formulario);
                if (filtroEstado != null)
                {
                    int activos = propiedadEstado == null ? todos.Count : todos.Count(fila =>
                        Convert.ToBoolean(propiedadEstado.GetValue(fila, null) ?? false));
                    int inactivos = todos.Count - activos;
                    lblRegistros.Text = "Resultados: " + todos.Count + "  |  Activos: " + activos + "  |  Inactivos: " + inactivos;
                }
            };

            origen.DataSourceChanged += (s, e) =>
            {
                if (actualizando)
                    return;

                todos.Clear();
                foreach (object fila in origen.List)
                    todos.Add(fila);
                paginaActual = 0;
                mostrar();
            };

            anterior.Click += (s, e) =>
            {
                if (paginaActual > 0)
                {
                    paginaActual--;
                    mostrar();
                }
            };
            siguiente.Click += (s, e) =>
            {
                int paginas = (int)Math.Ceiling(obtenerFiltrados().Count / (double)RegistrosPorPagina);
                if (paginaActual + 1 < paginas)
                {
                    paginaActual++;
                    mostrar();
                }
            };
            if (filtroEstado != null)
                filtroEstado.SelectedIndexChanged += (s, e) => { paginaActual = 0; mostrar(); };
            barra.Resize += (s, e) => acomodar();

            // También pagina la lista que ya estaba enlazada antes de configurar la tabla.
            foreach (object fila in origen.List)
                todos.Add(fila);
            pagina.Text = "Página 1 de 1";
            acomodar();
            if (todos.Count > RegistrosPorPagina)
                mostrar();
        }

        /// <summary>
        /// Mejora el espacio del pie de paginación en formularios seleccionados,
        /// sin cambiar el estilo usado por los demás formularios.
        /// </summary>
        public static void AjustarBarraPaginacion(DataGridView tabla)
        {
            if (tabla == null)
                return;

            Panel barra = tabla.FindForm()?.Controls.Find("pnlPaginacion", true)
                .OfType<Panel>().FirstOrDefault();
            if (barra == null)
                return;

            Button anterior = barra.Controls.Find("btnPaginaAnterior", false).OfType<Button>().FirstOrDefault();
            Button siguiente = barra.Controls.Find("btnPaginaSiguiente", false).OfType<Button>().FirstOrDefault();
            Label pagina = barra.Controls.Find("lblPagina", false).OfType<Label>().FirstOrDefault();
            Label registros = tabla.FindForm()?.Controls.Find("lblRegistros", true)
                .OfType<Label>().FirstOrDefault();

            // El ancho adicional evita que se recorten las etiquetas en pantallas anchas.
            barra.Height = 48;
            if (anterior != null)
            {
                anterior.Size = new Size(128, 32);
                anterior.Text = "‹ Anterior";
                anterior.Padding = new Padding(3, 0, 3, 0);
            }
            if (pagina != null)
            {
                pagina.Size = new Size(180, 32);
                pagina.AutoEllipsis = false;
                pagina.TextAlign = ContentAlignment.MiddleCenter;
            }
            if (siguiente != null)
            {
                siguiente.Size = new Size(128, 32);
                siguiente.Text = "Siguiente ›";
                siguiente.Padding = new Padding(3, 0, 3, 0);
            }

            // Reacomoda los controles después de cambiar sus dimensiones.
            // También conserva el espacio para los resultados a la izquierda.
            Action acomodar = () =>
            {
                if (anterior == null || siguiente == null || pagina == null)
                    return;

                const int margen = 10;
                int y = Math.Max(0, (barra.ClientSize.Height - anterior.Height) / 2);
                int siguienteX = barra.ClientSize.Width - margen - siguiente.Width;
                int paginaX = siguienteX - margen - pagina.Width;
                int anteriorX = paginaX - margen - anterior.Width;

                siguiente.Location = new Point(siguienteX, y);
                pagina.Location = new Point(paginaX, y);
                anterior.Location = new Point(anteriorX, y);

                if (registros != null)
                {
                    registros.Location = new Point(margen, y);
                    registros.Size = new Size(Math.Max(0, anteriorX - margen * 2), anterior.Height);
                    registros.TextAlign = ContentAlignment.MiddleLeft;
                }
            };

            barra.Resize += (s, e) => acomodar();
            acomodar();
        }

        private static Type ObtenerTipoFila(object origen)
        {
            if (origen == null)
                return null;

            Type tipo = origen.GetType();
            if (tipo.IsArray)
                return tipo.GetElementType();
            if (tipo.IsGenericType)
                return tipo.GetGenericArguments()[0];
            return null;
        }

        private static PropertyInfo ObtenerPropiedadEstado(object origen)
        {
            if (origen is BindingSource binding)
                origen = binding.DataSource;
            Type tipo = ObtenerTipoFila(origen);
            if (tipo == null) return null;
            PropertyInfo propiedad = tipo.GetProperty("Estado");
            return propiedad != null && (propiedad.PropertyType == typeof(bool) || propiedad.PropertyType == typeof(bool?))
                ? propiedad : null;
        }

        private static Button CrearBotonPaginacion(string texto, string nombre, DataGridView tabla)
        {
            Form formulario = tabla.FindForm();
            Button buscar = formulario == null ? null : formulario.Controls.Find("btnBuscar", true)
                .OfType<Button>().FirstOrDefault();
            return new Button
            {
                Name = nombre,
                Text = texto,
                Size = new Size(92, 26),
                FlatStyle = FlatStyle.Flat,
                BackColor = buscar == null ? Color.FromArgb(3, 43, 83) : buscar.BackColor,
                ForeColor = buscar == null ? Color.White : buscar.ForeColor,
                Font = buscar == null ? tabla.Font : buscar.Font,
                Cursor = Cursors.Hand
            };
        }

        public static void Aplicar(Panel contenido, Panel menu, TableLayoutPanel botones)
        {
            CrearFranjaAcciones(contenido, botones);
            DistribuirPestanas(menu);
        }

        /// <summary>Construye en formularios relacionales la misma navegación adaptable de Usuarios.</summary>
        public static void ConstruirBarraLateralModelo(Form formulario)
        {
            if (formulario == null) return;
            Panel menu = formulario.Controls.Find("pnlMenu", true).OfType<Panel>().FirstOrDefault();
            Panel identidad = formulario.Controls.Find("pnlLogo", true).OfType<Panel>().FirstOrDefault();
            Panel opciones = formulario.Controls.Find("pnlItemsMenu", true).OfType<Panel>().FirstOrDefault();
            Label marca = formulario.Controls.Find("lblSistema", true).OfType<Label>().FirstOrDefault();
            Label logoAnterior = formulario.Controls.Find("lblLogo", true).OfType<Label>().FirstOrDefault();
            Label rol = formulario.Controls.Find("lblUsuario", true).OfType<Label>().FirstOrDefault();
            Label descripcionRol = formulario.Controls.Find("lblUsuarioRol", true).OfType<Label>().FirstOrDefault();
            Button volverAnterior = formulario.Controls.Find("btnVolverMenu", true).OfType<Button>().FirstOrDefault();
            Button cerrarAnterior = formulario.Controls.Find("btnCerrarSesion", true).OfType<Button>().FirstOrDefault();
            if (menu == null || identidad == null || opciones == null || marca == null || rol == null || descripcionRol == null) return;

            Label[] etiquetas = opciones.Controls.OfType<Label>().Where(x => x.Tag != null).OrderBy(x => x.TabIndex).ToArray();
            Color fondo = ColorTranslator.FromHtml("#061D30");
            Color cian = ColorTranslator.FromHtml("#00D9FF");
            Color blanco = Color.White;
            identidad.Controls.Clear();
            identidad.Height = 142;
            identidad.Dock = DockStyle.Fill;
            identidad.BackColor = ColorTranslator.FromHtml("#032B53");
            identidad.Padding = Padding.Empty;
            CampoRedondeado.Aplicar(identidad, 12);
            IconPictureBox iconoMarca = new IconPictureBox
            {
                Name = "icoMarcaTelefonia", IconChar = IconChar.MobileAlt, IconFont = IconFont.Auto,
                IconColor = ColorTranslator.FromHtml("#00CBFF"), BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom, Size = new Size(42, 42), Anchor = AnchorStyles.Top
            };
            marca.Name = "lblSistema";
            marca.Text = "Sistema de Telefonía\ny Accesorios";
            marca.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            marca.ForeColor = blanco;
            marca.TextAlign = ContentAlignment.TopCenter;
            marca.AutoSize = false;
            marca.SetBounds(6, 52, Math.Max(0, identidad.ClientSize.Width - 12), 82);
            marca.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            identidad.Controls.Add(iconoMarca);
            identidad.Controls.Add(marca);
            identidad.SizeChanged += (s, e) =>
            {
                iconoMarca.Left = (identidad.ClientSize.Width - iconoMarca.Width) / 2;
                marca.SetBounds(6, 52, Math.Max(0, identidad.ClientSize.Width - 12), 82);
            };
            if (logoAnterior != null) logoAnterior.Visible = false;

            rol.Name = "lblRol";
            rol.Text = string.IsNullOrWhiteSpace(rol.Text) ? "Administrador" : rol.Text;
            rol.Dock = DockStyle.Fill;
            rol.Height = 24;
            rol.ForeColor = blanco;
            rol.TextAlign = ContentAlignment.MiddleCenter;
            rol.AutoEllipsis = true;
            descripcionRol.Name = "lblDescripcionRol";
            descripcionRol.Text = "Usuario del sistema";
            descripcionRol.Dock = DockStyle.Fill;
            descripcionRol.Height = 24;
            descripcionRol.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            descripcionRol.TextAlign = ContentAlignment.MiddleCenter;
            descripcionRol.Font = new Font("Segoe UI", 9F);

            opciones.Controls.Clear();
            opciones.Dock = DockStyle.Fill;
            opciones.AutoScroll = false;
            opciones.BackColor = fondo;
            TableLayoutPanel filas = new TableLayoutPanel
            {
                Name = "layoutBotonesBarraLateralRelacional", Dock = DockStyle.Fill,
                ColumnCount = 1, RowCount = 18, BackColor = fondo,
                Margin = Padding.Empty, Padding = Padding.Empty, GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            filas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 18; i++) filas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 18F));

            IconButton volver = CrearBotonLateral("btnVolverMenu", "Volver al menú principal", IconChar.Home, null, formulario, cian, fondo, blanco);
            volver.IconColor = blanco;
            volver.BackColor = ColorTranslator.FromHtml("#08243A");
            volver.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#526779");
            volver.FlatAppearance.BorderSize = 1;
            CampoRedondeado.Aplicar(volver, 6);
            filas.Controls.Add(volver, 0, 0);

            string[] modulos = { "Usuarios", "Sucursales", "Empleados", "Clientes", "Planes", "Lineas",
                "Proveedores", "Marcas", "Equipos", "Compras", "Inventario", "Ventas", "Facturacion", "Pagos", "Cardex", "Reclamos" };
            Dictionary<string, Label> origen = etiquetas.ToDictionary(x => NormalizarDestino(Convert.ToString(x.Tag)), x => x,
                StringComparer.OrdinalIgnoreCase);
            string activo = NormalizarDestino(formulario.Name);
            for (int i = 0; i < modulos.Length; i++)
            {
                Label etiqueta;
                if (!origen.TryGetValue(modulos[i], out etiqueta)) continue;
                string ruta = Convert.ToString(etiqueta.Tag);
                IconButton boton = CrearBotonLateral("btn" + modulos[i],
                    (etiqueta.Text ?? modulos[i]).Trim().TrimStart('›', '⌂').Trim(), IconoModulo(ruta), ruta,
                    formulario, cian, fondo, blanco);
                if (string.Equals(modulos[i], activo, StringComparison.OrdinalIgnoreCase))
                {
                    boton.BackColor = ColorTranslator.FromHtml("#0B3558");
                    boton.ForeColor = cian;
                    boton.IconColor = cian;
                }
                filas.Controls.Add(boton, 0, i + 1);
            }

            IconButton cerrar = CrearBotonLateral("btnCerrarSesion", "Cerrar sesión", IconChar.SignOutAlt, null, formulario, cian, fondo, blanco);
            cerrar.IconColor = blanco;
            cerrar.BackColor = ColorTranslator.FromHtml("#A83F48");
            cerrar.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#D56A70");
            cerrar.FlatAppearance.BorderSize = 1;
            CampoRedondeado.Aplicar(cerrar, 6);
            filas.Controls.Add(cerrar, 0, 17);

            TableLayoutPanel bloqueRol = new TableLayoutPanel
            {
                Name = "layoutRolBarraLateral", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                BackColor = fondo, Margin = Padding.Empty, Padding = new Padding(2, 0, 2, 0)
            };
            bloqueRol.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            bloqueRol.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            bloqueRol.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            bloqueRol.Controls.Add(rol, 0, 0);
            bloqueRol.Controls.Add(descripcionRol, 0, 1);
            opciones.Controls.Add(filas);

            TableLayoutPanel estructura = new TableLayoutPanel
            {
                Name = "layoutBarraLateralRelacional", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
                BackColor = fondo, Margin = Padding.Empty, Padding = Padding.Empty
            };
            estructura.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            estructura.RowStyles.Add(new RowStyle(SizeType.Absolute, 142));
            estructura.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            estructura.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            estructura.Controls.Add(identidad, 0, 0);
            estructura.Controls.Add(bloqueRol, 0, 1);
            estructura.Controls.Add(opciones, 0, 2);
            menu.Controls.Clear();
            menu.Padding = new Padding(8);
            menu.Controls.Add(estructura);
            menu.Dock = DockStyle.Left;
            menu.BackColor = fondo;
            menu.SizeChanged += (s, e) => menu.Width = Math.Max(148, Math.Min(195, (int)(formulario.ClientSize.Width * 0.18)));
            menu.Width = Math.Max(148, Math.Min(195, (int)(formulario.ClientSize.Width * 0.18)));
            menu.Paint += (s, e) =>
            {
                using (Pen borde = new Pen(ColorTranslator.FromHtml("#34495E"), 1))
                    e.Graphics.DrawRectangle(borde, 0, 0, menu.Width - 1, menu.Height - 1);
            };
            if (volverAnterior != null) volverAnterior.Visible = false;
            if (cerrarAnterior != null) cerrarAnterior.Visible = false;
        }

        private static IconButton CrearBotonLateral(string nombre, string texto, IconChar icono, string ruta,
            Form formulario, Color cian, Color fondo, Color blanco)
        {
            IconButton boton = new IconButton
            {
                Name = nombre, Text = texto, Tag = ruta, Dock = DockStyle.Fill, Margin = Padding.Empty,
                Padding = new Padding(12, 0, 4, 0), TextAlign = ContentAlignment.MiddleLeft,
                ImageAlign = ContentAlignment.MiddleLeft, TextImageRelation = TextImageRelation.ImageBeforeText,
                IconChar = icono, IconFont = IconFont.Auto, IconColor = cian, IconSize = 20,
                BackColor = fondo, ForeColor = blanco, FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F), Cursor = Cursors.Hand, UseVisualStyleBackColor = false
            };
            boton.FlatAppearance.BorderSize = 0;
            CampoRedondeado.Aplicar(boton, 6);
            if (nombre == "btnVolverMenu")
                boton.Click += (s, e) => Navegacion.VolverAlMenu(formulario);
            else if (nombre == "btnCerrarSesion")
                boton.Click += (s, e) => Navegacion.CerrarSesion(formulario);
            else
            {
                boton.Click += (s, e) => Navegacion.Abrir(formulario, ruta);
                boton.MouseEnter += (s, e) =>
                {
                    boton.BackColor = ColorTranslator.FromHtml("#0B3558");
                    boton.ForeColor = cian;
                    boton.IconColor = cian;
                };
                boton.MouseLeave += (s, e) =>
                {
                    bool seleccionado = string.Equals(NormalizarDestino(formulario.Name), NormalizarDestino(ruta), StringComparison.OrdinalIgnoreCase);
                    boton.BackColor = seleccionado ? ColorTranslator.FromHtml("#0B3558") : fondo;
                    boton.ForeColor = seleccionado ? cian : blanco;
                    boton.IconColor = cian;
                };
            }
            return boton;
        }

        private static string NormalizarDestino(string destino)
        {
            return string.IsNullOrWhiteSpace(destino) ? string.Empty :
                destino.StartsWith("Frm", StringComparison.OrdinalIgnoreCase) ? destino.Substring(3) : destino;
        }

        /// <summary>
        /// Mantiene el mismo ancho de menú en ventanas normales y lo reduce en
        /// ventanas angostas; los textos de navegación ocupan el ancho disponible.
        /// </summary>
        private static void ConfigurarMenuLateral(DataGridView tabla)
        {
            Form formulario = tabla.FindForm();
            if (formulario == null)
                return;

            Action ajustar = () =>
            {
                Panel lateral = formulario.Controls.Find("pnlMenu", true)
                    .OfType<Panel>().FirstOrDefault();
                Panel opciones = formulario.Controls.Find("pnlItemsMenu", true)
                    .OfType<Panel>().FirstOrDefault();
                if (lateral == null || opciones == null)
                    return;

                // La barra estándar ya usa botones en filas fijas y no requiere
                // iconos adicionales ni AutoScroll (que dibuja flechas laterales).
                if (opciones.Controls.Find("layoutBotonesBarraLateralRelacional", true).Length > 0)
                    return;

                AgregarIconosMenu(formulario, opciones);

                int ancho = Math.Max(AnchoMenuLateralMinimo,
                    Math.Min(AnchoMenuLateralEstandar,
                        (int)(formulario.ClientSize.Width * 0.18)));

                lateral.Dock = DockStyle.Left;
                lateral.Width = ancho;
                lateral.PerformLayout();
                opciones.PerformLayout();

                float tamanoFuente = Math.Max(8.5F,
                    Math.Min(10F, ancho / 19.5F));
                foreach (Label opcion in opciones.Controls.OfType<Label>()
                    .Where(etiqueta => etiqueta.Tag != null))
                {
                    opcion.SetBounds(opcion.Left, opcion.Top,
                        opciones.ClientSize.Width, opcion.Height);
                    opcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    opcion.AutoSize = false;
                    opcion.AutoEllipsis = true;
                    opcion.TextAlign = ContentAlignment.MiddleLeft;
                    IconPictureBox iconoInterno = opcion.Controls.OfType<IconPictureBox>().FirstOrDefault();
                    IconPictureBox iconoPanel = opciones.Controls.OfType<IconPictureBox>()
                        .FirstOrDefault(i => ReferenceEquals(i.Tag, opcion));
                    bool tieneIcono = iconoInterno != null || iconoPanel != null;
                    opcion.Padding = new Padding(tieneIcono ? 42 : 12, 0, 8, 0);

                    if (iconoInterno != null)
                    {
                        iconoInterno.SetBounds(10, Math.Max(0, (opcion.Height - 24) / 2), 24, 24);
                        iconoInterno.BringToFront();
                    }
                    if (iconoPanel != null)
                        iconoPanel.SetBounds(opcion.Left + 10, opcion.Top + Math.Max(0, (opcion.Height - 24) / 2), 24, 24);

                    if (opcion.Font.Name != "Segoe UI" ||
                        Math.Abs(opcion.Font.Size - tamanoFuente) > 0.1F)
                        opcion.Font = new Font("Segoe UI", tamanoFuente, opcion.Font.Style);
                }

                Button volver = formulario.Controls.Find("btnVolverMenu", true)
                    .OfType<Button>().FirstOrDefault();
                Button cerrar = formulario.Controls.Find("btnCerrarSesion", true)
                    .OfType<Button>().FirstOrDefault();
                if (volver != null) volver.AutoEllipsis = true;
                if (cerrar != null) cerrar.AutoEllipsis = true;
            };

            formulario.SizeChanged += (s, e) => ajustar();
            formulario.Shown += (s, e) => ajustar();
            ajustar();
        }

        private static void AgregarIconosMenu(Form formulario, Panel opciones)
        {
            if (formulario == null || opciones == null) return;
            foreach (Label opcion in opciones.Controls.OfType<Label>().Where(l => l.Tag != null && l.Visible))
            {
                // Algunos formularios ya crean el icono como hijo de la etiqueta;
                // otros lo agregan directamente al panel. Revisar ambos niveles evita
                // superponer un segundo icono sobre las opciones de navegación.
                if (opcion.Controls.OfType<IconPictureBox>().Any()) continue;
                if (opciones.Controls.OfType<IconPictureBox>().Any(i => ReferenceEquals(i.Tag, opcion))) continue;
                string texto = (opcion.Text ?? string.Empty).Trim();
                if (texto.StartsWith("›", StringComparison.Ordinal)) texto = texto.Substring(1).Trim();
                opcion.Text = texto;
                IconPictureBox icono = new IconPictureBox
                {
                    Name = "icoNavegacion" + opcion.Tag.ToString().Replace("Frm", string.Empty),
                    Tag = opcion,
                    IconChar = IconoModulo(opcion.Tag.ToString()),
                    IconFont = IconFont.Auto,
                    IconColor = Color.FromArgb(0, 203, 255),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Transparent,
                    Cursor = Cursors.Hand,
                    AccessibleName = "Abrir " + texto
                };
                icono.Click += (s, e) => Navegacion.Abrir(formulario, opcion.Tag.ToString());
                icono.SetBounds(opcion.Left + 10, opcion.Top + Math.Max(0, (opcion.Height - 17) / 2), 17, 17);
                opciones.Controls.Add(icono);
                icono.BringToFront();
            }
        }

        private static IconChar IconoModulo(string pantalla)
        {
            string nombre = pantalla.StartsWith("Frm", StringComparison.Ordinal) ? pantalla.Substring(3) : pantalla;
            switch (nombre)
            {
                case "Usuarios": return IconChar.UserShield;
                case "Sucursales": return IconChar.Building;
                case "Empleados": return IconChar.UserTie;
                case "Clientes": return IconChar.Users;
                case "Planes": return IconChar.Wifi;
                case "Lineas": return IconChar.SimCard;
                case "Proveedores": return IconChar.Truck;
                case "Marcas": return IconChar.Tags;
                case "Equipos": return IconChar.MobileAlt;
                case "Compras": return IconChar.CartPlus;
                case "Inventario": return IconChar.Boxes;
                case "Ventas": return IconChar.ShoppingCart;
                case "Facturacion": return IconChar.FileInvoiceDollar;
                case "Pagos": return IconChar.CreditCard;
                case "Cardex": return IconChar.ChartLine;
                case "Reclamos": return IconChar.Headset;
                case "DetalleCompra":
                case "DetalleVenta":
                case "DetalleFactura": return IconChar.FileInvoiceDollar;
                default: return IconChar.FolderOpen;
            }
        }

        private static void CrearFranjaAcciones(Panel contenido, TableLayoutPanel botones)
        {
            Panel franja = new Panel
            {
                Name = "pnlAccionesModelo",
                Dock = DockStyle.Top,
                Height = 62,
                BackColor = Color.FromArgb(3, 22, 43)
            };

            contenido.SuspendLayout();
            contenido.Controls.Remove(botones);
            botones.Dock = DockStyle.None;
            botones.Height = 54;
            franja.Controls.Add(botones);
            contenido.Controls.Add(franja);

            // La misma franja y las mismas acciones de archivo se aplican a los CRUD que usan el tema común.
            CentrarAcciones(franja, botones);

            // Mantiene el orden de acoplamiento: captura, acciones y catálogo.
            contenido.Controls.SetChildIndex(franja, 1);
            contenido.ResumeLayout(true);

            Action centrar = () =>
            {
                int ancho = Math.Min(900, franja.ClientSize.Width);
                botones.SetBounds(
                    Math.Max(0, (franja.ClientSize.Width - ancho) / 2),
                    Math.Max(0, (franja.ClientSize.Height - botones.Height) / 2),
                    ancho,
                    botones.Height);
            };

            franja.SizeChanged += (s, e) => centrar();
            centrar();
        }

        public static void DistribuirPestanas(Panel menu)
        {
            foreach (Label etiqueta in menu.Controls.OfType<Label>().Where(etiqueta => etiqueta.Tag != null))
                if (string.Equals(etiqueta.Tag.ToString(), "DetalleCompra", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(etiqueta.Tag.ToString(), "FrmDetalleCompra", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(etiqueta.Tag.ToString(), "DetalleVenta", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(etiqueta.Tag.ToString(), "FrmDetalleVenta", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(etiqueta.Tag.ToString(), "DetalleFactura", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(etiqueta.Tag.ToString(), "FrmDetalleFactura", StringComparison.OrdinalIgnoreCase))
                    etiqueta.Visible = false;

            Label[] pestanas = menu.Controls
                .OfType<Label>()
                .Where(etiqueta => etiqueta.Tag != null &&
                    !string.Equals(etiqueta.Tag.ToString(), "DetalleCompra", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(etiqueta.Tag.ToString(), "FrmDetalleCompra", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(etiqueta.Tag.ToString(), "DetalleVenta", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(etiqueta.Tag.ToString(), "FrmDetalleVenta", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(etiqueta.Tag.ToString(), "DetalleFactura", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(etiqueta.Tag.ToString(), "FrmDetalleFactura", StringComparison.OrdinalIgnoreCase))
                .OrderBy(etiqueta => etiqueta.TabIndex)
                .ToArray();

            if (pestanas.Length == 0)
                return;

            Action distribuir = () =>
            {
                const int alto = 56;
                int y = 0;

                foreach (Label pestana in pestanas)
                {
                    pestana.SetBounds(0, y, menu.ClientSize.Width, alto);
                    pestana.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    pestana.AutoSize = false;
                    pestana.AutoEllipsis = true;
                    pestana.TextAlign = ContentAlignment.MiddleLeft;
                    pestana.Padding = new Padding(42, 0, 8, 0);
                    IconPictureBox iconoInterno = pestana.Controls.OfType<IconPictureBox>().FirstOrDefault();
                    if (iconoInterno != null)
                    {
                        iconoInterno.SetBounds(10, (alto - 24) / 2, 24, 24);
                        iconoInterno.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        iconoInterno.BringToFront();
                    }
                    IconPictureBox iconoPanel = menu.Controls.OfType<IconPictureBox>()
                        .FirstOrDefault(i => ReferenceEquals(i.Tag, pestana));
                    if (iconoPanel != null)
                    {
                        iconoPanel.SetBounds(pestana.Left + 10, pestana.Top + (alto - 24) / 2, 24, 24);
                        iconoPanel.BringToFront();
                    }
                    y += alto;
                }

                menu.AutoScrollMinSize = new Size(0, y);
            };

            menu.SizeChanged += (s, e) => distribuir();
            distribuir();
        }

        /// <summary>
        /// Resalta la pantalla actual en el menú lateral con la misma paleta de Pagos.
        /// Los Tag pueden contener el nombre lógico o el nombre de clase con prefijo Frm.
        /// </summary>
        public static void ResaltarPestanaActiva(Panel menu, string nombreFormulario)
        {
            if (menu == null || string.IsNullOrWhiteSpace(nombreFormulario))
                return;

            string nombreActivo = nombreFormulario.StartsWith("Frm", StringComparison.OrdinalIgnoreCase)
                ? nombreFormulario.Substring(3)
                : nombreFormulario;

            Color fondoNormal = ColorTranslator.FromHtml("#061D30");
            Color fondoActivo = ColorTranslator.FromHtml("#0B3558");
            Color textoActivo = ColorTranslator.FromHtml("#00D9FF");

            foreach (Label pestana in menu.Controls.OfType<Label>().Where(etiqueta => etiqueta.Tag != null))
            {
                string nombrePestana = pestana.Tag.ToString();
                if (nombrePestana.StartsWith("Frm", StringComparison.OrdinalIgnoreCase))
                    nombrePestana = nombrePestana.Substring(3);

                bool activa = string.Equals(nombrePestana, nombreActivo, StringComparison.OrdinalIgnoreCase);
                pestana.BackColor = activa ? fondoActivo : fondoNormal;
                pestana.ForeColor = activa ? textoActivo : Color.White;
            }
        }
    }
}
