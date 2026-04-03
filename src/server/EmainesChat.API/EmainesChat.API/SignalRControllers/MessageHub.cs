using EmainesChat.Application.Messages.Commands.SendMessage;
using EmainesChat.Application.Messages.Queries.GetAllMessages;
using EmainesChat.Application.Messages.Queries.GetMessagesByRoom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace EmainesChat.API.SignalRControllers;

[Authorize]
public class MessageHub : Hub
{
    private readonly ISender _sender;
    private readonly ILogger<MessageHub> _logger;

    public MessageHub(ISender sender, ILogger<MessageHub> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public async Task CreateMessage(string content, int roomId)
    {
        var idClaim = Context.User?.FindFirst("Id");
        if (idClaim is null || !int.TryParse(idClaim.Value, out var userId))
        {
            _logger.LogWarning("Conexão {ConnectionId} com claim 'Id' ausente ou inválida.", Context.ConnectionId);
            await Clients.Caller.SendAsync("Error", "Token inválido: claim 'Id' ausente.");
            return;
        }

        var result = await _sender.Send(new SendMessageCommand(content, userId, roomId));
        if (result.IsFailure)
        {
            _logger.LogWarning("Falha ao enviar mensagem do usuário {UserId} na sala {RoomId}: {Error}", userId, roomId, result.Error);
            await Clients.Caller.SendAsync("Error", result.Error);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (exception is not null)
            _logger.LogError(exception, "Conexão {ConnectionId} desconectada com erro.", Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
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
        await Clients.Caller.SendAsync("ReceiveMessageByRoomId", messages);
    }

    public async Task GetAllMessages()
    {
        var messages = await _sender.Send(new GetAllMessagesQuery());
        await Clients.Caller.SendAsync("ReceiveAllMessages", messages);
    }
}
