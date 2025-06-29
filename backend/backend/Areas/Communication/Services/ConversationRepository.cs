using backend.Areas.Communication.Models;
using backend.Areas.Communication.Models.ViewModels;
using backend.Areas.Identity.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Communication.Services;

public class ConversationRepository : IConversationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ConversationRepository> _logger;

    public ConversationRepository(ApplicationDbContext context, IUserRepository userRepository,
        ILogger<ConversationRepository> logger)
    {
        _context = context;
        _userRepository = userRepository;
        _logger = logger;
    }
    
     public async Task<List<Conversation>> GetConversationsByUserIdAsync(string userId)
    {
        return await _context.Conversations
            .Include(c => c.Participants)
            .ThenInclude(p => p.User)
            .Include(c => c.Messages)
            .ThenInclude(m => m.Sender)
            .Where(c => c.Participants.Any(p => p.UserId == userId))
            .ToListAsync();
    }

    public async Task<List<Message>> GetAllMessagesByConversationIdAsync(int conversationId)
    {
        var conversation = await GetConversationByIdAsync(conversationId);
        var messages = await _context.Messages
            .Where(c => c.ConversationId == conversation.Id)
            .Include(cu => cu.Sender)
            .Include(cc => cc.Conversation)
            .ThenInclude(cc => cc.Participants)
            .ThenInclude(cp => cp.User)
            .ToListAsync();
        if (!messages.Any()) return new List<Message>();
        return messages;
    }

    public async Task<List<ConversationParticipants>> GetConversationParticipantsByConversationIdAsync(int conversationId)
    {
        var participants = await _context.ConversationParticipants
            .Where(p => p.ConversationId == conversationId)
            .Include(c => c.Conversation)
            .ThenInclude(c => c.Participants)
            .ThenInclude(cp => cp.User)
            .Include(cu => cu.User)
            .ToListAsync();
        if (!participants.Any()) return new List<ConversationParticipants>();
        return participants;
    }

    public async Task<Message> AddMessageAsync(int conversationId, [FromBody] AddMessageViewModel model)
    {
        var conversation = await GetConversationByIdAsync(conversationId);
        var message = new Message
        {
            ConversationId = conversation.Id,
            SenderId = model.SenderId,
            Content = model.Content,
            SentAt = model.SentAt,
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<Conversation> CreateConversationAsync([FromBody] CreateConversationViewModel model)
    {
        var conversation = new Conversation
        {
            Title = model.Title ?? "Untitled",
            Participants = new List<ConversationParticipants>()
        };

        foreach (var userId in model.UserIds)
        {
            conversation.Participants.Add(new ConversationParticipants
            {
                UserId = userId
            });
        }

        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync();
        return conversation;
    }

    public async Task<Conversation> GetConversationByIdAsync(int conversationId)
    {
        var conversation = await _context.Conversations
            .Include(c => c.Participants).ThenInclude(p => p.User)
            .Include(c => c.Messages).ThenInclude(x => x.Sender)
            .FirstOrDefaultAsync(c => c.Id == conversationId);
        if (conversation == null)
        {
            _logger.LogWarning("Conversation with id {conversationId} could not be found", conversationId);
            return null;
        }

        return conversation;
    }

    public async Task UpdateConversationAsync(int id, [FromBody] UpdateConversationViewModel model)
    {
        var conversation = await GetConversationByIdAsync(id);
        conversation.Title = model.Title;
        _context.Conversations.Update(conversation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteConversationAsync(int conversationId)
    {
        var conversation = await _context.Conversations
            .Include(c => c.Messages)
            .Include(c => c.Participants)
            .FirstOrDefaultAsync(c => c.Id == conversationId);

        if (conversation != null)
        {
            _context.Messages.RemoveRange(conversation.Messages);
            _context.ConversationParticipants.RemoveRange(conversation.Participants);
            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteMessagesAsync(int conversationId, string userId, int messageId)
    {
        var messages = await _context.Messages
            .Where(m => m.ConversationId == conversationId && m.SenderId == userId)
            .FirstOrDefaultAsync(m => m.Id == messageId);
        if(messages == null) return;

        _context.Messages.Remove(messages);
        await _context.SaveChangesAsync();
    }
    

    public async Task DeleteConversationParticipantsAsync(int conversationId, int conversationParticipantId)
    {
        var participant = await _context.ConversationParticipants
            .Where(p => p.Conversation.Participants.Any(c => c.Id == conversationParticipantId))
            .ToListAsync();

        _context.ConversationParticipants.RemoveRange(participant);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountMessagesAsync(int conversationId)
    {
        var conversation = await GetConversationByIdAsync(conversationId);
        var messageCount = await _context.Messages
            .Where(m => m.ConversationId == conversation.Id)
            .CountAsync();
        return messageCount;
    }

    public async Task<int> CountConversationParticipantsAsync(int conversationId)
    {
        var conversation = await GetConversationByIdAsync(conversationId);
        var participantCount = await _context.ConversationParticipants
            .Where(p => p.ConversationId == conversation.Id)
            .CountAsync();
        return participantCount;
    }

    public async Task<int> CountConversationsAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var conversations = await _context.Conversations
            .Where(c => c.Participants.Any(p => p.UserId == user.Id))
            .CountAsync();
        return conversations;
    }
}