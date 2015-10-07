using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConnectUs.ServerSide.Command
{
    public class ClientList
    {
        private readonly IClientListener _clientListener;
        private readonly List<ClientViewModel> _clientViewModels = new List<ClientViewModel>();
        private readonly Timer _timer;

        public ClientList(IClientListener clientListener)
        {
            _clientListener = clientListener;
            _clientListener.ClientConnected += ClientListenerOnClientConnected;
            _clientListener.ClientDisconnected += ClientListenerOnClientDisconnected;
            _timer = new Timer(Callback, null, 0, 10000);
        }
        ~ClientList()
        {
            _clientListener.ClientConnected -= ClientListenerOnClientConnected;
            _clientListener.ClientDisconnected -= ClientListenerOnClientDisconnected;
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

        private void ClientListenerOnClientConnected(object sender, ClientConnectedEventArgs args)
        {
            _clientViewModels.Add(new ClientViewModel(args.Client));
        }
        private void ClientListenerOnClientDisconnected(object sender, ClientDisconnectedEventArgs args)
        {
            var viewModel = _clientViewModels.FirstOrDefault(x => x.Match(args.Client));
            if (viewModel != null) {
                _clientViewModels.Remove(viewModel);
            }
        }
    }
}