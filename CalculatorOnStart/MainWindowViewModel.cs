using System.Windows.Input;
using CalculatorOnStart.Helpers;
using Prism.Commands;
using Prism.Mvvm;

namespace CalculatorOnStart
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly SystemHelper _helper;

        public MainWindowViewModel(SystemHelper helper)
        {
            _helper = helper;
            ToggleStartup = new DelegateCommand<bool?>(OnToggled);
        }

        public ICommand ToggleStartup { get; }


        private void OnToggled(bool? value)
        {
            _helper.IsStartupItem = value ?? false;
        }

    }
}