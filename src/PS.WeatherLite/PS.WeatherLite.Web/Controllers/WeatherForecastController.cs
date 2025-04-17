using Microsoft.AspNetCore.Mvc;

namespace PS.WeatherLite.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return GenerateForecasts(5);
        }

        [HttpGet("one")]
        public WeatherForecast GetOne()
        {
            return GenerateForecasts(1).First();
        }

        [HttpGet("three")]
        public IEnumerable<WeatherForecast> GetThree()
        {
            return GenerateForecasts(3);
        }

        private static IEnumerable<WeatherForecast> GenerateForecasts(int count)
        {
            return Enumerable.Range(1, count).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
        }
    }
}
