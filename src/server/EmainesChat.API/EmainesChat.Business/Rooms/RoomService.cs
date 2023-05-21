using EmainesChat.Business.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Rooms
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Task<bool> Create(RoomCreateCommand command)
        {
            Room room = new Room(command.Name);

            return _roomRepository.Create(room);
        }

        public Task<bool> Update(RoomUpdateCommand command)
        {
            return _roomRepository.Update(command.Id, command.Name);
        }

        public List<Room> GetAll()
        {
            return _roomRepository.GetAll();
        }

        public Room GetById(int id)
        {
            return _roomRepository.GetById(id);
        }

        public Room GetByName(string name)
        {
            return _roomRepository.GetByName(name);
        }
    }
}
