namespace ConnectUs.ServerSide.Command
{
    public interface ICommandLineHandlerLocator
    {
        CommandLineHandler Get(string commandName);
    }
}