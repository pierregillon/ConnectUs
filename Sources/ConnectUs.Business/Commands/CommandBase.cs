using Newtonsoft.Json;

namespace ConnectUs.Business.Commands
{
    public abstract class CommandBase<TRequest, TResponse> : ICommand
    {
        public object Execute(string data)
        {
            var request = JsonConvert.DeserializeObject<TRequest>(data);
            return ExecuteRequest(request);
        }

        protected abstract TResponse ExecuteRequest(TRequest request);
    }
}