using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configurations;

/// <summary>
    /// Конфигурация сущности <see cref="Employer"/> для Entity Framework Core.
    /// </summary>
    public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {
        /// <summary>
        /// Настраивает маппинг сущности <see cref="Employer"/> на таблицу.
        /// </summary>
        /// <param name="builder">Конструктор сущности.</param>
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.ToTable("Employers");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(e => e.EntityType).IsRequired().HasConversion<string>();
            builder.Property(e => e.CompanyDescription).IsRequired().HasMaxLength(1000);
            builder.Property(e => e.About).HasMaxLength(2000);
            builder.OwnsOne(e => e.ContactInfo, ci =>
            {
                ci.Property(c => c.Email).HasColumnName("ContactEmail").IsRequired().HasMaxLength(255);
                ci.Property(c => c.PhoneNumber).HasColumnName("PhoneNumber").IsRequired().HasMaxLength(15);
                ci.Property(c => c.SocialMediaUrl).HasColumnName("SocialMediaUrl").HasMaxLength(255);
            });
            builder.OwnsOne(e => e.Location, l =>
            {
                l.Property(l => l.Address).HasColumnName("Address").IsRequired().HasMaxLength(255);
                l.Property(l => l.Region).HasColumnName("Region").IsRequired().HasMaxLength(100);
            });
            builder.Property(e => e.Password).IsRequired().HasMaxLength(255);
            builder.Property(e => e.IsEmailConfirmed).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.ImageUrl).HasMaxLength(255);
            builder.HasMany(e => e.Vacancies).WithOne(v => v.Employer).HasForeignKey(v => v.EmployerId);
            builder.HasMany(e => e.Chats).WithOne(c => c.Employer).HasForeignKey(c => c.EmployerId);
            builder.HasMany(e => e.Categories).WithMany(c => c.Employers)
                .UsingEntity(j => j.ToTable("EmployerCategories"));
        }
    }