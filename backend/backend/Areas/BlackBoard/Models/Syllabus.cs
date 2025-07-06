using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.BlackBoard.Models;

public class Syllabus
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }
}