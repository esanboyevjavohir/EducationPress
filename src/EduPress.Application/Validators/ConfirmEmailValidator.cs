using EduPress.Application.Models.User;
using FluentValidation;

namespace EduPress.Application.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailModel>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(ce => ce.Token)
                .NotEmpty()
                .WithMessage("Your verification link is not valid");

            RuleFor(ce => ce.UserId)
                .NotEmpty()
                .WithMessage("Your verification link is not valid");
        }
    }
}
