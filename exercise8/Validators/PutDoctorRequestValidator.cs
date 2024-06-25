using FluentValidation;
using kol2APBD.DTOs;

namespace kol2APBD.Validators;

public class PutDoctorRequestValidator : AbstractValidator<PutDoctorRequestDTO>
{
    public PutDoctorRequestValidator()
    {
        RuleFor(e => e.Id).NotNull().NotEmpty();
        RuleFor(e => e.FirstName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(e => e.LastName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(e => e.Email).NotNull().NotEmpty().MaximumLength(100);
    }
}