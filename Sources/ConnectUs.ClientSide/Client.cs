using System;

namespace ConnectUs.ClientSide
{
    public class Client
    {
        private readonly IClientInformationService _clientInformationService;

        public Client(IClientInformationService clientInformationService)
        {
            _clientInformationService = clientInformationService;
        }

        public Response Execute(Request request)
        {
            if (request.Name == "GetClientInformation") {
                return new Response
                {
                    Content = "{" + string.Format("\"Ip\": \"{0}\", \"MachineName\": \"{1}\"", _clientInformationService.GetIp(), _clientInformationService.GetMachineName()) + "}"
                };
            }

            throw new Exception("request invalid");
        }
    }

    public class Response
    {
        public string Content { get; set; }
    }

    public class Request
    {
        public string Name { get; set; }
        public object[] Parameters { get; set; }
    }
}