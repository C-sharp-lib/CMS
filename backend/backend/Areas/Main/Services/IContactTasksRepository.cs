using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface IContactTasksRepository
{
    Task<IEnumerable<ContactTasks>> GetAllContactTasksAsync();
    Task<ContactTasks> GetContactTaskByIdAsync(int id);
    Task<IEnumerable<ContactTasks>> GetContactTasksByContactIdAsync(int contactId);
    Task<ContactTasks> AddContactTaskAsync(int contactId, [FromBody] AddContactTasksViewModel contactTask);
    Task<ContactTasks> UpdateContactTaskAsync(int id, [FromBody] UpdateContactTasksViewModel contactTask);
    Task DeleteContactTaskAsync(int id);
    Task<int> CountContactTaskAsync();
}