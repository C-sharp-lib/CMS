using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ICompanyNoteRepository
{
    Task<IEnumerable<CompanyNotes>> GetAllCompanyNotesAsync();
    Task<CompanyNotes> GetCompanyNoteById(int id);
    Task<IEnumerable<CompanyNotes>> GetCompanyNotesByCompanyId(int companyId);
    Task<CompanyNotes> AddAsync(int companyId, [FromBody] AddCompanyNoteViewModel model);
    Task UpdateAsync(int id, [FromBody] UpdateCompanyNoteViewModel model);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}