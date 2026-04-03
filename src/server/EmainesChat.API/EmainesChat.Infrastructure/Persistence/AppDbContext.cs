using EmainesChat.Domain.Aggregates.Messages;
using EmainesChat.Domain.Aggregates.Rooms;
using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EmainesChat.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
