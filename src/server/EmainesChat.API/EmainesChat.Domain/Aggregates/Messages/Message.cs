using EmainesChat.Domain.Aggregates.Rooms;
using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Domain.Common;
using EmainesChat.Domain.ValueObjects;

namespace EmainesChat.Domain.Aggregates.Messages;

public class Message
{
    public int Id { get; private set; }
    public MessageContent Content { get; private set; } = null!;
    public DateTime SentAt { get; private set; }

    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public int RoomId { get; private set; }
    public Room Room { get; private set; } = null!;

    private Message() { } // EF Core

    public static Result<Message> Create(string content, User user, Room room)
    {
        var contentResult = MessageContent.Create(content);
        if (contentResult.IsFailure)
            return Result.Failure<Message>(contentResult.Error);

        return Result.Success(new Message
        {
            Content = contentResult.Value,
            User = user,
            UserId = user.Id,
            Room = room,
            RoomId = room.Id,
            SentAt = DateTime.UtcNow
        });
    }
}
