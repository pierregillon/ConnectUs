using System.IO;

namespace ConnectUs.Core.Connections
{
    public interface IConnection
    {
        int TimeOut { get; }
        void Send(byte[] data);
        void Send(Stream stream);
        byte[] Read();
        void Read(Stream stream);
        void Close();
    }
}