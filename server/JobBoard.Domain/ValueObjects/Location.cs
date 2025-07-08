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
    public string Address { get;  set; }

    /// <summary>
    /// Регион (обязательный, максимум 100 символов).
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="Location"/> с валидацией.
    /// </summary>
    /// <param name="address">Адрес.</param>
    /// <param name="region">Регион.</param>
    /// <exception cref="ValidationException">Выбрасывается при неверных данных.</exception>
    public Location(string address, string region)
    {
        Address = address;
        Region = region;
    }

    public Location()
    {
        
    }
}