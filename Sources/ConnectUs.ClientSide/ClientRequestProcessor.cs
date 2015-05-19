using System;
using System.Linq;
using ConnectUs.Business;

namespace ConnectUs.ClientSide
{
    public class ClientRequestProcessor : IClientRequestProcessor
    {
        private readonly ICommandLocator _commandLocator;
        private readonly IRequestParser _requestParser;

        public ClientRequestProcessor(ICommandLocator commandLocator, IRequestParser requestParser)
        {
            _commandLocator = commandLocator;
            _requestParser = requestParser;
        }

        public string Process(string requestName, string originalData)
        {
            object result;
            try {
                var command = _commandLocator.GetCommand(requestName);
                if (command == null) {
                    throw new ProcessException(string.Format("The request '{0}' is unknown.", requestName));
                }
                result = ExecuteCommand(command, originalData);
            }
            catch (Exception ex) {
                result = new ErrorResponse {Error = ex.Message};
            }
            return _requestParser.ConvertToString(result);
        }

        private object ExecuteCommand(object command, string originalData)
        {
            var methodInfo = command.GetType().GetMethod("Execute");
            if (methodInfo == null) {
                throw new ProcessException(string.Format("Unable to find the method 'Execute' on the command '{0}'.", command.GetType().Name));
            }
            var requestType = methodInfo.GetParameters().Single().ParameterType;
            var request = _requestParser.FromString(originalData, requestType);
            return methodInfo.Invoke(command, new[] {request});
        }
    }
}