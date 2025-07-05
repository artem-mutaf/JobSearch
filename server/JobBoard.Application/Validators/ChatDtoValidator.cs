using FluentValidation;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Validators;

public class ChatDtoValidator : AbstractValidator<ChatDto>
{
    public ChatDtoValidator()
    {
        RuleFor(x => x.ApplicantId).NotEmpty();
        RuleFor(x => x.EmployerId).NotEmpty();
        RuleFor(x => x.VacancyId).NotEmpty();
    }
}