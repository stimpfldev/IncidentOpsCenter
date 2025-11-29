using IncidentOpsCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IncidentOpsCenter.Application.Mapping;
using IncidentOpsCenter.Application.Interfaces;
using IncidentOpsCenter.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IncidentOpsCenterDbContext>(options =>
{
    options.UseInMemoryDatabase("IncidentOpsCenterDb");
});

// AutoMapper (v12, con paquete de DI)
builder.Services.AddAutoMapper(typeof(IncidentProfile).Assembly);

// Service de consulta de incidentes
builder.Services.AddScoped<IIncidentQueryService, IncidentQueryService>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();
// Seed de datos iniciales en la base InMemory
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IncidentOpsCenterDbContext>();
    DbInitializer.Seed(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
