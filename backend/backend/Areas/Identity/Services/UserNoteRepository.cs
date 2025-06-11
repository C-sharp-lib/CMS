using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Identity.Services;

public class UserNoteRepository : IUserNoteRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;

    public UserNoteRepository(ApplicationDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<UserNotes>> GetAllUserNotesAsync()
    {
        return await _context.UserNotes.Include(u => u.User).Include(n => n.Note).ToListAsync();
    }

    public async Task<UserNotes> GetUserNoteById(int id)
    {
        var notes = await _context.UserNotes.Include(u => u.User).Include(n => n.Note).FirstOrDefaultAsync(n => n.Id == id);
        if (notes == null) return null;
        return notes;
    }

    public async Task<IEnumerable<UserNotes>> GetUserNotesByUserId(string userId)
    {
        var notes = await _context.UserNotes.Include(u => u.User).Include(n => n.Note).Where(u => u.Id.ToString() == userId).ToListAsync();
        return notes;
    }

    public async Task<UserNotes> AddAsync(string id, [FromBody] AddUserNoteViewModel note)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        var userNote = new UserNotes
        {
            UserId = user.Id,
            Note = new Note
            {
                Title = note.Title,
                Content = note.Content,
                Created = note.Created
            }
        };
        _context.UserNotes.Add(userNote);
        await _context.SaveChangesAsync();
        return userNote;
    }

    public async Task UpdateAsync(int noteId, [FromBody] UpdateUserNoteViewModel note)
    {
        var notes = await _context.UserNotes.Include(u => u.User).Include(n => n.Note).FirstOrDefaultAsync(u => u.Id == noteId);
        if (notes == null) return;
        notes.Note.Title = note.Title;
        notes.Note.Content = note.Content;
        notes.Note.Updated = note.Updated;
        _context.UserNotes.Update(notes);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var note = await GetUserNoteById(id);
        _context.UserNotes.Remove(note);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.UserNotes.CountAsync();
    }
}