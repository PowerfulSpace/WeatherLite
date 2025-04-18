using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PS.WeatherLite.Web.Models;
using PS.WeatherLite.Web.Services.Interfaces;

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
        private readonly ILinkShortenerService _linkShortener;
        private readonly string _baseUrl;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ILinkShortenerService linkShortener,
            IOptions<ShortenerSettings> options
            )
        {
            _logger = logger;
            _linkShortener = linkShortener;
            _baseUrl = options.Value.BaseUrl.TrimEnd('/');
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




        /// <summary>
        /// POST /link/shorten
        /// </summary>
        [HttpPost("shorten")]
        public IActionResult Shorten([FromBody] LinkRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.LongUrl))
                return BadRequest("Long URL is required.");

            var shortUrl = _linkShortener.Shorten(request.LongUrl);

            return Ok(new LinkResponse { ShortUrl = shortUrl });
        }

        /// <summary>
        /// GET /link/expand?shortUrl=...
        /// </summary>
        [HttpGet("expand")]
        public IActionResult Expand([FromQuery] string shortUrl)
        {
            var longUrl = _linkShortener.Expand(shortUrl);

            if (longUrl == null)
                return NotFound("Short URL not found.");

            return Ok(new { LongUrl = longUrl });
        }

        /// <summary>
        /// GET /{code}
        /// </summary>
        [HttpGet("/r/{code}")]
        public IActionResult RedirectToOriginal(string code)
        {
            var fullShortUrl = $"{_baseUrl}/r/{code}";
            var longUrl = _linkShortener.Expand(fullShortUrl);

            if (longUrl == null)
                return NotFound("Link not found.");

            return Redirect(longUrl);
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
