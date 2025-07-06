using System.Text;
using System.Text.Json.Serialization;
using backend.Areas.BlackBoard.Configuration.Identity.Student;
using backend.Areas.BlackBoard.Configuration.Identity.Teacher;
using backend.Areas.BlackBoard.Models;
using backend.Areas.BlackBoard.Services;
using backend.Areas.Blog.Services;
using backend.Areas.Communication.Services;
using backend.Areas.Ecommerce.Services;
using backend.Areas.Identity.Models;
using backend.Areas.Identity.Services;
using backend.Areas.Main.Services;
using backend.Areas.Utility.Services;
using backend.Configuration.Identity.CookieAndJwt;
using backend.Configuration.Identity.User;
using backend.Configuration.Services;
using backend.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace backend.Configuration;

public static class AppConfiguration
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        /*services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));;
            */
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        services.AddDistributedMemoryCache();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("/Areas/Ecommerce/Logs/ApplicationLogs.txt")
            .CreateLogger();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("/Areas/Blog/Logs/ApplicationLogs.txt")
            .CreateLogger();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("/Areas/Communication/Logs/Communication_Logs.txt")
            .CreateLogger();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("/Areas/Utility/Logs/ApplicationLogs.txt")
            .CreateLogger();

        services.AddUserIdentityConfiguration(builder.Configuration);
        services.AddTeacherIdentityConfiguration(builder.Configuration);
        services.AddStudentIdentityConfiguration(builder.Configuration);
        services.AddCookieAndJwtConfiguration(builder.Configuration);
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowCredentials();
                
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                
                // (Optional: restrict origins in production)
            });
        });
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        services.AddControllers().AddNewtonsoftJson(options =>
        {   
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        services.AddServiceConfiguration(builder.Configuration);
        services.AddHttpContextAccessor();
        services.AddSerilog();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}