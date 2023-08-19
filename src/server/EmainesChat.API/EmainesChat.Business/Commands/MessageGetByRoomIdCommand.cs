using EmainesChat.Business.Rooms;
using EmainesChat.Business.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Commands
{
    public class MessageGetByRoomIdCommand
    {
        public int roomId { get; set; }
    }
    public class MessageGetByRoomIdCommandValidator : AbstractValidator<MessageGetByRoomIdCommand>
    {
        public MessageGetByRoomIdCommandValidator()
        {
            RuleFor(p => p.roomId)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
