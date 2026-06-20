namespace FreelancerCopilot.API.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // New, Contacted, Won, Lost
        public int JobId { get; set; }
    }
}