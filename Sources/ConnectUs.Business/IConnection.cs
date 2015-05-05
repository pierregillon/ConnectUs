namespace ConnectUs.Business
{
    public interface IConnection
    {
        void Send<T>(T message) where T : Message;
        T Read<T>() where T : Message;
    }
}