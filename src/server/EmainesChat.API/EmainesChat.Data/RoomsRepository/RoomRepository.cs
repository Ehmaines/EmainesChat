using EmainesChat.Business.Rooms;
using EmainesChat.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Data.RoomsRepository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataBaseContext _context;

        public RoomRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Room room)
        {
            _context.Rooms.Add(room);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Update(int id, string newName)
        {
            var oldroom = GetById(id);

            oldroom.Name = newName;

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public List<Room> GetAll()
        {
            return _context.Rooms.ToList();
        }

        public Room GetById(int id)
        {
            var teste = _context.Rooms.FirstOrDefault(r => r.Id == id)!;
            return teste;
        }

        public Room GetByName(string name)
        {
           return _context.Rooms.FirstOrDefault(r => r.Name == name)!;
        }
    }
}
