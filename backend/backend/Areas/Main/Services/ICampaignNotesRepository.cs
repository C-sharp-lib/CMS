using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ICampaignNotesRepository
{
    Task<IEnumerable<CampaignNotes>> GetCampaignNotesAsync();
    Task<CampaignNotes> GetCampaignNoteById(int id);
    Task<IEnumerable<CampaignNotes>> GetCampaignNotesbyCampaignIdAsync(int campaignId);
    Task<CampaignNotes> AddAsync(int campaignId, [FromBody] AddCampaignNoteViewModel model);
    Task UpdateAsync(int id, [FromBody] UpdateCampaignNoteViewModel model);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}