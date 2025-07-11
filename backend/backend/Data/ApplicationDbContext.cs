using backend.Areas.BlackBoard.Models;
using backend.Areas.Blog.Models;
using backend.Areas.Communication.Models;
using backend.Areas.Ecommerce.Models;
using backend.Areas.Identity.Models;
using backend.Areas.Main.Models;
using backend.Areas.Utility.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<User, Role, string,
    IdentityUserClaim<string>, UserRoles, IdentityUserLogin<string>, IdentityRoleClaim<string>,
    IdentityUserToken<string>>(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Lead> Leads { get; set; }
    public DbSet<UserRoles> UserRoles { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<EmailMessage> Emails { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyContacts> CompanyContacts { get; set; }
    public DbSet<Analytic> Analytics { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<UserMeeting> UserMeetings { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<UserNotes> UserNotes { get; set; }
    public DbSet<UserTasks> UserTasks { get; set; }
    public DbSet<CampaignTasks> CampaignTasks { get; set; }
    public DbSet<ContactTasks> ContactTasks { get; set; }
    public DbSet<TaskNotes> TaskNotes { get; set; }
    public DbSet<LeadNotes> LeadNotes { get; set; }
    public DbSet<ContactNotes> ContactNotes { get; set; }
    public DbSet<CampaignNotes> CampaignNotes { get; set; }
    public DbSet<CompanyNotes> CompanyNotes { get; set; }
    public DbSet<JobNotes> JobNotes { get; set; }
    public DbSet<JobTask> JobTasks { get; set; }
    public DbSet<LeadTask> LeadTasks { get; set; }
    public DbSet<CompanyTask> CompanyTasks { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostCategory> Categories { get; set; }
    public DbSet<PostCategories> PostCategories { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<ConversationParticipants> ConversationParticipants { get; set; }
    public DbSet<Comment> Comments { get; set; }
    //----------------  Ecommerce Tables  ---------------------//
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategories> ProductCategories { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ProductImages> ProductImages { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<CustomerOrders> CustomerOrders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<WishListItem> WishListItems { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Syllabus> Syllabus { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<TeacherStudents> TeacherStudents { get; set; }
    public DbSet<BbRoles> BbRole { get; set; }
    public DbSet<BbTandSRoles> BbTandSRole { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(x => x.Contacts)
                .WithOne(x => x.OwnerUser)
                .HasForeignKey(x => x.OwnerUserId);
            entity.HasMany(x => x.Leads)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedBy);
            entity.HasMany(x => x.AssignedJobs)
                .WithOne(x => x.AssignedUser)
                .HasForeignKey(x => x.AssignedUserId);
            entity.HasMany(x => x.CreatedJobs)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId);
            entity.HasMany(x => x.Tasks)
                .WithOne(c => c.AssignedToUser)
                .HasForeignKey(c => c.AssignedToUserId)
                .HasPrincipalKey(u => u.Id);
            entity.HasMany(x => x.Campaigns)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .HasPrincipalKey(u => u.Id);
            entity.HasMany(x => x.Analytics)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(x => x.UserMeetings)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
            entity.HasMany(x => x.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);
            entity.HasMany(x => x.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            entity.HasMany(x => x.Customers)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            entity.HasMany(x => x.Reviews)
                .WithOne(x => x.Reviewer)
                .HasForeignKey(x => x.ReviewerId);
            entity.HasMany(p => p.Participants)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            entity.HasMany(x => x.Messages)
                .WithOne(x => x.Sender)
                .HasForeignKey(x => x.SenderId);
        });
        builder.Entity<Role>(entity => { entity.HasKey(e => e.Id); });
        builder.Entity<UserRoles>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId, e.RoleId });
            entity.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);
            entity.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);
        });
        builder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CreatedBy });
            entity.HasOne(x => x.CreatedByUser)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.CreatedBy);
            entity.HasMany(x => x.Meetings)
                .WithOne(x => x.Lead)
                .HasForeignKey(x => x.LeadId)
                .HasPrincipalKey(l => l.Id);
        });
        builder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.OwnerUserId });
            entity.HasOne(x => x.OwnerUser)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.OwnerUserId);
            entity.HasMany(x => x.Meetings)
                .WithOne(x => x.Contact)
                .HasForeignKey(x => x.ContactId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<Job>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.AssignedUserId, e.CreatedByUserId, e.ContactId });
            entity.HasOne(x => x.AssignedUser)
                .WithMany(x => x.AssignedJobs)
                .HasForeignKey(x => x.AssignedUserId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.CreatedByUser)
                .WithMany(x => x.CreatedJobs)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Contact)
                .WithMany(x => x.Jobs)
                .HasForeignKey(x => x.ContactId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CreatedByUserId });
            entity.HasOne(x => x.CreatedByUser)
                .WithMany(x => x.Campaigns)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(c => c.Leads)
                .WithMany(x => x.Campaigns)
                .UsingEntity(j => j.ToTable("CampaignLeads"));
            entity.HasMany(x => x.Contacts)
                .WithMany(c => c.Campaigns)
                .UsingEntity(j => j.ToTable("CampaignContacts"));
        });
        builder.Entity<EmailMessage>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.To, e.From });
            entity.HasOne(x => x.FromUser)
                .WithMany()
                .HasForeignKey(e => e.From)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.ToUser)
                .WithMany()
                .HasForeignKey(e => e.To)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Tasks>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.AssignedToUserId});
            entity.HasOne(c => c.AssignedToUser)
                .WithMany(t => t.Tasks)
                .HasForeignKey(c => c.AssignedToUserId)
                .OnDelete(DeleteBehavior.NoAction);
            
        });
        builder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        builder.Entity<CompanyContacts>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ContactId, e.CompanyId });
            entity.HasOne(x => x.Company)
                .WithMany(x => x.CompanyContacts)
                .HasForeignKey(x => x.CompanyId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Contact)
                .WithMany(x => x.CompanyContacts)
                .HasForeignKey(x => x.ContactId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Analytic>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CreatedByUserId });
            entity.HasOne(x => x.CreatedByUser)
                .WithMany(c => c.Analytics)
                .HasForeignKey(x => x.CreatedByUserId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Message>(entity =>
        {
            entity.HasKey(e => new {e.Id, e.SenderId, e.ConversationId});
            entity.HasOne(m => m.Sender)
                .WithMany(m => m.Messages)
                .HasForeignKey(m => m.SenderId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(m => m.Conversation)
                .WithMany(m => m.Messages)
                .HasForeignKey(m => m.ConversationId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.OrganizerId });
            entity.HasMany(x => x.UserMeetings)
                .WithOne(m => m.Meeting)
                .HasForeignKey(m => m.MeetingId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Organizer)
                .WithMany(c => c.Meetings)
                .HasForeignKey(j => j.OrganizerId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<UserMeeting>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId, e.MeetingId });
            entity.HasOne(mu => mu.Meeting)
                .WithMany(c => c.UserMeetings)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(mu => mu.User)
                .WithMany(c => c.UserMeetings)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        builder.Entity<UserNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.NoteId, e.UserId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.UserNotes)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.User)
                .WithMany(n => n.UserNotes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<UserTasks>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.TaskId, e.UserId });
            entity.HasOne(n => n.Tasks)
                .WithMany(n => n.UserTasks)
                .HasForeignKey(n => n.TaskId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.User)
                .WithMany(n => n.UserTasks)
                .HasForeignKey(n => n.UserId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CampaignTasks>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.TaskId, e.CampaignId });
            entity.HasOne(n => n.Tasks)
                .WithMany(n => n.CampaignTasks)
                .HasForeignKey(n => n.TaskId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Campaign)
                .WithMany(n => n.CampaignTasks)
                .HasForeignKey(n => n.CampaignId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<ContactTasks>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.TaskId, e.ContactId });
            entity.HasOne(n => n.Tasks)
                .WithMany(n => n.ContactTasks)
                .HasForeignKey(n => n.TaskId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Contact)
                .WithMany(n => n.ContactTasks)
                .HasForeignKey(n => n.ContactId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<TaskNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.TaskId, e.NoteId });
            entity.HasOne(t => t.Note)
                .WithMany(n => n.TaskNotes)
                .HasForeignKey(t => t.NoteId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(t => t.Task)
                .WithMany(n => n.TaskNotes)
                .HasForeignKey(t => t.TaskId)
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<LeadNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.LeadId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.LeadNotes)
                .HasForeignKey(n => n.NoteId)
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Lead)
                .WithMany(n => n.LeadNotes)
                .HasForeignKey(n => n.LeadId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<ContactNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ContactId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.ContactNotes)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Contact)
                .WithMany(n => n.ContactNotes)
                .HasForeignKey(n => n.ContactId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CompanyNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CompanyId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.CompanyNotes)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Company)
                .WithMany(n => n.CompanyNotes)
                .HasForeignKey(n => n.CompanyId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CampaignNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CampaignId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.CampaignNotes)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Campaign)
                .WithMany(n => n.CampaignNotes)
                .HasForeignKey(n => n.CampaignId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<JobNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.JobId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.JobNotes)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Job)
                .WithMany(n => n.JobNotes)
                .HasForeignKey(n => n.JobId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<JobTask>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.JobId, e.TaskId });
            entity.HasOne(n => n.Job)
                .WithMany(n => n.JobTasks)
                .HasForeignKey(n => n.JobId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(g => g.Tasks)
                .WithMany(n => n.JobTasks)
                .HasForeignKey(n => n.TaskId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<LeadTask>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.LeadId, e.TaskId });
            entity.HasOne(n => n.Lead)
                .WithMany(n => n.LeadTasks)
                .HasForeignKey(n => n.LeadId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Tasks)
                .WithMany(n => n.LeadTasks)
                .HasForeignKey(n => n.TaskId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CompanyTask>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CompanyId, e.TaskId });
            entity.HasOne(n => n.Company)
                .WithMany(n => n.CompanyTasks)
                .HasForeignKey(n => n.CompanyId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Tasks)
                .WithMany(n => n.CompanyTasks)
                .HasForeignKey(n => n.TaskId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        //----------------  Blog Tables  ---------------------//
        builder.Entity<Post>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.AuthorId });
            entity.HasOne(n => n.Author)
                .WithMany(n => n.Posts)
                .HasForeignKey(n => n.AuthorId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(c => c.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<PostCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        builder.Entity<PostCategories>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CategoryId, e.PostId });
            entity.HasOne(n => n.PostCategory)
                .WithMany(n => n.PostCategories)
                .HasForeignKey(n => n.CategoryId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Post)
                .WithMany(n => n.PostCategories)
                .HasForeignKey(n => n.PostId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.AuthorId, e.PostId });
            entity.HasOne(n => n.Author)
                .WithMany(n => n.Comments)
                .HasForeignKey(n => n.AuthorId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(n => n.Post)
                .WithMany(n => n.Comments)
                .HasForeignKey(n => n.PostId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });
        //----------------  Ecommerce Tables  ---------------------//
        builder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        builder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
        builder.Entity<ProductCategories>(entity =>
        {
            entity.HasKey(x => new { x.Id, x.ProductId, x.CategoryId });
            entity.HasOne(n => n.Product)
                .WithMany(n => n.ProductCategories)
                .HasForeignKey(n => n.ProductId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(c => c.Category)
                .WithMany(n => n.ProductCategories)
                .HasForeignKey(n => n.CategoryId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        builder.Entity<ProductImages>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProductId, e.ImageId });
            entity.HasOne(n => n.Product)
                .WithMany(n => n.ProductImages)
                .HasForeignKey(n => n.ProductId)
                .HasPrincipalKey(c => c.Id);
            entity.HasOne(n => n.Image)
                .WithMany(n => n.ProductImages)
                .HasForeignKey(n => n.ImageId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<Address>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CustomerId });
            entity.HasOne(n => n.Customer)
                .WithMany(n => n.Addresses)
                .HasForeignKey(n => n.CustomerId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<Order>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId });
            entity.HasOne(x => x.User)
                .WithMany(n => n.Orders)
                .HasForeignKey(n => n.UserId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.OrderId, e.ProductId });
            entity.HasOne(n => n.Order)
                .WithMany(n => n.OrderItems)
                .HasForeignKey(n => n.OrderId)
                .HasPrincipalKey(c => c.Id);
            entity.HasOne(n => n.Product)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(n => n.ProductId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<Payment>(entity =>
        {
            entity.HasKey(x => new { x.Id, x.OrderId });
            entity.HasOne(n => n.Order)
                .WithMany(n => n.Payments)
                .HasForeignKey(n => n.OrderId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CustomerId });
            entity.HasOne(n => n.Customer)
                .WithMany(n => n.Carts)
                .HasForeignKey(n => n.CustomerId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProductId, e.CartId });
            entity.HasOne(n => n.Product)
                .WithMany(n => n.CartItems)
                .HasForeignKey(n => n.ProductId)
                .HasPrincipalKey(c => c.Id);
            entity.HasOne(n => n.Cart)
                .WithMany(n => n.CartItems)
                .HasForeignKey(n => n.CartId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<WishListItem>(entity =>
        {
            entity.HasKey(x => new { x.Id, x.ProductId, x.CustomerId });
            entity.HasOne(n => n.Product)
                .WithMany(n => n.WishListItems)
                .HasForeignKey(n => n.ProductId)
                .HasPrincipalKey(c => c.Id);
            entity.HasOne(n => n.Customer)
                .WithMany(n => n.WishListItems)
                .HasForeignKey(n => n.CustomerId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<Review>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProductId, e.ReviewerId });
            entity.HasOne(n => n.Product)
                .WithMany(n => n.Reviews)
                .HasForeignKey(n => n.ProductId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Reviewer)
                .WithMany(n => n.Reviews)
                .HasForeignKey(n => n.ReviewerId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId });
            entity.HasOne(n => n.User)
                .WithMany(n => n.Customers)
                .HasForeignKey(n => n.UserId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CustomerOrders>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.OrderId, e.CustomerId });
            entity.HasOne(n => n.Order)
                .WithMany(n => n.CustomerOrders)
                .HasForeignKey(n => n.OrderId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(n => n.Customer)
                .WithMany(n => n.CustomerOrders)
                .HasForeignKey(n => n.CustomerId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Conversation>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasMany(c => c.Participants)
                .WithOne(n => n.Conversation)
                .HasForeignKey(n => n.ConversationId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(m => m.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(n => n.ConversationId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<ConversationParticipants>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.UserId, c.ConversationId });
            entity.HasOne(c => c.User)
                .WithMany(c => c.Participants)
                .HasForeignKey(c => c.UserId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Conversation)
                .WithMany(c => c.Participants)
                .HasForeignKey(c => c.ConversationId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.StudentId, c.CourseId });
            entity.HasOne(c => c.Student)
                .WithMany(cx => cx.StudentCourses)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(c => c.CourseId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<TeacherStudents>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.StudentId, c.TeacherId });
            entity.HasOne(x => x.Student)
                .WithMany(x => x.Teachers)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Teacher)
                .WithMany(x => x.Students)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Teacher>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasMany(c => c.Students)
                .WithOne(s => s.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(v => v.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(v => v.Exams)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(v => v.Quizzes)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(v => v.Assignments)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(v => v.Projects)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
        });
                builder.Entity<Student>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasMany(c => c.StudentCourses)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(p => p.Projects)
                .WithOne(p => p.Student)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(e => e.Exams)
                .WithOne(e => e.Student)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(e => e.Assignments)
                .WithOne(e => e.Student)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(p => p.Teachers)
                .WithOne(e => e.Student)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(e => e.Quizzes)
                .WithOne(e => e.Student)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<BbRoles>(entity =>
        {
            entity.HasKey(c => c.Id);
        });
        builder.Entity<BbTandSRoles>(entity =>
        {
            entity.HasKey(c => new {c.Id, c.RoleId, c.TeacherId, c.StudentId});
            entity.HasOne(c => c.Student)
                .WithMany(c => c.Roles)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Teacher)
                .WithMany(c => c.Roles)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Role)
                .WithMany(c => c.BbUserRoles)
                .HasForeignKey(c => c.RoleId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
   
        builder.Entity<Course>(entity =>
        {
            entity.HasKey(c => new {c.Id, c.TeacherId, c.SyllabusId});
            entity.HasOne(c => c.Teacher)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Syllabus)
                .WithOne(c => c.Course)
                .HasForeignKey<Course>(c => c.SyllabusId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(c => c.Modules)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(x => x.StudentCourses)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Syllabus>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.CourseId });
            entity.HasOne(c => c.Course)
                .WithOne(c => c.Syllabus)
                .HasForeignKey<Syllabus>(c => c.CourseId)
                .HasPrincipalKey<Course>(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Exam>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.StudentId, c.TeacherId, c.ModuleId });
            entity.HasOne(c => c.Student)
                .WithMany(s => s.Exams)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Teacher)
                .WithMany(c => c.Exams)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Module)
                .WithMany(c => c.Exams)
                .HasForeignKey(c => c.ModuleId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(c => c.Questions)
                .WithOne(c => c.Exam)
                .HasForeignKey(c => c.ExamId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Quiz>(entity =>
        {
            entity.HasKey(c => new {c.Id, c.StudentId, c.TeacherId, c.ModuleId});
            entity.HasOne(c => c.Student)
                .WithMany(s => s.Quizzes)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Teacher)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Module)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(c => c.ModuleId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(c => c.Questions)
                .WithOne(c => c.Quiz)
                .HasForeignKey(c => c.QuizId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Assignment>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.StudentId, c.TeacherId, c.ModuleId });
            entity.HasOne(c => c.Student)
                .WithMany(s => s.Assignments)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Teacher)
                .WithMany(c => c.Assignments)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Module)
                .WithMany(c => c.Assignments)
                .HasForeignKey(c => c.ModuleId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(c => c.Questions)
                .WithOne(c => c.Assignment)
                .HasForeignKey(c => c.AssignmentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Project>(entity =>
        {
            entity.HasKey(c => new {c.Id, c.StudentId, c.TeacherId, c.ModuleId });
            entity.HasOne(c => c.Student)
                .WithMany(s => s.Projects)
                .HasForeignKey(c => c.StudentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Teacher)
                .WithMany(c => c.Projects)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Module)
                .WithMany(c => c.Projects)
                .HasForeignKey(c => c.ModuleId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(c => c.Questions)
                .WithOne(c => c.Project)
                .HasForeignKey(c => c.ProjectId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Question>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.ExamId, c.QuizId, c.AssignmentId, c.ProjectId });
            entity.HasOne(c => c.Exam)
                .WithMany(c => c.Questions)
                .HasForeignKey(c => c.ExamId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Quiz)
                .WithMany(c => c.Questions)
                .HasForeignKey(c => c.QuizId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Assignment)
                .WithMany(c => c.Questions)
                .HasForeignKey(c => c.AssignmentId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(c => c.Project)
                .WithMany(c => c.Questions)
                .HasForeignKey(c => c.ProjectId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(v => v.Choices)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Choice>(entity =>
        {
            entity.HasKey(c => new {c.Id, c.QuestionId});
            entity.HasOne(c => c.Question)
                .WithMany(c => c.Choices)
                .HasForeignKey(c => c.QuestionId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }
}