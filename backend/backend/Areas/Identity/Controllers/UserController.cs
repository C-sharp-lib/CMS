using System.Security.Claims;
using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using backend.Areas.Identity.Services;
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
    
    public UserController(ApplicationDbContext context, IUserRepository userRepository, UserManager<User> userManager, IRoleRepository roleRepository, 
        ILogger<UserController> logger)
    {
        _context = context;
        _userRepository = userRepository;
        _userManager = userManager;
        _roleRepository = roleRepository;
        _logger = logger;
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
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(string id, [FromBody] UpdateUserViewModel model)
    {
        try
        {
            var result = await _userRepository.UpdateUserAsync(id, model);
            if (!result.Succeeded)
            {
                return BadRequest(new {error = $"{result.Errors}"});
            }

            return Ok(new {message = "User updated successfully"});
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest(new { message = "Failed to update user - DbUpdateConcurrencyException: " + ex.Message });
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = "Failed to update user - DbUpdateException: " + ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Failed to update user - Exception: " + ex.Message });
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var result = await _userRepository.DeleteUserAsync(id);
        if (!result.Succeeded)
            return BadRequest(new {error = $"{result.Errors}"});

        return Ok(new {message = "User deleted successfully"});
    }
}