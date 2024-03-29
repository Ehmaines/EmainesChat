﻿using EmainesChat.Business.Commands;
using EmainesChat.Business.Messages;
using EmainesChat.Business.Rooms;
using EmainesChat.Business.Token;
using EmainesChat.Business.Users;
using EmainesChat.Data;
using EmainesChat.Data.MessagesRepository;
using EmainesChat.Data.RoomsRepository;
using EmainesChat.Data.UsersRepository;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmainesChat.Infra
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<DataBaseContext>();

            //User
            services.AddScoped<UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Room
            services.AddScoped<RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            //Message
            services.AddScoped<MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            //Login
            services.AddScoped<TokenService>();

            //Validators
            services.AddTransient<IValidator<UserAddCommand>, UserAddCommandValidator>();
            services.AddTransient<IValidator<RoomCreateCommand>, RoomCreateCommandValidator>();
            services.AddTransient<IValidator<RoomUpdateCommand>, RoomUpdateCommandValidator>();
            services.AddTransient<IValidator<MessageCreateCommand>, MessageCreateCommandValidator>();
            services.AddTransient<IValidator<MessageGetByRoomIdCommand>, MessageGetByRoomIdCommandValidator>();
            services.AddTransient<IValidator<LoginCommand>, LoginCommandValidator>();
        }
    }
}