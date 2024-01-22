using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;
using AutoPilot.Actions;

namespace EditorTest.EditViews
{
    /// <summary>
    /// Interaction logic for Anzeige.xaml
    /// </summary>
    public partial class Anzeige : Window
    {
        private DispatcherTimer timer;

        // Importiere die GetCursorPos-Funktion aus user32.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out POINT lpPoint);

        // Definiere die POINT-Struktur, um die Mauskoordinaten zu speichern
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public Anzeige()
        {
            InitializeComponent();

            // Setze die Position des Fensters (optional)
            Left = 100;
            Top = 100;

            // Initialisiere den Timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5); // Alle 0.5 Sekunden
            timer.Tick += Timer_Tick;

            // Starte den Timer
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Aktualisiere die Labels mit den globalen Mauskoordinaten
            POINT cursorPos;
            if (GetCursorPos(out cursorPos))
            {
                tX.Content = $"X: {cursorPos.X}";
                tY.Content = $"Y: {cursorPos.Y}";
            }
        }
    }
}
