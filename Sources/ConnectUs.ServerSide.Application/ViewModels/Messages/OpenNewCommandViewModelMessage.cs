namespace ConnectUs.ServerSide.Application.ViewModels.Messages
{
    internal class OpenNewCommandViewModelMessage
    {
        public ClientViewModel ClientViewModel { get; private set; }

        public OpenNewCommandViewModelMessage(ClientViewModel clientViewModel)
        {
            ClientViewModel = clientViewModel;
        }
    }
}