using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Vacancy"/> для Entity Framework Core.
/// </summary>
public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
{
    /// <summary>
    /// Настраивает маппинг сущности <see cref="Vacancy"/> на таблицу.
    /// </summary>
    /// <param name="builder">Конструктор сущности.</param>
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder.ToTable("Vacancies");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Title).IsRequired().HasMaxLength(255);
        builder.HasIndex(v => v.Title); // Для поиска
        builder.Property(v => v.Description).IsRequired().HasMaxLength(5000);
        builder.Property(v => v.Salary).IsRequired().HasColumnType("decimal(18,2)");
        builder.HasIndex(v => v.Salary); // Для сортировки
        builder.Property(v => v.EmploymentType).IsRequired().HasConversion<string>();
        builder.Property(v => v.ImageUrl).HasMaxLength(255);
        builder.HasOne(v => v.Category).WithMany(c => c.Vacancies).HasForeignKey(v => v.CategoryId);
        builder.HasOne(v => v.Employer).WithMany(e => e.Vacancies).HasForeignKey(v => v.EmployerId);
        builder.HasIndex(v => v.CategoryId); // Для фильтрации
    }
}