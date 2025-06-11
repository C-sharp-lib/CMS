namespace backend.Areas.Main.Models.ViewModels;

public class AddCompanyViewModel
{
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
}

public class UpdateCompanyViewModel
{
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
}

public class AddCompanyTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int CompanyId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

public class UpdateCompanyTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int CompanyId { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}

public class AddCompanyNoteViewModel
{
    public int CompanyId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

public class UpdateCompanyNoteViewModel
{
    public int CompanyId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}