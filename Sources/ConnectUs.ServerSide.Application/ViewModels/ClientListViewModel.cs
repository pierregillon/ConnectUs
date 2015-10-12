using System.Collections.ObjectModel;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.ServerSide.Application.Services;
using ConnectUs.ServerSide.Application.ViewModels.Base;
using ConnectUs.ServerSide.Application.ViewModels.Messages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class ClientListViewModel : ViewModelBase, IBootable
    {
        private readonly IRemoteClientListener _remoteClientListener;
        private readonly IClientViewModelService _clientViewModelService;

        public ObservableCollection<ClientViewModel> Clients { get; private set; }
        public ClientViewModel SelectedClient
        {
            get { return GetNotifiableProperty<ClientViewModel>("SelectedClient"); }
            set { SetNotifiableProperty("SelectedClient", value); }
        }
        public RelayCommand OpenNewClientCommandViewModelCommand { get; private set; }

        // ----- Constructors
        public ClientListViewModel(IRemoteClientListener remoteClientListener, IClientViewModelService clientViewModelService)
        {
            _remoteClientListener = remoteClientListener;
            _clientViewModelService = clientViewModelService;
            Clients = new ObservableCollection<ClientViewModel>();
            OpenNewClientCommandViewModelCommand = new RelayCommand(OpenNewClientCommandViewModel, CanOpenNewClientCommandViewModel);
        }

        // ----- Internal logics
        private bool CanOpenNewClientCommandViewModel()
        {
            return SelectedClient != null;
        }
        private void OpenNewClientCommandViewModel()
        {
            Messenger.Default.Send(new OpenNewCommandViewModelMessage(SelectedClient));
        }

        // ----- Public methods
        public void Boot()
        {
            _remoteClientListener.Start(9000);
            Clients = _clientViewModelService.GetClients();
        }
        public void Dispose()
        {
            foreach (var clientViewModel in Clients) {
                clientViewModel.Close();
            }
        }
    }
}