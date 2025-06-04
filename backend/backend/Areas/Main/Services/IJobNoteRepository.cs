using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface IJobNoteRepository
{
    Task<IEnumerable<JobNotes>> GetAllJobNotesAsync();
    Task<JobNotes> GetJobNoteById(int id);
    Task<JobNotes> AddAsync([FromBody] AddJobNoteViewModel note);
    Task UpdateAsync(int id, [FromBody] UpdateJobNoteViewModel note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}