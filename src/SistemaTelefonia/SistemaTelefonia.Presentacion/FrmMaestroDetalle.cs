using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Formulario compartido para encabezados y sus líneas dependientes.</summary>
    internal sealed class FrmMaestroDetalle<TMaestro, TDetalle> : Form
        where TMaestro : new() where TDetalle : new()
    {
        private static readonly Color AzulPanel = Color.FromArgb(7, 35, 66);
        private static readonly Color VerdeCaptura = Color.FromArgb(232, 248, 232);
        private static readonly Color AzulAutomatico = Color.FromArgb(220, 230, 240);
        private const decimal TasaIvaGuatemala = 0.12m;

        private readonly CrudRelacionalNegocio negocio = new CrudRelacionalNegocio();
        private readonly string titulo;
        private readonly string claveMaestro;
        private readonly string claveDetalle;
        private readonly string claveForanea;
        private readonly string[] calculadosMaestro;
        private readonly string[] calculadosDetalle;
        private readonly Dictionary<string, Control> camposMaestro = new Dictionary<string, Control>();
        private readonly Dictionary<string, Control> camposDetalle = new Dictionary<string, Control>();
        private readonly List<TDetalle> detalles = new List<TDetalle>();
        private readonly DataGridView grillaDetalles = new DataGridView();
        private readonly Label lblConteoDetalles = new Label();
        private readonly Label lblPaginaDetalles = new Label();
        private readonly Button btnPaginaAnteriorDetalles = new Button();
        private readonly Button btnPaginaSiguienteDetalles = new Button();
        private readonly Button nuevo = Boton("btnNuevo", "Nuevo", Color.FromArgb(32, 142, 96));
        private readonly Button guardar = Boton("btnGuardar", "Guardar", Color.FromArgb(0, 112, 210));
        private readonly Button editar = Boton("btnEditar", "Editar", Color.FromArgb(0, 130, 170));
        private readonly Button eliminar = Boton("btnEliminar", "Eliminar", Color.FromArgb(190, 58, 66));
        private readonly Button limpiar = Boton("btnLimpiar", "Limpiar", Color.FromArgb(91, 108, 126));
        private readonly Label ayuda = new Label { ForeColor = Color.FromArgb(170, 200, 225), AutoSize = true };
        private Label tituloFormulario;
        private TMaestro maestroActual;
        private bool modoEdicion;
        private bool editandoLinea;
        private bool modificoCamposDetalle;
        private bool cargandoObjeto;
        private int paginaDetalle;
        private int indiceDetalleEditado = -1;
        private TableLayoutPanel tablaCamposMaestro;
        private TableLayoutPanel tablaCamposDetalle;
        private readonly Color cianActivo = Color.FromArgb(128, 222, 255);
        private readonly Color cianHover = Color.FromArgb(205, 243, 255);
        private List<EmpleadosEntidad> empleadosCatalogo = new List<EmpleadosEntidad>();
        private List<EquiposEntidad> equiposCatalogo = new List<EquiposEntidad>();

        public FrmMaestroDetalle(string titulo, string claveMaestro, string claveDetalle, string claveForanea,
            string[] calculadosMaestro, string[] calculadosDetalle, TMaestro maestro)
        {
            this.titulo = titulo;
            this.claveMaestro = claveMaestro;
            this.claveDetalle = claveDetalle;
            this.claveForanea = claveForanea;
            this.calculadosMaestro = calculadosMaestro ?? new string[0];
            this.calculadosDetalle = calculadosDetalle ?? new string[0];
            Text = "Sistema de Telefonía - " + (ReferenceEquals(maestro, null) ? "Nueva " + titulo.ToLowerInvariant() : titulo);
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(1000, 780);
            Size = new Size(1260, 940);
            BackColor = Color.FromArgb(3, 22, 43);
            Font = new Font("Segoe UI", 9F);
            KeyPreview = true;
            ConstruirInterfaz();
            CargarCatalogosVenta();
            ConectarEventos();
            EstadoInicial();
            AplicarEsquinasRedondeadas();
            if (!ReferenceEquals(maestro, null)) CargarExistente(maestro);
        }

        private void ConstruirInterfaz()
        {
            Panel contenido = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(16) };
            Controls.Add(contenido);
            Panel encabezado = new Panel { Dock = DockStyle.Top, Height = 88, BackColor = Color.FromArgb(3, 43, 83), Padding = new Padding(22, 10, 22, 8) };
            tituloFormulario = new Label { Text = "Nueva " + titulo.ToLowerInvariant(), ForeColor = Color.White, Font = new Font("Segoe UI", 20F, FontStyle.Bold), Dock = DockStyle.Top, Height = 42 };
            encabezado.Controls.Add(tituloFormulario);
            encabezado.Controls.Add(new Label { Text = "Ingresa el encabezado y sus detalles en una sola operación", ForeColor = Color.FromArgb(170, 200, 225), Dock = DockStyle.Bottom, Height = 24 });
            Controls.Add(encabezado);

            Panel pie = new Panel { Dock = DockStyle.Bottom, Height = 72, BackColor = Color.FromArgb(7, 35, 66), Padding = new Padding(12) };
            FlowLayoutPanel acciones = new FlowLayoutPanel
            {
                Size = new Size(5 * 158 + 5 * 8, 48), FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false, Margin = Padding.Empty, Padding = Padding.Empty
            };
            foreach (Button boton in new[] { limpiar, eliminar, editar, guardar, nuevo })
            {
                boton.Size = new Size(158, 48); boton.Margin = new Padding(4, 0, 4, 0); acciones.Controls.Add(boton);
            }
            pie.Controls.Add(acciones);
            Action centrarAccionesModal = () => acciones.Location = new Point(
                Math.Max(0, (pie.ClientSize.Width - acciones.Width) / 2),
                Math.Max(0, (pie.ClientSize.Height - acciones.Height) / 2));
            pie.SizeChanged += (s, e) => centrarAccionesModal();
            centrarAccionesModal();
            Controls.Add(pie);

            int filasMaestro = (typeof(TMaestro).GetProperties(BindingFlags.Instance | BindingFlags.Public).Length + 1) / 2;
            int altoMaestro = 78 + filasMaestro * 42;
            Panel panelMaestro = Seccion("Información de " + titulo.ToLowerInvariant(), altoMaestro);
            panelMaestro.SetBounds(16, 10, Math.Max(500, contenido.ClientSize.Width - 32), altoMaestro);
            panelMaestro.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            contenido.Controls.Add(panelMaestro);
            tablaCamposMaestro = CrearTabla(camposMaestro, typeof(TMaestro), claveMaestro, null, calculadosMaestro);
            tablaCamposMaestro.SetBounds(12, 42, panelMaestro.ClientSize.Width - 24, filasMaestro * 42);
            tablaCamposMaestro.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelMaestro.Controls.Add(tablaCamposMaestro);

            const int altoGrillaDetalle = 326;
            const int altoPaginacionDetalle = 48;
            Panel panelDetalle = Seccion("Detalle de " + titulo.ToLowerInvariant(), 690);
            panelDetalle.SetBounds(16, altoMaestro + 22, Math.Max(500, contenido.ClientSize.Width - 32), 690);
            panelDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            contenido.Controls.Add(panelDetalle);
            tablaCamposDetalle = CrearTabla(camposDetalle, typeof(TDetalle), claveDetalle, claveForanea, calculadosDetalle);
            AjustarAnchosCamposUniformes();
            tablaCamposDetalle.SetBounds(12, 42, panelDetalle.ClientSize.Width - 24, 210);
            tablaCamposDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelDetalle.Controls.Add(tablaCamposDetalle);
            ayuda.Text = "Ctrl+Enter agrega una línea; doble clic la carga para editarla; Supr quita la seleccionada. Guardar aplica el encabezado y todos los detalles.";
            ayuda.SetBounds(14, 256, panelDetalle.ClientSize.Width - 28, 26);
            ayuda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelDetalle.Controls.Add(ayuda);

            grillaDetalles.Name = "dgvDetallesMaestro";
            grillaDetalles.SetBounds(12, 286, panelDetalle.ClientSize.Width - 24, altoGrillaDetalle);
            grillaDetalles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grillaDetalles.ReadOnly = true;
            grillaDetalles.AllowUserToAddRows = false;
            grillaDetalles.AllowUserToDeleteRows = false;
            grillaDetalles.AllowUserToResizeRows = false;
            grillaDetalles.MultiSelect = false;
            grillaDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaDetalles.AutoGenerateColumns = true;
            grillaDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grillaDetalles.RowHeadersVisible = false;
            grillaDetalles.ColumnHeadersHeight = 34;
            grillaDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grillaDetalles.RowTemplate.Height = 29;
            grillaDetalles.ScrollBars = ScrollBars.Horizontal;
            grillaDetalles.BackgroundColor = Color.FromArgb(7, 35, 66);
            grillaDetalles.BorderStyle = BorderStyle.None;
            panelDetalle.Controls.Add(grillaDetalles);

            Panel paginacionDetalle = new Panel
            {
                Name = "pnlPaginacionDetalles", BackColor = AzulPanel,
                Bounds = new Rectangle(12, 612, panelDetalle.ClientSize.Width - 24, altoPaginacionDetalle),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            lblConteoDetalles.Name = "lblConteoDetalles";
            lblConteoDetalles.Text = "Resultados: 0";
            lblConteoDetalles.ForeColor = Color.White;
            lblConteoDetalles.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblConteoDetalles.TextAlign = ContentAlignment.MiddleLeft;
            lblConteoDetalles.SetBounds(10, 8, 300, 32);
            btnPaginaAnteriorDetalles.Name = "btnPaginaAnterior";
            btnPaginaAnteriorDetalles.Text = "‹ Anterior";
            btnPaginaAnteriorDetalles.Size = new Size(128, 32);
            btnPaginaSiguienteDetalles.Name = "btnPaginaSiguiente";
            btnPaginaSiguienteDetalles.Text = "Siguiente ›";
            btnPaginaSiguienteDetalles.Size = new Size(128, 32);
            lblPaginaDetalles.Name = "lblPaginaDetalles";
            lblPaginaDetalles.Size = new Size(180, 32);
            lblPaginaDetalles.ForeColor = Color.White;
            lblPaginaDetalles.Font = new Font("Segoe UI", 9.5F);
            lblPaginaDetalles.TextAlign = ContentAlignment.MiddleCenter;
            paginacionDetalle.Controls.Add(lblConteoDetalles);
            paginacionDetalle.Controls.Add(btnPaginaAnteriorDetalles);
            paginacionDetalle.Controls.Add(lblPaginaDetalles);
            paginacionDetalle.Controls.Add(btnPaginaSiguienteDetalles);
            panelDetalle.Controls.Add(paginacionDetalle);

            contenido.SizeChanged += (s, e) =>
            {
                int ancho = Math.Max(500, contenido.ClientSize.Width - 32);
                panelMaestro.Width = panelDetalle.Width = ancho;
                tablaCamposMaestro.Width = panelMaestro.ClientSize.Width - 24;
                tablaCamposDetalle.Width = panelDetalle.ClientSize.Width - 24;
                ayuda.Width = panelDetalle.ClientSize.Width - 28;
                grillaDetalles.Width = panelDetalle.ClientSize.Width - 24;
                paginacionDetalle.Width = panelDetalle.ClientSize.Width - 24;
                AjustarPaginacionDetalles();
            };
            CampoRedondeado.Aplicar(encabezado, 12);
            CampoRedondeado.Aplicar(panelMaestro, 12);
            CampoRedondeado.Aplicar(panelDetalle, 12);
            CampoRedondeado.Aplicar(pie, 12);
            CampoRedondeado.Aplicar(grillaDetalles, 10);
            btnPaginaAnteriorDetalles.Click += (s, e) => CambiarPaginaDetalles(-1);
            btnPaginaSiguienteDetalles.Click += (s, e) => CambiarPaginaDetalles(1);
            paginacionDetalle.Resize += (s, e) => AjustarPaginacionDetalles();
            AjustarPaginacionDetalles();
            TemaVisualCrud.Aplicar(this);
        }

        private void AplicarEsquinasRedondeadas()
        {
            foreach (Control control in ControlesDescendientes<Control>(this))
            {
                if (control is TextBoxBase || control is ComboBox || control is NumericUpDown || control is DateTimePicker)
                    CampoRedondeado.Aplicar(control, 7);
                else if (control is Button)
                    CampoRedondeado.Aplicar(control, 8);
                else if (control is ListBox)
                    CampoRedondeado.Aplicar(control, 10);
                else if (control is DataGridView)
                    CampoRedondeado.Aplicar(control, 10);
            }
        }

        private static IEnumerable<T> ControlesDescendientes<T>(Control raiz) where T : Control
        {
            foreach (Control control in raiz.Controls)
            {
                T coincidencia = control as T;
                if (coincidencia != null) yield return coincidencia;
                foreach (T hijo in ControlesDescendientes<T>(control)) yield return hijo;
            }
        }

        private TableLayoutPanel CrearTabla(Dictionary<string, Control> destino, Type tipo, string clave, string fk, string[] calculados)
        {
            PropertyInfo[] props = tipo.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite && p.GetIndexParameters().Length == 0).ToArray();
            int visibles = props.Length;
            int filas = (visibles + 1) / 2;
            int anchoEtiqueta = Math.Max(178, Math.Min(250, props.Select(p =>
                TextRenderer.MeasureText(Etiqueta(p.Name), new Font("Segoe UI", 9F, FontStyle.Bold)).Width + 18).DefaultIfEmpty(178).Max()));
            TableLayoutPanel tabla = new TableLayoutPanel
            {
                ColumnCount = 4, RowCount = filas, BackColor = Color.Transparent,
                Margin = Padding.Empty, Padding = Padding.Empty
            };
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoEtiqueta)); tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoEtiqueta)); tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < filas; i++) tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo propiedad = props[i];
                bool soloLectura = propiedad.Name == clave || (!string.IsNullOrEmpty(fk) && propiedad.Name == fk) || calculados.Contains(propiedad.Name);
                bool esListaFk = (propiedad.Name != clave && EsCampoCatalogo(propiedad.Name)) ||
                    (!string.IsNullOrEmpty(fk) && propiedad.Name == fk);
                Control entrada = CrearEntrada(propiedad, soloLectura, esListaFk);
                entrada.Name = propiedad.Name;
                destino.Add(propiedad.Name, entrada);
                int fila = i / 2; int columna = (i % 2) * 2;
                tabla.Controls.Add(new Label
                {
                    Name = "lbl" + propiedad.Name, Text = Etiqueta(propiedad.Name), ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = false, Dock = DockStyle.Fill,
                    AutoEllipsis = false, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(3, 4, 8, 3)
                }, columna, fila);
                tabla.Controls.Add(entrada, columna + 1, fila);
            }
            return tabla;
        }

        private void AjustarAnchosCamposUniformes()
        {
            if (tablaCamposMaestro == null || tablaCamposDetalle == null) return;
            float anchoEtiqueta = Math.Max(tablaCamposMaestro.ColumnStyles[0].Width, tablaCamposDetalle.ColumnStyles[0].Width);
            foreach (TableLayoutPanel tabla in new[] { tablaCamposMaestro, tablaCamposDetalle })
            {
                tabla.ColumnStyles[0].SizeType = SizeType.Absolute;
                tabla.ColumnStyles[0].Width = anchoEtiqueta;
                tabla.ColumnStyles[2].SizeType = SizeType.Absolute;
                tabla.ColumnStyles[2].Width = anchoEtiqueta;
                tabla.PerformLayout();
            }
        }

        private Control CrearEntrada(PropertyInfo p, bool soloLectura, bool esListaFk)
        {
            Type tipo = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
            Control control;
            if (esListaFk)
                control = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Tag = "Catalogo" };
            else if (tipo == typeof(bool))
            {
                ComboBox combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                combo.Items.AddRange(new object[] { "Activo", "Inactivo" }); combo.SelectedIndex = 0; control = combo;
            }
            else if (tipo == typeof(DateTime))
                control = new DateTimePicker { Format = DateTimePickerFormat.Short };
            else if (tipo == typeof(int) || tipo == typeof(decimal))
            {
                NumericUpDown numero = new NumericUpDown { Minimum = 0, Maximum = 999999999, ThousandsSeparator = true, DecimalPlaces = tipo == typeof(decimal) ? 2 : 0 };
                if (p.Name == "Cantidad") numero.Value = 1;
                control = numero;
            }
            else control = new TextBox { MaxLength = 200 };
            control.Dock = DockStyle.Fill; control.Margin = new Padding(3, 5, 8, 5);
            control.Font = new Font("Segoe UI", 9.5F);
            control.BackColor = soloLectura ? AzulAutomatico : VerdeCaptura;
            if (soloLectura)
            {
                NumericUpDown n = control as NumericUpDown; if (n != null) n.ReadOnly = true;
                DateTimePicker d = control as DateTimePicker; if (d != null) d.Enabled = false;
                ComboBox c = control as ComboBox; if (c != null) c.Enabled = false;
                TextBox t = control as TextBox; if (t != null) t.ReadOnly = true;
                // Evita que las flechas del NumericUpDown cambien claves o valores calculados.
                control.Enabled = false;
            }
            return control;
        }

        private static bool EsCampoCatalogo(string nombre)
        {
            return nombre == "CodigoSucursal" || nombre == "CodigoCliente" || nombre == "CodigoEmpleado" ||
                nombre == "CodigoEquipo" || nombre == "CodigoPlan" || nombre == "CodigoVenta";
        }

        private sealed class OpcionCatalogo
        {
            public int Codigo { get; set; }
            public string Texto { get; set; }
        }

        private void CargarCatalogosVenta()
        {
            // Las consultas usan la capa de negocio y las entidades existentes.
            List<SucursalesEntidad> sucursales = new SucursalesNegocio().Listar();
            List<ClientesEntidad> clientes = new ClientesNegocio().Listar();
            Dictionary<int, string> nombresClientes = clientes.GroupBy(x => x.CodigoCliente)
                .ToDictionary(x => x.Key, x => x.First().Nombre);
            empleadosCatalogo = negocio.Listar<EmpleadosEntidad>();
            equiposCatalogo = negocio.Listar<EquiposEntidad>();
            List<PlanesEntidad> planes = new PlanesNegocio().Listar();

            ConfigurarCatalogo(camposMaestro, "CodigoSucursal", sucursales
                .Select(x => new OpcionCatalogo { Codigo = x.CodigoSucursal, Texto = x.Nombre }).ToList(), true);
            ConfigurarCatalogo(camposMaestro, "CodigoCliente", clientes
                .Select(x => new OpcionCatalogo { Codigo = x.CodigoCliente, Texto = x.Nombre }).ToList(), true);
            ConfigurarCatalogo(camposMaestro, "CodigoEmpleado", new List<OpcionCatalogo>(), true);
            ConfigurarCatalogo(camposDetalle, "CodigoEquipo", equiposCatalogo
                .Select(x => new OpcionCatalogo { Codigo = x.CodigoEquipo, Texto = x.Modelo + " - " + x.Descripcion + " (Q " + x.Precio.ToString("N2") + ")" }).ToList(), true);
            ConfigurarCatalogo(camposDetalle, "CodigoPlan", planes
                .Select(x => new OpcionCatalogo { Codigo = x.CodigoPlan, Texto = x.Nombre + " (Q " + x.CostoMensual.ToString("N2") + " / mes)" }).ToList(), false);

            // Facturación selecciona una venta existente. La FK del detalle apunta al
            // encabezado actual y queda como lista no editable, nunca como número libre.
            if (camposMaestro.ContainsKey("CodigoVenta") && claveMaestro != "CodigoVenta")
            {
                List<VentasEntidad> ventas = negocio.ListarCatalogo<VentasEntidad>();
                ConfigurarCatalogo(camposMaestro, "CodigoVenta", ventas
                    .Where(x => x.Estado)
                    .Select(x => new OpcionCatalogo
                    {
                        Codigo = x.CodigoVenta,
                        Texto = "Venta " + x.FechaVenta.ToString("dd/MM/yyyy") + " | Cliente " +
                            (nombresClientes.TryGetValue(x.CodigoCliente, out string nombre) ? nombre : "#" + x.CodigoCliente)
                    }).ToList(), true);
            }
            if (!string.IsNullOrWhiteSpace(claveForanea) && camposDetalle.ContainsKey(claveForanea))
                ConfigurarCatalogo(camposDetalle, claveForanea, new List<OpcionCatalogo>(), false);

            Control campoSucursal;
            if (camposMaestro.TryGetValue("CodigoSucursal", out campoSucursal))
            {
                ComboBox comboSucursal = campoSucursal as ComboBox;
                if (comboSucursal != null) comboSucursal.SelectedValueChanged += (s, e) => FiltrarEmpleadosPorSucursal();
            }
            Control campoEquipo;
            if (camposDetalle.TryGetValue("CodigoEquipo", out campoEquipo))
            {
                ComboBox comboEquipo = campoEquipo as ComboBox;
                if (comboEquipo != null) comboEquipo.SelectedValueChanged += (s, e) => SugerirPrecioEquipo();
            }
        }

        private static void ConfigurarCatalogo(Dictionary<string, Control> campos, string nombre, List<OpcionCatalogo> opciones, bool requerido)
        {
            Control control;
            if (!campos.TryGetValue(nombre, out control)) return;
            ComboBox combo = control as ComboBox;
            if (combo == null) return;
            List<OpcionCatalogo> elementos = new List<OpcionCatalogo>
            {
                new OpcionCatalogo { Codigo = 0, Texto = "- Seleccione -" }
            };
            elementos.AddRange(opciones.Select(x => new OpcionCatalogo
            {
                Codigo = x.Codigo,
                Texto = x.Codigo.ToString("00") + " - " + x.Texto
            }));
            combo.DataSource = elementos;
            combo.DisplayMember = "Texto";
            combo.ValueMember = "Codigo";
            if (elementos.Count > 0) combo.SelectedValue = 0;
        }

        private void FiltrarEmpleadosPorSucursal()
        {
            Control controlSucursal;
            Control controlEmpleado;
            if (!camposMaestro.TryGetValue("CodigoSucursal", out controlSucursal) ||
                !camposMaestro.TryGetValue("CodigoEmpleado", out controlEmpleado)) return;
            ComboBox sucursal = controlSucursal as ComboBox;
            ComboBox empleado = controlEmpleado as ComboBox;
            if (sucursal == null || empleado == null) return;
            int codigoSucursal = Convert.ToInt32(sucursal.SelectedValue ?? 0);
            int seleccionado = Convert.ToInt32(empleado.SelectedValue ?? 0);
            List<OpcionCatalogo> opciones = empleadosCatalogo
                .Where(x => codigoSucursal > 0 && x.CodigoSucursal == codigoSucursal)
                .Select(x => new OpcionCatalogo { Codigo = x.CodigoEmpleado, Texto = x.NombreEmpleado }).ToList();
            ConfigurarCatalogo(camposMaestro, "CodigoEmpleado", opciones, true);
            if (opciones.Any(x => x.Codigo == seleccionado)) empleado.SelectedValue = seleccionado;
            if (!cargandoObjeto && seleccionado > 0 && !opciones.Any(x => x.Codigo == seleccionado)) empleado.SelectedValue = 0;
        }

        private void SugerirPrecioEquipo()
        {
            if (cargandoObjeto) return;
            Control controlEquipo;
            Control controlPrecio;
            if (!camposDetalle.TryGetValue("CodigoEquipo", out controlEquipo) ||
                !camposDetalle.TryGetValue("PrecioUnitario", out controlPrecio)) return;
            ComboBox combo = controlEquipo as ComboBox;
            NumericUpDown precio = controlPrecio as NumericUpDown;
            if (combo == null || precio == null || combo.SelectedValue == null) return;
            int codigo = Convert.ToInt32(combo.SelectedValue);
            EquiposEntidad equipo = equiposCatalogo.FirstOrDefault(x => x.CodigoEquipo == codigo);
            if (equipo != null) precio.Value = Math.Min(precio.Maximum, Math.Max(precio.Minimum, equipo.Precio));
        }

        private void ConectarEventos()
        {
            nuevo.Click += (s, e) => IniciarNuevo();
            guardar.Click += Guardar_Click;
            editar.Click += Editar_Click;
            eliminar.Click += Eliminar_Click;
            limpiar.Click += Limpiar_Click;
            foreach (Control control in camposDetalle.Values)
            {
                TextBox texto = control as TextBox; if (texto != null) texto.TextChanged += (s, e) => MarcarDetalleModificado();
                NumericUpDown numero = control as NumericUpDown; if (numero != null) numero.ValueChanged += (s, e) => MarcarDetalleModificado();
                DateTimePicker fecha = control as DateTimePicker; if (fecha != null) fecha.ValueChanged += (s, e) => MarcarDetalleModificado();
                ComboBox combo = control as ComboBox; if (combo != null) combo.SelectedIndexChanged += (s, e) => MarcarDetalleModificado();
            }
            grillaDetalles.SelectionChanged += (s, e) =>
            {
                TDetalle seleccionado = grillaDetalles.CurrentRow == null ? default(TDetalle) : grillaDetalles.CurrentRow.DataBoundItem is TDetalle
                    ? (TDetalle)grillaDetalles.CurrentRow.DataBoundItem : default(TDetalle);
                int indice = ReferenceEquals(seleccionado, null) ? -1 : detalles.IndexOf(seleccionado);
                if (indice >= 0) { indiceDetalleEditado = indice; CargarObjeto(seleccionado, camposDetalle); }
            };
            grillaDetalles.CellDoubleClick += (s, e) =>
            {
                if (modoEdicion && e.RowIndex >= 0)
                {
                    TDetalle seleccionado = grillaDetalles.Rows[e.RowIndex].DataBoundItem is TDetalle
                        ? (TDetalle)grillaDetalles.Rows[e.RowIndex].DataBoundItem : default(TDetalle);
                    indiceDetalleEditado = ReferenceEquals(seleccionado, null) ? -1 : detalles.IndexOf(seleccionado);
                    if (indiceDetalleEditado >= 0) { CargarObjeto(seleccionado, camposDetalle); editandoLinea = true; }
                }
            };
            grillaDetalles.KeyDown += (s, e) =>
            {
                int indice = ObtenerIndiceDetalleSeleccionado();
                if (e.KeyCode == Keys.Delete && modoEdicion && indice >= 0)
                {
                    detalles.RemoveAt(indice); paginaDetalle = Math.Min(paginaDetalle, Math.Max(0, (detalles.Count - 1) / 10));
                    MostrarDetalles(); editandoLinea = false; indiceDetalleEditado = -1; CalcularTotales(); e.Handled = true;
                }
            };
            foreach (Control campo in camposMaestro.Values.Concat(camposDetalle.Values))
            {
                campo.Enter += Campo_Enter;
                campo.Leave += Campo_Leave;
                campo.MouseEnter += Campo_MouseEnter;
                campo.MouseLeave += Campo_MouseLeave;
                campo.KeyDown += Campo_KeyDown;
                campo.EnabledChanged += (s, e) => RestaurarColorCampo(campo);
            }
        }

        private void EstadoInicial()
        {
            HabilitarCampos(false); nuevo.Enabled = true;
            guardar.Enabled = editar.Enabled = eliminar.Enabled = limpiar.Enabled = false;
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
        }

        private void CargarExistente(TMaestro registro)
        {
            maestroActual = registro;
            ConfigurarCatalogoForaneaActual(registro);
            CargarObjeto(registro, camposMaestro);
            int id = Convert.ToInt32(typeof(TMaestro).GetProperty(claveMaestro).GetValue(registro, null));
            Text = "Sistema de Telefonía - " + titulo + " " + id;
            tituloFormulario.Text = titulo + " " + id;
            detalles.AddRange(negocio.ListarRelacionados<TDetalle>(claveForanea, id));
            MostrarDetalles();
            if (detalles.Count > 0) grillaDetalles.Rows[0].Selected = true;
            nuevo.Enabled = guardar.Enabled = false;
            editar.Enabled = eliminar.Enabled = limpiar.Enabled = true;
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
        }

        private void ConfigurarCatalogoForaneaActual(TMaestro registro)
        {
            if (string.IsNullOrWhiteSpace(claveForanea) || !camposDetalle.ContainsKey(claveForanea)) return;
            PropertyInfo propiedadClave = typeof(TMaestro).GetProperty(claveMaestro);
            if (propiedadClave == null) return;
            int codigo = Convert.ToInt32(propiedadClave.GetValue(registro, null));
            if (codigo <= 0) return;

            List<string> descripcion = new List<string> { titulo + " " + codigo };
            foreach (string propiedad in new[] { "NumeroFactura", "NombreCliente", "FechaVenta" })
            {
                PropertyInfo propiedadDetalle = typeof(TMaestro).GetProperty(propiedad);
                if (propiedadDetalle == null) continue;
                object valor = propiedadDetalle.GetValue(registro, null);
                if (valor == null) continue;
                descripcion.Add(valor is DateTime
                    ? ((DateTime)valor).ToString("dd/MM/yyyy")
                    : Convert.ToString(valor));
            }
            ConfigurarCatalogo(camposDetalle, claveForanea, new List<OpcionCatalogo>
            {
                new OpcionCatalogo { Codigo = codigo, Texto = string.Join(" | ", descripcion) }
            }, false);
        }

        private void HabilitarCampos(bool habilitado)
        {
            foreach (KeyValuePair<string, Control> campo in camposMaestro)
                if (campo.Key != claveMaestro && !calculadosMaestro.Contains(campo.Key)) campo.Value.Enabled = habilitado;
            foreach (KeyValuePair<string, Control> campo in camposDetalle)
                if (campo.Key != claveDetalle && campo.Key != claveForanea && !calculadosDetalle.Contains(campo.Key)) campo.Value.Enabled = habilitado;
            foreach (Control campo in camposMaestro.Values.Concat(camposDetalle.Values)) RestaurarColorCampo(campo);
        }

        private void IniciarNuevo()
        {
            maestroActual = default(TMaestro); detalles.Clear(); modoEdicion = true; editandoLinea = false; modificoCamposDetalle = false;
            indiceDetalleEditado = -1; paginaDetalle = 0;
            LimpiarCampos(camposMaestro); LimpiarCampos(camposDetalle); MostrarDetalles();
            HabilitarCampos(true); nuevo.Enabled = editar.Enabled = eliminar.Enabled = false;
            guardar.Enabled = limpiar.Enabled = true;
            EnfocarPrimerCampoDisponible();
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(maestroActual, null)) return;
            if (!modoEdicion)
            {
                modoEdicion = true; HabilitarCampos(true); nuevo.Enabled = eliminar.Enabled = false;
                guardar.Enabled = true;
                editandoLinea = false;
                editar.Enabled = limpiar.Enabled = true;
                EnfocarPrimerCampoDisponible();
                TemaVisualCrud.ActualizarBotonesModeloCrud(this);
                MessageBox.Show("Modifica los campos y pulsa Editar nuevamente para aplicar los cambios. Guardar también aplica los cambios del encabezado y sus detalles.", "Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Guardar_Click(sender, e);
        }

        private void AgregarLinea()
        {
            if (!modoEdicion) return;
            TDetalle detalle = CrearObjeto<TDetalle>(camposDetalle);
            string error = ValidarDetalle(detalle);
            if (error != null) { MessageBox.Show(error, "Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            PropertyInfo pk = typeof(TDetalle).GetProperty(claveDetalle);
            if (editandoLinea && indiceDetalleEditado >= 0)
                pk.SetValue(detalle, pk.GetValue(detalles[indiceDetalleEditado], null), null);
            if (editandoLinea && indiceDetalleEditado >= 0) detalles[indiceDetalleEditado] = detalle;
            else detalles.Add(detalle);
            editandoLinea = false; modificoCamposDetalle = false; indiceDetalleEditado = -1;
            paginaDetalle = Math.Max(0, (detalles.Count - 1) / 10);
            CalcularTotales(); MostrarDetalles(); LimpiarCampos(camposDetalle);
            EnfocarPrimerCampoDisponible(camposDetalle);
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (!ReferenceEquals(maestroActual, null) && !modoEdicion) return;
            if (editandoLinea || modificoCamposDetalle)
            {
                AgregarLinea();
                if (editandoLinea || modificoCamposDetalle) return;
            }
            if (!ValidarEncabezado()) return;
            if (editandoLinea) return;
            if (detalles.Count == 0) { MessageBox.Show("Agrega al menos una línea de detalle."); return; }
            CalcularTotales();
            try
            {
                TMaestro entidad = CrearObjeto<TMaestro>(camposMaestro);
                if (ReferenceEquals(maestroActual, null))
                    negocio.AgregarConDetalles<TMaestro, TDetalle>(entidad, detalles, claveForanea);
                else
                    negocio.EditarConDetalles<TMaestro, TDetalle>(Convert.ToInt32(typeof(TMaestro).GetProperty(claveMaestro).GetValue(maestroActual, null)), entidad, detalles, claveForanea);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "No fue posible guardar", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private bool ValidarEncabezado()
        {
            foreach (PropertyInfo propiedad in typeof(TMaestro).GetProperties())
            {
                if (propiedad.Name == claveMaestro || calculadosMaestro.Contains(propiedad.Name)) continue;
                Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
                Control campo;
                if (tipo == typeof(int) && camposMaestro.TryGetValue(propiedad.Name, out campo))
                {
                    ComboBox combo = campo as ComboBox;
                    if (combo != null && Convert.ToInt32(combo.SelectedValue ?? 0) <= 0)
                    { MessageBox.Show("Selecciona " + Etiqueta(propiedad.Name).TrimEnd(':') + "."); combo.Focus(); return false; }
                    NumericUpDown numero = campo as NumericUpDown;
                    if (numero != null && numero.Value <= 0) { MessageBox.Show("Indica un valor válido para " + Etiqueta(propiedad.Name).TrimEnd(':') + "."); numero.Focus(); return false; }
                }
                if (tipo == typeof(string) && (propiedad.Name == "NumeroFactura" || propiedad.Name == "NombreCliente"))
                {
                    TextBox texto = camposMaestro[propiedad.Name] as TextBox;
                    if (texto == null || string.IsNullOrWhiteSpace(texto.Text)) { MessageBox.Show("El campo " + Etiqueta(propiedad.Name).TrimEnd(':') + " es obligatorio."); if (texto != null) texto.Focus(); return false; }
                }
            }
            return true;
        }

        private string ValidarDetalle(TDetalle detalle)
        {
            PropertyInfo equipo = typeof(TDetalle).GetProperty("CodigoEquipo");
            if (equipo != null && Convert.ToInt32(equipo.GetValue(detalle, null)) <= 0) return "Indica un código de equipo válido.";
            PropertyInfo descripcion = typeof(TDetalle).GetProperty("Descripcion");
            if (descripcion != null && string.IsNullOrWhiteSpace(Convert.ToString(descripcion.GetValue(detalle, null)))) return "La descripción del detalle es obligatoria.";
            PropertyInfo cantidad = typeof(TDetalle).GetProperty("Cantidad");
            if (cantidad != null && Convert.ToInt32(cantidad.GetValue(detalle, null)) <= 0) return "La cantidad debe ser mayor que cero.";
            PropertyInfo precio = typeof(TDetalle).GetProperty("PrecioUnitario") ?? typeof(TDetalle).GetProperty("CostoUnitario");
            if (precio != null && Convert.ToDecimal(precio.GetValue(detalle, null)) <= 0) return "El precio o costo unitario debe ser mayor que cero.";
            return null;
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(maestroActual, null) || modoEdicion) return;
            int id = Convert.ToInt32(typeof(TMaestro).GetProperty(claveMaestro).GetValue(maestroActual, null));
            if (MessageBox.Show("¿Deseas inactivar " + titulo.ToLowerInvariant() + " " + id + " y sus detalles?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try { negocio.EliminarConDetalles<TMaestro, TDetalle>(id, claveForanea); DialogResult = DialogResult.OK; Close(); }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "No fue posible inactivar", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Limpiar_Click(object sender, EventArgs e)
        {
            if (!ReferenceEquals(maestroActual, null)) { DialogResult = DialogResult.Cancel; Close(); return; }
            maestroActual = default(TMaestro); detalles.Clear(); modoEdicion = editandoLinea = false; modificoCamposDetalle = false;
            paginaDetalle = 0; indiceDetalleEditado = -1;
            LimpiarCampos(camposMaestro); LimpiarCampos(camposDetalle); MostrarDetalles(); HabilitarCampos(false); EstadoInicial();
        }

        private void CargarObjeto(object objeto, Dictionary<string, Control> campos)
        {
            cargandoObjeto = true;
            foreach (KeyValuePair<string, Control> campo in campos)
            {
                PropertyInfo p = objeto.GetType().GetProperty(campo.Key); object valor = p.GetValue(objeto, null);
                NumericUpDown n = campo.Value as NumericUpDown; if (n != null) { n.Value = Math.Min(n.Maximum, Math.Max(n.Minimum, valor == null ? 0 : Convert.ToDecimal(valor))); continue; }
                TextBox t = campo.Value as TextBox; if (t != null) { t.Text = valor == null ? "" : Convert.ToString(valor); continue; }
                DateTimePicker d = campo.Value as DateTimePicker; if (d != null) { DateTime fecha = valor == null ? DateTime.Today : Convert.ToDateTime(valor); d.Value = fecha < d.MinDate ? d.MinDate : fecha > d.MaxDate ? d.MaxDate : fecha; continue; }
                ComboBox c = campo.Value as ComboBox;
                if (c != null)
                {
                    if (string.Equals(Convert.ToString(c.Tag), "Catalogo", StringComparison.Ordinal))
                    {
                        int codigo = valor == null ? 0 : Convert.ToInt32(valor);
                        try { c.SelectedValue = codigo; } catch (ArgumentException) { c.SelectedIndex = -1; }
                    }
                    else c.SelectedIndex = valor != null && Convert.ToBoolean(valor) ? 0 : 1;
                }
            }
            cargandoObjeto = false;
        }

        private void MarcarDetalleModificado()
        {
            if (modoEdicion && !cargandoObjeto) modificoCamposDetalle = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Enter) && modoEdicion)
            {
                AgregarLinea();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private T CrearObjeto<T>(Dictionary<string, Control> campos) where T : new()
        {
            T objeto = new T();
            foreach (KeyValuePair<string, Control> campo in campos)
            {
                PropertyInfo p = typeof(T).GetProperty(campo.Key); Type nullable = Nullable.GetUnderlyingType(p.PropertyType); Type tipo = nullable ?? p.PropertyType;
                NumericUpDown n = campo.Value as NumericUpDown;
                if (n != null) { object valor = nullable != null && n.Value == 0 ? null : Convert.ChangeType(n.Value, tipo); p.SetValue(objeto, valor, null); continue; }
                TextBox t = campo.Value as TextBox; if (t != null) { p.SetValue(objeto, t.Text.Trim(), null); continue; }
                DateTimePicker d = campo.Value as DateTimePicker; if (d != null) { p.SetValue(objeto, d.Value, null); continue; }
                ComboBox c = campo.Value as ComboBox;
                if (c != null)
                {
                    if (string.Equals(Convert.ToString(c.Tag), "Catalogo", StringComparison.Ordinal))
                    {
                        object seleccionado = c.SelectedValue;
                        int codigo = seleccionado == null ? 0 : Convert.ToInt32(seleccionado);
                        p.SetValue(objeto, nullable != null && codigo == 0 ? null : Convert.ChangeType(codigo, tipo), null);
                    }
                    else p.SetValue(objeto, c.SelectedIndex == 0, null);
                }
            }
            return objeto;
        }

        private void LimpiarCampos(Dictionary<string, Control> campos)
        {
            bool estabaCargando = cargandoObjeto;
            cargandoObjeto = true;
            try
            {
                foreach (Control c in campos.Values)
                {
                    NumericUpDown n = c as NumericUpDown; if (n != null) { n.Value = n.Name == "Cantidad" ? 1 : 0; continue; }
                    TextBox t = c as TextBox; if (t != null) { t.Clear(); continue; }
                    DateTimePicker d = c as DateTimePicker; if (d != null) { d.Value = DateTime.Today; continue; }
                    ComboBox combo = c as ComboBox;
                    if (combo != null)
                    {
                        if (string.Equals(Convert.ToString(combo.Tag), "Catalogo", StringComparison.Ordinal))
                        {
                            combo.SelectedValue = 0;
                        }
                        else combo.SelectedIndex = 0;
                    }
                }
            }
            finally { cargandoObjeto = estabaCargando; }
        }

        private void MostrarDetalles()
        {
            int totalPaginas = (int)Math.Ceiling(detalles.Count / 10.0);
            paginaDetalle = totalPaginas == 0 ? 0 : Math.Min(paginaDetalle, totalPaginas - 1);
            List<TDetalle> filas = detalles.Skip(paginaDetalle * 10).Take(10).ToList();
            grillaDetalles.DataSource = null;
            grillaDetalles.DataSource = filas;
            for (int i = 0; i < grillaDetalles.Columns.Count; i++)
            {
                DataGridViewColumn columna = grillaDetalles.Columns[i];
                columna.HeaderText = Etiqueta(columna.DataPropertyName).TrimEnd(':');
                columna.MinimumWidth = 90;
                columna.FillWeight = Math.Max(65, 130 - columna.HeaderText.Length);
            }
            lblConteoDetalles.Text = "Resultados: " + detalles.Count;
            lblPaginaDetalles.Text = "Página " + (totalPaginas == 0 ? 0 : paginaDetalle + 1) + " de " + totalPaginas;
            btnPaginaAnteriorDetalles.Enabled = paginaDetalle > 0;
            btnPaginaSiguienteDetalles.Enabled = paginaDetalle + 1 < totalPaginas;
            TemaVisualCrud.ActualizarBotonesModeloCrud(this);
            if (filas.Count > 0 && indiceDetalleEditado >= paginaDetalle * 10 && indiceDetalleEditado < paginaDetalle * 10 + filas.Count)
                grillaDetalles.Rows[indiceDetalleEditado - paginaDetalle * 10].Selected = true;
        }

        private void CambiarPaginaDetalles(int desplazamiento)
        {
            int paginas = (int)Math.Ceiling(detalles.Count / 10.0);
            int destino = paginaDetalle + desplazamiento;
            if (destino < 0 || destino >= paginas) return;
            paginaDetalle = destino;
            indiceDetalleEditado = -1;
            MostrarDetalles();
        }

        private void AjustarPaginacionDetalles()
        {
            Panel panel = ControlesDescendientes<Panel>(this).FirstOrDefault(x => x.Name == "pnlPaginacionDetalles");
            if (panel == null || panel.ClientSize.Width <= 0) return;
            const int margen = 10;
            int y = Math.Max(0, (panel.ClientSize.Height - 32) / 2);
            int siguienteX = panel.ClientSize.Width - margen - btnPaginaSiguienteDetalles.Width;
            int paginaX = siguienteX - margen - lblPaginaDetalles.Width;
            int anteriorX = paginaX - margen - btnPaginaAnteriorDetalles.Width;
            btnPaginaSiguienteDetalles.Location = new Point(siguienteX, y);
            lblPaginaDetalles.Location = new Point(paginaX, y);
            btnPaginaAnteriorDetalles.Location = new Point(anteriorX, y);
            lblConteoDetalles.Location = new Point(margen, y);
            lblConteoDetalles.Size = new Size(Math.Max(0, anteriorX - margen * 2), 32);
        }

        private int ObtenerIndiceDetalleSeleccionado()
        {
            if (grillaDetalles.CurrentRow == null) return -1;
            TDetalle seleccionado = grillaDetalles.CurrentRow.DataBoundItem is TDetalle
                ? (TDetalle)grillaDetalles.CurrentRow.DataBoundItem : default(TDetalle);
            return ReferenceEquals(seleccionado, null) ? -1 : detalles.IndexOf(seleccionado);
        }

        private IEnumerable<Control> ObtenerOrdenVisual(Dictionary<string, Control> campos, TableLayoutPanel tabla)
        {
            return campos.Values.OrderBy(c => tabla.GetRow(c)).ThenBy(c => tabla.GetColumn(c));
        }

        private void Campo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Alt || e.Control || e.Shift) return;
            ComboBox combo = sender as ComboBox;
            if (combo != null && combo.DroppedDown) return;
            Control actual = sender as Control;
            if (actual == null || !ValidarCampoAntesDeAvanzar(actual)) return;
            e.SuppressKeyPress = true;
            e.Handled = true;

            List<Control> orden = ObtenerOrdenVisual(camposMaestro, tablaCamposMaestro)
                .Concat(ObtenerOrdenVisual(camposDetalle, tablaCamposDetalle))
                .Where(c => c.Enabled && c.Visible && !c.IsDisposed).ToList();
            int indice = orden.IndexOf(actual);
            for (int i = indice + 1; indice >= 0 && i < orden.Count; i++)
            {
                if (orden[i].Focus()) return;
            }
        }

        private bool ValidarCampoAntesDeAvanzar(Control campo)
        {
            if (campo.Name == "NumeroFactura" || campo.Name == "NombreCliente" || campo.Name == "Descripcion")
            {
                TextBox texto = campo as TextBox;
                if (texto != null && string.IsNullOrWhiteSpace(texto.Text))
                {
                    MessageBox.Show("El campo " + Etiqueta(campo.Name).TrimEnd(':') + " es obligatorio.");
                    campo.Focus();
                    return false;
                }
            }
            if (campo is ComboBox && Convert.ToString(campo.Tag) == "Catalogo" &&
                (campo.Name == "CodigoVenta" || campo.Name == "CodigoEquipo"))
            {
                ComboBox lista = (ComboBox)campo;
                if (Convert.ToInt32(lista.SelectedValue ?? 0) <= 0)
                {
                    MessageBox.Show("Selecciona " + Etiqueta(campo.Name).TrimEnd(':') + ".");
                    lista.Focus();
                    return false;
                }
            }
            if (campo.Name == "Cantidad" && campo is NumericUpDown && ((NumericUpDown)campo).Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero.");
                campo.Focus();
                return false;
            }
            return true;
        }

        private void EnfocarPrimerCampoDisponible(Dictionary<string, Control> soloCampos = null)
        {
            IEnumerable<Control> orden = soloCampos == camposDetalle
                ? ObtenerOrdenVisual(camposDetalle, tablaCamposDetalle)
                : ObtenerOrdenVisual(camposMaestro, tablaCamposMaestro).Concat(ObtenerOrdenVisual(camposDetalle, tablaCamposDetalle));
            Control primerCampo = orden.FirstOrDefault(c => c.Enabled && c.Visible && !c.IsDisposed);
            if (primerCampo != null) primerCampo.Focus();
        }

        private void Campo_Enter(object sender, EventArgs e)
        {
            Control campo = sender as Control;
            if (campo != null && campo.Enabled)
            {
                campo.BackColor = cianActivo;
                campo.ForeColor = Color.FromArgb(7, 35, 66);
            }
        }

        private void Campo_Leave(object sender, EventArgs e) { RestaurarColorCampo(sender as Control); }

        private void Campo_MouseEnter(object sender, EventArgs e)
        {
            Control campo = sender as Control;
            if (campo != null && campo.Enabled && !campo.Focused)
            {
                campo.BackColor = cianHover;
                campo.ForeColor = Color.FromArgb(7, 35, 66);
            }
        }

        private void Campo_MouseLeave(object sender, EventArgs e) { RestaurarColorCampo(sender as Control); }

        private void RestaurarColorCampo(Control campo)
        {
            if (campo == null) return;
            campo.BackColor = campo.Enabled ? VerdeCaptura : AzulAutomatico;
            campo.ForeColor = Color.FromArgb(7, 35, 66);
        }

        private void CalcularTotales()
        {
            Type tipoMaestro = typeof(TMaestro); Type tipoDetalle = typeof(TDetalle);
            PropertyInfo cantidad = tipoDetalle.GetProperty("Cantidad");
            PropertyInfo precio = tipoDetalle.GetProperty("PrecioUnitario") ?? tipoDetalle.GetProperty("CostoUnitario");
            PropertyInfo descuento = tipoDetalle.GetProperty("Descuento");
            PropertyInfo subtotalDetalle = tipoDetalle.GetProperty("Subtotal");
            PropertyInfo ivaDetalle = tipoDetalle.GetProperty("Iva");
            decimal subtotal = 0m, iva = 0m;
            foreach (TDetalle detalle in detalles)
            {
                if (cantidad != null && precio != null && subtotalDetalle != null)
                {
                    decimal linea = Convert.ToDecimal(cantidad.GetValue(detalle, null)) * Convert.ToDecimal(precio.GetValue(detalle, null));
                    if (descuento != null) linea -= Convert.ToDecimal(descuento.GetValue(detalle, null));
                    linea = Math.Max(0m, linea);
                    subtotalDetalle.SetValue(detalle, linea, null);
                    if (ivaDetalle != null) { decimal ivaLinea = decimal.Round(linea * TasaIvaGuatemala, 2, MidpointRounding.AwayFromZero); ivaDetalle.SetValue(detalle, ivaLinea, null); iva += ivaLinea; }
                    subtotal += linea;
                }
            }
            AsignarImporte("Subtotal", subtotal);
            if (tipoMaestro.GetProperty("Iva") != null) AsignarImporte("Iva", decimal.Round(subtotal * TasaIvaGuatemala, 2, MidpointRounding.AwayFromZero));
            decimal ivaMaestro = tipoMaestro.GetProperty("Iva") == null ? iva : decimal.Round(subtotal * TasaIvaGuatemala, 2, MidpointRounding.AwayFromZero);
            AsignarImporte("Total", subtotal + ivaMaestro);
        }

        private void AsignarImporte(string nombre, decimal valor)
        {
            Control c; if (!camposMaestro.TryGetValue(nombre, out c)) return;
            NumericUpDown n = c as NumericUpDown; if (n != null) n.Value = Math.Min(n.Maximum, Math.Max(n.Minimum, valor));
        }

        private static Panel Seccion(string texto, int alto)
        {
            Panel panel = new Panel { BackColor = AzulPanel, Height = alto, Padding = new Padding(12) };
            panel.Controls.Add(new Label { Text = texto, Dock = DockStyle.Top, Height = 30, ForeColor = Color.FromArgb(0, 190, 230), Font = new Font("Segoe UI", 11F, FontStyle.Bold) }); return panel;
        }
        private static string Etiqueta(string nombre) { return System.Text.RegularExpressions.Regex.Replace(nombre, "([a-z])([A-Z])", "$1 $2") + ":"; }
        private static Button Boton(string nombre, string texto, Color color)
        {
            Button boton = new Button
            {
                Name = nombre, Text = texto, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), UseVisualStyleBackColor = false, Cursor = Cursors.Hand
            };
            boton.FlatAppearance.BorderSize = 0;
            CampoRedondeado.Aplicar(boton, 8);
            return boton;
        }
    }
}
