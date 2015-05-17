using ConnectUs.ServerSide.Application.ViewModels;

namespace ConnectUs.ServerSide.Application.Services
{
    public class ClientCommandService : IClientCommandService
    {
        public string ExecuteCommand(ClientViewModel clientViewModel, string[] arguments)
        {
            return "Unknown command";
        }
    }
}