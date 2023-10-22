namespace BrazilianAddresses.Domain.Entities
{
    public class IBGE : BaseEntity
    {
        public string IBGECode { get; set; }

        public string State { get; set; }

        public string City { get; set; }
    }
}
