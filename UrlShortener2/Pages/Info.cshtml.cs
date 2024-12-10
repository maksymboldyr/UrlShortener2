using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener2.Services;

namespace UrlShortener2.Pages
{
    public class InfoModel(UrlManager urlManager) : PageModel
    {
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public int Clicks { get; set; }
        public string CreatedAt { get; set; }
        public string Email { get; set; }


        public async void OnGet()
        {
            var url = await urlManager.GetShortUrlInfo(Request.Query["shortUrl"]);

            if (url == null)
            {
                Response.StatusCode = 404;
                return;
            }

            ShortUrl = url.ShortUrl;
            OriginalUrl = url.OriginalUrl;
            Clicks = url.Clicks;
            CreatedAt = url.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
            Email = url.UserEmail;
        }
    }
}
