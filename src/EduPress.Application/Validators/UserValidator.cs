using EduPress.Core.Entities;
using FluentValidation;

namespace EduPress.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user)
                .NotEmpty();
        }
    }
}
