﻿using System.IO;

namespace ConnectUs.Business.Connections
{
    public interface IConnection
    {
        int TimeOut { get; set; }
        void Send(byte[] data);
        void Send(Stream stream);
        byte[] Read();
        void Read(Stream stream);
        void Close();
    }
}