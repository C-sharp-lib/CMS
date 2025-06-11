using backend.Areas.Identity.Models;
using backend.Areas.Main.Models.Enums;

namespace backend.Areas.Main.Models.ViewModels;

public class AddCampaignTaskViewModel
{
    public int CampaignId { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; } 
    public Priority Priority { get; set; } 
    public User AssignedToUserId { get; set; } 
    public DateTime DateCreated { get; set; } = DateTime.Now;
}

public class UpdateCampaignTaskViewModel
{
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; } 
    public Priority Priority { get; set; } 
    public User AssignedToUserId { get; set; } 
    public DateTime DateUpdated { get; set; } = DateTime.Now;
    public DateTime DateCompleted {get; set;}
}

public class AddCampaignNoteViewModel
{
    public int CampaignId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}
public class UpdateCampaignNoteViewModel
{
    public int CampaignId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}