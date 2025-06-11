using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using backend.Areas.Main.Models.Enums;

namespace backend.Areas.Main.Models;

public class Tasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; } 
    public Priority Priority { get; set; } 
    public string AssignedToUserId { get; set; } 
    [ForeignKey(nameof(AssignedToUserId))]
    public virtual User AssignedToUser { get; set; } 
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public DateTime? DateCompleted { get; set; } 
    public IEnumerable<TaskNotes>? TaskNotes { get; set; }
    public IEnumerable<JobTask>? JobTasks { get; set; }
    public IEnumerable<LeadTask>? LeadTasks { get; set; }
    public IEnumerable<CampaignTasks>? CampaignTasks { get; set; }
    public IEnumerable<CompanyTask>? CompanyTasks { get; set; }
    public IEnumerable<UserTasks>? UserTasks { get; set; }
    public IEnumerable<ContactTasks>? ContactTasks { get; set; }
    
}