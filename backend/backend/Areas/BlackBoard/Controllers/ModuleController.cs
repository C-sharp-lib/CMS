using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.BlackBoard.Controllers;


[ApiController]
[Area("BlackBoard")]
[Route("api/[area]/[controller]")]
public class ModuleController : ControllerBase
{
    private readonly ILogger<ModuleController> _logger;
    private readonly ApplicationDbContext _context;

    public ModuleController(ILogger<ModuleController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
}