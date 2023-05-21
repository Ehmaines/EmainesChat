using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Commands
{
    public class RoomCreateCommand
    {
        public string Name { get; set; }
    }
    public class RoomCreateCommandValidator : AbstractValidator<RoomCreateCommand>
    {
        public RoomCreateCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .Length(3, 255);
        }
    }
}
