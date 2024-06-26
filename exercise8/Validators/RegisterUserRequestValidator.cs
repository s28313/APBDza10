using FluentValidation;
using kol2APBD.DTOs;


namespace kol2APBD.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequestDTO>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .MaximumLength(100).WithMessage("Login cannot exceed 100 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}