using ConnectUs.Business.Connections;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeConnection : IConnection
    {
        public void Send(Request request)
        {
            throw new System.NotImplementedException();
        }
        public Response Read()
        {
            throw new System.NotImplementedException();
        }
    }
}