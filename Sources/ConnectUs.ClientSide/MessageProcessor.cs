using ConnectUs.Business;

namespace ConnectUs.ClientSide
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IClientInformationService _clientInformationService;

        public MessageProcessor(IClientInformationService clientInformationService)
        {
            _clientInformationService = clientInformationService;
        }

        public void Process(IConnection connection, Message message)
        {
            
        }
    }
}