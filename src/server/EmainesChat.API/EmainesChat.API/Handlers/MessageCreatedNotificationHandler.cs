using EmainesChat.Application.Messages.Notifications;
using EmainesChat.API.SignalRControllers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EmainesChat.API.Handlers;

public class MessageCreatedNotificationHandler : INotificationHandler<MessageCreatedNotification>
{
    private readonly IHubContext<MessageHub> _hubContext;

    public MessageCreatedNotificationHandler(IHubContext<MessageHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(MessageCreatedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.Group($"room-{notification.RoomId}")
            .SendAsync("MessageCreated", notification, cancellationToken);
    }
}
