using IncidentOpsCenter.Application.Interfaces;
using IncidentOpsCenter.Application.Mapping;
using IncidentOpsCenter.Infrastructure.Persistence;
using IncidentOpsCenter.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IncidentOpsCenterDbContext>(options =>
{
    options.UseInMemoryDatabase("IncidentOpsCenterDb");
});

builder.Services.AddAutoMapper(typeof(IncidentProfile).Assembly);

builder.Services.AddScoped<IIncidentQueryService, IncidentQueryService>();
builder.Services.AddScoped<IIncidentCommandService, IncidentCommandService>();

var app = builder.Build();

// ✅ Seed inicial InMemory usando DataSeeder
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IncidentOpsCenterDbContext>();
    DataSeeder.Seed(db); // <- NUEVO
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
