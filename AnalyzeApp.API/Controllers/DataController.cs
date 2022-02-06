using Microsoft.AspNetCore.Mvc;

namespace AnalyzeApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DataController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast2")]
        public IActionResult GetWeatherForecast2()
        {
            return Ok();
        }
    }
}
