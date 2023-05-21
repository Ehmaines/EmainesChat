using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Users
{
    public interface IUserRepository
    {
        Task<bool> Add(User user);
    }
}
