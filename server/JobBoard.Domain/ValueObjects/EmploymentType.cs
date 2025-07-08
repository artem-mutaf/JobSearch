using System.ComponentModel.DataAnnotations;

namespace JobBoard.Core.ValueObjects;

/// <summary>
/// Представляет тип занятости (полная, частичная, удаленная, фриланс).
/// </summary>
public class EmploymentType
{
    /// <summary>
    /// Значение типа занятости.
    /// </summary>
    public Enums.EmploymentType Value { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EmploymentType"/> с валидацией.
    /// </summary>
    /// <param name="value">Тип занятости.</param>
    /// <exception cref="ValidationException">Выбрасывается при неверном типе занятости.</exception>
    public EmploymentType(Enums.EmploymentType value)
    {
        if (!Enum.IsDefined(typeof(Enums.EmploymentType), value))
            throw new ValidationException("Invalid employment type.");

        Value = value;
    }
}