
namespace ConnectUs.ServerSide
{
    public interface IRemoteClient
    {
        TResponse ExecuteCommand<TRequest, TResponse>(TRequest request);
        string Upload(string sourceFilePath, string targetDirectory);
        void CloseConnection();
    }
}