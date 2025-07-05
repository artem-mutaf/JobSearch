using FluentValidation;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Validators;

public class VacancySearchDtoValidator : AbstractValidator<VacancySearchDto>
{
    public VacancySearchDtoValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        RuleFor(x => x.SortBy).Must(x => x == null || x == "CreatedAt" || x == "Salary")
            .WithMessage("SortBy must be 'CreatedAt' or 'Salary'.");
    }
}