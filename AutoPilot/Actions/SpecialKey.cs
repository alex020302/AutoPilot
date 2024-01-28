using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;

namespace AutoPilot.Actions
{
    public enum KeyType
    { 
        Windows = VirtualKeyCode.LWIN,
        Enter = VirtualKeyCode.RETURN,
        Spacebar = VirtualKeyCode.SPACE,
        Control = VirtualKeyCode.CONTROL,
        Shift = VirtualKeyCode.SHIFT,
        Tab = VirtualKeyCode.TAB,
        Delete = VirtualKeyCode.DELETE
    }

    public class SpecialKey : Action
    {
        public string ActionType { get; } = "SpecialKey";
        public List<VirtualKeyCode> KeyCodes { get; set; } = new List<VirtualKeyCode>();
        

        public override string ToString()
        {
            return $"Key Combination: {string.Join(", ", KeyCodes.Select(k => k.ToString()))}, Comment: {Comment}";
        }

        public async Task Execute()
        {
            InputSimulator simulator = new InputSimulator();

            if (KeyCodes.Count > 0)
            {
                // Erstellen einer Liste von Tasks für das Drücken jeder Taste
                var keyPressTasks = KeyCodes.Select(keyCode => Task.Run(() => simulator.Keyboard.KeyDown(keyCode))).ToList();

                // Warten auf das Ende aller Tasks, um sicherzustellen, dass alle Tasten gedrückt sind
                await Task.WhenAll(keyPressTasks);

                // Warten um sicherzustellen, dass alle Tasten gedrückt bleiben
                await Task.Delay(300);

                // Erstellen einer Liste von Tasks für das Loslassen jeder Taste
                var keyReleaseTasks = KeyCodes.Select(keyCode => Task.Run(() => simulator.Keyboard.KeyUp(keyCode))).ToList();

                // Warten auf das Ende aller Tasks, um sicherzustellen, dass alle Tasten losgelassen sind
                await Task.WhenAll(keyReleaseTasks);
            }
            else
            {
                throw new InvalidOperationException("No key codes specified for the combination.");
            }
        }
        public void AddKeyToKeyCodes(KeyType key)
        {
            KeyCodes.Add((VirtualKeyCode)key);
        }

        public void AddCharToKeyCodes(char charachter)
        {
            KeyCodes.Add(ConvertCharToKeyCode(charachter));
        }

        private VirtualKeyCode ConvertCharToKeyCode(char character)
        {
            // Konvertierung des Buchstaben zu einem Großbuchstaben
            char upperCaseChar = char.ToUpper(character);

            // Gültiges Zeichen?
            if (upperCaseChar >= 'A' && upperCaseChar <= 'Z')
            {
                // Konvertierung zu VirtualKeyCode
                return (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), "VK_" + upperCaseChar);
            }

            return VirtualKeyCode.VK_0;
            
        }
    }
}
