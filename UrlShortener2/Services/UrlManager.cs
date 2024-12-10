using Mapster;
using Microsoft.AspNetCore.Identity;
using UrlShortener2.Data.Entities;
using UrlShortener2.Data.Repository.Interfaces;
using UrlShortener2.Data.UnitOfWork;
using UrlShortener2.DTOs;

namespace UrlShortener2.Services
{
    public class UrlManager(
        UnitOfWork unitOfWork,
        IUrlShortener urlShortener,
        UserManager<IdentityUser> _userManager)
    {
        private readonly IShortUrlRepository _shortUrlRepository = unitOfWork.ShortenedUrlRepository;

        public async Task<UrlTableEntryDto> ShortenUrl(string originalUrl, string userId)
        {
            var existingShortenedUrl = await _shortUrlRepository.GetByOriginalUrlAsync(originalUrl);

            if (existingShortenedUrl != null)
            {
                return null;
            }

            var shortUrl = new ShortUrl
            {
                OriginalUrlString = originalUrl,
                ShortUrlString = urlShortener.ShortenUrl(originalUrl),
                UserId = userId
            };

            _shortUrlRepository.Add(shortUrl);

            return shortUrl.Adapt<UrlTableEntryDto>();
        }

        public async Task<ShortUrl?> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _shortUrlRepository.GetByOriginalUrlAsync(originalUrl);
        }

        public async Task<ShortUrl?> GetByShortUrlAsync(string shortenedUrl)
        {
            return await _shortUrlRepository.GetByShortUrlAsync(shortenedUrl);
        }

        public IEnumerable<ShortUrl> GetAllUrls()
        {
            return _shortUrlRepository.GetAll();
        }

        public async Task<string> GetOriginalUrl(string shortUrl)
        {
            var shortUrlEntity = await _shortUrlRepository.GetByShortUrlAsync(shortUrl);

            if (shortUrlEntity != null)
            {
                return shortUrlEntity.OriginalUrlString;
            }

            return string.Empty;
        }

        public async Task DeleteShortUrl(string shortUrl)
        {
            var urlEntity = await _shortUrlRepository.GetByShortUrlAsync(shortUrl);

            if (urlEntity == null)
            {
                return;
            }

            _shortUrlRepository.Delete(urlEntity.Id);
        }

        public void DeleteAllShortUrls()
        {
            var urls = _shortUrlRepository.GetAll();

            foreach (var url in urls)
            {
                _shortUrlRepository.Delete(url.Id);
            }
        }

        public async Task AddClick(string shortUrl)
        {
            var urlEntity = await _shortUrlRepository.GetByShortUrlAsync(shortUrl);

            if (urlEntity == null)
            {
                return;
            }

            urlEntity.Clicks++;
            _shortUrlRepository.Update(urlEntity);
        }

        public async Task<ShortUrlInfoDto> GetShortUrlInfo(string shortUrl)
        {
            var urlEntity = await _shortUrlRepository.GetByShortUrlAsync(shortUrl);

            if (urlEntity == null)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(urlEntity.UserId);

            if (user == null)
            {
                return null;
            }

            var urlInfo = urlEntity.Adapt<ShortUrlInfoDto>();

            urlInfo.UserEmail = user.Email;

            return urlInfo;
        }
    }
}
