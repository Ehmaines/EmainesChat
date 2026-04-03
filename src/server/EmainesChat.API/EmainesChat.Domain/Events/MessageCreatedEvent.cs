namespace EmainesChat.Domain.Events;

public record MessageCreatedEvent(int MessageId, int RoomId, int UserId, string Content, DateTime SentAt);
