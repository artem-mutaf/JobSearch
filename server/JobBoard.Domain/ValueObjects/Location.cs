using System.ComponentModel.DataAnnotations;

namespace JobBoard.Core.ValueObjects;

/// <summary>
/// Представляет местоположение (адрес, регион) с валидацией.
/// </summary>
public class Location
{
    /// <summary>
    /// Адрес (обязательный, максимум 255 символов).
    /// </summary>
    public string Address { get; private set; }

    /// <summary>
    /// Регион (обязательный, максимум 100 символов).
    /// </summary>
    public string Region { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="Location"/> с валидацией.
    /// </summary>
    /// <param name="address">Адрес.</param>
    /// <param name="region">Регион.</param>
    /// <exception cref="ValidationException">Выбрасывается при неверных данных.</exception>
    public Location(string address, string region)
    {
        if (string.IsNullOrWhiteSpace(address) || address.Length > 255)
            throw new ValidationException("Address is required and must not exceed 255 characters.");
        if (string.IsNullOrWhiteSpace(region) || region.Length > 100)
            throw new ValidationException("Region is required and must not exceed 100 characters.");

        Address = address;
        Region = region;
    }
}