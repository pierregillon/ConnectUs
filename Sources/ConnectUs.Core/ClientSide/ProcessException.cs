using System;

namespace ConnectUs.Core.ClientSide
{
    public class ProcessException : Exception
    {
        public ProcessException(string message) : base(message) {}
    }
}