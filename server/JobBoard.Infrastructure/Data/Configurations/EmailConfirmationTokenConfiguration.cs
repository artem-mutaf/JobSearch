using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="EmailConfirmationToken"/> для Entity Framework Core.
/// </summary>
public class EmailConfirmationTokenConfiguration : IEntityTypeConfiguration<EmailConfirmationToken>
{
    /// <summary>
    /// Настраивает маппинг сущности <see cref="EmailConfirmationToken"/> на таблицу.
    /// </summary>
    /// <param name="builder">Конструктор сущности.</param>
    public void Configure(EntityTypeBuilder<EmailConfirmationToken> builder)
    {
        builder.ToTable("EmailConfirmationTokens");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Email).IsRequired().HasMaxLength(255);
        builder.Property(t => t.Token).IsRequired().HasMaxLength(100);
        builder.HasIndex(t => t.Token).IsUnique();
        builder.Property(t => t.ExpiresAt).IsRequired();
        builder.Property(t => t.IsUsed).IsRequired().HasDefaultValue(false);
        builder.Property(t => t.UserId).IsRequired();
    }
}