using JobBoard.Core.Enums;

namespace JobBoard.Application.DTOs;

public class VacancyDto
{
    public Guid Id { get; set; }
    public Guid EmployerId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public decimal Salary { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}