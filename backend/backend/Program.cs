using System.Text;
using System.Text.Json.Serialization;
using backend.Areas.Blog.Services;
using backend.Areas.Communication.Services;
using backend.Areas.Ecommerce.Services;
using backend.Areas.Identity.Models;
using backend.Areas.Identity.Services;
using backend.Areas.Main.Services;
using backend.Areas.Utility.Services;
using backend.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace backend;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));;
        
        builder.Services.AddDistributedMemoryCache();
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
        builder.Host.UseSerilog();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.MySQL(connectionString: connectionString, tableName: "Logs")
            .CreateLogger();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add Identity
        builder.Services.AddIdentityCore<User>(options =>
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
            .AddUserManager<UserManager<User>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.MetadataAddress = jwtSettings["MetadataAddress"]!;
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.Authority = jwtSettings["Authority"];
                options.Audience = jwtSettings["Audience"];
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Login";
            options.Cookie.Name = "crm_cookie";
            options.Cookie.HttpOnly = true;
            options.LogoutPath = "/Identity/Logout";
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Identity/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        });
        builder.Services.AddCors(options =>
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
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IEmailRepository, EmailRepository>();
        builder.Services.AddScoped<ILeadRepository, LeadRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
        builder.Services.AddScoped<IMessageUserRepository, MessageUsersRepository>();
        builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<IJobRepository, JobRepository>();
        builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
        builder.Services.AddScoped<ITasksRepository, TasksRepository>();
        builder.Services.AddScoped<INoteRepository, NoteRepository>();
        builder.Services.AddScoped<IUserNoteRepository, UserNoteRepository>();
        builder.Services.AddScoped<ICampaignNotesRepository, CampaignNotesRepository>();
        builder.Services.AddScoped<ITaskNotesRepository, TaskNotesRepository>();
        builder.Services.AddScoped<ILeadNotesRepository, LeadNotesRepository>();
        builder.Services.AddScoped<IContactNotesRepository, ContactNotesRepository>();
        builder.Services.AddScoped<IJobNoteRepository, JobNoteRepository>();
        builder.Services.AddScoped<IAnalyticRepository, AnalyticRepository>();
        builder.Services.AddScoped<IJobTaskRepository, JobTaskRepository>();
        builder.Services.AddScoped<ILeadTaskRepository, LeadTaskRepository>();
        builder.Services.AddScoped<ICampaignTaskRepository, CampaignTaskRepository>();
        builder.Services.AddScoped<ICompanyTaskRepository, CompanyTaskRepository>();
        //-------------  Blog Services   ----------------//
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        //-------------  Ecommerce Services   ----------------//
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        //-------------  End of Services   ----------------//
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSerilog();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
    
}
