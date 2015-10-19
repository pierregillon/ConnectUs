using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConnectUs.Core.Connections;

namespace ConnectUs.Core.ClientSide
{
    public class RemoteServerConnector : IRemoteServerConnector
    {
        private bool _continueProcessing;
        private int _remoteServerIndex;
        private static readonly AutoResetEvent ResetEvent = new AutoResetEvent(false);

        private readonly IList<RemoteServer> _remoteServers = new List<RemoteServer>
        {
            new RemoteServer("localhost", 9000),
            new RemoteServer("localhost", 8000)
        };
        private readonly IContinuousRequestProcessor _continuousRequestProcessor;
        private readonly IClientInformation _clientInformation;

        // ----- Constructors
        public RemoteServerConnector(IContinuousRequestProcessor continuousRequestProcessor, IClientInformation clientInformation)
        {
            _clientInformation = clientInformation;
            _continuousRequestProcessor = continuousRequestProcessor;
            _continuousRequestProcessor.ConnectionLost += ContinuousRequestProcessorOnConnectionLost;
        }

        // ----- Public methods
        public void StartFinding()
        {
            _continueProcessing = true;
            var thread = new Thread(FindRemoteServer);
            thread.Start();
        }
        public void StopFinding()
        {
            _continueProcessing = false;
        }

        // ----- Internal logics
        private void FindRemoteServer()
        {
            while (_continueProcessing) {
                try {
                    var remoteServer = GetNextRemoteServer();
                    Console.Write("\t => {0} : {1} ... ", remoteServer.Host, remoteServer.Port);
                    ConnectToServer(remoteServer.Host, remoteServer.Port);
                    Console.WriteLine("[OK]");
                    ResetEvent.WaitOne();
                }
                catch (ClientException) {
                    Console.WriteLine("[ERROR]");
                }
                finally {
                    Thread.Sleep(1000);
                }
            }
        }
        private void ConnectToServer(string hostName, int port)
        {
            try {
                var connection = TcpClientConnectionFactory.Build(hostName, port);
                _continuousRequestProcessor.StartProcessingRequestFromConnection(connection);
                _clientInformation.CurrentConnection = connection;
            }
            catch (ConnectionException) {
                throw new ClientException(string.Format("Unable to connect to the host '{0}' on the port '{1}'.", hostName, port));
            }
        }
        private RemoteServer GetNextRemoteServer()
        {
            if (_remoteServerIndex == _remoteServers.Count - 1) {
                _remoteServerIndex = 0;
            }
            else {
                _remoteServerIndex++;
            }
            return _remoteServers.ElementAt(_remoteServerIndex);
        }

        // ----- Event callbacks
        private void ContinuousRequestProcessorOnConnectionLost(object sender, EventArgs args)
        {
            _continuousRequestProcessor.StopProcessingRequestFromConnection();
            _clientInformation.CurrentConnection = null;
            ResetEvent.Set();
        }
    }
}