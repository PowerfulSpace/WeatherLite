using PS.WeatherLite.Web.Services.Interfaces;
using PS.WeatherLite.Web.Storage.Interfaces;

namespace PS.WeatherLite.Web.Services
{
    public class LinkShortenerService : ILinkShortenerService
    {
        private readonly ILinkStorage _storage;
        const string BaseUrl = "https://localhost:8081/";

        public LinkShortenerService(ILinkStorage storage)
        {
            _storage = storage;
        }

        public string Shorten(string longUrl)
        {
            string code;
            string shortLink;

            do
            {
                code = GenerateShortCode();
                shortLink = $"{BaseUrl}{code}";
            }
            while (_storage.TryGet(shortLink, out _));

            _storage.Save(shortLink, longUrl);

            return shortLink;
        }

        public string? Expand(string shortUrl)
        {
            _storage.TryGet(shortUrl, out var longUrl);
            return longUrl;
        }

        private string GenerateShortCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }
    }
}
