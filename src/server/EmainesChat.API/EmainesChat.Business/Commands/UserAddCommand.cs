﻿using EmainesChat.Business.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmainesChat.Business.Commands
{
    public class UserAddCommand: CommandBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserAddCommandValidador: AbstractValidator<UserAddCommand>
    {
        public UserAddCommandValidador() 
        {
            RuleFor(p => p.UserName)
                .NotEmpty()
                .Length(3, 255);

            RuleFor(p => p.Email)
                .NotEmpty()
                .Length(7, 255)
                .EmailAddress();

            RuleFor(p => p.Password)
                .NotEmpty()
                .Length(8, 255);
        }
    }
}
