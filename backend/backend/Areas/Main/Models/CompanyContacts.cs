using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class CompanyContacts
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
}