using FluentValidation;
using kol2APBD.DTOs;
using kol2APBD.Services;
using Microsoft.AspNetCore.Mvc;

namespace kol2APBD.Endpoints;


public static class UserEndpoints
{
    public static void RegisterUserEndpoints(this WebApplication app)
    {
        app.MapPost("/register", RegisterUser);
    }

    public static async Task<IResult> RegisterUser(IUserService service, IValidator<RegisterUserRequestDTO> validator, RegisterUserRequestDTO request)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            return Results.ValidationProblem(validation.ToDictionary());
        }

        int id = await service.RegisterUser(request);
        return Results.Created($"/users/{id}", new { id });
    }
}
