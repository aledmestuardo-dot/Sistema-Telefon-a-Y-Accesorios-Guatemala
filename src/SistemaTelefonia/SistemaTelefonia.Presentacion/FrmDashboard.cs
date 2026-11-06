using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SistemaTelefonia.Entidad;
using SistemaTelefonia.Negocio;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Panel general de ventas, compras e inventario.</summary>
    public class FrmDashboard : Form
    {
        private static readonly Color Fondo = Color.FromArgb(11, 19, 36);
        private static readonly Color Superficie = Color.FromArgb(17, 27, 48);
        private static readonly Color Borde = Color.FromArgb(38, 57, 84);
        private static readonly Color Azul = Color.FromArgb(0, 119, 204);
        private static readonly Color TextoSuave = Color.FromArgb(166, 185, 211);
        private readonly DashboardNegocio negocio = new DashboardNegocio();
        private readonly DateTimePicker desde = new DateTimePicker();
        private readonly DateTimePicker hasta = new DateTimePicker();
        private readonly ComboBox agrupacion = new ComboBox();
        private readonly ComboBox tipoGrafica = new ComboBox();
        private readonly ComboBox paleta = new ComboBox();
        private readonly ComboBox paletaTarjetas = new ComboBox();
        private readonly Label[] valores = { new Label(), new Label(), new Label(), new Label(), new Label() };
        private readonly Label[] variaciones = { new Label(), new Label() };
        private readonly Chart grafica = new Chart();
        private readonly DataGridView tablaProductos = new DataGridView();
        private readonly DataGridView tablaMasVendidos = new DataGridView();
        private List<DashboardEntidad> serieGrafica = new List<DashboardEntidad>();
        private List<DashboardEntidad> productosMasVendidos = new List<DashboardEntidad>();
        private List<DashboardEntidad> productosBajoMinimo = new List<DashboardEntidad>();
        private DashboardEntidad resumenActual;

        public FrmDashboard()
        {
            Text = "Panel de Administración";
            BackColor = Fondo;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(920, 660);
            Size = new Size(1360, 900);
            StartPosition = FormStartPosition.CenterParent;
            ConstruirInterfaz();
            Load += (s, e) => CargarDatos();
        }

        private void ConstruirInterfaz()
        {
            TableLayoutPanel raiz = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(28, 24, 28, 24), ColumnCount = 1, RowCount = 4, BackColor = Fondo };
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 82));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 158));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(raiz);

            Label titulo = new Label { Text = "Panel de administración\nResumen general de operaciones", Font = new Font("Segoe UI", 20F, FontStyle.Bold), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.White };
            raiz.Controls.Add(titulo, 0, 0);
            FlowLayoutPanel filtros = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 13, 0, 0), AutoScroll = true, BackColor = Fondo };
            Label lblDesde = CrearEtiqueta("Desde"); lblDesde.Padding = new Padding(4, 8, 2, 0); filtros.Controls.Add(lblDesde); desde.Width = 118; desde.Format = DateTimePickerFormat.Short; filtros.Controls.Add(desde);
            Label lblHasta = CrearEtiqueta("Hasta"); lblHasta.Padding = new Padding(4, 8, 2, 0); filtros.Controls.Add(lblHasta); hasta.Width = 118; hasta.Format = DateTimePickerFormat.Short; hasta.Value = DateTime.Today; filtros.Controls.Add(hasta);
            agrupacion.DropDownStyle = ComboBoxStyle.DropDownList; agrupacion.Items.AddRange(new object[] { "Día", "Semana", "Mes" }); agrupacion.SelectedIndex = 2; agrupacion.Width = 92; filtros.Controls.Add(agrupacion);
            tipoGrafica.DropDownStyle = ComboBoxStyle.DropDownList; tipoGrafica.Items.AddRange(new object[] { "Barras", "Líneas", "Circular" }); tipoGrafica.SelectedIndex = 0; tipoGrafica.Width = 92; filtros.Controls.Add(tipoGrafica);
            paleta.DropDownStyle = ComboBoxStyle.DropDownList; paleta.Items.AddRange(new object[] { "Azul", "Verde", "Morado", "Naranja" }); paleta.SelectedIndex = 0; paleta.Width = 92; filtros.Controls.Add(paleta);
            Button actualizar = new Button { Text = "Actualizar", Width = 100, Height = 36, BackColor = Azul, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            actualizar.Click += (s, e) => CargarDatos(); filtros.Controls.Add(actualizar); tipoGrafica.SelectedIndexChanged += (s, e) => AplicarTipoGrafica(); paleta.SelectedIndexChanged += (s, e) => AplicarTipoGrafica();
            Button exportar = new Button { Text = "Exportar Excel", Width = 112, Height = 36, BackColor = Color.FromArgb(20, 91, 139), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            exportar.Click += (s, e) => ExportarExcel(); filtros.Controls.Add(exportar);
            Button importar = new Button { Text = "Importar Excel", Width = 112, Height = 36, BackColor = Color.FromArgb(28, 108, 104), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            importar.Click += (s, e) => ImportarDashboard(); filtros.Controls.Add(importar);
            Button imprimir = new Button { Text = "Imprimir", Width = 88, Height = 36, BackColor = Color.FromArgb(55, 72, 96), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            imprimir.Click += (s, e) => ImprimirDashboard(); filtros.Controls.Add(imprimir);
            Label lblTarjetas = CrearEtiqueta("Tarjetas"); lblTarjetas.Padding = new Padding(4, 8, 2, 0); filtros.Controls.Add(lblTarjetas);
            paletaTarjetas.DropDownStyle = ComboBoxStyle.DropDownList;
            paletaTarjetas.Items.AddRange(new object[] { "Clara", "Azul", "Verde", "Morada" });
            paletaTarjetas.SelectedIndex = 0; paletaTarjetas.Width = 84;
            paletaTarjetas.SelectedIndexChanged += (s, e) => AplicarColoresTarjetas(tarjetasDashboard);
            filtros.Controls.Add(paletaTarjetas);
            raiz.Controls.Add(filtros, 0, 1);

            TableLayoutPanel tarjetas = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 5, RowCount = 1, BackColor = Fondo };
            tarjetasDashboard = tarjetas;
            for (int i = 0; i < 5; i++) tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            string[] nombres = { "VENTAS DEL PERÍODO", "COMPRAS DEL PERÍODO", "UTILIDAD BRUTA", "OPERACIONES", "BAJO STOCK MÍNIMO" };
            for (int i = 0; i < 5; i++)
            {
                Panel tarjeta = new Panel { Dock = DockStyle.Fill, BackColor = Superficie, Margin = new Padding(0, 0, 10, 0), Padding = new Padding(12), BorderStyle = BorderStyle.FixedSingle };
                tarjeta.Controls.Add(new Label { Text = nombres[i], Dock = DockStyle.Top, Height = 30, ForeColor = TextoSuave, Font = new Font("Segoe UI", 7.5F, FontStyle.Bold), AutoEllipsis = true });
                valores[i].Text = "—"; valores[i].Dock = DockStyle.Top; valores[i].Height = 42; valores[i].Font = new Font("Segoe UI", 16F, FontStyle.Bold); valores[i].ForeColor = Color.White; tarjeta.Controls.Add(valores[i]);
                if (i < 2) { variaciones[i].Text = ""; variaciones[i].Dock = DockStyle.Top; variaciones[i].ForeColor = Color.FromArgb(24, 220, 164); variaciones[i].Font = new Font("Segoe UI", 7.5F); tarjeta.Controls.Add(variaciones[i]); }
                tarjetas.Controls.Add(tarjeta, i, 0);
            }
            AplicarColoresTarjetas(tarjetas);
            raiz.Controls.Add(tarjetas, 0, 2);

            TableLayoutPanel inferior = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, BackColor = Fondo, Padding = new Padding(0, 8, 0, 0) };
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65)); inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            grafica.Dock = DockStyle.Fill; grafica.BackColor = Superficie; grafica.Palette = ChartColorPalette.BrightPastel;
            ChartArea area = new ChartArea("Principal"); area.BackColor = Superficie; area.AxisX.MajorGrid.LineColor = Borde; area.AxisY.MajorGrid.LineColor = Borde; area.AxisY.LabelStyle.Format = "C0"; grafica.ChartAreas.Add(area);
            area.AxisX.LabelStyle.ForeColor = TextoSuave;
            area.AxisY.LabelStyle.ForeColor = TextoSuave;
            grafica.Titles.Add(new Title("Ventas y compras del período", Docking.Top, new Font("Segoe UI", 11F, FontStyle.Bold), Color.White));
            grafica.Legends.Add(new Legend("Leyenda") { BackColor = Superficie, ForeColor = TextoSuave }); inferior.Controls.Add(CrearTarjeta("Tendencia de ventas", grafica), 0, 0);
            TabControl panelListas = new TabControl { Dock = DockStyle.Fill };
            TabPage paginaStock = new TabPage("Bajo mínimo"); TabPage paginaVentas = new TabPage("Más vendidos");
            ConfigurarTabla(tablaProductos); tablaProductos.Columns.Add("Producto", "Producto"); tablaProductos.Columns.Add("Stock", "Stock"); tablaProductos.Columns.Add("Minimo", "Mínimo");
            ConfigurarTabla(tablaMasVendidos); tablaMasVendidos.Columns.Add("Producto", "Producto"); tablaMasVendidos.Columns.Add("Unidades", "Unidades");
            paginaStock.Controls.Add(tablaProductos); paginaVentas.Controls.Add(tablaMasVendidos); panelListas.TabPages.Add(paginaStock); panelListas.TabPages.Add(paginaVentas);
            panelListas.BackColor = Superficie; panelListas.ForeColor = Color.White;
            inferior.Controls.Add(CrearTarjeta("Productos y alertas de inventario", panelListas), 1, 0);
            raiz.Controls.Add(inferior, 0, 3);
            desde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            AplicarEstiloControles(raiz);
        }

        private static Panel CrearTarjeta(string titulo, Control contenido)
        {
            Panel panel = new Panel { Dock = DockStyle.Fill, BackColor = Superficie, Padding = new Padding(16), Margin = new Padding(0, 0, 14, 0), BorderStyle = BorderStyle.FixedSingle };
            Label encabezado = new Label { Text = titulo, Dock = DockStyle.Top, Height = 30, ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            contenido.Dock = DockStyle.Fill;
            panel.Controls.Add(contenido);
            panel.Controls.Add(encabezado);
            return panel;
        }

        private static void AplicarEstiloControles(Control raiz)
        {
            foreach (Control control in raiz.Controls)
            {
                DataGridView tabla = control as DataGridView;
                if (tabla != null)
                {
                    tabla.BackgroundColor = Superficie;
                    tabla.GridColor = Borde;
                    tabla.ForeColor = Color.White;
                    tabla.EnableHeadersVisualStyles = false;
                    tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 43, 72);
                    tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    tabla.DefaultCellStyle.BackColor = Superficie;
                    tabla.DefaultCellStyle.ForeColor = Color.White;
                    tabla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(18, 75, 125);
                    tabla.DefaultCellStyle.SelectionForeColor = Color.White;
                }
                if (control.HasChildren) AplicarEstiloControles(control);
            }
        }

        private TableLayoutPanel tarjetasDashboard;

        private void AplicarColoresTarjetas(TableLayoutPanel tarjetas)
        {
            if (tarjetas == null) return;
            Color fondoTarjeta; Color texto; Color titulo;
            switch (paletaTarjetas.SelectedIndex)
            {
                case 1: fondoTarjeta = Color.FromArgb(224, 238, 250); texto = Color.FromArgb(7, 55, 95); titulo = Color.FromArgb(60, 95, 125); break;
                case 2: fondoTarjeta = Color.FromArgb(225, 244, 232); texto = Color.FromArgb(25, 85, 48); titulo = Color.FromArgb(70, 110, 80); break;
                case 3: fondoTarjeta = Color.FromArgb(238, 228, 248); texto = Color.FromArgb(72, 40, 105); titulo = Color.FromArgb(105, 80, 130); break;
                default: fondoTarjeta = Color.FromArgb(17, 27, 48); texto = Color.White; titulo = TextoSuave; break;
            }
            // El selector cambia la superficie y el contraste de cada tarjeta.
            foreach (Control tarjeta in tarjetas.Controls)
            {
                tarjeta.BackColor = fondoTarjeta;
                foreach (Control control in tarjeta.Controls)
                {
                    Label etiqueta = control as Label;
                    if (etiqueta != null && etiqueta.Font.Size > 15) etiqueta.ForeColor = texto;
                    else if (etiqueta != null) etiqueta.ForeColor = titulo;
                }
            }
        }

        private static Label CrearEtiqueta(string texto) { return new Label { Text = texto, AutoSize = true, Padding = new Padding(12, 8, 4, 0) }; }

        private static void ConfigurarTabla(DataGridView tabla)
        {
            tabla.Dock = DockStyle.Fill; tabla.ReadOnly = true; tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false;
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.RowHeadersVisible = false;
            tabla.BackgroundColor = Superficie; tabla.BorderStyle = BorderStyle.None;
            tabla.GridColor = Borde; tabla.ForeColor = Color.White; tabla.EnableHeadersVisualStyles = false;
            tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 43, 72);
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tabla.DefaultCellStyle.BackColor = Superficie; tabla.DefaultCellStyle.ForeColor = Color.White;
            tabla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(18, 75, 125);
            tabla.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void CargarDatos()
        {
            if (desde.Value.Date > hasta.Value.Date)
            {
                MessageBox.Show("La fecha inicial no puede ser posterior a la fecha final.",
                    "Rango de fechas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                desde.Focus();
                return;
            }

            try
            {
                string periodo = agrupacion.SelectedItem.ToString();
                DashboardEntidad resumen = negocio.ObtenerResumen(desde.Value, hasta.Value, periodo);
                resumenActual = resumen;
                valores[0].Text = resumen.TotalVentas.ToString("C2"); valores[1].Text = resumen.TotalCompras.ToString("C2");
                valores[2].Text = (resumen.TotalVentas - resumen.TotalCompras).ToString("C2");
                valores[3].Text = (resumen.CantidadVentas + resumen.CantidadCompras).ToString("N0");
                valores[4].Text = resumen.ProductosBajoMinimo.ToString("N0");
                variaciones[0].Text = TextoVariacion(resumen.TotalVentas, resumen.TotalVentasPeriodoAnterior);
                variaciones[1].Text = TextoVariacion(resumen.TotalCompras, resumen.TotalComprasPeriodoAnterior);
                productosMasVendidos = negocio.ObtenerMasVendidos(desde.Value, hasta.Value);
                productosBajoMinimo = negocio.ObtenerProductosBajoMinimo();
                tablaProductos.Rows.Clear();
                foreach (DashboardEntidad producto in productosBajoMinimo) tablaProductos.Rows.Add(producto.Producto, producto.Stock, producto.StockMinimo);
                tablaMasVendidos.Rows.Clear();
                foreach (DashboardEntidad producto in productosMasVendidos) tablaMasVendidos.Rows.Add(producto.Producto, producto.UnidadesVendidas);
                serieGrafica = negocio.ObtenerSerie(desde.Value, hasta.Value, periodo);
                AplicarTipoGrafica();
            }
            catch (Exception ex) { MessageBox.Show("No fue posible cargar el dashboard: " + MensajesError.Resumir(ex), "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void ExportarExcel()
        {
            if (resumenActual == null)
            {
                MessageBox.Show("Actualiza el dashboard antes de exportar el reporte.", "Exportar reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string ruta;
                ArchivosExcel.ExportarDashboard(resumenActual, serieGrafica, productosBajoMinimo, productosMasVendidos,
                    desde.Value, hasta.Value, agrupacion.SelectedItem.ToString(), out ruta);
                if (!string.IsNullOrEmpty(ruta)) MessageBox.Show("El libro de Excel se exportó correctamente.", "Exportar reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("No fue posible exportar el reporte: " + MensajesError.Resumir(ex), "Exportar reporte", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ImprimirDashboard()
        {
            if (resumenActual == null) { MessageBox.Show("Actualiza el dashboard antes de imprimir."); return; }
            using (PrintPreviewDialog vista = new PrintPreviewDialog())
            using (System.Drawing.Printing.PrintDocument documento = new System.Drawing.Printing.PrintDocument())
            {
                documento.PrintPage += (s, e) =>
                {
                    float y = 45;
                    using (Font titulo = new Font("Segoe UI", 16F, FontStyle.Bold))
                    using (Font texto = new Font("Segoe UI", 10F))
                    {
                        e.Graphics.DrawString("Reporte del dashboard", titulo, Brushes.Black, 45, y); y += 38;
                        e.Graphics.DrawString("Período: " + desde.Value.ToString("dd/MM/yyyy") + " al " + hasta.Value.ToString("dd/MM/yyyy"), texto, Brushes.Black, 45, y); y += 28;
                        e.Graphics.DrawString("Entradas (compras): " + resumenActual.TotalCompras.ToString("C2") + "     Salidas (ventas): " + resumenActual.TotalVentas.ToString("C2"), texto, Brushes.Black, 45, y); y += 25;
                        e.Graphics.DrawString("Operaciones: " + (resumenActual.CantidadVentas + resumenActual.CantidadCompras) + "     Productos bajo mínimo: " + resumenActual.ProductosBajoMinimo, texto, Brushes.Black, 45, y);
                    }
                };
                vista.Document = documento;
                vista.ShowDialog(this);
            }
        }

        private void ImportarDashboard()
        {
            string nombre; DataTable datos;
            try { datos = ArchivosExcel.ImportarTabla(out nombre); }
            catch (Exception ex) { MessageBox.Show("No fue posible leer el archivo: " + MensajesError.Resumir(ex), "Importación", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (datos == null) return;
            using (Form vista = new Form { Text = "Vista previa: " + nombre, Size = new Size(900, 600), StartPosition = FormStartPosition.CenterParent })
            using (DataGridView tabla = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells, DataSource = datos })
            { vista.Controls.Add(tabla); vista.ShowDialog(this); }
        }

        private static string TextoVariacion(decimal actual, decimal anterior)
        {
            if (anterior == 0) return actual == 0 ? "Sin variación" : "Sin período anterior comparable";
            return string.Format("{0:+0.0%;-0.0%;0.0%} frente al período anterior", (actual - anterior) / anterior);
        }

        private void AplicarTipoGrafica()
        {
            if (grafica.ChartAreas.Count == 0) return;
            grafica.Series.Clear();
            if (tipoGrafica.SelectedIndex == 2)
            {
                grafica.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
                grafica.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
                Series productos = new Series("Productos más vendidos")
                {
                    ChartType = SeriesChartType.Pie,
                    ChartArea = "Principal",
                    Legend = "Leyenda",
                    IsValueShownAsLabel = true
                };
                foreach (DashboardEntidad producto in productosMasVendidos)
                    productos.Points.AddXY(producto.Producto, producto.UnidadesVendidas);
                grafica.Series.Add(productos);
                grafica.Palette = PaletaActual();
                grafica.Legends[0].Enabled = true;
                return;
            }

            grafica.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
            grafica.ChartAreas[0].AxisY.Enabled = AxisEnabled.True;
            grafica.Legends[0].Enabled = true;
            SeriesChartType tipo = tipoGrafica.SelectedIndex == 1 ? SeriesChartType.Line : SeriesChartType.Column;
            bool esLinea = tipo == SeriesChartType.Line;
            grafica.ChartAreas[0].AxisX.Interval = 1;
            Series ventas = new Series("Ventas")
            {
                ChartType = tipo,
                ChartArea = "Principal",
                Legend = "Leyenda",
                BorderWidth = 3,
                IsXValueIndexed = true,
                MarkerStyle = esLinea ? MarkerStyle.Circle : MarkerStyle.None,
                MarkerSize = esLinea ? 7 : 0
            };
            Series compras = new Series("Compras")
            {
                ChartType = tipo,
                ChartArea = "Principal",
                Legend = "Leyenda",
                BorderWidth = 3,
                IsXValueIndexed = true,
                MarkerStyle = esLinea ? MarkerStyle.Circle : MarkerStyle.None,
                MarkerSize = esLinea ? 7 : 0
            };
            string periodo = agrupacion.SelectedItem == null ? "Mes" : agrupacion.SelectedItem.ToString();
            foreach (DashboardEntidad punto in serieGrafica)
            {
                string etiqueta = periodo == "Mes" ? punto.Fecha.ToString("MMM yyyy") : punto.Fecha.ToString("dd/MM");
                ventas.Points.AddXY(etiqueta, punto.VentasPorFecha);
                compras.Points.AddXY(etiqueta, punto.ComprasPorFecha);
            }
            grafica.Series.Add(ventas);
            grafica.Series.Add(compras);
            Color[] coloresVentas = { Color.FromArgb(0, 150, 205), Color.FromArgb(20, 145, 90), Color.FromArgb(120, 75, 180), Color.FromArgb(230, 125, 35) };
            Color[] coloresCompras = { Color.FromArgb(245, 166, 35), Color.FromArgb(230, 90, 75), Color.FromArgb(235, 180, 45), Color.FromArgb(80, 130, 190) };
            int indiceColor = Math.Max(0, paleta.SelectedIndex);
            ventas.Color = coloresVentas[indiceColor];
            compras.Color = coloresCompras[indiceColor];
            grafica.Palette = PaletaActual();
        }

        private ChartColorPalette PaletaActual()
        {
            switch (paleta.SelectedIndex)
            {
                case 1: return ChartColorPalette.Excel;
                case 2: return ChartColorPalette.Excel;
                case 3: return ChartColorPalette.Fire;
                default: return ChartColorPalette.BrightPastel;
            }
        }
    }
}
