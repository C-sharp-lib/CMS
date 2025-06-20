using System.ComponentModel.DataAnnotations;
using backend.Areas.Identity.Models;

namespace backend.Areas.Communication.Models.ViewModels;

public class SendMessageViewModel : Message
{
    public int ConversationId { get; set; }
    public string SenderId { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    
}