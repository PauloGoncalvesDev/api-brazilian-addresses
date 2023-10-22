using System.ComponentModel.DataAnnotations;
using BrazilianAddresses.Exceptions.ResourcesMessage;

namespace BrazilianAddresses.Communication.Requests
{
    public class PaginationBaseRequestJson
    {
        [Required(ErrorMessageResourceName = "EMPTY_PAGEINDEX", ErrorMessageResourceType = typeof(APIMSG))]
        [Range(0, int.MaxValue, ErrorMessageResourceName = "INVALID_PAGEINDEX", ErrorMessageResourceType = typeof(APIMSG))]
        public int PageIndex { get; set; }

        [Required(ErrorMessageResourceName = "EMPTY_PAGESIZE", ErrorMessageResourceType = typeof(APIMSG))]
        [Range(10, 50, ErrorMessageResourceName = "INVALID_PAGESIZE", ErrorMessageResourceType = typeof(APIMSG))]
        public int PageSize { get; set; }
    }
}