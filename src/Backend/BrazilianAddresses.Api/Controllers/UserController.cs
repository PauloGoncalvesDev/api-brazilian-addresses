using BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BrazilianAddresses.Api.Controllers
{
    public class UserController : BrazilianAddressesControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromServices] ICreateUser createUser, [FromBody] UserRequestJson userRequestJson)
        {
            BaseResponseJson userResponseJson = await createUser.Execute(userRequestJson);

            return Created(string.Empty, userResponseJson);
        }
    }
}
