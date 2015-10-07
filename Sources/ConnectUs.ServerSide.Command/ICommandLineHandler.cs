namespace ConnectUs.ServerSide.Command
{
    public interface ICommandLineHandler
    {
        string Handle(CommandLine commandLine);
    }
}