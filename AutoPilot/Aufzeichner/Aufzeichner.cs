using AutoPilot.Actions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoPilot
{
    public class Aufzeichner
    {
        public ObservableCollection<Action> RecordedActions { get; } = new ObservableCollection<Action>();

        private bool isRecording = false;
        private LowLevelMouseProc mouseProc;
        private LowLevelKeyboardProc keyboardProc;
        private IntPtr mouseHookID = IntPtr.Zero;
        private IntPtr keyboardHookID = IntPtr.Zero;
        private Stopwatch stopwatch;
        private StringBuilder recordedText = new StringBuilder();

        public void StartRecording()
        {
            if (!isRecording)
            {
                isRecording = true;
                stopwatch = new Stopwatch();
                mouseProc = HookMouseCallback;
                keyboardProc = HookKeyboardCallback;
                mouseHookID = SetHook(mouseProc);
                keyboardHookID = SetHook(keyboardProc);
            }
        }

        public ObservableCollection<Action> StopRecording()
        {
            if (isRecording)
            {
                isRecording = false;
                UnhookWindowsHookEx(mouseHookID);
                UnhookWindowsHookEx(keyboardHookID);
            }
            return RecordedActions;
        }

        private IntPtr SetHook(Delegate proc)
        {
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
            {
                return SetWindowsHookEx(proc == mouseProc ? WH_MOUSE_LL : WH_KEYBOARD_LL, proc, GetModuleHandle(module.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookMouseCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (MouseMessages)wParam == MouseMessages.WM_LBUTTONDOWN)
            {
                RecordTextEmulation();
                RecordMouseClick();
            }

            return CallNextHookEx(mouseHookID, nCode, wParam, lParam);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookKeyboardCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (KeyboardMessages)wParam == KeyboardMessages.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                char keyChar = GetCharFromKey(vkCode, lParam);
                recordedText.Append(keyChar);
            }

            return CallNextHookEx(keyboardHookID, nCode, wParam, lParam);
        }

        private char GetCharFromKey(int vkCode, IntPtr lParam)
        {
            StringBuilder result = new StringBuilder(5);
            int toUnicodeResult = ToUnicodeEx((uint)vkCode, MapVirtualKey((uint)vkCode, 0), new byte[256], result, result.Capacity, 0, IntPtr.Zero);

            if (toUnicodeResult > 0)
            {
                return result[0];
            }

            return '?'; // If conversion fails, return a placeholder character
        }

        private void RecordMouseClick()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                int milliseconds = (int)stopwatch.ElapsedMilliseconds;
                milliseconds = (int)Math.Round((double)milliseconds / 10) * 10; // Runde auf hundertstel Millisekunden
                if (milliseconds > 0)
                {
                    Delay delay = new Delay
                    {
                        Milliseconds = milliseconds,
                        Comment = $"Delay: {milliseconds} ms"
                    };
                    RecordedActions.Add(delay);
                }
            }

            POINT cursorPos;
            if (GetCursorPos(out cursorPos))
            {
                MouseClick mouseClick = new MouseClick
                {
                    X_Coordinate = cursorPos.X,
                    Y_Coordinate = cursorPos.Y,
                    NumberOfClicks = 1,
                    Comment = $"Mouse Click at ({cursorPos.X}, {cursorPos.Y})"
                };

                RecordedActions.Add(mouseClick);
            }

            stopwatch.Restart();
        }

        private void RecordTextEmulation()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                int milliseconds = (int)stopwatch.ElapsedMilliseconds;
                milliseconds = (int)Math.Round((double)milliseconds / 10) * 10; // Runde auf hundertstel Millisekunden
                if (milliseconds > 0)
                {
                    Delay delay = new Delay
                    {
                        Milliseconds = milliseconds,
                        Comment = $"Delay: {milliseconds} ms"
                    };
                    RecordedActions.Add(delay);
                }
            }

            if (recordedText.Length > 0)
            {
                TextEmulation textEmulation = new TextEmulation
                {
                    Text = recordedText.ToString(),
                    Comment = "Text Emulation"
                };

                RecordedActions.Add(textEmulation);
                //stopwatch.Restart();
                recordedText.Clear();
            }
            
        }

        #region Win32 API

        private const int WH_MOUSE_LL = 14;
        private const int WH_KEYBOARD_LL = 13;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
        }

        private enum KeyboardMessages
        {
            WM_KEYDOWN = 0x0100,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, Delegate lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ushort MapVirtualKey(uint wCode, MapVirtualKeyMapTypes wMapType);

        private enum MapVirtualKeyMapTypes : uint
        {
            MAPVK_VK_TO_CHAR = 2,
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

        #endregion
    }
}
