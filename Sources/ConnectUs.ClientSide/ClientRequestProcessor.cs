using System;
using System.Linq;
using ConnectUs.Business;
using Newtonsoft.Json;

namespace ConnectUs.ClientSide
{
    public class ClientRequestProcessor : IClientRequestProcessor
    {
        private readonly ICommandLocator _commandLocator;

        public ClientRequestProcessor(ICommandLocator commandLocator)
        {
            _commandLocator = commandLocator;
        }

        public string Process(string requestName, string originalData)
        {
            try {
                var command = _commandLocator.GetCommand(requestName);
                if (command == null) {
                    throw new ProcessException(string.Format("The request '{0}' is unknown.", requestName));
                }
                return ExecuteCommand(command, originalData);
            }
            catch (Exception ex) {
                return JsonConvert.SerializeObject(new ErrorResponse {Error = ex.Message});
            }
        }

        private static string ExecuteCommand(object command, string originalData)
        {
            var methodInfo = command.GetType().GetMethod("Execute");
            if (methodInfo == null) {
                throw new ProcessException(string.Format("Unable to find the method 'Execute' on the command '{0}'.", command.GetType().Name));
            }
            var requestType = methodInfo.GetParameters().Single().ParameterType;
            var responseType = methodInfo.ReturnType;
            var request = JsonConvert.DeserializeObject(originalData, requestType);
            var response = methodInfo.Invoke(command, new[] {request});
            return JsonConvert.SerializeObject(response, responseType, new JsonSerializerSettings());
        }
    }
}