using System.Text.RegularExpressions;
using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webenv;
    public ContactRepository(ApplicationDbContext context, IWebHostEnvironment webenv)
    {
        _context = context;
        _webenv = webenv;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        return await _context.Contacts
            .Include(c => c.OwnerUser)
            .Include(n => n.ContactNotes)!
            .ThenInclude(n => n.Note)
            .Include(c => c.Tasks)!
            .ThenInclude(ct => ct.TaskNotes)!
            .ThenInclude(ct => ct.Note)
            .ToListAsync();
    }

    public async Task<Contact> GetContactByIdAsync(int id)
    {
        var contact = await _context.Contacts
            .Include(c => c.OwnerUser)
            .Include(cp => cp.Company)
            .Include(n => n.ContactNotes)!
            .ThenInclude(n => n.Note)
            .Include(c => c.Tasks)!
            .ThenInclude(ct => ct.TaskNotes)!
            .ThenInclude(ct => ct.Note)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (contact == null)
        {
            throw new NullReferenceException("Contact not found");
        }
        return contact;
    }

    public async Task<Contact> AddContactAsync([FromForm] AddContactViewModel contact)
    {

        try
        { 
            string uniqueFileName = null;
            if (contact.ImageUrl != null && contact.ImageUrl.Length > 0)
            {
                var permittedExtensions3 = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension3 = Path.GetExtension(contact.ImageUrl.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(extension3) || !permittedExtensions3.Contains(extension3))
                {
                    throw new FormatException("Invalid image extension");
                }

                string fileName = Path.GetFileNameWithoutExtension(contact.ImageUrl.FileName);
                uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension3}";
                string uploadsFolder3 = Path.Combine(_webenv.WebRootPath, "Uploads/Contact");
                if (!Directory.Exists(uploadsFolder3))
                {
                    Directory.CreateDirectory(uploadsFolder3);
                }

                string filePath = Path.Combine(uploadsFolder3, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await contact.ImageUrl.CopyToAsync(fileStream);
                }
                var contacts = new Contact
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    JobTitle = contact.JobTitle,
                    PhoneNumber = contact.PhoneNumber,
                    AddressLine1 = contact.AddressLine1,
                    AddressLine2 = contact.AddressLine2,
                    City = contact.City,
                    State = contact.State,
                    ZipCode = contact.ZipCode,
                    Country = contact.Country,
                    Notes = contact.Notes,
                    DateCreated = contact.DateCreated,
                    OwnerUserId = contact.OwnerUserId,
                    CompanyId = contact.CompanyId,
                    ImageUrl = (uniqueFileName != null ? Path.Combine("Uploads/Contact/", uniqueFileName) : null)!
                };
                _context.Contacts.Add(contacts);
                await _context.SaveChangesAsync();
                return contacts;
            }
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return null;
    }

    public async Task<Contact> UpdateContactAsync(int id, [FromForm] UpdateContactViewModel contact)
    {
        var contactToUpdate = await GetContactByIdAsync(id);
        string uploadsFolder = Path.Combine(_webenv.WebRootPath, "Uploads/Contact");
        if (contactToUpdate.ImageUrl != null && contact.ImageUrl?.Length > 0)
        {
            string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(contact.ImageUrl.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName1);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await contact.ImageUrl.CopyToAsync(fileStream);
            }

            contactToUpdate.ImageUrl = uniqueFileName1;
        }
        
        contactToUpdate.FirstName = contact.FirstName;
        contactToUpdate.LastName = contact.LastName;
        contactToUpdate.JobTitle = contact.JobTitle;
        contactToUpdate.AddressLine1 = contact.AddressLine1;
        contactToUpdate.AddressLine2 = contact.AddressLine2;
        contactToUpdate.City = contact.City;
        contactToUpdate.State = contact.State;
        contactToUpdate.ZipCode = contact.ZipCode;
        contactToUpdate.Country = contact.Country;
        contactToUpdate.PhoneNumber = contact.PhoneNumber;
        contactToUpdate.Email = contact.Email;
        contactToUpdate.DateUpdated = contact.DateUpdated;
        contactToUpdate.Notes = contact.Notes;
        contactToUpdate.CompanyId = contact.CompanyId;
        _context.Contacts.Update(contactToUpdate);
        await _context.SaveChangesAsync();
        return contactToUpdate;
    }

    public async Task<int> GetContactsCountAsync()
    {
        return await _context.Contacts.CountAsync();
    }

    public async Task DeleteContactAsync(int id)
    {
        var contact = await GetContactByIdAsync(id);
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Contact>> GetContactsByOwnerAsync(string ownerUserId)
    {
        return await _context.Contacts
            .Where(c => c.OwnerUserId == ownerUserId)
            .Include(c => c.OwnerUser)
            .ToListAsync();
    }
}