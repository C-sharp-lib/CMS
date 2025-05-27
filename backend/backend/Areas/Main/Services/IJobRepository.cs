using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetAllJobsAsync();
    Task<Job> GetJobByIdAsync(int id);
    Task<Job> CreateJobAsync([FromBody] AddJobViewModel model);
    Task<Job> UpdateJobAsync(int id, [FromBody] UpdateJobViewModel model);
    Task<bool> DeleteJobAsync(int id);
    Task<int> CountAllJobsAsync();
}