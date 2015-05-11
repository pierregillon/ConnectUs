using System.Windows;
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

            SimpleIoc.Default.GetInstance<IServer>().Stop();
        }
    }
}
