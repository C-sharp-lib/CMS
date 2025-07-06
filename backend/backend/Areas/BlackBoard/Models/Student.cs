using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.BlackBoard.Models;

public class Student : IdentityUser
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Precision(10,2)]
    public decimal GPA { get; set; }
    public virtual IEnumerable<BbTandSRoles> Roles { get; set; }
    public virtual IEnumerable<Quiz>? Quizzes { get; set; }
    public virtual IEnumerable<Exam>? Exams { get; set; }
    public virtual IEnumerable<Assignment>? Assignments { get; set; }
    public virtual IEnumerable<Project>? Projects { get; set; }
    public virtual IEnumerable<StudentCourse>? StudentCourses { get; set; }
    public virtual IEnumerable<TeacherStudents>? Teachers { get; set; }
}