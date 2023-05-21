using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Commands
{
    public class RoomUpdateCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RoomUpdateCommandValidator : AbstractValidator<RoomUpdateCommand>
    {
        public RoomUpdateCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.Name)
                .NotEmpty()
                .Length(3, 255);
        }
    }
}
