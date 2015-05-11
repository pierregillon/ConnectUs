using ConnectUs.ServerSide.Application.Services;
using ConnectUs.ServerSide.Application.ViewModels.Base;
using GalaSoft.MvvmLight.Command;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class ClientCommandViewModel : ViewModelBase, IBootable<ClientViewModel>
    {
        private readonly IClientCommandService _clientCommandService;
        private ClientViewModel _clientViewModel;

        public string CurrentCommand
        {
            get { return GetNotifiableProperty<string>("CurrentCommand"); }
            set
            {
                SetNotifiableProperty("CurrentCommand", value);
                ExecuteCommand.RaiseCanExecuteChanged();
            }
        }
        public string History
        {
            get { return GetNotifiableProperty<string>("History"); }
            set { SetNotifiableProperty("History", value); }
        }
        public RelayCommand ExecuteCommand { get; private set; }

        public ClientCommandViewModel(IClientCommandService clientCommandService)
        {
            _clientCommandService = clientCommandService;
            History = string.Empty;
            ExecuteCommand = new RelayCommand(Execute, CanExecute);
        }

        private bool CanExecute()
        {
            return string.IsNullOrEmpty(CurrentCommand);
        }
        private void Execute()
        {
            var arguments = CurrentCommand.Trim().Split(' ');
            History += _clientCommandService.ExecuteCommand(_clientViewModel, arguments);
        }

        public void Boot(ClientViewModel clientViewModel)
        {
            _clientViewModel = clientViewModel;
        }
    }
}