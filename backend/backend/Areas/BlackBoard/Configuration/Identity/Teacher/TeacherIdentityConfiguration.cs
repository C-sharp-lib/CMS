using backend.Areas.BlackBoard.Models;
using backend.Data;
using Microsoft.AspNetCore.Identity;

namespace backend.Areas.BlackBoard.Configuration.Identity.Teacher;

public static class TeacherIdentityConfiguration
{
    public static IServiceCollection AddTeacherIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<Models.Teacher>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            })
            .AddRoles<BbRoles>()
            .AddRoleManager<RoleManager<BbRoles>>()
            .AddUserManager<UserManager<Models.Teacher>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}