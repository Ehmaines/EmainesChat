using EmainesChat.Domain.Aggregates.Rooms;

namespace EmainesChat.Application.DTOs;

public record RoomDto(Guid Id, string Name, DateTime CreatedAt)
{
    public static RoomDto From(Room room) =>
        new(room.Id, room.Name.Value, room.CreatedAt);
}
