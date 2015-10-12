namespace ConnectUs.ClientSide
{
    public interface ICommandLocator
    {
        object GetCommand(string requestName);
    }
}