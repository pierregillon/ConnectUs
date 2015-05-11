using System.Collections.ObjectModel;
using System.Linq;
using ConnectUs.ServerSide.Application.ViewModels.Base;
using ConnectUs.ServerSide.Application.ViewModels.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewModelBuilder _viewModelBuilder;

        public ObservableCollection<ClientCommandViewModel> ClientCommandViewModels { get; private set; }
        public ClientCommandViewModel SelectedClientCommandViewModel
        {
            get { return GetNotifiableProperty<ClientCommandViewModel>("SelectedClientCommandViewModel"); }
            set { SetNotifiableProperty("SelectedClientCommandViewModel", value); }
        }

        public MainViewModel(IViewModelBuilder viewModelBuilder)
        {
            _viewModelBuilder = viewModelBuilder;
            ClientCommandViewModels = new ObservableCollection<ClientCommandViewModel>();

            Messenger.Default.Register<OpenNewCommandViewModelMessage>(this, OpenNewCommandViewModelMessageReceived);
            Messenger.Default.Register<CloseCommandViewModelMessage>(this, CloseCommandViewModelMessageReceived);
        }

        private void OpenNewCommandViewModelMessageReceived(OpenNewCommandViewModelMessage message)
        {
            var vm = ClientCommandViewModels.FirstOrDefault(x => x.Match(message.ClientViewModel));
            if (vm != null) {
                SelectedClientCommandViewModel = vm;
            }
            else {
                var viewModel = _viewModelBuilder.BuildNewClientCommandViewModel();
                viewModel.Boot(message.ClientViewModel);
                ClientCommandViewModels.Add(viewModel);
                if (SelectedClientCommandViewModel == null) {
                    SelectedClientCommandViewModel = viewModel;
                }
            }
        }
        private void CloseCommandViewModelMessageReceived(CloseCommandViewModelMessage message)
        {
            ClientCommandViewModels.Remove(message.ViewModel);
            message.ViewModel.Dispose();
        }
    }
}