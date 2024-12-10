namespace UrlShortener2.Services
{
    public interface IUrlShortener
    {
        string ExpandUrl(string shortUrl);
        string ShortenUrl(string url);
    }
}