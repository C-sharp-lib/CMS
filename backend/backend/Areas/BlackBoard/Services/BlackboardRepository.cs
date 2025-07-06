using backend.Areas.BlackBoard.Models;

namespace backend.Areas.BlackBoard.Services;

public class BlackboardRepository : IBlackboardRepository
{
    public async Task<IEnumerable<Course>> GetCourseListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Course> GetCourseAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Student>> GetStudentListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Student> GetStudentAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Teacher>> GetTeacherListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Teacher> GetTeacherAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StudentCourse>> GetStudentCourseListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<StudentCourse> GetStudentCourseAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CourseCountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> StudentCountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> TeacherCountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> TeacherCourseCountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> StudentCourseCountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CourseModuleCountAsync(int courseId)
    {
        throw new NotImplementedException();
    }
}