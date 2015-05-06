using System.Net.Sockets;
using ConnectUs.Business.Encodings;

namespace ConnectUs.Business.Connections
{
    public class TcpClientConnection : IConnection
    {
        private readonly TcpClient _client;
        private readonly IEncoder _encoder;

        public TcpClientConnection(TcpClient client, IEncoder encoder)
        {
            _client = client;
            _encoder = encoder;
        }

        public void Send<T>(T request)
        {
            var bytes = _encoder.Encode(request);
            var networkStream = _client.GetStream();
            networkStream.Write(bytes, 0, bytes.Length);
        }

        public T Read<T>()
        {
            var networkStream = _client.GetStream();
            var buffer = new byte[1024];
            networkStream.Read(buffer, 0, buffer.Length);
            return _encoder.Decode<T>(buffer);
        }
    }
}