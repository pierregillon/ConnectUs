namespace ConnectUs.Business.Commands
{
    public interface ICommand
    {
        object Execute(string data);
    }
}