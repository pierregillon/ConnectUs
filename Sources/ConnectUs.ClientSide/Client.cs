using System;
using System.Threading;
using ConnectUs.Business;

namespace ConnectUs.ClientSide
{
    public class Client
    {
        private readonly IConnector _connector;
        private readonly IMessageProcessor _messageProcessor;

        public Client(IConnector connector, IMessageProcessor messageProcessor)
        {
            _connector = connector;
            _messageProcessor = messageProcessor;
        }

        public void StartProcessRequest(string host, int port)
        {
            var thread = new Thread(() => ProcessRequest(host, port));
            thread.Start();
        }

        private void ProcessRequest(string host, int port)
        {
            while (true) {
                try {
                    var connection = _connector.Connect(host, port);
                    ProcessRequest(connection);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    Thread.Sleep(1000);
                }
            }
        }
        private void ProcessRequest(IConnection connection)
        {
            while (true) {
                try {
                    var message = connection.Read<Message>();
                    _messageProcessor.Process(connection, message);
                }
                catch (NothingToReadException) {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}