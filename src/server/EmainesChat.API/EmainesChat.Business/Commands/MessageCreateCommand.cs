using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Commands
{
    public class MessageCreateCommand
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }
    public class MessageCreateCommandValidator : AbstractValidator<MessageCreateCommand>
    {
        public MessageCreateCommandValidator()
        {
            RuleFor(p => p.Content)
                .NotEmpty();

            //TODO: descomentar quando existir UserId e RoomId vindo do front
            //RuleFor(p => p.UserId)
            //    .NotNull();

            //RuleFor(p => p.RoomId)
            //    .NotNull();
        }
    }
}
