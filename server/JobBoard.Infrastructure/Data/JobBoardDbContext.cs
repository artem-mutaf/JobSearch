using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Data;

/// <summary>
/// Контекст данных для взаимодействия с базой данных приложения JobBoard.
/// </summary>
public class JobBoardDbContext : DbContext
{
    /// <summary>
    /// Коллекция соискателей.
    /// </summary>
    public DbSet<Applicant> Applicants { get; set; }

    /// <summary>
    /// Коллекция работодателей.
    /// </summary>
    public DbSet<Employer> Employers { get; set; }

    /// <summary>
    /// Коллекция вакансий.
    /// </summary>
    public DbSet<Vacancy> Vacancies { get; set; }

    /// <summary>
    /// Коллекция категорий (родов деятельности).
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Коллекция чатов между соискателями и работодателями.
    /// </summary>
    public DbSet<Chat> Chats { get; set; }

    /// <summary>
    /// Коллекция сообщений в чатах.
    /// </summary>
    public DbSet<Message> Messages { get; set; }

    /// <summary>
    /// Коллекция токенов для подтверждения электронной почты.
    /// </summary>
    public DbSet<EmailConfirmationToken> EmailConfirmationTokens { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="JobBoardDbContext"/>.
    /// </summary>
    /// <param name="options">Опции конфигурации контекста.</param>
    public JobBoardDbContext(DbContextOptions<JobBoardDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Настраивает модель данных, применяя конфигурации из сборки.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели EF Core.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobBoardDbContext).Assembly);
    }
}