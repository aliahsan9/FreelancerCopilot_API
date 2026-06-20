using FreelancerCopilot.API.Data;
using FreelancerCopilot.API.Models;
using FreelancerCopilot.API.Services;
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
        private readonly AiProposalService _aiService;

        public ProposalsController(AppDbContext context, AiProposalService aiService)
        {
            _context = context;
            _aiService = aiService;
        }

        [HttpPost("generate/{jobId}")]
        public async Task<IActionResult> Generate(int jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);

            if (job == null)
                return NotFound("Job not found");

            // 🤖 AI GENERATION
            var aiText = await _aiService.GenerateProposal(job);

            var proposal = new Proposal
            {
                JobId = jobId,
                Content = aiText,
                CreatedAt = DateTime.Now
            };

            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();

            return Ok(proposal);
        }
    }
}