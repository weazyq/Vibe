using Microsoft.EntityFrameworkCore;
using Vibe.EF;
using Vibe.Services.Scooters;
using Vibe.Services.Scooters.Interface;
using Vibe.Services.Scooters.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Services
builder.Services.AddSingleton<IScootersService, ScootersService>();
builder.Services.AddSingleton<IScootersProvider, ScootersProvider>();
#endregion

#region Repositories
builder.Services.AddSingleton<IScootersRepository, ScootersRepository>();
#endregion

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql("").UseSnakeCaseNamingConvention());

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
