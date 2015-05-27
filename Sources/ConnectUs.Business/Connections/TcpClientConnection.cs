using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public void Send(string data)
        {
            try {
                var bytes = _encoder.Encode(data);
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
        public string Read()
        {
            try {
                var networkStream = _client.GetStream();
                var buffer = Read(networkStream);
                return _encoder.Decode(buffer);
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

        // ----- Internal logics
        private static byte[] Read(NetworkStream networkStream)
        {
            var buffers = new List<byte[]>();
            do {
                var buffer = Read(networkStream, 1024);
                buffers.Add(buffer);
            } while (networkStream.DataAvailable);
            return buffers.SelectMany(bytes => bytes).ToArray();
        }
        private static byte[] Read(Stream stream, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            var bytesReceived = stream.Read(buffer, 0, buffer.Length);
            if (bytesReceived == 0) {
                throw new SocketException((int) SocketError.ConnectionAborted);
            }
            return buffer.Take(bytesReceived).ToArray();
        }
    }
}