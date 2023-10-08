using EmainesChat.Business.Commands;
using EmainesChat.Business.Helpers.Enums;
using EmainesChat.Business.Users;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [Route("")]
        public IActionResult GetUser()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> AddUser([FromBody] UserAddCommand command)
        {
            return Ok(await _userService.Create(command));
        }
    }
}
