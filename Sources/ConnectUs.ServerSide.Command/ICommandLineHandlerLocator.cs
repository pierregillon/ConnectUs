namespace ConnectUs.ServerSide.Command
{
    public interface ICommandLineHandlerLocator
    {
        ICommandLineHandler Get(string commandName);
    }
}