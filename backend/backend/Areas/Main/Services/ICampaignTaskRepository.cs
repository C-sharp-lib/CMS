using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ICampaignTaskRepository
{
    Task<IEnumerable<CampaignTasks>> GetAllCampaignTasks();
    Task<CampaignTasks> GetCampaignTask(int id);
    Task<IEnumerable<CampaignTasks>> GetCampaignTasksByCampaignId(int campaignId);
    Task<CampaignTasks> AddCampaignTask(int campaignId, [FromBody] AddCampaignTaskViewModel task);
    Task UpdateCampaignTask(int id, [FromBody] UpdateCampaignTaskViewModel task);
    Task DeleteCampaignTask(int id);
    Task<int> CountCampaignTasks();
 }