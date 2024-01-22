using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPilot.Actions
{
    public class MouseClick : Action
    {
        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public string ActionType { get; } = "MouseClick";
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
        public int NumberOfClicks { get; set; } = 1;
        

        public override string ToString()
        {
            return $"MouseClick:  X: {X_Coordinate}, Y: {Y_Coordinate}, Clicks: {NumberOfClicks}, Comment: {Comment}";
        }
        public override void Execute()
        {
            SetCursorPos(X_Coordinate, Y_Coordinate);

            for (int i = 0; i < NumberOfClicks; i++)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_Coordinate, Y_Coordinate, 0, 0);
            }
            SetCursorPos(X_Coordinate, Y_Coordinate);
            //MessageBox.Show(ToString());
        }
    }
}
