using EmainesChat.Domain.Aggregates.Messages;
using Microsoft.EntityFrameworkCore;

namespace EmainesChat.Infrastructure.Persistence.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context) => _context = context;

    public async Task<IReadOnlyList<Message>> GetAllAsync()
        => await _context.Messages
            .Include(m => m.User)
            .Include(m => m.Room)
            .ToListAsync();

    public async Task<IReadOnlyList<Message>> GetByRoomIdAsync(int roomId)
        => await _context.Messages
            .Include(m => m.User)
            .Include(m => m.Room)
            .Where(m => m.RoomId == roomId)
            .ToListAsync();

    public Task AddAsync(Message message)
    {
        _context.Messages.Add(message);
        return Task.CompletedTask;
    }
}
