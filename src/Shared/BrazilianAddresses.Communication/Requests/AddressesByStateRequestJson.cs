using BrazilianAddresses.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace BrazilianAddresses.Communication.Requests
{
    public class AddressesByStateRequestJson : PaginationBaseRequestJson
    {
        [Required]
        public StateEnum State { get; set; }
    }
}