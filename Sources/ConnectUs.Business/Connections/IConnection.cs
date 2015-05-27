using System.IO;

namespace ConnectUs.Business.Connections
{
    public interface IConnection
    {
        int TimeOut { get; set; }
        void Send(string data);
        void Send(Stream stream);
        string Read();
        void Read(Stream stream);
        void Close();
    }
}