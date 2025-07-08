namespace JobBoard.Core.Entities;

/// <summary>
/// Представляет чат между соискателем и работодателем по конкретной вакансии.
/// </summary>
public class Chat
{
    /// <summary>
    /// Уникальный идентификатор чата.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор соискателя, участвующего в чате.
    /// </summary>
    public Guid ApplicantId { get; set; }

    /// <summary>
    /// Соискатель, участвующий в чате.
    /// </summary>
    public Applicant Applicant { get; set; }

    /// <summary>
    /// Идентификатор работодателя, участвующего в чате.
    /// </summary>
    public Guid EmployerId { get; set; }

    /// <summary>
    /// Работодатель, участвующий в чате.
    /// </summary>
    public Employer Employer { get; set; }

    /// <summary>
    /// Идентификатор вакансии, связанной с чатом.
    /// </summary>
    public Guid VacancyId { get; set; }

    /// <summary>
    /// Вакансия, связанная с чатом.
    /// </summary>
    public Vacancy Vacancy { get; set; }

    /// <summary>
    /// Список сообщений в чате.
    /// </summary>
    public List<Message> Messages { get; set; } = new();
}