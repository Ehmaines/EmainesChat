using EmainesChat.Business.Commands;
using EmainesChat.Business.Rooms;
using EmainesChat.Business.Users;
using EmainesChat.Data;
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

            //Validators
            services.AddTransient<IValidator<UserAddCommand>, UserAddCommandValidator>();
            services.AddTransient<IValidator<RoomCreateCommand>, RoomCreateCommandValidator>();
            services.AddTransient<IValidator<RoomUpdateCommand>, RoomUpdateCommandValidator>();
        }
    }
}