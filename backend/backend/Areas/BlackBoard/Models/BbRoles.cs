using Microsoft.AspNetCore.Identity;

namespace backend.Areas.BlackBoard.Models;

public class BbRoles : IdentityRole
{
    public string? Description { get; set; }
    public virtual IEnumerable<BbTandSRoles>? BbUserRoles { get; set; }
}