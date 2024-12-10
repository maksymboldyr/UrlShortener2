using UrlShortener2.Data.Entities;

namespace UrlShortener2.Data.Repository.Interfaces
{
    public interface IShortUrlRepository : IRepository<ShortUrl>
    {
        Task<ShortUrl?> GetByOriginalUrlAsync(string originalUrl);
        Task<ShortUrl?> GetByShortUrlAsync(string shortenedUrl);
    }
}