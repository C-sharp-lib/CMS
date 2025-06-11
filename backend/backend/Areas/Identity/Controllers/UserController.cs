using System.Security.Claims;
using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using backend.Areas.Identity.Services;
using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Identity.Controllers;

[ApiController]
[Area("Identity")]
[Route("api/[area]/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<UserController> _logger;
    private readonly IUserNoteRepository _userNoteRepository;
    private readonly IUserTasksRepository _userTasksRepository;
    
    public UserController(ApplicationDbContext context, IUserRepository userRepository, UserManager<User> userManager, IRoleRepository roleRepository, 
        ILogger<UserController> logger, IUserNoteRepository userNoteRepository, IUserTasksRepository userTasksRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _userManager = userManager;
        _roleRepository = roleRepository;
        _logger = logger;
        _userNoteRepository = userNoteRepository;
        _userTasksRepository = userTasksRepository;
    }

    [HttpGet("current-user")]
    public async Task<ActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Unauthorized();
        }
        return Ok(new
        {
            user.Id,
            user.Email,
            user.UserName,
            user.Name,
            user.PhoneNumber,
            user.Address,
            user.City,
            user.State,
            user.ZipCode,
            user.DateOfBirth,
            user.Description
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterViewModel model)
    {
        try
        {
            var result = await _userRepository.RegisterAsync(model);
            if (!result.Succeeded)
                return BadRequest(new {error = $"{result.Errors}"});
        
            return Ok(new {message = "User registered successfully"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {error = $"An error occured while registering: {ex.Message}"});
        }
        
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginViewModel model)
    {
        try
        {
            var token = await _userRepository.LoginAsync(model);
            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(new {error = $"An error occured while login: {ex.Message}"});
        }
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("get-user-image-path")]
    public IActionResult GetUserImagePath([FromQuery] string relativePath)
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
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return Ok(user);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserViewModel model)
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
            await _userRepository.UpdateUserAsync(id, model);
            return Ok(new {message = "User updated successfully"});
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(new { message = "Failed to update user - DbUpdateConcurrencyException: " + ex.Message });
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(new { message = "Failed to update user - DbUpdateException: " + ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(new { message = "Failed to update user - Exception: " + ex.Message });
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        await _userRepository.DeleteUserAsync(id);
        return Ok(new {message = "User deleted successfully"});
    }

    [HttpGet("count")]
    public async Task<ActionResult> GetUserCount()
    {
        var countUsers = await _userRepository.CountUsersAsync();
        return Ok(countUsers);
    }

    [HttpGet("user/{userId}/notes")]
    public async Task<ActionResult<IEnumerable<UserNotes>>> GetUserNotesByUserId(string userId)
    {
        var userNotes = await _userNoteRepository.GetUserNotesByUserId(userId);
        if(userNotes.Count() < 0) return NoContent();
        return Ok(userNotes);
    }
    [HttpGet("notes/{noteId}")]
    public async Task<ActionResult<UserNotes>> GetUserNotesById(int noteId)
    {
        var userNotes = await _userNoteRepository.GetUserNoteById(noteId);
        return Ok(userNotes);
    }

    [HttpPost("notes")]
    public async Task<ActionResult> AddNote(string id, [FromBody] AddUserNoteViewModel model)
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
            var userNote = await _userNoteRepository.AddAsync(id, model);
            return Ok(userNote);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = $"Failed to add note: {ex.Message}"});
        }
    }
    [HttpPut("notes/{id}")]
    public async Task<ActionResult> UpdateNote(int id, [FromBody] UpdateUserNoteViewModel model)
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
            await _userNoteRepository.UpdateAsync(id, model);
            return Ok();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest(new {message = $"Failed to add note: {ex.Message}"});
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new {message = $"Failed to add note: {ex.Message}"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = $"Failed to add note: {ex.Message}"});
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult> DeleteNote(int id)
    {
        await _userNoteRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("notes/count")]
    public async Task<int> GetUserNotesCount()
    {
        return await _userNoteRepository.CountAsync();
    }

    [HttpGet("user/{userId}/tasks")]
    public async Task<ActionResult<IEnumerable<UserTasks>>> GetUserTasks(string userId)
    {
        var tasks = await _userTasksRepository.GetUserTasksByUserId(userId);
        if (tasks.Count() < 0) return NoContent();
        return Ok(tasks);
    }

    [HttpGet("tasks/count")]
    public async Task<int> GetUserTasksCount()
    {
        return await _userTasksRepository.CountUserTasksAsync();
    }

    [HttpGet("tasks/{id}")]
    public async Task<ActionResult<UserTasks>> GetUserTask(int id)
    {
        var task = await _userTasksRepository.GetUserTaskById(id);
        return Ok(task);
    }

    [HttpPost("tasks")]
    public async Task<ActionResult<UserTasks>> CreateUserTask(string userId, [FromBody] AddUserTasksViewModel model)
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
            var userNote = await _userTasksRepository.AddUserTaskAsync(userId, model);
            return Ok(userNote);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = $"Failed to add note: {ex.Message}"});
        }
    }

    [HttpPut("tasks/{id}")]
    public async Task<ActionResult<UserTasks>> UpdateUserTask(int id, [FromBody] UpdateUserTasksViewModel model)
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
            await _userTasksRepository.UpdateUserTaskAsync(id, model);
            return Ok();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest(new {message = $"Failed to add note: {ex.Message}"});
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new {message = $"Failed to add note: {ex.Message}"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = $"Failed to add note: {ex.Message}"});
        }
    }
}