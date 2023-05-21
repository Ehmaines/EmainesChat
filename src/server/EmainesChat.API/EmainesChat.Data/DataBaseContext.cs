using EmainesChat.Business.Messages;
using EmainesChat.Business.Rooms;
using EmainesChat.Business.Users;
using Microsoft.EntityFrameworkCore;

namespace EmainesChat.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(u => u.User)
                .WithMany()
                .IsRequired();

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Room)
                .WithMany()
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source = localhost; initial catalog = EmainesChat; persist security info = True; user id = sa; password = P@ssw0rd; Integrated Security = SSPI; TrustServerCertificate=true");
        }
    }
}