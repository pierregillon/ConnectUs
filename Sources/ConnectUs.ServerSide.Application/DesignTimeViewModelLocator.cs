﻿using System.Collections.ObjectModel;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.ServerSide.Application.Services;
using ConnectUs.ServerSide.Application.ViewModels;
using Moq;

namespace ConnectUs.ServerSide.Application
{
    public class DesignTimeViewModelLocator
    {
        public ClientListViewModel ClientListViewModel
        {
            get
            {
                var mock = new Mock<IClientViewModelService>();
                mock.Setup(server => server.GetClients()).Returns(new ObservableCollection<ClientViewModel>
                {
                    new ClientViewModel {Ip = "156.253.32.2", MachineName = "Desktop PC", Ping = 20},
                    new ClientViewModel {Ip = "224.210.10.11", MachineName = "My machine", Ping = 35},
                    new ClientViewModel {Ip = "110.8.102.8", MachineName = "Marine", Ping = 20},
                });

                var mockServer = new Mock<IRemoteClientListener>();
                mockServer.Setup(server => server.Start(9000)).Callback(()=>{});

                var vm = new ClientListViewModel(mockServer.Object, mock.Object);
                vm.Boot();
                return vm;
            }
        }
        public ClientCommandViewModel ClientCommandViewModel
        {
            get
            {
                var mock = new Mock<IClientCommandService>();
                var vm = new ClientCommandViewModel(mock.Object);
                return vm;
            }
        }
    }
}