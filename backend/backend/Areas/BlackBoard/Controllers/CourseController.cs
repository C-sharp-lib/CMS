using backend.Areas.BlackBoard.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Controllers;

[ApiController]
[Area("BlackBoard")]
[Route("api/[area]/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly ICourseRepository _courseRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    public CourseController(ILogger<CourseController> logger, ApplicationDbContext context, ICourseRepository courseRepository, 
        ITeacherRepository teacherRepository, IStudentRepository studentRepository)
    {
        _logger = logger;
        _context = context;
        _courseRepository = courseRepository;
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
    }
}