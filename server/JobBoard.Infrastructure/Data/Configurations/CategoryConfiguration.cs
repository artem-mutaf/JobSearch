using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Category"/> для Entity Framework Core.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <summary>
    /// Настраивает маппинг сущности <see cref="Category"/> на таблицу.
    /// </summary>
    /// <param name="builder">Конструктор сущности.</param>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(c => c.Name); // Для поиска и фильтрации
        builder.HasOne(c => c.ParentCategory).WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId).IsRequired(false);
    }
}