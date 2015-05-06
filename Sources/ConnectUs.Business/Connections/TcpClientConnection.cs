using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace ConnectUs.Business.Connections
{
    internal class TcpClientConnection : IConnection
    {
        private readonly TcpClient _client;

        public TcpClientConnection(TcpClient client)
        {
            _client = client;
        }

        public void Send(Request request)
        {
            var encoding = new UTF8Encoding();
            var json = JsonConvert.SerializeObject(request);
            var bytes = encoding.GetBytes(json);
            var networkStream = _client.GetStream();
            networkStream.Write(bytes, 0, bytes.Length);
        }
        public Response Read()
        {
            throw new NotImplementedException();
        }
    }
}