using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionsPatternDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly CompanyOptions _companyIOptions;
        private readonly CompanyOptions _companyIOptionsSnapshot;
        private readonly CompanyOptions _companyIOptionsMonitor;
        private string CompanyName = string.Empty;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;


        public WeatherForecastController(IOptions<CompanyOptions> companyIOptions,
            IOptionsSnapshot<CompanyOptions> companyIOptionsSnapshot,
            IOptionsMonitor<CompanyOptions> companyIOptionsMonitor,
            ILogger<WeatherForecastController> logger)
        {
            _companyIOptions = companyIOptions.Value;
            _companyIOptionsSnapshot = companyIOptionsSnapshot.Value;
            _companyIOptionsMonitor = companyIOptionsMonitor.CurrentValue;
            _logger = logger;

            //listen to onchange and call our private OnComapnyValueChange method
            companyIOptionsMonitor.OnChange(updatedsettings => OnComapnyValueChange());
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("[action]")]
        public IActionResult GetOptionsPattern()
        {
            var optionResponse = new
            {
                IOptions = _companyIOptions.Name,
                IOptionsSnapShot = _companyIOptionsSnapshot.Name,
                IOptionsMonitor = _companyIOptionsMonitor.Name,
                CreationDate = _companyIOptions.CreationDate
            };
            return Ok(optionResponse);
        }

        [HttpGet("[action]")]
        public IActionResult GetOptionsPatternWithOnchange()
        {
            var optionResponse = new
            {
                IOptions = _companyIOptions.Name,
                IOptionsSnapShot = _companyIOptionsSnapshot.Name,
                IOptionsMonitor = _companyIOptionsMonitor.Name,
                CreationDate = _companyIOptionsMonitor.CreationDate
            };
            CompanyName = _companyIOptionsMonitor.Name;
            Console.WriteLine(CompanyName);
            return Ok(optionResponse);
        }

        private void OnComapnyValueChange()
        {
            Console.WriteLine($"{CompanyName} is called from OnValueChange Method{_companyIOptionsMonitor.CreationDate}");
        }
    }
}
