using Vibe.VirtualScooter.Data;
using Microsoft.EntityFrameworkCore;
using Vibe.VirtualScooter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLowerCaseNamingConvention()
);

builder.Services.AddScoped<VirtualScooterService>();
builder.Services.AddSingleton<IHostedService, ScooterScheduler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    VirtualScooterService? virtualScooterService = scope.ServiceProvider.GetService<VirtualScooterService>();
    if(virtualScooterService == null) throw new NullReferenceException($"При запуске приложения не удалось получить сервис инициализиации самоката: VirtualScooterService");

    virtualScooterService.Initialize();
}

app.MapControllers();

app.Run();
