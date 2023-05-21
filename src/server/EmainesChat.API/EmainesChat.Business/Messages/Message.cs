using EmainesChat.Business.Rooms;
using EmainesChat.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Messages
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        //Relationships
        public User User { get; set; }
        public Room Room { get; set; }

        public Message() { }
    }
}
