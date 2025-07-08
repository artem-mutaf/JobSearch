namespace JobBoard.Core.Entities;

/// <summary>
/// Представляет сообщение в чате между соискателем и работодателем.
/// </summary>
public class Message
{
    /// <summary>
    /// Уникальный идентификатор сообщения.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Текст сообщения (обязательный, максимум 1000 символов).
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Идентификатор отправителя (соискатель или работодатель).
    /// </summary>
    public Guid SenderId { get; set; }

    /// <summary>
    /// Идентификатор чата, к которому относится сообщение.
    /// </summary>
    public Guid ChatId { get; set; }

    /// <summary>
    /// Чат, к которому относится сообщение.
    /// </summary>
    public Chat Chat { get; set; }

    /// <summary>
    /// Время отправки сообщения.
    /// </summary>
    public DateTime SentAt { get; set; }
}