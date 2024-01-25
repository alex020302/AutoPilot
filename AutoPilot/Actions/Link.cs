using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows;

namespace AutoPilot.Actions
{
    public class Link : Action
    {
        public string ActionType { get; } = "Link";
        public string Url { get; set; }

        public override string ToString()
        {
            return $"Link: URL: {Url}, Comment: {Comment}";
        }

        public override void Execute()
        {
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {Url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Link.Execute: {ex.Message}");
            }
        }
    }
}
