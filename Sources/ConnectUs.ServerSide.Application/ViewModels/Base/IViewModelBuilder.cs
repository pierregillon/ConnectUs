namespace ConnectUs.ServerSide.Application.ViewModels.Base
{
    public interface IViewModelBuilder
    {
        ClientCommandViewModel BuildNewClientCommandViewModel();
    }
}