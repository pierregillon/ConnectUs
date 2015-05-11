using System;

namespace ConnectUs.ClientSide
{
    public class ProcessException : Exception
    {
        public ProcessException(string message) : base(message) {}
    }
}