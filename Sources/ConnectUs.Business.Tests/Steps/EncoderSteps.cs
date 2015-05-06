using System.Linq;
using ConnectUs.Business.Encodings;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class EncoderSteps
    {
        public IEncoder Encoder
        {
            get { return ScenarioContext.Current.Get<IEncoder>("Encoder"); }
            set { ScenarioContext.Current.Add("Encoder", value); }
        }
        public byte[] EncodedData
        {
            get { return ScenarioContext.Current.Get<byte[]>("EncodedData"); }
            set { ScenarioContext.Current.Add("EncodedData", value); }
        }
        public Request DecodedRequest
        {
            get { return ScenarioContext.Current.Get<Request>("DecodedRequest"); }
            set { ScenarioContext.Current.Add("DecodedRequest", value); }
        }
        public Request Request
        {
            get { return ScenarioContext.Current.Get<Request>("Request"); }
            set { ScenarioContext.Current.Add("Request", value); }
        }

        [Given(@"An encoder")]
        public void GivenAnEncoder()
        {
            Encoder = new JsonEncoder();
        }

        [When(@"I encode the request with the encoder")]
        public void WhenIEncodeTheRequestWithTheEncoder()
        {
            EncodedData = Encoder.Encode(Request);
        }

        [When(@"I decode the request with the encoder")]
        public void WhenIDecodeTheRequestWithTheEncoder()
        {
            DecodedRequest = Encoder.Decode<Request>(EncodedData);
        }

        [Then(@"I got an encoded data")]
        public void ThenIGotAnEncodedData()
        {
            Check.That(EncodedData).IsNotNull();
        }

        [Then(@"I got a decoded request")]
        public void ThenIGotADecodedRequest()
        {
            Check.That(DecodedRequest).IsNotNull();
        }

        [Then(@"The decoded request has the name ""(.*)""")]
        public void ThenTheDecodedRequestHasTheName(string expectedName)
        {
            Check.That(DecodedRequest.Name).IsEqualTo(expectedName);
        }

        [Then(@"the decoded request has (.*) parameter")]
        public void ThenTheDecodedRequestHasParameter(int expectedCount)
        {
            Check.That(DecodedRequest.Parameters.Count).IsEqualTo(expectedCount);
        }

        [Then(@"the decoded request has a parameter ""(.*)"" with the value ""(.*)""")]
        public void ThenTheDecodedRequestHasAParameterWithTheValue(string expectedName, string expectedValue)
        {
            var parameter = DecodedRequest.Parameters.First(p => p.Name == expectedName);
            Check.That(parameter.Value).IsEqualTo(expectedValue);
        }

    }
}