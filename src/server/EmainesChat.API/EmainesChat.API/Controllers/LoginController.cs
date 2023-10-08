using EmainesChat.Business.Commands;
using EmainesChat.Business.Messages;
using EmainesChat.Business.Rooms;
using EmainesChat.Business.Token;
using EmainesChat.Business.Users;
using Microsoft.AspNetCore.Mvc;

namespace EmainesChat.API.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly TokenService _loginService;

        private readonly ILogger<UserController> _logger;

        public LoginController(ILogger<UserController> logger, TokenService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Authenticate([FromBody] LoginCommand command)
        {
            var token = _loginService.GenerateToken(command);
            if (token == null)
            {
                return NotFound(new {message = "Usuário ou senha inválidos"});
            }
            
            return Ok(token);
        }
    }
}
