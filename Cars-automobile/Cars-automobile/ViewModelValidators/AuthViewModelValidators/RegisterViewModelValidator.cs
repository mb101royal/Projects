using Cars_automobile.ViewModels.AuthViewModels;
using FluentValidation;

namespace Cars_automobile.ViewModelValidators.AuthViewModelValidators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {

            RuleFor(t => t.Fullname)
                .NotNull().WithMessage("Fullname cannot be null.")
                .NotEmpty().WithMessage("Fullname cannot be empty.")
                .MaximumLength(64).WithMessage("Fullname can have 64 symbols length.");
            RuleFor(t => t.Username)
                .NotNull().WithMessage("Username cannot be null.")
                .NotEmpty().WithMessage("Username cannot be empty.")
                .MaximumLength(32).WithMessage("Username can have 32 symbols length.");
            RuleFor(t => t.Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .MaximumLength(32).WithMessage("Email can have 32 symbols length.");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Your password cannot be empty")
                .NotNull().WithMessage("Your password cannot be null.")
                .MinimumLength(6).WithMessage("Your password length must be at least 6.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Equal("PasswordConfirm").WithMessage("Passwords don't match.");
        }
    }
}
