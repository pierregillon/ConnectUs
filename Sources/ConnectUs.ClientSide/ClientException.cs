using System;

namespace ConnectUs.ClientSide
{
    public class ClientException : Exception
    {
        public ClientException(string message) : base(message)
        {
        }
    }
}