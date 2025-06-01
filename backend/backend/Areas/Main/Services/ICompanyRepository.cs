using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompaniesAsync();
    Task<Company> GetCompanyById(int id);
    Task<Company> AddAsync([FromBody] AddCompanyViewModel company);
    Task UpdateAsync(int id, [FromBody] UpdateCompanyViewModel company);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}