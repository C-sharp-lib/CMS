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
public class CampaignController : ControllerBase
{
    private readonly ICampaignRepository _repository;
    private readonly ILogger<CampaignController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly ICampaignNotesRepository _campaignNotesRepository;
    private readonly ICampaignTaskRepository _campaignTaskRepository;
    public CampaignController(ICampaignRepository repository, ILogger<CampaignController> logger, ApplicationDbContext context,
        ICampaignNotesRepository campaignNotesRepository, ICampaignTaskRepository campaignTaskRepository)
    {
        _repository = repository;
        _context = context;
        _logger = logger;
        _campaignNotesRepository = campaignNotesRepository;
        _campaignTaskRepository = campaignTaskRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetAll()
    {
        var campaigns = await _repository.GetAllAsync();
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Campaign>> Get(int id)
    {
        var campaign = await _repository.GetByIdAsync(id);
        if (campaign == null)
            return NotFound();
        return Ok(campaign);
    }

    [HttpPost]
    public async Task<ActionResult<Campaign>> Create([FromBody] Campaign campaign)
    {
        var created = await _repository.AddAsync(campaign);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCampaign(int id, [FromBody] Campaign campaign)
    {
        if (id != campaign.Id)
            return BadRequest();

        if (!await _repository.ExistsAsync(id))
            return NotFound();

        await _repository.UpdateAsync(campaign);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCampaign(int id)
    {
        if (!await _repository.ExistsAsync(id))
            return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("campaignCount")]
    public async Task<ActionResult<int>> GetCampaignCount()
    {
        var campaigns = await _repository.CountAsync();
        return Ok(campaigns);
    }

    [HttpGet("notes")]
    public async Task<ActionResult<IEnumerable<CampaignNotes>>> GetCampaignNotes()
    {
        var campaignNotes = await _campaignNotesRepository.GetCampaignNotesAsync();
        return Ok(campaignNotes);
    }

    [HttpGet("campaign/{campaignId}/notes")]
    public async Task<ActionResult<IEnumerable<CampaignNotes>>> GetCampaignNotesByCampaignId(int campaignId)
    {
        var campaignNotes = await _campaignNotesRepository.GetCampaignNotesbyCampaignIdAsync(campaignId);
        return Ok(campaignNotes);
    }

    [HttpGet("notes/{id}")]
    public async Task<ActionResult<CampaignNotes>> GetCampaignNotes(int id)
    {
        var campaignNote = await _campaignNotesRepository.GetCampaignNoteById(id);
        return Ok(campaignNote);
    }

    [HttpPost("notes")]
    public async Task<ActionResult<CampaignNotes>> CreateCampaignNotes(int campaignId, [FromBody] AddCampaignNoteViewModel model)
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
            await _campaignNotesRepository.AddAsync(campaignId, model);
            return Ok(new {message = "Campaign Note created"});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("notes/{id}")]
    public async Task<ActionResult<CampaignNotes>> UpdateCampaignNotes(int id, [FromBody] UpdateCampaignNoteViewModel model)
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
            await _campaignNotesRepository.UpdateAsync(id, model);
            return Ok(new {message = "Campaign Note updated"});
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Note with id - Exception {id}");
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult<CampaignNotes>> DeleteCampaignNotes(int id)
    {
        await _campaignNotesRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("notes/count")]
    public async Task<ActionResult<int>> GetCampaignNoteCount()
    {
        var campaignNoteCount = await _campaignNotesRepository.CountAsync();
        return Ok(campaignNoteCount);
    }

    [HttpGet("tasks")]
    public async Task<ActionResult<IEnumerable<Task>>> GetCampaignTasks()
    {
        var campaignTasks = await _campaignTaskRepository.GetAllCampaignTasks();
        return Ok(campaignTasks);
    }

    [HttpGet("campaign/{campaignId}/tasks")]
    public async Task<ActionResult<IEnumerable<Task>>> GetCampaignTasksByCampaignId(int campaignId)
    {
        var campaignTasks = await _campaignTaskRepository.GetCampaignTasksByCampaignId(campaignId);
        return Ok(campaignTasks);
    }

    [HttpGet("tasks/{id}")]
    public async Task<ActionResult<CampaignTasks>> GetCampaignTaskById(int id)
    {
        var campaignTask = await _campaignTaskRepository.GetCampaignTask(id);
        return Ok(campaignTask);
    }

    [HttpGet("tasks/count")]
    public async Task<ActionResult<int>> GetCampaignTaskCount()
    {
        return Ok(await _campaignTaskRepository.CountCampaignTasks());
    }

    [HttpPost("tasks")]
    public async Task<ActionResult<CampaignTasks>> CreateCampaignTasks(int campaignId,
        [FromBody] AddCampaignTaskViewModel model)
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
            await _campaignTaskRepository.AddCampaignTask(campaignId, model);
            return Ok(new {message = "Campaign Task created"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {Errors = ex.Message});
        }
    }
    [HttpPut("tasks/{id}")]
    public async Task<ActionResult<CampaignTasks>> UpdateCampaignTask(int id, [FromBody] UpdateCampaignTaskViewModel model)
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
            await _campaignTaskRepository.UpdateCampaignTask(id, model);
            return Ok(new {message = "Campaign Task updated"});
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Campaign Task with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Task with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Campaign Task with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Task with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Campaign Task with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Task with id - Exception {id}");
        }
    }
}