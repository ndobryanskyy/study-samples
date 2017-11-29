using System;
using System.Windows;
using CalculatorOnStart.Helpers;
using Path = System.IO.Path;

namespace CalculatorOnStart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var currentDomain = AppDomain.CurrentDomain;
            var executablePath = Path.Combine(currentDomain.BaseDirectory, currentDomain.FriendlyName);

            var systemHelper = new SystemHelper(Title, executablePath);
            DataContext = new MainWindowViewModel(systemHelper);

            StartupSwitch.IsChecked = systemHelper.IsStartupItem;
        }
    }
}
