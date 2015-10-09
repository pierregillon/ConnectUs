using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConnectUs.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command
{
    public class ClientList
    {
        private readonly IRemoteClientListener _remoteClientListener;
        private readonly List<ClientViewModel> _clientViewModels = new List<ClientViewModel>();
        private readonly Timer _timer;

        public ClientList(IRemoteClientListener remoteClientListener)
        {
            _remoteClientListener = remoteClientListener;
            _remoteClientListener.ClientConnected += RemoteClientListenerOnRemoteClientConnected;
            _remoteClientListener.ClientDisconnected += RemoteClientListenerOnRemoteClientDisconnected;
            _timer = new Timer(Callback, null, 0, 10000);
        }
        ~ClientList()
        {
            _remoteClientListener.ClientConnected -= RemoteClientListenerOnRemoteClientConnected;
            _remoteClientListener.ClientDisconnected -= RemoteClientListenerOnRemoteClientDisconnected;
            _timer.Dispose();
        }
        private void Callback(object state)
        {
            foreach (var clientViewModel in _clientViewModels.ToArray()) {
                clientViewModel.Ping();
            }
        }

        public IEnumerable<ClientViewModel> GetClients()
        {
            return _clientViewModels.ToArray();
        }

        private void RemoteClientListenerOnRemoteClientConnected(object sender, RemoteClientConnectedEventArgs args)
        {
            _clientViewModels.Add(new ClientViewModel(args.RemoteClient));
        }
        private void RemoteClientListenerOnRemoteClientDisconnected(object sender, RemoteClientDisconnectedEventArgs args)
        {
            var viewModel = _clientViewModels.FirstOrDefault(x => x.Match(args.RemoteClient));
            if (viewModel != null) {
                _clientViewModels.Remove(viewModel);
            }
        }
    }
}