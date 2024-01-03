using Microsoft.EntityFrameworkCore;
using Vibe.EF;
using Vibe.EF.Entities;
using Vibe.Services.Scooters;
using Vibe.Services.Scooters.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Services
builder.Services.AddScoped<IScootersService, ScootersService>();

builder.Services.AddSingleton<IScootersProvider, ScootersProvider>();
builder.Services.AddHttpClient<IScootersProvider, ScootersProvider>();
#endregion

#region Repositories
builder.Services.AddScoped<IDataRepository<ScooterEntity>, ScooterRepository>();
#endregion

builder.Services.AddDbContext<DataContext>(options => options
    .UseNpgsql("Server=localhost;Database=vibe;User Id=postgres;Password=123")
    .UseLowerCaseNamingConvention()
);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
