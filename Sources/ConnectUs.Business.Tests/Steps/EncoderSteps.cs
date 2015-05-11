using System.Text;
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
        public string DecodedData
        {
            get { return ScenarioContext.Current.Get<string>("DecodedData"); }
            set { ScenarioContext.Current.Add("DecodedData", value); }
        }
        public string DataToEncode
        {
            get { return ScenarioContext.Current.Get<string>("DataToEncode"); }
            set { ScenarioContext.Current.Add("DataToEncode", value); }
        }
        public byte[] DataToDecode
        {
            get { return ScenarioContext.Current.Get<byte[]>("DataToDecode"); }
            set { ScenarioContext.Current.Add("DataToDecode", value); }
        }

        [Given(@"An encoder")]
        public void GivenAnEncoder()
        {
            Encoder = new JsonEncoder();
        }

        [Given(@"A data to encode ""(.*)""")]
        public void GivenADataToEncode(string data)
        {
            DataToEncode = data;
        }

        [Given(@"A data to decode")]
        public void GivenADataToDecode()
        {
            DataToDecode = new UTF8Encoding().GetBytes("hello world");
        }

        [When(@"I encode the data with the encoder")]
        public void WhenIEncodeTheDataWithTheEncoder()
        {
            EncodedData = Encoder.Encode(DataToEncode);
        }

        [When(@"I decode the encoded data with the encoder")]
        public void WhenIDecodeTheEncodedDataWithTheEncoder()
        {
            DecodedData = Encoder.Decode(EncodedData);
        }

        [Then(@"I get an encoded data")]
        public void ThenIGetAnEncodedData()
        {
            Check.That(EncodedData).IsNotNull();
        }

        [Then(@"I get the decoded data ""(.*)""")]
        public void ThenIGetTheDecodedData(string decodedData)
        {
            Check.That(DecodedData).IsEqualTo(decodedData);
        }
    }
}