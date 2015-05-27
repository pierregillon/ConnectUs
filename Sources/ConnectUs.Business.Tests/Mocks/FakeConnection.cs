using System.IO;
using ConnectUs.Business.Connections;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeConnection : IConnection
    {
        public int TimeOut { get; set; }
        public void Send(byte[] data)
        {
            throw new System.NotImplementedException();
        }
        public void Send(Stream stream)
        {
            throw new System.NotImplementedException();
        }
        public byte[] Read()
        {
            throw new System.NotImplementedException();
        }
        public void Read(Stream stream)
        {
            throw new System.NotImplementedException();
        }
        public void Close()
        {
            throw new System.NotImplementedException();
        }
    }
}