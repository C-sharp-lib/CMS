using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Communication.Models;
using backend.Areas.Identity.Models;


namespace backend.Areas.Main.Models;

public class Contact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string JobTitle { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string AddressLine1 { get; set; }
    
    public string? AddressLine2 { get; set; }
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string ZipCode { get; set; }
    
    public string Country { get; set; }

    public string? Notes { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }
    public string? ImageUrl { get; set; }
    // (Optional) Foreign key to User who created it
    
    public string? OwnerUserId { get; set; }
    [ForeignKey(nameof(OwnerUserId))]
    public virtual User? OwnerUser { get; set; }
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
    public IEnumerable<Job>? Jobs { get; set; }
    public virtual IEnumerable<Campaign>? Campaigns { get; set; }
    public virtual IEnumerable<Tasks>? Tasks { get; set; }
    public virtual IEnumerable<Meeting>? Meetings { get; set; }
    public virtual IEnumerable<ContactNotes>? ContactNotes { get; set; }
}