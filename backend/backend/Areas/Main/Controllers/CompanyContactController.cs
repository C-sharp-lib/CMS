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
public class CompanyContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactController> _logger;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyContactsRepository _companyContactsRepository;

    public CompanyContactController(IContactRepository contactRepository, ILogger<ContactController> logger, 
        ICompanyRepository companyRepository, ICompanyContactsRepository companyContactsRepository)
    {
        _contactRepository = contactRepository;
        _logger = logger;
        _companyRepository = companyRepository;
        _companyContactsRepository = companyContactsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyContacts>>> GetCompanyContacts()
    {
        return Ok(await _companyContactsRepository.GetCompanyContacts());
    }

    [HttpGet("company/{companyId}")]
    public async Task<ActionResult<CompanyContacts>> GetCompanyContactsByCompanyId(int companyId)
    {
        return Ok(await _companyContactsRepository.GetCompanyContactsByCompanyId(companyId));
    }
    [HttpGet("contact/{contactId}")]
    public async Task<ActionResult<CompanyContacts>> GetCompanyContactsByContactId(int contactId)
    {
        return Ok(await _companyContactsRepository.GetCompanyContactsByContactId(contactId));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyContacts>> GetCompanyContactById(int id)
    {
        return Ok(await _companyContactsRepository.GetCompanyContactById(id));
    }

    [HttpPost]
    public async Task<ActionResult<CompanyContacts>> CreateCompanyContact([FromBody] AddCompanyContactsViewModel model)
    {
        try
        {
            await _companyContactsRepository.CreateCompanyContact(model);
            return Ok(new {message = "Company Contact Created"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCompanyContact(int id, [FromBody] UpdateCompanyContactsViewModel model)
    {
        try
        {
            await _companyContactsRepository.UpdateCompanyContact(id, model);
            return Ok(new {message = "Company Contact Updated"});
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest(new {message = ex.Message});
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new {message = ex.Message});
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCompanyContact(int id)
    {
        await _companyContactsRepository.DeleteCompanyContact(id);
        return NoContent();
    }

    [HttpGet("companyContact/count")]
    public async Task<ActionResult<int>> GetCompanyContactCount()
    {
        return Ok(await _companyContactsRepository.CountCompanyContacts());
    }
}