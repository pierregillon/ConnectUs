using System;
using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public interface IContinuousRequestProcessor
    {
        event EventHandler ConnectionLost;

        void StartProcessingRequestFromConnection(IConnection connection);
        void StopProcessingRequestFromConnection();
    }
}