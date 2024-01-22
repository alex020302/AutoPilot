using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPilot.Views
{
    /// <summary>
    /// Interaction logic for Data1.xaml
    /// </summary>
    public partial class Data1 : UserControl
    {

        private int numberOfColumns;

        public Data1()
        {
            InitializeComponent();
        }

        private void CheckIfEnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(t_input.Text, out numberOfColumns) && numberOfColumns > 0)
                {
                    var viewModel = (MainWindowViewModel)DataContext;
                    viewModel.numberOfColumns = Convert.ToInt32(t_input.Text);

                    if (viewModel.GotoViewData2Command.CanExecute(null))
                    {
                        viewModel.GotoViewData2Command.Execute(null);
                    }
                    else
                    {
                        new Exception("View Change failed!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Bitte geben SIe eine gültige Zahl ein");
                    t_input.Text = "";
                }
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
