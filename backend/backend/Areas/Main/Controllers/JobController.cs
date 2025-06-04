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
public class JobController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<JobController> _logger;
    private readonly IJobNoteRepository _jobNoteRepository;
    private readonly IJobTaskRepository _jobTaskRepository;
    public JobController(ApplicationDbContext context, IJobRepository jobRepository, ILogger<JobController> logger,
        IJobNoteRepository jobNoteRepository, IJobTaskRepository jobTaskRepository)
    {
        _context = context;
        _jobRepository = jobRepository;
        _logger = logger;
        _jobNoteRepository = jobNoteRepository;
        _jobTaskRepository = jobTaskRepository;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
    {
        return Ok(await _jobRepository.GetAllJobsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Job>> GetJob(int id)
    {
        var job = await _jobRepository.GetJobByIdAsync(id);
        return Ok(job);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Job>> UpdateJob(int id, [FromBody] UpdateJobViewModel model)
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
            await _jobRepository.UpdateJobAsync(id, model);
            return Ok(new {message = "Job updated"});
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest(new {message = "DbConcurrencyUpdateException: " + ex.Message});
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new {message = "DbUpdateException: " + ex.Message});
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpPost]
    public async Task<ActionResult<Job>> CreateJob([FromBody] AddJobViewModel model )
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
            await _jobRepository.CreateJobAsync(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Job>> DeleteJob(int id)
    {
        await _jobRepository.GetJobByIdAsync(id);
        return Ok(await _jobRepository.DeleteJobAsync(id));
    }

    [HttpGet("jobCount")]
    public async Task<ActionResult<int>> GetJobCount()
    {
        return Ok(await _jobRepository.CountAllJobsAsync());
    }

    [HttpGet("notes")]
    public async Task<ActionResult<IEnumerable<JobNotes>>> JobNotes()
    {
        return Ok(await _jobNoteRepository.GetAllJobNotesAsync());
    }

    [HttpGet("notes/{id}")]
    public async Task<ActionResult<JobNotes>> GetJobNotes(int id)
    {
        var jobNote = await _jobNoteRepository.GetJobNoteById(id);
        return Ok(jobNote);
    }
    
    [HttpGet("job/{jobId}")]
    public async Task<ActionResult<IEnumerable<JobNotes>>> GetJobNotesByJobId(int jobId)
    {
        var jobNote = await _jobNoteRepository.GetJobNotesByJobId(jobId);
        if (jobNote == null)
        {
            return NotFound();
        }
        return Ok(jobNote);
    }

    [HttpPut("notes/{id}")]
    public async Task<ActionResult<JobNotes>> UpdateJobNotes(int id, [FromBody] UpdateJobNoteViewModel jobNote)
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
            await _jobNoteRepository.UpdateAsync(id, jobNote);
            return Ok("Job Notes updated.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Job Note with id {id} failed", ex);
            return BadRequest($"Failed to update Job Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Job Note with id {id} failed", ex);
            return BadRequest($"Failed to update Job Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Job Note with id {id} failed", ex);
            return BadRequest($"Failed to update Job Note with id - Exception {id}");
        }
    }

    [HttpPost("notes/{id}")]
    public async Task<ActionResult<JobNotes>> CreateJobNotes(int id, [FromBody] AddJobNoteViewModel jobNote)
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
            await _jobNoteRepository.AddAsync(id, jobNote);
            return Ok(new {message = "Job Notes created"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult<JobNotes>> DeleteJobNotes(int id)
    {
        await _jobNoteRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("notes/count")]
    public async Task<ActionResult<int>> GetNotesCount()
    {
        return Ok(await _jobNoteRepository.CountAsync());
    }

    [HttpGet("jobTasks")]
    public async Task<ActionResult<IEnumerable<JobTask>>> GetJobTasks()
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
            return Ok(await _jobTaskRepository.GetJobTasks());
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception at retrieving job tasks {ex.Message}");
        }
    }

    [HttpGet("jobTasks/{id}")]
    public async Task<ActionResult<JobTask>> GetJobTask(int id)
    {
        try
        {
            return Ok(await _jobTaskRepository.GetJobTask(id));
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception at retrieving job tasks with the id: {id} - {ex.Message}");
        }
    }

    [HttpPost("jobTasks")]
    public async Task<ActionResult<JobTask>> CreateJobTask([FromBody] AddJobTaskViewModel jobTask)
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
            await _jobTaskRepository.CreateJobTask(jobTask);
            return Ok("Job Task Created.");
        }
        catch (Exception ex)
        {
            return BadRequest($"There was an Exception when creating a job task - {ex.Message}");
        }
    }

    [HttpPut("jobTasks/{id}")]
    public async Task<ActionResult<JobTask>> UpdateJobTask(int id, [FromBody] UpdateJobTaskViewModel jobTask)
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
            await _jobTaskRepository.UpdateJobTask(id, jobTask);
            return Ok("Job Task Updated.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest($"DbUpdateConcurrencyException: {ex.Message}");
        }
        catch (DbUpdateException ex)
        {
            return BadRequest($"DbUpdateException: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception: {ex.Message}");
        }
    }

    [HttpDelete("jobTasks/{id}")]
    public async Task<ActionResult<JobTask>> DeleteJobTask(int id)
    {
        await _jobTaskRepository.DeleteJobTask(id);
        return Ok("Job Task Deleted.");
    }

    [HttpGet("jobTasks/count")]
    public async Task<ActionResult<int>> GetJobTasksCount()
    {
        return Ok(await _jobTaskRepository.CountJobTasks());
    }
}