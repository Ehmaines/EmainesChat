using EmainesChat.Business.Messages;
using EmainesChat.Business.Users;
using Microsoft.AspNetCore.Mvc;

namespace EmainesChat.API.Controllers
{
    [ApiController]
    [Route("message")]
    public class MessageController : Controller
    {
        private readonly MessageService _messageService;

        private readonly ILogger<UserController> _logger;

        public MessageController(ILogger<UserController> logger, MessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllMessage()
        {
            return Ok(_messageService.GetAll());
        }
    }
}