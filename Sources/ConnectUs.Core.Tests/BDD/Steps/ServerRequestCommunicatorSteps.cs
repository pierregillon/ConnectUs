using System;
using ConnectUs.Core.Connections;
using ConnectUs.Core.ServerSide.Requests;
using ConnectUs.Modules.Integrated.GetClientInformation;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.BDD.Steps
{
    [Binding]
    public class ServerRequestDispatcherSteps
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
        public IRequestDispatcher RequestDispatcher
        {
            get { return ScenarioContext.Current.Get<IRequestDispatcher>("RequestDispatcher"); }
            set { ScenarioContext.Current.Set(value, "RequestDispatcher"); }
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

        [Given(@"A server request dispatcher")]
        public void GivenAServerRequestDispatcher()
        {
            RequestDispatcher = new RequestDispatcher(ServerConnection, new JsonRequestParser());
        }

        [When(@"I send the request by the server request dispatcher")]
        public void WhenISendTheRequestByTheServerRequestDispatcher()
        {
            RequestDispatcher.SendRequest(Request);
        }

        [When(@"I read the response from the server request dispatcher")]
        public void WhenIReadTheResponseFromTheServerRequestDispatcher()
        {
            try {
                Response = RequestDispatcher.ReceiveResponse<GetClientInformationResponse>();
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
