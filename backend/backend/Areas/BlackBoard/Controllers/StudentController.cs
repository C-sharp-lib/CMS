using backend.Areas.BlackBoard.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Controllers;


[ApiController]
[Area("BlackBoard")]
[Route("api/[area]/[controller]")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IStudentRepository _studentRepository;
    private readonly IGradedItemsRepository _gradedItemsRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly IAssignmentRepository _assignmentRepository;
    public StudentController(ILogger<StudentController> logger, ApplicationDbContext context,
        IStudentRepository studentRepository, IGradedItemsRepository gradedItemsRepository, 
        ICourseRepository courseRepository, IModuleRepository moduleRepository, IAssignmentRepository assignmentRepository)
    {
        _logger = logger;
        _context = context;
        _studentRepository = studentRepository;
        _gradedItemsRepository = gradedItemsRepository;
        _courseRepository = courseRepository;
        _moduleRepository = moduleRepository;
        _assignmentRepository = assignmentRepository;
    }
}