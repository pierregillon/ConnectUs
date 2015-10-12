using System.Windows;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.ServerSide.Application.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace ConnectUs.ServerSide.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            SimpleIoc.Default.GetInstance<IRemoteClientListener>().Stop();
            SimpleIoc.Default.GetInstance<ClientListViewModel>().Dispose();
        }
    }
}
