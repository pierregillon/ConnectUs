using System.Collections.Generic;
using System.Threading;
using ConnectUs.ServerSide.Application.CommandLines;
using ConnectUs.ServerSide.Application.ViewModels.Base;
using ConnectUs.ServerSide.Business;
using ConnectUs.ServerSide.Clients;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private readonly IRemoteClient _remoteClient;
        private readonly ClientInformationDecorator _clientInformationDecorator;
        private bool _continuePinging;

        public string Ip
        {
            get { return GetNotifiableProperty<string>("Ip"); }
            set { SetNotifiableProperty("Ip", value); }
        }
        public string MachineName
        {
            get { return GetNotifiableProperty<string>("MachineName"); }
            set { SetNotifiableProperty("MachineName", value); }
        }
        public int Ping
        {
            get { return GetNotifiableProperty<int>("Ping"); }
            set { SetNotifiableProperty("Ping", value); }
        }

        public ClientViewModel() {}
        public ClientViewModel(IRemoteClient remoteClient)
        {
            _remoteClient = remoteClient;
            _clientInformationDecorator = new ClientInformationDecorator(remoteClient);
        }

        public void StartPing()
        {
            var information = _clientInformationDecorator.GetClientInformation();
            Ip = information.Ip;
            MachineName = information.MachineName;
            _continuePinging = true;
            new Thread(PingProcess).Start();
        }
        public void Close()
        {
            _continuePinging = false;
            _remoteClient.Close();
        }
        public string ExecuteCommand(ICommandLine commandLine, IEnumerable<string> parameters)
        {
            return commandLine.ExecuteCommand(_remoteClient, parameters);
        }

        private void PingProcess()
        {
            try {
                while (_continuePinging) {
                    Ping = _clientInformationDecorator.Ping();
                    Thread.Sleep(5000);
                }
            }
            catch (RemoteClientException) {
                _remoteClient.Close();
            }
        }
    }
}