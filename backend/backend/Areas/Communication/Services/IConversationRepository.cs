using backend.Areas.Communication.Models;
using backend.Areas.Communication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Communication.Services;

public interface IConversationRepository
{
    Task<List<Conversation>> GetConversationsByUserIdAsync(string userId);
    Task<List<Message>> GetAllMessagesByConversationIdAsync(int conversationId);
    Task<List<ConversationParticipants>> GetConversationParticipantsByConversationIdAsync(int conversationId);
    Task<Message> AddMessageAsync(int conversationId, [FromBody] AddMessageViewModel model);
    Task<Conversation> CreateConversationAsync([FromBody] CreateConversationViewModel model);
    Task<Conversation> GetConversationByIdAsync(int conversationId);
    Task UpdateConversationAsync(int id, [FromBody] UpdateConversationViewModel conversation);
    Task DeleteConversationAsync(int conversationId);
    Task DeleteMessagesAsync(int conversationId, string userId, int messageId);
    Task DeleteConversationParticipantsAsync(int conversationId, int conversationParticipantId);
    Task<long> CountConversationParticipantsAsync(int conversationId);
    Task<long> CountMessagesAsync(int conversationId);
    Task<long> CountConversationsAsync(string userId);
}