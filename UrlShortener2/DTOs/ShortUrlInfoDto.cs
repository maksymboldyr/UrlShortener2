namespace UrlShortener2.DTOs
{
    public class ShortUrlInfoDto
    {
        public string? ShortUrl { get; set; }
        public string? OriginalUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Clicks { get; set; }
        public string? UserEmail { get; set; }
    }
}
