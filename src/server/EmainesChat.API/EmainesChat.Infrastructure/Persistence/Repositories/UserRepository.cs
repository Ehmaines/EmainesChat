using EmainesChat.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

namespace EmainesChat.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public Task<User?> GetByIdAsync(int id)
        => _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<User?> GetByEmailAsync(string email)
        => _context.Users.FirstOrDefaultAsync(u => u.Email.Value == email.ToLowerInvariant());

    public async Task<IReadOnlyList<User>> GetAllAsync()
        => await _context.Users.ToListAsync();

    public Task AddAsync(User user)
    {
        _context.Users.Add(user);
        return Task.CompletedTask;
    }
}
