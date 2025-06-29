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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace backend.Configuration;

public static class AppConfiguration
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
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

                services.AddIdentityCore<User>(options =>
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

        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

        services.AddAuthentication(options =>
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
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Login";
            options.Cookie.Name = "crm_cookie";
            options.Cookie.HttpOnly = true;
            options.LogoutPath = "/Identity/Logout";
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Identity/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.Cookie.Expiration = TimeSpan.FromMinutes(30);
            options.Cookie.MaxAge = TimeSpan.FromMinutes(31);
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        });
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
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<ILeadRepository, LeadRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyNoteRepository, CompanyNoteRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<ICampaignRepository, CampaignRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUserNoteRepository, UserNoteRepository>();
        services.AddScoped<ICampaignNotesRepository, CampaignNotesRepository>();
        services.AddScoped<ILeadNotesRepository, LeadNotesRepository>();
        services.AddScoped<IContactNotesRepository, ContactNotesRepository>();
        services.AddScoped<IJobNoteRepository, JobNoteRepository>();
        services.AddScoped<IAnalyticRepository, AnalyticRepository>();
        services.AddScoped<ILeadTaskRepository, LeadTaskRepository>();
        services.AddScoped<ICampaignTaskRepository, CampaignTaskRepository>();
        services.AddScoped<ICompanyTaskRepository, CompanyTaskRepository>();
        services.AddScoped<ICompanyContactsRepository, CompanyContactsRepository>();
        //-------------  Blog Services   ----------------//
        services.AddScoped<IPostRepository, PostRepository>();
        //-------------  Ecommerce Services   ----------------//
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        //-------------  Tasks Services   ----------------//
        services.AddScoped<ITaskNotesRepository, TaskNotesRepository>();
        services.AddScoped<IUserTasksRepository, UserTasksRepository>();
        services.AddScoped<IJobTaskRepository, JobTaskRepository>();
        services.AddScoped<ICampaignTaskRepository, CampaignTaskRepository>();
        services.AddScoped<ICompanyTaskRepository, CompanyTaskRepository>();
        services.AddScoped<IContactTasksRepository, ContactTasksRepository>();
        services.AddHttpContextAccessor();
        services.AddSerilog();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}