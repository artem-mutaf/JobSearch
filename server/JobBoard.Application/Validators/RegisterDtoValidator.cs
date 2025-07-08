using FluentValidation;
using JobBoard.Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(role => role == "Applicant" || role == "Employer")
            .WithMessage("Role must be either 'Applicant' or 'Employer'.");

        RuleFor(x => x.Applicant)
            .NotNull()
            .When(x => x.Role == "Applicant")
            .SetValidator(new ApplicantDtoValidator())
            .WithMessage("Applicant data is required for Applicant role.");

        RuleFor(x => x.Employer)
            .NotNull()
            .When(x => x.Role == "Employer")
            .SetValidator(new EmployerDtoValidator())
            .WithMessage("Employer data is required for Employer role.");

        RuleFor(x => x)
            .Must(x => (x.Role == "Applicant" && x.Applicant != null && x.Employer == null) ||
                       (x.Role == "Employer" && x.Employer != null && x.Applicant == null))
            .WithMessage("Exactly one of Applicant or Employer must be provided, matching the Role.");
    }
}