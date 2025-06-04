using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class JobNoteRepository : IJobNoteRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IJobRepository _jobRepository;

    public JobNoteRepository(ApplicationDbContext context, IJobRepository jobRepository)
    {
        _context = context;
        _jobRepository = jobRepository;
    }

    public async Task<IEnumerable<JobNotes>> GetAllJobNotesAsync()
    {
        return await _context.JobNotes.ToListAsync();
    }

    public async Task<IEnumerable<JobNotes>> GetJobNotesByJobId(int id)
    {
        var notes = await _context.JobNotes
            .Where(jn => jn.JobId == id)
            .Include(jn => jn.Note)
            .Include(jn => jn.Job)
            .ToListAsync();

        return notes;
    }

    public async Task<JobNotes> GetJobNoteById(int id)
    {
        var jobNote = await _context.JobNotes.Include(j => j.Job).Include(n => n.Note).FirstOrDefaultAsync(j => j.Id == id);
        if (jobNote == null)
        {
            throw new NullReferenceException($"JobNote with id: {id} was not found.");
        }
        return jobNote;
    }

    public async Task<JobNotes> AddAsync(int id, [FromBody] AddJobNoteViewModel note)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == id);
        if (job == null)
        {
            throw new NullReferenceException($"Job with id: {id} was not found.");
        }
        var jobNote = new JobNotes
        {
            JobId = job.Id,
            Note = new Note
            {
                Title = note.Title,
                Content = note.Content,
                Created = note.Created
            }
        };
        _context.JobNotes.Add(jobNote);
        await _context.SaveChangesAsync();
        return jobNote;
    }

    public async Task UpdateAsync(int noteId, [FromBody] UpdateJobNoteViewModel note)
    {
        var jobNote = await GetJobNoteById(noteId);
        jobNote.Note.Title = note.Title;
        jobNote.Note.Content = note.Content;
        jobNote.Note.Updated = note.Updated;
        jobNote.JobId = note.JobId;
        _context.JobNotes.Update(jobNote);
        await _context.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(int id)
    {
        var note = await _context.JobNotes.FirstOrDefaultAsync(j => j.Id == id);
        if (note == null)
        {
            throw new NullReferenceException($"JobNote with id: {id} was not found.");
        }
        _context.JobNotes.Remove(note);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        var jobNotes = await _context.JobNotes.CountAsync();
        return jobNotes;
    }
}