using System;
using ConnectUs.Core.Connections;

namespace ConnectUs.Core.ClientSide
{
    public interface IContinuousRequestProcessor
    {
        event EventHandler ConnectionLost;

        void StartProcessingRequestFromConnection(IConnection connection);
        void StopProcessingRequestFromConnection();
    }
}