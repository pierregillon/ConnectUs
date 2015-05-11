using System.Collections.ObjectModel;
using ConnectUs.ServerSide.Application.Services;
using ConnectUs.ServerSide.Application.ViewModels.Base;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class ClientListViewModel : ViewModelBase, IBootable
    {
        private readonly IServer _server;
        private readonly IClientViewModelService _clientViewModelService;

        public ObservableCollection<ClientViewModel> Clients { get; private set; }
        public ClientViewModel SelectedClient

        {
            get { return GetNotifiableProperty<ClientViewModel>("SelectedClient"); }
            set { SetNotifiableProperty("SelectedClient", value); }
        }

        // ----- Constructors
        public ClientListViewModel(IServer server, IClientViewModelService clientViewModelService)
        {
            _server = server;
            _clientViewModelService = clientViewModelService;
            Clients = new ObservableCollection<ClientViewModel>();
        }

        // ----- Public methods
        public void Boot()
        {
            _server.Start();
            Clients = _clientViewModelService.GetClients();
        }
    }
}