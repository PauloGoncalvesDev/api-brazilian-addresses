namespace BrazilianAddresses.Communication.Responses
{
    public class AddressResponseJson
    {
        public string IBGECode { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}