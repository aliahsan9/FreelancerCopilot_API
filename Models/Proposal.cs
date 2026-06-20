namespace FreelancerCopilot.API.Models
{
    public class Proposal
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 🔑 ownership
        public string UserId { get; set; }
    }
}