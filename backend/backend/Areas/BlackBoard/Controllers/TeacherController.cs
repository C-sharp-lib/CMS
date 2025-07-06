using backend.Areas.BlackBoard.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Controllers;

[ApiController]
[Area("BlackBoard")]
[Route("api/[area]/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ILogger<TeacherController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly IGradedItemsRepository _gradedItemsRepository;
    private readonly IAssignmentRepository _assignmentRepository;
    public TeacherController(ILogger<TeacherController> logger, ApplicationDbContext context,
        ITeacherRepository teacherRepository, IStudentRepository studentRepository, ICourseRepository courseRepository,
        IModuleRepository moduleRepository, IGradedItemsRepository gradedItemsRepository, IAssignmentRepository assignmentRepository)
    {
        _logger = logger;
        _context = context;
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _moduleRepository = moduleRepository;
        _gradedItemsRepository = gradedItemsRepository;
        _assignmentRepository = assignmentRepository;
    }
}