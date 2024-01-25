using Microsoft.Win32;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace AutoPilot.Views
{
    public partial class Data2 : UserControl
    {
        public Data2()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        public ExcelHandler excelWriter = new ExcelHandler();

        private void Create()
        {
            var viewModel = (MainWindowViewModel)DataContext;


            DataTable dataTable = new DataTable();

            for (int i = 1; i <= viewModel.numberOfColumns; i++)
            {
                DataColumn column = new DataColumn($"Variable {i}", typeof(string));
                dataTable.Columns.Add(column);
            }

            dataGrid.ItemsSource = dataTable.DefaultView;

            if (viewModel.numberOfColumns < 7)
            {
                foreach (DataGridColumn column in dataGrid.Columns)
                {
                    column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            }

            // Füge leere Zeilen hinzu
            for (int i = 0; i < viewModel.numberOfColumns; i++)
            {
                DataRow newRow = dataTable.NewRow();
                dataTable.Rows.Add(newRow);
            }
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Create();
        }

        
        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Save Excel File",
                    FileName = "MeinDatensatz.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    excelWriter.WriteData(dataGrid, saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern der Excel-Datei: {ex.Message}");
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedRow = dataGrid.SelectedItem;
                var dataView = dataGrid.ItemsSource as DataView;

                if (dataView != null)
                {
                    int selectedIndex = dataGrid.SelectedIndex;

                    if (selectedIndex >= 0 && selectedIndex < dataView.Table.Rows.Count)
                    {
                        // Entfernen der Zeile aus der Datenquelle
                        dataView.Table.Rows.RemoveAt(selectedIndex);

                        // Aktualisieren der Anzeige
                        dataView.Table.AcceptChanges();
                    }
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var dataView = dataGrid.ItemsSource as DataView;

            if (dataView != null)
            {
                // Füge eine neue Zeile zur Datenquelle hinzu
                var newRow = dataView.Table.NewRow();
                dataView.Table.Rows.Add(newRow);

                // Aktualisiere die Anzeige
                dataView.Table.AcceptChanges();

                // Wähle die neu hinzugefügte Zeile aus
                dataGrid.SelectedItem = newRow;
                dataGrid.ScrollIntoView(newRow);
            }
        }






        private void HomeClicked(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            if (viewModel.GotoViewHomeCommand.CanExecute(null))
            {
                viewModel.GotoViewHomeCommand.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
        }

        private void DataClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            if (viewModel.GotoViewData1Command.CanExecute(null))
            {
                viewModel.GotoViewData1Command.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
        }

        private void RecorderClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            if (viewModel.GotoViewRecorderCommand.CanExecute(null))
            {
                viewModel.GotoViewRecorderCommand.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
        }

        private void EditorClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            if (viewModel.GotoViewEditorCommand.CanExecute(null))
            {
                viewModel.GotoViewEditorCommand.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
        }

        private void ExecutorClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            if (viewModel.GotoViewExecutorCommand.CanExecute(null))
            {
                viewModel.GotoViewExecutorCommand.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {

        }
    }


















}
