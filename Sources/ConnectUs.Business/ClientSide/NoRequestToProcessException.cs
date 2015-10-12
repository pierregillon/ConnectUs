using System;

namespace ConnectUs.ClientSide
{
    public class NoRequestToProcessException : Exception
    {
        public NoRequestToProcessException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}