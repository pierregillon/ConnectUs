using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ConnectUs.ServerSide.Application.Controls;
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

        public RelayCommand CloseTabCommand { get; private set; }
        public string TabHeader
        {
            get { return GetNotifiableProperty<string>("TabHeader"); }
            set { SetNotifiableProperty("TabHeader", value); }
        }
        public ObservableCollection<CommandLine> CommandLines
        {
            get { return GetNotifiableProperty<ObservableCollection<CommandLine>>("CommandLines"); }
            set { SetNotifiableProperty("CommandLines", value); }
        }

        // ----- Constructors
        public ClientCommandViewModel(IClientCommandService clientCommandService)
        {
            _clientCommandService = clientCommandService;
            CloseTabCommand = new RelayCommand(CloseTab);
            CommandLines = new ObservableCollection<CommandLine>();
            CommandLines.CollectionChanged+=CommandLinesOnCollectionChanged;
        }

        // ----- Public methods
        public void Boot(ClientViewModel clientViewModel)
        {
            _clientViewModel = clientViewModel;
            TabHeader = string.Format("{0} ({1})", _clientViewModel.MachineName, _clientViewModel.Ip);
        }
        public void Dispose() { }
        public bool Match(ClientViewModel clientViewModel)
        {
            return _clientViewModel == clientViewModel;
        }

        // ----- Callbacks
        private void CommandLinesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add) {
                var commandLine = args.NewItems.OfType<CommandLine>().First();
                ExecuteCommandLine(commandLine);
            }
        }

        // ----- Command implementations
        private void CloseTab()
        {
            Messenger.Default.Send(new CloseCommandViewModelMessage(this));
        }

        // ----- Internal logics
        private void ExecuteCommandLine(CommandLine commandLine)
        {
            if (string.IsNullOrEmpty(commandLine.Command) == false) {
                var arguments = commandLine.Command.Trim().Split(' ');
                commandLine.Result = _clientCommandService.ExecuteCommand(_clientViewModel, arguments);
            }
        }
    }
}