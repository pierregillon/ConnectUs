using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using ConnectUs.ServerSide.Application.ViewModels;

namespace ConnectUs.ServerSide.Application.Services
{
    public class ClientViewModelService : IClientViewModelService
    {
        private readonly IClientListener _clientListener;
        private readonly ObservableCollection<ClientViewModel> _clients = new ObservableCollection<ClientViewModel>();
        private readonly Dictionary<Client, ClientViewModel> _relations = new Dictionary<Client, ClientViewModel>();
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public ClientViewModelService(IClientListener clientListener)
        {
            _clientListener = clientListener;
            _clientListener.ClientConnected += ClientListenerOnClientConnected;
            _clientListener.ClientDisconnected += ClientListenerOnClientDisconnected;
        }

        public ObservableCollection<ClientViewModel> GetClients()
        {
            return _clients;
        }

        // ----- Event callbacks
        private void ClientListenerOnClientConnected(object sender, ClientConnectedEventArgs args)
        {
            var clientViewModel = new ClientViewModel(args.Client);
            clientViewModel.StartPing();
            _relations.Add(args.Client, clientViewModel);
            _synchronizationContext.Post(state => _clients.Add(clientViewModel), null);
        }
        private void ClientListenerOnClientDisconnected(object sender, ClientDisconnectedEventArgs args)
        {
            var clientViewModel = _relations[args.Client];
            _relations.Remove(args.Client);
            _synchronizationContext.Post(state => _clients.Remove(clientViewModel), null);
        }
    }
}