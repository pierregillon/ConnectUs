namespace ConnectUs.Core.ClientSide
{
    public interface IInstaller
    {
        bool IsInstalled { get; }
        string Install();
    }
}