using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ICompanyTaskRepository
{
    Task<IEnumerable<CompanyTask>> GetAllAsync();
    Task<CompanyTask> GetByIdAsync(int id);
    Task<IEnumerable<CompanyTask>> GetByCompanyIdAsync(int companyId);
    Task<CompanyTask> AddAsync(int companyId, [FromBody] AddCompanyTaskViewModel companyTask);
    Task UpdateAsync(int id, [FromBody] UpdateCompanyTaskViewModel companyTask);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}