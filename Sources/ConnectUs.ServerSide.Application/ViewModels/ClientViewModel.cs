using System;
using System.Diagnostics;
using System.Threading;
using ConnectUs.Business;
using ConnectUs.ServerSide.Application.ViewModels.Base;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private readonly Client _client;

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
        public ClientViewModel(Client client)
        {
            _client = client;
        }

        public void StartPing()
        {
            var information = _client.GetClientInformation();
            Ip = information.Ip;
            MachineName = information.MachineName;
            new Thread(PingProcess).Start();
        }

        public Response Execute(Request request)
        {
            return _client.Execute(request);
        }

        private void PingProcess()
        {
            try {
                var watch = new Stopwatch();
                while (true) {
                    watch.Start();
                    _client.Ping();
                    watch.Stop();
                    Ping = (int) watch.ElapsedMilliseconds;
                    watch.Reset();
                    Thread.Sleep(5000);
                }
            }
            catch (ClientException) {}
        }
    }
}