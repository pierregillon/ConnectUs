using ConnectUs.ServerSide.Application.Services;
using GalaSoft.MvvmLight.Ioc;

namespace ConnectUs.ServerSide.Application.ViewModels.Base
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic == false) {
                SimpleIoc.Default.Register(() => new ServerConfiguration { Port = 9000 });
                SimpleIoc.Default.Register<IServer, Server>();
                SimpleIoc.Default.Register<IClientViewModelService, ClientViewModelService>();
                SimpleIoc.Default.Register<ClientListViewModel>();

                SimpleIoc.Default.GetInstance<ClientListViewModel>().Boot();
            }
        }

        public ClientListViewModel ClientListViewModel
        {
            get { return SimpleIoc.Default.GetInstance<ClientListViewModel>(); }
        }
    }
}