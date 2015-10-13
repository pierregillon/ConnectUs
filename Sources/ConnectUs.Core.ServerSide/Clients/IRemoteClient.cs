
namespace ConnectUs.Core.ServerSide.Clients
{
    public interface IRemoteClient
    {
        TResponse Send<TRequest, TResponse>(TRequest request);
        string UploadFile(string sourceFilePath, string targetDirectory);
        string DownloadFile(string remoteFilePath, string localFolder);
        void Close();
    }
}