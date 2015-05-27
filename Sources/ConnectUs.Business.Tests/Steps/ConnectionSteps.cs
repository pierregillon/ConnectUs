using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ConnectUs.Business.Connections;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ConnectionSteps
    {
        public IConnection ServerConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ServerConnection"); }
            set { ScenarioContext.Current.Add("ServerConnection", value); }
        }
        public IConnection ClientConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ClientConnection"); }
            set { ScenarioContext.Current.Add("ClientConnection", value); }
        }
        public ConnectionException ConnectionException
        {
            get { return ScenarioContext.Current.Get<ConnectionException>("ConnectionException"); }
            set { ScenarioContext.Current.Add("ConnectionException", value); }
        }
        
        [Given(@"A connection is established between server and client on port (.*)")]
        public void GivenAConnectionIsEstablishedBetweenServerAndClientOnPort(int port)
        {
            var serverTask = Task.Run<IConnection>(() =>
            {
                var listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                var client = listener.AcceptTcpClient();
                listener.Stop();
                return new TcpClientConnection(client);
            });

            var clientTask = Task.Run(() => TcpClientConnectionFactory.Build("localhost", port, 50));

            Task.WaitAll(serverTask, clientTask);

            ServerConnection = serverTask.Result;
            ClientConnection = clientTask.Result;
        }


        [When(@"I send the data ""(.*)"" through the server connection")]
        public void WhenISendTheDataThroughTheServerConnection(string data)
        {
            var encoding = new UTF8Encoding();
            ServerConnection.Send(encoding.GetBytes(data));
        }

        [When(@"I send the data ""(.*)"" through the client connection")]
        public void WhenISendTheDataThroughTheClientConnection(string data)
        {
            var encoding = new UTF8Encoding();
            ClientConnection.Send(encoding.GetBytes(data));
        }

        [When(@"I close the client connection")]
        public void WhenICloseTheClientConnection()
        {
            ClientConnection.Close();
        }

        [Then(@"A client connection is instancied")]
        public void ThenAClientConnectionIsInstancied()
        {
            Check.That(ClientConnection).IsNotNull();
        }

        [Then(@"A server connection is instancied")]
        public void ThenAServerConnectionIsInstancied()
        {
            Check.That(ServerConnection).IsNotNull();
        }

        [Then(@"The client connection receives the data ""(.*)""")]
        public void ThenTheClientConnectionReceivesTheData(string expectedJson)
        {
            var encoding = new UTF8Encoding();
            var json = encoding.GetString(ClientConnection.Read());
            Check.That(json).IsEqualTo(expectedJson);
        }

        [Then(@"The server connection receives the data ""(.*)""")]
        public void ThenTheServerConnectionReceivesTheData(string expectedJson)
        {
            var encoding = new UTF8Encoding();
            var json = encoding.GetString(ServerConnection.Read());
            Check.That(json).IsEqualTo(expectedJson);
        }

        [Then(@"I get a connection exception")]
        public void ThenIGetAClientConnectionException()
        {
            ScenarioContext.Current.Pending();
            Check.That(ConnectionException).IsNotNull();
        }
    }
}