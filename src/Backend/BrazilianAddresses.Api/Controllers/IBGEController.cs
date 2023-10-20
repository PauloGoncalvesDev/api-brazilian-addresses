using Microsoft.AspNetCore.Mvc;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Exceptions.ResourcesMessage;
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

        [HttpPut]
        [ProducesResponseType(typeof(IBGEResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateIBGE([FromServices] IUpdateIBGE updateIBGE, [FromBody] IBGEUpdateRequestJson ibgeUpdateRequestJson)
        {
            IBGEResponseJson ibgeResponseJson = await updateIBGE.Execute(ibgeUpdateRequestJson);

            return Ok(ibgeResponseJson);
        }

        [HttpGet("GetAllIBGEAddresses")]
        [ProducesResponseType(typeof(AddressResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAllIBGEAddresses([FromServices] IGetIBGEAddresses getIBGEAddresses, [FromQuery] PaginationBaseRequestJson paginationBaseRequestJson)
        {
            List<AddressResponseJson> addressResponseJsons = await getIBGEAddresses.Execute(paginationBaseRequestJson);

            return Ok(new { sucess = true, message = APIMSG.EXECUTION_SUCCESS_MSG, addressResponseJsons });
        }

        [HttpDelete]
        [ProducesResponseType(typeof(IBGEResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveIBGE([FromServices] IRemoveIBGE removeIBGE, [FromQuery] IBGERemoveRequestJson ibgeRemoveRequestJson)
        {
            IBGEResponseJson ibgeResponseJson = await removeIBGE.Execute(ibgeRemoveRequestJson.IBGECode);

            return Ok(ibgeResponseJson);
        }

        [HttpGet("GetIBGEAddressesByState")]
        [ProducesResponseType(typeof(AddressResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListIBGEAddressesByState([FromServices] IGetIBGEAddresses getIBGEAddresses, [FromQuery] AddressesByStateRequestJson addressesByStateRequestJson)
        {
            List<AddressResponseJson> addressResponseJsons = await getIBGEAddresses.Execute(addressesByStateRequestJson);

            return Ok(new { sucess = true, message = APIMSG.EXECUTION_SUCCESS_MSG, addressResponseJsons });
        }
    }
}