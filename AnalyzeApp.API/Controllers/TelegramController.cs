using AnalyzeApp.API.Interface;
using AnalyzeApp.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzeApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TelegramController : ControllerBase
    {
        private readonly IAnalyzeService _service;
        public TelegramController(IAnalyzeService service)
        {
            _service = service;
        }
        [HttpPost(Name = "SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] NotifyModel model)
        {
            var phoneUser = model.Phone.PhoneFormat();
            if (string.IsNullOrWhiteSpace(phoneUser))
                return BadRequest(-1);
            model.Phone = phoneUser;
            var result = await _service.InsertNotify(model);
            return Ok(result);
        }
        [HttpPost(Name = "Verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyModel model)
        {
            var result = await _service.Verify(model);
            return Ok(result);
        }

        [HttpPost(Name = "GetNotify")]
        public async Task<IActionResult> GetNotify(int top)
        {
            var result = await _service.GetNotify(top);
            return Ok(result);
        }
    }
}
