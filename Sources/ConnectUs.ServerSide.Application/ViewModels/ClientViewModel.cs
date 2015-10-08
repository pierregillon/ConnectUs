using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ConnectUs.ServerSide.Application.CommandLines;
using ConnectUs.ServerSide.Application.ViewModels.Base;
using ConnectUs.ServerSide.Business;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private readonly RemoteClient _remoteClient;
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
        public ClientViewModel(RemoteClient remoteClient)
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

        private void PingProcess()
        {
            try {
                var watch = new Stopwatch();
                while (_continuePinging) {
                    watch.Start();
                    _clientInformationDecorator.Ping();
                    watch.Stop();
                    Ping = (int) watch.ElapsedMilliseconds;
                    watch.Reset();
                    Thread.Sleep(5000);
                }
            }
            catch (ClientException) {
                _remoteClient.CloseConnection();
            }
        }
        public void Close()
        {
            _continuePinging = false;
            _remoteClient.CloseConnection();
        }

        public string ExecuteCommand(ICommandLine commandLine, IEnumerable<string> parameters)
        {
            return commandLine.ExecuteCommand(_remoteClient, parameters);
        }
    }
}