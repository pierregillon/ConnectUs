namespace ConnectUs.Core.ClientSide
{
    public interface IRemoteServerConnector
    {
        void StartFinding();
        void StopFinding();
    }
}