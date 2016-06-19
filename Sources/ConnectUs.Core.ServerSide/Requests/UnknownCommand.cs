using System;

namespace ConnectUs.Core.ServerSide.Requests
{
    public class UnknownCommand : Exception
    {
        public UnknownCommand(string message) : base(message)
        {
            
        }
    }
}