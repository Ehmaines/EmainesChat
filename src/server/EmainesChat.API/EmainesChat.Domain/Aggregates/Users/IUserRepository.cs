namespace EmainesChat.Domain.Aggregates.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string userName);
    Task<IReadOnlyList<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}
