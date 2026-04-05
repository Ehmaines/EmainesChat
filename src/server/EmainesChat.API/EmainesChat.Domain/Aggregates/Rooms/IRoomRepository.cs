namespace EmainesChat.Domain.Aggregates.Rooms;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id);
    Task<Room?> GetByNameAsync(string name);
    Task<IReadOnlyList<Room>> GetAllAsync();
    Task AddAsync(Room room);
    void Update(Room room);
}
