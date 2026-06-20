using FreelancerCopilot.API.Data;
using FreelancerCopilot.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelancerCopilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProposalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProposalsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetByJob(int jobId)
        {
            var proposals = await _context.Proposals
                .Where(x => x.JobId == jobId)
                .ToListAsync();

            return Ok(proposals);
        }

        [HttpPost("generate/{jobId}")]
        public async Task<IActionResult> Generate(int jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);

            if (job == null)
                return NotFound("Job not found");

            // 🧠 SIMPLE TEMPLATE GENERATION (MVP)
            var proposalText =
$@"
Hello,

I carefully reviewed your job post: {job.Title}

I have experience in building scalable web applications using ASP.NET Core and Angular.

I can help you deliver this project with:
- Clean architecture
- Scalable backend
- Secure API design

Let’s discuss your requirements in detail.

Best Regards,
Freelancer Copilot User
";

            var proposal = new Proposal
            {
                JobId = jobId,
                Content = proposalText,
                CreatedAt = DateTime.Now
            };

            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();

            return Ok(proposal);
        }
    }
}