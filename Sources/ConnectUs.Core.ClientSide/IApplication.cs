namespace ConnectUs.Core.ClientSide
{
    public interface IApplication
    {
        bool IsWellLocated();
        void LoadModules();
        void ProcessRequests();
        string Install();
    }
}