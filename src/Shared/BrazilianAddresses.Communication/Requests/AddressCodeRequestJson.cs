using System.ComponentModel.DataAnnotations;
using BrazilianAddresses.Exceptions.ResourcesMessage;

namespace BrazilianAddresses.Communication.Requests
{
    public class AddressCodeRequestJson
    {
        [Required(ErrorMessageResourceName = "EMPTY_IBGECODE", ErrorMessageResourceType = typeof(APIMSG))]
        [RegularExpression("[0-9]+", ErrorMessageResourceName = "INVALID_CODE", ErrorMessageResourceType = typeof(APIMSG))]
        public string IBGECode { get; set; }
    }
}