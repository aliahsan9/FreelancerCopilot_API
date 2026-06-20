using System.Security.Claims;

namespace FreelancerCopilot.API.Helpers
{
    public static class UserHelper
    {
        public static string GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}