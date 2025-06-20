using backend.Areas.Main.Models;
using backend.Areas.Main.Models.Enums;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _context;

    public JobRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Job>> GetAllJobsAsync()
    {
        return await _context.Jobs
            .Include(j => j.Contact)
            .ThenInclude(j => j.CompanyContacts)!
            .ThenInclude(j => j.Company)
            .Include(j => j.AssignedUser)
            .Include(j => j.CreatedByUser)
            .ToListAsync();
    }

    public async Task<Job> GetJobByIdAsync(int id)
    {
        var job = await _context.Jobs
            .Include(j => j.Contact)
            .ThenInclude(j => j.CompanyContacts)!
            .ThenInclude(j => j.Company)
            .Include(j => j.AssignedUser)
            .Include(j => j.CreatedByUser)
            .Include(jn => jn.JobNotes)
            .FirstOrDefaultAsync(j => j.Id == id);
        if (job == null)
        {
            throw new NullReferenceException("Job not found");
        }

        return job;
    }

    public async Task<Job> CreateJobAsync([FromBody] AddJobViewModel model)
    {
        var job = new Job
        {
            Title = model.Title,
            AssignedUserId = model.AssignedUserId,
            ActualCost = model.ActualCost,
            CreatedByUserId = model.CreatedByUserId,
            ContactId = model.ContactId,
            DateCreated = model.DateCreated,
            Priority = model.Priority,
            Status = model.Status,
            Description = model.Description,
            ScheduledDate = model.ScheduledDate,
            EstimatedCost = model.EstimatedCost,
            Notes = model.Notes
        };
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task<Job> UpdateJobAsync(int id, [FromBody] UpdateJobViewModel model)
    {
        var job = await GetJobByIdAsync(id);
            job.Title = model.Title;
            job.Description = model.Description;
            job.ActualCost = model.ActualCost;
            job.Priority = model.Priority;
            job.Status = model.Status;
            job.Notes = model.Notes;
            job.ScheduledDate = model.ScheduledDate;
            job.CompletionDate = model.CompletionDate;
            job.DateUpdated = model.DateUpdated;
            job.EstimatedCost = model.EstimatedCost;

        _context.Jobs.Update(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task<bool> DeleteJobAsync(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job == null)
            return false;

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> CountAllJobsAsync()
    {
        return await _context.Jobs.CountAsync();
    }
}