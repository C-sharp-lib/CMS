using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Controllers;

[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyNoteRepository _companyNoteRepository;
    private readonly ICompanyTaskRepository _companyTaskRepository;
    public CompanyController(ApplicationDbContext context, ICompanyRepository companyRepository, ILogger<CompanyController> logger, 
        ICompanyNoteRepository companyNoteRepository, ICompanyTaskRepository companyTaskRepository)
    {
        _context = context;
        _companyRepository = companyRepository;
        _logger = logger;
        _companyNoteRepository = companyNoteRepository;
        _companyTaskRepository = companyTaskRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
    {
        var companies = await _context.Companies.ToListAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompany(int id)
    {
        return Ok(await _companyRepository.GetCompanyById(id));
    }

    [HttpPost]
    public async Task<ActionResult<Company>> CreateCompany([FromBody] AddCompanyViewModel company)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new {Errors = errors});
        }

        try
        {
            var companies = await _companyRepository.AddAsync(company);
            return Ok(companies);
        }
        catch (Exception ex)
        {
            return BadRequest(new {Error = ex.Message});
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyViewModel company)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _companyRepository.UpdateAsync(id, company);
            return Ok("Company updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Company with id {id} failed", ex);
            return BadRequest($"Failed to update Company with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Company with id {id} failed", ex);
            return BadRequest($"Failed to update Company with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Company with id {id} failed", ex);
            return BadRequest($"Failed to update Company with id - Exception {id}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCompany(int id)
    {
        await _companyRepository.DeleteAsync(id);
        return Ok("Company deleted");
    }

    [HttpGet("notes")]
    public async Task<ActionResult<IEnumerable<CampaignNotes>>> CampaignNotes()
    {
        return Ok(await _companyNoteRepository.GetAllCompanyNotesAsync());
    }

    [HttpGet("notes/{id}")]
    public async Task<ActionResult<CompanyNotes>> GetCompanyNotes(int id)
    {
        return Ok(await _companyNoteRepository.GetCompanyNoteById(id));
    }

    [HttpPost("notes")]
    public async Task<ActionResult<CompanyNotes>> CreateCompanyNotes(int companyId, [FromBody] AddCompanyNoteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new {Errors = errors});
        }

        try
        {
            await _companyNoteRepository.AddAsync(companyId, model);
            return Ok(new {message = "Note created successfully"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {Errors = ex.Message});
        }
    }

    [HttpPut("notes/{id}")]
    public async Task<ActionResult> UpdateCompanyNotes(int id, [FromBody] UpdateCompanyNoteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new {Errors = errors});
        }

        try
        {
            await _companyNoteRepository.UpdateAsync(id, model);
            return Ok(new {message = "Note updated successfully"});
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Contact Note with id {id} failed", ex.Message);
            return BadRequest(new { Errors = $"Failed to update Contact Note with id - DbUpdateConcurrencyException {id}" });
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Contact Note with id {id} failed", ex.Message);
            return BadRequest(new {Errors = $"Failed to update Contact Note with id - DbUpdateException {id}" });
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Contact Note with id {id} failed", ex.Message);
            return BadRequest(new {Errors = $"Failed to update Contact Note with id - Exception {id}"});
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult> DeleteCompanyNotes(int id)
    {
        await _companyNoteRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("notes/count")]
    public async Task<ActionResult<int>> GetCompanyNotesCount()
    {
        return Ok(await _companyNoteRepository.CountAsync());
    }

    [HttpGet("tasks")]
    public async Task<ActionResult<IEnumerable<CompanyTask>>> GetCompanyTasks()
    {
        return Ok(await _companyTaskRepository.GetAllAsync());
    }

    [HttpGet("tasks/{id}")]
    public async Task<ActionResult<CompanyTask>> GetCompanyTasks(int id)
    {
        return Ok(await _companyTaskRepository.GetByIdAsync(id));
    }

    [HttpGet("company/{companyId}/tasks")]
    public async Task<ActionResult<IEnumerable<CompanyTask>>> GetCompanyTasksByCompanyId(int companyId)
    {
        var companyTasks = await _companyTaskRepository.GetByCompanyIdAsync(companyId);
        return Ok(companyTasks);
    }

    [HttpPost("tasks")]
    public async Task<ActionResult<CompanyTask>> CreateCompanyTasks(int companyId, [FromBody] AddCompanyTaskViewModel companyTasks)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new {Errors = errors});
        }

        try
        {
            await _companyTaskRepository.AddAsync(companyId, companyTasks);
            return Ok(new {message = "Tasks created successfully"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {Errors = ex.Message});
        }
    }

    [HttpPut("tasks/{id}")]
    public async Task<ActionResult> UpdateCompanyTask(int id, [FromBody] UpdateCompanyTaskViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new { Errors = errors });
        }

        try
        {
            await _companyTaskRepository.UpdateAsync(id, model);
            return Ok(new { message = "Tasks updated successfully" });
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Company Task with id {id} failed", ex.Message);
            return BadRequest(new
                { Errors = $"Failed to update Company Task with id - DbUpdateConcurrencyException {id}" });
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Company Task with id {id} failed", ex.Message);
            return BadRequest(new { Errors = $"Failed to update Company Task with id - DbUpdateException {id}" });
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Company Task with id {id} failed", ex.Message);
            return BadRequest(new { Errors = $"Failed to update Company Task with id - Exception {id}" });
        }
    }

    [HttpDelete("companyTasks/{id}")]
    public async Task<ActionResult> DeleteCompanyTasks(int id)
    {
        await _companyTaskRepository.DeleteAsync(id);
        return Ok("CompanyTasks deleted");
    }

    [HttpGet("companyTasks/count")]
    public async Task<ActionResult<int>> GetCompanyTasksCount()
    {
        return Ok(await _companyTaskRepository.CountAsync());
    }
}