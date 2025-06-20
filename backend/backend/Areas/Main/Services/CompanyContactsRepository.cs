using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CompanyContactsRepository : ICompanyContactsRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ICompanyRepository _companyRepository;
    private readonly IContactRepository _contactRepository;

    public CompanyContactsRepository(ApplicationDbContext context, ICompanyRepository companyRepository,
        IContactRepository contactRepository)
    {
        _context = context;
        _companyRepository = companyRepository;
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<CompanyContacts>> GetCompanyContacts()
    {
        return await _context.CompanyContacts
            .Include(x => x.Company)
            .Include(x => x.Contact)
            .ToListAsync();
    }

    public async Task<IEnumerable<CompanyContacts>> GetCompanyContactsByCompanyId(int companyId)
    {
        var company = await _companyRepository.GetCompanyById(companyId);
        var contacts = await _context.CompanyContacts
            .Include(x => x.Company)
            .Include(x => x.Contact)
            .Where(c => c.CompanyId == company.Id)
            .ToListAsync();
        if(!contacts.Any()) return Array.Empty<CompanyContacts>();
        return contacts;
    }

    public async Task<IEnumerable<CompanyContacts>> GetCompanyContactsByContactId(int contactId)
    {
        var contact = await _contactRepository.GetContactByIdAsync(contactId);
        var company = await _context.CompanyContacts
            .Include(x => x.Company)
            .Include(x => x.Contact)
            .Where(c => c.ContactId == contact.Id)
            .ToListAsync();
        if(!company.Any()) return Array.Empty<CompanyContacts>();
        return company;
    }

    public async Task<CompanyContacts> GetCompanyContactById(int id)
    {
        var companyContact = await _context.CompanyContacts
            .Include(x => x.Company)
            .Include(x => x.Contact)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (companyContact is null) return null;
        return companyContact;
    }

    public async Task<CompanyContacts> CreateCompanyContact([FromBody] AddCompanyContactsViewModel model)
    {
        var companyContact = new CompanyContacts
        {
            CompanyId = model.CompanyId,
            ContactId = model.ContactsId
        };
        _context.CompanyContacts.Add(companyContact);
        await _context.SaveChangesAsync();
        return companyContact;
    }

    public async Task UpdateCompanyContact(int id, UpdateCompanyContactsViewModel model)
    {
        var companyContact = await GetCompanyContactById(id);
        companyContact.CompanyId = model.CompanyId;
        companyContact.ContactId = model.ContactsId;
        _context.CompanyContacts.Update(companyContact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCompanyContact(int id)
    {
        var companyContact = await GetCompanyContactById(id);
        _context.CompanyContacts.Remove(companyContact);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountCompanyContacts()
    {
        return await _context.CompanyContacts.CountAsync();
    }
}