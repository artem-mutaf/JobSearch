namespace JobBoard.Application.DTOs;

public class VacancySearchDto
{
    public Guid? CategoryId { get; set; } // Фильтр по категории
    public int PageNumber { get; set; } = 1; // Номер страницы
    public int PageSize { get; set; } = 10; // Размер страницы
    public string? SortBy { get; set; } // Например, "CreatedAt", "Salary"
    public bool SortDescending { get; set; } = false;
}