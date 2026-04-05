using EmainesChat.Domain.Common;
using EmainesChat.Domain.ValueObjects;

namespace EmainesChat.Domain.Aggregates.Rooms;

public class Room
{
    public Guid Id { get; private set; }
    public RoomName Name { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    private Room() { } // EF Core

    public static Result<Room> Create(string name)
    {
        var nameResult = RoomName.Create(name);
        if (nameResult.IsFailure)
            return Result.Failure<Room>(nameResult.Error);

        return Result.Success(new Room
        {
            Id = Guid.NewGuid(),
            Name = nameResult.Value,
            CreatedAt = DateTime.UtcNow
        });
    }

    public Result UpdateName(string newName)
    {
        var nameResult = RoomName.Create(newName);
        if (nameResult.IsFailure)
            return Result.Failure(nameResult.Error);

        Name = nameResult.Value;
        return Result.Success();
    }
}
