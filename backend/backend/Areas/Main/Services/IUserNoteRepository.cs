using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface IUserNoteRepository
{
    Task<IEnumerable<UserNotes>> GetAllUserNotesAsync();
    Task<UserNotes> GetUserNoteById(int id);
    Task<UserNotes> AddAsync([FromBody] AddUserNoteViewModel note);
    Task UpdateAsync(int id, [FromBody] UpdateUserNoteViewModel note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}