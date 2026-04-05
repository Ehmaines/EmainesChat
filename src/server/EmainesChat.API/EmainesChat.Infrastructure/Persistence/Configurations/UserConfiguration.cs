using EmainesChat.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmainesChat.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.OwnsOne(u => u.Email, e =>
        {
            e.Property(x => x.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(200);
        });

        builder.OwnsOne(u => u.Password, p =>
        {
            p.Property(x => x.Hash)
                .HasColumnName("PasswordHash")
                .IsRequired();
        });

        builder.OwnsOne(u => u.Name, n =>
        {
            n.Property(x => x.Value)
                .HasColumnName("Name")
                .HasMaxLength(100);
        });

        builder.Property(u => u.ProfilePictureUrl)
            .HasMaxLength(2048);

        builder.Property(u => u.Role)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();
    }
}
