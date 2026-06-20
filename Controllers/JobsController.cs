using FreelancerCopilot.API.Data;
using FreelancerCopilot.API.Helpers;
using FreelancerCopilot.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelancerCopilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = UserHelper.GetUserId(User);

            var jobs = await _context.Jobs
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(jobs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            var userId = UserHelper.GetUserId(User);

            job.UserId = userId;
            job.CreatedAt = DateTime.Now;

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return Ok(job);
        }
    }
}