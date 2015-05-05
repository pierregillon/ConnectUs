using System;
using ConnectUs.ClientSide;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeConnection : IConnection
    {
        private readonly RequestProcessor _requestProcessor;

        public FakeConnection(RequestProcessor requestProcessor)
        {
            _requestProcessor = requestProcessor;
        }

        public TResponse Execute<TRequest, TResponse>(TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}