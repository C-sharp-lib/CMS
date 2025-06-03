using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Identity.Services;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webenv;

    public UserRepository(ApplicationDbContext context, UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<Role> roleManager, IConfiguration configuration, IWebHostEnvironment webenv)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _webenv = webenv;
    }

    public async Task<IdentityResult> RegisterAsync([FromBody] RegisterViewModel model)
    {
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            UserName = model.UserName,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            City = model.City,
            State = model.State,
            ZipCode = model.ZipCode,
            DateOfBirth = model.DateOfBirth,
            DateCreated = DateTime.Now
        };

        return await _userManager.CreateAsync(user, model.Password);
    }

    public async Task<string?> LoginAsync([FromBody] LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return null!;

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded)
            return null!;

        // Optional: generate a JWT token (simple example)
        var token = GenerateJwtToken(user);
        return token;
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        // This is a *basic* JWT creation example.
        // You can customize claims, expiry, secret key, etc.
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var key = System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email!)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _context.Users
            .Include(x => x.UserRoles)!
            .ThenInclude(xu => xu.Role)
            .ToListAsync();
        return users;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        var user = await _context.Users
            .Include(x => x.UserRoles)!
            .ThenInclude(xu => xu.Role)
            .Include(j => j.AssignedJobs)
            .Include(ct => ct.UserNotes)!
            .ThenInclude(ct => ct.Note)
            .Include(c => c.Contacts)!
            .ThenInclude(c => c.Jobs)
            .FirstOrDefaultAsync(xc => xc.Id == userId);
        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }

        return user;
    }

    public async Task<User> UpdateUserAsync(string id, [FromForm] UpdateUserViewModel model)
    {
        try
        {
            var existingUser = await GetUserByIdAsync(id);
            if (existingUser == null)
                throw new NullReferenceException("User not found");

            string uploadsFolder = Path.Combine(_webenv.WebRootPath, "Uploads/User");
            if (model.ImageUrl != null && model.ImageUrl?.Length > 0)
            {
                string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageUrl.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName1);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageUrl.CopyToAsync(fileStream);
                }

                existingUser.ImageUrl = uniqueFileName1;
            }

            existingUser.Name = model.Name;
            existingUser.PhoneNumber = model.PhoneNumber;
            existingUser.Address = model.Address;
            existingUser.City = model.City;
            existingUser.Description = model.Description;
            existingUser.State = model.State;
            existingUser.ZipCode = model.ZipCode;
            existingUser.DateOfBirth = model.DateOfBirth;
            await _userManager.UpdateAsync(existingUser);
            await _context.SaveChangesAsync();
            return existingUser;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
    

    public async Task<int> CountUsersAsync()
    {
        return await _context.Users.CountAsync();
    }
}