using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CampaignNotesRepository : ICampaignNotesRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ICampaignRepository _campaignRepository;

    public CampaignNotesRepository(ApplicationDbContext context, ICampaignRepository campaignRepository)
    {
        _context = context;
        _campaignRepository = campaignRepository;
    }

    public async Task<IEnumerable<CampaignNotes>> GetCampaignNotesAsync()
    {
        return await _context.CampaignNotes
            .Include(c => c.Campaign)
            .Include(c => c.Note)
            .ToListAsync();
    }

    public async Task<CampaignNotes> GetCampaignNoteById(int id)
    {
        var campaignNote = await _context.CampaignNotes
            .Include(c => c.Campaign)
            .Include(c => c.Note)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (campaignNote == null)
        {
            throw new NullReferenceException($"Campaign note with id {id} was not found.");
        }
        return campaignNote;
    }

    public async Task<IEnumerable<CampaignNotes>> GetCampaignNotesbyCampaignIdAsync(int campaignId)
    {
        var campaign = await _campaignRepository.GetByIdAsync(campaignId);
        var notes = await _context.CampaignNotes.Include(c => c.Campaign).Include(c => c.Note).Where(cn => cn.Campaign.Id == campaign.Id).ToListAsync();
        if(!notes.Any()) return Array.Empty<CampaignNotes>();
        return notes;
    }
    
    public async Task<CampaignNotes> AddAsync(int campaignId, [FromBody] AddCampaignNoteViewModel model)
    {
        var campaign = await _campaignRepository.GetByIdAsync(campaignId);
        var campaignNote = new CampaignNotes
        {
            CampaignId = campaign.Id,
            Note = new Note
            {
                Title = model.Title,
                Content = model.Content,
                Created = model.Created
            }
        };
        _context.CampaignNotes.Add(campaignNote);
        await _context.SaveChangesAsync();
        return campaignNote;
    }

    public async Task UpdateAsync(int id, [FromBody] UpdateCampaignNoteViewModel model)
    {
        var campaignNote = await GetCampaignNoteById(id);
        campaignNote.Note.Title = model.Title;
        campaignNote.Note.Content = model.Content;
        campaignNote.Note.Updated = model.Updated;
        _context.CampaignNotes.Update(campaignNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var campaignNote = await GetCampaignNoteById(id);
        _context.CampaignNotes.Remove(campaignNote);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.CampaignNotes.CountAsync();
    }
}