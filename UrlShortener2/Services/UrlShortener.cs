using System.Text;

namespace UrlShortener2.Services
{
    public class UrlShortener : IUrlShortener
    {
        public string ShortenUrl(string url)
        {
            // Create a SHA-256 hash of the original URL
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));

            // Convert hash to a Base64-like string with URL-safe characters
            var base64 = Convert.ToBase64String(hash)
                .Replace('+', '-') // Replace URL-unsafe characters with safe ones
                .Replace('/', '_')
                .Replace("=", "_")
                .Substring(0, 8); // Truncate to 8 characters for compactness

            return base64;
        }
    }
}
