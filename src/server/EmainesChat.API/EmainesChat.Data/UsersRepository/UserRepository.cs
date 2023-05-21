using EmainesChat.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Data.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(User user)
        {
            using (var context = new DataBaseContext())
            {
                context.Users.Add(user);

                var result = await context.SaveChangesAsync();

                return result > 0;
            };
        }
    }
}
