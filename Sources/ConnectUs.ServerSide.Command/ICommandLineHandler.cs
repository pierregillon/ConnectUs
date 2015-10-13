namespace ConnectUs.ServerSide.Command
{
    public interface ICommandLineHandler
    {
        void Handle(CommandLine commandLine);
    }
}