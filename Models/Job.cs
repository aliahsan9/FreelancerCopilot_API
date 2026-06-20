namespace FreelancerCopilot.API.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Budget { get; set; }
        public string Source { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 🔑 IMPORTANT: SaaS ownership
        public string UserId { get; set; }
    }
}