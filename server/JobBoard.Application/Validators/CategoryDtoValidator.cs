using FluentValidation;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Validators;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}