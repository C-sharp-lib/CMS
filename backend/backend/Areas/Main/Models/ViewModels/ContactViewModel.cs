using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using Microsoft.AspNetCore.Mvc;


namespace backend.Areas.Main.Models.ViewModels;

public class AddContactViewModel
{
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

    public DateTime DateCreated { get; set; } = DateTime.Now;
    
    public string? OwnerUserId { get; set; }
    [FromForm]
    public IFormFile? ImageUrl { get; set; }
    public int? CompanyId { get; set; }
}

public class UpdateContactViewModel
{

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string JobTitle { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string AddressLine1 { get; set; }
    
    public string AddressLine2 { get; set; }
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string ZipCode { get; set; }
    
    public string Country { get; set; }

    public string Notes { get; set; }

    public DateTime? DateUpdated { get; set; } = DateTime.Now;
    [FromForm]
    public IFormFile? ImageUrl { get; set; }
    public int? CompanyId { get; set; }
}

public class AddContactNoteViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public int ContactId { get; set; }
}

public class UpdateContactNoteViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}