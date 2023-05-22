using EmainesChat.Business.Messages;
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

            var teste = _context.Messages.FirstOrDefault(m => m.Id == message.Id);
            return teste!;
        }

        public List<Message> GetAll()
        {
            return _context.Messages.ToList();
        }
    }
}
