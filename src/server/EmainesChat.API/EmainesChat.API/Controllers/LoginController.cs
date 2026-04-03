using EmainesChat.Application.Users.Commands.AuthenticateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmainesChat.API.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase
{
    private readonly ISender _sender;

    public LoginController(ISender sender) => _sender = sender;

    [HttpGet("isAuthenticated")]
    [Authorize]
    public IActionResult IsAuthenticated() => Ok(true);

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : Unauthorized(new { message = result.Error });
    }
}
