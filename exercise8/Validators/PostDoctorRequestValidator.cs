using FluentValidation;
using kol2APBD.DTOs;

namespace kol2APBD.Validators;

public class PostDoctorRequestValidator : AbstractValidator<PostDoctorRequestDTO>
{
    public PostDoctorRequestValidator()
    {
        RuleFor(e => e.FirstName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(e => e.LastName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(e => e.Email).NotNull().NotEmpty().MaximumLength(100);
    }
}