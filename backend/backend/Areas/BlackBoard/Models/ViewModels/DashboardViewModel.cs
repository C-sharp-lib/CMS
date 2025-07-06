using backend.Areas.Identity.Models;

namespace backend.Areas.BlackBoard.Models.ViewModels;

public class DashboardViewModel
{
    public int? TeachCount {get; set;}
    public int? StudentCount {get; set;}
    public int? CourseCount {get; set;}
    public int? StudentCourseCount {get; set;}
    public int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public int? StudentId { get; set; }
    public Student? Student { get; set; }
    public virtual IEnumerator<Teacher>? Teachers { get; set; }
    public virtual IEnumerator<Student>? Students { get; set; }
    public virtual IEnumerator<Course>? Courses { get; set; }
    public virtual IEnumerator<Syllabus>? Syllabus { get; set; }
}