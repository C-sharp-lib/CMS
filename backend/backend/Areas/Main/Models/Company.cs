using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Industry { get; set; }
    public string Website { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Fax { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public IEnumerable<Contact>? Contacts { get; set; }
    public IEnumerable<Tasks>? Tasks { get; set; }
    public IEnumerable<CompanyNotes>? CompanyNotes { get; set; }
    public virtual IEnumerable<CompanyTask>? CompanyTasks { get; set; }
}