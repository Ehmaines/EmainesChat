using EmainesChat.Business.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Messages
{
    public interface IMessageRepository
    {
        Task<Message> Create(Message message);
        List<Message> GetAll();
        List<Message> GetByRoomId(int id);
    }
}
