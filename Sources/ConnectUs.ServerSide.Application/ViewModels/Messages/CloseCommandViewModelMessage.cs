namespace ConnectUs.ServerSide.Application.ViewModels.Messages
{
    internal class CloseCommandViewModelMessage
    {
        public ClientCommandViewModel ViewModel { get; private set; }

        public CloseCommandViewModelMessage(ClientCommandViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}