using backend.Areas.BlackBoard.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Controllers;


[ApiController]
[Area("BlackBoard")]
[Route("api/[area]/[controller]")]
public class SyllabusController : ControllerBase
{
    private readonly ILogger<SyllabusController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly ICourseRepository _courseRepository;
    private readonly ITeacherRepository _teacherRepository;

    public SyllabusController(ILogger<SyllabusController> logger, ApplicationDbContext context,
        ICourseRepository courseRepository, ITeacherRepository teacherRepository)
    {
        _logger = logger;
        _context = context;
        _courseRepository = courseRepository;
        _teacherRepository = teacherRepository;
    }
    
}