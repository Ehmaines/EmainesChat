namespace EmainesChat.Domain.Events;

public record MessageCreatedEvent(Guid MessageId, Guid RoomId, Guid UserId, string Content, DateTime SentAt);
