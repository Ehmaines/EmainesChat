using EmainesChat.Application.Messages.Commands.SendMessage;
using EmainesChat.Application.Messages.Queries.GetAllMessages;
using EmainesChat.Application.Messages.Queries.GetMessagesByRoom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace EmainesChat.API.SignalRControllers;

[Authorize]
public class MessageHub : Hub
{
    private readonly ISender _sender;

    public MessageHub(ISender sender) => _sender = sender;

    public async Task CreateMessage(string content, int roomId)
    {
        var userId = int.Parse(Context.User!.FindFirst("Id")!.Value);
        var result = await _sender.Send(new SendMessageCommand(content, userId, roomId));
        if (result.IsFailure)
            await Clients.Caller.SendAsync("Error", result.Error);
    }

    // Disponível para uso futuro: permite entrar em um grupo sem buscar mensagens.
    // Atualmente a entrada no grupo ocorre implicitamente em GetMessagesByRoomId.
    public async Task JoinRoom(int roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"room-{roomId}");
    }

    public async Task LeaveRoom(int roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"room-{roomId}");
    }

    public async Task GetMessagesByRoomId(int roomId)
    {
        if (roomId <= 0) return;
        await Groups.AddToGroupAsync(Context.ConnectionId, $"room-{roomId}");
        var messages = await _sender.Send(new GetMessagesByRoomQuery(roomId));
        await Clients.Caller.SendAsync("ReciveMessageByRoomId", messages);
    }

    public async Task GetAllMessages()
    {
        var messages = await _sender.Send(new GetAllMessagesQuery());
        await Clients.Caller.SendAsync("ReciveAllMessages", messages);
    }
}
