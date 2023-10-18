using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut]
        [ProducesResponseType(typeof(IBGEResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateIBGE([FromServices] IUpdateIBGE updateIBGE, [FromBody] IBGEUpdateRequestJson ibgeUpdateRequestJson)
        {
            IBGEResponseJson ibgeResponseJson = await updateIBGE.Execute(ibgeUpdateRequestJson);

            return Ok(ibgeResponseJson);
        }
    }
}
