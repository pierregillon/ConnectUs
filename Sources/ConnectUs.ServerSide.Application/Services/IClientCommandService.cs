using ConnectUs.ServerSide.Application.ViewModels;

namespace ConnectUs.ServerSide.Application.Services
{
    public interface IClientCommandService
    {
        string ExecuteCommand(ClientViewModel clientViewModel, string[] arguments);
    }
}