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
        public class LoginCommand
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        public class LoginCommandValidator : AbstractValidator<LoginCommand>
        {
            public LoginCommandValidator()
            {
                //TODO: descomentar quando existir UserId e RoomId vindo do front
                RuleFor(p => p.email)
                    .NotNull();

                RuleFor(p => p.password)
                    .NotNull();
            }
        }
}
