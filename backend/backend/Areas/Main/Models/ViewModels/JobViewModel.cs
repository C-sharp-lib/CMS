

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using backend.Areas.Main.Models.Enums;

namespace backend.Areas.Main.Models.ViewModels;

public class AddJobViewModel
{
    public string Title { get; set; }
    public string? Description { get; set; }
    
    public Status Status { get; set; }

    public Priority Priority { get; set; }

    public DateTime ScheduledDate { get; set; }
    

    [Column(TypeName = "decimal(18,2)")]
    public decimal EstimatedCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? ActualCost { get; set; }

    public string? Notes { get; set; }

    public DateTime DateCreated { get; set; }
    public int ContactId { get; set; }
    public string AssignedUserId { get; set; }
    public string CreatedByUserId { get; set; }
}

public class UpdateJobViewModel
{
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public Status Status { get; set; }
    
    public Priority Priority { get; set; }

    public DateTime ScheduledDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal EstimatedCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? ActualCost { get; set; }

    public string? Notes { get; set; }

    public DateTime? DateUpdated { get; set; } = DateTime.Now;
}

