using backend.Areas.BlackBoard.Models;
using backend.Areas.BlackBoard.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Identity;

namespace backend.Areas.BlackBoard.Services;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StudentRepository> _logger;
    private readonly IModuleRepository _moduleRepository;

    public StudentRepository(ApplicationDbContext context, ILogger<StudentRepository> logger,
        IModuleRepository moduleRepository)
    {
        _context = context;
        _logger = logger;
        _moduleRepository = moduleRepository;
    }
    public async Task<IdentityResult> StudentRegisterAsync(StudentRegisterViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> StudentLoginAsync(StudentLoginViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StudentCourse>> GetStudentCoursesAsync(string studentId)
    {
        throw new NotImplementedException();
    }

    public async Task<StudentCourse?> GetStudentCourseAsync(string studentId, string courseId)
    {
        throw new NotImplementedException();
    }

    public async Task<Student> GetStudentAsync(string studentId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateStudentAsync(string id, UpdateStudentViewModel student)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteStudentAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetStudentCountAsync(string studentId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetStudentCoursesCountAsync(string studentCourseId)
    {
        throw new NotImplementedException();
    }
}