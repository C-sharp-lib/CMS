using backend.Areas.Communication.Models;
using backend.Areas.Communication.Models.ViewModels;
using backend.Areas.Communication.Services;
using backend.Areas.Identity.Services;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Communication.Controllers;

[ApiController]
[Area("Communication")]
[Route("api/[area]/[controller]")]
public class MessageController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMessageRepository _messageRepository;
    private readonly ILogger<MessageController> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IContactRepository _contactRepository;

    public MessageController(ApplicationDbContext context, IMessageRepository messageRepository,
        ILogger<MessageController> logger, IUserRepository userRepository, IContactRepository contactRepository)
    {
        _context = context;
        _messageRepository = messageRepository;
        _logger = logger;
        _userRepository = userRepository;
        _contactRepository = contactRepository;
    }
}