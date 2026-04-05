using EmainesChat.Domain.Aggregates.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmainesChat.Infrastructure.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(m => m.Content, c =>
        {
            c.Property(x => x.Value)
                .HasColumnName("Content")
                .IsRequired()
                .HasMaxLength(2000);
        });

        builder.Property(m => m.SentAt)
            .IsRequired();

        builder.HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .IsRequired();

        builder.HasOne(m => m.Room)
            .WithMany()
            .HasForeignKey(m => m.RoomId)
            .IsRequired();
    }
}
