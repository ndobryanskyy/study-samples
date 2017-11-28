using Microsoft.Win32;

namespace CalculatorOnStart.Helpers
{
    public class SystemHelper
    {
        private readonly string _appName;
        private readonly string _executablePath;

        private readonly RegistryKey _registryKey;


        public SystemHelper(string appName, string executablePath)
        {
            _appName = appName;
            _executablePath = executablePath;

            _registryKey =
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        }

        public bool IsStartupItem
        {
            get => _registryKey.GetValue(_appName) != null;
            set
            {
                if (value) _registryKey.SetValue(_appName, _executablePath);
                else _registryKey.DeleteValue(_appName, false);
            }
        }
    }
}