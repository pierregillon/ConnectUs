﻿using System;

namespace ConnectUs.ServerSide
{
    public class ClientException : Exception
    {
        public ClientException(string message) : base(message)
        {
            
        }
    }
}