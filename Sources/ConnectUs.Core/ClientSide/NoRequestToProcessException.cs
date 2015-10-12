using System;

namespace ConnectUs.Core.ClientSide
{
    public class NoRequestToProcessException : Exception
    {
        public NoRequestToProcessException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}