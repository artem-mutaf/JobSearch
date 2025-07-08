namespace JobBoard.Core.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при ошибках валидации на доменном уровне.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Список сообщений об ошибках валидации.
    /// </summary>
    public IReadOnlyList<string> Errors { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ValidationException"/> с одним сообщением.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    public ValidationException(string message) : base(message)
    {
        Errors = new List<string> { message }.AsReadOnly();
    }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ValidationException"/> с несколькими сообщениями.
    /// </summary>
    /// <param name="errors">Список сообщений об ошибках.</param>
    public ValidationException(IEnumerable<string> errors) : base(string.Join("; ", errors))
    {
        Errors = errors.ToList().AsReadOnly();
    }
}