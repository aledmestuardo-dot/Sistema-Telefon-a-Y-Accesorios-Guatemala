using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using ClosedXML.Excel;
using SistemaTelefonia.Entidad;

namespace SistemaTelefonia.Presentacion
{
    /// <summary>Exporta e importa CSV con codificación UTF-8, compatible con Excel.</summary>
    internal static class ArchivosExcel
    {
        public static void ExportarTabla(DataGridView tabla, string titulo, out string ruta)
        {
            ruta = null;
            using (SaveFileDialog dialogo = new SaveFileDialog { Filter = "Libro de Excel (*.xlsx)|*.xlsx", DefaultExt = "xlsx", AddExtension = true, FileName = titulo + ".xlsx" })
            {
                if (dialogo.ShowDialog() != DialogResult.OK) return;
                ruta = dialogo.FileName;
                using (XLWorkbook libro = new XLWorkbook())
                {
                    IXLWorksheet hoja = libro.Worksheets.Add("Datos");
                    int columnaDestino = 1;
                    foreach (DataGridViewColumn columna in tabla.Columns)
                    {
                        if (!columna.Visible) continue;
                        hoja.Cell(1, columnaDestino++).Value = string.IsNullOrWhiteSpace(columna.DataPropertyName) ? columna.HeaderText : columna.DataPropertyName;
                    }
                    int filaDestino = 2;
                    foreach (DataGridViewRow fila in tabla.Rows)
                    {
                        if (fila.IsNewRow) continue;
                        columnaDestino = 1;
                        foreach (DataGridViewColumn columna in tabla.Columns)
                        {
                            if (!columna.Visible) continue;
                            object valor = fila.Cells[columna.Index].Value;
                            IXLCell celda = hoja.Cell(filaDestino, columnaDestino++);
                            AsignarValor(celda, valor);
                        }
                        filaDestino++;
                    }
                    hoja.Row(1).Style.Font.Bold = true;
                    hoja.SheetView.FreezeRows(1);
                    hoja.Columns().AdjustToContents();
                    libro.SaveAs(dialogo.FileName);
                }
            }
        }

        public static DataTable ImportarTabla(out string nombreArchivo)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog { Filter = "Excel o CSV (*.xlsx;*.csv)|*.xlsx;*.csv", Title = "Seleccionar archivo de Excel" })
            {
                if (dialogo.ShowDialog() != DialogResult.OK) { nombreArchivo = null; return null; }
                nombreArchivo = Path.GetFileName(dialogo.FileName);
                DataTable tabla = new DataTable();
                if (string.Equals(Path.GetExtension(dialogo.FileName), ".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    using (XLWorkbook libro = new XLWorkbook(dialogo.FileName))
                    {
                        IXLWorksheet hoja = libro.Worksheet(1);
                        IXLRange rango = hoja.RangeUsed();
                        if (rango == null) return tabla;
                        int primeraColumna = rango.FirstColumn().ColumnNumber();
                        int ultimaColumna = rango.LastColumn().ColumnNumber();
                        int primeraFila = rango.FirstRow().RowNumber();
                        int ultimaFila = rango.LastRow().RowNumber();
                        for (int columna = primeraColumna; columna <= ultimaColumna; columna++)
                            tabla.Columns.Add(hoja.Cell(primeraFila, columna).GetString());
                        for (int numeroFila = primeraFila + 1; numeroFila <= ultimaFila; numeroFila++)
                        {
                            object[] valores = new object[tabla.Columns.Count];
                            for (int columna = primeraColumna; columna <= ultimaColumna; columna++)
                                valores[columna - primeraColumna] = hoja.Cell(numeroFila, columna).Value.ToString();
                            tabla.Rows.Add(valores);
                        }
                    }
                    return tabla;
                }
                using (StreamReader archivo = new StreamReader(dialogo.FileName, Encoding.UTF8, true))
                {
                    string linea; bool encabezado = true;
                    while ((linea = archivo.ReadLine()) != null)
                    {
                        string[] campos = Separar(linea);
                        if (encabezado)
                        {
                            foreach (string campo in campos) tabla.Columns.Add(campo);
                            encabezado = false;
                        }
                        else
                        {
                            while (campos.Length > tabla.Columns.Count) tabla.Columns.Add("Columna" + (tabla.Columns.Count + 1));
                            tabla.Rows.Add(campos);
                        }
                    }
                }
                return tabla;
            }
        }

        public static object CrearEntidad(DataRow fila, Type tipoEntidad)
        {
            object entidad = Activator.CreateInstance(tipoEntidad);
            foreach (PropertyInfo propiedad in tipoEntidad.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite))
            {
                if (!fila.Table.Columns.Contains(propiedad.Name)) continue;
                string texto = Convert.ToString(fila[propiedad.Name]);
                if (string.IsNullOrWhiteSpace(texto)) continue;
                Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
                object valor;
                if (tipo == typeof(string)) valor = texto;
                else if (tipo == typeof(DateTime))
                {
                    DateTime fecha;
                    if (!DateTime.TryParse(texto, CultureInfo.CurrentCulture, DateTimeStyles.None, out fecha) &&
                        !DateTime.TryParse(texto, CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                        throw new FormatException("La fecha de " + propiedad.Name + " no tiene un formato válido.");
                    valor = fecha;
                }
                else if (tipo == typeof(bool))
                {
                    string estado = texto.Trim();
                    if (estado == "1" || estado.Equals("Activo", StringComparison.OrdinalIgnoreCase)) valor = true;
                    else if (estado == "0" || estado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase)) valor = false;
                    else valor = bool.Parse(estado);
                }
                else
                {
                    try { valor = Convert.ChangeType(texto, tipo, CultureInfo.InvariantCulture); }
                    catch { valor = Convert.ChangeType(texto, tipo, CultureInfo.CurrentCulture); }
                }
                propiedad.SetValue(entidad, valor, null);
            }
            return entidad;
        }

        public static void ExportarDashboard(DashboardEntidad resumen, System.Collections.Generic.IList<DashboardEntidad> serie,
            System.Collections.Generic.IList<DashboardEntidad> bajoMinimo, System.Collections.Generic.IList<DashboardEntidad> masVendidos,
            DateTime desde, DateTime hasta, string agrupacion, out string ruta)
        {
            ruta = null;
            using (SaveFileDialog dialogo = new SaveFileDialog { Filter = "Libro de Excel (*.xlsx)|*.xlsx", DefaultExt = "xlsx", AddExtension = true, FileName = "Reporte_Dashboard_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx" })
            {
                if (dialogo.ShowDialog() != DialogResult.OK) return;
                ruta = dialogo.FileName;
                using (XLWorkbook libro = new XLWorkbook())
                {
                    IXLWorksheet resumenHoja = libro.Worksheets.Add("Resumen");
                    resumenHoja.Cell(1, 1).Value = "Indicador"; resumenHoja.Cell(1, 2).Value = "Valor";
                    resumenHoja.Cell(2, 1).Value = "Desde"; resumenHoja.Cell(2, 2).Value = desde;
                    resumenHoja.Cell(3, 1).Value = "Hasta"; resumenHoja.Cell(3, 2).Value = hasta;
                    resumenHoja.Cell(4, 1).Value = "Agrupación"; resumenHoja.Cell(4, 2).Value = agrupacion;
                    resumenHoja.Cell(5, 1).Value = "Ventas del período"; resumenHoja.Cell(5, 2).Value = resumen.TotalVentas;
                    resumenHoja.Cell(6, 1).Value = "Compras del período"; resumenHoja.Cell(6, 2).Value = resumen.TotalCompras;
                    resumenHoja.Cell(7, 1).Value = "Cantidad de operaciones"; resumenHoja.Cell(7, 2).Value = resumen.CantidadVentas + resumen.CantidadCompras;
                    resumenHoja.Cell(8, 1).Value = "Productos bajo stock mínimo"; resumenHoja.Cell(8, 2).Value = resumen.ProductosBajoMinimo;
                    CrearHoja(libro, "Entradas y salidas", new[] { "Período", "Ventas", "Compras" }, serie.Select(x => new object[] { x.Fecha, x.VentasPorFecha, x.ComprasPorFecha }));
                    CrearHoja(libro, "Bajo mínimo", new[] { "Producto", "Stock", "Mínimo" }, bajoMinimo.Select(x => new object[] { x.Producto, x.Stock, x.StockMinimo }));
                    CrearHoja(libro, "Más vendidos", new[] { "Producto", "Unidades vendidas" }, masVendidos.Select(x => new object[] { x.Producto, x.UnidadesVendidas }));
                    foreach (IXLWorksheet hoja in libro.Worksheets) { hoja.Row(1).Style.Font.Bold = true; hoja.Columns().AdjustToContents(); }
                    libro.SaveAs(dialogo.FileName);
                }
            }
        }

        private static void CrearHoja(XLWorkbook libro, string nombre, string[] encabezados, System.Collections.Generic.IEnumerable<object[]> filas)
        {
            IXLWorksheet hoja = libro.Worksheets.Add(nombre);
            for (int c = 0; c < encabezados.Length; c++) hoja.Cell(1, c + 1).Value = encabezados[c];
            int r = 2;
            foreach (object[] fila in filas)
            {
                for (int c = 0; c < fila.Length; c++)
                {
                    AsignarValor(hoja.Cell(r, c + 1), fila[c]);
                }
                r++;
            }
        }

        private static void AsignarValor(IXLCell celda, object valor)
        {
            if (valor == null || valor == DBNull.Value) { celda.Value = string.Empty; return; }
            if (valor is DateTime) celda.Value = (DateTime)valor;
            else if (valor is bool) celda.Value = (bool)valor;
            else if (valor is int) celda.Value = (int)valor;
            else if (valor is long) celda.Value = (long)valor;
            else if (valor is decimal) celda.Value = (decimal)valor;
            else if (valor is double) celda.Value = (double)valor;
            else celda.Value = Convert.ToString(valor);
        }

        private static string Campo(string valor) { return "\"" + (valor ?? string.Empty).Replace("\"", "\"\"") + "\""; }

        private static string[] Separar(string linea)
        {
            System.Collections.Generic.List<string> campos = new System.Collections.Generic.List<string>();
            StringBuilder campo = new StringBuilder(); bool comillas = false;
            for (int i = 0; i < linea.Length; i++)
            {
                char c = linea[i];
                if (c == '"' && comillas && i + 1 < linea.Length && linea[i + 1] == '"') { campo.Append('"'); i++; }
                else if (c == '"') comillas = !comillas;
                else if (c == ';' && !comillas) { campos.Add(campo.ToString()); campo.Clear(); }
                else campo.Append(c);
            }
            campos.Add(campo.ToString()); return campos.ToArray();
        }
    }
}
