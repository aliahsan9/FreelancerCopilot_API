namespace FreelancerCopilot.API.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Budget { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty; // Upwork/Fiverr etc
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}