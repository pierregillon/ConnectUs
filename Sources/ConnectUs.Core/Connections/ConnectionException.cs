﻿using System;

namespace ConnectUs.Core.Connections
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message) {}
        public ConnectionException(string message, Exception innerException) : base(message, innerException) {}
    }
}