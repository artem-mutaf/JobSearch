namespace JobBoard.Application.DTOs;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Guid> ApplicantIds { get; set; } = new();
    public List<Guid> EmployerIds { get; set; } = new();
    public List<Guid> VacancyIds { get; set; } = new();
}