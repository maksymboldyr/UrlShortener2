using System.Text;

namespace UrlShortener2.Services
{
    public class UrlShortener : IUrlShortener
    {
        public string ShortenUrl(string url)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));

            // Convert hash to a Base64-like string with URL-safe characters
            var base64 = Convert.ToBase64String(hash)
                .Replace('+', '-')
                .Replace('/', '_')
                .Substring(0, 8); // Truncate to 8 characters for compactness

            return base64;
        }

        public string ExpandUrl(string shortUrl)
        {
            var base64 = shortUrl.Replace('-', '+').Replace('_', '/');
            var padding = new string('=', (4 - base64.Length % 4) % 4);
            var url = Encoding.UTF8.GetString(Convert.FromBase64String(base64 + padding));
            return url;
        }
    }
}
