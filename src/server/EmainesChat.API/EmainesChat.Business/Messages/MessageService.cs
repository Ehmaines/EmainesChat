using EmainesChat.Business.Commands;
using EmainesChat.Business.Rooms;
using EmainesChat.Business.Users;

namespace EmainesChat.Business.Messages
{
    public class MessageService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository ,IRoomRepository roomRepository)
        {
            _messageRepository = messageRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public Task<Message> Create(MessageCreateCommand command)
        {
            command.RoomId = 4;
            command.UserId = 11;

            Room room = _roomRepository.GetById(command.RoomId);

            User user = _userRepository.GetById(command.UserId);
            
            Message message = new Message(command.Content, user, room);

            return _messageRepository.Create(message);
        }
    }
}
