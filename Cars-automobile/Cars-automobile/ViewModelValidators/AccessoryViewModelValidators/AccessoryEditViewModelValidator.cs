using Cars_automobile.Models;
using FluentValidation;

namespace Cars_automobile.ViewModelValidators.AccessoryViewModelValidators
{
    public class AccessoryEditViewModelValidator : AbstractValidator<Accessory>
    {
        public AccessoryEditViewModelValidator()
        {

            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .NotNull().WithMessage("Title cannot be null.")
                .MaximumLength(16).WithMessage("Title can have 16 symbols length.");
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .NotNull().WithMessage("Description cannot be null.")
                .MaximumLength(64).WithMessage("Description can have 64 symbols length.");

        }
    }
}
