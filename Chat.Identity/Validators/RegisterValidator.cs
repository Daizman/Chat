using FluentValidation;

namespace Chat.Identity.Validators;

public class RegisterValidator: AbstractValidator<RegisterViewModel>
{
    public RegisterValidator()
    {
        RuleFor(vm => vm.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required");

        RuleFor(vm => vm.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password is required");

        RuleFor(vm => vm.ConfirmPassword)
            .NotEmpty()
            .NotNull()
            .Equal(vm => vm.Password);
    }
}