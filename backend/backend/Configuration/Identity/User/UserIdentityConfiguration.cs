using System.Text;
using backend.Areas.Identity.Models;
using backend.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace backend.Configuration.Identity.User;

public static class UserIdentityConfiguration
{
    public static IServiceCollection AddUserIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
             
                services.AddIdentityCore<Areas.Identity.Models.User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddUserManager<UserManager<Areas.Identity.Models.User>>()
            .AddSignInManager<SignInManager<Areas.Identity.Models.User>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}