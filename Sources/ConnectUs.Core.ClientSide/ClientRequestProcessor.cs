using System;
using System.Linq;
using System.Reflection;

namespace ConnectUs.Core.ClientSide
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

        public byte[] Process(byte[] data)
        {
            object result;
            try {
                var requestName = _requestParser.GetRequestName(data);
                var command = _commandLocator.GetCommand(requestName);
                if (command == null) {
                    throw new ProcessException(string.Format("The request '{0}' is unknown.", requestName));
                }
                result = ExecuteCommand(command, data);
            }
            catch (TargetInvocationException ex) {
                result = new ErrorResponse {Error = ex.InnerException.Message};
            }
            catch (Exception ex) {
                result = new ErrorResponse {Error = ex.Message};
            }
            return _requestParser.ConvertToBytes(result);
        }

        private object ExecuteCommand(object command, byte[] data)
        {
            var methodInfo = command.GetType().GetMethod("Execute");
            if (methodInfo == null) {
                throw new ProcessException(string.Format("Unable to find the method 'Execute' on the command '{0}'.", command.GetType().Name));
            }
            var requestType = methodInfo.GetParameters().Single().ParameterType;
            var request = _requestParser.FromBytes(requestType, data);
            return methodInfo.Invoke(command, new[] {request});
        }
    }
}