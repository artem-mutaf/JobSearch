using FluentValidation;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Validators;

public class EmployerDtoValidator : AbstractValidator<EmployerDto>
{
    public EmployerDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.EntityType).IsInEnum().WithMessage("EntityType must be a valid EntityType value (Physical or Legal).");
        RuleFor(x => x.CompanyDescription).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.About).MaximumLength(2000);
        RuleFor(x => x.ContactInfo).NotNull().SetValidator(new ContactInfoDtoValidator());
        RuleFor(x => x.Location).NotNull().SetValidator(new LocationDtoValidator());
        RuleFor(x => x.ImageUrl).MaximumLength(255);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Za-z0-9]").WithMessage("Password must contain at least one letter or number.");
    }
}

public class ContactInfoDtoValidator : AbstractValidator<ContactInfoDto>
{
    public ContactInfoDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(15);
        RuleFor(x => x.SocialMediaUrl).MaximumLength(255);
    }
}

public class LocationDtoValidator : AbstractValidator<LocationDto>
{
    public LocationDtoValidator()
    {
        RuleFor(x => x.Address).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Region).NotEmpty().MaximumLength(100);
    }
}