using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class RequestSteps
    {
        public Request Request
        {
            get { return ScenarioContext.Current.Get<Request>("Request"); }
            set { ScenarioContext.Current.Add("Request", value); }
        }

        [Given(@"A request with the name ""(.*)""")]
        public void GivenARequestWithTheName(string requestName)
        {
            Request = new Request(requestName);
        }

        [Given(@"A ""(.*)"" request")]
        public void GivenARequest(string requestType)
        {
            if (requestType == "GetClientInformation") {
                Request = new GetClientInformationRequest();
            }
            else {
                Assert.Fail("Unknown request");
            }
        }
    }
}