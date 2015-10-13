namespace ConnectUs.ServerSide.Command
{
    public interface IConsole
    {
        void WriteInfo(string message, params object[] parameters);
        void WriteError(string message, params object[] parameters);
        void WriteWarning(string message, params object[] parameters);
    }
}