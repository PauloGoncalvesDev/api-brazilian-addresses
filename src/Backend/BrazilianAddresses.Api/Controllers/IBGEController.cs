using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BrazilianAddresses.Api.Controllers
{
    public class IBGEController : BrazilianAddressesControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(IBGEResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser([FromServices] ICreateIBGE createIBGE, [FromBody] IBGERequestJson ibgeRequestJson)
        {
            IBGEResponseJson ibgeResponseJson = await createIBGE.Execute(ibgeRequestJson);

            return Created(string.Empty, ibgeResponseJson);
        }
    }
}
