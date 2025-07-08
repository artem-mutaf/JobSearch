using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Applicant"/> для Entity Framework Core.
/// </summary>
public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    /// <summary>
    /// Настраивает маппинг сущности <see cref="Applicant"/> на таблицу.
    /// </summary>
    /// <param name="builder">Конструктор сущности.</param>
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("Applicants");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(a => a.Email).IsUnique();
        builder.Property(a => a.FullName).IsRequired().HasMaxLength(100);
        builder.OwnsOne(a => a.ContactInfo, ci =>
        {
            ci.Property(c => c.Email).HasColumnName("ContactEmail").IsRequired().HasMaxLength(255);
            ci.Property(c => c.PhoneNumber).HasColumnName("PhoneNumber").IsRequired().HasMaxLength(15);
            ci.Property(c => c.SocialMediaUrl).HasColumnName("SocialMediaUrl").HasMaxLength(255);
        });
        builder.OwnsOne(a => a.Location, l =>
        {
            l.Property(l => l.Address).HasColumnName("Address").IsRequired().HasMaxLength(255);
            l.Property(l => l.Region).HasColumnName("Region").IsRequired().HasMaxLength(100);
        });
        builder.Property(a => a.Password).IsRequired().HasMaxLength(255);
        builder.Property(a => a.IsEmailConfirmed).IsRequired().HasDefaultValue(false);
        builder.Property(a => a.ResumeUrl).HasMaxLength(255);
        builder.HasMany(a => a.Chats).WithOne(c => c.Applicant).HasForeignKey(c => c.ApplicantId);
        builder.HasMany(a => a.Categories).WithMany(c => c.Applicants)
            .UsingEntity(j => j.ToTable("ApplicantCategories"));
    }
}