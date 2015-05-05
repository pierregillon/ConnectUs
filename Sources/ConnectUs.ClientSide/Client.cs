using System;
using ConnectUs.Business;

namespace ConnectUs.ClientSide
{
    public class Client : IConnection
    {
        private readonly IClientInformationService _clientInformationService;

        public Client(IClientInformationService clientInformationService)
        {
            _clientInformationService = clientInformationService;
        }

        public TResponse Execute<TRequest, TResponse>(TRequest request)
        {
            if (request is ClientInformationRequest) {
                return (TResponse) (object) new ClientInformationResponse
                {
                    Ip = _clientInformationService.GetIp(),
                    MachineName = _clientInformationService.GetMachineName(),
                };
            }

            throw new Exception("request invalid");
        }
    }
}