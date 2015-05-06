using System;
using System.Net;
using System.Net.Sockets;

namespace ConnectUs.Business.Connections
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

        public void Start(int port)
        {
            if (_listener != null) {
                throw new ConnectionListenerAlreadyStartedException();
            }
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _listener.BeginAcceptTcpClient(Callback, _listener);
        }

        private void Callback(IAsyncResult result)
        {
            var client = _listener.EndAcceptTcpClient(result);
            OnConnectionEstablished(new ConnectionEstablishedEventArgs(new TcpClientConnection(client)));
            _listener.BeginAcceptTcpClient(Callback, _listener);
        }

        public void Stop()
        {
            if (_listener == null) {
                throw new ConnectionListenerAlreadyStoppedException();
            }
            _listener.Stop();
        }
    }
}