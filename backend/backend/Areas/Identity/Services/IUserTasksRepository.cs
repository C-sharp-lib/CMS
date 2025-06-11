using backend.Areas.Identity.Models.ViewModels;
using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Identity.Services;

public interface IUserTasksRepository
{
    Task<IEnumerable<UserTasks>> GetAllUserTasksAsync();
    Task<UserTasks> GetUserTaskById(int id);
    Task<IEnumerable<UserTasks>> GetUserTasksByUserId(string userId);
    Task<UserTasks> AddUserTaskAsync(string id, [FromBody] AddUserTasksViewModel tasks);
    Task UpdateUserTaskAsync(int id, [FromBody] UpdateUserTasksViewModel tasks);
    Task DeleteUserTaskAsync(int id);
    Task<int> CountUserTasksAsync();
}