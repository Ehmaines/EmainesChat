
using EmainesChat.Business.Commands;
using EmainesChat.Business.Helpers.Enums;

namespace EmainesChat.Business.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Create(UserAddCommand command)
        {
           User user = new User(command.UserName, command.Email, command.Password, Roles.User);

            //TODO: mais para frente verificar se Email já foi ultilizado

           return _userRepository.Create(user);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByEmailAndPassword(string email, string password) 
        {
            return _userRepository.GetByEmailAndPassword(email, password);
        }
    }
}
