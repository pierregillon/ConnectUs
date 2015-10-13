using System;

namespace ConnectUs.Core.ClientSide
{
    public class ClientException : Exception
    {
        public ClientException(string message) : base(message)
        {
        }
    }
}