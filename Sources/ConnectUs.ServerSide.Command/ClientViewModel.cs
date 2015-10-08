using System;
using System.Diagnostics;
using ConnectUs.ServerSide.Business;

namespace ConnectUs.ServerSide.Command
{
    public class ClientViewModel
    {
        private readonly ClientInformationDecorator _clientInformationDecorator;

        public string Ip { get; private set; }
        public string MachineName { get; private set; }
        public int Latency { get; private set; }
        public Client Client { get; private set; }

        public ClientViewModel(Client client)
        {
            Client = client;
            
            _clientInformationDecorator = new ClientInformationDecorator(Client);
            var information = _clientInformationDecorator.GetClientInformation();
            Ip = information.Ip;
            MachineName = information.MachineName;
        }

        public bool Match(Client client)
        {
            return Client == client;
        }
        public void Ping()
        {
            try {
                var watch = new Stopwatch();
                watch.Start();
                _clientInformationDecorator.Ping();
                watch.Stop();
                Latency = (int) watch.ElapsedMilliseconds;
            }
            catch (Exception) {
                Client.CloseConnection();
            }
        }
    }
}