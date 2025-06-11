using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class ContactTasksRepository : IContactTasksRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IContactRepository _contactRepository;

    public ContactTasksRepository(ApplicationDbContext context, IContactRepository contactRepository)
    {
        _context = context;
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<ContactTasks>> GetAllContactTasksAsync()
    {
        return await _context.ContactTasks.Include(c => c.Contact).Include(t => t.Tasks).ToListAsync();
    }

    public async Task<ContactTasks> GetContactTaskByIdAsync(int id)
    {
        var task = await _context.ContactTasks.Include(c => c.Contact).Include(t => t.Tasks).FirstOrDefaultAsync(t => t.Id == id);
        if (task is null) return null;
        return task;
    }

    public async Task<IEnumerable<ContactTasks>> GetContactTasksByContactIdAsync(int contactId)
    {
        var contact = await _contactRepository.GetContactByIdAsync(contactId);
        var tasks = await _context.ContactTasks.Include(c => c.Contact).Include(t => t.Tasks).Where(c => c.Contact.Id == contact.Id).ToListAsync();
        if(!tasks.Any()) return null;
        return tasks;
    }

    public async Task<ContactTasks> AddContactTaskAsync(int contactId, [FromBody] AddContactTasksViewModel model)
    {
        var contact = await _contactRepository.GetContactByIdAsync(contactId);
        var tasks = new ContactTasks
        {
            ContactId = contact.Id,
            Tasks = new Tasks
            {
                TaskTitle = model.TaskTitle,
                TaskDescription = model.TaskDescription,
                DueDate = model.DueDate,
                Status = model.Status,
                Priority = model.Priority,
                AssignedToUserId = model.AssignedToUserId,
                DateCreated = model.DateCreated
            }
        };
        _context.ContactTasks.Add(tasks);
        await _context.SaveChangesAsync();
        return tasks;
    }

    public async Task<ContactTasks> UpdateContactTaskAsync(int id, [FromBody] UpdateContactTasksViewModel model)
    {
        var contactTask = await GetContactTaskByIdAsync(id);
        contactTask.ContactId = model.ContactId;
        contactTask.Tasks.TaskTitle = model.TaskTitle;
        contactTask.Tasks.TaskDescription = model.TaskDescription;
        contactTask.Tasks.DueDate = model.DueDate;
        contactTask.Tasks.AssignedToUserId = model.AssignedToUserId;
        contactTask.Tasks.Status = model.Status;
        contactTask.Tasks.Priority = model.Priority;
        contactTask.Tasks.DateUpdated = model.DateUpdated;
        contactTask.Tasks.DateCompleted = model.DateCompleted;
        _context.ContactTasks.Update(contactTask);
        await _context.SaveChangesAsync();
        return contactTask;
    }

    public async Task DeleteContactTaskAsync(int id)
    {
        var contactTask = await GetContactTaskByIdAsync(id);
        _context.ContactTasks.Remove(contactTask);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountContactTaskAsync()
    {
        return await _context.ContactTasks.CountAsync();
    }
}