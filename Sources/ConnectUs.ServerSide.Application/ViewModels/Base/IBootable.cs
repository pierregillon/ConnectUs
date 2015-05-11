namespace ConnectUs.ServerSide.Application.ViewModels.Base
{
    public interface IBootable
    {
        void Boot();
    }

    public interface IBootable<in T>
    {
        void Boot(T parameter);
    }
}