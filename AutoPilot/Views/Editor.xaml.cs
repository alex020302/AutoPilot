using AutoPilot.Actions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EditorTest.EditViews;
using System.Collections.ObjectModel;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;
using Newtonsoft.Json;


namespace AutoPilot.Views
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public ActionViewModel ViewModel{ get; set; }

        public Editor()
        {
            InitializeComponent();
            ViewModel = new ActionViewModel();
            DataContext = ViewModel;
        }

        public void updateActions(ObservableCollection<Action> actions)
        {
            ViewModel.Actions = actions;
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedAction = (sender as ListBox)?.SelectedItem as Action;

            if (selectedAction != null)
            {
                // Typ der Aktion überprüfen und entsprechend handeln
                if (selectedAction is MouseClick)
                {
                    // Verarbeitung
                    MouseClick mouseClick = (MouseClick)selectedAction;
                    MouseEdit mouseClickFenster = new MouseEdit(mouseClick);
                    mouseClickFenster.ShowDialog();

                    // Speichern derIndexposition der ausgewählten Aktion
                    int selectedIndex = ViewModel.Actions.IndexOf(selectedAction);

                    // Entfernen der alten Aktion
                    ViewModel.Actions.RemoveAt(selectedIndex);
                    // Hinzufügen der aktualisierten Aktion an derselben Position
                    ViewModel.Actions.Insert(selectedIndex, mouseClickFenster.zuBearbeiten);
                }
                else if (selectedAction is Delay)
                {
                    // Verarbeitung
                    Delay delay = (Delay)selectedAction;
                    DelayEdit delayFenster = new DelayEdit(delay);
                    delayFenster.ShowDialog();

                    // Speichern derIndexposition der ausgewählten Aktion
                    int selectedIndex = ViewModel.Actions.IndexOf(selectedAction);

                    // Entfernen der alten Aktion
                    ViewModel.Actions.RemoveAt(selectedIndex);
                    // Hinzufügen der aktualisierten Aktion an derselben Position
                    ViewModel.Actions.Insert(selectedIndex, delayFenster.zuBearbeiten);
                }
                // Typ der Aktion überprüfen und entsprechend handeln
                else if (selectedAction is TextEmulation)
                {
                    // Verarbeitung
                    TextEmulation emulation = (TextEmulation)selectedAction;
                    TextEmulationEdit editFenster = new TextEmulationEdit(emulation);
                    editFenster.ShowDialog();

                    // Speichern derIndexposition der ausgewählten Aktion
                    int selectedIndex = ViewModel.Actions.IndexOf(selectedAction);

                    // Entfernen der alten Aktion
                    ViewModel.Actions.RemoveAt(selectedIndex);
                    // Hinzufügen der aktualisierten Aktion an derselben Position
                    ViewModel.Actions.Insert(selectedIndex, editFenster.zuBearbeiten);
                }
                // Typ der Aktion überprüfen und entsprechend handeln
                else if (selectedAction is DataInput)
                {
                    // Verarbeitung
                    DataInput data = (DataInput)selectedAction;
                    DataInputEdit editFenster = new DataInputEdit(data);
                    editFenster.ShowDialog();

                    // Speichern derIndexposition der ausgewählten Aktion
                    int selectedIndex = ViewModel.Actions.IndexOf(selectedAction);

                    // Entfernen der alten Aktion
                    ViewModel.Actions.RemoveAt(selectedIndex);
                    // Hinzufügen der aktualisierten Aktion an derselben Position
                    ViewModel.Actions.Insert(selectedIndex, editFenster.zuBearbeiten);
                }
                else
                {
                    // Standardaktion, wenn der Typ nicht erkannt wird
                    MessageBox.Show("Nicht unterstützter Aktionstyp");
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string choosenAktion = c_Action.Text;

            var selectedAction = ActionsListBox.SelectedItem as Action;
            int selectedIndex = ViewModel.Actions.IndexOf(selectedAction);

            switch (choosenAktion)
            {
                case "MouseClick":
                    MouseClick newMouseClick = new MouseClick();

                    // Bearbeitungsfenster öffnen um Eigenschaften festzulegen
                    MouseEdit mouseClickFenster = new MouseEdit(newMouseClick);
                    mouseClickFenster.ShowDialog();

                    // Neue Aktion hinzufügen
                    
                    ViewModel.Actions.Insert(selectedIndex + 1, newMouseClick);
                    break;

                case "Delay":
                    Delay newDelay = new Delay();

                    // Bearbeitungsfenster öffnen um Eigenschaften festzulegen
                    DelayEdit delayFenster = new DelayEdit(newDelay);
                    delayFenster.ShowDialog();

                    // Neue Aktion hinzufügen
                    ViewModel.Actions.Insert(selectedIndex + 1, newDelay);
                    break;

                case "TextEmulation":
                    TextEmulation newEmulation= new TextEmulation();

                    // Bearbeitungsfenster öffnen um Eigenschaften festzulegen
                    TextEmulationEdit editFenster = new TextEmulationEdit(newEmulation);
                    editFenster.ShowDialog();

                    // Neue Aktion hinzufügen
                    ViewModel.Actions.Insert(selectedIndex + 1, newEmulation);
                    break;

                case "DataInput":
                    DataInput newDataInput= new DataInput();

                    // Bearbeitungsfenster öffnen um Eigenschaften festzulegen
                    DataInputEdit datainputeditFenster = new DataInputEdit(newDataInput);
                    datainputeditFenster.ShowDialog();

                    // Neue Aktion hinzufügen
                    ViewModel.Actions.Insert(selectedIndex + 1, newDataInput);
                    break;

                default:
                    MessageBox.Show("Error");
                    break;
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            
            var selectedAction = ActionsListBox.SelectedItem as Action;

            if (selectedAction != null)
            {
                int selectedIndex = ActionsListBox.SelectedIndex;
                ViewModel.Actions.Remove(selectedAction);

                if (ViewModel.Actions.Count > 0)
                {
                    int newIndex = Math.Min(selectedIndex, ViewModel.Actions.Count - 1);
                    ActionsListBox.SelectedIndex = newIndex;
                }
            }
        }

        /// <summary>
        // Json-Handling
        /// </summary>
        private JsonHandler l_oJsonHandler = new JsonHandler();

        private void save(object sender, RoutedEventArgs e)
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
                    l_oJsonHandler.WriteData(ViewModel.Actions, saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern der JSON-Datei: {ex.Message}");
            }
            
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json";
            openFileDialog.Title = "Select a JSON File";

            if (openFileDialog.ShowDialog() == true)
            {
                ViewModel.Actions = l_oJsonHandler.ReadData(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Fehler beim Laden der Datei.");
            }

            
        }

        /// <summary>
        // Navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeClicked(object sender, RoutedEventArgs e)
        {

            var mainWindowViewModel = (MainWindowViewModel)Window.GetWindow(this).DataContext;
            if (mainWindowViewModel.GotoViewHomeCommand.CanExecute(null))
            {
                mainWindowViewModel.GotoViewHomeCommand.Execute(null);
            }
            else
            {
                throw new Exception("View Change failed!!!");
            }
        }

        private void DataClick(object sender, RoutedEventArgs e)
        {
            var mainWindowViewModel = (MainWindowViewModel)Window.GetWindow(this).DataContext;
            if (mainWindowViewModel.GotoViewData1Command.CanExecute(null))
            {
                mainWindowViewModel.GotoViewData1Command.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
            
        }

        private void RecorderClick(object sender, RoutedEventArgs e)
        {
            var mainWindowViewModel = (MainWindowViewModel)Window.GetWindow(this).DataContext;
            if (mainWindowViewModel.GotoViewRecorderCommand.CanExecute(null))
            {
                mainWindowViewModel.GotoViewRecorderCommand.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
        }

        private void EditorClick(object sender, RoutedEventArgs e)
        {
            var mainWindowViewModel = (MainWindowViewModel)Window.GetWindow(this).DataContext;
            if (mainWindowViewModel.GotoViewEditorCommand.CanExecute(null))
            {
                mainWindowViewModel.GotoViewEditorCommand.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
        }

        private void ExecutorClick(object sender, RoutedEventArgs e)
        {
            var mainWindowViewModel = (MainWindowViewModel)Window.GetWindow(this).DataContext;
            if (mainWindowViewModel.GotoViewExecutorCommand.CanExecute(null))
            {
                mainWindowViewModel.GotoViewExecutorCommand.Execute(null);
            }
            else
            {
                new Exception("View Change failed!!!");
            }
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {

        }
        

        private void Help(object sender, RoutedEventArgs e)
        {
            Anzeige anzeige = new Anzeige();
            anzeige.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Execute(object sender, RoutedEventArgs e)
        {
            int i = 0;

            foreach (var action in ViewModel.Actions)
            {
                ActionsListBox.SelectedIndex = i;
                if (action is Delay delayAction)
                {
                    await delayAction.Execute();
                }
                else
                {
                    action.Execute();
                }
                i++;
            }
        }

    }
}
