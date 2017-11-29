using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace ScreenCapture
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            SaveSnapshot = new DelegateCommand(OnSaveSnapshot);
        }

        private async void OnSaveSnapshot()
        {
            var service = new ScreenCaptureService();

            var window = Application.Current.MainWindow;
            window.WindowState = WindowState.Minimized;
            await Task.Delay(TimeSpan.FromSeconds(1));

            var bitmap = service.TakeScreenSnapshot();
            try
            {
                bitmap.Save("D:\\Screenshot.jpg");
                window.WindowState = WindowState.Normal;
            }
            catch
            {
                
            }
        }

        public ICommand SaveSnapshot { get; }
    }
}