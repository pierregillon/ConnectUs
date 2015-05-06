namespace ConnectUs.Business.Connections
{
    public interface IConnection
    {
        void Send<T>(T request);
        T Read<T>();
    }
}