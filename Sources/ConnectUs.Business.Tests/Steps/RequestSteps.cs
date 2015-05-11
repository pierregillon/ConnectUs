using System.Linq;
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
            Request = new Request{Name = requestName};
        }

        [Given(@"The request has a parameter ""(.*)"" with the value ""(.*)""")]
        public void GivenTheRequestHasAParameterWithTheValue(string name, string value)
        {
            Request.Parameters.Add(new RequestParameter(name, value));
        }

        [Then(@"The request has the name ""(.*)""")]
        public void ThenTheRequestReceivedHasTheName(string name)
        {
            Check.That(Request.Name).IsEqualTo(name);
        }

        [Then(@"The request contains (.*) parameters")]
        public void ThenTheRequestReceivedContainsParameters(int parameterCount)
        {
            Check.That(Request.Parameters.Count).IsEqualTo(parameterCount);
        }

        [Then(@"The request contains a parameter ""(.*)"" with the value ""(.*)""")]
        public void ThenTheRequestReceivedContainsAParameterWithTheValue(string parameterName, string parameterValue)
        {
            var parameter = Request.Parameters.First(requestParameter => requestParameter.Name == parameterName);
            Check.That(parameter).IsNotNull();
            Check.That(parameter.Value).IsEqualTo(parameterValue);
        }
    }
}