using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JobBoard.Core.ValueObjects;

/// <summary>
/// Представляет контактную информацию (email, телефон, соцсети) с валидацией.
/// </summary>
public class ContactInfo
{
    /// <summary>
    /// Электронная почта (обязательная).
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Номер телефона (обязательный).
    /// </summary>
    public string PhoneNumber { get; private set; }

    /// <summary>
    /// URL социальной сети (опционально).
    /// </summary>
    public string SocialMediaUrl { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ContactInfo"/> с валидацией.
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    /// <param name="phoneNumber">Номер телефона.</param>
    /// <param name="socialMediaUrl">URL социальной сети (опционально).</param>
    /// <exception cref="ValidationException">Выбрасывается при неверном формате данных.</exception>
    public ContactInfo(string email, string phoneNumber, string socialMediaUrl)
    {
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ValidationException("Invalid email format.");
        if (!Regex.IsMatch(phoneNumber, @"^\+?\d{10,15}$"))
            throw new ValidationException("Invalid phone number format.");
        if (socialMediaUrl != null && !Uri.TryCreate(socialMediaUrl, UriKind.Absolute, out _))
            throw new ValidationException("Invalid social media URL.");

        Email = email;
        PhoneNumber = phoneNumber;
        SocialMediaUrl = socialMediaUrl;
    }
}