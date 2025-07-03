using System.ComponentModel.DataAnnotations;

namespace JobBoard.Core.ValueObjects;

/// <summary>
/// Представляет хэшированный пароль с валидацией.
/// </summary>
public class PasswordHash
{
    /// <summary>
    /// Хэшированное значение пароля.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="PasswordHash"/> с валидацией.
    /// </summary>
    /// <param name="value">Хэшированный пароль.</param>
    /// <exception cref="ValidationException">Выбрасывается, если хэш пустой или слишком длинный.</exception>
    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 255)
            throw new ValidationException("Password hash is required and must not exceed 255 characters.");

        Value = value;
    }
}