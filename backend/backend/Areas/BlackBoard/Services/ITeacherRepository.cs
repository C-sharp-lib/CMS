using backend.Areas.BlackBoard.Models;
using backend.Areas.BlackBoard.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Services;

public interface ITeacherRepository
{
    Task<IdentityResult> TeacherRegisterAsync([FromBody] TeacherRegisterViewModel model);
    Task<string?> TeacherLoginAsync([FromBody] TeacherLoginViewModel model);
    Task<IEnumerable<Teacher>> GetAllTeachers();
    Task<Teacher> GetTeacher(string id);
    Task<Teacher> CreateTeacher([FromBody] AddTeacherViewModel teacher);
    Task UpdateTeacher(string id, [FromBody] UpdateTeacherViewModel teacher);
    Task DeleteTeacher(string id);
    Task<int> TeacherCount();
    Task<IEnumerable<StudentCourse>> GetTeachersStudentCoursesAsync(string teacherId);
    Task<StudentCourse> GetTeachersStudentCourseDetailsAsync(string teacherId, string studentId);
    Task<int> GetStudentCourseCountAsync(string teacherId, int courseId);
    Task<int> GetCompleteTeachersStudentsCountAsync(string teacherId);
    Task DeleteStudentFromCourseAsync(string teacherId, string studentId, string courseId);
    Task DeleteCourseFromTeacherListAsync(string teacherId, string courseId);
    
}