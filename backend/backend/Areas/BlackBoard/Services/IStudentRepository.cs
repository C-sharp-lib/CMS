using System.Collections;
using backend.Areas.BlackBoard.Models;
using backend.Areas.BlackBoard.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Services;

public interface IStudentRepository
{
    Task<IdentityResult> StudentRegisterAsync([FromBody] StudentRegisterViewModel model);
    Task<string?> StudentLoginAsync([FromBody] StudentLoginViewModel model);
    Task<IEnumerable<StudentCourse>> GetStudentCoursesAsync(string studentId);
    Task<StudentCourse?> GetStudentCourseAsync(string studentId, string courseId);
    Task<Student> GetStudentAsync(string studentId);
    Task UpdateStudentAsync(string id, [FromForm] UpdateStudentViewModel student);
    Task DeleteStudentAsync(string id);
    Task<int> GetStudentCountAsync(string studentId);
    Task<int> GetStudentCoursesCountAsync(string studentCourseId);
}