using AutoPilot.Actions;
using DocumentFormat.OpenXml.Vml;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WindowsInput.Native;

namespace AutoPilot.EditWindows
{
    /// <summary>
    /// Interaction logic for SpecialKeyEdit.xaml
    /// </summary>
    public partial class SpecialKeyEdit : Window
    {
        public SpecialKey zuBearbeiten;

        public SpecialKeyEdit(SpecialKey editThisKeys)
        {
            InitializeComponent();
            zuBearbeiten = editThisKeys; // zuBearbeiten auf die übergebene Instanz setzen
            CommentTextBox.Text = editThisKeys.Comment;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            List<ComboBox> boxes = new List<ComboBox>();
            boxes.Add(t1);
            boxes.Add(t2);
            boxes.Add(t3);
            boxes.Add(t4);
            zuBearbeiten.KeyCodes.Clear();
            zuBearbeiten.Comment = CommentTextBox.Text;

            foreach (var box in boxes)
            {
                switch (box.Text)
                {
                    case "":
                        break;
                    case "Windows":
                        zuBearbeiten.AddKeyToKeyCodes(KeyType.Windows);
                        break;
                    case "Enter":
                        zuBearbeiten.AddKeyToKeyCodes(KeyType.Enter);
                        break;
                    case "Space":
                        zuBearbeiten.AddKeyToKeyCodes(KeyType.Spacebar);
                        break;
                    case "Control":
                        zuBearbeiten.AddKeyToKeyCodes(KeyType.Control);
                        break;
                    case "Shift":
                        zuBearbeiten.AddKeyToKeyCodes(KeyType.Shift);
                        break;
                    case "Tab":
                        zuBearbeiten.AddKeyToKeyCodes(KeyType.Tab);
                        break;
                    case "Delete":
                        zuBearbeiten.AddKeyToKeyCodes(KeyType.Delete);
                        break;
                default:
                        zuBearbeiten.AddCharToKeyCodes(ConvertStringToChar(box.Text));
                        break;
                }
            }
            this.Close();
        }
        static char ConvertStringToChar(string inputString)
        {
            if (inputString != null)
            {
                if (inputString.Length == 1)
                {
                    return inputString[0];
                }
                else
                {
                    throw new ArgumentException("Der Eingabestring sollte genau einen Buchstaben enthalten.");
                }
            }

            return  ' ';
        }
    }
}
