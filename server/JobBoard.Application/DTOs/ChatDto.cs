namespace JobBoard.Application.DTOs;

public class ChatDto
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid EmployerId { get; set; }
    public Guid VacancyId { get; set; }
}