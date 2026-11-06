using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Captura los datos de Compras arriba y sus DetalleCompra abajo.</summary>
    internal sealed class FrmCompraCompleta : Form
    {
        private sealed class OpcionCombo
        {
            public int Codigo { get; set; }
            public string Texto { get; set; }
        }

        private static readonly Color ColorCampoEditable = Color.FromArgb(232, 248, 232);
        private static readonly Color ColorCampoAutomatico = Color.FromArgb(220, 230, 240);
        private readonly ComprasNegocio negocio = new ComprasNegocio();
        private readonly ProveedoresNegocio proveedoresNegocio = new ProveedoresNegocio();
        private readonly CrudRelacionalNegocio catalogosNegocio = new CrudRelacionalNegocio();
        private readonly List<DetalleCompraEntidad> detalles = new List<DetalleCompraEntidad>();
        private List<ProveedoresEntidad> proveedores = new List<ProveedoresEntidad>();
        private List<EquiposEntidad> equipos = new List<EquiposEntidad>();
        private bool cargandoEquipo;

        // Campos del encabezado de Compras.
        private readonly TextBox txtCodigoCompra = CajaSoloLectura("Automático");
        private readonly ComboBox cmbProveedor = CrearCombo();
        private readonly TextBox txtDocumento = new TextBox();
        private readonly DateTimePicker dtpFecha = new DateTimePicker();
        private readonly NumericUpDown nudSubtotalCompra = Numero(0, 999999999, 0);
        private readonly NumericUpDown nudIva = Numero(0, 999999999, 0);
        private readonly NumericUpDown nudTotalCompra = Numero(0, 999999999, 0);
        private readonly TextBox txtObservacionesCompra = new TextBox();
        private readonly RadioButton rdoCompraActiva = Radio("Activo", true);
        private readonly RadioButton rdoCompraInactiva = Radio("Inactivo", false);

        // Campos del detalle; el código de detalle y el código de compra se asignan al guardar.
        private readonly TextBox txtCodigoDetalle = CajaSoloLectura("Automático");
        private readonly ComboBox cmbCodigoCompraDetalle = CrearCombo();
        private readonly ComboBox cmbEquipo = CrearCombo();
        private readonly NumericUpDown nudCantidad = Numero(1, 1000000, 1);
        private readonly NumericUpDown nudCosto = Numero(0, 999999999, 0);
        private readonly NumericUpDown nudSubtotalDetalle = Numero(0, 999999999, 0);
        private readonly TextBox txtObservacionesDetalle = new TextBox();
        private readonly RadioButton rdoDetalleActivo = Radio("Activo", true);
        private readonly RadioButton rdoDetalleInactivo = Radio("Inactivo", false);

        private readonly ListBox lstDetalles = new ListBox();
        private readonly Button btnNuevo = Boton("Nuevo", Color.FromArgb(30, 135, 220));
        private readonly Button btnGuardar = Boton("Guardar", Color.FromArgb(0, 150, 110));
        private readonly Button btnEditarCompra = Boton("Editar", Color.FromArgb(230, 145, 25));
        private readonly Button btnEliminarCompra = Boton("Eliminar", Color.FromArgb(205, 55, 65));
        private readonly Button btnLimpiar = Boton("Limpiar", Color.FromArgb(75, 100, 125));
        private readonly Label lblAyudaDetalles = new Label
        {
            Text = "Selecciona o haz doble clic en una línea para editarla. Guardar o Editar otra vez aplica los cambios. Ctrl+Enter agrega una línea; Supr la quita.",
            ForeColor = Color.FromArgb(170, 200, 225), Location = new Point(14, 218), AutoSize = true
        };
        private bool editandoDetalle;
        private bool modoEdicion;
        private bool compraSinDetallesRegistrados;
        private ComprasEntidad compraEnEdicion;

        public FrmCompraCompleta() : this(null) { }

        public FrmCompraCompleta(ComprasEntidad compra)
        {
            Text = "Sistema de Telefonía - Nueva compra";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(1000, 780);
            Size = new Size(1260, 940);
            BackColor = Color.FromArgb(3, 22, 43);
            Font = new Font("Segoe UI", 9F);
            KeyPreview = true;
            ConstruirInterfaz();
            CargarCatalogos();
            ConectarEventos();
            EstablecerEstadoInicial();
            if (compra != null) CargarCompraExistente(compra);
        }

        private void ConstruirInterfaz()
        {
            Panel contenido = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(16) };
            Controls.Add(contenido);

            // Usa el mismo alto de encabezado que Nueva venta para que no se recorte el título.
            Panel encabezado = new Panel { Dock = DockStyle.Top, Height = 88, BackColor = Color.FromArgb(3, 43, 83), Padding = new Padding(22, 10, 22, 8) };
            encabezado.Controls.Add(new Label { Text = "Nueva compra", ForeColor = Color.White, Font = new Font("Segoe UI", 20F, FontStyle.Bold), Dock = DockStyle.Top, Height = 42 });
            encabezado.Controls.Add(new Label { Text = "Ingresa la compra y sus equipos en una sola operación", ForeColor = Color.FromArgb(170, 200, 225), Dock = DockStyle.Bottom, Height = 24 });
            Controls.Add(encabezado);

            Panel pie = new Panel { Dock = DockStyle.Bottom, Height = 64, BackColor = Color.FromArgb(7, 35, 66), Padding = new Padding(12) };
            FlowLayoutPanel acciones = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
            foreach (Button boton in new[] { btnLimpiar, btnEliminarCompra, btnEditarCompra, btnGuardar, btnNuevo })
            {
                boton.Width = 135;
                boton.Height = 42;
                boton.Margin = new Padding(5, 0, 5, 0);
                acciones.Controls.Add(boton);
            }
            pie.Controls.Add(acciones);
            Controls.Add(pie);

            // PanelCompra conserva arriba todos los atributos de Tbl_Compras.
            Panel panelCompra = Seccion("Información de la compra", 290);
            panelCompra.SetBounds(16, 10, Math.Max(500, contenido.ClientSize.Width - 32), 290);
            panelCompra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            contenido.Controls.Add(panelCompra);
            TableLayoutPanel camposCompra = CrearTabla(5, 4, 43);
            camposCompra.SetBounds(12, 42, panelCompra.ClientSize.Width - 24, 225);
            camposCompra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AgregarPar(camposCompra, 0, "Código de compra:", txtCodigoCompra, "Proveedor:", cmbProveedor);
            AgregarPar(camposCompra, 1, "Número de documento:", txtDocumento, "Fecha de compra:", dtpFecha);
            AgregarPar(camposCompra, 2, "Subtotal:", nudSubtotalCompra, "IVA (12%):", nudIva);
            AgregarPar(camposCompra, 3, "Total:", nudTotalCompra, "Observaciones:", txtObservacionesCompra);
            AgregarPar(camposCompra, 4, "Estado:", PanelRadios(rdoCompraActiva, rdoCompraInactiva), "", new Label());
            panelCompra.Controls.Add(camposCompra);

            // PanelDetalle conserva abajo los atributos de Tbl_DetalleCompra.
            Panel panelDetalle = Seccion("Detalle de la compra", 500);
            panelDetalle.SetBounds(16, 322, Math.Max(500, contenido.ClientSize.Width - 32), 500);
            panelDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            contenido.Controls.Add(panelDetalle);

            TableLayoutPanel camposDetalle = CrearTabla(4, 4, 42);
            camposDetalle.SetBounds(12, 42, panelDetalle.ClientSize.Width - 24, 168);
            camposDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AgregarPar(camposDetalle, 0, "Código de detalle:", txtCodigoDetalle, "Compra relacionada:", cmbCodigoCompraDetalle);
            AgregarPar(camposDetalle, 1, "Equipo:", cmbEquipo, "Cantidad:", nudCantidad);
            AgregarPar(camposDetalle, 2, "Costo unitario:", nudCosto, "Subtotal:", nudSubtotalDetalle);
            AgregarPar(camposDetalle, 3, "Observaciones:", txtObservacionesDetalle, "Estado:", PanelRadios(rdoDetalleActivo, rdoDetalleInactivo));
            panelDetalle.Controls.Add(camposDetalle);

            panelDetalle.Controls.Add(lblAyudaDetalles);
            lstDetalles.SetBounds(12, 242, panelDetalle.ClientSize.Width - 24, 243);
            lstDetalles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstDetalles.IntegralHeight = false;
            lstDetalles.ItemHeight = 30;
            lstDetalles.Font = new Font("Segoe UI", 10F);
            lstDetalles.BackColor = Color.White;
            lstDetalles.BorderStyle = BorderStyle.FixedSingle;
            panelDetalle.Controls.Add(lstDetalles);

            contenido.SizeChanged += (s, e) =>
            {
                int ancho = Math.Max(500, contenido.ClientSize.Width - 32);
                panelCompra.Width = ancho;
                panelDetalle.Width = ancho;
                camposCompra.Width = panelCompra.ClientSize.Width - 24;
                camposDetalle.Width = panelDetalle.ClientSize.Width - 24;
                lstDetalles.Width = panelDetalle.ClientSize.Width - 24;
            };

            foreach (Control control in new Control[] { cmbProveedor, txtDocumento, dtpFecha, nudSubtotalCompra, nudIva, nudTotalCompra, txtObservacionesCompra,
                cmbEquipo, nudCantidad, nudCosto, nudSubtotalDetalle, txtObservacionesDetalle, txtCodigoCompra, txtCodigoDetalle, cmbCodigoCompraDetalle })
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(3, 5, 8, 5);
                control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            }
            foreach (Control control in new Control[] { nudSubtotalCompra, nudIva, nudTotalCompra, nudSubtotalDetalle })
            {
                ((NumericUpDown)control).ReadOnly = true;
                control.BackColor = ColorCampoAutomatico;
            }
            foreach (Control control in new Control[] { cmbProveedor, txtDocumento, dtpFecha, txtObservacionesCompra,
                cmbEquipo, nudCantidad, nudCosto, txtObservacionesDetalle })
            {
                control.BackColor = ColorCampoEditable;
                control.ForeColor = Color.FromArgb(25, 40, 35);
            }
            foreach (RadioButton opcion in new[] { rdoCompraActiva, rdoCompraInactiva, rdoDetalleActivo, rdoDetalleInactivo })
                opcion.BackColor = ColorCampoEditable;
            txtCodigoCompra.BackColor = txtCodigoDetalle.BackColor = cmbCodigoCompraDetalle.BackColor = ColorCampoAutomatico;
            txtDocumento.MaxLength = 50;
            txtObservacionesCompra.MaxLength = 200;
            txtObservacionesDetalle.MaxLength = 200;
            nudCosto.DecimalPlaces = nudSubtotalCompra.DecimalPlaces = nudIva.DecimalPlaces = nudTotalCompra.DecimalPlaces = nudSubtotalDetalle.DecimalPlaces = 2;
            dtpFecha.Format = DateTimePickerFormat.Short;
        }

        private void CargarCatalogos()
        {
            try
            {
                proveedores = proveedoresNegocio.Listar().Where(p => p.Estado).ToList();
                equipos = catalogosNegocio.Listar<EquiposEntidad>().Where(e => e.Estado).ToList();
                EnlazarCombo(cmbProveedor, proveedores, "Nombre", "CodigoProveedor");
                EnlazarCombo(cmbEquipo, equipos, "Modelo", "CodigoEquipo");
                EnlazarCompraRelacionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al cargar proveedores y equipos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void EnlazarCombo(ComboBox combo, object origen, string mostrar, string valor)
        {
            List<OpcionCombo> opciones = new List<OpcionCombo>
            {
                new OpcionCombo { Codigo = 0, Texto = "- Seleccione -" }
            };
            IEnumerable registros = origen as IEnumerable;
            if (registros != null)
            {
                foreach (object registro in registros)
                {
                    if (registro == null) continue;
                    object id = registro.GetType().GetProperty(valor).GetValue(registro, null);
                    object descripcion = registro.GetType().GetProperty(mostrar).GetValue(registro, null);
                    int codigo = Convert.ToInt32(id, CultureInfo.InvariantCulture);
                    opciones.Add(new OpcionCombo
                    {
                        Codigo = codigo,
                        Texto = codigo.ToString("00", CultureInfo.InvariantCulture) + " - "
                            + Convert.ToString(descripcion, CultureInfo.CurrentCulture)
                    });
                }
            }
            combo.DataSource = null;
            combo.DisplayMember = "Texto";
            combo.ValueMember = "Codigo";
            combo.DataSource = opciones;
            combo.SelectedValue = 0;
        }

        private void AsegurarProveedorDisponible(int codigoProveedor)
        {
            if (proveedores.Any(p => p.CodigoProveedor == codigoProveedor)) return;
            ProveedoresEntidad proveedor = proveedoresNegocio.Listar().FirstOrDefault(p => p.CodigoProveedor == codigoProveedor);
            if (proveedor == null) return;
            proveedores.Add(proveedor);
            EnlazarCombo(cmbProveedor, proveedores, "Nombre", "CodigoProveedor");
        }

        private void EnlazarCompraRelacionada()
        {
            IEnumerable<ComprasEntidad> opciones = compraEnEdicion == null
                ? Enumerable.Empty<ComprasEntidad>()
                : new[] { compraEnEdicion };
            EnlazarCombo(cmbCodigoCompraDetalle, opciones, "NumeroDocumento", "CodigoCompra");
            cmbCodigoCompraDetalle.Enabled = false;
        }

        private void AsegurarEquipoDisponible(int codigoEquipo)
        {
            if (equipos.Any(e => e.CodigoEquipo == codigoEquipo)) return;
            EquiposEntidad equipo = catalogosNegocio.Listar<EquiposEntidad>().FirstOrDefault(e => e.CodigoEquipo == codigoEquipo);
            if (equipo == null) return;
            equipos.Add(equipo);
            EnlazarCombo(cmbEquipo, equipos, "Modelo", "CodigoEquipo");
        }

        private static int CodigoSeleccionado(ComboBox combo)
        {
            int codigo;
            return combo.SelectedValue != null && int.TryParse(Convert.ToString(combo.SelectedValue), out codigo) ? codigo : 0;
        }

        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => IniciarNuevo();
            btnGuardar.Click += Guardar_Click;
            btnEditarCompra.Click += Editar_Click;
            btnEliminarCompra.Click += EliminarCompra_Click;
            btnLimpiar.Click += Limpiar_Click;
            lstDetalles.DoubleClick += EditarDetalle_Click;
            lstDetalles.SelectedIndexChanged += (s, e) =>
            {
                // Muestra en el editor los campos de la línea seleccionada, incluso al consultar.
                if (lstDetalles.SelectedIndex >= 0)
                {
                    // Al editar, la línea seleccionada queda vinculada al editor para actualizar su PK.
                    editandoDetalle = modoEdicion;
                    CargarDetalleEnCampos(lstDetalles.SelectedIndex);
                }
                // La selección de una línea solo carga sus datos; no cambia los
                // botones de la compra. Editar siempre corresponde al encabezado.
            };
            lstDetalles.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete && modoEdicion && lstDetalles.SelectedIndex >= 0)
                {
                    EliminarDetalleSeleccionado();
                    e.Handled = true;
                }
            };
            nudCantidad.ValueChanged += (s, e) => ActualizarSubtotalDetalle();
            nudCosto.ValueChanged += (s, e) => ActualizarSubtotalDetalle();
            cmbEquipo.SelectedValueChanged += EquipoSeleccionado;
        }

        private void EquipoSeleccionado(object sender, EventArgs e)
        {
            if (cargandoEquipo || !modoEdicion) return;
            int codigoEquipo = CodigoSeleccionado(cmbEquipo);
            if (codigoEquipo <= 0)
            {
                nudCosto.Value = 0;
                ActualizarSubtotalDetalle();
                return;
            }

            try
            {
                decimal? ultimoCosto = negocio.ObtenerUltimoCostoPorEquipo(codigoEquipo);
                if (ultimoCosto.HasValue)
                {
                    nudCosto.Value = Math.Min(nudCosto.Maximum, Math.Max(nudCosto.Minimum, ultimoCosto.Value));
                    lblAyudaDetalles.Text = "Costo sugerido: último costo de compra registrado. Puedes ajustarlo al valor de la factura actual.";
                }
                else
                {
                    nudCosto.Value = 0;
                    lblAyudaDetalles.Text = "Este equipo no tiene compras previas. Ingresa manualmente el costo unitario de esta factura.";
                }
                ActualizarSubtotalDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar el costo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EstablecerEstadoInicial()
        {
            modoEdicion = false;
            editandoDetalle = false;
            HabilitarCaptura(false);
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = btnEditarCompra.Enabled = btnEliminarCompra.Enabled = btnLimpiar.Enabled = false;
        }

        private void CargarCompraExistente(ComprasEntidad compra)
        {
            try
            {
                compraEnEdicion = compra;
                txtCodigoCompra.Text = compra.CodigoCompra.ToString();
                EnlazarCompraRelacionada();
                AsegurarProveedorDisponible(compra.CodigoProveedor);
                cmbProveedor.SelectedValue = compra.CodigoProveedor;
                txtDocumento.Text = compra.NumeroDocumento;
                dtpFecha.Value = compra.FechaCompra < dtpFecha.MinDate ? dtpFecha.MinDate :
                    compra.FechaCompra > dtpFecha.MaxDate ? dtpFecha.MaxDate : compra.FechaCompra;
                nudIva.Value = Math.Min(nudIva.Maximum, Math.Max(nudIva.Minimum, compra.Iva));
                nudSubtotalCompra.Value = Math.Min(nudSubtotalCompra.Maximum, Math.Max(nudSubtotalCompra.Minimum, compra.Subtotal));
                nudTotalCompra.Value = Math.Min(nudTotalCompra.Maximum, Math.Max(nudTotalCompra.Minimum, compra.Total));
                txtObservacionesCompra.Text = compra.Observaciones ?? string.Empty;
                rdoCompraActiva.Checked = compra.Estado;
                rdoCompraInactiva.Checked = !compra.Estado;
                detalles.AddRange(negocio.ListarDetalles(compra.CodigoCompra));
                foreach (DetalleCompraEntidad detalle in detalles)
                    AsegurarEquipoDisponible(detalle.CodigoEquipo);
                compraSinDetallesRegistrados = detalles.Count == 0;
                if (compraSinDetallesRegistrados)
                    lblAyudaDetalles.Text = "Esta compra no tiene líneas en DetalleCompra. Puedes editar sus datos; al añadir equipos, los totales se recalcularán.";
                MostrarDetalles();
                if (detalles.Count > 0)
                    lstDetalles.SelectedIndex = 0;
                ActualizarTotales();

                Text = "Sistema de Telefonía - Compra " + compra.CodigoCompra;
                Panel cabecera = Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Top);
                Label titulo = cabecera == null ? null : cabecera.Controls.OfType<Label>().FirstOrDefault();
                if (titulo != null) titulo.Text = "Compra " + compra.CodigoCompra;
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnEditarCompra.Enabled = true;
                btnEliminarCompra.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "No fue posible abrir la compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void HabilitarEdicionCompra()
        {
            if (compraEnEdicion == null) return;
            modoEdicion = true;
            HabilitarCaptura(true);
            editandoDetalle = lstDetalles.SelectedIndex >= 0;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = btnLimpiar.Enabled = true;
            btnEliminarCompra.Enabled = false;
            // Editar queda disponible para aplicar el cambio en el segundo clic.
            btnEditarCompra.Enabled = true;
            cmbProveedor.Focus();
            MessageBox.Show("Modifica los campos y pulsa Editar nuevamente para aplicar los cambios. Guardar también aplica los cambios de la compra y sus detalles.",
                "Edición de compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            if (compraEnEdicion != null && !modoEdicion)
            {
                HabilitarEdicionCompra();
                return;
            }
            if (compraEnEdicion != null && modoEdicion)
            {
                Guardar_Click(sender, e);
                return;
            }
        }

        private void EliminarCompra_Click(object sender, EventArgs e)
        {
            if (compraEnEdicion == null || modoEdicion) return;
            DialogResult respuesta = MessageBox.Show(
                "¿Marcar como inactiva la compra " + compraEnEdicion.CodigoCompra + " y sus detalles?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                negocio.Eliminar(compraEnEdicion.CodigoCompra);
                MessageBox.Show("La compra y sus detalles quedaron inactivos.", "Compra inactiva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CerrarYRegresarAlDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar la compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IniciarNuevo()
        {
            LimpiarFormulario(false);
            HabilitarCaptura(true);
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = btnLimpiar.Enabled = true;
            btnEditarCompra.Enabled = btnEliminarCompra.Enabled = false;
            modoEdicion = true;
            cmbProveedor.Focus();
        }

        private void Limpiar_Click(object sender, EventArgs e)
        {
            // Si se abrió desde una compra seleccionada, Limpiar cancela la edición
            // y cierra este diálogo para regresar al dashboard sin guardar cambios.
            if (compraEnEdicion != null)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            // En una compra nueva, limpia los datos y vuelve al estado bloqueado.
            LimpiarFormulario(true);
        }

        private bool AgregarDetalleTemporal()
        {
            int codigoEquipo = CodigoSeleccionado(cmbEquipo);
            if (codigoEquipo <= 0 || nudCantidad.Value <= 0 || nudCosto.Value <= 0)
            {
                MessageBox.Show("Selecciona un equipo e indica cantidad y costo unitario mayores que cero.", "Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEquipo.Focus();
                return false;
            }
            decimal subtotal = nudCantidad.Value * nudCosto.Value;
            DetalleCompraEntidad detalle = new DetalleCompraEntidad(0, codigoEquipo, decimal.ToInt32(nudCantidad.Value), nudCosto.Value, subtotal, TextoONulo(txtObservacionesDetalle.Text), rdoDetalleActivo.Checked);
            if (editandoDetalle && lstDetalles.SelectedIndex >= 0)
            {
                detalle.CodigoDetalleCompra = detalles[lstDetalles.SelectedIndex].CodigoDetalleCompra;
                detalle.CodigoCompra = detalles[lstDetalles.SelectedIndex].CodigoCompra;
                detalles[lstDetalles.SelectedIndex] = detalle;
            }
            else
                detalles.Add(detalle);
            editandoDetalle = false;
            if (detalles.Count > 0)
                compraSinDetallesRegistrados = false;
            MostrarDetalles();
            LimpiarCamposDetalle();
            ActualizarTotales();
            return true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Enter) && modoEdicion)
            {
                AgregarDetalleTemporal();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void EditarDetalle_Click(object sender, EventArgs e)
        {
            if (!modoEdicion || lstDetalles.SelectedIndex < 0) return;
            CargarDetalleEnCampos(lstDetalles.SelectedIndex);
            editandoDetalle = true;
        }

        private void CargarDetalleEnCampos(int indice)
        {
            if (indice < 0 || indice >= detalles.Count) return;
            DetalleCompraEntidad detalle = detalles[indice];
            txtCodigoDetalle.Text = detalle.CodigoDetalleCompra > 0 ? detalle.CodigoDetalleCompra.ToString() : "Automático";
            EnlazarCompraRelacionada();
            if (detalle.CodigoCompra > 0) cmbCodigoCompraDetalle.SelectedValue = detalle.CodigoCompra;
            AsegurarEquipoDisponible(detalle.CodigoEquipo);
            cargandoEquipo = true;
            cmbEquipo.SelectedValue = detalle.CodigoEquipo;
            nudCantidad.Value = detalle.Cantidad;
            nudCosto.Value = detalle.CostoUnitario;
            cargandoEquipo = false;
            txtObservacionesDetalle.Text = detalle.Observaciones ?? string.Empty;
            rdoDetalleActivo.Checked = detalle.Estado;
            rdoDetalleInactivo.Checked = !detalle.Estado;
        }

        private void EliminarDetalleSeleccionado()
        {
            if (!modoEdicion || lstDetalles.SelectedIndex < 0) return;
            if (MessageBox.Show("¿Quitar el equipo seleccionado del detalle?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            detalles.RemoveAt(lstDetalles.SelectedIndex);
            MostrarDetalles();
            ActualizarTotales();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            // Guarda también la línea que siga capturada en el editor.
            if (editandoDetalle || HayLineaEnEdicion())
                if (!AgregarDetalleTemporal()) return;
            if (!Validar()) return;
            ActualizarTotales();
            bool conservarImportesExistentes = compraEnEdicion != null && compraSinDetallesRegistrados && detalles.Count == 0;
            decimal subtotal = conservarImportesExistentes ? nudSubtotalCompra.Value : detalles.Sum(d => d.Subtotal);
            decimal iva = conservarImportesExistentes ? nudIva.Value : ComprasNegocio.CalcularIva(subtotal);
            decimal total = conservarImportesExistentes ? nudTotalCompra.Value : subtotal + iva;
            ComprasEntidad compra = new ComprasEntidad(CodigoSeleccionado(cmbProveedor), txtDocumento.Text.Trim(), dtpFecha.Value, subtotal, iva, total, TextoONulo(txtObservacionesCompra.Text), rdoCompraActiva.Checked);
            try
            {
                if (compraEnEdicion == null)
                {
                    int codigo = negocio.AgregarConDetalles(compra, detalles);
                    MessageBox.Show("Compra y detalles guardados correctamente. Código: " + codigo, "Compra guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (conservarImportesExistentes)
                        negocio.Editar(compraEnEdicion.CodigoCompra, compra);
                    else
                        negocio.EditarConDetalles(compraEnEdicion.CodigoCompra, compra, detalles);
                    MessageBox.Show("Compra y detalles actualizados correctamente.", "Compra actualizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CerrarYRegresarAlDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al guardar la compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool HayLineaEnEdicion()
        {
            return CodigoSeleccionado(cmbEquipo) > 0 || nudCosto.Value > 0 || !string.IsNullOrWhiteSpace(txtObservacionesDetalle.Text);
        }

        private void CerrarYRegresarAlDashboard()
        {
            // El formulario de Compras refresca la grilla cuando recibe OK.
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool Validar()
        {
            if (CodigoSeleccionado(cmbProveedor) <= 0) { MessageBox.Show("Selecciona un proveedor."); cmbProveedor.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtDocumento.Text)) { MessageBox.Show("El número de documento es obligatorio."); txtDocumento.Focus(); return false; }
            if (detalles.Count == 0 && !(compraEnEdicion != null && compraSinDetallesRegistrados)) { MessageBox.Show("Agrega al menos un equipo al detalle de la compra."); cmbEquipo.Focus(); return false; }
            return true;
        }

        private void LimpiarFormulario(bool bloquear)
        {
            txtCodigoCompra.Text = "Automático";
            cmbProveedor.SelectedValue = 0;
            txtDocumento.Clear();
            dtpFecha.Value = DateTime.Now;
            nudIva.Value = 0;
            txtObservacionesCompra.Clear();
            rdoCompraActiva.Checked = true;
            rdoCompraInactiva.Checked = false;
            detalles.Clear();
            compraEnEdicion = null;
            compraSinDetallesRegistrados = false;
            modoEdicion = false;
            editandoDetalle = false;
            LimpiarCamposDetalle();
            MostrarDetalles();
            ActualizarTotales();
            if (bloquear) EstablecerEstadoInicial();
        }

        private void HabilitarCaptura(bool habilitar)
        {
            foreach (Control control in new Control[] { cmbProveedor, txtDocumento, dtpFecha, txtObservacionesCompra, rdoCompraActiva, rdoCompraInactiva,
                cmbEquipo, nudCantidad, nudCosto, txtObservacionesDetalle, rdoDetalleActivo, rdoDetalleInactivo })
                control.Enabled = habilitar;
            nudIva.ReadOnly = true;
            txtCodigoCompra.Enabled = txtCodigoDetalle.Enabled = true;
            cmbCodigoCompraDetalle.Enabled = false;
            nudSubtotalCompra.Enabled = nudTotalCompra.Enabled = nudSubtotalDetalle.Enabled = true;
        }

        private void LimpiarCamposDetalle()
        {
            txtCodigoDetalle.Text = "Automático";
            EnlazarCompraRelacionada();
            cargandoEquipo = true;
            cmbEquipo.SelectedValue = 0;
            cargandoEquipo = false;
            nudCantidad.Value = 1;
            nudCosto.Value = 0;
            nudSubtotalDetalle.Value = 0;
            txtObservacionesDetalle.Clear();
            rdoDetalleActivo.Checked = true;
            rdoDetalleInactivo.Checked = false;
        }

        private void MostrarDetalles()
        {
            lstDetalles.Items.Clear();
            foreach (DetalleCompraEntidad detalle in detalles)
            {
                EquiposEntidad equipo = equipos.FirstOrDefault(e => e.CodigoEquipo == detalle.CodigoEquipo);
                string nombreEquipo = equipo == null ? "Equipo " + detalle.CodigoEquipo : equipo.Modelo + " (" + detalle.CodigoEquipo + ")";
                lstDetalles.Items.Add(nombreEquipo + "  |  " + detalle.Cantidad + " × Q " + detalle.CostoUnitario.ToString("0.00") +
                    "  |  Subtotal Q " + detalle.Subtotal.ToString("0.00") + "  |  " + (detalle.Estado ? "Activo" : "Inactivo") +
                    (string.IsNullOrWhiteSpace(detalle.Observaciones) ? "" : "  |  " + detalle.Observaciones));
            }
        }

        private void ActualizarSubtotalDetalle()
        {
            decimal subtotal = nudCantidad.Value * nudCosto.Value;
            nudSubtotalDetalle.Value = Math.Min(nudSubtotalDetalle.Maximum, subtotal);
        }

        private void ActualizarTotales()
        {
            // Conserva importes de registros históricos sin filas en Tbl_DetalleCompra.
            if (compraEnEdicion != null && compraSinDetallesRegistrados && detalles.Count == 0)
                return;
            decimal subtotal = detalles.Sum(d => d.Subtotal);
            nudSubtotalCompra.Value = Math.Min(nudSubtotalCompra.Maximum, subtotal);
            decimal iva = ComprasNegocio.CalcularIva(subtotal);
            nudIva.Value = Math.Min(nudIva.Maximum, iva);
            nudTotalCompra.Value = Math.Min(nudTotalCompra.Maximum, subtotal + iva);
        }

        private static Panel Seccion(string titulo, int alto)
        {
            Panel panel = new Panel { BackColor = Color.FromArgb(7, 35, 66), Height = alto, Padding = new Padding(12), Margin = new Padding(0, 0, 0, 12) };
            panel.Controls.Add(new Label { Text = titulo, Dock = DockStyle.Top, Height = 30, ForeColor = Color.FromArgb(0, 190, 230), Font = new Font("Segoe UI", 11F, FontStyle.Bold) });
            return panel;
        }

        private static TableLayoutPanel CrearTabla(int filas, int columnas, int altoFila)
        {
            TableLayoutPanel tabla = new TableLayoutPanel { ColumnCount = columnas, RowCount = filas, Padding = new Padding(0), BackColor = Color.Transparent };
            // Deja ancho suficiente para las etiquetas completas, incluso con DPI alto.
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 225));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 225));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < filas; i++) tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, altoFila));
            return tabla;
        }

        private static void AgregarPar(TableLayoutPanel tabla, int fila, string etiquetaIzquierda, Control izquierda, string etiquetaDerecha, Control derecha)
        {
            tabla.Controls.Add(Etiqueta(etiquetaIzquierda), 0, fila);
            tabla.Controls.Add(izquierda, 1, fila);
            tabla.Controls.Add(Etiqueta(etiquetaDerecha), 2, fila);
            tabla.Controls.Add(derecha, 3, fila);
        }

        private static FlowLayoutPanel PanelRadios(RadioButton activo, RadioButton inactivo)
        {
            FlowLayoutPanel panel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, Margin = new Padding(3, 5, 8, 5), BackColor = ColorCampoEditable };
            panel.Controls.Add(activo);
            panel.Controls.Add(inactivo);
            return panel;
        }

        private static Label Etiqueta(string texto)
        {
            return new Label { Text = texto, ForeColor = Color.White, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 8, 8, 3) };
        }

        private static NumericUpDown Numero(decimal minimo, decimal maximo, decimal valor)
        {
            return new NumericUpDown { Minimum = minimo, Maximum = maximo, Value = valor, ThousandsSeparator = true, Width = 180 };
        }

        private static ComboBox CrearCombo()
        {
            return new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                IntegralHeight = false,
                MaxDropDownItems = 12
            };
        }

        private static TextBox CajaSoloLectura(string texto)
        {
            return new TextBox { Text = texto, ReadOnly = true, BackColor = Color.FromArgb(220, 230, 240) };
        }

        private static RadioButton Radio(string texto, bool seleccionado)
        {
            return new RadioButton { Text = texto, Checked = seleccionado, ForeColor = Color.FromArgb(25, 40, 35), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        }

        private static Button Boton(string texto, Color color)
        {
            return new Button { Text = texto, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), UseVisualStyleBackColor = false, Cursor = Cursors.Hand };
        }

        private static string TextoONulo(string texto)
        {
            string valor = texto == null ? string.Empty : texto.Trim();
            return valor.Length == 0 ? null : valor;
        }
    }
}
