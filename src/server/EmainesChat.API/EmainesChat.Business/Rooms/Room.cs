using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Rooms
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public Room() { }

        public Room(string name) 
        { 
            Name = name;
            CreatedAt = DateTime.Now;
        }
    }
}
