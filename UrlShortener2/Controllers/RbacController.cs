using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RbacController(UserManager<IdentityUser> userManager)
        : ControllerBase
    {
        private readonly string guestRoleName = "ANONYMOUS";

        [HttpGet]
        [Route("CurrentRoles")]
        public async Task<IActionResult> GetCurrentRole()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Ok(new List<string> { guestRoleName });
            }

            var roles = await userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        [HttpGet]
        [Route("CurrentUserId")]
        public async Task<IActionResult> GetCurrentUserId()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Ok("Not authorized.");
            }

            return Ok(user.Id);
        }

    }
}
