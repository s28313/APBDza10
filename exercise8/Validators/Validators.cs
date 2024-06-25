
namespace kol2APBD.Validators;
using FluentValidation;

public static class Validators
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<PostDoctorRequestValidator>();
    }
}