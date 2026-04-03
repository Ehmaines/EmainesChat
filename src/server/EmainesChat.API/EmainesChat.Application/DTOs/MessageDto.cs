using EmainesChat.Domain.Aggregates.Messages;

namespace EmainesChat.Application.DTOs;

public record MessageDto(int Id, string Content, DateTime SentAt, int UserId, string UserName, int RoomId)
{
    public static MessageDto From(Message message) =>
        new(message.Id, message.Content.Value, message.SentAt, message.UserId, message.User.UserName, message.RoomId);
}
