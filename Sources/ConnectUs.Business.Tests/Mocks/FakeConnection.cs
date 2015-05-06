using ConnectUs.Business.Connections;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeConnection : IConnection
    {
        public void Send<T>(T request)
        {
            throw new System.NotImplementedException();
        }
        public T Read<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}