public class Subscription
{
    public int Id { get; set; }
    public string UserId { get; set; }

    public string PlanName { get; set; } // Free, Pro
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }
}