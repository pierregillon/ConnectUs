using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace ConnectUs.Core.Connections
{
    public class TcpClientConnection : IConnection
    {
        private const int StreamBufferSize = 2048;
        private const int ReadBufferSize = 1024;
        private const int DefaultTimeout = 1000;

        private readonly TcpClient _client;
        private Queue<byte[]> _alreadyReadBuffers = new Queue<byte[]>();
        private byte[] END = {0, 0, 0, 0, 0 };

        public int TimeOut
        {
            get { return _client.ReceiveTimeout; }
        }

        public event EventHandler Disconnected;
        protected virtual void OnDisconnected()
        {
            EventHandler handler = Disconnected;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        // ----- Constructors
        public TcpClientConnection(TcpClient client, int timeout = DefaultTimeout)
        {
            _client = client;
            _client.ReceiveTimeout = timeout;
            _client.SendTimeout = timeout;
        }

        // ----- Public methods
        public void Send(byte[] data)
        {
            var networkStream = _client.GetStream();
            Send(networkStream, data);
            Send(networkStream, END);
        }
        public void Send(Stream stream)
        {
            stream.ForeachRead(StreamBufferSize, Send);
        }
        public byte[] Read()
        {
            if (_alreadyReadBuffers.Any()) {
                return _alreadyReadBuffers.Dequeue();
            }

            var buffers = new List<byte[]>();
            WhileDataAvailable(ReadBufferSize, buffers.Add);

            var correctBuffers = buffers
                .SelectMany(bytes => bytes)
                .ToArray()
                .Split(END)
                .ToArray();

            foreach (var correctBuffer in correctBuffers) {
                _alreadyReadBuffers.Enqueue(correctBuffer);
            }
            return _alreadyReadBuffers.Dequeue();
        }
        public void Read(Stream stream)
        {
            WhileDataAvailable(ReadBufferSize, buffer => stream.Write(buffer, 0, buffer.Length));
        }
        public void Close()
        {
            _client.Close();
            OnDisconnected();
        }

        // ----- Internal logics
        private static byte[] Read(Stream stream, int bufferSize)
        {
            try {
                var buffer = new byte[bufferSize];
                var bytesReceived = stream.Read(buffer, 0, buffer.Length);
                if (bytesReceived == 0) {
                    throw new SocketException((int) SocketError.ConnectionAborted);
                }
                return buffer.Take(bytesReceived).ToArray();
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
        private static void Send(Stream stream, byte[] buffer)
        {
            try {
                stream.Write(buffer, 0, buffer.Length);
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

        // ----- Utils
        private void WhileDataAvailable(int bufferSize, Action<byte[]> callback)
        {
            var networkStream = _client.GetStream();
            do {
                var buffer = Read(networkStream, bufferSize);
                callback(buffer);
            } while (networkStream.DataAvailable);
        }
    }

    public static class ByteArrayExtensions
    {
        public static IEnumerable<byte[]> Split(this IReadOnlyList<byte> array, IReadOnlyList<byte> sequenceToFind)
        {
            var lastIndex = 0;
            for (int i = 0; i < array.Count; i++) {
                var j = 0;
                for (; j < sequenceToFind.Count; j++) {
                    if (array[j+i] != sequenceToFind[j]) {
                        break;
                    }
                }
                if (j == sequenceToFind.Count) {
                    yield return array.Skip(lastIndex).Take(i - lastIndex).ToArray();
                    lastIndex = i += j;
                }
            }

            if (lastIndex == 0) {
                yield return array.ToArray();
            }
        }
    }
}