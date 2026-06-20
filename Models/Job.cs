using FreelancerCopilot.API.Models;

public class Job
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string UserId { get; set; }
    public decimal Budget { get; set; }
    public DateTime CreatedAt { get; set; }

    public ApplicationUser User { get; set; }

    public ICollection<Proposal> Proposals { get; set; }
}