using System;

namespace ConnectUs.Core.ServerSide.Clients
{
    public class RemoteClientException : Exception
    {
        public RemoteClientException(string message): base(message) { }
        public RemoteClientException(string message, Exception innerException): base(message, innerException) {}
    }
}