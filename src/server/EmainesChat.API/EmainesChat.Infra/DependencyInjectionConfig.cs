using EmainesChat.Business.Commands;
using EmainesChat.Business.Users;
using EmainesChat.Data;
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
            services.AddScoped<UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Adicionas os validadores
            services.AddTransient<IValidator<UserAddCommand>, UserAddCommandValidador>();
        }
    }
}