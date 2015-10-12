using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConnectUs.Core.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command
{
    public class ClientList
    {
        private readonly IRemoteClientListener _remoteClientListener;
        private readonly List<ClientViewModel> _clientViewModels = new List<ClientViewModel>();

        private bool _pinging;
        private bool _updateInformation;

        private readonly Timer _pingTimer;
        private readonly Timer _updateInfoTimer;

        public ClientList(IRemoteClientListener remoteClientListener)
        {
            _remoteClientListener = remoteClientListener;
            _remoteClientListener.ClientConnected += RemoteClientListenerOnRemoteClientConnected;
            _remoteClientListener.ClientDisconnected += RemoteClientListenerOnRemoteClientDisconnected;
            _pingTimer = new Timer(Ping, null, 0, 10000);
            _updateInfoTimer = new Timer(UpdateInformation, null, 0, 30000);
        }
        ~ClientList()
        {
            _remoteClientListener.ClientConnected -= RemoteClientListenerOnRemoteClientConnected;
            _remoteClientListener.ClientDisconnected -= RemoteClientListenerOnRemoteClientDisconnected;
            _pingTimer.Dispose();
            _updateInfoTimer.Dispose();
        }

        private void Ping(object state)
        {
            if (_pinging == false) {
                try {
                    _pinging = true;
                    foreach (var clientViewModel in _clientViewModels.ToArray()) {
                        clientViewModel.Ping();
                    }
                }
                finally {
                    _pinging = false;
                }
            }
        }
        private void UpdateInformation(object state)
        {
            if (_updateInformation == false) {
                try {
                    _updateInformation = true;
                    foreach (var clientViewModel in _clientViewModels.ToArray()) {
                        clientViewModel.UpdateInformation();
                    }
                }
                finally {
                    _updateInformation = false;
                }
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