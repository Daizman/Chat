using FluentValidation;

namespace Chat.Identity.Validators;

public class LoginValidator: AbstractValidator<LoginViewModel>
{
    public LoginValidator()
    {
        RuleFor(vm => vm.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required");

        RuleFor(vm => vm.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password is Required");
    }
}