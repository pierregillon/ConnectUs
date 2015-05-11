﻿using System;
using ConnectUs.Business.Commands;
using ConnectUs.Business.Commands.ClientInformation;
using ConnectUs.Business.Connections;
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
            set { ScenarioContext.Current.Add("ServerConnection", value); }
        }
        public IConnection ClientConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ClientConnection"); }
            set { ScenarioContext.Current.Add("ClientConnection", value); }
        }
        public ServerRequestCommunicator ServerRequestCommunicator
        {
            get { return ScenarioContext.Current.Get<ServerRequestCommunicator>("ServerRequestCommunicator"); }
            set { ScenarioContext.Current.Add("ServerRequestCommunicator", value); }
        }
        public Request Request
        {
            get { return ScenarioContext.Current.Get<Request>("Request"); }
            set { ScenarioContext.Current.Add("Request", value); }
        }
        public Response Response
        {
            get { return ScenarioContext.Current.Get<Response>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }
        public Exception Error
        {
            get { return ScenarioContext.Current.Get<Exception>("Error"); }
            set { ScenarioContext.Current.Add("Error", value); }
        }

        [Given(@"A server request communicator")]
        public void GivenAServerRequestCommunicator()
        {
            ServerRequestCommunicator = new ServerRequestCommunicator(ServerConnection, new JsonRequestParser());
        }

        [When(@"I send the request by the server request communicator")]
        public void WhenISendTheRequestByTheServerRequestCommunicator()
        {
            ServerRequestCommunicator.SendToClient(Request);
        }

        [When(@"I read the response from the server request communicator")]
        public void WhenIReadTheResponseFromTheServerRequestCommunicator()
        {
            try {
                Response = ServerRequestCommunicator.ReceiveFromClient<GetClientInformationResponse>();
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