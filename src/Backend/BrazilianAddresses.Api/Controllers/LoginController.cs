using BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BrazilianAddresses.Api.Controllers
{
    public class LoginController : BrazilianAddressesControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserLoginResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser([FromServices] ILoginUser loginUser, [FromBody] UserLoginRequestJson userLoginRequestJson)
        {
            UserLoginResponseJson userLoginResponseJson = await loginUser.Execute(userLoginRequestJson);

            return Ok(userLoginResponseJson);
        }
    }
}