using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionsPatternDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly MySettings _settings;
        private readonly IOptionsSnapshot<MySettings> _setting1;


        public MyController(IOptions<MySettings> options, IOptionsSnapshot<MySettings> setting1)
        {
            _settings = options.Value;
            _setting1 = setting1;
        }

        [HttpGet("options")]
        public IActionResult GetIOptionsSetting()
        {
            var mySettingValue = _settings.MySetting;
            return Ok(mySettingValue);
        }

        [HttpGet("optionsnapshot")]
        public IActionResult GetIOptionsSnapshotSetting()
        {
            var setting = _setting1.Value.MySetting;
            return Ok(setting);
        }


    }
}
