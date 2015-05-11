using System;

namespace ConnectUs.ClientSide
{
    public class ClientRequestProcessor : IClientRequestProcessor
    {
        private readonly IModuleService _moduleService;

        public ClientRequestProcessor(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        public object Process(string requestName, string originalData)
        {
            var command = _moduleService.GetCommand(requestName);
            if (command == null) {
                throw new Exception("");
            }
            return command.Execute(originalData);
        }
    }
}