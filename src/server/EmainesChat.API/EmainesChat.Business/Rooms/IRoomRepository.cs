using EmainesChat.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Rooms
{
    public interface IRoomRepository
    {
        Task<bool> Create(Room room);
        Task<bool> Update(int id, string newName);
        List<Room> GetAll();
        Room GetById(int id);
        Room GetByName(string name);
    }
}
