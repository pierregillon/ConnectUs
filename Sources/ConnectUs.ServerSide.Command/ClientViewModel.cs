using System;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command
{
    public class ClientViewModel
    {
        private readonly ClientInformationDecorator _clientInformationDecorator;

        public string Ip { get; private set; }
        public string MachineName { get; private set; }
        public int Latency { get; private set; }
        public string OperatingSystem { get; set; }
        public string UserName { get; set; }

        public IRemoteClient RemoteClient { get; private set; }

        public ClientViewModel(IRemoteClient remoteClient)
        {
            RemoteClient = remoteClient;

            _clientInformationDecorator = new ClientInformationDecorator(RemoteClient);

            UpdateInformation();
        }

        public bool Match(IRemoteClient remoteClient)
        {
            return RemoteClient == remoteClient;
        }
        public void Ping()
        {
            try {
                Latency = _clientInformationDecorator.Ping();
            }
            catch (Exception) {
                RemoteClient.Close();
            }
        }
        public void UpdateInformation()
        {
            try {
                var information = _clientInformationDecorator.GetClientInformation();
                Ip = information.Ip;
                MachineName = information.MachineName;
                UserName = information.UserName;
                OperatingSystem = information.OperatingSystem;
            }
            catch (Exception) {
                RemoteClient.Close();
            }
        }
    }
}