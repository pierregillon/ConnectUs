using ConnectUs.Business.Commands;

namespace ConnectUs.ClientSide
{
    public interface IModuleService
    {
        ICommand GetCommand(string requestName);
    }
}