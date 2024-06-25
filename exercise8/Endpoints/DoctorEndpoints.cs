using FluentValidation;
using kol2APBD.DTOs;
using kol2APBD.Exceptions;
using kol2APBD.Services;
using kol2APBD.Validators;

namespace kol2APBD.Endpoints;

public static class DoctorEndpoints
{
    public static void RegisterDoctorEndpoints(this WebApplication app)
    {
        var docs = app.MapGroup("doctors");
        docs.MapGet("/add/{id:int}", GetDoctor);
        docs.MapPost("/", PostDoctor);
        docs.MapPut("/", PostDoctor);
        docs.MapDelete("/delete/{id:int}", DeleteDoctor);
    }

    public static async Task<IResult> GetDoctor(int id, IDoctorService service)
    {
        try
        {
            var doctor = await service.GetDoc(id);
            return Results.Ok(doctor);
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }

    public static async Task<IResult> PostDoctor(IDoctorService service, IValidator<PostDoctorRequestDTO> validator, PostDoctorRequestDTO request)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            return Results.ValidationProblem(validation.ToDictionary());
        }
        int id = await service.AddDoc(request);
        return Results.Created("doctors/"+id, request);
    }

    public static async Task<IResult> PutDoctor(IDoctorService service, IValidator<PutDoctorRequestDTO> validator, PutDoctorRequestDTO request)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            return Results.ValidationProblem(validation.ToDictionary());
        }
        try
        {
            await service.ReplaceDoc(request);
            return Results.NoContent();
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }

    public static async Task<IResult> DeleteDoctor(IDoctorService service,int id)
    {
        try
        {
            var doctor = await service.GetDoc(id);
            return Results.NoContent();
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }

}