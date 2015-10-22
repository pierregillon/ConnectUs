namespace ConnectUs.Core.ClientSide
{
    public interface IRegistry
    {
        void AddFileToStartupRegistry(string filePath);
        void RemoveFileFromStartupRegistry(string filePath);
        bool IsRegisteredAtStartup(string filePath);
        
        string Get(string subKey, string key);
        void Add(string subKey, string key, string value);
        void Remove(string subKey, string key);
    }
}