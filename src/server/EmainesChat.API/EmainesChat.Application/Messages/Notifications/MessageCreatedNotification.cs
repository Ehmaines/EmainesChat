using MediatR;

namespace EmainesChat.Application.Messages.Notifications;

public record MessageCreatedNotification(
    int MessageId,
    int RoomId,
    int UserId,
    string Content,
    string UserName,
    DateTime SentAt) : INotification;
