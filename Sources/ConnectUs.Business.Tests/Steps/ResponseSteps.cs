using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ResponseSteps
    {
        public Response Response
        {
            get { return ScenarioContext.Current.Get<Response>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }

        [Given(@"A response with the content ""(.*)""")]
        public void GivenAResponseWithTheContent(string content)
        {
            Response = new Response {Result = content};
        }

        [Then(@"I get a response with the error ""(.*)""")]
        public void ThenIGetAResponseWithTheError(string message)
        {
            Check.That(Response.Error).IsEqualTo(message);
        }

        [Then(@"I get a response with the result ""(.*)""")]
        public void ThenIGetAResponseWithTheResult(string expectedResult)
        {
            Check.That(Response.Result).IsEqualTo(expectedResult);
        }

    }
}