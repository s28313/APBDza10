using kol2APBD.DTOs;
using kol2APBD.Exceptions;
using kol2APBD.Services;

namespace kol2APBD.Endpoints;

public static class PrescriptionsEndpoints
{
    public static void RegisterPrescriptionsEndpoints(this WebApplication app)
    {
        app.MapGet("/{id:int}", GetPrescriptions);
    }

    public static async Task<IResult> GetPrescriptions(IPrescriptionsService service,int id)
    {
        try
        {
            var presc = await service.GetPrescription(id);
            return Results.Ok(presc);
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }
}