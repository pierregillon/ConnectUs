namespace ConnectUs.Core.ClientSide
{
    public interface IInstaller
    {
        bool IsInstalled { get; }
        bool IsPartiallyInstalled { get; }
        string Install();
        void Uninstall();
    }
}