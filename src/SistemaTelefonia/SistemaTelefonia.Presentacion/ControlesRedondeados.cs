using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>RadioButton nativo con texto blanco legible también cuando está deshabilitado.</summary>
    public class RadioButtonTextoBlanco : RadioButton
    {
        public RadioButtonTextoBlanco()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            ForeColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            int diametro = 14;
            Rectangle circulo = new Rectangle(2, Math.Max(2, (Height - diametro) / 2), diametro, diametro);
            Color borde = Enabled ? Color.FromArgb(0, 217, 255) : Color.FromArgb(169, 186, 200);
            using (SolidBrush fondo = new SolidBrush(BackColor)) e.Graphics.FillEllipse(fondo, circulo);
            using (Pen lapiz = new Pen(borde, 1.5f)) e.Graphics.DrawEllipse(lapiz, circulo);
            if (Checked)
            {
                Rectangle punto = Rectangle.Inflate(circulo, -4, -4);
                using (SolidBrush relleno = new SolidBrush(Color.FromArgb(0, 217, 255))) e.Graphics.FillEllipse(relleno, punto);
            }
            Rectangle texto = new Rectangle(22, 0, Math.Max(0, Width - 22), Height);
            TextRenderer.DrawText(e.Graphics, Text, Font, texto, Color.White,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            if (Focused && ShowFocusCues) ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(ClientRectangle, -1, -1));
        }
    }

    /// <summary>Panel con esquinas suaves para las tarjetas de Usuarios.</summary>
    public class PanelRedondeado : Panel
    {
        private int radioEsquinas = 12;

        public int RadioEsquinas
        {
            get { return radioEsquinas; }
            set { radioEsquinas = Math.Max(0, value); ActualizarRegion(); Invalidate(); }
        }

        public PanelRedondeado()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            UpdateStyles();
            Resize += (s, e) => ActualizarRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 2 || Height < 2) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectangulo = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath ruta = CrearRuta(rectangulo, radioEsquinas))
            using (Pen borde = new Pen(Color.FromArgb(45, 120, 155, 190), 1F))
                e.Graphics.DrawPath(borde, ruta);
        }

        private void ActualizarRegion()
        {
            if (Width < 2 || Height < 2) return;
            Region anterior = Region;
            using (GraphicsPath ruta = CrearRuta(new Rectangle(0, 0, Width, Height), radioEsquinas))
                Region = new Region(ruta);
            if (anterior != null) anterior.Dispose();
        }

        internal static GraphicsPath CrearRuta(Rectangle rectangulo, int radio)
        {
            int diametro = Math.Min(Math.Max(0, radio) * 2,
                Math.Min(rectangulo.Width, rectangulo.Height));
            GraphicsPath ruta = new GraphicsPath();
            if (diametro < 2)
            {
                ruta.AddRectangle(rectangulo);
                return ruta;
            }
            Rectangle arco = new Rectangle(rectangulo.Left, rectangulo.Top, diametro, diametro);
            ruta.AddArc(arco, 180, 90);
            arco.X = rectangulo.Right - diametro;
            ruta.AddArc(arco, 270, 90);
            arco.Y = rectangulo.Bottom - diametro;
            ruta.AddArc(arco, 0, 90);
            arco.X = rectangulo.Left;
            ruta.AddArc(arco, 90, 90);
            ruta.CloseFigure();
            return ruta;
        }
    }

    /// <summary>DataGridView con recorte redondeado y doble búfer para la grilla de Usuarios.</summary>
    public class DataGridViewRedondeado : DataGridView
    {
        private int radioEsquinas = 10;

        public int RadioEsquinas
        {
            get { return radioEsquinas; }
            set { radioEsquinas = Math.Max(0, value); ActualizarRegion(); Invalidate(); }
        }

        public DataGridViewRedondeado()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            Resize += (s, e) => ActualizarRegion();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ActualizarRegion();
        }

        private void ActualizarRegion()
        {
            if (Width < 2 || Height < 2 || IsDisposed) return;
            Region anterior = Region;
            using (GraphicsPath ruta = PanelRedondeado.CrearRuta(
                new Rectangle(0, 0, Width, Height), radioEsquinas))
                Region = new Region(ruta);
            if (anterior != null) anterior.Dispose();
        }
    }

    /// <summary>Aplica recorte redondeado a controles estándar de entrada sin reemplazarlos.</summary>
    internal static class CampoRedondeado
    {
        private sealed class Registro
        {
        }

        private static readonly ConditionalWeakTable<Control, Registro> ControlesConfigurados =
            new ConditionalWeakTable<Control, Registro>();

        public static void Aplicar(Control control, int radio)
        {
            if (control == null || ControlesConfigurados.TryGetValue(control, out _)) return;

            ControlesConfigurados.Add(control, new Registro());
            control.Resize += (s, e) => ActualizarRegion((Control)s, radio);
            control.HandleCreated += (s, e) => ActualizarRegion((Control)s, radio);
            ActualizarRegion(control, radio);
        }

        private static void ActualizarRegion(Control control, int radio)
        {
            if (control == null || control.IsDisposed || control.Width < 2 || control.Height < 2) return;

            Region anterior = control.Region;
            using (GraphicsPath ruta = PanelRedondeado.CrearRuta(
                new Rectangle(0, 0, control.Width, control.Height), radio))
                control.Region = new Region(ruta);
            if (anterior != null) anterior.Dispose();
        }
    }
}
