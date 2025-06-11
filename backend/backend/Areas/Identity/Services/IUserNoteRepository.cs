using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Identity.Services;

public interface IUserNoteRepository
{
    Task<IEnumerable<UserNotes>> GetAllUserNotesAsync();
    Task<UserNotes> GetUserNoteById(int id);
    Task<IEnumerable<UserNotes>> GetUserNotesByUserId(string userId);
    Task<UserNotes> AddAsync(string id, [FromBody] AddUserNoteViewModel note);
    Task UpdateAsync(int id, [FromBody] UpdateUserNoteViewModel note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}