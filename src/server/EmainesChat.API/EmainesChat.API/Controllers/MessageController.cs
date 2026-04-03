using EmainesChat.Application.Messages.Queries.GetAllMessages;
using EmainesChat.Application.Messages.Queries.GetMessagesByRoom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmainesChat.API.Controllers;

[ApiController]
[Route("message")]
[Authorize]
public class MessageController : ControllerBase
{
    private readonly ISender _sender;

    public MessageController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var result = await _sender.Send(new GetAllMessagesQuery());
        return Ok(result);
    }

    [HttpGet("{roomId:int}")]
    public async Task<IActionResult> GetMessagesByRoomId(int roomId)
    {
        var result = await _sender.Send(new GetMessagesByRoomQuery(roomId));
        return Ok(result);
    }
}
