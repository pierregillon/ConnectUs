using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using ConnectUs.ServerSide.Application.ViewModels;

namespace ConnectUs.ServerSide.Application.Services
{
    public class ClientViewModelService : IClientViewModelService
    {
        private readonly IRemoteClientListener _remoteClientListener;
        private readonly ObservableCollection<ClientViewModel> _clients = new ObservableCollection<ClientViewModel>();
        private readonly Dictionary<RemoteClient, ClientViewModel> _relations = new Dictionary<RemoteClient, ClientViewModel>();
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public ClientViewModelService(IRemoteClientListener remoteClientListener)
        {
            _remoteClientListener = remoteClientListener;
            _remoteClientListener.ClientConnected += RemoteClientListenerOnRemoteClientConnected;
            _remoteClientListener.ClientDisconnected += RemoteClientListenerOnRemoteClientDisconnected;
        }

        public ObservableCollection<ClientViewModel> GetClients()
        {
            return _clients;
        }

        // ----- Event callbacks
        private void RemoteClientListenerOnRemoteClientConnected(object sender, RemoteClientConnectedEventArgs args)
        {
            var clientViewModel = new ClientViewModel(args.RemoteClient);
            clientViewModel.StartPing();
            _relations.Add(args.RemoteClient, clientViewModel);
            _synchronizationContext.Post(state => _clients.Add(clientViewModel), null);
        }
        private void RemoteClientListenerOnRemoteClientDisconnected(object sender, RemoteClientDisconnectedEventArgs args)
        {
            var clientViewModel = _relations[args.RemoteClient];
            _relations.Remove(args.RemoteClient);
            _synchronizationContext.Post(state => _clients.Remove(clientViewModel), null);
        }
    }
}