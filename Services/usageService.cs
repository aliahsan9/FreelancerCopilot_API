using FreelancerCopilot.API.Data;
using FreelancerCopilot.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelancerCopilot.API.Services
{
    public class UsageService
    {
        private readonly AppDbContext _context;

        public UsageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CanGenerateProposal(
            ApplicationUser user)
        {
            var now = DateTime.UtcNow;

            var usage = await _context.ProposalUsages
                .FirstOrDefaultAsync(x =>
                    x.UserId == user.Id &&
                    x.Month == now.Month &&
                    x.Year == now.Year);

            var currentCount = usage?.ProposalCount ?? 0;

            var limit = user.Plan switch
            {
                SubscriptionPlan.Free => 5,
                SubscriptionPlan.Pro => 100,
                SubscriptionPlan.Agency => 1000,
                _ => 5
            };

            return currentCount < limit;
        }

        public async Task IncrementUsage(string userId)
        {
            var now = DateTime.UtcNow;

            var usage = await _context.ProposalUsages
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.Month == now.Month &&
                    x.Year == now.Year);

            if (usage == null)
            {
                usage = new ProposalUsage
                {
                    UserId = userId,
                    Month = now.Month,
                    Year = now.Year,
                    ProposalCount = 1
                };

                _context.ProposalUsages.Add(usage);
            }
            else
            {
                usage.ProposalCount++;
            }

            await _context.SaveChangesAsync();
        }
    }
}