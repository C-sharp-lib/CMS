using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Controllers;


[ApiController]
[Area("BlackBoard")]
[Route("api/[area]/[controller]")]
public class AssignmentController : ControllerBase
{
    private readonly ILogger<AssignmentController> _logger;
    private readonly ApplicationDbContext _context;

    public AssignmentController(ILogger<AssignmentController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
}