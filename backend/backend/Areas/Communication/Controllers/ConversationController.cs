using backend.Areas.Communication.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Communication.Controllers;

[ApiController]
[Area("Communication")]
[Route("api/[area]/[controller]")]
public class ConversationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ConversationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserConversations(string userId)
    {
        var conversations = await _context.ConversationParticipants
            .Where(cp => cp.UserId == userId)
            .Select(cp => cp.Conversation)
            .Include(c => c.Participants)
            .ThenInclude(p => p.User)
            .ToListAsync();

        return Ok(conversations);
    }
    [HttpGet("{conversationId}/messages")]
    public async Task<IActionResult> GetMessages(int conversationId)
    {
        var messages = await _context.Messages
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();

        return Ok(messages);
    }

    [HttpPost("{conversationId}/messages")]
    public async Task<IActionResult> SendMessage(int conversationId, [FromBody] SendMessageViewModel message)
    {
        message.ConversationId = conversationId;
        message.SentAt = DateTime.UtcNow;

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return Ok(message);
    }
}