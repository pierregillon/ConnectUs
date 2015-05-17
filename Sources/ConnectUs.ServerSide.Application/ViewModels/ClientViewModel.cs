﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ConnectUs.ServerSide.Application.ViewModels.Base;
using ConnectUs.ServerSide.CommandLines;

namespace ConnectUs.ServerSide.Application.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private readonly Client _client;
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
        public ClientViewModel(Client client)
        {
            _client = client;
        }

        public void StartPing()
        {
            var information = _client.GetClientInformation();
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
                    _client.Ping();
                    watch.Stop();
                    Ping = (int) watch.ElapsedMilliseconds;
                    watch.Reset();
                    Thread.Sleep(5000);
                }
            }
            catch (ClientException) {
                _client.CloseConnection();
            }
        }
        public void Close()
        {
            _continuePinging = false;
            _client.CloseConnection();
        }

        public string ExecuteCommand(ICommandLine commandLine, IEnumerable<string> parameters)
        {
            return commandLine.ExecuteCommand(_client, parameters);
        }
    }
}