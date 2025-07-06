using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.BlackBoard.Models;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Directions { get; set; }
    public string StudentId { get; set; }
    public virtual Student Student { get; set; }
    public string TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }
    public int ModuleId { get; set; }
    public virtual Module Module { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Precision(10,2)]
    public decimal Weight { get; set; }
    [Precision(10,2)]
    public decimal Grade { get; set; }
    public IEnumerable<Question>? Questions { get; set; }
}