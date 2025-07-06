using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.BlackBoard.Models;

public class Question
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ExamId { get; set; }
    public virtual Exam? Exam { get; set; }
    public int? AssignmentId { get; set; }
    public virtual Assignment? Assignment { get; set; }
    public int? QuizId { get; set; }
    public virtual Quiz? Quiz { get; set; }
    public int? ProjectId { get; set; }
    public virtual Project? Project { get; set; }
    public virtual IEnumerable<Choice>? Choices { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}