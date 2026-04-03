namespace EmainesChat.Domain.Aggregates.Messages;

public interface IMessageRepository
{
    Task<IReadOnlyList<Message>> GetAllAsync();
    Task<IReadOnlyList<Message>> GetByRoomIdAsync(int roomId);
    Task AddAsync(Message message);
}
