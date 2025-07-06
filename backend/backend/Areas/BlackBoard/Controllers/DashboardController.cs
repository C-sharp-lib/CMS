using backend.Areas.BlackBoard.Models;
using backend.Areas.BlackBoard.Models.ViewModels;
using backend.Areas.BlackBoard.Services;
using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Controllers;

[ApiController]
[Area("BlackBoard")]
[Route("api/[area]/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ILogger<DashboardController> _logger;
    private readonly UserManager<Teacher> _teacherManager;
    private readonly SignInManager<Teacher> _teacherSignInManager;
    private readonly UserManager<Student> _studentManager;
    private readonly SignInManager<Student> _studentSignInManager;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IGradedItemsRepository _gradedItemsRepository;
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context, UserManager<Teacher> teacherManager,
        UserManager<Student> studentManager, SignInManager<Teacher> teacherSignInManager,
        SignInManager<Student> studentSignInManager, ITeacherRepository teacherRepository,
        IStudentRepository studentRepository, ICourseRepository courseRepository, IGradedItemsRepository gradedItemsRepository,
        ILogger<DashboardController> logger)
    {
        _context = context;
        _teacherManager = teacherManager;
        _teacherSignInManager = teacherSignInManager;
        _studentManager = studentManager;
        _studentSignInManager = studentSignInManager;
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _gradedItemsRepository = gradedItemsRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DashboardViewModel>>> Dashboard(DashboardViewModel model)
    {
        return new List<DashboardViewModel>();
    }
}