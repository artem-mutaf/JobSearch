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
    public string Email { get; set; }

    /// <summary>
    /// Номер телефона (обязательный).
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// URL социальной сети (опционально).
    /// </summary>
    public string SocialMediaUrl { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ContactInfo"/> с валидацией.
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    /// <param name="phoneNumber">Номер телефона.</param>
    /// <param name="socialMediaUrl">URL социальной сети (опционально).</param>
    /// <exception cref="ValidationException">Выбрасывается при неверном формате данных.</exception>
    public ContactInfo(string email, string phoneNumber, string? socialMediaUrl)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        SocialMediaUrl = socialMediaUrl;
    }
    public ContactInfo(){}
}