using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using AutoPilot.Actions;
using WindowsInput.Native;

namespace AutoPilot.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            // TODO ?
        }

        private async void Test(object sender, RoutedEventArgs e)
        {
            //string url = "https://y2nb.com/en/download";
            //Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            
            Delay d = new Delay();
            d.Milliseconds = 2000;
            await d.Execute();

            SpecialKey keyCombinationAction = new SpecialKey()
            {
                KeyCodes = new List<VirtualKeyCode> {VirtualKeyCode.LWIN },
                Comment = "Simultaneous key press example"
            };
            keyCombinationAction.AddCharToKeyCodes('d');
            await keyCombinationAction.Execute();



        }
    }
}
