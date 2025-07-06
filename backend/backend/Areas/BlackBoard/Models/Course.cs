using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.BlackBoard.Models;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }
    public int SyllabusId { get; set; }
    public virtual Syllabus Syllabus { get; set; }
    public virtual IEnumerable<StudentCourse>? StudentCourses { get; set; }
    public virtual IEnumerable<Module>? Modules { get; set; }
}