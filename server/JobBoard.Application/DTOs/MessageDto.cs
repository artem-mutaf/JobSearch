namespace JobBoard.Application.DTOs;

public class MessageDto
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }
}