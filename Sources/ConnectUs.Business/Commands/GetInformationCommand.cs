using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConnectUs.Business.Commands
{
    public class GetInformationCommand : ICommandExecutable
    {
        public string Name { get { return "GetClientInformation"; } }

        private readonly IClientInformationService _clientInformationService;

        public GetInformationCommand(IClientInformationService clientInformationService)
        {
            _clientInformationService = clientInformationService;
        }

        public string Execute(IEnumerable<RequestParameter> parameters)
        {
            return JsonConvert.SerializeObject(new
            {
                Ip = _clientInformationService.GetIp(),
                MachineName = _clientInformationService.GetMachineName()
            });
        }
        public Request BuildRequest(IEnumerable<string> arguments)
        {
            throw new System.NotImplementedException();
        }

        public Request BuildRequest(string arguments)
        {
            throw new System.NotImplementedException();
        }

        public string DisplayResponse(Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}
