using EmainesChat.Business.Commands;
using EmainesChat.Business.Users;
using Microsoft.AspNetCore.Mvc;

namespace EmainesChat.API.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddUser([FromBody] UserAddCommand command)
        {
            return Ok(await _userService.AddUser(command));
        }
    }
}
