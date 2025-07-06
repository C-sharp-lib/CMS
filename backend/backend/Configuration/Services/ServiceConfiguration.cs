using backend.Areas.BlackBoard.Services;
using backend.Areas.Blog.Services;
using backend.Areas.Communication.Services;
using backend.Areas.Ecommerce.Services;
using backend.Areas.Identity.Services;
using backend.Areas.Main.Services;
using backend.Areas.Utility.Services;
using backend.Data;

namespace backend.Configuration.Services;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServiceConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
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
        //-------------  BlackBoard Services   ----------------//
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IGradedItemsRepository, GradedItemsRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IBlackboardRepository, BlackboardRepository>();
        return services;
    }
}