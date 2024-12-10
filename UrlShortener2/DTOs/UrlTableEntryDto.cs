namespace UrlShortener2.DTOs
{
    public class UrlTableEntryDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string OriginalUrlString { get; set; } = string.Empty;
        public string ShortUrlString { get; set; } = string.Empty;
    }
}
