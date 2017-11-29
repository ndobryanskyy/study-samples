using System.Diagnostics;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Requester.Client;

namespace Requester
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ApiClient _apiClient;

        private string _endpointUrl;
        private string _postData;
        private bool _isBusy;

        public MainWindowViewModel()
        {
            _apiClient = new ApiClient();

            PostRequestCommand = new DelegateCommand(PostRequest, CanPostRequest)
                .ObservesProperty(() => EndpointUrl)
                .ObservesProperty(() => PostData)
                .ObservesProperty(() => IsBusy);
        }

        public ICommand PostRequestCommand { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string EndpointUrl
        {
            get => _endpointUrl;
            set => SetProperty(ref _endpointUrl, value);
        }

        public string PostData
        {
            get => _postData;
            set => SetProperty(ref _postData, value);
        }

        private async void PostRequest()
        {
            IsBusy = true;

            try
            {
                var response = await _apiClient.PostString(EndpointUrl, PostData);
                Debug.WriteLine(response.StatusCode);
            }

            catch
            {
                Debug.WriteLine("Error!");
            }

            finally
            {
                IsBusy = false;
            }
        }

        private bool CanPostRequest() =>
            !IsBusy
            && !string.IsNullOrWhiteSpace(EndpointUrl)
            && !string.IsNullOrWhiteSpace(PostData);
    }
}