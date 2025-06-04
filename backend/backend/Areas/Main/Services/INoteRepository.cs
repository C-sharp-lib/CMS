using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllNotesAsync();
    Task<Note> GetNoteById(int id);
    Task<Note> AddAsync([FromBody] AddNoteViewModel note);
    Task<Note> UpdateAsync(int id, [FromBody] UpdateNoteViewModel note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}