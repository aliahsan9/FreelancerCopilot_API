using Microsoft.AspNetCore.Identity;

namespace FreelancerCopilot.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}