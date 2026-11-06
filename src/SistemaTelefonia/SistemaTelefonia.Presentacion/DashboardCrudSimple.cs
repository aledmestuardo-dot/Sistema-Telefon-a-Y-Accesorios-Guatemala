using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Resumen visual reutilizable para los CRUD simples.</summary>
    internal sealed class DashboardCrudDatos
    {
        public string[] TitulosIndicadores { get; set; }
        public string[] ValoresIndicadores { get; set; }
        public IList<KeyValuePair<string, int>> Serie { get; set; }
        public string Alertas { get; set; }
        public string Actividad { get; set; }
    }

    internal sealed class DashboardCrudSimple : PanelRedondeado
    {
        private static readonly Color Fondo = ColorTranslator.FromHtml("#08243A");
        private static readonly Color Superficie = ColorTranslator.FromHtml("#0B3558");
        private static readonly Color Cian = ColorTranslator.FromHtml("#00D9FF");
        private static readonly Color TextoSuave = ColorTranslator.FromHtml("#A9BAC8");
        private readonly Func<DashboardCrudDatos> cargarDatos;
        private readonly Label[] titulos = new Label[4];
        private readonly Label[] valores = new Label[4];
        private readonly Label alertas = new Label();
        private readonly Label actividad = new Label();
        private readonly Chart grafica = new Chart();
        private readonly Button actualizar = new Button();
        private bool cargando;

        public DashboardCrudSimple(string titulo, string tituloGrafica, Func<DashboardCrudDatos> cargarDatos)
        {
            this.cargarDatos = cargarDatos ?? throw new ArgumentNullException("cargarDatos");
            Name = "pnlDashboard" + titulo.Replace(" ", string.Empty);
            Height = 360;
            Dock = DockStyle.Top;
            Margin = new Padding(0, 0, 0, 10);
            Padding = new Padding(12);
            BackColor = Fondo;
            RadioEsquinas = 12;

            TableLayoutPanel raiz = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
                BackColor = Fondo, Margin = Padding.Empty, Padding = Padding.Empty
            };
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(raiz);

            TableLayoutPanel encabezado = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                BackColor = Fondo, Margin = Padding.Empty, Padding = Padding.Empty
            };
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            Label lblTitulo = new Label
            {
                Text = titulo, Dock = DockStyle.Fill, ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft
            };
            actualizar.Text = "↻ Actualizar";
            actualizar.Dock = DockStyle.Fill;
            actualizar.FlatStyle = FlatStyle.Flat;
            actualizar.FlatAppearance.BorderSize = 0;
            actualizar.BackColor = ColorTranslator.FromHtml("#0B6E91");
            actualizar.ForeColor = Color.White;
            actualizar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            actualizar.Click += async (s, e) => await CargarAsync();
            encabezado.Controls.Add(lblTitulo, 0, 0);
            encabezado.Controls.Add(actualizar, 1, 0);
            raiz.Controls.Add(encabezado, 0, 0);

            TableLayoutPanel tarjetas = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1,
                BackColor = Fondo, Margin = new Padding(0, 5, 0, 5), Padding = Padding.Empty
            };
            for (int i = 0; i < 4; i++)
            {
                tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                Panel tarjeta = new Panel
                {
                    Dock = DockStyle.Fill, Margin = new Padding(0, 0, i == 3 ? 0 : 8, 0),
                    Padding = new Padding(10, 6, 6, 4), BackColor = Superficie
                };
                titulos[i] = new Label
                {
                    Dock = DockStyle.Top, Height = 20, ForeColor = TextoSuave,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), AutoEllipsis = true
                };
                valores[i] = new Label
                {
                    Text = "—", Dock = DockStyle.Fill, ForeColor = i == 0 ? Cian : Color.White,
                    Font = new Font("Segoe UI", 18F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft
                };
                tarjeta.Controls.Add(valores[i]);
                tarjeta.Controls.Add(titulos[i]);
                tarjetas.Controls.Add(tarjeta, i, 0);
            }
            tarjetas.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            raiz.Controls.Add(tarjetas, 0, 1);

            TableLayoutPanel detalle = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                BackColor = Fondo, Margin = Padding.Empty, Padding = Padding.Empty
            };
            detalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64F));
            detalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            ConfigurarGrafica(tituloGrafica);
            Panel contenedorGrafica = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(7, 5, 7, 5),
                BackColor = Superficie, Margin = Padding.Empty
            };
            TableLayoutPanel graficaLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                BackColor = Superficie, Margin = Padding.Empty, Padding = Padding.Empty
            };
            graficaLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 21));
            graficaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            graficaLayout.Controls.Add(new Label
            {
                Text = tituloGrafica, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Cian
            }, 0, 0);
            graficaLayout.Controls.Add(grafica, 0, 1);
            contenedorGrafica.Controls.Add(graficaLayout);
            detalle.Controls.Add(contenedorGrafica, 0, 0);

            TableLayoutPanel texto = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                BackColor = Fondo, Padding = Padding.Empty, Margin = new Padding(8, 0, 0, 0)
            };
            texto.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            texto.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            texto.Controls.Add(CrearPanelTexto("Alertas operativas", alertas), 0, 0);
            texto.Controls.Add(CrearPanelTexto("Actividad reciente", actividad), 0, 1);
            detalle.Controls.Add(texto, 1, 0);
            raiz.Controls.Add(detalle, 0, 2);
        }

        public async Task CargarAsync()
        {
            if (cargando || IsDisposed) return;
            cargando = true;
            actualizar.Enabled = false;
            actualizar.Text = "Cargando…";
            try
            {
                DashboardCrudDatos datos = await Task.Run(cargarDatos);
                if (IsDisposed) return;
                for (int i = 0; i < 4; i++)
                {
                    titulos[i].Text = datos.TitulosIndicadores[i];
                    valores[i].Text = datos.ValoresIndicadores[i];
                }
                alertas.Text = datos.Alertas;
                actividad.Text = datos.Actividad;
                grafica.Series[0].Points.Clear();
                foreach (KeyValuePair<string, int> punto in datos.Serie)
                    grafica.Series[0].Points.AddXY(punto.Key, punto.Value);
            }
            catch (Exception)
            {
                if (!IsDisposed)
                {
                    for (int i = 0; i < 4; i++) valores[i].Text = "—";
                    alertas.Text = "No se pudo cargar el resumen. Comprueba la conexión.";
                    actividad.Text = "Disponible al recuperar la conexión.";
                }
            }
            finally
            {
                cargando = false;
                if (!IsDisposed)
                {
                    actualizar.Enabled = true;
                    actualizar.Text = "↻ Actualizar";
                }
            }
        }

        private void ConfigurarGrafica(string titulo)
        {
            grafica.Dock = DockStyle.Fill;
            grafica.BackColor = Superficie;
            grafica.Palette = ChartColorPalette.None;
            ChartArea area = new ChartArea("Principal");
            area.BackColor = Superficie;
            area.AxisX.LabelStyle.ForeColor = TextoSuave;
            area.AxisY.LabelStyle.ForeColor = TextoSuave;
            area.AxisX.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisY.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#34495E");
            grafica.ChartAreas.Add(area);
            grafica.Legends.Add(new Legend("Leyenda")
            {
                Docking = Docking.Bottom, BackColor = Superficie, ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F)
            });
            grafica.Series.Add(new Series(titulo)
            {
                ChartType = SeriesChartType.Line, ChartArea = "Principal", Legend = "Leyenda",
                BorderWidth = 3, Color = Cian, MarkerStyle = MarkerStyle.Circle, MarkerSize = 5
            });
        }

        private static Panel CrearPanelTexto(string titulo, Label contenido)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(9, 6, 9, 6),
                BackColor = Superficie, Margin = Padding.Empty
            };
            contenido.Dock = DockStyle.Fill;
            contenido.ForeColor = Color.White;
            contenido.Font = new Font("Segoe UI", 9F);
            contenido.TextAlign = ContentAlignment.TopLeft;
            contenido.AutoEllipsis = true;
            Label encabezado = new Label
            {
                Text = titulo, Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Cian,
                AutoEllipsis = true
            };
            panel.Controls.Add(contenido);
            panel.Controls.Add(encabezado);
            return panel;
        }
    }
}
