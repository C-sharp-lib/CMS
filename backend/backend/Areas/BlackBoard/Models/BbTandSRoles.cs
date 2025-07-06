using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace backend.Areas.BlackBoard.Models;

public class BbTandSRoles : IdentityUserRole<string>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? TeacherId { get; set; }
    public virtual Teacher? Teacher { get; set; }
    public string? StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public string RoleId { get; set; }
    public virtual BbRoles Role { get; set; }
}