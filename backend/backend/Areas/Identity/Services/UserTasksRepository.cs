using backend.Areas.Identity.Models.ViewModels;
using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Identity.Services;

public class UserTasksRepository : IUserTasksRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;

    public UserTasksRepository(ApplicationDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserTasks>> GetAllUserTasksAsync()
    {
        return await _context.UserTasks.Include(u => u.User).Include(t => t.Tasks).ToListAsync();
    }

    public async Task<UserTasks> GetUserTaskById(int id)
    {
        var userTask = await _context.UserTasks.Include(u => u.User).Include(t => t.Tasks).FirstOrDefaultAsync(ut => ut.Id == id);
        if (userTask is null) return null;
        return userTask;
    }

    public async Task<IEnumerable<UserTasks>> GetUserTasksByUserId(string userId)
    {
        var tasks = await _context.UserTasks.Include(u => u.User).Include(t => t.Tasks).Where(ut => ut.User.Id == userId).ToListAsync();
        return tasks;
    }

    public async Task<UserTasks> AddUserTaskAsync(string id, [FromBody] AddUserTasksViewModel tasks)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        var newTask = new UserTasks
        {
            UserId = user.Id,
            Tasks = new Tasks
            {
                TaskTitle = tasks.TaskTitle,
                TaskDescription = tasks.TaskDescription,
                DueDate = tasks.DueDate,
                Status = tasks.Status,
                Priority = tasks.Priority,
                AssignedToUserId = tasks.AssignedToUserId,
                DateCreated = tasks.DateCreated
            }
        };
        _context.UserTasks.Add(newTask);
        await _context.SaveChangesAsync();
        return newTask;
    }

    public async Task UpdateUserTaskAsync(int id, [FromBody] UpdateUserTasksViewModel tasks)
    {
        var userTask = await _context.UserTasks.Include(u => u.User).Include(t => t.Tasks).FirstOrDefaultAsync(ut => ut.Id == id);
        if (userTask is null) return;
        userTask.Tasks.TaskTitle = tasks.TaskTitle;
        userTask.Tasks.TaskDescription = tasks.TaskDescription;
        userTask.Tasks.DueDate = tasks.DueDate;
        userTask.Tasks.Status = tasks.Status;
        userTask.Tasks.Priority = tasks.Priority;
        userTask.Tasks.AssignedToUserId = tasks.AssignedToUserId;
        userTask.Tasks.DateUpdated = tasks.DateUpdated;
        userTask.Tasks.DateCompleted = tasks.DateCompleted;
        _context.UserTasks.Update(userTask);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserTaskAsync(int id)
    {
        var userTask = await GetUserTaskById(id);
        _context.UserTasks.Remove(userTask);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountUserTasksAsync()
    {
        return await _context.UserTasks.CountAsync();
    }
}