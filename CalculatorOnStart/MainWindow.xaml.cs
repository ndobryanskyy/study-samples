using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CalculatorOnStart.Helpers;
using Path = System.IO.Path;

namespace CalculatorOnStart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SystemHelper _systemHelper;

        public MainWindow()
        {
            InitializeComponent();

            var currentDomain = AppDomain.CurrentDomain;
            var executablePath = Path.Combine(currentDomain.BaseDirectory, currentDomain.FriendlyName);

            _systemHelper = new SystemHelper(Title, executablePath);
        }

        private void SetStartupOnLogin(object sender, RoutedEventArgs e)
        {
            _systemHelper.IsStartupItem = !_systemHelper.IsStartupItem;
        }
    }
}
