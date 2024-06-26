using System.Text;
using FluentValidation;
using kol2APBD.Contexts;
using kol2APBD.DTOs;
using kol2APBD.Endpoints;
using kol2APBD.Exceptions;
using kol2APBD.Services;
using kol2APBD.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;// nie mam pojęcia czemu nie mogę go dodać, pobrałem pakiet i dodałem go, wciąz go nie widzi
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPrescriptionsService, PrescriptionsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.RegisterValidators();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseErrorHandlingMiddleware();
app.RegisterDoctorEndpoints();
app.RegisterPrescriptionsEndpoints();
app.RegisterUserEndpoints();

app.MapPost("/api/auth/register", async (IUserService userService, HttpContext context) =>
{
    try
    {
        var request = await context.Request.ReadFromJsonAsync<RegisterUserRequestDTO>();
        
        var userId = await userService.RegisterUser(request);
        
        context.Response.StatusCode = StatusCodes.Status201Created;
        await context.Response.WriteAsJsonAsync(new { UserId = userId });
    }
    catch (ValidationException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
    }
});

app.MapPost("/api/auth/refresh-token", async (IAuthService authService, HttpContext context) =>
{
    try
    {
        var refreshToken = await context.Request.ReadAsStringAsync();
        var newAccessToken = await authService.RefreshAccessToken(refreshToken);

        if (newAccessToken == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { Error = "Invalid refresh token" });
            return;
        }

        context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsJsonAsync(new { AccessToken = newAccessToken });
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
    }
});


app.Run();
