
namespace ConnectUs.ServerSide
{
    public interface IRemoteClient
    {
        TResponse Send<TRequest, TResponse>(TRequest request);
        string UploadFile(string sourceFilePath, string targetDirectory);
        void Close();
    }
}