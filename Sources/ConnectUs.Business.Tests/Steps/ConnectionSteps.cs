using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ConnectUs.Business.Connections;
using ConnectUs.Business.Encodings;
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
        public Request Request
        {
            get { return ScenarioContext.Current.Get<Request>("Request"); }
            set { ScenarioContext.Current.Add("Request", value); }
        }
        public Request RequestReceived
        {
            get { return ScenarioContext.Current.Get<Request>("RequestReceived"); }
            set { ScenarioContext.Current.Add("RequestReceived", value); }
        }
        public Response Response
        {
            get { return ScenarioContext.Current.Get<Response>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }
        public Response ResponseReceived
        {
            get { return ScenarioContext.Current.Get<Response>("ResponseReceived"); }
            set { ScenarioContext.Current.Add("ResponseReceived", value); }
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
                return new TcpClientConnection(client, new JsonEncoder());
            });

            var clientTask = Task.Run<IConnection>(() =>
            {
                var client = new TcpClient();
                client.Connect("localhost", port);
                return new TcpClientConnection(client, new JsonEncoder());
            });

            Task.WaitAll(serverTask, clientTask);

            ServerConnection = serverTask.Result;
            ClientConnection = clientTask.Result;
        }

        [When(@"I send the request through the server connection")]
        public void WhenISendTheRequestThroughTheServerConnection()
        {
            ServerConnection.Send(Request);
        }

        [When(@"I send the response through the client connection")]
        public void WhenISendTheResponseThroughTheClientConnection()
        {
            ClientConnection.Send(Response);
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

        [Then(@"The client connection receives the request")]
        public void ThenTheClientConnectionReceivesTheRequest()
        {
            RequestReceived = ClientConnection.Read<Request>();
        }

        [Then(@"The request received has the name ""(.*)""")]
        public void ThenTheRequestReceivedHasTheName(string name)
        {
            Check.That(RequestReceived.Name).IsEqualTo(name);
        }

        [Then(@"The request received contains (.*) parameters")]
        public void ThenTheRequestReceivedContainsParameters(int parameterCount)
        {
            Check.That(RequestReceived.Parameters.Count).IsEqualTo(parameterCount);
        }

        [Then(@"The request received contains a parameter ""(.*)"" with the value ""(.*)""")]
        public void ThenTheRequestReceivedContainsAParameterWithTheValue(string parameterName, string parameterValue)
        {
            var parameter = RequestReceived.Parameters.First(requestParameter => requestParameter.Name == parameterName);
            Check.That(parameter).IsNotNull();
            Check.That(parameter.Value).IsEqualTo(parameterValue);
        }

        [Then(@"The server connection receives the response")]
        public void ThenTheServerConnectionReceivesTheResponse()
        {
            ResponseReceived = ServerConnection.Read<Response>();
        }

        [Then(@"The response received contains contains the value ""(.*)""")]
        public void ThenTheResponseReceivedContainsContainsTheValue(string expectedContent)
        {
            Check.That(ResponseReceived.Content).IsEqualTo(expectedContent);
        }

    }
}