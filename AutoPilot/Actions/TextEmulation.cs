using WindowsInput;

namespace AutoPilot.Actions
{
    public class TextEmulation : Action
    {
        public string ActionType { get; } = "TextEmulation";
        public string Text { get; set; }

        public override string ToString()
        {
            return $"TextEmulation: Text: {Text}, Comment: {Comment}";
        }

        public override void Execute()
        {
            InputSimulator simulator = new InputSimulator();

            simulator.Keyboard.TextEntry(Text);
        }
    }
}
