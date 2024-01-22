using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AutoPilot.Views
{
    /// <summary>
    /// Interaction logic for Recorder.xaml
    /// </summary>
    public partial class Recorder : UserControl
    {
        public Recorder()
        {
            InitializeComponent();
        }

        public Aufzeichner lRecorder = new Aufzeichner();
        private ObservableCollection<Action> Actions = new ObservableCollection<Action>();


        private void Start(object sender, RoutedEventArgs e)
        {
            Actions.Clear();
            lRecorder.StartRecording();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            Actions = lRecorder.StopRecording();
            int length = Actions.Count();
            Actions.RemoveAt(length - 1);
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.updateEditor(Actions);
            EditorClick(null, null);
        }

        private JsonHandler l_oJsonHandler = new JsonHandler();
        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "JSON Files|*.json",
                    Title = "Save JSON File",
                    FileName = "MeinSzenario.json"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    l_oJsonHandler.WriteData(Actions, saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern der JSON-Datei: {ex.Message}");
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
