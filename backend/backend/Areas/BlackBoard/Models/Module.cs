using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.BlackBoard.Models;

public class Module
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Directions { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual IEnumerable<Assignment>? Assignments { get; set; }
    public virtual IEnumerable<Quiz>? Quizzes { get; set; }
    public virtual IEnumerable<Exam>? Exams { get; set; }
    public virtual IEnumerable<Project>? Projects { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}