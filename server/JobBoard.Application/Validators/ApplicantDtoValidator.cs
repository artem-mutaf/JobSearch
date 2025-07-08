using FluentValidation;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Validators;

public class ApplicantDtoValidator : AbstractValidator<ApplicantDto>
{
    public ApplicantDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ContactInfo).NotNull().SetValidator(new ContactInfoDtoValidator());
        RuleFor(x => x.Location).NotNull().SetValidator(new LocationDtoValidator());
        RuleFor(x => x.ResumeUrl).MaximumLength(255);
        RuleFor(x => x.CategoryIds).NotNull();
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Za-z0-9]").WithMessage("Password must contain at least one letter or number.");
    }
}