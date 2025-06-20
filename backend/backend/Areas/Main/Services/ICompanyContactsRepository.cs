using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ICompanyContactsRepository
{
    Task<IEnumerable<CompanyContacts>> GetCompanyContacts();
    Task<IEnumerable<CompanyContacts>> GetCompanyContactsByCompanyId(int companyId);
    Task<IEnumerable<CompanyContacts>> GetCompanyContactsByContactId(int contactId);
    Task<CompanyContacts> GetCompanyContactById(int id);
    Task<CompanyContacts> CreateCompanyContact([FromBody] AddCompanyContactsViewModel model);
    Task UpdateCompanyContact(int id, [FromBody] UpdateCompanyContactsViewModel model);
    Task DeleteCompanyContact(int id);
    Task<int> CountCompanyContacts();
}