using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class CampaignTasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
    public int TaskId { get; set; }
    public Tasks Tasks { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}