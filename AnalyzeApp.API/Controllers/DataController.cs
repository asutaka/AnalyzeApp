using Binance.Net;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

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
