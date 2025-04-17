namespace PS.WeatherLite.Web.Services.Interfaces
{
    public interface ILinkShortenerService
    {
        string Shorten(string longUrl);

        string? Expand(string shortUrl);
    }
}
