using System;
using System.IO;
using WindowsInput;

namespace AutoPilot.Actions
{
    public class DataInput : Action
    {
        public string ActionType { get; } = "DataInput";
        public int Column { get; set; }
        public string ExcelPath;
        public int Row;
        private ExcelHandler lExcelHandler = new ExcelHandler();

        public override string ToString()
        {
            return $"DataInput: Column: {Convert.ToString(Column)}, Comment: {Comment}";
        }

        public override void Execute()
        {
            try
            {
                string text = lExcelHandler.GetCellFromExcel(ExcelPath, Row, Column);

                InputSimulator simulator = new InputSimulator();
                simulator.Keyboard.TextEntry(text);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error in DataInput.Execute: Excel file not found - {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error in DataInput.Execute: Input/output error - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DataInput.Execute: {ex.GetType().Name} - {ex.Message}");
            }
        }
    }
}