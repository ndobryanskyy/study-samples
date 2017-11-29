using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;
using Prism.Events;

namespace IndexedClipboard
{
    public class ShortcutsManager : IDisposable
    {
        private readonly ClipboardManager _clipboardManager;

        // ReSharper disable once InconsistentNaming
        private const int WH_KEYBOARD_LL = 13;
        // ReSharper disable once InconsistentNaming
        private const int WM_KEYDOWN = 0x0100;

        // ReSharper disable once InconsistentNaming
        private const int KEY_PRESSED = 0x8000;

        private readonly LowLevelKeyboardProc _proc;
        private IntPtr _hookId = IntPtr.Zero;

        public ShortcutsManager(ClipboardManager clipboardManager)
        {
            _clipboardManager = clipboardManager;
            _proc = HookCallback;
        }

        public void Initialize()
        {
            _hookId = SetHook(_proc);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookId);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var currentProcess = Process.GetCurrentProcess())
            using (var currentModule = currentProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(currentModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                var pressedKey = (Keys)Marshal.ReadInt32(lParam);

                if ((IsKeyPressed(Keys.ControlKey)
                     || IsKeyPressed(Keys.RControlKey)
                     || IsKeyPressed(Keys.LControlKey))
                        && IsKeyPressed(Keys.C)
                        && pressedKey >= Keys.D0 
                        && pressedKey <= Keys.D9)
                {
                    _clipboardManager.StoreData(pressedKey - Keys.D0);
                }
                
                else if ((IsKeyPressed(Keys.ControlKey)
                           || IsKeyPressed(Keys.RControlKey)
                           || IsKeyPressed(Keys.LControlKey))
                          && pressedKey >= Keys.D0
                          && pressedKey <= Keys.D9)
                {
                    _clipboardManager.SetData(pressedKey - Keys.D0);
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private static bool IsKeyPressed(Keys key) 
            => (GetKeyState((int)key) & KEY_PRESSED) != 0;

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern short GetKeyState(int key);
    }
}