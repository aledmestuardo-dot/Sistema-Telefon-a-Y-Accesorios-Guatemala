using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Presenta el dashboard comercial con la distribución de Facturación.</summary>
    internal sealed class DashboardFacturacionVisual
    {
        private readonly Label[] valores = new Label[4];
        private readonly Label lblEstado;
        private readonly Label lblActividad;
        private readonly Chart grafica;
        private readonly Button btnActualizar;

        public Panel Panel { get; private set; }
        public FlowLayoutPanel Acciones { get; private set; }
        public Label[] Valores { get { return valores; } }

        public DashboardFacturacionVisual(string titulo, IEnumerable<Control> acciones, EventHandler actualizar)
        {
            Color fondo = ColorTranslator.FromHtml("#08243A");
            Color superficie = ColorTranslator.FromHtml("#0B3558");
            Panel = new PanelRedondeado
            {
                Name = "pnlDashboard" + titulo.Replace(" ", string.Empty), Dock = DockStyle.Fill,
                Margin = Padding.Empty, Padding = new Padding(12), BackColor = fondo, RadioEsquinas = 12
            };

            TableLayoutPanel raiz = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            raiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            TableLayoutPanel encabezado = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            encabezado.Controls.Add(new Label
            {
                Text = "Dashboard de " + titulo.ToLowerInvariant(), Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);
            btnActualizar = new Button
            {
                Name = "btnActualizarDashboard" + titulo.Replace(" ", string.Empty), Text = "↻ Actualizar",
                Dock = DockStyle.Fill, Margin = new Padding(4, 1, 0, 1), FlatStyle = FlatStyle.Flat,
                BackColor = ColorTranslator.FromHtml("#0B6E91"), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnActualizar.FlatAppearance.BorderSize = 0;
            if (actualizar != null) btnActualizar.Click += actualizar;
            encabezado.Controls.Add(btnActualizar, 1, 0);
            raiz.Controls.Add(encabezado, 0, 0);

            FlowLayoutPanel filaAcciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false, Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            Acciones = filaAcciones;
            if (acciones != null)
                foreach (Control accion in acciones)
                {
                    if (accion == null) continue;
                    accion.Dock = DockStyle.None;
                    accion.Size = new Size(232, 36);
                    accion.Margin = new Padding(4, 9, 4, 9);
                    ButtonBase botonAccion = accion as ButtonBase;
                    if (botonAccion != null)
                        botonAccion.TextAlign = ContentAlignment.MiddleCenter;
                    Label etiquetaAccion = accion as Label;
                    if (etiquetaAccion != null)
                        etiquetaAccion.TextAlign = ContentAlignment.MiddleCenter;
                    filaAcciones.Controls.Add(accion);
                }
            raiz.Controls.Add(filaAcciones, 0, 1);

            TableLayoutPanel indicadores = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            for (int i = 0; i < 4; i++)
            {
                indicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                indicadores.Controls.Add(new Label
                {
                    Text = "INDICADOR " + (i + 1), Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true
                }, i, 0);
                valores[i] = new Label
                {
                    Text = "0", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                    ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true
                };
                indicadores.Controls.Add(valores[i], i, 1);
            }
            indicadores.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            indicadores.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            raiz.Controls.Add(indicadores, 0, 2);

            TableLayoutPanel inferior = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = fondo
            };
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            Panel panelGrafica = CrearPanel("Actividad por mes", superficie);
            grafica = new Chart { Dock = DockStyle.Fill, BackColor = superficie, Palette = ChartColorPalette.None };
            ChartArea area = new ChartArea("Principal") { BackColor = superficie };
            area.AxisX.LabelStyle.ForeColor = Color.White;
            area.AxisY.LabelStyle.ForeColor = Color.White;
            area.AxisX.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisY.LineColor = ColorTranslator.FromHtml("#526779");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#34495E");
            grafica.ChartAreas.Add(area);
            grafica.Series.Add(new Series("Actividad")
            {
                ChartType = SeriesChartType.Column, ChartArea = "Principal",
                Color = ColorTranslator.FromHtml("#00D9FF"), BorderWidth = 2
            });
            panelGrafica.Controls.Add(grafica);
            inferior.Controls.Add(panelGrafica, 0, 0);

            TableLayoutPanel panelDerecho = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                Margin = new Padding(8, 0, 0, 0), Padding = Padding.Empty, BackColor = fondo
            };
            panelDerecho.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Panel estado = CrearPanel("Resumen por estado", superficie);
            Panel actividad = CrearPanel("Actividad reciente", superficie);
            lblEstado = CrearTextoPanel(estado);
            lblActividad = CrearTextoPanel(actividad);
            panelDerecho.Controls.Add(estado, 0, 0);
            panelDerecho.Controls.Add(actividad, 0, 1);
            inferior.Controls.Add(panelDerecho, 1, 0);
            raiz.Controls.Add(inferior, 0, 3);
            Panel.Controls.Add(raiz);
        }

        public void Actualizar(string[] titulos, string[] textos,
            IEnumerable<KeyValuePair<string, double>> puntos, string resumenEstado, string actividadReciente)
        {
            if (Panel.IsDisposed) return;
            for (int i = 0; i < 4; i++)
            {
                Control encabezado = ((TableLayoutPanel)((TableLayoutPanel)Panel.Controls[0]).GetControlFromPosition(0, 2))
                    .GetControlFromPosition(i, 0);
                encabezado.Text = titulos[i];
                valores[i].Text = textos[i];
            }
            lblEstado.Text = resumenEstado;
            lblActividad.Text = actividadReciente;
            grafica.Series[0].Points.Clear();
            if (puntos != null)
                foreach (KeyValuePair<string, double> punto in puntos)
                    grafica.Series[0].Points.AddXY(punto.Key, punto.Value);
        }

        private static Panel CrearPanel(string titulo, Color fondo)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill, Padding = new Padding(9, 6, 9, 6),
                BackColor = fondo, Margin = new Padding(0, 4, 0, 4)
            };
            Label encabezado = new Label
            {
                Text = titulo, Dock = DockStyle.Top, Height = 21,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#00D9FF"), AutoEllipsis = true
            };
            panel.Controls.Add(encabezado);
            return panel;
        }

        private static Label CrearTextoPanel(Panel panel)
        {
            Label texto = new Label
            {
                Dock = DockStyle.Fill, ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F), TextAlign = ContentAlignment.TopLeft,
                AutoEllipsis = true
            };
            panel.Controls.Add(texto);
            return texto;
        }
    }
}
