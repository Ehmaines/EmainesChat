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
        public async Task<IActionResult> AddUser()
        {
            User user = new User()
            {
                UserName = "user",
                Email = "email",
                Password = "P@ssw0rd",
                CreatedAt = DateTime.Now
            };

            return Ok(await _userService.AddUser(user));
        }
    }
}
