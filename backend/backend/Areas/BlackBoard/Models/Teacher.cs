using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace backend.Areas.BlackBoard.Models;

public class Teacher : IdentityUser
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public virtual IEnumerable<BbTandSRoles>? Roles { get; set; }
    public virtual IEnumerable<Exam>? Exams { get; set; }
    public virtual IEnumerable<Quiz>? Quizzes { get; set; }
    public virtual IEnumerable<Assignment>? Assignments { get; set; }
    public virtual IEnumerable<Project>? Projects { get; set; }
    public virtual IEnumerable<Course>? Courses { get; set; }
    public virtual IEnumerable<TeacherStudents>? Students { get; set; }
}