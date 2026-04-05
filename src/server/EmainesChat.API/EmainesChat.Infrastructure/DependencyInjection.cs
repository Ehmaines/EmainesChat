using EmainesChat.Application.Common;
using EmainesChat.Domain.Aggregates.Messages;
using EmainesChat.Domain.Aggregates.Rooms;
using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Infrastructure.Authentication;
using EmainesChat.Infrastructure.Persistence;
using EmainesChat.Infrastructure.Persistence.Repositories;
using EmainesChat.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmainesChat.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' não encontrada.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<ITokenService, JwtTokenService>();

        // Armazenamento de arquivos — troque LocalStorageService por uma implementação
        // de Azure Blob Storage, AWS S3, etc. para produção (ver IStorageService.cs).
        services.AddHttpContextAccessor();
        services.AddScoped<IStorageService, LocalStorageService>();

        return services;
    }
}
