using System;
using System.Collections.Generic;
using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class RemoteClientListener : IRemoteClientListener
    {
        private readonly IConnectionListener _connectionListener;
        private readonly Dictionary<IConnection, RemoteClient> _connectedClients = new Dictionary<IConnection, RemoteClient>();

        public event EventHandler<RemoteClientConnectedEventArgs> ClientConnected;
        protected virtual void OnClientConnected(RemoteClientConnectedEventArgs e)
        {
            var handler = ClientConnected;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<RemoteClientDisconnectedEventArgs> ClientDisconnected;
        protected virtual void OnClientDisconnected(RemoteClientDisconnectedEventArgs e)
        {
            var handler = ClientDisconnected;
            if (handler != null) handler(this, e);
        }

        public RemoteClientListener(IConnectionListener connectionListener)
        {
            _connectionListener = connectionListener;
            _connectionListener.ConnectionEstablished += ConnectionListenerOnConnectionEstablished;
            _connectionListener.ConnectionLost += ConnectionListenerOnConnectionLost;
        }

        public void Start(int port)
        {
            _connectionListener.Start(port);
        }
        public void Stop()
        {
            _connectionListener.Stop();
        }

        private void ConnectionListenerOnConnectionEstablished(object sender, ConnectionEstablishedEventArgs args)
        {
            var client = new RemoteClient(new ServerRequestCommunicator(args.Connection, new JsonRequestParser()));
            _connectedClients.Add(args.Connection, client);
            OnClientConnected(new RemoteClientConnectedEventArgs(client));
        }
        private void ConnectionListenerOnConnectionLost(object sender, ConnectionLostEventArgs args)
        {
            var client = _connectedClients[args.Connection];
            _connectedClients.Remove(args.Connection);
            OnClientDisconnected(new RemoteClientDisconnectedEventArgs(client));
        }
    }
}