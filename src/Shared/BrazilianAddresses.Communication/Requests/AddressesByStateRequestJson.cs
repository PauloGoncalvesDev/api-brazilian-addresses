using System.ComponentModel.DataAnnotations;

namespace BrazilianAddresses.Communication.Requests
{
    public class AddressesByStateRequestJson : PaginationBaseRequestJson
    {
        [Required]
        [MinLength(2)]
        public string State { get; set; }
    }
}