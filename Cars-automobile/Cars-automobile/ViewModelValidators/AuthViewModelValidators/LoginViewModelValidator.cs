using Cars_automobile.ViewModels.AuthViewModels;
using FluentValidation;

namespace Cars_automobile.ViewModelValidators.AuthViewModelValidators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {

            RuleFor(t => t.UsernameOrEmail)
                .NotNull().WithMessage("Username or Email field cannot be null.")
                .NotEmpty().WithMessage("Username or Email field cannot be empty.")
                .MaximumLength(32).WithMessage("Username or Email field can have 32 symbols length.");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Your password cannot be empty")
                .NotNull().WithMessage("Your password cannot be null.")
                .MinimumLength(6).WithMessage("Your password length must be at least 6.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }
    }
}
