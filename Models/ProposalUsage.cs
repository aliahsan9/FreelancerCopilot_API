namespace FreelancerCopilot.API.Models
{
    public class ProposalUsage
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int ProposalCount { get; set; }
    }
}