using EmainesChat.Domain.Aggregates.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmainesChat.Infrastructure.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(r => r.Name, n =>
        {
            n.Property(x => x.Value)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.Property(r => r.CreatedAt)
            .IsRequired();
    }
}
