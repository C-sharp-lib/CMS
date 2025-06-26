namespace backend.Areas.Communication.Models.ViewModels;

    public class CreateConversationViewModel
    {
        public string? Title { get; set; }
        public List<string> UserIds { get; set; } = new();
    }

    public class UpdateConversationViewModel : Conversation
    {
        
    }

    public class AddMessageViewModel
    {
        public int ConversationId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }