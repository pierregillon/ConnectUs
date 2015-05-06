using System;
using System.Collections.Generic;
using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class ClientListener : IClientListener
    {
        private readonly IConnectionListener _connectionListener;
        private readonly ServerConfiguration _serverConfiguration;
        private readonly Dictionary<IConnection, Client> _connectedClients = new Dictionary<IConnection, Client>();

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        protected virtual void OnClientConnected(ClientConnectedEventArgs e)
        {
            var handler = ClientConnected;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        protected virtual void OnClientDisconnected(ClientDisconnectedEventArgs e)
        {
            var handler = ClientDisconnected;
            if (handler != null) handler(this, e);
        }

        public ClientListener(IConnectionListener connectionListener, ServerConfiguration serverConfiguration)
        {
            _serverConfiguration = serverConfiguration;
            _connectionListener = connectionListener;
            _connectionListener.ConnectionEstablished += ConnectionListenerOnConnectionEstablished;
            _connectionListener.ConnectionLost += ConnectionListenerOnConnectionLost;
        }

        public void Start()
        {
            _connectionListener.Start(_serverConfiguration.Port);
        }
        public void Stop()
        {
            _connectionListener.Stop();
        }

        private void ConnectionListenerOnConnectionEstablished(object sender, ConnectionEstablishedEventArgs args)
        {
            var client = new Client(new RemoteRequestProcessor(args.Connection));
            _connectedClients.Add(args.Connection, client);
            OnClientConnected(new ClientConnectedEventArgs(client));
        }
        private void ConnectionListenerOnConnectionLost(object sender, ConnectionLostEventArgs args)
        {
            var client = _connectedClients[args.Connection];
            _connectedClients.Remove(args.Connection);
            OnClientDisconnected(new ClientDisconnectedEventArgs(client));
        }
    }
}