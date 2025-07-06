using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Areas.BlackBoard.Models.ViewModels;

public class AddTeacherViewModel
{
    
}

public class UpdateTeacherViewModel
{
    
}

public class TeacherRegisterViewModel : IdentityUser
{
    [Required] public string FirstName { get; set; }
    [Required] public string MiddleName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string PhoneNumber { get; set; }
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

public class TeacherLoginViewModel : IdentityUser
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}