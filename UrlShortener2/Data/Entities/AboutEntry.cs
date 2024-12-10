namespace UrlShortener2.Data.Entities
{
    public class AboutEntry : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
