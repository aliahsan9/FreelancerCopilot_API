using Microsoft.AspNetCore.Identity;

namespace FreelancerCopilot.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public SubscriptionPlan Plan { get; set; }
           = SubscriptionPlan.Free;

        public DateTime SubscriptionStartDate { get; set; }
            = DateTime.UtcNow;
    }
}