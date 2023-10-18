using Microsoft.AspNetCore.Mvc;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;

namespace BrazilianAddresses.Api.Controllers
{
    public class IBGEController : BrazilianAddressesControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(IBGEResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateIBGE([FromServices] ICreateIBGE createIBGE, [FromBody] IBGERequestJson ibgeRequestJson)
        {
            IBGEResponseJson ibgeResponseJson = await createIBGE.Execute(ibgeRequestJson);

            return Created(string.Empty, ibgeResponseJson);
        }

        [HttpGet("ListAllIBGEAddress")]
        public async Task<IActionResult> ListAllIBGEAddress([FromServices] IListAllIBGEAddress listAllIBGEAddress, [FromQuery] PaginationBaseRequestJson paginationBaseRequestJson)
        {
            List<IBGEResponseJson> ibgeResponseJsons = await listAllIBGEAddress.Execute(paginationBaseRequestJson);

            return Ok(new { sucess = true, message = "apenas um teste", ibgeResponseJsons });
        }
    }
}