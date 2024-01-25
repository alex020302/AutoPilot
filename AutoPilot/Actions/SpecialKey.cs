using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.AdditionalCharacteristics;
using WindowsInput.Native;
using WindowsInput;

namespace AutoPilot.Actions
{
    public enum SpecialKeyType
    {
        Enter = VirtualKeyCode.RETURN,
        Spacebar,
        WindowsKey
    }

    public class SpecialKey : Action
    {
        public List<VirtualKeyCode> KeyCodes { get; set; }

        public override string ToString()
        {
            return $"Key Combination: {string.Join(", ", KeyCodes.Select(k => k.ToString()))}, Comment: {Comment}";
        }

        public async Task Execute()
        {
            InputSimulator simulator = new InputSimulator();

            // Überprüfe, ob es mindestens einen KeyCode gibt
            if (KeyCodes.Count > 0)
            {
                // Erstelle eine Liste von Tasks für das Drücken jeder Taste
                var keyPressTasks = KeyCodes.Select(keyCode => Task.Run(() => simulator.Keyboard.KeyDown(keyCode))).ToList();

                // Warte auf das Ende aller Tasks, um sicherzustellen, dass alle Tasten gedrückt sind
                await Task.WhenAll(keyPressTasks);

                // Warte hier, um sicherzustellen, dass alle Tasten gedrückt bleiben
                await Task.Delay(100);

                // Erstelle eine Liste von Tasks für das Loslassen jeder Taste
                var keyReleaseTasks = KeyCodes.Select(keyCode => Task.Run(() => simulator.Keyboard.KeyUp(keyCode))).ToList();

                // Warte auf das Ende aller Tasks, um sicherzustellen, dass alle Tasten losgelassen sind
                await Task.WhenAll(keyReleaseTasks);
            }
            else
            {
                throw new InvalidOperationException("No key codes specified for the combination.");
            }
        }

        public void AddCharToKeyCodes(char charachter)
        {
            KeyCodes.Add(ConvertCharToKeyCode(charachter));
        }

        private VirtualKeyCode ConvertCharToKeyCode(char character)
        {
            // Konvertiere den Buchstaben zu einem Großbuchstaben, um einheitliche Ergebnisse zu erhalten
            char upperCaseChar = char.ToUpper(character);

            // Überprüfe, ob der Buchstabe ein gültiges ASCII-Zeichen ist
            if (upperCaseChar >= 'A' && upperCaseChar <= 'Z')
            {
                // Wenn es ein Buchstabe ist, konvertiere ihn zu VirtualKeyCode
                return (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), "VK_" + upperCaseChar);
            }
            else
            {
                // Wenn es kein Buchstabe ist, gib VK_0 zurück (kann für nicht unterstützte Zeichen stehen)
                return VirtualKeyCode.VK_0;
            }
        }
    }

}
