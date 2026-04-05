using EmainesChat.Domain.Aggregates.Messages;

namespace EmainesChat.Application.DTOs;

public record MessageDto(Guid Id, string Content, DateTime SentAt, Guid UserId, string UserName, Guid RoomId, string? ProfilePictureUrl)
{
    public static MessageDto From(Message message) =>
        new(message.Id, message.Content.Value, message.SentAt, message.UserId, message.User.UserName, message.RoomId, message.User.ProfilePictureUrl);
}
