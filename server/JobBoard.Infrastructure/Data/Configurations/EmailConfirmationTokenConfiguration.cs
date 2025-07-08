using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobBoard.Core.Entities;

namespace JobBoard.Infrastructure.Data.Configurations;

public class EmailConfirmationTokenConfiguration : IEntityTypeConfiguration<EmailConfirmationToken>
{
    public void Configure(EntityTypeBuilder<EmailConfirmationToken> builder)
    {
        builder.ToTable("EmailConfirmationTokens");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Email).IsRequired().HasMaxLength(255);
        builder.Property(t => t.Code).IsRequired().HasMaxLength(6);
        builder.Property(t => t.ExpiresAt).IsRequired();
        builder.Property(t => t.IsUsed).IsRequired();
        builder.Property(t => t.UserId).IsRequired();
        builder.HasIndex(t => t.Code).IsUnique();
    }
}