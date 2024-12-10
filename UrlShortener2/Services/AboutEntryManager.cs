using Mapster;
using UrlShortener2.Data.Entities;
using UrlShortener2.Data.Repository.Interfaces;
using UrlShortener2.Data.UnitOfWork;
using UrlShortener2.DTOs;

namespace UrlShortener2.Services
{
    public class AboutEntryManager
    {
        private readonly IRepository<AboutEntry> _aboutEntryRepository;

        public AboutEntryManager(UnitOfWork unitOfWork)
        {
            _aboutEntryRepository = unitOfWork.AboutEntryRepository;
        }

        public void AddAboutEntry(AboutEntryDto aboutEntry)
        {
            _aboutEntryRepository.Add(aboutEntry.Adapt<AboutEntry>());
        }

        public void UpdateAboutEntry(AboutEntryDto aboutEntry)
        {
            var existingAboutEntry = _aboutEntryRepository.GetById(aboutEntry.Id);
            if (existingAboutEntry != null)
            {
                existingAboutEntry.Title = aboutEntry.Title;
                existingAboutEntry.Content = aboutEntry.Content;
                _aboutEntryRepository.Update(existingAboutEntry);
            }
        }

        public AboutEntryDto GetAboutEntry()
        {
            var aboutEntry = _aboutEntryRepository.GetAll().FirstOrDefault();
            var aboutEntryDto = aboutEntry?.Adapt<AboutEntryDto>();

            if (aboutEntryDto == null)
            {
                aboutEntryDto = new AboutEntryDto
                {
                    Title = "About",
                    Content = "This is a simple URL shortener application."
                };

                _aboutEntryRepository.Add(aboutEntryDto.Adapt<AboutEntry>());
            }

            return aboutEntryDto;
        }
    }
}
