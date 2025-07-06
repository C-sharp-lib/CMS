using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.BlackBoard.Models;

public class TeacherStudents
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }
    public string StudentId { get; set; }
    public virtual Student Student { get; set; }
}