using Microsoft.EntityFrameworkCore;
using UrlShortener2.Data.Entities;
using UrlShortener2.Data.Repository.Interfaces;

namespace UrlShortener2.Data.Repository
{
    public class ShortUrlRepository(ApplicationDbContext context)
        : RepositoryBase<ShortUrl>(context), IShortUrlRepository
    {
        public async Task<ShortUrl?> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.OriginalUrlString == originalUrl);
        }

        public async Task<ShortUrl?> GetByShortUrlAsync(string shortenedUrl)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.ShortUrlString == shortenedUrl);
        }
    }
}
