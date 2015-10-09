using System;

namespace ConnectUs.ServerSide.Requests
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