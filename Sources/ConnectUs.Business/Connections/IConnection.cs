namespace ConnectUs.Business.Connections
{
    public interface IConnection
    {
        void Send(Request request);
        Response Read();
    }
}