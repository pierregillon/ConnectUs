namespace ConnectUs.Business.Connections
{
    public interface IConnection
    {
        int TimeOut { get; set; }
        void Send(string data);
        string Read();
        void Close();
    }
}