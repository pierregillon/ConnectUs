namespace ConnectUs.Core.ClientSide
{
    public interface IRegistry
    {
        void AddFileToStartupRegistry(string filePath);
        void RemoveFileFromStartupRegistry(string filePath);
    }
}