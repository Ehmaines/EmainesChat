using EmainesChat.Business.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Data.MessagesRepository
{
    public class MessageRepository: IMessageRepository
    {
        private readonly DataBaseContext _context;

        public MessageRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<Message> Create(Message message)
        {
            _context.Messages.Add(message);

            var result = await _context.SaveChangesAsync();

            return _context.Messages.FirstOrDefault(m => m.Id == message.Id)!;
        }

        public List<Message> GetAll()
        {
            var messages = _context.Messages.Include(r => r.Room).Include(u => u.User).ToList();
            return messages;
        }

        public List<Message> GetByRoomId(int roomId)
        {
            var messages = _context.Messages.Include(r => r.Room).Include(u => u.User).Where(r => r.Room.Id == roomId).ToList();
            return messages;
        }
    }
}
