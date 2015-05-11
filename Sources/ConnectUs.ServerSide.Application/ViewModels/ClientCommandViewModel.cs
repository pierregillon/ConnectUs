using System;
using ConnectUs.ServerSide.Application.Services;
using ConnectUs.ServerSide.Application.ViewModels.Base;
using ConnectUs.ServerSide.Application.ViewModels.Messages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class ClientCommandViewModel : ViewModelBase, IBootable<ClientViewModel>, IDisposable
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
        public RelayCommand CloseTabCommand { get; private set; }
        public string TabHeader
        {
            get { return GetNotifiableProperty<string>("TabHeader"); }
            set { SetNotifiableProperty("TabHeader", value); }
        }

        public ClientCommandViewModel(IClientCommandService clientCommandService)
        {
            _clientCommandService = clientCommandService;
            History = string.Empty;
            ExecuteCommand = new RelayCommand(Execute, CanExecute);
            CloseTabCommand = new RelayCommand(CloseTab);
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
        private void CloseTab()
        {
            Messenger.Default.Send(new CloseCommandViewModelMessage(this));
        }

        public void Boot(ClientViewModel clientViewModel)
        {
            _clientViewModel = clientViewModel;
            TabHeader = string.Format("{0} ({1})", _clientViewModel.MachineName, _clientViewModel.Ip);
        }
        public void Dispose()
        {
        }
        public bool Match(ClientViewModel clientViewModel)
        {
            return _clientViewModel == clientViewModel;
        }
    }
}