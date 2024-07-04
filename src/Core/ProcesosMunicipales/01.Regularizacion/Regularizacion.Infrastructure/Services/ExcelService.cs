using iText.IO.Image;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Layout;
using iText.Layout.Properties;
using iText.Html2pdf;
using ClosedXML.Excel;
using System.Data;
using Regularizacion.Application.Dtos;
using static Regularizacion.Application.Dtos.PlantillaRecord;
using DocumentFormat.OpenXml.Bibliography;
using ClosedXML;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;

namespace Regularizacion.Infrastructure.Services
{
    public class ExcelService
    {
        public static string hoja1 = "Ficha";

        public static byte[] generatedFicha(List<PlantillaExcel> cells)
        {
            var encabezado = CreateEncabezado();
            var rows = AddRows(encabezado, cells);
            var group = ReturnDataGroup(rows);

            var workBook = new XLWorkbook();
            var sh = workBook.Worksheets.Add(hoja1);
            var pageSetup = sh.PageSetup;
            pageSetup.PaperSize = XLPaperSize.A4Paper;
            pageSetup.Margins.Top = 0;
            pageSetup.Margins.Bottom = 0;
            pageSetup.Margins.Left = 0;
            pageSetup.Margins.Right = 0;
            pageSetup.PageOrientation = XLPageOrientation.Landscape;
            pageSetup.Scale = 95;
            var encabezadoExcel = crearEncabezadoExcel(sh);
            IngresarData(rows, group, encabezadoExcel);
            //workBook.SaveAs("prueba.xlsx");
            var result = obtenerBytes(workBook);
            
            return result;
            //workBook.Save();
            // Lee el archivo PDF


            // Cierra el documento PDF
        }
        private static DataTable CreateEncabezado()
        {
            var datatable = new DataTable();
            datatable.Columns.Add("#", typeof(int));
            datatable.Columns.Add("Tipo Tramite", typeof(string));
            datatable.Columns.Add("Estado", typeof(string));
            datatable.Columns.Add("Fecha", typeof(DateTime));
            datatable.Columns.Add("Nombres", typeof(string));
            datatable.Columns.Add("Valor", typeof(decimal));
            datatable.Columns.Add("Pagado", typeof(decimal));
            datatable.Columns.Add("Pendiente", typeof(decimal));
            datatable.Columns.Add("Codigo Catastral", typeof(string));
            return datatable;
        }
        private static DataTable AddRows(DataTable encabezado, List<PlantillaExcel> _cells)
        {
            //encabezado.Rows.Add(_cells);
            
            _cells.ForEach(x =>
            {
                encabezado.Rows.Add(x.numRegularizacion, x.NombreTramite, x.EstadoRegularizacion, x.FechaRegistro, x.nombres,
                    x.ValorRegularizacion, x.Anticipo, x.ValorPendiente, x.CodigoCatastral);
            });
            
            return encabezado;
        }

        private static IEnumerable<IGrouping<object, DataRow>> ReturnDataGroup(DataTable encabezado)
        {
            var result = encabezado.AsEnumerable().GroupBy(row => new
            {
                Month = ((DateTime)row["Fecha"]).Month, 
                Year = ((DateTime)row["Fecha"]).Year
            });
            return result;
        }
        private static IXLWorksheet crearEncabezadoExcel(IXLWorksheet ws)
        {
            ws.Cell("A1").Value = "#";
            ws.Cell("B1").Value = "Tipo Tramite";
            ws.Cell("C1").Value = "Estado";
            ws.Cell("D1").Value = "Fecha";
            ws.Cell("E1").Value = "Nombres";
            ws.Cell("F1").Value = "Valor";
            ws.Cell("G1").Value = "Pagado";
            ws.Cell("H1").Value = "Pendiente";
            ws.Cell("I1").Value = "Codigo Catastral";
            for (int col = 1; col <= 9; col++)
            {
                ws.Cell(1, col).Style.Fill.BackgroundColor = XLColor.LightYellow;
                ws.Cell(1, col).Style.Font.FontColor = XLColor.Black; // Hacer las letras de color negro
                ws.Cell(1, col).Style.Font.Bold = true; // Hacer las letras de color negro
            }
            return ws;
        }
        private static void IngresarData(DataTable dataTable, IEnumerable<IGrouping<object, DataRow>> groups, IXLWorksheet ws)
        {
            var rowCount = -2;

            // Agregar cálculos mensuales
            var currentRow = rowCount + 3;
            var listF = new List<int>();
            var listG = new List<int>();
            var listH = new List<int>();
            foreach (var group in groups)
            {
                var startRow = currentRow;
                //ws.Cell(currentRow, 1).Value = $"{group.Key}/{group.Key}";
                foreach (var row in group)
                {
                    currentRow++;
                    ws.Cell(currentRow, 1).Value = (int)row["#"];
                    ws.Cell(currentRow, 2).Value = (string)row["Tipo Tramite"];
                    ws.Cell(currentRow, 3).Value = (string)row["Estado"];
                    ws.Cell(currentRow, 4).Value = obtenerFecha((DateTime)row["Fecha"]);
                    ws.Cell(currentRow, 5).Value = (string)row["Nombres"];
                    ws.Cell(currentRow, 6).Value = (decimal)row["Valor"];
                    ws.Cell(currentRow, 7).Value = (decimal)row["Pagado"];
                    ws.Cell(currentRow, 8).Value = (decimal)row["Pendiente"];
                    ws.Cell(currentRow, 9).Value = (string)row["Codigo Catastral"];

                    ws.Cell(currentRow, 6).Style.Fill.BackgroundColor = XLColor.LightCoral;
                    ws.Cell(currentRow, 7).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    ws.Cell(currentRow, 8).Style.Fill.BackgroundColor = XLColor.LightPastelPurple;
                    // Formatear columnas como moneda
                    ws.Cell(currentRow, 6).Style.NumberFormat.Format = "$ #,##0.00";
                    ws.Cell(currentRow, 7).Style.NumberFormat.Format = "$ #,##0.00";
                    ws.Cell(currentRow, 8).Style.NumberFormat.Format = "$ #,##0.00";
                }
                // Agregar total mensual
                var monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName((int)group.Key.GetType().GetProperty("Month").GetValue(group.Key, null));
                var year = (int)group.Key.GetType().GetProperty("Year").GetValue(group.Key, null);

                ws.Cell(currentRow + 1, 5).Value = $"Total del mes de {monthName}-{year}";
                ws.Cell(currentRow + 1, 6).FormulaA1 = $"=SUM(F{startRow + 1}:F{currentRow})";
                ws.Cell(currentRow + 1, 7).FormulaA1 = $"=SUM(G{startRow + 1}:G{currentRow})";
                ws.Cell(currentRow + 1, 8).FormulaA1 = $"=SUM(H{startRow + 1}:H{currentRow})";
                listF.Add(currentRow + 1);
                listG.Add(currentRow + 1);
                listH.Add(currentRow + 1);

                for (int col = 1; col <= 9; col++)
                {
                    ws.Cell(currentRow + 1, col).Style.Fill.BackgroundColor = XLColor.AirForceBlue;
                    ws.Cell(currentRow + 1, col).Style.Font.Bold = true; // Hacer las letras de color negro
                }
                // Formatear total del mes como moneda
                ws.Cell(currentRow + 1, 6).Style.NumberFormat.Format = "$ #,##0.00";
                ws.Cell(currentRow + 1, 7).Style.NumberFormat.Format = "$ #,##0.00";
                ws.Cell(currentRow + 1, 8).Style.NumberFormat.Format = "$ #,##0.00";

                currentRow += 1;

            }
            //for (int i = 0; i < listF[-1]; i++)
            //{
            //    ws.Row(6).Style.Fill.BackgroundColor = XLColor.GreenPigment;
            //}
            
            
            ws.Cell(currentRow + 1, 5).Value = "Total General";
            ws.Cell(currentRow + 1, 6).FormulaA1 = $"=SUM({string.Join(",", listF.Select(x => $"F{x}"))})";
            ws.Cell(currentRow + 1, 7).FormulaA1 = $"=SUM({string.Join(",", listG.Select(x => $"G{x}"))})";
            ws.Cell(currentRow + 1, 8).FormulaA1 = $"=SUM({string.Join(",", listH.Select(x => $"H{x}"))})";
            for (int col = 1; col <= 9; col++)
            {
                ws.Cell(currentRow + 1, col).Style.Fill.BackgroundColor = XLColor.CoralRed;
                ws.Cell(currentRow + 1, col).Style.Font.Bold = true; // Hacer las letras de color negro
            }

            ws.Cell(currentRow + 1, 6).Style.NumberFormat.Format = "$ #,##0.00";
            ws.Cell(currentRow + 1, 7).Style.NumberFormat.Format = "$ #,##0.00";
            ws.Cell(currentRow + 1, 8).Style.NumberFormat.Format = "$ #,##0.00";

            for (int i = 1; i <= 9; i++)
            {
                ws.Column(i).AdjustToContents();
            }
            var range = ws.RangeUsed();
            var table = range.CreateTable();
            table.Theme = XLTableTheme.TableStyleLight20;
        }
        private static byte[] obtenerBytes(IXLWorkbook workbook)
        {
            byte[] excelFile;
            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                excelFile = memoryStream.ToArray();
            }
            return excelFile;
        }
        private static string obtenerFecha(DateTime fecha)
        {
            //DateTime fechaActual = DateTime.Now;
            CultureInfo culturaEspañol = new CultureInfo("es-ES");
            string formatoPersonalizado = "dddd dd 'de' MMMM 'del' yyyy";

            string fechaActualFormateada = fecha.ToString(formatoPersonalizado, culturaEspañol);

            // Capitaliza la primera letra del día de la semana
            fechaActualFormateada = char.ToUpper(fechaActualFormateada[0]) + fechaActualFormateada.Substring(1);
            return fechaActualFormateada;
        }
    }
}
