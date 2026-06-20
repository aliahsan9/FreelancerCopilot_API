namespace FreelancerCopilot.API.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}