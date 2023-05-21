
using EmainesChat.Business.Commands;

namespace EmainesChat.Business.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> AddUser(UserAddCommand command)
        {
           User user = new User(command.UserName, command.Email, command.Password);

            //TODO: mais para frente verificar se Email já foi ultilizado

           return _userRepository.Add(user);
        }
    }
}
