using System;

namespace ConnectUs.ServerSide
{
    public class RequestException : Exception
    {
        public RequestException(string message, Exception exception) : base(message, exception)
        {
        }
        public RequestException(string message) : base(message)
        {
            
        }
    }
}