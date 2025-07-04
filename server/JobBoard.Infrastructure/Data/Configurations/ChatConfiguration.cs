using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Chat"/> для Entity Framework Core.
/// </summary>
public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    /// <summary>
    /// Настраивает маппинг сущности <see cref="Chat"/> на таблицу.
    /// </summary>
    /// <param name="builder">Конструктор сущности.</param>
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");
        builder.HasKey(c => c.Id);
        builder.HasOne(c => c.Applicant).WithMany(a => a.Chats).HasForeignKey(c => c.ApplicantId);
        builder.HasOne(c => c.Employer).WithMany(e => e.Chats).HasForeignKey(c => c.EmployerId);
        builder.HasOne(c => c.Vacancy).WithMany().HasForeignKey(c => c.VacancyId);
        builder.HasMany(c => c.Messages).WithOne(m => m.Chat).HasForeignKey(m => m.ChatId);
    }
}