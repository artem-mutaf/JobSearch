using FluentValidation;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Validators;

public class VacancyDtoValidator : AbstractValidator<VacancyDto>
{
    public VacancyDtoValidator()
    {
        RuleFor(x => x.EmployerId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.EmploymentType).IsInEnum().WithMessage("EmploymentType must be a valid EmploymentType value (FullTime, PartTime, Remote, Freelance).");
        RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.ImageUrl).MaximumLength(255);
    }
}