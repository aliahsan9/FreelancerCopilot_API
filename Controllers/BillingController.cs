using FreelancerCopilot.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerCopilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BillingController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BillingController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("upgrade-to-pro")]
        public async Task<IActionResult> UpgradeToPro()
        {
            var user = await _userManager.GetUserAsync(User);

            user.Plan = SubscriptionPlan.Pro;

            await _userManager.UpdateAsync(user);

            return Ok("User upgraded to Pro");
        }
    }
}