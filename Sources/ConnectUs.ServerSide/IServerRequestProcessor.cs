
namespace ConnectUs.ServerSide
{
    public interface IServerRequestProcessor
    {
        TResponse ProcessRequest<TRequest, TResponse>(TRequest request);
        void UploadFile(string filePath);
        void Close();
    }
}