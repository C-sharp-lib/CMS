using backend.Areas.Communication.Models;
using backend.Areas.Communication.Models.ViewModels;
using backend.Areas.Communication.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Communication.Controllers;

[ApiController]
[Area("Communication")]
[Route("api/[area]/[controller]")]
public class ConversationController : ControllerBase
{
    private readonly IConversationRepository _conversationRepository;
    private readonly ILogger<ConversationController> _logger;

    public ConversationController(IConversationRepository conversationRepository, ILogger<ConversationController> logger)
    {
        _conversationRepository = conversationRepository;
        _logger = logger;
    }

    [HttpGet("conversation/{conversationId}")]
    public async Task<ActionResult<Conversation>> GetConversation(int conversationId)
    {
        var conversation = await _conversationRepository.GetConversationByIdAsync(conversationId);
        return Ok(conversation);
    }

    [HttpGet("conversation-participants/{id}")]
    public async Task<ActionResult<List<ConversationParticipants>>> GetUserConversationParticipants(int id)
    {
        var conversation = await _conversationRepository.GetConversationByIdAsync(id);
        var participants = await _conversationRepository.GetConversationParticipantsByConversationIdAsync(conversation.Id);
        if (!participants.Any()) return new List<ConversationParticipants>();
        return Ok(participants);
    }

    [HttpGet("conversations/{userId}")]
    public async Task<ActionResult<List<Conversation>>> GetConversations(string userId)
    {
        var conversations = await _conversationRepository.GetConversationsByUserIdAsync(userId);
        if (!conversations.Any()) return new List<Conversation>();
        return Ok(conversations);
    }

    [HttpGet("conversation/{conversationId}/messages")]
    public async Task<ActionResult<IEnumerable<Message>>> GetMessages(int conversationId)
    {
        var messages = await _conversationRepository.GetAllMessagesByConversationIdAsync(conversationId);
        if (!messages.Any()) return new List<Message>();
        return Ok(messages);
    }
    [HttpGet("get-user-image-path")]
    public IActionResult GetUserImagePath([FromQuery] string relativePath)
    {
        if (string.IsNullOrEmpty(relativePath))
            return BadRequest("Image path is required.");

        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var fullPath = Path.Combine(wwwrootPath, relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (!System.IO.File.Exists(fullPath))
            return NotFound("Image not found.");

        var request = HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}";
        var fullImageUrl = $"{baseUrl}/{relativePath}";

        return Ok(new { imageUrl = fullImageUrl });
    }

    [HttpPost("conversation/{conversationId}/messages")]
    public async Task<ActionResult> SendMessage(int conversationId, [FromBody] AddMessageViewModel message)
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
            await _conversationRepository.AddMessageAsync(conversationId, message);
            return Ok(new {message = "Conversation created successfully"});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(new {message = "An error occured: " + ex.Message});
        }
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateConversation([FromBody] CreateConversationViewModel model)
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
            if (model.UserIds == null || !model.UserIds.Any())
                return BadRequest("At least one participant is required.");
            
            await _conversationRepository.CreateConversationAsync(model);
            return Ok(new {message = "Conversation created successfully"});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(new {message = "An error occured: " + ex.Message});
        }
    }

    [HttpDelete("conversation/{conversationId}/delete-conversation")]
    public async Task<ActionResult> DeleteConversation(int conversationId)
    {
        await _conversationRepository.DeleteConversationAsync(conversationId);
        return NoContent();
    }

    [HttpDelete("conversation/{conversationId}/participant/{conversationParticipantId}/delete")]
    public async Task<ActionResult> DeleteParticipant(int conversationId, int conversationParticipantId)
    {
        await _conversationRepository.DeleteConversationParticipantsAsync(conversationId, conversationParticipantId);
        return NoContent();
    }

    [HttpDelete("conversations/{userId}/conversation/{conversationId}/delete-message/{messageId}")]
    public async Task<ActionResult> DeleteMessage(int conversationId, string userId, int messageId)
    {
        await _conversationRepository.DeleteMessagesAsync(conversationId, userId, messageId);
        return NoContent();
    }

    [HttpGet("conversation/{conversationId}/conversation-participants/count")]
    public async Task<ActionResult<long>> GetConversationParticipantCount(int conversationId)
    {
        return Ok(await _conversationRepository.CountConversationParticipantsAsync(conversationId));
    }

    [HttpGet("{userId}/conversations/count")]
    public async Task<ActionResult<int>> GetConversationCount(string userId)
    {
        return Ok(await _conversationRepository.CountConversationsAsync(userId));
    }

    [HttpGet("conversation/{conversationId}/messages/count")]
    public async Task<ActionResult<long>> GetMessagesCount(int conversationId)
    {
        return Ok(await _conversationRepository.CountMessagesAsync(conversationId));
    }
}