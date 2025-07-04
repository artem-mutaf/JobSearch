using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Message"/> для Entity Framework Core.
/// </summary>
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    /// <summary>
    /// Настраивает маппинг сущности <see cref="Message"/> на таблицу.
    /// </summary>
    /// <param name="builder">Конструктор сущности.</param>
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Text).IsRequired().HasMaxLength(1000);
        builder.Property(m => m.SenderId).IsRequired();
        builder.Property(m => m.SentAt).IsRequired();
        builder.HasOne(m => m.Chat).WithMany(c => c.Messages).HasForeignKey(m => m.ChatId);
        builder.HasIndex(m => m.SentAt); // Для сортировки сообщений
    }
}