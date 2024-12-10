using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener2.DTOs;
using UrlShortener2.Services;

namespace UrlShortener2.Pages
{
    public class AboutModel(
        AboutEntryManager aboutEntryManager,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        : PageModel
    {
        public string Title { get; set; } = string.Empty;
        public string PageContent { get; set; } = string.Empty;
        public List<string> RolesList { get; set; } = new List<string>();
        public bool IsAdmin { get; set; } = false;

        [BindProperty]
        public string InputTitle { get; set; } = string.Empty;

        [BindProperty]
        public string InputContent { get; set; } = string.Empty;

        public async void OnGet()
        {
            var aboutEntry = aboutEntryManager.GetAboutEntry();
            Title = aboutEntry.Title;
            PageContent = aboutEntry.Content;

            if (signInManager.IsSignedIn(User))
            {
                var user = await userManager.GetUserAsync(User);

                if (user == null)
                {
                    return;
                }

                var roles = await userManager.GetRolesAsync(user);

                RolesList = roles.ToList();

                IsAdmin = roles.Contains("ADMIN");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var aboutEntry = aboutEntryManager.GetAboutEntry();

            if (aboutEntry == null)
            {
                aboutEntry = new AboutEntryDto
                {
                    Title = InputTitle,
                    Content = InputContent
                };
                aboutEntryManager.AddAboutEntry(aboutEntry);
            }
            else
            {
                aboutEntry.Title = InputTitle;
                aboutEntry.Content = InputContent;
                aboutEntryManager.UpdateAboutEntry(aboutEntry);
            }

            return RedirectToPage();
        }
    }
}
