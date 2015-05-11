using System;
using ConnectUs.Business;

namespace ConnectUs.ClientSide
{
    public class RequestProcessor : IRequestProcessor
    {
        private readonly IClientInformationService _clientInformationService;

        public RequestProcessor(IClientInformationService clientInformationService)
        {
            _clientInformationService = clientInformationService;
        }

        public Response Process(Request request)
        {
            if (request.Name == "GetClientInformation") {
                return new Response
                {
                    Result = "{" + string.Format("\"Ip\": \"{0}\", \"MachineName\": \"{1}\"", _clientInformationService.GetIp(), _clientInformationService.GetMachineName()) + "}"
                };
            }

            throw new Exception("request invalid");
        }
        public void Close()
        {
            
        }
    }
}