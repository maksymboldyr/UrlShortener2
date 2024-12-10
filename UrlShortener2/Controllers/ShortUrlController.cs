using Microsoft.AspNetCore.Mvc;
using UrlShortener2.Services;

namespace UrlShortener2.Controllers
{

    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly UrlManager urlManager;

        public ShortUrlController(UrlManager urlShortener)
        {
            urlManager = urlShortener;
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public async Task<IActionResult> RedirectTo(string shortUrl)
        {
            var url = await urlManager.GetOriginalUrl(shortUrl);

            if (url == null)
            {
                return NotFound();
            }

            await urlManager.AddClick(shortUrl);

            return Redirect(url);
        }

        [HttpGet]
        [Route("{shortUrl}/Info")]
        public async Task<IActionResult> GetInfo(string shortUrl)
        {
            var url = await urlManager.GetByShortUrlAsync(shortUrl);

            if (url == null)
            {
                return NotFound();
            }

            return RedirectToPage("/Info", new { shortUrl });
        }

    }
}
