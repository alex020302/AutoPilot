using System.Windows.Controls;
using System.Windows.Data;
using AutoPilot;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelHanlderTests
    {
        private ExcelHandler lExcelHandler;
        private string TestPath;
        private string WrittenExcelFilePath;

        [SetUp]
        public void Setup()
        {
            lExcelHandler = new ExcelHandler();
            TestPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData/TestFile.xlsx");
            WrittenExcelFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData/WrittenTestFile.xlsx");
        }

        [Test]
        public void TestGetNumberOfUsedRows()
        {
            
            int actualUsedRows = lExcelHandler.GetNumberOfUsedRows(TestPath);
            
            int expectedUsedRows = 10;

            if (expectedUsedRows == actualUsedRows)
            {
                Console.WriteLine(
                    $"Erfolg: Anzahl der verwendeten Zeilen stimmt überein. Path: {TestPath}, Aktuelle Anzahl an Reihen: {actualUsedRows}");
                Assert.Pass();
            }
            else
            {
                Assert.Fail(
                    $"Fehler: Anzahl der verwendeten Zeilen stimmt nicht überein. Path: {TestPath}, Aktuelle Anzahl an Reihen: {actualUsedRows}");
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetCellFromExcel()
        {
            string actuelCellConten = lExcelHandler.GetCellFromExcel(TestPath, 2, 2);

            string expectedCellConten = "B";

            if (actuelCellConten == expectedCellConten)
            {
                Console.WriteLine($"Erfolg: Inhalt einer Zelle konnte extrahiert werden. Path: {TestPath} Inhalt der Zelle: {actuelCellConten}, Zuerwartender Inhalt: B");
                Assert.Pass();
            }
            else
            {
                Console.WriteLine($"Fehler: Inhalt einer Zelle konnte nicht extrahiert werden. Path: {TestPath}, Inhalt der Zelle: {actuelCellConten}, Zuerwartender Inhalt: B");
                Assert.Fail();
            }
        }

        [Test]
        public void TestWriteData()
        {
            DataGrid lDataGrid = CreateTestDataGrid();

            lExcelHandler.WriteData(lDataGrid, WrittenExcelFilePath);
            
            if (File.Exists(WrittenExcelFilePath))
            {
                if (lExcelHandler.GetCellFromExcel(WrittenExcelFilePath, 1,2) == "Variable2")
                {
                    Console.WriteLine($"Erfolg: Es wurde ExcelFile erstellt. Path: {WrittenExcelFilePath}, Und der Inhalt der Zellen passt, Beweis: {lExcelHandler.GetCellFromExcel(WrittenExcelFilePath,1,2)}");
                    Assert.Pass();
                }
            }
            else
            {
                Console.WriteLine($"Fehler: Es wurde keine ExcelFile erstellt. Path: {WrittenExcelFilePath}");
                Assert.Fail();
            }
        }

        private DataGrid CreateTestDataGrid()
        {
            DataGrid dataGrid = new DataGrid();

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Variable1", Binding = new Binding("Variable1") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Variable2", Binding = new Binding("Variable2") });

            return dataGrid;
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(WrittenExcelFilePath))
            {
                File.Delete(WrittenExcelFilePath);
            }
        }
    }
}