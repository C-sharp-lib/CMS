namespace backend.Areas.Main.Models.Enums;

public enum Status
{
    // Pending, In Progress, Completed, Cancelled
    Pending,
    Approved,
    Rejected,
    Completed,
    Cancelled,
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