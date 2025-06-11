using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CompanyNoteRepository : ICompanyNoteRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ICompanyRepository _companyRepository;

    public CompanyNoteRepository(ApplicationDbContext context, ICompanyRepository companyRepository)
    {
        _context = context;
        _companyRepository = companyRepository;
    }

    public async Task<IEnumerable<CompanyNotes>> GetAllCompanyNotesAsync()
    {
        return await _context.CompanyNotes.ToListAsync();
    }

    public async Task<CompanyNotes> GetCompanyNoteById(int id)
    {
        var companyNote = await _context.CompanyNotes.FindAsync(id);
        if (companyNote == null)
        {
            throw new NullReferenceException($"CompanyNote with id {id} not found");
        }
        return companyNote;
    }

    public async Task<IEnumerable<CompanyNotes>> GetCompanyNotesByCompanyId(int companyId)
    {
        var company = await _companyRepository.GetCompanyById(companyId);
        var notes = await _context.CompanyNotes
            .Include(c => c.Company)
            .Include(n => n.Note)
            .Where(n => n.Company.Id == company.Id)
            .ToListAsync();
        if(!notes.Any()) return Array.Empty<CompanyNotes>();
        return notes;
    }

    public async Task<CompanyNotes> AddAsync(int companyId, [FromBody] AddCompanyNoteViewModel model)
    {
        var company = await _companyRepository.GetCompanyById(companyId);
        var notes = new CompanyNotes
        {
            CompanyId = company.Id,
            Note = new Note
            {
                Title = model.Title,
                Content = model.Content,
                Created = model.Created,
            }
        };
        _context.CompanyNotes.Add(notes);
        await _context.SaveChangesAsync();
        return notes;
    }

    public async Task UpdateAsync(int id, [FromBody] UpdateCompanyNoteViewModel model)
    {
        var companyNote = await GetCompanyNoteById(id);
        companyNote.Note.Title = model.Title;
        companyNote.Note.Content = model.Content;
        companyNote.Note.Updated = model.Updated;
        _context.CompanyNotes.Update(companyNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var companyNote = await GetCompanyNoteById(id);
        _context.CompanyNotes.Remove(companyNote);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.CompanyNotes.CountAsync();
    }
}