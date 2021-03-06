﻿using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.ServerSide.Application.Services;
using GalaSoft.MvvmLight.Ioc;

namespace ConnectUs.ServerSide.Application.ViewModels.Base
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic == false) {
                SimpleIoc.Default.Register(() => new RemoteClientListenerFactory().Build());
                SimpleIoc.Default.Register<IViewModelBuilder, ViewModelBuilder>();
                SimpleIoc.Default.Register<IClientCommandService, ClientCommandService>();
                SimpleIoc.Default.Register<IClientViewModelService, ClientViewModelService>();
                SimpleIoc.Default.Register<ClientListViewModel>();
                SimpleIoc.Default.Register<MainViewModel>();

                SimpleIoc.Default.GetInstance<ClientListViewModel>().Boot();
            }
        }

        public ClientListViewModel ClientListViewModel
        {
            get { return SimpleIoc.Default.GetInstance<ClientListViewModel>(); }
        }

        public MainViewModel MainViewModel
        {
            get
            {
                var viewModel = new MainViewModel(new ViewModelBuilder());
                return viewModel;
            }
        }
    }
}