﻿using ConnectUs.ClientSide;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeConnection : IConnection
    {
        private readonly Client _client;

        public FakeConnection(Client client)
        {
            _client = client;
        }

        public TResponse Execute<TRequest, TResponse>(TRequest request)
        {
            return _client.Execute<TRequest, TResponse>(request);
        }
    }
}