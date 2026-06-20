namespace FreelancerCopilot.API.Models
{
    public class Lead
    {
        public int Id { get; set; }

        public string ClientName { get; set; }
        public string Status { get; set; }

        public int JobId { get; set; }

        public string UserId { get; set; }
    }
}