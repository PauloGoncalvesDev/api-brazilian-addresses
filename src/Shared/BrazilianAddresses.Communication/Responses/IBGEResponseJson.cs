namespace BrazilianAddresses.Communication.Responses
{
    public class IBGEResponseJson : BaseResponseJson
    {
        public string IBGECode { get; set; }

        public string State { get; set; }

        public string City { get; set; }
    }
}