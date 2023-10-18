using System.ComponentModel.DataAnnotations;

namespace BrazilianAddresses.Communication.Requests
{
    public class PaginationBaseRequestJson
    {
        [Required]
        [MinLength(0)]
        public int PageIndex { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public int PageSize { get; set; }
    }
}