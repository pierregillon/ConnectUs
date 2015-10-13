using System;

namespace ConnectUs.Core.Connections
{
    public class ConnectionListenerAlreadyStartedException : Exception
    {
        public ConnectionListenerAlreadyStartedException(string message):base(message)
        {
            
        }
    }
}