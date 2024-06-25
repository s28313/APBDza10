using kol2APBD.Contexts;
using kol2APBD.Endpoints;
using kol2APBD.Services;
using kol2APBD.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPrescriptionsService, PrescriptionsService>();
builder.Services.RegisterValidators();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.RegisterDoctorEndpoints();
app.RegisterPrescriptionsEndpoints();
app.Run();
