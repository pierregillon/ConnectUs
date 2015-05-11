namespace ConnectUs.Business.Connections
{
    public interface IConnection
    {
        int TimeOut { get; set; }

        void Send<T>(T request);
        T Read<T>();
        void Dispose();
    }
}