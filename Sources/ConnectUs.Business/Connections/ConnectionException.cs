﻿using System;

namespace ConnectUs.Business.Connections
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message, Exception innerException) : base(message, innerException) {}
    }
}