using System;

namespace ConnectUs.Core.ServerSide.Requests
{
    internal class RequestException : Exception
    {
        public RequestException(string message, Exception exception) : base(message, exception)
        {
        }
        public RequestException(string message) : base(message)
        {
            
        }
    }
}