using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmDetalleFactura : Form
    {
        // Lista temporal para probar el formulario. Reemplazar por la capa de Negocio
        // cuando se conecte a SQL Server.
        private readonly BindingList<DetalleFacturaEntidad> detalleFactura = new BindingList<DetalleFacturaEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly CrudRelacionalNegocio relacional = new CrudRelacionalNegocio();
        private bool cargando = false;
        private PanelRedondeado pnlDashboardDetalleFactura;
        private TableLayoutPanel layoutBloquesDetalleFactura;
        private Label[] valoresDashboard;
        private Label lblResumenFacturas;
        private Label lblDetallesRecientes;
        private Chart graficaDashboard;
        private Dictionary<int, FacturacionEntidad> facturas = new Dictionary<int, FacturacionEntidad>();

        public FrmDetalleFactura()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            CargarCatalogosFK();
            ConstruirNavegacionLateral();
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);

            origen.DataSource = detalleFactura;
            dgvDetalleFactura.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvDetalleFactura);

            ConectarEventos();
            CrearDashboardDetalleFactura();
            AplicarEsquinasRedondeadasRelacional();
            ActualizarColumnas();
            LimpiarFormulario();
            ConsultarRelacional();
        }

        private void ConstruirNavegacionLateral()
        {
            AjustesDisenoCrud.ConstruirBarraLateralModelo(this);
        }

        // Conserva la distribución lateral de Pagos y el scroll si no caben las pestañas.
        private void AjustarAlturaMenuLateral(List<Label> opciones)
        {
            if (opciones.Count == 0) return;

            int altoFila = Math.Max(34, pnlItemsMenu.ClientSize.Height / opciones.Count);
            int posicionY = 0;
            foreach (Label opcion in opciones)
            {
                opcion.SetBounds(0, posicionY, 160, altoFila);
                posicionY += altoFila;
            }

            pnlItemsMenu.AutoScrollMinSize = new Size(0, posicionY);
        }

        // Centra la fila de acciones y limita su ancho en ventanas grandes.
        private void CentrarBotonesAccion()
        {
            layoutBotones.Dock = DockStyle.None;
            layoutBotones.Anchor = AnchorStyles.Top;

            Action centrar = () =>
            {
                int anchoDisponible = Math.Max(0, pnlAcciones.ClientSize.Width);
                int anchoBotones = Math.Min(900, anchoDisponible);
                layoutBotones.SetBounds(
                    Math.Max(0, (anchoDisponible - anchoBotones) / 2),
                    Math.Max(0, (pnlAcciones.ClientSize.Height - layoutBotones.Height) / 2),
                    anchoBotones,
                    layoutBotones.Height);
            };

            pnlAcciones.SizeChanged += (s, e) => centrar();
            centrar();
        }
        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                MostrarDetalleFactura(detalleFactura.ToList());
                LimpiarFormulario();
                ActualizarContador();
            };

            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarDetalleFactura();

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarDetalleFactura();
                    e.SuppressKeyPress = true;
                }
            };

            dgvDetalleFactura.SelectionChanged += DgvDetalleFactura_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;

            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCodigoFactura.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDescripcion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCantidad.CheckedChanged += (s, e) => ActualizarColumnas();
            chkPrecioUnitario.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDescuento.CheckedChanged += (s, e) => ActualizarColumnas();
            chkIva.CheckedChanged += (s, e) => ActualizarColumnas();
            chkSubtotal.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Label opcion = sender as Label;
            if (opcion != null && opcion.Tag != null)
                Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoDetalleFactura", chkCodigo.Checked);
            CambiarVisibilidad("CodigoFactura", chkCodigoFactura.Checked);
            CambiarVisibilidad("Descripcion", chkDescripcion.Checked);
            CambiarVisibilidad("Cantidad", chkCantidad.Checked);
            CambiarVisibilidad("PrecioUnitario", chkPrecioUnitario.Checked);
            CambiarVisibilidad("Descuento", chkDescuento.Checked);
            CambiarVisibilidad("Iva", chkIva.Checked);
            CambiarVisibilidad("Subtotal", chkSubtotal.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void ConsultarRelacional()
        {
            try
            {
                List<DetalleFacturaEntidad> registros = relacional.Listar<DetalleFacturaEntidad>();
                detalleFactura.Clear();
                foreach (DetalleFacturaEntidad registro in registros) detalleFactura.Add(registro);
                MostrarDetalleFactura(detalleFactura.ToList());
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActualizarContador();
            }
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvDetalleFactura.Columns.Contains(columna))
                dgvDetalleFactura.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvDetalleFactura.Rows.Count > 0)
                {
                    int indice = dgvDetalleFactura.CurrentRow == null ? 0 : dgvDetalleFactura.CurrentRow.Index;
                    if (indice < 0 || indice >= dgvDetalleFactura.Rows.Count)
                        indice = 0;

                    SeleccionarFila(indice);
                }
            }
            else
            {
                dgvDetalleFactura.ClearSelection();
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvDetalleFactura.Rows.Count)
                return;

            dgvDetalleFactura.ClearSelection();

            DataGridViewCell celda = dgvDetalleFactura.Rows[indice].Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.Visible);

            if (celda != null)
                dgvDetalleFactura.CurrentCell = celda;

            dgvDetalleFactura.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            try
            {
                int codigo = relacional.Agregar(CrearEntidad());
                ConsultarRelacional();
                LimpiarFormulario();
                MessageBox.Show("Registro agregado correctamente. Código: " + codigo,
                    "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (!AjustesDisenoCrud.PermitirEdicion(this)) return;
            DetalleFacturaEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                relacional.Editar(seleccionado.CodigoDetalleFactura, CrearEntidad());
                ConsultarRelacional();
                LimpiarFormulario();
                MessageBox.Show("Registro actualizado correctamente.", "Editar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al editar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            DetalleFacturaEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Marca Seleccionar y elige una fila de la tabla.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el registro " + seleccionado.CodigoDetalleFactura + "?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;
            try
            {
                relacional.Eliminar<DetalleFacturaEntidad>(seleccionado.CodigoDetalleFactura);
                ConsultarRelacional();
                LimpiarFormulario();
                MessageBox.Show("Registro marcado como inactivo.", "Eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DetalleFacturaEntidad CrearEntidad()
        {
            return new DetalleFacturaEntidad(
                ComboFK.Obtener(cmbCodigoFactura).Value,
                txtDescripcion.Text.Trim(),
                decimal.ToInt32(nudCantidad.Value),
                nudPrecioUnitario.Value,
                nudDescuento.Value,
                nudIva.Value,
                nudSubtotal.Value,
                rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!ComboFK.Obtener(cmbCodigoFactura).HasValue)
            {
                MessageBox.Show("El campo Cód. factura debe ser mayor que cero.");
                cmbCodigoFactura.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("El campo Descripción es obligatorio.");
                txtDescripcion.Focus();
                return false;
            }

            if (txtDescripcion.Text.Trim().Length > 200)
            {
                MessageBox.Show("El campo Descripción no puede superar 200 caracteres.");
                txtDescripcion.Focus();
                return false;
            }

            if (nudCantidad.Value <= 0)
            {
                MessageBox.Show("El campo Cantidad debe ser mayor que cero.");
                nudCantidad.Focus();
                return false;
            }

            if (nudPrecioUnitario.Value <= 0)
            {
                MessageBox.Show("El campo Precio unitario debe ser mayor que cero.");
                nudPrecioUnitario.Focus();
                return false;
            }

            if (nudSubtotal.Value <= 0)
            {
                MessageBox.Show("El campo Subtotal debe ser mayor que cero.");
                nudSubtotal.Focus();
                return false;
            }

            return true;
        }

        private DetalleFacturaEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvDetalleFactura.CurrentRow == null)
                return null;

            return dgvDetalleFactura.CurrentRow.DataBoundItem as DetalleFacturaEntidad;
        }

        private void DgvDetalleFactura_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked)
                return;

            DetalleFacturaEntidad registro = ObtenerSeleccionado();
            if (registro == null)
                return;

            cargando = true;

            txtCodigo.Text = registro.CodigoDetalleFactura.ToString();
            ComboFK.Seleccionar(cmbCodigoFactura, registro.CodigoFactura);
            txtDescripcion.Text = registro.Descripcion;
            nudCantidad.Value = Limitar(nudCantidad, registro.Cantidad);
            nudPrecioUnitario.Value = Limitar(nudPrecioUnitario, registro.PrecioUnitario);
            nudDescuento.Value = Limitar(nudDescuento, registro.Descuento);
            nudIva.Value = Limitar(nudIva, registro.Iva);
            nudSubtotal.Value = Limitar(nudSubtotal, registro.Subtotal);
            rdoActivo.Checked = registro.Estado;
            rdoInactivo.Checked = !registro.Estado;

            cargando = false;
        }

        private decimal Limitar(NumericUpDown control, decimal valor)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, valor));
        }

        private static bool Contiene(string valor, string criterio)
        {
            return valor != null && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void BuscarDetalleFactura()
        {
            string criterio = txtBuscar.Text.Trim();
            if (criterio.Length == 0)
            {
                MostrarDetalleFactura(detalleFactura.ToList());
                ActualizarContador();
                return;
            }
            try
            {
                List<DetalleFacturaEntidad> resultado = relacional.Buscar<DetalleFacturaEntidad>(criterio);
                MostrarDetalleFactura(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + detalleFactura.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MensajesError.Resumir(ex), "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalleFactura(List<DetalleFacturaEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvDetalleFactura.DataSource = origen;
            ActualizarColumnas();
            dgvDetalleFactura.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;

            txtCodigo.Clear();
            ComboFK.Seleccionar(cmbCodigoFactura, null);
            txtDescripcion.Clear();
            nudCantidad.Value = 0;
            nudPrecioUnitario.Value = 0;
            nudDescuento.Value = 0;
            nudIva.Value = 0;
            nudSubtotal.Value = 0;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;

            dgvDetalleFactura.ClearSelection();
            chkSeleccionar.Checked = false;

            cargando = false;
            cmbCodigoFactura.Focus();
        }

        private void ActualizarContador()
        {
            lblRegistros.Text = "Total de detalles de factura registrados: " + detalleFactura.Count;
            ActualizarDatosDashboardDetalleFactura();
        }

        private void CrearDashboardDetalleFactura()
        {
            pnlContenido.AutoScroll = true;
            Color fondo = ColorTranslator.FromHtml("#08243A");
            Color superficie = ColorTranslator.FromHtml("#0B3558");
            pnlDashboardDetalleFactura = new PanelRedondeado
            {
                Name = "pnlDashboardDetalleFactura", Dock = DockStyle.Fill, Margin = Padding.Empty,
                Padding = new Padding(12), BackColor = fondo, RadioEsquinas = 12
            };
            TableLayoutPanel raiz = new TableLayoutPanel
            {
                Name = "layoutDashboardDetalleFactura", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            raiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            TableLayoutPanel encabezado = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
            encabezado.Controls.Add(new Label
            {
                Text = "Resumen de conceptos de factura", Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);
            Button actualizar = new Button
            {
                Name = "btnActualizarDashboardDetalleFactura", Text = "↻ Actualizar", Dock = DockStyle.Fill,
                Margin = new Padding(4, 1, 0, 1), FlatStyle = FlatStyle.Flat,
                BackColor = ColorTranslator.FromHtml("#0B6E91"), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            actualizar.FlatAppearance.BorderSize = 0;
            actualizar.Click += (s, e) => ConsultarRelacional();
            encabezado.Controls.Add(actualizar, 1, 0);
            raiz.Controls.Add(encabezado, 0, 0);

            TableLayoutPanel tarjetas = new TableLayoutPanel
            {
                Name = "layoutIndicadoresDashboardDetalleFactura", Dock = DockStyle.Fill,
                ColumnCount = 4, RowCount = 1, Margin = new Padding(0, 5, 0, 5),
                Padding = Padding.Empty, BackColor = fondo
            };
            for (int i = 0; i < 4; i++) tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            string[] nombres = { "CONCEPTOS REGISTRADOS", "CONCEPTOS ACTIVOS", "CONCEPTOS INACTIVOS", "MONTO DE CONCEPTOS" };
            Color[] acentos = { ColorTranslator.FromHtml("#3B9EFF"), ColorTranslator.FromHtml("#35C98B"),
                ColorTranslator.FromHtml("#E7B84B"), ColorTranslator.FromHtml("#8B72E8") };
            valoresDashboard = new Label[4];
            for (int i = 0; i < nombres.Length; i++)
            {
                PanelRedondeado tarjeta = new PanelRedondeado
                {
                    Name = "pnlIndicadorDashboardDetalleFactura" + i, Dock = DockStyle.Fill,
                    Margin = new Padding(0, 0, i == nombres.Length - 1 ? 0 : 8, 0),
                    BackColor = superficie, RadioEsquinas = 10
                };
                TableLayoutPanel contenido = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
                    Margin = Padding.Empty, Padding = Padding.Empty, BackColor = superficie
                };
                contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 3));
                contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                contenido.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = acentos[i], Margin = Padding.Empty }, 0, 0);
                contenido.Controls.Add(new Label
                {
                    Text = nombres[i], Dock = DockStyle.Fill, Margin = new Padding(10, 0, 8, 0),
                    ForeColor = ColorTranslator.FromHtml("#A9BAC8"), Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    AutoEllipsis = true, TextAlign = ContentAlignment.BottomLeft
                }, 0, 1);
                valoresDashboard[i] = new Label
                {
                    Name = "lblValorIndicadorDashboardDetalleFactura" + i, Text = "—", Dock = DockStyle.Fill,
                    Margin = new Padding(10, 0, 8, 4), ForeColor = Color.White,
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft,
                    AutoEllipsis = true
                };
                contenido.Controls.Add(valoresDashboard[i], 0, 2);
                tarjeta.Controls.Add(contenido);
                tarjetas.Controls.Add(tarjeta, i, 0);
            }
            raiz.Controls.Add(tarjetas, 0, 1);

            TableLayoutPanel inferior = new TableLayoutPanel
            {
                Name = "layoutDetalleDashboardDetalleFactura", Dock = DockStyle.Fill,
                ColumnCount = 2, RowCount = 1, Margin = new Padding(0, 4, 0, 0),
                Padding = Padding.Empty, BackColor = fondo
            };
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
            Panel panelGrafica = CrearPanelGraficaDetalleFactura(out graficaDashboard);
            inferior.Controls.Add(panelGrafica, 0, 0);
            TableLayoutPanel derecha = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = new Padding(8, 0, 0, 0),
                Padding = Padding.Empty, BackColor = fondo
            };
            derecha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            derecha.RowStyles.Add(new RowStyle(SizeType.Percent, 42));
            derecha.RowStyles.Add(new RowStyle(SizeType.Percent, 58));
            Panel resumen = CrearPanelDashboardDetalleFactura("Conceptos por factura", out lblResumenFacturas);
            Panel recientes = CrearPanelDashboardDetalleFactura("Detalles recientes", out lblDetallesRecientes);
            resumen.Margin = new Padding(0, 0, 0, 5);
            recientes.Margin = new Padding(0, 5, 0, 0);
            derecha.Controls.Add(resumen, 0, 0);
            derecha.Controls.Add(recientes, 0, 1);
            inferior.Controls.Add(derecha, 1, 0);
            raiz.Controls.Add(inferior, 0, 2);
            pnlDashboardDetalleFactura.Controls.Add(raiz);

            layoutBloquesDetalleFactura = new TableLayoutPanel
            {
                Name = "layoutBloquesDetalleFactura", Dock = DockStyle.Top, ColumnCount = 1, RowCount = 5,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = pnlContenido.BackColor,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesDetalleFactura.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layoutBloquesDetalleFactura.RowStyles.Add(new RowStyle(SizeType.Absolute, 380));
            layoutBloquesDetalleFactura.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            layoutBloquesDetalleFactura.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlFormulario.Height));
            layoutBloquesDetalleFactura.RowStyles.Add(new RowStyle(SizeType.Absolute, pnlAcciones.Height));
            layoutBloquesDetalleFactura.RowStyles.Add(new RowStyle(SizeType.Absolute, 320));
            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesDetalleFactura);
            layoutBloquesDetalleFactura.Controls.Add(pnlDashboardDetalleFactura, 0, 0);
            layoutBloquesDetalleFactura.Controls.Add(new Panel
            {
                Name = "pnlSeparadorDashboardDetalleFactura", Dock = DockStyle.Fill,
                Margin = Padding.Empty, BackColor = pnlContenido.BackColor
            }, 0, 1);
            pnlFormulario.Margin = pnlAcciones.Margin = pnlCatalogo.Margin = Padding.Empty;
            layoutBloquesDetalleFactura.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesDetalleFactura.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesDetalleFactura.Controls.Add(pnlCatalogo, 0, 4);
            pnlContenido.SizeChanged += (s, e) => AjustarInterfazRelacional();
            Shown += (s, e) => AplicarEsquinasRedondeadasRelacional();
            AjustarInterfazRelacional();
        }

        private static Panel CrearPanelDashboardDetalleFactura(string titulo, out Label contenido)
        {
            PanelRedondeado panel = new PanelRedondeado
            {
                Dock = DockStyle.Fill, Padding = new Padding(9, 6, 9, 6),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty, RadioEsquinas = 10
            };
            panel.Controls.Add(new Label
            {
                Text = titulo, Dock = DockStyle.Top, Height = 21, Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true
            });
            contenido = new Label
            {
                Dock = DockStyle.Fill, ForeColor = Color.White, Font = new Font("Segoe UI", 9F),
                AutoEllipsis = true, TextAlign = ContentAlignment.TopLeft
            };
            panel.Controls.Add(contenido);
            return panel;
        }

        private static Panel CrearPanelGraficaDetalleFactura(out Chart grafica)
        {
            PanelRedondeado panel = new PanelRedondeado
            {
                Dock = DockStyle.Fill, Padding = new Padding(7, 5, 7, 5),
                BackColor = ColorTranslator.FromHtml("#0B3558"), Margin = Padding.Empty, RadioEsquinas = 10
            };
            panel.Controls.Add(new Label
            {
                Text = "Conceptos por mes de factura", Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#00D9FF"),
                AutoEllipsis = true
            });
            grafica = new Chart { Dock = DockStyle.Fill, BackColor = ColorTranslator.FromHtml("#0B3558") };
            ChartArea area = new ChartArea("ConceptosMensuales") { BackColor = ColorTranslator.FromHtml("#0B3558") };
            area.AxisX.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisX.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.LabelStyle.ForeColor = ColorTranslator.FromHtml("#A9BAC8");
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisY.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#34495E");
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            grafica.ChartAreas.Add(area);
            grafica.Series.Add(new Series("Conceptos")
            {
                ChartArea = "ConceptosMensuales", ChartType = SeriesChartType.Column,
                BorderWidth = 0, Color = ColorTranslator.FromHtml("#00D9FF"), IsXValueIndexed = true
            });
            panel.Controls.Add(grafica);
            return panel;
        }

        private void AjustarInterfazRelacional()
        {
            if (layoutBloquesDetalleFactura == null) return;
            int disponible = Math.Max(0, pnlContenido.ClientSize.Height - pnlContenido.Padding.Vertical);
            int altoDashboard = Math.Max(380, disponible - pnlFormulario.Height - pnlAcciones.Height - 330 - 10);
            int altoCatalogo = Math.Max(320, disponible - altoDashboard - pnlFormulario.Height - pnlAcciones.Height - 10);
            layoutBloquesDetalleFactura.RowStyles[0].Height = altoDashboard;
            layoutBloquesDetalleFactura.RowStyles[2].Height = pnlFormulario.Height;
            layoutBloquesDetalleFactura.RowStyles[3].Height = pnlAcciones.Height;
            layoutBloquesDetalleFactura.RowStyles[4].Height = altoCatalogo;
            layoutBloquesDetalleFactura.Height = altoDashboard + 10 + pnlFormulario.Height + pnlAcciones.Height + altoCatalogo;
        }

        private void AplicarEsquinasRedondeadasRelacional()
        {
            foreach (Panel panel in ControlesDescendientes<Panel>(this))
            {
                if (panel.Name == "pnlMenu" || panel.Name == "pnlItemsMenu" || panel.Name == "pnlContenido" ||
                    panel.Name == "pnlAcciones" || panel.Name.IndexOf("Separador", StringComparison.OrdinalIgnoreCase) >= 0) continue;
                CampoRedondeado.Aplicar(panel, panel == pnlDashboardDetalleFactura ? 12 : 10);
            }
            foreach (Control control in ControlesDescendientes<Control>(this))
            {
                if (control is TextBoxBase || control is ComboBox || control is NumericUpDown || control is DateTimePicker)
                    CampoRedondeado.Aplicar(control, 7);
                else if (control is DataGridView)
                    CampoRedondeado.Aplicar(control, 10);
                else if (control is Button)
                    CampoRedondeado.Aplicar(control, 8);
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

        private void ActualizarDatosDashboardDetalleFactura()
        {
            if (valoresDashboard == null) return;
            valoresDashboard[0].Text = detalleFactura.Count.ToString("N0");
            valoresDashboard[1].Text = detalleFactura.Count(x => x.Estado).ToString("N0");
            valoresDashboard[2].Text = detalleFactura.Count(x => !x.Estado).ToString("N0");
            valoresDashboard[3].Text = "Q " + detalleFactura.Where(x => x.Estado).Sum(x => x.Subtotal).ToString("N2");
            var grupos = detalleFactura.GroupBy(x => x.CodigoFactura)
                .Select(g => new { Codigo = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad).ThenBy(x => x.Codigo).Take(4).ToArray();
            lblResumenFacturas.Text = grupos.Length == 0 ? "No hay conceptos registrados." : string.Join(Environment.NewLine, grupos.Select(x =>
            {
                FacturacionEntidad factura;
                string numero = facturas.TryGetValue(x.Codigo, out factura) ? factura.NumeroFactura : "Factura " + x.Codigo;
                return numero + ": " + x.Cantidad + " conceptos";
            }));

            Series serie = graficaDashboard.Series["Conceptos"];
            serie.Points.Clear();
            DateTime mesActual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            for (int i = 5; i >= 0; i--)
            {
                DateTime mes = mesActual.AddMonths(-i);
                HashSet<int> codigosFacturas = new HashSet<int>(facturas.Values
                    .Where(x => x.FechaEmision.Year == mes.Year && x.FechaEmision.Month == mes.Month)
                    .Select(x => x.CodigoFactura));
                serie.Points.AddXY(mes.ToString("MMM"), detalleFactura.Count(x => codigosFacturas.Contains(x.CodigoFactura)));
            }
            DetalleFacturaEntidad[] recientes = detalleFactura.OrderByDescending(x =>
            {
                FacturacionEntidad factura;
                return facturas.TryGetValue(x.CodigoFactura, out factura) ? factura.FechaEmision : DateTime.MinValue;
            }).Take(4).ToArray();
            lblDetallesRecientes.Text = recientes.Length == 0 ? "No hay conceptos registrados." : string.Join(Environment.NewLine,
                recientes.Select(x => (facturas.ContainsKey(x.CodigoFactura) ? facturas[x.CodigoFactura].NumeroFactura : "Factura " + x.CodigoFactura) +
                    " | " + x.Descripcion + " | Q " + x.Subtotal.ToString("N2")));
        }

        private void CargarCatalogosFK()
        {
            try
            {
                List<FacturacionEntidad> registros = relacional.ListarCatalogo<FacturacionEntidad>();
                facturas = registros.GroupBy(x => x.CodigoFactura).ToDictionary(x => x.Key, x => x.First());
                ComboFK.Cargar(cmbCodigoFactura, registros, x => x.CodigoFactura, x => "Factura | " + x.NumeroFactura + " | " + x.NombreCliente, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar los registros relacionados: " + MensajesError.Resumir(ex),
                    "Catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
