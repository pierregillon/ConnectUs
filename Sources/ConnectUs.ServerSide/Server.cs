using System;
using System.Collections.Generic;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class Server : IServer
    {
        private readonly IClientListener _clientListener;
        private readonly List<Client> _clients = new List<Client>();

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        protected virtual void OnClientConnected(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = ClientConnected;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        protected virtual void OnClientDisconnected(ClientDisconnectedEventArgs e)
        {
            EventHandler<ClientDisconnectedEventArgs> handler = ClientDisconnected;
            if (handler != null) handler(this, e);
        }

        public Server(ServerConfiguration serverConfiguration) : this(new ClientListener(new TcpClientConnectionListener(), serverConfiguration)) {}
        private Server(IClientListener clientListener)
        {
            _clientListener = clientListener;
            _clientListener.ClientConnected += ClientListenerOnClientConnected;
            _clientListener.ClientDisconnected += ClientListenerOnClientDisconnected;
        }

        private void ClientListenerOnClientConnected(object sender, ClientConnectedEventArgs args)
        {
            _clients.Add(args.Client);
            OnClientConnected(args);
        }
        private void ClientListenerOnClientDisconnected(object sender, ClientDisconnectedEventArgs args)
        {
            _clients.Remove(args.Client);
            OnClientDisconnected(args);
        }

        public void Start()
        {
            _clientListener.Start();
        }
        public void Stop()
        {
            _clientListener.Stop();
        }

        public IEnumerable<Client> GetConnectedClients()
        {
            return _clients;
        }
    }
}