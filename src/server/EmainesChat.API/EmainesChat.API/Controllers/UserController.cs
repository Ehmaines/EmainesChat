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

        [HttpGet]
        [Route("")]
        public IActionResult GetUser()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddUser([FromBody] UserAddCommand command)
        {
            return Ok(await _userService.CreateUser(command));
        }
    }
}
