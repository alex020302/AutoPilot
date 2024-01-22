using System.Threading.Tasks;

namespace AutoPilot.Actions
{
    public class Delay : Action
    {
        public string ActionType { get; } = "Delay";
        public int Milliseconds { get; set; }

        public override string ToString()
        {
            return $"Delay:  Milliseconds: {Milliseconds} ms, Comment: {Comment}";
        }

        public async Task Execute()
        {
            await Task.Delay(Milliseconds);
        }
    }
}
