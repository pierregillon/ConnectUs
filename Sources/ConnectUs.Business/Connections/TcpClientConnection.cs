using System;
using System.IO;
using System.Net.Sockets;
using ConnectUs.Business.Encodings;

namespace ConnectUs.Business.Connections
{
    public class TcpClientConnection : IConnection
    {
        private readonly TcpClient _client;
        private readonly IEncoder _encoder;

        public int TimeOut
        {
            get { return _client.ReceiveTimeout; }
            set
            {
                _client.ReceiveTimeout = value;
                _client.SendTimeout = value;
            }
        }

        public event EventHandler Disconnected;
        protected virtual void OnDisconnected()
        {
            EventHandler handler = Disconnected;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        // ----- Constructors
        public TcpClientConnection(TcpClient client, IEncoder encoder)
        {
            _client = client;
            _encoder = encoder;
        }

        // ----- Public methods
        public void Send<T>(T request)
        {
            try {
                if (_client.Client.Poll(0, SelectMode.SelectError)) {
                    throw new SocketException((int)SocketError.ConnectionAborted);
                }
                var bytes = _encoder.Encode(request);
                var networkStream = _client.GetStream();
                networkStream.Write(bytes, 0, bytes.Length);
            }
            catch (IOException ex) {
                if (ex.InnerException != null) {
                    var socketException = ex.InnerException as SocketException;
                    if (socketException != null) {
                        throw new ConnectionException("An error occured while sending data to the connection.", ex);
                    }
                }
                throw;
            }
            catch (SocketException ex) {
                throw new ConnectionException("An error occured while sending data to the connection.", ex);
            }
        }
        public T Read<T>()
        {
            try {
                var networkStream = _client.GetStream();
                var buffer = new byte[1024];
                var bytesReceived = networkStream.Read(buffer, 0, buffer.Length);
                if (bytesReceived == 0) {
                    Close();
                    throw new SocketException((int)SocketError.ConnectionAborted);
                }
                return _encoder.Decode<T>(buffer);
            }
            catch (IOException ex) {
                if (ex.InnerException != null) {
                    var socketException = ex.InnerException as SocketException;
                    if (socketException != null) {
                        if (socketException.SocketErrorCode == SocketError.TimedOut) {
                            throw new NoDataToReadFromConnectionException("No data to read from the connection.", ex);
                        }
                        throw new ConnectionException("An error occured while reading data from connection.", ex);
                    }
                }
                throw;
            }
            catch (SocketException ex) {
                throw new ConnectionException("An error occured while reading data from connection.", ex);
            }
        }
        public void Close()
        {
            _client.Close();
            OnDisconnected();
        }
    }
}