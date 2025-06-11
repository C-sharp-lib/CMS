using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface IJobTaskRepository
{
    Task<IEnumerable<JobTask>> GetJobTasks();
    Task<JobTask> GetJobTask(int id);
    Task<IEnumerable<JobTask>> GetJobTasksByJobId(int jobId);
    Task<JobTask> CreateJobTask(int jobId, [FromBody] AddJobTaskViewModel model);
    Task UpdateJobTask(int id, [FromBody] UpdateJobTaskViewModel model);
    Task DeleteJobTask(int id);
    Task<int> CountJobTasks();
}