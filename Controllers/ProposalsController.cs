using FreelancerCopilot.API.Data;
using FreelancerCopilot.API.Helpers;
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

        public ProposalsController(
            AppDbContext context,
            AiProposalService aiService)
        {
            _context = context;
            _aiService = aiService;
        }

        [HttpPost("generate/{jobId}")]
        public async Task<IActionResult> Generate(int jobId)
        {
            try
            {
                var userId = UserHelper.GetUserId(User);

                var job = await _context.Jobs
                    .FirstOrDefaultAsync(x =>
                        x.Id == jobId &&
                        x.UserId == userId);

                if (job == null)
                    return NotFound("Job not found.");

                var generatedProposal =
                    await _aiService.GenerateProposal(job);

                var proposal = new Proposal
                {
                    JobId = job.Id,
                    UserId = userId,
                    Content = generatedProposal,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Proposals.Add(proposal);

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    proposal.Id,
                    proposal.Content,
                    proposal.CreatedAt
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProposals()
        {
            var userId = UserHelper.GetUserId(User);

            var proposals = await _context.Proposals
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(proposals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = UserHelper.GetUserId(User);

            var proposal = await _context.Proposals
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.UserId == userId);

            if (proposal == null)
                return NotFound();

            return Ok(proposal);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = UserHelper.GetUserId(User);

            var proposal = await _context.Proposals
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.UserId == userId);

            if (proposal == null)
                return NotFound();

            _context.Proposals.Remove(proposal);

            await _context.SaveChangesAsync();

            return Ok("Proposal deleted.");
        }
    }
}