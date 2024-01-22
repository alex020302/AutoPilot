using ClosedXML.Excel;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using AutoPilot.Actions;

namespace AutoPilot
{
    public class JsonHandler : FileHandler
    {
        
        public void WriteData(ObservableCollection<Action> pAction, string pFilePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var json = JsonConvert.SerializeObject(pAction, Formatting.Indented);
                File.WriteAllText(pFilePath, json);
            }
        }

        public ObservableCollection<Action> ReadData(String pFilePath)
        {
            ObservableCollection<Action> p_Actions = new ObservableCollection<Action>();

            List<Action> actions = CreateActionsFromJsonFile(pFilePath);
            p_Actions.Clear();

            foreach (var action in actions)
            {
                p_Actions.Add(action);
            }
                
            return p_Actions;
        }

        public List<Action> CreateActionsFromJsonFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var jsonObjects = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

            return ToCollection(jsonObjects);
        }

        public List<Action> ToCollection(List<Dictionary<string, object>> jsonObjects)
        {
            List<Action> actions = new List<Action>();
            foreach (var jsonObject in jsonObjects)
            {
                Action action = CreateActionFromJsonObject(jsonObject);
                if (action != null)
                {
                    actions.Add(action);
                }
            }

            return actions;
        }

        public Action CreateActionFromJsonObject(Dictionary<string, object> jsonObject)
        {
            if (!jsonObject.ContainsKey("ActionType"))
                return null; // ActionType fehlt im JSON-Objekt

            string actionType = jsonObject["ActionType"].ToString();

            switch (actionType)
            {
                case "MouseClick":
                    return CreateActionFromJsonObject<MouseClick>(jsonObject);
                case "Delay":
                    return CreateActionFromJsonObject<Delay>(jsonObject);
                case "TextEmulation":
                    return CreateActionFromJsonObject<TextEmulation>(jsonObject);
                case "DataInput":
                    return CreateActionFromJsonObject<DataInput>(jsonObject);
                // Weitere Aktionstypen hinzufügen, wenn nötig
                default:
                    return null; // Unbekannter Aktionstyp
            }
        }

        public T CreateActionFromJsonObject<T>(Dictionary<string, object> jsonObject) where T : Action, new()
        {
            T action = new T();

            // Allgemeine Eigenschaften setzen
            action.Comment = jsonObject.ContainsKey("Comment") ? jsonObject["Comment"].ToString() : "";

            // Spezifische Eigenschaften je nach Aktionstyp setzen
            switch (action)
            {
                case MouseClick mouseClick when jsonObject.ContainsKey("NumberOfClicks") && jsonObject.ContainsKey("X_Coordinate") && jsonObject.ContainsKey("Y_Coordinate"):
                    mouseClick.NumberOfClicks = Convert.ToInt32(jsonObject["NumberOfClicks"]);
                    mouseClick.X_Coordinate = Convert.ToInt32(jsonObject["X_Coordinate"]);
                    mouseClick.Y_Coordinate = Convert.ToInt32(jsonObject["Y_Coordinate"]);
                    break;

                case Delay delay when jsonObject.ContainsKey("Milliseconds"):
                    delay.Milliseconds = Convert.ToInt32(jsonObject["Milliseconds"]);
                    break;

                case TextEmulation textEmulation when jsonObject.ContainsKey("Text"):
                    textEmulation.Text = Convert.ToString(jsonObject["Text"]);
                    break;

                case DataInput dataInput when jsonObject.ContainsKey("Column"):
                    dataInput.Column = Convert.ToInt32(jsonObject["Column"]);
                    break;
            }

            return action;
        }

    }
}
