using System.ComponentModel.DataAnnotations;
using backend.Areas.Main.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Identity.Models.ViewModels;

public class RegisterViewModel : IdentityUser
{
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string UserName { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string City { get; set; }
    [Required] public string State { get; set; }
    [Required] public string ZipCode { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public DateTime DateCreated { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; }
}

public class LoginViewModel : IdentityUser
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public class UpdateUserViewModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string? Description { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    [FromForm]
    public IFormFile? ImageUrl { get; set; }
}

public class AddUserTasksViewModel
{
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; } 
    public Priority Priority { get; set; } 
    public string AssignedToUserId { get; set; } 
    public DateTime DateCreated { get; set; } = DateTime.Now;
}

public class UpdateUserTasksViewModel
{
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; } 
    public Priority Priority { get; set; } 
    public string AssignedToUserId { get; set; } 
    public DateTime DateUpdated { get; set; } = DateTime.Now;
    public DateTime DateCompleted {get; set;}
}
