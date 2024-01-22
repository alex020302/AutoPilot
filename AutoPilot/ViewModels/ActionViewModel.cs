using AutoPilot.Actions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using static AutoPilot.Views.Editor;
using System.Reflection;
using System.Windows;

namespace AutoPilot
{
    public class ActionViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Action> actions;

        public ObservableCollection<Action> Actions
        {
            get { return actions; }
            set
            {
                if (actions != value)
                {
                    actions = value;
                    OnPropertyChanged(nameof(Actions));
                }
            }
        }

        public ActionViewModel()
        {
            // Für Testzwecke:
            Actions = new ObservableCollection<Action>
            {
                new MouseClick { Comment = "MouseClick 1", X_Coordinate = 100, Y_Coordinate = 200, NumberOfClicks = 1 },
                new Delay { Comment = "Delay 1", Milliseconds = 500 },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveToJsonFile(string filePath)
        {
            var json = JsonConvert.SerializeObject(Actions, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}

