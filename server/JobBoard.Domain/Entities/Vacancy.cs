using JobBoard.Core.Enums;

namespace JobBoard.Core.Entities;

/// <summary>
/// Представляет вакансию, опубликованную работодателем.
/// </summary>
public class Vacancy
{
    /// <summary>
    /// Уникальный идентификатор вакансии.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Заголовок вакансии (обязательный, максимум 255 символов).
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание вакансии (обязательное, максимум 5000 символов).
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Зарплата по вакансии (десятичное значение).
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Тип занятости (полная, частичная, удаленная, фриланс).
    /// </summary>
    public EmploymentType EmploymentType { get; set; }

    /// <summary>
    /// URL изображения вакансии (опционально).
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Идентификатор категории, к которой относится вакансия.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Категория вакансии.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Идентификатор работодателя, создавшего вакансию.
    /// </summary>
    public Guid EmployerId { get; set; }

    /// <summary>
    /// Работодатель, создавший вакансию.
    /// </summary>
    public Employer Employer { get; set; }
}