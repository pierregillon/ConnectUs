using ConnectUs.Business.Commands.ClientInformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class RequestSteps
    {
        public RequestBase Request
        {
            get { return ScenarioContext.Current.Get<RequestBase>("RequestBase"); }
            set { ScenarioContext.Current.Add("RequestBase", value); }
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