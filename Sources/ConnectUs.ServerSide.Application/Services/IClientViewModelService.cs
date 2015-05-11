using System.Collections.ObjectModel;
using ConnectUs.ServerSide.Application.ViewModels;

namespace ConnectUs.ServerSide.Application.Services
{
    public interface IClientViewModelService
    {
        ObservableCollection<ClientViewModel> GetClients();
    }
}