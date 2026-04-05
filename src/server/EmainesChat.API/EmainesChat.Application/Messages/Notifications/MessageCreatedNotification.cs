using MediatR;

namespace EmainesChat.Application.Messages.Notifications;

public record MessageCreatedNotification(
    Guid MessageId,
    Guid RoomId,
    Guid UserId,
    string Content,
    string UserName,
    DateTime SentAt,
    string? ProfilePictureUrl) : INotification;
