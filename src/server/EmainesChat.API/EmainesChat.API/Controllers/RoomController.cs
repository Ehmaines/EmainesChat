using EmainesChat.Application.Rooms.Commands.CreateRoom;
using EmainesChat.Application.Rooms.Commands.UpdateRoom;
using EmainesChat.Application.Rooms.Queries.GetAllRooms;
using EmainesChat.Application.Rooms.Queries.GetRoomById;
using EmainesChat.Application.Rooms.Queries.GetRoomByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmainesChat.API.Controllers;

[ApiController]
[Route("Room")]
[Authorize]
public class RoomController : ControllerBase
{
    private readonly ISender _sender;

    public RoomController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var result = await _sender.Send(new GetAllRoomsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoomById(Guid id)
    {
        var result = await _sender.Send(new GetRoomByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(new { message = result.Error });
    }

    [HttpGet("{name}/search-by-name")]
    public async Task<IActionResult> GetRoomByName(string name)
    {
        var result = await _sender.Send(new GetRoomByNameQuery(name));
        return result.IsSuccess ? Ok(result.Value) : NotFound(new { message = result.Error });
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] UpdateRoomCommand command)
    {
        var result = await _sender.Send(command with { Id = id });
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
    }
}
