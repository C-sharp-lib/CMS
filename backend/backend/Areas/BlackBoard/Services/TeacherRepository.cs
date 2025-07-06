using backend.Areas.BlackBoard.Models;
using backend.Areas.BlackBoard.Models.ViewModels;
using backend.Areas.Identity.Services;
using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.BlackBoard.Services;

public class TeacherRepository : ITeacherRepository
{
    private readonly ILogger<TeacherRepository> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;

    public TeacherRepository(ApplicationDbContext context, ILogger<TeacherRepository> logger, IStudentRepository studentRepository,
        IUserRepository userRepository)
    {
        _context = context;
        _logger = logger;
        _studentRepository = studentRepository;
        _userRepository = userRepository;
    }

    public async Task<IdentityResult> TeacherRegisterAsync([FromBody] TeacherRegisterViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> TeacherLoginAsync([FromBody] TeacherLoginViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachers()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<Teacher> GetTeacher(string id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            return null;
        }
        return teacher;
    }

    public async Task<Teacher> CreateTeacher([FromBody] AddTeacherViewModel teacher)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateTeacher(string id, [FromBody] UpdateTeacherViewModel teacher)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteTeacher(string id)
    {
        var teacher = await GetTeacher(id);
        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<int> TeacherCount()
    {
        return await _context.Teachers.CountAsync();
    }

    public async Task<IEnumerable<StudentCourse>> GetTeachersStudentCoursesAsync(string teacherId)
    {
        throw new NotImplementedException();
    }

    public async Task<StudentCourse> GetTeachersStudentCourseDetailsAsync(string teacherId, string studentId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetStudentCourseCountAsync(string teacherId, int courseId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetCompleteTeachersStudentsCountAsync(string teacherId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteStudentFromCourseAsync(string teacherId, string studentId, string courseId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCourseFromTeacherListAsync(string teacherId, string courseId)
    {
        throw new NotImplementedException();
    }
}