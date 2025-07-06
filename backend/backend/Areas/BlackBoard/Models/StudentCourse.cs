using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.BlackBoard.Models;

public class StudentCourse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string StudentId { get; set; }
    public virtual Student Student { get; set; }
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }
}