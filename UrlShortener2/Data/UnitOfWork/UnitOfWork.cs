using UrlShortener2.Data.Entities;
using UrlShortener2.Data.Repository;
using UrlShortener2.Data.Repository.Interfaces;

namespace UrlShortener2.Data.UnitOfWork
{
    public class UnitOfWork(ApplicationDbContext context)
    {
        private IShortUrlRepository? _shortenedUrlRepository;

        private IRepository<AboutEntry>? _aboutEntryRepository;

        public IShortUrlRepository ShortenedUrlRepository
        {
            get
            {
                _shortenedUrlRepository ??= new ShortUrlRepository(context);

                return _shortenedUrlRepository;
            }
        }

        public IRepository<AboutEntry> AboutEntryRepository
        {
            get
            {
                _aboutEntryRepository ??= new RepositoryBase<AboutEntry>(context);

                return _aboutEntryRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
