using System.Collections;
using backend.Areas.BlackBoard.Models;

namespace backend.Areas.BlackBoard.Services;

public interface IBlackboardRepository
{
    Task<IEnumerable<Course>> GetCourseListAsync();
    Task<Course> GetCourseAsync(int id);
    Task<IEnumerable<Student>> GetStudentListAsync();
    Task<Student> GetStudentAsync(string id);
    Task<IEnumerable<Teacher>> GetTeacherListAsync();
    Task<Teacher> GetTeacherAsync(string id);
    Task<IEnumerable<StudentCourse>> GetStudentCourseListAsync();
    Task<StudentCourse> GetStudentCourseAsync(int id);
    Task<int> CourseCountAsync();
    Task<int> StudentCountAsync();
    Task<int> TeacherCountAsync();
    Task<int> TeacherCourseCountAsync();
    Task<int> StudentCourseCountAsync();
    Task<int> CourseModuleCountAsync(int courseId);
}