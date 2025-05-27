namespace backend.Areas.Main.Models.Enums;

public enum Priority
{
    // Low, Normal, High, Urgent
    Low = 1,
    Normal = 2,
    High = 3,
    Urgent = 4
}

public static class JobPriorityExtensions
{
    public static string ToStringValue(this Priority priority)
    {
        return priority switch
        {
            
            Priority.Low => "Low",
            Priority.Normal => "Normal",
            Priority.High => "High",
            Priority.Urgent => "Urgent",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}