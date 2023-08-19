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

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository, IRoomRepository roomRepository)
        {
            _messageRepository = messageRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public Task<Message> Create(MessageCreateCommand command)
        {
            Room room = _roomRepository.GetByName(command.Room.Name);

            User user = _userRepository.GetByEmail(command.User.Email);

            Message message = new Message(command.Content, user, room);

            return _messageRepository.Create(message);
        }

        public List<Message> GetAll()
        {
            return _messageRepository.GetAll();
        }

        public List<Message> GetByRoomId(int roomId)
        {
            return _messageRepository.GetByRoomId(roomId);
        }
    }
}
