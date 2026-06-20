using FreelancerCopilot.API.Models;

public class Proposal
{
    public int Id { get; set; }

    public int JobId { get; set; }
    public Job Job { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }
}