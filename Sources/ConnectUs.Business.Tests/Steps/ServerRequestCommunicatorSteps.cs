using System;
using ConnectUs.Business.Connections;
using ConnectUs.ClientSide.Commands.GetClientInformation;
using ConnectUs.ServerSide;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ServerRequestCommunicatorSteps
    {
        public IConnection ServerConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ServerConnection"); }
            set { ScenarioContext.Current.Set(value, "ServerConnection"); }
        }
        public IConnection ClientConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ClientConnection"); }
            set { ScenarioContext.Current.Set(value, "ClientConnection"); }
        }
        public IServerRequestCommunicator ServerRequestCommunicator
        {
            get { return ScenarioContext.Current.Get<IServerRequestCommunicator>("ServerRequestCommunicator"); }
            set { ScenarioContext.Current.Set(value, "ServerRequestCommunicator"); }
        }
        public object Request
        {
            get { return ScenarioContext.Current.Get<object>("Request"); }
            set { ScenarioContext.Current.Set(value, "Request"); }
        }
        public object Response
        {
            get { return ScenarioContext.Current.Get<object>("Response"); }
            set { ScenarioContext.Current.Set(value, "Response"); }
        }
        public Exception Error
        {
            get { return ScenarioContext.Current.Get<Exception>("Error"); }
            set { ScenarioContext.Current.Set(value, "Error"); }
        }

        [Given(@"A server request communicator")]
        public void GivenAServerRequestCommunicator()
        {
            ServerRequestCommunicator = new ServerRequestCommunicator(ServerConnection, new JsonRequestParser());
        }

        [When(@"I send the request by the server request communicator")]
        public void WhenISendTheRequestByTheServerRequestCommunicator()
        {
            ServerRequestCommunicator.SendRequest(Request);
        }

        [When(@"I read the response from the server request communicator")]
        public void WhenIReadTheResponseFromTheServerRequestCommunicator()
        {
            try {
                Response = ServerRequestCommunicator.ReceiveResponse<GetClientInformationResponse>();
            }
            catch (Exception ex) {
                Error = ex;
            }
        }

        [Then(@"An exception is thrown with the message ""(.*)""")]
        public void ThenAnExceptionIsThrownWithTheMessage(string p0)
        {
            Check.That(Error.Message).IsEqualTo(p0);
        }
    }
}
