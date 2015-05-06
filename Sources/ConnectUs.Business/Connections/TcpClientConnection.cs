using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace ConnectUs.Business.Connections
{
    public class TcpClientConnection : IConnection
    {
        private readonly TcpClient _client;

        public TcpClientConnection(TcpClient client)
        {
            _client = client;
        }

        public void Send<T>(T request)
        {
            var encoding = new UTF8Encoding();
            var json = JsonConvert.SerializeObject(request);
            var bytes = encoding.GetBytes(json);
            var networkStream = _client.GetStream();
            networkStream.Write(bytes, 0, bytes.Length);
        }

        public T Read<T>()
        {
            var encoding = new UTF8Encoding();
            var networkStream = _client.GetStream();
            var buffer = new byte[1024];
            networkStream.Read(buffer, 0, buffer.Length);
            var json = encoding.GetString(buffer);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}