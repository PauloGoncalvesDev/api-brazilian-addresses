using BrazilianAddresses.Exceptions.ResourcesMessage;
using System.ComponentModel.DataAnnotations;

namespace BrazilianAddresses.Communication.Requests
{
    public class IBGERemoveRequestJson
    {
        [Required(ErrorMessageResourceName = "EMPTY_IBGECODE", ErrorMessageResourceType = typeof(APIMSG))]
        public string IBGECode { get; set; }
    }
}
