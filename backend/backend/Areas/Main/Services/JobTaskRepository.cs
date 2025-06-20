using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class JobTaskRepository : IJobTaskRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IJobRepository _jobRepository;
    public JobTaskRepository(ApplicationDbContext context, IJobRepository jobRepository)
    {
        _context = context;
        _jobRepository = jobRepository;
    }
    public async Task<IEnumerable<JobTask>> GetJobTasks()
    {
        return await _context.JobTasks
            .Include(j => j.Job)
            .ThenInclude(c => c.Contact)
            .ThenInclude(cj => cj.CompanyContacts)!
            .ThenInclude(cj => cj.Company)
            .Include(c => c.Tasks)
            .ToListAsync();
    }

    public async Task<JobTask> GetJobTask(int id)
    {
        var jobTasks =  await _context.JobTasks
            .Include(j => j.Job)
            .ThenInclude(c => c.Contact)
            .ThenInclude(cj => cj.CompanyContacts)!
            .ThenInclude(cj => cj.Company)
            .Include(c => c.Tasks)
            .FirstOrDefaultAsync(j => j.Id == id);
        if (jobTasks == null)
        {
            throw new Exception($"Job task with id {id} not found");
        }
        return jobTasks;
    }
    
    public async Task<IEnumerable<JobTask>> GetJobTasksByJobId(int id)
    {
        var job = await _jobRepository.GetJobByIdAsync(id);
        var tasks = await _context.JobTasks.Include(j => j.Job).Include(t => t.Tasks).Where(j => j.JobId == job.Id).ToListAsync();
        if(!tasks.Any()) return Array.Empty<JobTask>();
        return tasks;
    }

    public async Task<JobTask> CreateJobTask(int jobId, [FromBody] AddJobTaskViewModel model)
    {
        var job = await _jobRepository.GetJobByIdAsync(jobId);
        var createJobTask = new JobTask
        {
            Tasks = new Tasks
            {
                TaskTitle = model.TaskTitle,
                TaskDescription = model.TaskDescription,
                DueDate = model.DueDate,
                Status = model.Status,
                Priority = model.Priority,
                DateCreated = model.DateCreated,
                AssignedToUserId = model.AssignedToUserId,
            },
            JobId = job.Id,
            Created = model.Created,
        };
        _context.JobTasks.Add(createJobTask);
        await _context.SaveChangesAsync();
        return createJobTask;
    }

    public async Task UpdateJobTask(int id, [FromBody] UpdateJobTaskViewModel model)
    {
        var updateJobTask = await GetJobTask(id);
        updateJobTask.Tasks.DueDate = model.DueDate;
        updateJobTask.Tasks.TaskTitle = model.TaskTitle;
        updateJobTask.Tasks.TaskDescription = model.TaskDescription;
        updateJobTask.Tasks.Priority = model.Priority;
        updateJobTask.Tasks.AssignedToUserId = model.AssignedToUserId;
        updateJobTask.JobId = model.JobId;
        updateJobTask.Tasks.Status = model.Status;
        updateJobTask.Tasks.DateUpdated = model.DateUpdated;
        updateJobTask.Tasks.DateCompleted = model.DateCompleted;
        updateJobTask.Updated = model.Updated;
        _context.JobTasks.Update(updateJobTask);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteJobTask(int id)
    {
        var jobTask = await GetJobTask(id);
        _context.JobTasks.Remove(jobTask);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountJobTasks()
    {
        return await _context.JobTasks.CountAsync();
    }
}