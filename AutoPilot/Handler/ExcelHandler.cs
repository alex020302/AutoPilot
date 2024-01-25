using ClosedXML.Excel;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace AutoPilot
{
    public class ExcelHandler : FileHandler
    {
        public void WriteData(DataGrid pDataGrid, string pFilePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("DataGridSheet");

                // Kopfzeilen schreiben
                for (int i = 0; i < pDataGrid.Columns.Count; i++)
                {
                    var columnHeader = pDataGrid.Columns[i].Header?.ToString() ?? string.Empty;
                    worksheet.Cell(1, i + 1).Value = columnHeader;
                }

                // Daten schreiben
                int rowIndex = 2; // Startzeile für Daten
                foreach (var item in pDataGrid.Items)
                {
                    for (int j = 0; j < pDataGrid.Columns.Count; j++)
                    {
                        var cellContent = pDataGrid.Columns[j].GetCellContent(item);
                        if (cellContent is TextBlock textBlock)
                        {
                            var cellValue = textBlock.Text;
                            worksheet.Cell(rowIndex, j + 1).Value = cellValue ?? string.Empty;
                        }
                    }
                    rowIndex++;
                }

                workbook.SaveAs(pFilePath);
            }

        }

        public object ReadData(string filename)
        {
            // Implementierung für das Lesen von Excel-Daten
            return null;
        }

        public string GetCellFromExcel(string excelPath, int row, int column)
        {

            using (var package = new ExcelPackage(new FileInfo(excelPath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Annahme: Das Datenblatt ist das erste Datenblatt.

                // Beachte, dass Excel-Zellen im EPPlus-Index bei 1 beginnen.
                var cellValue = worksheet.Cells[row, column].Text;

                return cellValue;
            }
        }

        public int GetNumberOfUsedRows(string excelFilePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet != null)
                {
                    var dimension = worksheet.Dimension;

                    if (dimension != null)
                    {
                        int usedRows = dimension.Rows;
                        return usedRows;
                    }
                }

                return 0;
            }
        }

    }
}
