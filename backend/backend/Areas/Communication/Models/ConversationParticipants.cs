using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Communication.Models;

public class ConversationParticipants
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserId { get; set; }
    public int ConversationId { get; set; }
    public User User { get; set; }
    public Conversation Conversation { get; set; }
}