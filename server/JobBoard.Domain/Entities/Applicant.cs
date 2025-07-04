using JobBoard.Core.ValueObjects;

namespace JobBoard.Core.Entities;

/// <summary>
/// Представляет соискателя, который ищет работу и может откликаться на вакансии.
/// </summary>
public class Applicant
{
    /// <summary>
    /// Уникальный идентификатор соискателя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Электронная почта соискателя (уникальная, обязательная).
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Полное имя соискателя (обязательное, максимум 100 символов).
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Контактная информация соискателя (email, телефон, соцсети).
    /// </summary>
    public ContactInfo ContactInfo { get; set; }

    /// <summary>
    /// Местоположение соискателя (адрес, регион).
    /// </summary>
    public Location Location { get; set; }

    /// <summary>
    /// Хэшированный пароль соискателя.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Флаг, указывающий, подтверждена ли почта соискателя.
    /// </summary>
    public bool IsEmailConfirmed { get; set; }

    /// <summary>
    /// Список категорий (родов деятельности), в которых соискатель ищет работу.
    /// </summary>
    public List<Category> Categories { get; set; } = new();

    /// <summary>
    /// URL резюме соискателя (опционально).
    /// </summary>
    public string ResumeUrl { get; set; }

    /// <summary>
    /// Список чатов, в которых участвует соискатель.
    /// </summary>
    public List<Chat> Chats { get; set; } = new();
}
