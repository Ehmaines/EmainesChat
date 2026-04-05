using EmainesChat.Domain.Aggregates.Rooms;
using Microsoft.EntityFrameworkCore;

namespace EmainesChat.Infrastructure.Persistence.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context) => _context = context;

    public Task<Room?> GetByIdAsync(Guid id)
        => _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);

    public Task<Room?> GetByNameAsync(string name)
        => _context.Rooms.FirstOrDefaultAsync(r => r.Name.Value == name.Trim());

    public async Task<IReadOnlyList<Room>> GetAllAsync()
        => await _context.Rooms.ToListAsync();

    public Task AddAsync(Room room)
    {
        _context.Rooms.Add(room);
        return Task.CompletedTask;
    }

    public void Update(Room room)
        => _context.Rooms.Update(room);
}
