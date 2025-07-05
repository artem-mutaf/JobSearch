using FluentValidation;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Validators;

public class MessageDtoValidator : AbstractValidator<MessageDto>
{
    public MessageDtoValidator()
    {
        RuleFor(x => x.ChatId).NotEmpty();
        RuleFor(x => x.SenderId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(1000);
    }
}