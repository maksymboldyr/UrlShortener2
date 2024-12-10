using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortener2.DTOs;
using UrlShortener2.Services;

namespace UrlShortener2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController(UrlManager urlManager) : ControllerBase
    {
        [HttpGet]
        [Route("All")]
        public IActionResult GetAllUrls()
        {
            return Ok(urlManager.GetAllUrls());
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] ShortenUrlRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OriginalUrl))
            {
                return BadRequest("The originalUrl field is required.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                userId = Guid.NewGuid().ToString();
            }

            var shortUrl = await urlManager.ShortenUrl(request.OriginalUrl, userId);

            return Ok(shortUrl);
        }

        [HttpDelete("{shortUrl}")]
        [Authorize]
        public async Task<IActionResult> DeleteUrl(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                return BadRequest("The shortUrl field is required.");
            }

            await urlManager.DeleteShortUrl(shortUrl);

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllUrls()
        {
            urlManager.DeleteAllShortUrls();

            return Ok();
        }

    }
}
