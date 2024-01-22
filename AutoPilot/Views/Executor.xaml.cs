using AutoPilot.Actions;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoPilot.Executor;

namespace AutoPilot.Views
{
    /// <summary>
    /// Interaction logic for Executor.xaml
    /// </summary>
    public partial class Executor : UserControl
    {
        public ObservableCollection<FilePaths> FilePathList { get; set; }
        private JsonHandler lJsonHandler = new JsonHandler();

        public Executor()
        {
            InitializeComponent();
            FilePathList = new ObservableCollection<FilePaths>();
            filePathsDataGrid.ItemsSource = FilePathList;
        }

        private void OpenFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "JSON Files (*.json)|*.json|Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
                Title = "Select JSON Files"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string jsonFilePath in openFileDialog.FileNames)
                {
                    string excelFilePath = null;

                    MessageBoxResult result = MessageBox.Show("Do you want to attach an Excel file to this JSON file?",
                        "Excel Attachment", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        OpenFileDialog excelFileDialog = new OpenFileDialog
                        {
                            Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
                            Title = "Select Excel File"
                        };

                        if (excelFileDialog.ShowDialog() == true)
                        {
                            excelFilePath = excelFileDialog.FileName;
                        }
                    }

                    FilePathList.Add(new FilePaths { JsonFilePath = jsonFilePath, ExcelFilePath = excelFilePath });
                }
            }
        }
        //Das hier muss in eine separate Klasse Ausführer
        cExecutor lExecutor = new cExecutor();
        private async void Start(object sender, RoutedEventArgs e)
        {
            
            lExecutor.run(FilePathList);


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
