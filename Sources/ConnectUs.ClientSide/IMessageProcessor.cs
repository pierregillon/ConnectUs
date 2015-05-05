using ConnectUs.Business;

namespace ConnectUs.ClientSide
{
    public interface IMessageProcessor
    {
        void Process(IConnection connection, Message message);
    }
}