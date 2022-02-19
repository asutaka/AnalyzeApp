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

        [HttpGet(Name = "GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _service.GetProfile();
            return Ok(result);
        }

        [HttpPost(Name = "InsertProfile")]
        public async Task<IActionResult> InsertProfile([FromBody] ProfileModel model)
        {
            var result = await _service.InsertProfile(model);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteProfile")]
        public async Task<IActionResult> DeleteProfile()
        {
            var result = await _service.DeleteProfile();
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

        [HttpGet(Name = "GetConfigTable")]
        public async Task<IActionResult> GetConfigTable()
        {
            var result = await _service.GetConfigTable();
            return Ok(result);
        }

        [HttpPost(Name = "UpdateConfigTable")]
        public async Task<IActionResult> UpdateConfigTable([FromBody] ConfigTableModel model)
        {
            var result = await _service.UpdateConfigTable(model);
            return Ok(result);
        }
    }
}
