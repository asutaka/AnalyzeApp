using AnalyzeApp.API.Interface;
using AnalyzeApp.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzeApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SettingController : ControllerBase
    {
        private readonly IAnalyzeService _service;
        public SettingController(IAnalyzeService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var result = await _service.GetUser();
            return Ok(result);
        }

        [HttpPost(Name = "InsertUser")]
        public async Task<IActionResult> InsertUser([FromBody] UserModel model)
        {
            var result = await _service.InsertUser(model);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            var result = await _service.DeleteUser();
            return Ok(result);
        }

        [HttpGet(Name = "GetSettings")]
        public async Task<IActionResult> GetSettings()
        {
            var result = await _service.GetSettings();
            return Ok(result);
        }

        [HttpPost(Name = "InsertSetting")]
        public async Task<IActionResult> InsertSetting([FromBody] SettingModel model)
        {
            var result = await _service.InsertSetting(model);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateSetting")]
        public async Task<IActionResult> UpdateSetting([FromBody] SettingModel model)
        {
            var result = await _service.UpdateSetting(model);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteSetting")]
        public async Task<IActionResult> DeleteSetting(int id)
        {
            var result = await _service.DeleteSetting(id);
            return Ok(result);
        }

        [HttpGet(Name = "GetDataSettings")]
        public async Task<IActionResult> GetDataSettings()
        {
            var result = await _service.GetDataSettings();
            return Ok(result);
        }

        [HttpPost(Name = "InsertDataSettings")]
        public async Task<IActionResult> InsertDataSettings([FromBody] DataSettingModel model)
        {
            var result = await _service.InsertDataSettings(model);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateDataSettings")]
        public async Task<IActionResult> UpdateDataSettings([FromBody] DataSettingModel model)
        {
            var result = await _service.UpdateDataSettings(model);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteDataSettings")]
        public async Task<IActionResult> DeleteDataSettings(int id)
        {
            var result = await _service.DeleteDataSettings(id);
            return Ok(result);
        }
    }
}
