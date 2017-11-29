using System.Windows;

namespace SingleInstance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var guard = new MutexGuard();
            if (!guard.IsSingleInstance())
            {
                MessageBox.Show("Only one for you Tiger...", "Not so fast");
                Shutdown(1);
            }

            base.OnStartup(e);
        }
    }
}
