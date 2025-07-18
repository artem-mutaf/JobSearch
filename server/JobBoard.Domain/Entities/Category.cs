﻿namespace JobBoard.Core.Entities;

/// <summary>
/// Представляет категорию рода деятельности (например, IT, маркетинг).
/// </summary>
public class Category
{
    /// <summary>
    /// Уникальный идентификатор категории.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название категории (обязательное, максимум 100 символов).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Список вакансий, относящихся к категории.
    /// </summary>
    public List<Vacancy> Vacancies { get; set; } = new();

    /// <summary>
    /// Список соискателей, связанных с категорией.
    /// </summary>
    public List<Applicant> Applicants { get; set; } = new();

    /// <summary>
    /// Список работодателей, связанных с категорией.
    /// </summary>
    public List<Employer> Employers { get; set; } = new();
}
