using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CampaignTaskRepository : ICampaignTaskRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ICampaignRepository _campaignRepository;

    public CampaignTaskRepository(ApplicationDbContext context, ICampaignRepository campaignRepository)
    {
        _context = context;
        _campaignRepository = campaignRepository;
    }
    public async Task<IEnumerable<CampaignTasks>> GetAllCampaignTasks()
    {
        return await _context.CampaignTasks.Include(c => c.Campaign).Include(t => t.Tasks).ToListAsync();
    }

    public async Task<CampaignTasks> GetCampaignTask(int id)
    {
        var campaignTask = await _context.CampaignTasks
            .Include(x => x.Campaign)
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (campaignTask is null) return null;
        return campaignTask;
    }

    public async Task<IEnumerable<CampaignTasks>> GetCampaignTasksByCampaignId(int campaignId)
    {
        var campaign = await _campaignRepository.GetByIdAsync(campaignId);
        var tasks = await _context.CampaignTasks.Include(x => x.Campaign).Include(x => x.Tasks).Where(ct => ct.Campaign.Id == campaign.Id).ToListAsync();
        if (!tasks.Any()) return null;
        return tasks;
    }

    public async Task<CampaignTasks> AddCampaignTask(int campaignId, [FromBody] AddCampaignTaskViewModel task)
    {
        var campaign = await _campaignRepository.GetByIdAsync(campaignId);
        var campaignTask = new CampaignTasks
        {
            Tasks = new Tasks
            {
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                DueDate = task.DueDate,
                Status = task.Status,
                Priority = task.Priority,
                AssignedToUser = task.AssignedToUserId,
                DateCreated = task.DateCreated
            },
            CampaignId = campaign.Id
        };
        _context.CampaignTasks.Add(campaignTask);
        await _context.SaveChangesAsync();
        return campaignTask;
    }

    public async Task UpdateCampaignTask(int id, [FromBody] UpdateCampaignTaskViewModel task)
    {
        var campaignTask = await GetCampaignTask(id);
        campaignTask.Tasks.TaskDescription = task.TaskDescription;
        campaignTask.Tasks.DueDate = task.DueDate;
        campaignTask.Tasks.Status = task.Status;
        campaignTask.Tasks.Priority = task.Priority;
        campaignTask.Tasks.AssignedToUser = task.AssignedToUserId;
        campaignTask.Tasks.DateUpdated = task.DateUpdated;
        _context.CampaignTasks.Update(campaignTask);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCampaignTask(int id)
    {
        var campaignTask = await GetCampaignTask(id);
        _context.CampaignTasks.Remove(campaignTask);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountCampaignTasks()
    {
        return await _context.CampaignTasks.CountAsync();
    }
}