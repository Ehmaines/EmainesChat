using EmainesChat.Business.Helpers.Enums;

namespace EmainesChat.Business.Users
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public User() { }

        public User(string userName, string email, string password, Roles role)
        {
            UserName = userName;
            Email = email;
            Password = password;
            CreatedAt = DateTime.Now;
        }
    }
}