using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class NoteRepository : INoteRepository
{
    private readonly ApplicationDbContext _context;

    public NoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note> GetNoteById(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            throw new NullReferenceException("Note not found");
        }
        return note;
    }

    public async Task<Note> AddAsync([FromBody] AddNoteViewModel note)
    {
        var notes = new Note
        {
            Title = note.Title,
            Content = note.Content,
            Created = note.Created
        };
        _context.Notes.Add(notes);
        await _context.SaveChangesAsync();
        return notes;
    }

    public async Task<Note> UpdateAsync(int id, [FromBody] UpdateNoteViewModel note)
    {
        var notes = await GetNoteById(id);
        if (notes == null)
        {
            throw new NullReferenceException("Note not found");
        }
        notes.Title = note.Title;
        notes.Content = note.Content;
        notes.Updated = note.Updated;
        _context.Notes.Update(notes);
        await _context.SaveChangesAsync();
        return notes;
    }

    public async Task DeleteAsync(int id)
    {
        var notes = await GetNoteById(id);
        _context.Notes.Remove(notes);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Notes.CountAsync();
    }
}