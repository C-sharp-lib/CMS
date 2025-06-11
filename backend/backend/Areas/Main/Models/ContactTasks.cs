using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class ContactTasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
    public int TaskId { get; set; }
    public Tasks Tasks { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}