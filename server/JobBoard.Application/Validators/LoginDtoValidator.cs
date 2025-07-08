using FluentValidation;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(role => role == "Applicant" || role == "Employer")
            .WithMessage("Роль должна быть либо 'Applicant', либо 'Employer'.");
    }
}