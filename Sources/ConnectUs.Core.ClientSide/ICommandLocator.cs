namespace ConnectUs.Core.ClientSide
{
    public interface ICommandLocator
    {
        object GetCommand(string requestName);
    }
}