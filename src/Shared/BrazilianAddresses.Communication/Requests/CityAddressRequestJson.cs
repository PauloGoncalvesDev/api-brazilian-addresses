using System.ComponentModel.DataAnnotations;
using BrazilianAddresses.Exceptions.ResourcesMessage;

namespace BrazilianAddresses.Communication.Requests
{
    public class CityAddressRequestJson
    {
        [Required(ErrorMessageResourceName = "EMPTY_CITY", ErrorMessageResourceType = typeof(APIMSG), AllowEmptyStrings = false)]
        [RegularExpression("[a-zA-Zá-úÁ-Úà-ùÀ-Ù-'\\s]+", ErrorMessageResourceName = "INVALID_CITY", ErrorMessageResourceType = typeof(APIMSG))]
        public string City { get; set; }
    }
}