using Microsoft.AspNetCore.Mvc;

namespace AnalyzeApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AnalyzeController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast7")]
        public IActionResult GetWeatherForecast7()
        {
            return Ok();
        }
    }
}
