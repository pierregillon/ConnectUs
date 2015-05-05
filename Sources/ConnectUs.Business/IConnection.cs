namespace ConnectUs.Business
{
    public interface IConnection
    {
        void Send(Request request);
        Response Read();
    }
}