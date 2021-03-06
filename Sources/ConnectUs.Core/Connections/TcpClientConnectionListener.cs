﻿using System;
using System.Net;
using System.Net.Sockets;

namespace ConnectUs.Core.Connections
{
    public class TcpClientConnectionListener : IConnectionListener
    {
        private TcpListener _listener;

        public event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;
        protected virtual void OnConnectionEstablished(ConnectionEstablishedEventArgs e)
        {
            EventHandler<ConnectionEstablishedEventArgs> handler = ConnectionEstablished;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ConnectionLostEventArgs> ConnectionLost;
        protected virtual void OnConnectionLost(ConnectionLostEventArgs e)
        {
            EventHandler<ConnectionLostEventArgs> handler = ConnectionLost;
            if (handler != null) handler(this, e);
        }

        // ----- Public methods
        public void Start(int port)
        {
            if (_listener != null) {
                throw new ConnectionListenerAlreadyStartedException("The tcp listener is already listening.");
            }
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _listener.BeginAcceptTcpClient(Callback, _listener);
        }
        public void Stop()
        {
            if (_listener != null) {
                _listener.Stop();
                _listener = null;
            }
        }

        // ----- Internal logics
        private void Callback(IAsyncResult result)
        {
            try {
                var listener = (TcpListener)result.AsyncState;
                var client = listener.EndAcceptTcpClient(result);
                var connection = new TcpClientConnection(client);
                connection.Disconnected += ConnectionOnDisconnected;
                OnConnectionEstablished(new ConnectionEstablishedEventArgs(connection));
                listener.BeginAcceptTcpClient(Callback, listener);
            }
            catch (ObjectDisposedException) {}
        }

        private void ConnectionOnDisconnected(object sender, EventArgs eventArgs)
        {
            var connection = sender as IConnection;
            if (connection != null) {
                OnConnectionLost(new ConnectionLostEventArgs(connection));
            }
        }
    }
}