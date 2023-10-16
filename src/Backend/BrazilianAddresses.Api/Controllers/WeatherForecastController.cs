using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule;
using BrazilianAddresses.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BrazilianAddresses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromServices] ICreateIBGE createIBGE)
        {
            await createIBGE.Execute(new IBGERequestJson
            {
                City = "Russas",
                State = "Ceará",
                IBGECode = "12646453"
            });

            return Ok();
        }
    }
}