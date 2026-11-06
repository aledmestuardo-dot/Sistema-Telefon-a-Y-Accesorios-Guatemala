using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    public partial class FrmProveedores : Form
    {
        private readonly BindingList<ProveedoresEntidad> proveedores = new BindingList<ProveedoresEntidad>();
        private readonly BindingSource origen = new BindingSource();
        private readonly ProveedoresNegocio negocio = new ProveedoresNegocio();
        private bool cargando;
        private bool modoNuevo;
        private bool modoEdicion;
        private readonly Label[] valoresResumen = new Label[4];
        private readonly Panel[] barrasResumen = new Panel[4];
        private PanelRedondeado panelResumen;
        private TableLayoutPanel layoutBloquesProveedores;

        public FrmProveedores()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AjustesDisenoCrud.CentrarAcciones(pnlAcciones, layoutBotones);
            ConfigurarDisenoAdaptable();
            ConstruirResumenProveedores();
            ConstruirNavegacionLateral();
            origen.DataSource = proveedores;
            dgvProveedores.DataSource = origen;
            AjustesDisenoCrud.ConfigurarTabla(dgvProveedores);
            dgvProveedores.ScrollBars = ScrollBars.Both;
            AjustesDisenoCrud.AplicarCatalogoModeloPagos(this, pnlHeader, lblTitulo, lblSubtitulo,
                pnlBusqueda, txtBuscar, btnBuscar, pnlColumnas, lblColumnas, selector, chkSeleccionar);
            ConstruirOrdenBloques();
            dgvProveedores.DataBindingComplete += (s, e) => AjustarInterfazProveedores();
            Shown += (s, e) => AjustarInterfazProveedores();
            SizeChanged += (s, e) => AjustarInterfazProveedores();
            AplicarEstiloResumenProveedores();
            ConectarEventos();
            ActualizarColumnas();
            LimpiarFormulario();
            BuscarProveedores();
            AjustarInterfazProveedores();
        }

        private void AjustarInterfazProveedores()
        {
            if (IsDisposed || layoutBloquesProveedores == null || pnlCatalogo.Width <= 0) return;

            dgvProveedores.Dock = DockStyle.Top;
            dgvProveedores.ScrollBars = ScrollBars.Both;
            int altoGrilla = dgvProveedores.ColumnHeadersHeight + 10 * dgvProveedores.RowTemplate.Height + 2;
            if (dgvProveedores.Height != altoGrilla) dgvProveedores.Height = altoGrilla;

            int altoCatalogo = pnlCatalogo.Padding.Vertical + lblCatalogoTitulo.Height + pnlBusqueda.Height +
                pnlColumnas.Height + altoGrilla +
                (pnlCatalogo.Controls.Find("pnlPaginacion", false).Length > 0 ? 48 : lblRegistros.Height);
            if (pnlCatalogo.Height != altoCatalogo) pnlCatalogo.Height = altoCatalogo;
            if (layoutBloquesProveedores.RowStyles[4].Height != altoCatalogo)
                layoutBloquesProveedores.RowStyles[4].Height = altoCatalogo;

            int altoTotal = 108 + 10 + pnlFormulario.Height + pnlAcciones.Height + altoCatalogo;
            if (layoutBloquesProveedores.Height != altoTotal) layoutBloquesProveedores.Height = altoTotal;
        }

        private void ConfigurarDisenoAdaptable()
        {
            pnlHeader.Height = 108;
            pnlContenido.Padding = new Padding(14);
            pnlContenido.AutoScroll = true;
            pnlFormulario.Dock = DockStyle.Top;
            pnlAcciones.Dock = DockStyle.Top;
            pnlCatalogo.Dock = DockStyle.Top;
            pnlCatalogo.Height = 380;
            lblFormularioTitulo.Height = 40;
            lblCatalogoTitulo.Height = 40;
            lblColumnas.Height = 24;
            pnlBusqueda.Height = 48;
            pnlColumnas.Height = 76;
            selector.AutoScroll = false;
            dgvProveedores.RowTemplate.Height = 32;
            dgvProveedores.ColumnHeadersHeight = 36;
        }

        private void ConstruirResumenProveedores()
        {
            panelResumen = new PanelRedondeado
            {
                Name = "pnlResumenProveedores",
                Dock = DockStyle.Fill,
                BackColor = pnlContenido.BackColor,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                RadioEsquinas = 12
            };

            TableLayoutPanel tarjetas = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = panelResumen.BackColor,
                Padding = Padding.Empty,
                Margin = Padding.Empty
            };
            for (int i = 0; i < 4; i++)
            {
                tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                PanelRedondeado tarjeta = new PanelRedondeado
                {
                    Dock = DockStyle.Fill,
                    BackColor = ColorTranslator.FromHtml("#151F33"),
                    Margin = new Padding(i == 0 ? 0 : 7, 0, i == 3 ? 0 : 7, 0),
                    Padding = new Padding(14, 9, 14, 9),
                    RadioEsquinas = 12
                };
                Label titulo = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 22,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    ForeColor = ColorTranslator.FromHtml("#A5B3C8"),
                    AutoEllipsis = true
                };
                valoresResumen[i] = new Label
                {
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleLeft
                };
                Panel pista = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 6,
                    BackColor = ColorTranslator.FromHtml("#24334B"),
                    Margin = Padding.Empty
                };
                barrasResumen[i] = new Panel { Dock = DockStyle.Left, Width = 0, BackColor = ColorTranslator.FromHtml("#16B8A6") };
                pista.Controls.Add(barrasResumen[i]);
                tarjeta.Controls.Add(valoresResumen[i]);
                tarjeta.Controls.Add(pista);
                tarjeta.Controls.Add(titulo);
                tarjeta.Tag = titulo;
                tarjetas.Controls.Add(tarjeta, i, 0);
            }
            ((Label)((Control)tarjetas.GetControlFromPosition(0, 0)).Tag).Text = "PROVEEDORES REGISTRADOS";
            ((Label)((Control)tarjetas.GetControlFromPosition(1, 0)).Tag).Text = "ACTIVOS";
            ((Label)((Control)tarjetas.GetControlFromPosition(2, 0)).Tag).Text = "INACTIVOS";
            ((Label)((Control)tarjetas.GetControlFromPosition(3, 0)).Tag).Text = "CON CORREO ELECTRÓNICO";
            tarjetas.SizeChanged += (s, e) => ActualizarResumenProveedores();
            panelResumen.Controls.Add(tarjetas);
        }

        private void ConstruirOrdenBloques()
        {
            int altoFormulario = pnlFormulario.Height;
            int altoAcciones = pnlAcciones.Height;
            int altoCatalogo = pnlCatalogo.Height;
            layoutBloquesProveedores = new TableLayoutPanel
            {
                Name = "layoutBloquesProveedores",
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 5,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                BackColor = pnlContenido.BackColor,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };
            layoutBloquesProveedores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBloquesProveedores.RowStyles.Add(new RowStyle(SizeType.Absolute, 108F));
            layoutBloquesProveedores.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutBloquesProveedores.RowStyles.Add(new RowStyle(SizeType.Absolute, altoFormulario));
            layoutBloquesProveedores.RowStyles.Add(new RowStyle(SizeType.Absolute, altoAcciones));
            layoutBloquesProveedores.RowStyles.Add(new RowStyle(SizeType.Absolute, altoCatalogo));

            pnlContenido.Controls.Remove(pnlCatalogo);
            pnlContenido.Controls.Remove(pnlAcciones);
            pnlContenido.Controls.Remove(pnlFormulario);
            pnlContenido.Controls.Add(layoutBloquesProveedores);
            panelResumen.Dock = DockStyle.Fill;
            pnlFormulario.Dock = DockStyle.Fill;
            pnlAcciones.Dock = DockStyle.Fill;
            pnlCatalogo.Dock = DockStyle.Fill;
            layoutBloquesProveedores.Controls.Add(panelResumen, 0, 0);
            layoutBloquesProveedores.Controls.Add(pnlFormulario, 0, 2);
            layoutBloquesProveedores.Controls.Add(pnlAcciones, 0, 3);
            layoutBloquesProveedores.Controls.Add(pnlCatalogo, 0, 4);
            layoutBloquesProveedores.Height = 108 + 10 + altoFormulario + altoAcciones + altoCatalogo;
        }

        private void AplicarEstiloResumenProveedores()
        {
            Color[] colores =
            {
                ColorTranslator.FromHtml("#12B8A6"),
                ColorTranslator.FromHtml("#34D399"),
                ColorTranslator.FromHtml("#F87171"),
                ColorTranslator.FromHtml("#818CF8")
            };
            for (int i = 0; i < valoresResumen.Length; i++)
            {
                valoresResumen[i].ForeColor = colores[i];
                barrasResumen[i].BackColor = colores[i];
            }
            AplicarEstiloBoton(btnNuevo, ColorTranslator.FromHtml("#198754"));
            AplicarEstiloBoton(btnGuardar, ColorTranslator.FromHtml("#4F46E5"));
            AplicarEstiloBoton(btnEditar, ColorTranslator.FromHtml("#087EA4"));
            AplicarEstiloBoton(btnEliminar, ColorTranslator.FromHtml("#B91C1C"));
            AplicarEstiloBoton(btnLimpiar, ColorTranslator.FromHtml("#334155"));
            AplicarEstiloBoton(btnBuscar, ColorTranslator.FromHtml("#117A91"));
            AplicarEstiloBoton(layoutBotones.Controls.Find("btnImportarExcel", false).OfType<Button>().FirstOrDefault(), ColorTranslator.FromHtml("#7E22CE"));
            AplicarEstiloBoton(layoutBotones.Controls.Find("btnExportarExcel", false).OfType<Button>().FirstOrDefault(), ColorTranslator.FromHtml("#0F766E"));
            AplicarEstiloBoton(layoutBotones.Controls.Find("btnImprimir", false).OfType<Button>().FirstOrDefault(), ColorTranslator.FromHtml("#1D4ED8"));
            dgvProveedores.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#117A91");
        }

        private static void AplicarEstiloBoton(Button boton, Color color)
        {
            if (boton == null) return;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = color;
            boton.ForeColor = Color.White;
            boton.Cursor = Cursors.Hand;
        }

        private void ActualizarResumenProveedores()
        {
            int total = proveedores.Count;
            int activos = proveedores.Count(p => p.Estado);
            int inactivos = total - activos;
            int conCorreo = proveedores.Count(p => !string.IsNullOrWhiteSpace(p.CorreoElectronico));
            int[] cantidades = { total, activos, inactivos, conCorreo };
            for (int i = 0; i < cantidades.Length; i++)
            {
                valoresResumen[i].Text = cantidades[i].ToString();
                int anchoPista = barrasResumen[i].Parent == null ? 0 : barrasResumen[i].Parent.ClientSize.Width;
                barrasResumen[i].Width = total == 0 ? 0 : (int)Math.Round(anchoPista * (double)cantidades[i] / total);
            }
        }

        private void ConstruirNavegacionLateral()
        {
            AjustesDisenoCrud.ConstruirBarraLateralModelo(this);
        }

        private void ConectarEventos()
        {
            btnNuevo.Click += (s, e) => IniciarNuevo();
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                LimpiarFormulario();
                BuscarProveedores();
            };
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += (s, e) => BuscarProveedores();
            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { BuscarProveedores(); e.SuppressKeyPress = true; }
            };
            dgvProveedores.SelectionChanged += DgvProveedores_SelectionChanged;
            chkSeleccionar.CheckedChanged += ChkSeleccionar_CheckedChanged;
            chkCodigo.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNombre.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNit.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDireccion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkTelefono.CheckedChanged += (s, e) => ActualizarColumnas();
            chkCorreoElectronico.CheckedChanged += (s, e) => ActualizarColumnas();
            chkNombreContacto.CheckedChanged += (s, e) => ActualizarColumnas();
            chkDescripcion.CheckedChanged += (s, e) => ActualizarColumnas();
            chkEstado.CheckedChanged += (s, e) => ActualizarColumnas();
        }

        private void MenuOpcion_Click(object sender, EventArgs e)
        {
            Label opcion = sender as Label;
            if (opcion != null && opcion.Tag != null) Navegacion.Abrir(this, opcion.Tag.ToString());
        }

        private void ActualizarColumnas()
        {
            CambiarVisibilidad("CodigoProveedor", chkCodigo.Checked);
            CambiarVisibilidad("Nombre", chkNombre.Checked);
            CambiarVisibilidad("Nit", chkNit.Checked);
            CambiarVisibilidad("Direccion", chkDireccion.Checked);
            CambiarVisibilidad("Telefono", chkTelefono.Checked);
            CambiarVisibilidad("CorreoElectronico", chkCorreoElectronico.Checked);
            CambiarVisibilidad("NombreContacto", chkNombreContacto.Checked);
            CambiarVisibilidad("Descripcion", chkDescripcion.Checked);
            CambiarVisibilidad("Estado", chkEstado.Checked);
        }

        private void CambiarVisibilidad(string columna, bool visible)
        {
            if (dgvProveedores.Columns.Contains(columna)) dgvProveedores.Columns[columna].Visible = visible;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                if (dgvProveedores.Rows.Count > 0) SeleccionarFila(dgvProveedores.CurrentRow == null ? 0 : dgvProveedores.CurrentRow.Index);
            }
            else
            {
                dgvProveedores.ClearSelection();
                modoEdicion = false;
                EstablecerCamposHabilitados(false);
                ActualizarEstadoBotones(false);
            }
        }

        private void SeleccionarFila(int indice)
        {
            if (indice < 0 || indice >= dgvProveedores.Rows.Count) return;
            dgvProveedores.ClearSelection();
            DataGridViewCell celda = dgvProveedores.Rows[indice].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
            if (celda != null) dgvProveedores.CurrentCell = celda;
            dgvProveedores.Rows[indice].Selected = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!modoNuevo || !ValidarFormulario()) return;
            try
            {
                int codigo = negocio.Agregar(CrearEntidad());
                LimpiarFormulario();
                BuscarProveedores();
                MessageBox.Show("Proveedor agregado correctamente. Código: " + codigo, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al agregar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            ProveedoresEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null) { MessageBox.Show("Marca Seleccionar y elige una fila de la tabla."); return; }
            if (!modoEdicion)
            {
                modoNuevo = false;
                modoEdicion = true;
                EstablecerCamposHabilitados(true);
                ActualizarEstadoBotones(true);
                MessageBox.Show("Modifica los campos y pulsa Editar nuevamente para aplicar los cambios.");
                return;
            }
            if (!ValidarFormulario()) return;
            try
            {
                ProveedoresEntidad cambios = CrearEntidad();
                cambios.CodigoProveedor = seleccionado.CodigoProveedor;
                negocio.Editar(cambios);
                LimpiarFormulario();
                BuscarProveedores();
                MessageBox.Show("Proveedor actualizado correctamente.");
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al editar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            ProveedoresEntidad seleccionado = ObtenerSeleccionado();
            if (seleccionado == null) { MessageBox.Show("Marca Seleccionar y elige una fila de la tabla."); return; }
            if (MessageBox.Show("¿Deseas marcar como inactivo al proveedor " + seleccionado.CodigoProveedor + "?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                negocio.Eliminar(seleccionado.CodigoProveedor);
                LimpiarFormulario();
                BuscarProveedores();
                MessageBox.Show("Proveedor marcado como inactivo.", "Eliminar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al eliminar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private ProveedoresEntidad CrearEntidad()
        {
            return new ProveedoresEntidad(txtNombre.Text.Trim(), ValorONulo(txtNit.Text), txtDireccion.Text.Trim(),
                txtTelefono.Text.Trim(), ValorONulo(txtCorreoElectronico.Text), ValorONulo(txtNombreContacto.Text),
                ValorONulo(txtDescripcion.Text), rdoActivo.Checked);
        }

        private bool ValidarFormulario()
        {
            if (!Requerido(txtNombre, 100, "Nombre")) return false;
            if (!Longitud(txtNit, 45, "NIT")) return false;
            if (!Requerido(txtDireccion, 200, "Dirección")) return false;
            if (!Requerido(txtTelefono, 20, "Teléfono")) return false;
            if (!Longitud(txtCorreoElectronico, 100, "Correo electrónico")) return false;
            if (!Longitud(txtNombreContacto, 100, "Nombre de contacto")) return false;
            return Longitud(txtDescripcion, 200, "Descripción");
        }

        private bool Requerido(TextBox campo, int maximo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(campo.Text)) { MessageBox.Show("El campo " + nombre + " es obligatorio."); campo.Focus(); return false; }
            return Longitud(campo, maximo, nombre);
        }

        private bool Longitud(TextBox campo, int maximo, string nombre)
        {
            if (campo.Text.Trim().Length <= maximo) return true;
            MessageBox.Show("El campo " + nombre + " no puede superar " + maximo + " caracteres.");
            campo.Focus();
            return false;
        }

        private ProveedoresEntidad ObtenerSeleccionado()
        {
            if (!chkSeleccionar.Checked || dgvProveedores.CurrentRow == null) return null;
            return dgvProveedores.CurrentRow.DataBoundItem as ProveedoresEntidad;
        }

        private void DgvProveedores_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || !chkSeleccionar.Checked) return;
            ProveedoresEntidad p = ObtenerSeleccionado();
            if (p == null) return;
            cargando = true;
            txtCodigo.Text = p.CodigoProveedor.ToString();
            txtNombre.Text = p.Nombre;
            txtNit.Text = p.Nit ?? string.Empty;
            txtDireccion.Text = p.Direccion;
            txtTelefono.Text = p.Telefono;
            txtCorreoElectronico.Text = p.CorreoElectronico ?? string.Empty;
            txtNombreContacto.Text = p.NombreContacto ?? string.Empty;
            txtDescripcion.Text = p.Descripcion ?? string.Empty;
            rdoActivo.Checked = p.Estado;
            rdoInactivo.Checked = !p.Estado;
            cargando = false;
            modoEdicion = false;
            EstablecerCamposHabilitados(false);
            ActualizarEstadoBotones(true);
        }

        private static string ValorONulo(string valor) { return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim(); }

        private void BuscarProveedores()
        {
            string criterio = txtBuscar.Text.Trim();
            try
            {
                List<ProveedoresEntidad> todos = negocio.Listar();
                proveedores.Clear();
                foreach (ProveedoresEntidad proveedor in todos) proveedores.Add(proveedor);
                ActualizarResumenProveedores();
                List<ProveedoresEntidad> resultado = criterio.Length == 0 ? todos : negocio.Buscar(criterio);
                MostrarProveedores(resultado);
                lblRegistros.Text = "Resultados: " + resultado.Count + " | Total: " + proveedores.Count;
            }
            catch (Exception ex) { MessageBox.Show(MensajesError.Resumir(ex), "Error al consultar proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void MostrarProveedores(List<ProveedoresEntidad> lista)
        {
            origen.DataSource = null;
            origen.DataSource = lista;
            dgvProveedores.DataSource = origen;
            ActualizarColumnas();
            dgvProveedores.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cargando = true;
            modoNuevo = false;
            modoEdicion = false;
            txtCodigo.Clear(); txtNombre.Clear(); txtNit.Clear(); txtDireccion.Clear(); txtTelefono.Clear();
            txtCorreoElectronico.Clear(); txtNombreContacto.Clear(); txtDescripcion.Clear();
            rdoActivo.Checked = true; rdoInactivo.Checked = false;
            dgvProveedores.ClearSelection(); chkSeleccionar.Checked = false;
            cargando = false;
            EstablecerCamposHabilitados(false);
            ActualizarEstadoBotones(false);
        }

        private void IniciarNuevo()
        {
            LimpiarFormulario();
            modoNuevo = true;
            EstablecerCamposHabilitados(true);
            ActualizarEstadoBotones(false);
            txtNombre.Focus();
        }

        private void EstablecerCamposHabilitados(bool habilitados)
        {
            txtNombre.Enabled = habilitados; txtNit.Enabled = habilitados; txtDireccion.Enabled = habilitados;
            txtTelefono.Enabled = habilitados; txtCorreoElectronico.Enabled = habilitados;
            txtNombreContacto.Enabled = habilitados; txtDescripcion.Enabled = habilitados;
            rdoActivo.Enabled = habilitados; rdoInactivo.Enabled = habilitados;
        }

        private void ActualizarEstadoBotones(bool haySeleccion)
        {
            btnGuardar.Enabled = modoNuevo;
            btnEditar.Enabled = !modoNuevo && (modoEdicion || haySeleccion);
            btnEliminar.Enabled = !modoNuevo && !modoEdicion && haySeleccion;
        }
    }
}
