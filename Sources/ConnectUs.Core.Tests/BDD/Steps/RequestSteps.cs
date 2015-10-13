using ConnectUs.Modules.Integrated.GetClientInformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.BDD.Steps
{
    [Binding]
    public class RequestSteps
    {
        public object Request
        {
            get { return ScenarioContext.Current.Get<object>("Request"); }
            set { ScenarioContext.Current.Add("Request", value); }
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