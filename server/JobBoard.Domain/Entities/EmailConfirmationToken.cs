namespace JobBoard.Core.Entities;

/// <summary>
/// Представляет токен для подтверждения электронной почты.
/// </summary>
public class EmailConfirmationToken
{
    /// <summary>
    /// Уникальный идентификатор токена.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Электронная почта, для которой создан токен.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Уникальный токен подтверждения.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Дата и время истечения срока действия токена.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Флаг, указывающий, использован ли токен.
    /// </summary>
    public bool IsUsed { get; set; }

    /// <summary>
    /// Идентификатор пользователя (соискатель или работодатель).
    /// </summary>
    public Guid UserId { get; set; }
}