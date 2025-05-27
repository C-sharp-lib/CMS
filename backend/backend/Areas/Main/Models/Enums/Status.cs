namespace backend.Areas.Main.Models.Enums;

public enum Status
{
    // Pending, In Progress, Completed, Cancelled
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Completed = 4,
    Cancelled = 5,
}
public static class JobStatusExtensions
{
    public static string ToStringValue(this Status status)
    {
        return status switch
        {
            
            Status.Pending => "Pending",
            Status.Approved => "Approved",
            Status.Rejected => "Rejected",
            Status.Completed => "Completed",
            Status.Cancelled => "Cancelled",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}