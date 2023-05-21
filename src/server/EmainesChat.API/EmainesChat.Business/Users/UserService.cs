
namespace EmainesChat.Business.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> AddUser(User user)
        {
            // Realize qualquer lógica de negócios adicional, se necessário

           return _userRepository.Add(user);
        }
    }
}
