using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
    {
        return await _context.Companies
            .Include(x => x.CompanyContacts)!
            .ThenInclude(x => x.Contact)
            .Include(x => x.CompanyContacts)!
            .ThenInclude(x => x.Company)
            .Include(x => x.CompanyTasks)!
            .ThenInclude(x => x.Tasks)
            .Include(x => x.CompanyTasks)!
            .ThenInclude(x => x.Company)
            .Include(x => x.CompanyNotes)!
            .ThenInclude(x => x.Note)
            .ToListAsync();
    }

    public async Task<Company> GetCompanyById(int id)
    {
        var company = await _context.Companies
            .Include(x => x.CompanyContacts)!
            .ThenInclude(x => x.Contact)
            .Include(x => x.CompanyContacts)!
            .ThenInclude(x => x.Company)
            .Include(x => x.CompanyTasks)!
            .ThenInclude(x => x.Tasks)
            .Include(x => x.CompanyTasks)!
            .ThenInclude(x => x.Company)
            .Include(x => x.CompanyNotes)!
            .ThenInclude(x => x.Note)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (company is null) return null;
        return company;
    }

    public async Task<Company> AddAsync([FromBody] AddCompanyViewModel company)
    {
        var companies = new Company
        {
            Name = company.Name,
            Industry = company.Industry,
            Website = company.Website,
            Address = company.Address,
            City = company.City,
            State = company.State,
            ZipCode = company.ZipCode,
            Country = company.Country,
            PhoneNumber = company.PhoneNumber,
            Fax = company.Fax,
            Description = company.Description,
            DateCreated = company.DateCreated,
        };
        _context.Companies.Add(companies);
        await _context.SaveChangesAsync();
        return companies;
    }

    public async Task UpdateAsync(int id, [FromBody] UpdateCompanyViewModel company)
    {
        var companyToUpdate = await GetCompanyById(id);
        companyToUpdate.Name = company.Name;
        companyToUpdate.Industry = company.Industry;
        companyToUpdate.Website = company.Website;
        companyToUpdate.Address = company.Address;
        companyToUpdate.City = company.City;
        companyToUpdate.State = company.State;
        companyToUpdate.ZipCode = company.ZipCode;
        companyToUpdate.Country = company.Country;
        companyToUpdate.PhoneNumber = company.PhoneNumber;
        companyToUpdate.Fax = company.Fax;
        companyToUpdate.Description = company.Description;
        _context.Companies.Update(companyToUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var company = await GetCompanyById(id);
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Companies.CountAsync();
    }
}