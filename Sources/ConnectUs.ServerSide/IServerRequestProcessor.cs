
namespace ConnectUs.ServerSide
{
    public interface IServerRequestProcessor
    {
        TResponse ProcessRequest<TRequest, TResponse>(TRequest request);
        string UploadFile(string sourceFilePath, string targetDirectory);
        void Close();
    }
}