using System.Text.RegularExpressions;
using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Controllers;

    
    [ApiController]
    [Area("Main")]
    [Route("api/[area]/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactController> _logger;
        private readonly IContactNotesRepository _contactNotesRepository;
        private readonly IContactTasksRepository _contactTasksRepository;

        public ContactController(ApplicationDbContext context, IContactRepository contactRepository, ILogger<ContactController> logger,
            IContactNotesRepository contactNotesRepository, IContactTasksRepository contactTasksRepository)
        {
            _context = context;
            _contactRepository = contactRepository;
            _logger = logger;
            _contactNotesRepository = contactNotesRepository;
            _contactTasksRepository = contactTasksRepository;
        }

        // GET: api/Contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            return Ok(contacts);
        }
        [HttpGet("get-contact-image-path")]
        public IActionResult GetContactImagePath([FromQuery] string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return BadRequest("Image path is required.");

            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var fullPath = Path.Combine(wwwrootPath, relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (!System.IO.File.Exists(fullPath))
                return NotFound("Image not found.");

            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var fullImageUrl = $"{baseUrl}/{relativePath}";

            return Ok(new { imageUrl = fullImageUrl });
        }
        
        [HttpGet("get-contact-user-image-path")]
        public IActionResult GetContactUserImagePath([FromQuery] string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return BadRequest("Image path is required.");

            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var fullPath = Path.Combine(wwwrootPath, relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (!System.IO.File.Exists(fullPath))
                return NotFound("Image not found.");

            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var fullImageUrl = $"{baseUrl}/{relativePath}";

            return Ok(new { imageUrl = fullImageUrl });
        }

        // GET: api/Contact/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetContactById(int id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            return Ok(contact);
        }

        // GET: api/Contact/owner/{ownerUserId}
        [HttpGet("owner/{ownerUserId}")]
        public async Task<ActionResult> GetContactsByOwner(string ownerUserId)
        {
            var contacts = await _contactRepository.GetContactsByOwnerAsync(ownerUserId);
            return Ok(contacts);
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromForm] AddContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new {Errors = errors});
            }
            try
            {
                var contacts = await _contactRepository.AddContactAsync(contact);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        

        // PUT: api/Contact/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromForm] UpdateContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new {Errors = errors});
            }

            try
            {
                await _contactRepository.UpdateContactAsync(id, contact);
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogInformation($"Updating Contact with id {id} failed", ex);
                return BadRequest(new {message = $"Failed to update Contact with id - DbUpdateConcurrencyException {id}"});
            }
            catch (DbUpdateException ex)
            {
                _logger.LogInformation($"Updating Contact with id {id} failed", ex);
                return BadRequest(new {message = $"Failed to update Contact with id - DbUpdateException {id}"});
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Updating Contact with id {id} failed", ex);
                return BadRequest(new {message = $"Failed to update Contact with id - Exception {id}"});
            }
        }

        // DELETE: api/Contact/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            try
            {
                await _contactRepository.DeleteContactAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message});
            }
        }

        [HttpGet("contactCount")]
        public async Task<ActionResult<int>> GetContactCount()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            return Ok(contacts);
        }
        
        [HttpGet("contact/{contactId}/notes")]
        public async Task<ActionResult<IEnumerable<ContactNotes>>> GetJobNotesByJobId(int contactId)
        {
            var contactNotes = await _contactNotesRepository.GetAllContactNotesByContactId(contactId);
            if(!contactNotes.Any()) return NotFound();
            return Ok(contactNotes);
        }

        [HttpGet("notes")]
        public async Task<ActionResult<IEnumerable<ContactNotes>>> GetContactNotes()
        {
            var contactNotes = await _contactNotesRepository.GetAllContactNotesAsync();
            return Ok(contactNotes);
        }

        [HttpGet("notes/{id}")]
        public async Task<ActionResult<ContactNotes>> GetContactNotes(int id)
        {
            var contactNotes = await _contactNotesRepository.GetContactNoteById(id);
            return Ok(contactNotes);
        }

        [HttpPut("notes/{id}")]
        public async Task<ActionResult> UpdateContactNotes(int id, [FromBody] UpdateContactNoteViewModel notes)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new {Errors = errors});
            }

            try
            {
                await _contactNotesRepository.UpdateAsync(id, notes);
                return Ok("Notes updated.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogInformation($"Updating Contact Note with id {id} failed", ex);
                return BadRequest(new { Errors = $"Failed to update Contact Note with id - DbUpdateConcurrencyException {id}" });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogInformation($"Updating Contact Note with id {id} failed", ex);
                return BadRequest(new {Errors = $"Failed to update Contact Note with id - DbUpdateException {id}" });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Updating Contact Note with id {id} failed", ex);
                return BadRequest(new {Errors = $"Failed to update Contact Note with id - Exception {id}"});
            }
        }

        [HttpPost("notes")]
        public async Task<ActionResult<ContactNotes>> CreateContactNotes([FromBody] AddContactNoteViewModel notes)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new {Errors = errors});
            }

            try
            {
                await _contactNotesRepository.AddAsync(notes);
                return Ok("Notes created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("notes/{id}")]
        public async Task<ActionResult> DeleteContactNotes(int id)
        {
            await _contactNotesRepository.DeleteAsync(id);
            return Ok("Notes deleted.");
        }

        [HttpGet("notes/count")]
        public async Task<ActionResult<int>> GetContactNotesCount()
        {
            return Ok(await _contactNotesRepository.CountAsync());
        }

        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<ContactTasks>>> GetAllContactTasks()
        {
            var contactTasks = await _contactTasksRepository.GetAllContactTasksAsync();
            return Ok(contactTasks);
        }

        [HttpGet("contact/{contactId}/tasks")]
        public async Task<ActionResult<IEnumerable<ContactTasks>>> GetContactTasks(int contactId)
        {
            var contactTasks = await _contactTasksRepository.GetContactTasksByContactIdAsync(contactId);
            if(!contactTasks.Any()) return NotFound();
            return Ok(contactTasks);
        }

        [HttpGet("tasks/count")]
        public async Task<ActionResult<int>> GetContactTasksCount()
        {
            return Ok(await _contactTasksRepository.CountContactTaskAsync());
        }

        [HttpGet("tasks/{id}")]
        public async Task<ActionResult<ContactTasks>> GetContactTask(int id)
        {
            var contactTasks = await _contactTasksRepository.GetContactTaskByIdAsync(id);
            return Ok(contactTasks);
        }

        [HttpPost("tasks")]
        public async Task<ActionResult<ContactTasks>> CreateContactTask(int contactId, [FromBody] AddContactTasksViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new {Errors = errors});
            }

            try
            {
                await _contactTasksRepository.AddContactTaskAsync(contactId, model);
                return Ok(new {message = "Tasks created successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(new {Errors = ex.Message});
            }
        }
        [HttpPut("tasks/{id}")]
        public async Task<ActionResult> UpdateContactTask(int id, [FromBody] UpdateContactTasksViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new {Errors = errors});
            }

            try
            {
                await _contactTasksRepository.UpdateContactTaskAsync(id, model);
                return Ok(new {message = "Tasks updated successfully"});
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogInformation($"Updating Contact Task with id {id} failed", ex.Message);
                return BadRequest(new { Errors = $"Failed to update Contact Task with id - DbUpdateConcurrencyException {id}" });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogInformation($"Updating Contact Task with id {id} failed", ex.Message);
                return BadRequest(new {Errors = $"Failed to update Contact Task with id - DbUpdateException {id}" });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Updating Contact Task with id {id} failed", ex.Message);
                return BadRequest(new {Errors = $"Failed to update Contact Task with id - Exception {id}"});
            }
        }

        [HttpDelete("tasks/{id}")]
        public async Task<ActionResult> DeleteContactTask(int id)
        {
            await _contactTasksRepository.DeleteContactTaskAsync(id);
            return NoContent();
        }
    }